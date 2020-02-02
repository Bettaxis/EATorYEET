using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodHoleSounds : MonoBehaviour
{
    [SerializeField]
    private ScoreSystem _scoreSystem;

    [SerializeField]
    private Console _console;

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.GetComponent<FoodItem>() != null)
        {
            if(GetComponent<PlayAudioSoundsList>() != null)
            {
                GetComponent<PlayAudioSoundsList>().PlaySound();
                Debug.Log("FoodHoleSounds made a sound!");
            }

            FoodItem foodObj = other.gameObject.GetComponent<FoodItem>();

            if(_scoreSystem != null)
            {
                _scoreSystem.AdjustScore(foodObj, true);
            }
            else 
            {
                Debug.Log("Score System was not assigned to Food Hole. Score is not tracked");
            }

            if(_console != null)
            {
                string foodName = foodObj.foodScriptableObject.foodName;
                _console.CreateLog(foodName + Time.time, 9999999999f);
            }
            else 
            {
                Debug.Log("Console was not assigned to Food Hole. Items are not displayed on the terminal");
            }

            Debug.Log("FoodHoleSounds::OnTriggerEnter - " + other.gameObject.name + " is deactivated!");
            other.gameObject.SetActive(false);
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
