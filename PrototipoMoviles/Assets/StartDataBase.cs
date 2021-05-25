using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDataBase : MonoBehaviour
{
    public RankingManager rm;
    // Start is called before the first frame update
    void Start()
    {
        rm = GetComponent<RankingManager>();
        rm.MostrarRanking();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
