using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodHoleSounds : MonoBehaviour
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
                Debug.Log("FoodHoleSounds made a sound!");
            }

            if(_scoreSystem != null)
            {
                _scoreSystem.AdjustScore(other.gameObject.GetComponent<FoodItem>(), true);
            }
            else 
            {
                Debug.Log("Score System was not assigned to Food Hole. Score is not tracked");
            }
        }  

        if (other.gameObject.GetComponent<PowerUp>() != null)
        {
            if(_scoreSystem != null)
            {
                PowerUp powerup = other.gameObject.GetComponent<PowerUp>();
                _scoreSystem.SetGlobalMultiplier(powerup.GetScoreMultiplier(), powerup.GetMultiplierDuration());
            }
            else 
            {
                Debug.Log("Score System was not assigned to Food Hole. Score is not tracked");
            }
        }    
    }
}
