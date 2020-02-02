using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodItem : MonoBehaviour
{
    [SerializeField]
    public sFood foodScriptableObject;

    private Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = this.GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.name == "YeetZone")
        { 
            Debug.Log("Yeet Zone Triggered!");
            //rigidbody.AddForce(Vector3.back, ForceMode.Impulse);
            rigidbody.AddExplosionForce(400.0f, other.transform.position, 10f, 200f);
        } 
    }
}
