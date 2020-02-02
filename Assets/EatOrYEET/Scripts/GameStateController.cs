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
    private float _gameDuration;

    [SerializeField]
    private GameObject _totalScoreDisplay;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        
        if(_totalScoreDisplay != null)
        {
            DontDestroyOnLoad(_totalScoreDisplay);
        }
        else 
        {
            Debug.LogError("ScoreSystem::Start - Score display is not assigned to the Score System");
        }

        StartCoroutine(EndGameAfterTime(_gameDuration));
    }

    private IEnumerator EndGameAfterTime(float duration)
    {
        yield return new WaitForSeconds(duration);

        SceneManager.LoadScene(GAME_END_SCENE_NAME);

        if(_totalScoreDisplay != null)
        {
            _totalScoreDisplay.transform.position = new Vector3(0,1f,-8.21f);
            /*
            GameObject instance = Instantiate(_totalScoreDisplayPrefab);
            Transform canvasTransform = instance.transform.Find("Canvas");
            Transform totalScoreTransform = canvasTransform.Find("Total Score");
            TextMeshProUGUI totalScoreTextMp = totalScoreTransform.gameObject.GetComponent<TextMeshProUGUI>();
            totalScoreTextMp.SetText("" + _scoreSystem.GetScore());
            */
        }
        else 
        {
            Debug.LogError("ScoreSystem::UpdateTotalScoreDisplay - Score display is not assigned to the Score System");
        }

        yield return null;
    }
}
