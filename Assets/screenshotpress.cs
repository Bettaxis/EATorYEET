using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class screenshotpress : MonoBehaviour
{
    private void Reset()
    {
        ScreenCapture.CaptureScreenshot(Application.persistentDataPath + ".png", 3);
     }

    void OnMouseDown()
    {
        ScreenCapture.CaptureScreenshot(Application.persistentDataPath, 4);
    }
}
