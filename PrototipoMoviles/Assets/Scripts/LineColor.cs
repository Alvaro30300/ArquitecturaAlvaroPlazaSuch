using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineColor : MonoBehaviour
{
    SpriteRenderer lineColor;

    // Start is called before the first frame update
    void Start()
    {
        lineColor = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        lineColor.color = ColorChange.instance.sr.color;
    }
}
