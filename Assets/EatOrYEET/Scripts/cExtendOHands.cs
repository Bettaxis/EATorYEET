using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Shoots hand target forward on button press
/// </summary>

public class cExtendOHands : MonoBehaviour
{
    [SerializeField] private OVRInput.Controller controller;
    [SerializeField] private Transform backPos; 
    [SerializeField] private Transform forwardPos;

    [SerializeField] private float armExtendSpeed;
    private bool isLerping;
    private float lerpTimer;
    private float lerpLength;

    private Vector3 startPos;
    private Vector3 targetPos;

    private void Start()
    {
        startPos = backPos.position;
        targetPos = forwardPos.position;

        lerpLength = Vector3.Distance(backPos.position, forwardPos.position);

        transform.position = backPos.position;
    }

    void Update()
    {
        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, controller))
        {
            if (isLerping)
            {
                lerpTimer = 0;
                startPos = transform.position;
                targetPos = backPos.position;

                lerpLength = Vector3.Distance(transform.position, backPos.position);

            }

            else
            {
                MoveHandsBack();
            }
            
        }

        if (isLerping)
        {
            float distCovered = lerpTimer * armExtendSpeed;
            float journeyFraction = distCovered / lerpLength;
            transform.position = Vector3.Lerp(startPos, targetPos, journeyFraction);

            lerpTimer += Time.deltaTime;

            if(Mathf.Abs((transform.position - targetPos).magnitude) <= 0.01) //close enough to target
            {
                isLerping = false;

            }
        }


        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, controller))
        {
            if (isLerping)
            {
                lerpTimer = 0;
                startPos = transform.position;
                targetPos = forwardPos.position;

                lerpLength = Vector3.Distance(transform.position, forwardPos.position);

            }
            else
            {
                MoveHandsForward();
            }

            
        }

       
    }

    void MoveHandsForward()
    {
        lerpTimer = 0;
        isLerping = true;

        startPos = backPos.position;
        targetPos = forwardPos.position;
    }
    
    void MoveHandsBack()
    {
        lerpTimer = 0;
        isLerping = true;

        startPos = forwardPos.position;
        targetPos = backPos.position;

        lerpLength = Vector3.Distance(forwardPos.position, backPos.position);
    }

}
