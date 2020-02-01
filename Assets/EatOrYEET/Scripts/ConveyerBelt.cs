using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyerBelt : MonoBehaviour
{
    // References
    [Header("References")]
    public Material ConveyerBeltMaterial = null;

    // Settings
    [Header("Settings")]
    public float ConveyerSpeed = 1.0f;
    public bool ConveyerPause = false;
    
    // Data
    private float ConveyerOffset = 0.0f;

    // Behaviour
    void Update()
    {
        if (ConveyerBeltMaterial == null)
            return;

        if(!ConveyerPause)
            ConveyerOffset += Time.deltaTime * ConveyerSpeed;

        ConveyerBeltMaterial.SetVector("_Offset", new Vector4(0, ConveyerOffset, 0, 0));
    }
}
