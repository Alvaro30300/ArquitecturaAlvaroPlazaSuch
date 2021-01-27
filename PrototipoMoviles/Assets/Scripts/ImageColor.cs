using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageColor : MonoBehaviour
{

    public Image image;
    public Color[] colores;
    public int changingColor;

    public Color red;
    public Color blue;
    public Color green;
    public Color yellow;
    public Color white;
    public Color black;

    public float t = 0;
    public float interpTime = 2f;
    public static ImageColor instance;
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
        image = GetComponent<Image>();
        red = new Color(0.4f, 0, 0, 1f);
        blue = new Color(0.15f, 0.15f, 0.65f, 1f);
        green = new Color(0, 0.5f, 0, 1f);
        yellow = new Color(0.73f, 0.73f, 0, 1f);
        white = new Color(1f, 1f, 1f, 1f);
        black = new Color(0.3f, 0.3f, 0.3f, 1f);
        colores = new Color[6] { white, blue, green, yellow, red, black };
        image.color = white;

    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        if (t >= 10)
        {
            Color previousColor = image.color;
            do
            {
                changingColor = Random.Range(0, colores.Length);
            }
            while (colores[changingColor] == image.color);

            StartCoroutine(Lerp(image.color, colores[changingColor]));
            t = 0;
        }
    }
    public IEnumerator Lerp(Color ini, Color End)
    {
        float tiempo = 0;
        while (tiempo < interpTime)
        {
            tiempo += Time.deltaTime;
            image.color = Color.Lerp(ini, End, tiempo / interpTime);
            yield return null;
        }
    }
}

