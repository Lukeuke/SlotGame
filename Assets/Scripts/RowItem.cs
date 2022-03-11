using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RowItem : MonoBehaviour
{
    public Sprite[] sprites;

    [SerializeField] private SpriteRenderer _spriteRenderer;
    private float _intervalTime = 0.5f; // default: 0.15f

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
    }
    
    private void Update()
    {
        transform.Translate(0f, -_intervalTime, 0f, Space.World);

        if (gameObject.transform.position.y <= -4f)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x,
                4f, gameObject.transform.position.z);

            _spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        }
    }
}
