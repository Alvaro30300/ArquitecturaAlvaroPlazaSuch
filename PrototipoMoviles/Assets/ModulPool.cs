using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;

public class ModulPool : MonoBehaviour
{
    public List<GameObject> pooledModules;
    public GameObject modulToPool;
    public int amountToPool;

    public static ModulPool instance;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        pooledModules = new List<GameObject>();
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = (GameObject)Instantiate(modulToPool);
            obj.SetActive(true);
            pooledModules.Add(obj);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
