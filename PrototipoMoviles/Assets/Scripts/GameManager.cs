using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //variable estatica para el singleton
    public static GameManager instance;

    //PATRON SINGLETON
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //GESTION DE ESCENAS
    public void StartGame()
    {
        SceneManager.LoadScene("Juego");
    }
    public void LoadShop()
    {
        SceneManager.LoadScene("Tienda");
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void EndGame()
    {
        SceneManager.LoadScene("EndGame 2");
    }
    public void Ranking()
    {
        SceneManager.LoadScene("ScoreRanking");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
