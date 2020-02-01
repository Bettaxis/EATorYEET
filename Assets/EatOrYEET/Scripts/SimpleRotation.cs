using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRotation : MonoBehaviour
{
    public Vector3 rotationEulerIncrease;
    private Vector3 newEuler;

    private void Awake()
    {
        newEuler = transform.eulerAngles;
    }
    void Update()
    {
        newEuler += rotationEulerIncrease;
        transform.eulerAngles = newEuler;
    }
}
