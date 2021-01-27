using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModulManager : MonoBehaviour
{

    //OBJECT POOL
    //Creacion de variables y un array para controlar la activacion o desactivacion de los distitons modulos
    public GameObject[] modulArray;
    public Transform parent;
    ModulManager instance;
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
    void Start()
    {
        //Instantiate(modulArray[11], InstanceManager.instance.spawnPoint.position, Quaternion.identity, parent);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //Cuando un modulo entra en el trigger de debajo de la escena, se activa otro en la parte superior, para asi conseguir un bucle de activacion y desactivacion que permitan una partida infinita
    //
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Obstacle"))
        
        SpawnModul();
        collision.gameObject.GetComponentInParent<WallController>().gameObject.SetActive(false);

        //Debug.Log("entra");
        //Destroy(collision.gameObject.GetComponentInParent<WallController>().gameObject);
    }

    //Activacion de modulos
    void SpawnModul()
    {
        int moduleIndex = Random.Range(0, modulArray.Length);

        modulArray[moduleIndex].SetActive(true);
        modulArray[moduleIndex].transform.position = InstanceManager.instance.spawnPoint.position;

        //module.transform.position = InstanceManager.instance.spawnPoint.position;

        //GameObject module =  Instantiate(modulArray[moduleIndex],parent);
        //module.transform.position = InstanceManager.instance.spawnPoint.position;

    }

}

