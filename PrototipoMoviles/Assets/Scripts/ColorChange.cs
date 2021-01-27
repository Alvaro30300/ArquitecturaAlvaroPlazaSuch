using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    //Se establecen variables de tiempo, distintos colores y un array para manejar el cambio de estos durante la partida
    public Color[] colores;
    public int changingColor;

    public SpriteRenderer sr;

    public Color red;
    public Color blue;
    public Color green;
    public Color yellow;
    public Color white;
    public Color black;

    public float t = 0;
    public float maxtime;
    public float interpTime = 2f;
    public static ColorChange instance;
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
        //Se establecen los colores de las variables desde el codigo
        sr = GetComponent<SpriteRenderer>();
        red = new Color(0.4f, 0, 0, 1f);
        blue = new Color(0.15f, 0.15f, 0.65f, 1f);
        green = new Color(0, 0.5f, 0, 1f);
        yellow = new Color(0.73f, 0.73f, 0, 1f);
        white = new Color(1f, 1f, 1f, 1f);
        black = new Color(0.3f, 0.3f, 0.3f, 1f);
        colores = new Color[6] { white, blue, green, yellow, red, black };

        sr.color = white;

    }

    // Update is called once per frame
    void Update()
    {
        //Funcionamiento del cambio de colores durante la partida
        t += Time.deltaTime;
        if(t>= maxtime)
        {
            Color previousColor = sr.color;
            do
            {
                changingColor = Random.Range(0, colores.Length);
            }
            while (colores[changingColor ]== sr.color);

            StartCoroutine(Lerp(sr.color, colores[changingColor]));
            t = 0;
        }
    }
    //Corrutina para el suavizado entre los cambios de color
    public IEnumerator Lerp(Color ini, Color End)
    {
        float tiempo = 0;
        while (tiempo <interpTime)
        {
            tiempo += Time.deltaTime;
            sr.color = Color.Lerp(ini, End, tiempo / interpTime);
            yield return null;
        }
    }
}
