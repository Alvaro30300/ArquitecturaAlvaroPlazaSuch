using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapColor : MonoBehaviour
{
    //En este codigo se da el componente Tilemap para que éste acceda al cambio de color de la imagen de fondo y así tengan el mismo color durante toda la partida
    Tilemap tilemap;

    // Start is called before the first frame update
    void Start()
    {
        tilemap = GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
        tilemap.color = ColorChange.instance.sr.color;

    }
}
