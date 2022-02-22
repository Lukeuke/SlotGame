using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Transform rowSpawner;
    public Transform rowSpawner2;
    public Transform rowSpawner3;
    public GameObject rowItem;
    public Button playButton;
    public Text playText;
    public GameObject stopObject;

    public List<string> items = new List<string>()
    {
        "bar", "cherry", "crown", "diamond", "lemon", "melon", "seven"
    };

    private bool _isPlaying;
    private RowItem _rowItem;
    
    public void OnPlayGame()
    {
        stopObject.SetActive(false);
        
        if (_isPlaying)
        {
            _isPlaying = false;
            StopCoroutine(SpawnRowItems());
            
            playButton.gameObject.SetActive(false);
            StartCoroutine(ButtonWait());
            
            playText.text = "Play";
            Debug.Log($"{_isPlaying} stopped");
        }
        else
        {
            StartCoroutine(SpawnRowItems());
            _isPlaying = true;
            playText.text = "Stop";
            Debug.Log($"{_isPlaying} spawning");
        }
    }

    private IEnumerator SpawnRowItems()
    {
        Debug.Log("Coroutine started");
        _isPlaying = true;

        while (_isPlaying)
        {
            yield return new WaitForSeconds(0.5f);

            Instantiate(rowItem, rowSpawner.position, Quaternion.identity);
            Instantiate(rowItem, rowSpawner2.position, Quaternion.identity);
            Instantiate(rowItem, rowSpawner3.position, Quaternion.identity);
        }

        yield return new WaitForSeconds(0.5f);
        OnStopGame();
        yield return new WaitForSeconds(0.3f);
        stopObject.SetActive(true);
    }

    private void OnStopGame() // Ta metoda ustawia 3 kolejne obrazki
    {
        _isPlaying = false;

        Instantiate(rowItem, rowSpawner.position, Quaternion.identity);
        Instantiate(rowItem, rowSpawner2.position, Quaternion.identity);
        Instantiate(rowItem, rowSpawner3.position, Quaternion.identity);

        Debug.Log("On stop game");
    }

    private IEnumerator ButtonWait()
    {
        yield return new WaitForSeconds(2f);
        playButton.gameObject.SetActive(true);
    }

    private void Awake()
    {
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
