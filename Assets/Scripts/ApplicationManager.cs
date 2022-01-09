using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplicationManager : MonoBehaviour
{

    public GameObject btnEasy;
    public GameObject btnMedium;
    public GameObject btnHard;

    [HideInInspector]
    public int gameScore = 0;
    [HideInInspector]
    public int totalQuestions = 0;
    [HideInInspector]
    public int totalLevels = 0;
    [HideInInspector]
    public float levelTime = 0.0f;
    [HideInInspector]
    public bool displayAnswerBox = false;

    private string sceneName;
    private string gameMode;
    private bool toggle;

    private void Start()
    {
        toggle = false;
    }

    public void ToggleSettings()
    {
        sceneName = SceneManager.GetActiveScene().name;
        if (sceneName != "Game")
        {
            toggle = !toggle;

            btnEasy.SetActive(toggle);
            btnMedium.SetActive(toggle);
            btnHard.SetActive(toggle);
        }
        else
        {
            btnEasy.SetActive(false);
            btnMedium.SetActive(false);
            btnHard.SetActive(false);
        }
    }

    public void SetGameMode(string mode)
    {
        sceneName = SceneManager.GetActiveScene().name;
        if (sceneName != "Game")
        {
            gameMode = mode;
            PlayerPrefs.SetString("difficulty", gameMode);

            btnEasy.SetActive(false);
            btnMedium.SetActive(false);
            btnHard.SetActive(false);
        }
    }

    public string GetGameMode()
    {
        if (PlayerPrefs.GetString("difficulty") == "")
        {
            gameMode = "easy";
        }
        else
        {
            gameMode = PlayerPrefs.GetString("difficulty");
        }
        
        return gameMode;
    } 

}
