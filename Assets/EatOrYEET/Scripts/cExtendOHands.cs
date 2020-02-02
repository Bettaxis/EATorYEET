using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Shoots hand target forward on button press
/// </summary>

public class cExtendOHands : MonoBehaviour
{
    [SerializeField] private OVRInput.Controller controller;
    [SerializeField] private Transform startPos; 
    [SerializeField] private Transform forwardPos; 


    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, controller))
        {
            MoveHandsForward();
        }


        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, controller))
        {
            MoveHandsBack();
        }


    }

    void MoveHandsForward()
    {
        transform.position = forwardPos.position;
    }
    
    void MoveHandsBack()
    {
       transform.position = startPos.position;
    }

}
