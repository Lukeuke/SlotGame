using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomRowItem : MonoBehaviour
{
    public static CustomRowItem Instance;
    
    public string itemName;
    public Sprite[] customSprites;
    public SpriteRenderer _spriteRenderer;
    private void Start()
    {
        GameManager.Instance.SetCustomSprite();
        GetSpriteName();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("ResultCheck"))
        {
            Debug.Log(itemName);
            GameManager.Instance.CheckForWin();
        }

        if (col.gameObject.CompareTag("Destroy"))
        {
            // GameManager.Instance.customStopObject.SetActive(true);
            gameObject.transform.position = new Vector3(transform.position.x, 9f, transform.position.z);
        }
    }
    
    private string GetSpriteName()
    {
        Sprite sprite = _spriteRenderer.sprite;
        itemName = sprite.name;
        return itemName;
    }
}
