using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RowItem : MonoBehaviour
{
    public Sprite[] sprites;
    public string ItemName;

    [SerializeField] private SpriteRenderer _spriteRenderer;
    
    private void Start()
    {
        _spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Destroy"))
        {
            Destroy(gameObject);
        }
        
        if (col.gameObject.CompareTag("ResultCheck"))
        {
            ItemName = _spriteRenderer.sprite.ToString();
            Debug.Log(ItemName);
        }
    }
}
