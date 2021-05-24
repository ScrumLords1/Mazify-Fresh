using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreElement : MonoBehaviour
{

    public TMP_Text usernameText;
    public TMP_Text timeText;
    public TMP_Text deathsText;
    public TMP_Text highScoreText;

    public void NewScoreElement (string _username, int _time, int _deaths, int _highScore)
    {
        usernameText.text = _username;
        timeText.text = _time.ToString();
        deathsText.text = _deaths.ToString();
        highScoreText.text = _highScore.ToString();
    }

}
