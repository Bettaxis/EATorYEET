using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodItem : MonoBehaviour
{
    //[SerializeField]
    //private FoodItem
    public float speed = 0.25f;

    public float distanceFromPlayer = 0.3f; 
    public Transform playerTransform;
    public bool moving = true;

    private Rigidbody rigidbody;

    IEnumerator MoveCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        MoveCoroutine = MoveTowardsPlayer();
        rigidbody = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.name == "FoodHole")
        {
            Debug.Log(this.gameObject.name + " is destroyed!");
            Destroy(this.gameObject);
        }  

        else if(other.gameObject.name == "YeetZone")
        { 
            Debug.Log("Yeet Zone Triggered!");
            //rigidbody.AddForce(Vector3.back, ForceMode.Impulse);
            rigidbody.AddExplosionForce(400.0f, other.transform.position, 10f, 200f);
            //moving = false;
        } 
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        // if (moving)
        // {
        //     StartCoroutine(MoveCoroutine);
        // }

        if (moving)
        MoveTowardsPlayerFunction();
    }

    private IEnumerator MoveTowardsPlayer()
    {   
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, step);

        if (Vector3.Distance(transform.position, playerTransform.position) <= 1.5f)
        {
            moving = false;
        }

        yield return null;
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
