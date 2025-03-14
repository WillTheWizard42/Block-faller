using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScore : MonoBehaviour
{
    public Text text;

    void OnEnable()
    {
        int score = PlayerPrefs.GetInt("score");
        text.text = "Score : " + score;
    }
}

