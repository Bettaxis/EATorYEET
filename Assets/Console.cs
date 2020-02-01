using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Console : MonoBehaviour
{
    [SerializeField] private ConsoleLog consoleLogPrefab;
    [SerializeField] private Transform content;

    private readonly Queue<ConsoleLog> logs = new Queue<ConsoleLog>();
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            CreateLog("I am a pizza");
        }
    }

    private void CreateLog(string message)
    {
        ConsoleLog log = Instantiate(consoleLogPrefab, content);
        log.Initialize(null, message);
        logs.Enqueue(log);
    }
}
