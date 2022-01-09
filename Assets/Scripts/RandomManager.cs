using System.Collections.Generic;
using UnityEngine;

public class RandomManager : MonoBehaviour
{

    public GameObject gameManager;
    public XMLManager xmlManager;
    [HideInInspector]
    public List<int> inGameLevels;

    private List<int> allLevels;
    private GameObject application;

    private void Init()
    {
        application = GameObject.Find("Application");

        GenerateRandomLevels();
    }

    private void GenerateRandomLevels()
    {
        allLevels = new List<int>();
        inGameLevels = new List<int>();

        for (int i = 0; i < application.GetComponent<ApplicationManager>().totalQuestions; i++)
        {
            allLevels.Add(i);
        }

        for (int i = 0; i < application.GetComponent<ApplicationManager>().totalLevels; i++)
        {
            int randomNumber = allLevels[Random.Range(0, allLevels.Count)];
            inGameLevels.Add(randomNumber);

            allLevels.Remove(randomNumber);
        }

        gameManager.transform.SendMessage("Init");
    }
}
