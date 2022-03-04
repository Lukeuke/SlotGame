using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public List<Transform> rowSpawner;
    public List<Transform> customRowSpawner;
    /*public GameObject rowItem;*/
    public GameObject customRowItem;
    public Button playButton;
    public Text playText;
    public GameObject stopObject;
    public GameObject customStopObject;
    public GameObject itemContainer;
    public Transform target;
    
    public List<string> winningItemName = new List<string>()
    {
        "Item 0", "Item 1", "Item 2"
    };
    
    private bool _isPlaying;
    private float _spawnInterval = 0.1f;
    private int _rowsCount = 3;
    private RowItem _rowItem;
    private CustomRowItem _customRowItem;
    private float speed = 0.1f;
    
    public void OnPlayGame()
    {
        stopObject.SetActive(false);
        
        if (_isPlaying)
        {
            StopPlaying();
        }
        else
        {
            StartPlaying();
            StartCoroutine(SpawnCustomItems());
        }
    }
    
    private IEnumerator SpawnRowItems()
    {
        Debug.Log("Coroutine started");
        _isPlaying = true;

        while (_isPlaying)
        {
            itemContainer.SetActive(true);
            for (int i = 0; i < rowSpawner.Count; i++)
            {
                /*Instantiate(rowItem, rowSpawner[i].position, Quaternion.identity);*/
            }
            yield return new WaitForSeconds(_spawnInterval);
        }

        yield return new WaitForSeconds(_spawnInterval);

        for (int i = 0; i < _rowsCount; i++)
        {
            OnStopGame();
            stopObject.SetActive(true);
        }
    }

    private void OnStopGame()
    {
        _isPlaying = false;
        /*itemContainer.transform.position =
            Vector3.MoveTowards(itemContainer.transform.position, target.position, speed);*/
        
        Debug.Log("On stop game");
    }

    private IEnumerator SpawnCustomItems()
    {
        for (int i = 0; i < rowSpawner.Count; i++)
        {
            for (int j = 0; j < rowSpawner.Count; j++)
            {
                var spawnedItem = Instantiate(customRowItem, customRowSpawner[i].position, Quaternion.identity);
                spawnedItem.name = $"Item {i} {j}";
                winningItemName[i] = spawnedItem.name;
                yield return new WaitForSeconds(0.02f);
            }
            yield return new WaitForSeconds(0.02f);
        }
    }

    public void CheckForWin()
    {
        // Creates Array of Game Objects
        GameObject[] itemObject = GameObject.FindGameObjectsWithTag("WinningItem");
        
        foreach (GameObject go in itemObject)
        {
            SpriteRenderer sprite = go.GetComponent<SpriteRenderer>();
            
            if (go.name == "Item 0 0") 
                sprite.sprite = _customRowItem.customSprites[1];
            else if (go.name == "Item 0 1")
                sprite.sprite = _customRowItem.customSprites[2];
            else if(go.name == "Item 2")
                sprite.sprite = _customRowItem.customSprites[3];
        }
        
    }
    
    private IEnumerator ButtonWait()
    {
        yield return new WaitForSeconds(2f);
        playButton.gameObject.SetActive(true);
    }

    public void GetWinningSprite(SpriteRenderer spriteRenderer, Sprite[] customSprites, int index)
    {
        index = 2;
        spriteRenderer.sprite = customSprites[index];
    }

    private void StopPlaying()
    {
        _isPlaying = false;
        customStopObject.SetActive(false);
        itemContainer.SetActive(false);
        StopCoroutine(SpawnRowItems());

        playButton.gameObject.SetActive(false);
        StartCoroutine(ButtonWait());
            
        playText.text = "Play";
        Debug.Log($"{_isPlaying} stopped");
    }

    private void StartPlaying()
    {
        customStopObject.SetActive(true);
        StartCoroutine(SpawnRowItems());
        _isPlaying = true;
        playText.text = "Stop";
        Debug.Log($"{_isPlaying} spawning");
    }
    
    private void Awake()
    {
        itemContainer.SetActive(false);
        
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
