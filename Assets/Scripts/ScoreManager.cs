using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{

    public Text messageScore;
    public Text messageResult;

    private GameObject application;

    private void Start()
    {
        application = GameObject.Find("Application");

        DisplayResult();    
    }

    private void DisplayResult()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "Win")
        {
            messageResult.text = "you answered all " + application.GetComponent<ApplicationManager>().totalLevels + " questions correctly";
        }
        else
        {
            messageResult.text = "TOUGH LUCK!";
        }

        messageScore.text = "YOUR SCORE IS " + application.GetComponent<ApplicationManager>().gameScore + "!";
    }

}
