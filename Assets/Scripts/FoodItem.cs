using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodItem : MonoBehaviour
{
    public float speed = 0.2f;

    public float distanceFromPlayer = 0.75f; 
    public Transform playerTransform;
    public bool moving = true;

    // Temporary use. Remove when proper spawning system is implemented
    private bool isQuitting = false;

    private IEnumerator MoveCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        MoveCoroutine = MoveTowardsPlayer();
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


    void OnApplicationQuit()
    {
        isQuitting = true;
    }

    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    void OnDestroy()
    {
        // This is temporary. Replace this when the real spawning system is done.
        if(!isQuitting)
        {
            GameObject obj = Instantiate(gameObject, new Vector3(0.02890274f, 2.325924f, 6.049408f), Quaternion.identity);
            obj.transform.localScale = new Vector3(0.226374f, 0.226374f, 0.226374f);
            obj.GetComponent<FoodItem>().moving = true;
            obj.GetComponent<FoodItem>().enabled = true;
            obj.SetActive(true);
        }
    }
}
