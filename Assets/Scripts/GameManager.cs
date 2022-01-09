using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

	public GameObject game;
	public GameObject correctColor;
	public GameObject wrongColor;
	public GameObject transition;

	public XMLManager xmlManager;
	public RandomManager randomManager;

	public Button answer1Button;
	public Button answer2Button;
	public Button answer3Button;
	public Button answer4Button;
	public Text questionText;
	public Text answer1Text;
	public Text answer2Text;
	public Text answer3Text;
	public Text answer4Text;
	public Text timerText;

	private float timer;
	private bool levelCompleted;
	private string question;
	private string answer1;
	private string answer2;
	private string answer3;
	private string answer4;
	private string correctAnswer;
	private int actualLevel;
	private int numberOfCorrectAnswers;
	private GameObject application;

    private void Start()
    {
		levelCompleted = false;
		
		application = GameObject.Find("Application");
		application.GetComponent<ApplicationManager>().gameScore = 0;
		application.GetComponent<ApplicationManager>().ToggleSettings();

	}

    private void Init()
    {
		ResetQuiz();
		StartCoroutine(CreateLevel(0.0f));
    }

    private void Update()
    {
        if (!levelCompleted)
        {
			timer -= Time.deltaTime;
			
			if (timer < 0.0f)
			{
				timer = 0.0f;

				levelCompleted = true;
				
				MoveToNextLevel();
			}
		}
		
		ShowTimer();
    }

    private IEnumerator CreateLevel(float delay)
	{
		yield return new WaitForSeconds(delay);

		ResetQuiz();
		ToggleButtons(true);

		xmlManager.game[randomManager.inGameLevels[actualLevel]].TryGetValue("question", out question);
		xmlManager.game[randomManager.inGameLevels[actualLevel]].TryGetValue("answer1", out answer1);
		xmlManager.game[randomManager.inGameLevels[actualLevel]].TryGetValue("answer2", out answer2);
		xmlManager.game[randomManager.inGameLevels[actualLevel]].TryGetValue("answer3", out answer3);
		xmlManager.game[randomManager.inGameLevels[actualLevel]].TryGetValue("answer4", out answer4);
		xmlManager.game[randomManager.inGameLevels[actualLevel]].TryGetValue("correctAnswer", out correctAnswer);

		questionText.text = question;
		answer1Text.text = answer1;
		answer2Text.text = answer2;
		answer3Text.text = answer3;
		answer4Text.text = answer4;
	}

    private void ShowTimer()
    {
		int minutes = Mathf.FloorToInt(timer / 60F);
		int seconds = Mathf.FloorToInt(timer - minutes * 60);
		string formattedTime = string.Format("{0:0}:{1:00}", minutes, seconds);

		timerText.text = formattedTime;
	}

	public void SubmitQuizAnswer(Text selectedAnswer)
	{
        if (!levelCompleted)
        {
			ToggleButtons(false);

			if (selectedAnswer.text == correctAnswer)
			{
				correctColor.SetActive(application.GetComponent<ApplicationManager>().displayAnswerBox);

				levelCompleted = true;

				numberOfCorrectAnswers++;

				IncrementScore();
			}
			else
			{
				wrongColor.SetActive(application.GetComponent<ApplicationManager>().displayAnswerBox);

				levelCompleted = true;
			}

			MoveToNextLevel();
		}		
	}

    private void ToggleButtons(bool toggle)
    {
		answer1Button.interactable = toggle;
		answer2Button.interactable = toggle;
		answer3Button.interactable = toggle;
		answer4Button.interactable = toggle;
	}

	private void IncrementScore()
    {
		switch (application.GetComponent<ApplicationManager>().GetGameMode())
		{
			case "easy":
				application.GetComponent<ApplicationManager>().gameScore += Mathf.FloorToInt(timer) * 20;
				break;

			case "medium":
				application.GetComponent<ApplicationManager>().gameScore += Mathf.FloorToInt(timer) * 40;
				break;

			case "hard":
				application.GetComponent<ApplicationManager>().gameScore += Mathf.FloorToInt(timer) * 60;
				break;
		}
	}

    private void MoveToNextLevel()
    {
		actualLevel++;
		
		if (actualLevel < application.GetComponent<ApplicationManager>().totalLevels)
        {
			StartCoroutine(CreateLevel(1.0f));
		}
		else
		{
			CheckTotalCorrectAnswers();
		}
	}

    private void CheckTotalCorrectAnswers()
    {
		if (numberOfCorrectAnswers == application.GetComponent<ApplicationManager>().totalLevels)
        {
			StartCoroutine(EndGame(1.0f, "Win"));
		}
        else
        {
			StartCoroutine(EndGame(1.0f, "Lose"));
		}
    }

	private IEnumerator EndGame(float delay, string status)
    {
		yield return new WaitForSeconds(delay);

		transition.SetActive(true);
		transition.GetComponent<ScreenManager>().sceneName = status;
		
		if (status == "Win")
		{
			application.GetComponent<ServerManager>().WriteMessageToSend("won_" + application.GetComponent<ApplicationManager>().gameScore);
		}
	}

    private void ResetQuiz()
    {
		levelCompleted = false;

        switch (application.GetComponent<ApplicationManager>().GetGameMode())
        {
			case "easy":
				timer = application.GetComponent<ApplicationManager>().levelTime;
				break;

			case "medium":
				timer = application.GetComponent<ApplicationManager>().levelTime * 0.5f;
				break;

			case "hard":
				timer = application.GetComponent<ApplicationManager>().levelTime * 0.25f;
				break;
		}

		questionText.text = "";
		answer1Text.text = "";
		answer2Text.text = "";
		answer3Text.text = "";
		answer4Text.text = "";

		correctColor.SetActive(false);
		wrongColor.SetActive(false);
	}

}
