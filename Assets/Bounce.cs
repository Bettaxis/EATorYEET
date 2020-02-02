using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    [SerializeField] private float expStr;
    [SerializeField] private float expRds;

    private void OnTriggerEnter(Collider other)
    {
   
        other.attachedRigidbody.AddExplosionForce(expStr, transform.position, expRds, 3f, ForceMode.Impulse);
        Debug.Log("EXPLOSION");
    }
}
