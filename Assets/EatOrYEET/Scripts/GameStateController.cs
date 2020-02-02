using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameStateController : MonoBehaviour
{
    private const string GAME_END_SCENE_NAME = "EATorYEET/Scenes/EndGameScene";

    [SerializeField]
    private ScoreSystem _scoreSystem;
    
    [SerializeField]
    private GameObject _totalScoreDisplay;

    [SerializeField]
    private GameObject _resetButtonPrefab;

    [SerializeField]
    private Vector3 _resetButtonPosn;

    [SerializeField]
    private bool _isTimedGame;

    [Header("Timed Game Settings")]
    [SerializeField]
    private float _gameDuration;

    [Header("Score Attack Settings")]
    [SerializeField]
    private int _scoreToWin;

    private float _timeAtStart = 0f;

    // Start is called before the first frame update
    void Start()
    {
        //DontDestroyOnLoad(gameObject);
        
        if(_totalScoreDisplay != null)
        {
            //DontDestroyOnLoad(_totalScoreDisplay);
        }
        else 
        {
            Debug.LogError("ScoreSystem::Start - Score display is not assigned to the GameStateController");
        }

        _timeAtStart = Time.time;

        if(_isTimedGame)
        {
            StartCoroutine(EndGameAfterTime(_gameDuration));
        }
        else
        {
            if(_scoreSystem != null)
            {
                _scoreSystem.SetGameStateController(this);
            }
            else 
            {
                Debug.LogError("ScoreSystem::Start - Score System is not assigned to the GameStateController");
            }
        }
    }

    public int GetScoreToWin()
    {
        return _scoreToWin;
    }

    public void EndGame()
    {
        DeactivateAllFood();

        if(_resetButtonPrefab != null)
        {
            Instantiate(_resetButtonPrefab, _resetButtonPosn, Quaternion.identity);
        }

        //SceneManager.LoadScene(GAME_END_SCENE_NAME);

        /*
        if(_totalScoreDisplay != null)
        {
            _totalScoreDisplay.transform.position = new Vector3(0,1f,-8.21f);

            if(!_isTimedGame)
            {
                Transform canvasTransform = _totalScoreDisplay.transform.Find("Canvas");
                Transform totalScoreTransform = canvasTransform.Find("Total Score");
                TextMeshProUGUI totalScoreTextMp = totalScoreTransform.gameObject.GetComponent<TextMeshProUGUI>();
                totalScoreTextMp.SetText("" + (Time.time - _timeAtStart));
            }
        }
        else 
        {
            Debug.LogError("ScoreSystem::EndGame - Score display is not assigned to the Score System");
        }
        */
    }

    private IEnumerator EndGameAfterTime(float duration)
    {
        yield return new WaitForSeconds(duration);
        EndGame();
        yield return null;
    }

    private void DeactivateAllFood()
    {
        FoodItem[] foodThings = null;

        foodThings = FindObjectsOfType<FoodItem>();

        foreach(FoodItem food in foodThings){
            food.gameObject.SetActive(false);
        }

        cFoodSpawner[] foodThings = null;

        foodSpawners = FindObjectsOfType<cFoodSpawner>();

        foreach(cFoodSpawner foodSpawner in foodSpawners){
            foodSpawner.gameObject.SetActive(false);
        }
    }
}
