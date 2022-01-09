using UnityEngine;
using UnityEngine.UI;

public class SpriteManager : MonoBehaviour
{
		
	public Sprite[] frames;

    //private readonly int framesPerSecond = 24;
	private int index = 0;
	
	private void Update()
	{
        if (gameObject.activeSelf)
        {
			//index = (int)(Time.timeSinceLevelLoad * framesPerSecond);
			index += 1;
			
			if (gameObject.name == "Logo" || gameObject.name == "Ripple")
			{
				if (index >= 0 && index < frames.Length)
				{
					index = index % frames.Length;

					if (GetComponent<Image>() != null)
					{
						GetComponent<Image>().sprite = frames[index];
					}
					else
					{
						GetComponent<SpriteRenderer>().sprite = frames[index];
					}
				}
			}
			else
			{
				if (index >= 0)
				{
					index = index % frames.Length;

					if (GetComponent<Image>() != null)
					{
						GetComponent<Image>().sprite = frames[index];
					}
					else
					{
						GetComponent<SpriteRenderer>().sprite = frames[index];
					}
				}
			}
		}
		
	}

    public void HideAnimation(GameObject instance)
    {
		instance.GetComponent<SpriteManager>().enabled = false;
		instance.SetActive(false);
    }

    public void ShowAnimation(GameObject instance)
    {
        if (instance.name == "Ripple")
		    index = 0;

		instance.GetComponent<SpriteManager>().enabled = true;
		instance.SetActive(true);
    }

}
