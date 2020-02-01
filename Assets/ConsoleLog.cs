using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsoleLog : MonoBehaviour
{
    [SerializeField] private float displayForHowLongInSeconds = 3f;

    private float accumulatedTime;
    
    private void Update()
    {
        accumulatedTime += Time.deltaTime;

        if (accumulatedTime >= displayForHowLongInSeconds)
        {
            Destroy(gameObject);
        }
    }
}
