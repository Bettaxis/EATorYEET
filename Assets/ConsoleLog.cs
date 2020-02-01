using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ConsoleLog : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private float displayForHowLongInSeconds = 3f;

    private Console console;
    private bool isInit;
    private float accumulatedTime;

    public void Initialize(Console _console, string message)
    {
        if (console == null)
        {
            Debug.LogWarning("Console log could not be created.");
        }

        console = _console;
        text.text = message;
        
        isInit = true;
    }
    
    private void Update()
    {
        if (!isInit)
        {
            return;
        }
            
        accumulatedTime += Time.deltaTime;

        if (accumulatedTime >= displayForHowLongInSeconds)
        {
            console.RemoveLog(this);
        }
    }
}
