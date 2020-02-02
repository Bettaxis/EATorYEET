using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.ParticleSystemJobs;

public class MunchParticles : MonoBehaviour
{
    [SerializeField] private Color particleColour;
    private ParticleSystem munchParticleSystem;
    void Start()
    {
        munchParticleSystem = gameObject.GetComponent<ParticleSystem>();
    }

    /*
    private void Update() {
        if(Input.GetKeyDown(KeyCode.Space)){
            fireParticles();
        }
    }
    */

    public void fireParticles(){
        munchParticleSystem.Play();
    }
}
