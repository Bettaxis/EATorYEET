using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRotation : MonoBehaviour
{
    public Vector3 rotationEulerIncrease;
    private Vector3 newEuler = new Vector3();

    // Update is called once per frame
    void Update()
    {
        newEuler += rotationEulerIncrease;
        transform.eulerAngles = newEuler;
    }
}
