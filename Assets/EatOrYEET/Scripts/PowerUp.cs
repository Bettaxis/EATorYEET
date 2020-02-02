using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _scoreMultiplier;

    [SerializeField]
    private float _multiplierDuration;
    
    public float speed = 0.25f;

    public float distanceFromPlayer = 0.3f; 
    public Transform playerTransform;
    public bool moving = true;

    private Rigidbody rigidbody;

    public float GetScoreMultiplier(){
        return _scoreMultiplier;
    }

    public float GetMultiplierDuration(){
        return _multiplierDuration;
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = this.GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.name == "FoodHole")
        {
            Debug.Log(this.gameObject.name + " is destroyed!");
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        if (moving)
        MoveTowardsPlayerFunction();
    }

    public void MoveTowardsPlayerFunction()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, step);

        if (Vector3.Distance(transform.position, playerTransform.position) <= distanceFromPlayer)
        {
            moving = false;
        }
    }

}
