using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEventsCreator : MonoBehaviour
{
    [SerializeField]
    private ScoreSystem _scoreSystem;

    [SerializeField]
    private bool _isRandomEvents = false;

    [Header("Random Multiplier Events Settings")]
    [SerializeField]
    private float _eventFrequency;

    [Header("Combo Events Settings")]
    [SerializeField]
    private float _numForCombo = 5;

    [SerializeField]
    private int _maxMultiplier = 5;

    private Dictionary<sFood.FoodCategory, int> _foodCategoryComboCounts;

    // Start is called before the first frame update
    void Start()
    {
        if(_isRandomEvents)
        {
            StartCoroutine(CreateRandomEvent());
        }

        _foodCategoryComboCounts = new Dictionary<sFood.FoodCategory, int>();
        sFood.FoodCategory[] foodCategories = (sFood.FoodCategory[])System.Enum.GetValues(typeof(sFood.FoodCategory));

        foreach(sFood.FoodCategory category in foodCategories)
        {
            _foodCategoryComboCounts[category] = 0;
        }
    }

    public void IndicateFoodEaten(FoodItem food)
    {
        if(_isRandomEvents)
        {
            return;
        }

        List<sFood.FoodCategory> typesOfEatenFood = food.foodScriptableObject.foodCategories;

        sFood.FoodCategory[] foodCategories = (sFood.FoodCategory[])System.Enum.GetValues(typeof(sFood.FoodCategory));
        foreach(sFood.FoodCategory category in foodCategories)
        {
            if(typesOfEatenFood.Contains(category))
            {
                _foodCategoryComboCounts[category] += 1;

                if(_foodCategoryComboCounts[category] >= _numForCombo)
                {
                    _scoreSystem.SetCategoryPermanentMultiplier((int)(_foodCategoryComboCounts[category]/_numForCombo), category);
                }
            }
            else
            {
                _foodCategoryComboCounts[category] = 0;
                _scoreSystem.SetCategoryPermanentMultiplier(1, category);
            }
        }
    }

    private IEnumerator CreateRandomEvent()
    {
        while(true)
        {
            yield return new WaitForSeconds(_eventFrequency);
            System.Random rng = new System.Random();
            int multiplierBonus = rng.Next(_maxMultiplier - 1) + 1;
            _scoreSystem.SetCategoryTemporaryMultiplier(multiplierBonus, _eventFrequency, GetRandomFoodCategory());
        }
        
        yield return null;
    }

    private sFood.FoodCategory GetRandomFoodCategory()
    {
        System.Random rng = new System.Random();

        int num = rng.Next(10);

        switch(num)
        {
            case 0:
                return sFood.FoodCategory.MEAT;
                break;
            case 1:
                return sFood.FoodCategory.VEG;
                break;
            case 2:
                return sFood.FoodCategory.FRUIT;
                break;
            case 3:
                return sFood.FoodCategory.DAIRY;
                break;
            case 4:
                return sFood.FoodCategory.CANDY;
                break;
            case 5:
                return sFood.FoodCategory.DESSERT;
                break;
            case 6:
                return sFood.FoodCategory.JUNKFOOD;
                break;
            case 7:
                return sFood.FoodCategory.BREAD;
                break;
            case 8:
                return sFood.FoodCategory.SEAFOOD;
                break;
            case 9:
                return sFood.FoodCategory.INEDIBLE;
                break;
            default:
                return sFood.FoodCategory.INEDIBLE;
        }
    }
}
