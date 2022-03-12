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
    public Sprite[] customSprites;
    public bool isPlaying;
    
    private float _spawnInterval = 0.1f;
    private RowItem _rowItem;
    private CustomRowItem _customRowItem;
    private GameObject[] itemObject;

    [SerializeField] private TextAsset _winningItemJson;

    [SerializeField] private WinningItemDto _winningItemDto = new WinningItemDto();

    public WinningItemDto GetWinningItemDto()
    {
        _winningItemDto = JsonUtility.FromJson<WinningItemDto>(_winningItemJson.text);
        return _winningItemDto;
    }

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
            /*SpawnCustomItems();*/
        }
    }

    private void Start()
    {
        isPlaying = false;
        SpawnCustomItems();
    }

    private IEnumerator SpawnRowItems()
    {
        isPlaying = true;

        while (isPlaying)
        {
            itemContainer.SetActive(true);
            yield return new WaitForSeconds(_spawnInterval);
        }
    }

    private void OnStopGame()
    {
        isPlaying = false;
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
            }
        }
    }

    public void SetCustomSprite()
    {
        itemObject = GameObject.FindGameObjectsWithTag("WinSymbol");

        foreach (GameObject go in itemObject)
        {
            SpriteRenderer spriteRenderer = go.GetComponent<SpriteRenderer>();
            
            if (go.name == "Item 0 0") 
                spriteRenderer.sprite = customSprites[GetWinningItemDto().dimensions[0]];
            else if (go.name == "Item 0 1")
                spriteRenderer.sprite = customSprites[GetWinningItemDto().dimensions[1]];
            else if(go.name == "Item 0 2")
                spriteRenderer.sprite = customSprites[GetWinningItemDto().dimensions[2]];
            else if(go.name == "Item 1 0")
                spriteRenderer.sprite = customSprites[GetWinningItemDto().dimensions[3]];
            else if(go.name == "Item 1 1")
                spriteRenderer.sprite = customSprites[GetWinningItemDto().dimensions[4]];
            else if(go.name == "Item 1 2")
                spriteRenderer.sprite = customSprites[GetWinningItemDto().dimensions[5]];
            else if(go.name == "Item 2 0")
                spriteRenderer.sprite = customSprites[GetWinningItemDto().dimensions[6]];
            else if(go.name == "Item 2 1")
                spriteRenderer.sprite = customSprites[GetWinningItemDto().dimensions[7]];
            else if(go.name == "Item 2 2")
                spriteRenderer.sprite = customSprites[GetWinningItemDto().dimensions[8]];
        }
    }
    
    public void CheckForWin()
    {
        itemObject = GameObject.FindGameObjectsWithTag("WinSymbol");
        
        foreach (GameObject go in itemObject)
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
        customStopObject.SetActive(false);
        yield return new WaitForSeconds(0.22f); // deafult 0.22f
        itemContainer.SetActive(false);
        StopCoroutine(SpawnRowItems());

        playButton.gameObject.SetActive(false);
        StartCoroutine(ButtonWait());
            
        playText.text = "Play";
        Debug.Log($"{isPlaying} stopped");
    }

    private IEnumerator StartPlaying()
    {
        if (isPlaying)
        {
            itemContainer.transform.position = new Vector3(gameObject.transform.position.x,
                0f, gameObject.transform.position.z);
        }
        
        customStopObject.SetActive(true);
        yield return new WaitForSeconds(0.1f); // deafult 0.1f
        StartCoroutine(SpawnRowItems());
        isPlaying = true;
        playText.text = "Stop";
        Debug.Log($"{isPlaying} spawning");
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
