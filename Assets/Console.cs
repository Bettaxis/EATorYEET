using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Console : MonoBehaviour
{
    [SerializeField] private ConsoleLog consoleLogPrefab;
    
    [SerializeField] private List<ConsoleLog> logs = new List<ConsoleLog>();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            CreateLog("I am a pizza");
        }
    }

    private void CreateLog(string message)
    {
        ConsoleLog log = Instantiate(consoleLogPrefab, transform);
        log.Initialize(this, message);
        logs.Add(log);
    }

    public void RemoveLog(ConsoleLog _log)
    {
        logs.Remove(_log);
        Destroy(_log.gameObject);
    }
}
