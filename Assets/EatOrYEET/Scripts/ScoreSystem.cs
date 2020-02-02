using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ScoreSystem : MonoBehaviour
{
    private int _currentScore;
    private float _globalScoreMultiplierBonus;
    private Dictionary<sFood.FoodCategory, float> _foodCategoryMultiplierBonus;
    private Dictionary<sFood.FoodCategory, GameObject> _foodCategoryMultiplierDisplays;
    private GameStateController _gameStateController = null;

    [SerializeField]
    private GameObject _totalScoreDisplay;

    [Header("Multiplier Display Settings")]

    // This is the prefab that will spawn all the multiplier displays
    [SerializeField]
    private GameObject _multipliersDisplayPrefab;

    // This is an object which will be the parent of the multiplier displays
    // This will allow us to easily change where the displays are spawned
    [SerializeField]
    private GameObject _multipliersDisplayParent;

    [SerializeField]
    private float _multipliersDisplayHoriontalSpacing = 2.5f;

    [SerializeField]
    private float _multipliersDisplayVerticalSpacing = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        _currentScore = 0;
        _globalScoreMultiplierBonus = 0f;

        // Set up initial multiplier bonuses for all Food Categories
        _foodCategoryMultiplierBonus = new Dictionary<sFood.FoodCategory, float>();
        sFood.FoodCategory[] foodCategories = (sFood.FoodCategory[])System.Enum.GetValues(typeof(sFood.FoodCategory));

        foreach(sFood.FoodCategory category in foodCategories){
            _foodCategoryMultiplierBonus[category] = 0f;
        }
        
        _foodCategoryMultiplierDisplays = new Dictionary<sFood.FoodCategory, GameObject>();

        SetUpMultipliersDisplay();
    }

    private void SetUpMultipliersDisplay()
    {
        sFood.FoodCategory[] foodCategories = (sFood.FoodCategory[])System.Enum.GetValues(typeof(sFood.FoodCategory));
        
        float horizontalMultiplierSpacing = 2.5f;
        float verticalMultiplierSpacing = 1.5f;
        float numMultipliersSoFar = 0;
        foreach(sFood.FoodCategory category in foodCategories){
            Vector3 newMultiplierDisplayPosn = Vector3.zero;
            newMultiplierDisplayPosn.y -= (numMultipliersSoFar % 5) * _multipliersDisplayVerticalSpacing;
            newMultiplierDisplayPosn.x += (numMultipliersSoFar % 2) * _multipliersDisplayHoriontalSpacing;
            
            GameObject newMultiplierDisplay = Instantiate(_multipliersDisplayPrefab, Vector3.zero, Quaternion.identity, _multipliersDisplayParent.transform);
            newMultiplierDisplay.transform.localPosition = newMultiplierDisplayPosn;
            newMultiplierDisplay.transform.localRotation = Quaternion.identity;
            ++numMultipliersSoFar;

            Transform canvasTransform = newMultiplierDisplay.transform.Find("Canvas");

            Transform multiplierNameTransform = canvasTransform.Find("Multiplier Name");
            TextMeshProUGUI multiplierNameTextMp = multiplierNameTransform.gameObject.GetComponent<TextMeshProUGUI>();
            multiplierNameTextMp.SetText(category.ToString());

            Transform multiplierAmountTransform = canvasTransform.Find("Multiplier Amount");
            TextMeshProUGUI multiplierAmountTextMp = multiplierAmountTransform.gameObject.GetComponent<TextMeshProUGUI>();
            multiplierAmountTextMp.SetText("x1");

            _foodCategoryMultiplierDisplays[category] = newMultiplierDisplay;
        }
    }

    private void UpdateMultipliersDisplay()
    {
        sFood.FoodCategory[] foodCategories = (sFood.FoodCategory[])System.Enum.GetValues(typeof(sFood.FoodCategory));
        foreach(sFood.FoodCategory category in foodCategories){
            GameObject mutiplierDisplay = _foodCategoryMultiplierDisplays[category];
            Transform canvasTransform = mutiplierDisplay.transform.Find("Canvas");
            Transform multiplierAmountTransform = canvasTransform.Find("Multiplier Amount");
            TextMeshProUGUI multiplierAmountTextMp = multiplierAmountTransform.gameObject.GetComponent<TextMeshProUGUI>();

            float multiplierAmount = _foodCategoryMultiplierBonus[category];

            if(multiplierAmount == 0f)
            {
                multiplierAmount = 1f;
            }

            multiplierAmountTextMp.SetText("x" + multiplierAmount);
        }
    }

    public void AdjustScore(FoodItem food, bool addToScore)
    {
        int scoreValue = 1;
        float totalCategoryMultiplierBonuses = 0;

        // Checking here because the food scriptable objects have not been set up yet
        if(food.foodScriptableObject != null)
        {
            scoreValue = food.foodScriptableObject.pointValue;

            foreach(sFood.FoodCategory category in food.foodScriptableObject.foodCategories)
            {
                totalCategoryMultiplierBonuses += _foodCategoryMultiplierBonus[category];
            }
        }
        else
        {
            Debug.Log("ScoreSystem::AdjustScore - Food Object does not have food scriptable object set: " + food.gameObject.name);
        }

        if(!addToScore)
        {
            scoreValue *= -1;
        }

        scoreValue += (int)(scoreValue * Math.Max(totalCategoryMultiplierBonuses - 1, 0));
        scoreValue += (int)(scoreValue * Math.Max(_globalScoreMultiplierBonus - 1, 0));

        _currentScore += scoreValue;

        // Debugging purposes. Remove when UI is added.
        Debug.Log("ScoreSystem::AdjustScore - Player Score is now: " + _currentScore);
        UpdateTotalScoreDisplay();

        if(_gameStateController != null)
        {
            if(_gameStateController.GetScoreToWin() <= _currentScore)
            {
                _gameStateController.EndGame();
            }
        }
    }

    public void SetGameStateController(GameStateController gameStateController)
    {
        _gameStateController = gameStateController;
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
            Debug.LogError("ScoreSystem::SetGlobalMultiplier - multiplier should not be 0!");
            return;
        }

        if(duration <= 0)
        {
            Debug.LogError("ScoreSystem::SetGlobalMultiplier - duration should be greater than 0!");
            return;
        }

        StartCoroutine(TemporaryGlobalScoreMultiplier(multiplier, duration));
    }

    private IEnumerator TemporaryGlobalScoreMultiplier(float multiplier, float duration)
    {
        _globalScoreMultiplierBonus += multiplier;
        UpdateMultipliersDisplay();

        yield return new WaitForSeconds(duration);
        _globalScoreMultiplierBonus -= multiplier;
        UpdateMultipliersDisplay();

        yield return null;
    }

    // multiplier is the value by which to multiply the score
    // duration is the number of seconds to set the multiplier for
    public void SetCategoryPermanentMultiplier(float multiplier, sFood.FoodCategory foodCategory)
    {
        if(multiplier == 0)
        {
            Debug.LogError("ScoreSystem::SetCategoryPermanentMultiplier - multiplier should not be 0!");
            return;
        }

        _foodCategoryMultiplierBonus[foodCategory] = multiplier;

        UpdateMultipliersDisplay();
    }

    // multiplier is the value by which to multiply the score
    // duration is the number of seconds to set the multiplier for
    public void SetCategoryTemporaryMultiplier(float multiplier, float duration, sFood.FoodCategory foodCategory)
    {
        if(multiplier == 0)
        {
            Debug.LogError("ScoreSystem::SetCategoryTemporaryMultiplier - multiplier should not be 0!");
            return;
        }

        if(duration <= 0)
        {
            Debug.LogError("ScoreSystem::SetCategoryTemporaryMultiplier - duration should be greater than 0!");
            return;
        }

        StartCoroutine(TemporaryCategoryScoreMultiplier(multiplier, duration, foodCategory));
    }

    private IEnumerator TemporaryCategoryScoreMultiplier(float multiplier, float duration, sFood.FoodCategory foodCategory)
    {
        _foodCategoryMultiplierBonus[foodCategory] += multiplier;
        UpdateMultipliersDisplay();

        yield return new WaitForSeconds(duration);
        _foodCategoryMultiplierBonus[foodCategory] -= multiplier;
        UpdateMultipliersDisplay();

        yield return null;
    }

    private void UpdateTotalScoreDisplay()
    {
        if(_totalScoreDisplay != null)
        {
            Transform canvasTransform = _totalScoreDisplay.transform.Find("Canvas");
            Transform totalScoreTransform = canvasTransform.Find("Total Score");
            /*
            Debug.Log("ScoreSystem::UpdateTotalScoreDisplay - totalScoreTransform = " + totalScoreTransform);
            Debug.Log("ScoreSystem::UpdateTotalScoreDisplay - totalScoreTransform.gameObject = " + totalScoreTransform.gameObject);
            Debug.Log("ScoreSystem::UpdateTotalScoreDisplay - totalScoreTransform.gameObject.GetComponent<TextMeshProUGUI>() = " + totalScoreTransform.gameObject.GetComponent<TextMeshProUGUI>());
            */

            TextMeshProUGUI totalScoreTextMp = totalScoreTransform.gameObject.GetComponent<TextMeshProUGUI>();
            totalScoreTextMp.SetText("" + _currentScore);
        }
        else 
        {
            Debug.LogError("ScoreSystem::UpdateTotalScoreDisplay - Score display is not assigned to the Score System");
        }
    }
}
