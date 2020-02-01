using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleLog: MonoBehaviour
{
    [Header("References:")]
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI text;
    
    private bool isInit;
    private float lifetimeOfLogInSeconds;
    private float accumulatedTime;
    
    public void Initialize(Sprite _sprite, String _message, float _durationInSeconds = 3f)
    {
        ChangeSprite(_sprite);
        ChangeText(_message);
        SetLogDuration(_durationInSeconds);

        isInit = true;
    }

    private void Update()
    {
        if (!isInit)
            return;

        accumulatedTime += Time.deltaTime;

        if (accumulatedTime >= lifetimeOfLogInSeconds)
        {
            Destroy(gameObject);
        }
    }

    private void SetLogDuration(float _durationInSeconds)
    {
        lifetimeOfLogInSeconds = _durationInSeconds;
    }
    
    private void ChangeSprite(Sprite _sprite)
    {
        image.sprite = _sprite;
    }
    
    private void ChangeText(string _text)
    {
        text.text = _text;
    }
}
