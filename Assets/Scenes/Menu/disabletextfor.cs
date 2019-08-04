using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disabletextfor : MonoBehaviour
{
    private float timer = 5f;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<CanvasRenderer>().SetAlpha(0f);
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            GetComponent<CanvasRenderer>().SetAlpha(Math.Min(GetComponent<CanvasRenderer>().GetAlpha()+.1f,1f));
        }
    }
}
