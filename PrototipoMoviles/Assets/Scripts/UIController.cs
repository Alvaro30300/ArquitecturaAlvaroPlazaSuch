using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    //Texto de puntuacion
    public Text points;
    public Text points2;

    //Variables de puntuacion
    public float distance;
    public float multiplier;
    public int score;
    //Variable del singleton
    public static UIController instance;

    //Patron SINGLETON
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
        //La distancia empieza a 0
        distance = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Cómo sube la puntuacion
        distance += multiplier * Time.deltaTime;
        score = Mathf.CeilToInt(distance);
        points.text = score.ToString();
        points2.text = score.ToString();
    }


    public void Stop()
    {
        //Cuando la partida se acaba, deja de sumar puntos
        this.enabled = false;
    }

}
