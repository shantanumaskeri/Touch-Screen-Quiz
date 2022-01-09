using UnityEngine;

public class MaskManager : MonoBehaviour
{

    private Vector3[] startingWorldPositions;

    private void Start()
    {
        startingWorldPositions = new Vector3[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            startingWorldPositions[i] = transform.GetChild(i).transform.position;
        }
    }

    private void Update()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).transform.position = startingWorldPositions[i];
        }
    }

}
