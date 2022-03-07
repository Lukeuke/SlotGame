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
    public GameObject customRowItem;
    public Button playButton;
    public Text playText;
    public GameObject stopObject;
    public GameObject customStopObject;
    public GameObject itemContainer;
    public GameObject customItemContainer;
    public List<string> winningItemName = new List<string>();
    public Sprite[] customSprites;
    public bool _isPlaying;
    
    private float _spawnInterval = 0.1f;
    private RowItem _rowItem;
    private CustomRowItem _customRowItem;
    private GameObject[] itemObject;

    public void OnPlayGame()
    {
        if (_isPlaying)
        {
            StartCoroutine(StopPlaying());
            OnStopGame();
        }
        else
        {
            StartCoroutine(StartPlaying());
            OnStartGame();
            SpawnCustomItems();
        }
    }
    
    private IEnumerator SpawnRowItems()
    {
        Debug.Log("Coroutine started");
        _isPlaying = true;

        while (_isPlaying)
        {
            itemContainer.SetActive(true);
            yield return new WaitForSeconds(_spawnInterval);
        }
    }

    private void OnStopGame()
    {
        _isPlaying = false;
        stopObject.SetActive(true);
        customItemContainer.transform.position = new Vector3(customItemContainer.transform.position.x, -2f,
            customItemContainer.transform.position.z);
    }

    private void OnStartGame()
    {
        stopObject.SetActive(false);
        customItemContainer.transform.position = new Vector3(customItemContainer.transform.position.x, 4f,
            customItemContainer.transform.position.z);
    }

    private void SpawnCustomItems()
    {
        for (int i = 0; i < rowSpawner.Count; i++)
        {
            for (int j = 0; j < rowSpawner.Count; j++)
            {
                var vectorForItem = new Vector3(customRowSpawner[i].position.x, customRowSpawner[i].position.y + j,
                    customRowSpawner[i].position.z);
                var spawnedItem = Instantiate(customRowItem, vectorForItem, Quaternion.identity);
                spawnedItem.name = $"Item {i} {j}";
                winningItemName[i] = spawnedItem.name;
            }
        }
    }

    public void CheckForWin()
    {
        
    }

    public void SetCustomSprite()
    {
        itemObject = GameObject.FindGameObjectsWithTag("WinSymbol");
        
        foreach (GameObject go in itemObject)
        {
            SpriteRenderer spriteRenderer = go.GetComponent<SpriteRenderer>();
            
            if (go.name == "Item 0 0") 
                spriteRenderer.sprite = customSprites[1];
            else if (go.name == "Item 0 1")
                spriteRenderer.sprite = customSprites[2];
            else if(go.name == "Item 0 2")
                spriteRenderer.sprite = customSprites[3];
            else if(go.name == "Item 1 0")
                spriteRenderer.sprite = customSprites[3];
            else if(go.name == "Item 1 1")
                spriteRenderer.sprite = customSprites[5];
            else if(go.name == "Item 1 2")
                spriteRenderer.sprite = customSprites[6];
            else if(go.name == "Item 2 0")
                spriteRenderer.sprite = customSprites[2];
            else if(go.name == "Item 2 1")
                spriteRenderer.sprite = customSprites[4];
            else if(go.name == "Item 2 2")
                spriteRenderer.sprite = customSprites[3];
        }
    }
    
    private IEnumerator ButtonWait()
    {
        yield return new WaitForSeconds(1.5f);
        playButton.gameObject.SetActive(true);
    }

    private IEnumerator StopPlaying()
    {
        _isPlaying = false;
        customStopObject.SetActive(false);
        yield return new WaitForSeconds(0.22f); // Custom Items Delay
        itemContainer.SetActive(false);
        StopCoroutine(SpawnRowItems());

        playButton.gameObject.SetActive(false);
        StartCoroutine(ButtonWait());
            
        playText.text = "Play";
        Debug.Log($"{_isPlaying} stopped");
    }

    private IEnumerator StartPlaying()
    {
        if (_isPlaying)
        {
            itemContainer.transform.position = new Vector3(gameObject.transform.position.x,
                0f, gameObject.transform.position.z);
        }
        
        customStopObject.SetActive(true);
        yield return new WaitForSeconds(0.1f); // Custom Items Delay
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
