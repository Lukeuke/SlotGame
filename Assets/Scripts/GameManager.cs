using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
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
    public GameObject customItemStopObject;
    public GameObject itemContainer;
    public Sprite[] customSprites;
    public bool isPlaying;

    #region animations

    [Header("Animations")]
    public bool animation1 = false;
    public bool animation2 = false;
    public bool animation3 = false;
    public bool animation4 = false;

    #endregion
    
    private GameObject[] _itemObject;
    [SerializeField] private List<int> customSpriteIndex = new List<int>();
    [SerializeField] private TextAsset _winningItemJson;

    public CustomItemsDto _customItemsDto = new CustomItemsDto();
   // private GameManagerHttp _gameManagerHttp = new GameManagerHttp();
    
    public void OnPlayGame()
    {
        if (isPlaying)
        {
            StartCoroutine(StopPlaying());
            OnStopGame();
        }
        else
        {
            StartCoroutine(StartPlaying());
            OnStartGame();
        }
    }

    private void Start()
    {
        _customItemsDto = JsonUtility.FromJson<CustomItemsDto>(_winningItemJson.text);
        isPlaying = false;
        SpawnCustomItems();
    }

    private void OnStopGame()
    {
        isPlaying = false;
        stopObject.SetActive(true);
    }

    private void OnStartGame()
    {
        stopObject.SetActive(false);
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
            }
        }
    }

    private int GetIndexFromJson(int index)
    {
        return _customItemsDto.winningItem.dimensions[index];
    }

    public void SetCustomSprite()
    {
        _itemObject = GameObject.FindGameObjectsWithTag("WinSymbol");

        foreach (GameObject go in _itemObject)
        {
            SpriteRenderer spriteRenderer = go.GetComponent<SpriteRenderer>();
            
            if (go.name == "Item 0 0") 
                spriteRenderer.sprite = customSprites[GetIndexFromJson(0)];
            else if (go.name == "Item 0 1")
                spriteRenderer.sprite = customSprites[GetIndexFromJson(1)];
            else if(go.name == "Item 0 2")
                spriteRenderer.sprite = customSprites[GetIndexFromJson(2)];
            else if(go.name == "Item 1 0")
                spriteRenderer.sprite = customSprites[GetIndexFromJson(3)];
            else if(go.name == "Item 1 1")
                spriteRenderer.sprite = customSprites[GetIndexFromJson(4)];
            else if(go.name == "Item 1 2")
                spriteRenderer.sprite = customSprites[GetIndexFromJson(5)];
            else if (go.name == "Item 2 0") 
                spriteRenderer.sprite = customSprites[GetIndexFromJson(6)];
            else if (go.name == "Item 2 1") 
                spriteRenderer.sprite = customSprites[GetIndexFromJson(7)];
            else if (go.name == "Item 2 2") 
                spriteRenderer.sprite = customSprites[GetIndexFromJson(8)];
        }
    }
    
    public void CheckForWin()
    {
        _itemObject = GameObject.FindGameObjectsWithTag("WinSymbol");
        
        foreach (GameObject go in _itemObject)
        {
            SpriteRenderer spriteRenderer = go.GetComponent<SpriteRenderer>();

            if (go.name == "Item 0 0" && spriteRenderer.sprite == customSprites[1])
            {
                print("win");
            }
        }
    }
    
    private IEnumerator ButtonWait()
    {
        yield return new WaitForSeconds(1.5f);
        playButton.gameObject.SetActive(true);
    }

    private IEnumerator StopPlaying()
    {
        isPlaying = false;
        customItemStopObject.SetActive(false);
        playButton.gameObject.SetActive(false);
        StartCoroutine(ButtonWait());
        yield return new WaitForSeconds(0.22f); // deafult 0.22f
        itemContainer.SetActive(false);

        playText.text = "Play";
        Debug.Log($"{isPlaying} stopped");
    }

    private IEnumerator StartPlaying()
    {
        customItemStopObject.SetActive(true);
        if (isPlaying)
        {
            itemContainer.transform.position = new Vector3(itemContainer.transform.position.x,
                0f, itemContainer.transform.position.z);
        }
        
        yield return new WaitForSeconds(0.1f); // deafult 0.1f
        itemContainer.SetActive(true);
        isPlaying = true;
        playText.text = "Stop";
        Debug.Log($"{isPlaying} started");
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
