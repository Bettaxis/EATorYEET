using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Building on YeetHands, on collision with food - check the controller's velocity magnitude. 
/// If it's over a certain threshold, we decide that the player is attempting to yeet, and we apply that velocity directly to the object
/// </summary>

public class cYeetHands : MonoBehaviour
{

    [SerializeField]
    private OVRInput.Controller m_controller;

    [SerializeField] private float minVelocityToYeet = 1f;

    [SerializeField] private float yeetForce = 1f; //the amount of force we add to a collision over the threshold velocity

    private FoodItem currentCollidingFoodItem;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<FoodItem>())
        {
            GameObject foodObject = collision.collider.gameObject;

            Vector3 controllerVector = OVRInput.GetLocalControllerVelocity(m_controller);

            Debug.Log("controller vel: " + controllerVector.magnitude);

            if (controllerVector.magnitude > minVelocityToYeet)
            {
                //if the magnitude of the velocity vector is more than our cutoff, apply the vector directly to object
                Debug.Log("applying controller velocity to " + collision.collider.gameObject.name);

                
                foodObject.GetComponent<Rigidbody>().AddForce(controllerVector * yeetForce, ForceMode.Impulse);
            }
        }
    }
}
