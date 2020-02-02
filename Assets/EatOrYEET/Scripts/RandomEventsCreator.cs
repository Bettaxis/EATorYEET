using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEventsCreator : MonoBehaviour
{
    [SerializeField]
    private ScoreSystem _scoreSystem;

    [SerializeField]
    private float _eventFrequency;

    [SerializeField]
    private int _maxMultiplier = 5;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CreateRandomEvent());
    }

    private IEnumerator CreateRandomEvent()
    {
        while(true)
        {
            yield return new WaitForSeconds(_eventFrequency);
            System.Random rng = new System.Random();
            int multiplierBonus = rng.Next(_maxMultiplier - 1) + 1;
            _scoreSystem.SetCategoryMultiplier(multiplierBonus, _eventFrequency, GetRandomFoodCategory());
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
