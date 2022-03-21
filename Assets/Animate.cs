using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animate : MonoBehaviour
{
    private void FixedUpdate()
    {
        if(GameManager.Instance.animation1)
            gameObject.transform.Translate(0.3f, 0f * Time.deltaTime, 0f, Space.World);
        else if (GameManager.Instance.animation2)
            gameObject.transform.Translate(0.1f, 0f * Time.deltaTime, 0f, Space.World);
        else if (GameManager.Instance.animation3)
            gameObject.transform.Translate(-0.3f, 0f * Time.deltaTime, 0f, Space.World);
        else if (GameManager.Instance.animation4)
            gameObject.transform.Translate(-0.1f, 0f * Time.deltaTime, 0f, Space.World);
    }
}
