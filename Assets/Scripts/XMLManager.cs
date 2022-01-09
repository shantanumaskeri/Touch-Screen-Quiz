using UnityEngine;
using System.Collections.Generic;
using System.Xml;
using System.IO;

public class XMLManager : MonoBehaviour
{

	public GameObject randomNumber;
	
    [HideInInspector]
	public List<Dictionary<string,string>> game;
	
	private Dictionary<string,string> quizDetails;
	private GameObject application;

	private void Start()
	{
		game = new List<Dictionary<string, string>>();
		application = GameObject.Find("Application");

		LoadXML();
	}

    private void LoadXML()
    {
		XmlDocument xmlDoc = new XmlDocument();
		xmlDoc.LoadXml(File.ReadAllText(Application.dataPath + "/../Data/GameQuiz.xml"));

		ParseXML(xmlDoc);
	}

	private void ParseXML(XmlDocument document)
	{
		XmlNodeList questionnaireList = document.GetElementsByTagName("questionnaire");
	
		foreach (XmlNode questionnaireInfo in questionnaireList)
		{
			XmlNodeList quiz = questionnaireInfo.ChildNodes;
			quizDetails = new Dictionary<string, string>();

			foreach (XmlNode quizItems in quiz)
			{
				quizDetails.Add(quizItems.Name, quizItems.InnerText);
			}

			game.Add(quizDetails);
		}

		application.GetComponent<ApplicationManager>().totalQuestions = game.Count;

		XmlNodeList levelList = document.GetElementsByTagName("levels");
		foreach (XmlNode levelInfo in levelList)
		{
			XmlNodeList totalLevels = levelInfo.ChildNodes;

			foreach (XmlNode levelItem in totalLevels)
			{
				application.GetComponent<ApplicationManager>().totalLevels = int.Parse(levelItem.InnerText);
			}
		}

		XmlNodeList timeList = document.GetElementsByTagName("time");
		foreach (XmlNode timeInfo in timeList)
		{
			XmlNodeList totalTime = timeInfo.ChildNodes;

			foreach (XmlNode timeItem in totalTime)
			{
				application.GetComponent<ApplicationManager>().levelTime = float.Parse(timeItem.InnerText);
			}
		}

		XmlNodeList answerBoxList = document.GetElementsByTagName("answerBox");
		foreach (XmlNode answerBoxInfo in answerBoxList)
		{
			XmlNodeList displayAnswer = answerBoxInfo.ChildNodes;

			foreach (XmlNode answerBoxItem in displayAnswer)
			{
				application.GetComponent<ApplicationManager>().displayAnswerBox = bool.Parse(answerBoxItem.InnerText);
			}
		}
		
		randomNumber.transform.SendMessage("Init");
	}

}