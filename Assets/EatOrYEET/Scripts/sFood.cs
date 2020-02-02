using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName="Eat or YEET/Scriptable Objects/Food", fileName="New Food Type", order=51)]
public class sFood : ScriptableObject
{

    //[SerializeField] private List<GameObject> foodPrefabs = new List<GameObject>();
    [SerializeField]
    [FormerlySerializedAs("foodCategories")]
    private List<FoodCategory> _foodCategories = new List<FoodCategory>();

    [SerializeField]
    [FormerlySerializedAs("foodName")]
    private string _foodName = "New Food";

    [SerializeField]
    [FormerlySerializedAs("pointValue")]
    private int _pointValue = 0;

    [SerializeField]
    [FormerlySerializedAs("staminaCost")]
    private int _staminaCost = 0;

    [SerializeField]
    private Sprite _foodSprite = null;

    [SerializeField]
    private Color _foodColour; //This is used to tell the munch Particle system what colour to make the particles
    

    public List<FoodCategory> foodCategories
    {
        get
        {
            return _foodCategories;
        }
    }

    public string foodName
    {
        get
        {
            return _foodName;
        }
    }

    public int pointValue
    {
        get
        {
            return _pointValue;
        }
    }

    public int staminaCost
    {
        get
        {
            return _staminaCost;
        }
    }

    public Sprite foodSprite
    {
        get
        {
            return _foodSprite;
        }
    }

    public Color foodColour
    {
        get
        {
            return _foodColour;
        }
    }

    public enum FoodCategory
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
