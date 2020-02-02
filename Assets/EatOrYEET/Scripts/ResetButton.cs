using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetButton : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.GetComponent<cYeetHands>() != null)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
