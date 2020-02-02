using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorComponent : MonoBehaviour
{
    [SerializeField]
    private ScoreSystem _scoreSystem;

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.GetComponent<FoodItem>() != null)
        {
            if(GetComponent<PlayAudioSoundsList>() != null)
            {
                GetComponent<PlayAudioSoundsList>().PlaySound();
                Debug.Log("FloorComponent made a sound!");
            }

            if(_scoreSystem != null)
            {
                _scoreSystem.AdjustScore(other.gameObject.GetComponent<FoodItem>(), false);
            }
            else 
            {
                Debug.Log("Score System was not assigned to Floor. Score is not tracked");
            }

            Debug.Log("FloorComponent::OnTriggerEnter - " + other.gameObject.name + " is deactivated!");
            other.gameObject.SetActive(false);
        }   
    }
}
