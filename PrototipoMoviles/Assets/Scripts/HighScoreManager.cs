using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreManager : MonoBehaviour
{
    public string highKey = "highRun";
    public Text highScore;

    public static HighScoreManager instance;
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        UIController.instance.distance = PlayerPrefs.GetInt(highKey, 1);
    }

    // Update is called once per frame
    void Update()
    {
        //highScore.text =
    }
}
