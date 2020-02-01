using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorComponent : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.GetComponent<FoodItem>() != null)
        {
            if(GetComponent<PlayAudioSoundsList>() != null)
            {
                GetComponent<PlayAudioSoundsList>().PlaySound();
                Debug.Log("FloorComponent made a sound!");
            }

            Destroy(other);
        }   
    }
}
