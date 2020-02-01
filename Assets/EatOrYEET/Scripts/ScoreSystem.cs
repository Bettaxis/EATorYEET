using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    private int _currentScore;
    private float _globalScoreMultiplier;
    private Dictionary<sFood.FoodCategory, float> _foodCategoryMultipliers;

    // Start is called before the first frame update
    void Start()
    {
        _currentScore = 0;
        _globalScoreMultiplier = 1f;
        _foodCategoryMultipliers = new Dictionary<sFood.FoodCategory, float>();
    }

    public void AdjustScore(FoodItem food, bool addToScore)
    {
        int scoreValue = 1;

        // Checking here because the food scriptable objects have not been set up yet
        if(food.foodScriptableObject != null)
        {
            scoreValue = food.foodScriptableObject.pointValue;
        }
        else
        {
            Debug.Log("ScoreSystem::AdjustScore - Food Object does not have food scriptable object set: " + food.gameObject.name);
        }

        if(!addToScore)
        {
            scoreValue *= -1;
        }

        scoreValue = (int)(scoreValue * _globalScoreMultiplier);

        _currentScore += scoreValue;

        // Debugging purposes. Remove when UI is added.
        Debug.Log("ScoreSystem::AdjustScore - Player Score is now: " + _currentScore);
    }

    public int GetScore()
    {
        return _currentScore;
    }

    // multiplier is the value by which to multiply the score
    // duration is the number of seconds to set the multiplier for
    public void SetGlobalMultiplier(float multiplier, float duration)
    {
        if(multiplier == 0)
        {
            Debug.LogError("ScoreSystem::SetMultiplier - multiplier should not be 0!");
            return;
        }

        if(duration <= 0)
        {
            Debug.LogError("ScoreSystem::SetMultiplier - duration should be greater than 0!");
            return;
        }

        StartCoroutine(TemporaryScoreMultiplier(multiplier, duration));
    }

    private IEnumerator TemporaryScoreMultiplier(float multiplier, float duration)
    {
        _globalScoreMultiplier *= multiplier;
        yield return new WaitForSeconds(duration);
        _globalScoreMultiplier /= multiplier;

        yield return null;
    }
}
