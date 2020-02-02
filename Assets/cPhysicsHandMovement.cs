using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cPhysicsHandMovement : MonoBehaviour
{
    private Rigidbody rb;
    public Transform transformToFollow;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transformToFollow.position);
        rb.MoveRotation(transformToFollow.rotation);
    }
}
