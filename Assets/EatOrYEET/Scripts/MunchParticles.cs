using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.ParticleSystemJobs;

public class MunchParticles : MonoBehaviour
{
    private Color particleColour;
    [SerializeField] private ParticleSystem munchParticleSystem;
    private void OnTriggerEnter(Collider other) {

        if(other.gameObject.GetComponent<FoodItem>() != null){
            particleColour = other.gameObject.GetComponent<FoodItem>().foodScriptableObject.foodColour;
            fireParticles();
        }
        else
        {
            return;
        }
    }
    public void fireParticles(){
        var main = munchParticleSystem.main;  
        main.startColor = particleColour;
        munchParticleSystem.Play();
    }
}
