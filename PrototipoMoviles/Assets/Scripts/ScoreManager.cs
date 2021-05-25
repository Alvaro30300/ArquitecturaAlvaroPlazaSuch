using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    //Textos publicos para que el jugador sepa su puntuacion
    public Text score;
    public Text highScore;

    public int scoreNumb;

    // Start is called before the first frame update
    void Start()
    {
        //Informar al jugador de su puntuacion final
        score.text = "You climbed: " + UIController.instance.score.ToString() + " m";
        scoreNumb = SavegameManager.instance.Load();
    }

    // Update is called once per frame
    void Update()
    {
        //Usando el playerPrefs, se guarda la puntuacion en el dispositivo
        //highScore.text = "HighScore: " + PlayerPrefs.GetInt("HighScore", 0).ToString() + " m";
        //if (UIController.instance.score > PlayerPrefs.GetInt("HighScore", 0))
        //{
        //    PlayerPrefs.SetInt("HighScore", UIController.instance.score);
        //}
        highScore.text = "HighScore: " + scoreNumb + " m";
        if(UIController.instance.score > scoreNumb)
        {
            SavegameManager.instance.Save(UIController.instance.score);
            scoreNumb = UIController.instance.score;
        }
    }
}
