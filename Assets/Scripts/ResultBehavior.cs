using UnityEngine;
using System.Collections;
using Core;

public class ResultBehavior : MonoBehaviour
{
    Core.Color? winnerColor = null;

    public GameObject RedImage;
    public GameObject BlackImage;

    void Start()
    {

    }

    void Update()
    {

    }

    public void SetWinnerColor(Core.Color color)
    {
        winnerColor = color;
        RedImage.SetActive(color == Core.Color.Red);
        BlackImage.SetActive(color == Core.Color.Black);
    }
}
