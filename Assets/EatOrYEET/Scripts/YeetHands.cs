using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YeetHands : MonoBehaviour
{
    [SerializeField]
    protected OVRInput.Controller m_controller;
    
    public float xVelocity = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.GetComponent<FoodItem>())
        {   //FoodItem detected in collision
            if (OVRInput.GetLocalControllerVelocity(m_controller).x > xVelocity)
            {   // if the controller is moving faster than xVelocity then add that velocity to the food item
                other.transform.position += OVRInput.GetLocalControllerVelocity(m_controller); 
            }    
        }

    }
}
