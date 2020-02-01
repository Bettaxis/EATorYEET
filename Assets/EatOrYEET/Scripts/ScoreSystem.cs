using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    private int _currentScore;

    // Start is called before the first frame update
    void Start()
    {
        _currentScore = 0;
    }

    public void AdjustScore(FoodItem food, bool addToScore)
    {
        int scoreValue = 1;

        if(!addToScore)
        {
            scoreValue *= -1;
        }

        _currentScore += scoreValue;

        // Debugging purposes. Remove when UI is added.
        Debug.Log("Player Score is now: " + _currentScore);
    }

    public int GetScore()
    {
        return _currentScore;
    }
}
