using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomRowItem : MonoBehaviour
{
    public static CustomRowItem Instance;
    
    public string itemName;
    public int index;
    public Sprite[] customSprites;
    public SpriteRenderer _spriteRenderer;
    public Rigidbody2D rb;

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
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        
    }

    private string GetSpriteName()
    {
        Sprite sprite = _spriteRenderer.sprite;
        itemName = sprite.name;
        return itemName;
    }
}
