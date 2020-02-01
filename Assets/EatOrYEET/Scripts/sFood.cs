using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Eat or YEET/Scriptable Objects/Food", fileName="New Food Type", order=51)]
public class sFood : ScriptableObject
{

    //[SerializeField] private List<GameObject> foodPrefabs = new List<GameObject>();
    [SerializeField] private List<FoodCategory> foodCategories = new List<FoodCategory>();
    [SerializeField] private string foodName = "New Food";
    [SerializeField] private int pointValue = 0;
    [SerializeField] private int staminaCost = 0;
    
    enum FoodCategory
    {
        MEAT,
        VEG,
        FRUIT,
        DAIRY,
        CANDY,
        DESSERT,
        JUNKFOOD,
        BREAD,
        SEAFOOD,
        INEDIBLE,
    }

}
