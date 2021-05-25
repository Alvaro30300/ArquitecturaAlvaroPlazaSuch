using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameManager : MonoBehaviour
{
    public Text name;
    public RankingManager rm;
    // Start is called before the first frame update
    void Start()
    {
        rm = GetComponent<RankingManager>();
        name.text = "AAA";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void InsertName()
    {
        rm.InsertarPuntos(name.text, UIController.instance.score);
    }
}
