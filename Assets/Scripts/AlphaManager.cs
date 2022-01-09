using UnityEngine;
using UnityEngine.UI;

public class AlphaManager : MonoBehaviour
{

    //public Slider slider;
    public GameObject transition;
    public GameObject[] fadeObjects;
    
    private float alpha;
    private bool isAnimating;

    private void Start()
    {
        alpha = 1.0f;
        //slider.enabled = true;

        SetComponentAlpha();
    }

    private void Update()
    {
        if (isAnimating)
        {
            StartFading();
        }
    }

    private void SetComponentAlpha()
    {
        for (int i = 0; i < fadeObjects.Length; i++)
        {
            if (fadeObjects[i].GetComponent<Image>() != null)
            {
                fadeObjects[i].GetComponent<Image>().color = new Color(fadeObjects[i].GetComponent<Image>().color.r, fadeObjects[i].GetComponent<Image>().color.g, fadeObjects[i].GetComponent<Image>().color.b, alpha);
            }
            if (fadeObjects[i].GetComponent<Text>() != null)
            {
                fadeObjects[i].GetComponent<Text>().color = new Color(fadeObjects[i].GetComponent<Text>().color.r, fadeObjects[i].GetComponent<Text>().color.g, fadeObjects[i].GetComponent<Text>().color.b, alpha);
            }
        }
    }

    public void InitializeFading()
    {
        //slider.enabled = false;

        isAnimating = true;
    }

    private void StartFading()
    {
        if (isAnimating)
        {
            alpha -= Time.deltaTime;

            SetComponentAlpha();

            if (alpha <= 0.0f)
            {
                isAnimating = false;

                //slider.value = 0.0f;

                gameObject.SetActive(false);
                transition.SetActive(true);
            }
        }
    }

}
