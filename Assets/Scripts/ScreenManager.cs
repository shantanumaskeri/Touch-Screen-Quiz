using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour
{

    public float delay = 3.0f;
    public string transitionMode = "";
    public string sceneName = "";

    private void Start()
    {
        if (transitionMode != "Manual")
        {
            CheckTransitionMode(transitionMode);
        }
    }

    public void CheckTransitionMode(string mode)
    {
        transitionMode = mode;

        switch (transitionMode)
        {
            case "Automatic":
                StartCoroutine(MoveToNextScreen(delay));
                break;

            case "Manual":
                StartCoroutine(MoveToNextScreen(0.0f));
                break;
        }
    }

    private IEnumerator MoveToNextScreen(float delay)
    {
        yield return new WaitForSeconds(delay);

        DontDestroyOnLoad(GameObject.Find("Application"));

        StopCoroutine(MoveToNextScreen(delay));

        SceneManager.LoadScene(sceneName);
    }
    
}
