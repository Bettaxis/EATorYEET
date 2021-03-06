﻿using System;
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
        
        if (Input.GetKeyDown(KeyCode.F2))
        {
            CreateLog("I am a taco");
        }
        
        if (Input.GetKeyDown(KeyCode.F3))
        {
            CreateLog("Unity Hub keeps me up at night");
        }
        
        if (Input.GetKeyDown(KeyCode.F4))
        {
            CreateLog("I am a lemon");
        }
    }

    public void CreateLog(string message, float _durationInSeconds = 3f)
    {
        ConsoleLog log = Instantiate(consoleLogPrefab, content);
        log.transform.SetSiblingIndex(0);
        log.Initialize(null, message, _durationInSeconds);
        logs.Enqueue(log);
    }

    public void CreateLog(string message, Sprite sprite, float _durationInSeconds = 3f)
    {
        ConsoleLog log = Instantiate(consoleLogPrefab, content);
        log.transform.SetSiblingIndex(0);
        log.Initialize(sprite, message, _durationInSeconds);
        logs.Enqueue(log);
    }
}
