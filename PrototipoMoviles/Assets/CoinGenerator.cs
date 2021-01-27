using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//Patrón PROTOTYPE
public class CoinGenerator: Object
{
    public enum Cubeside { BOTTOM, TOP, LEFT, RIGHT, FRONT, BACK };

    static GameObject coin;

    public static GameObject Clone(Vector3 pos)
    {

        Texture2D tex = new Texture2D(16, 16);
        
        GameObject coinClone =  new GameObject();
        coinClone.AddComponent<SpriteRenderer>();
        coinClone.GetComponent<SpriteRenderer>().sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100);
        coinClone.GetComponent<SpriteRenderer>().color = new Color(255, 255, 0);

        //coinClone.GetComponent<SpriteRenderer>().material.color = new Color(255, 255, 255);

        coinClone.AddComponent<Rigidbody2D>();
        coinClone.GetComponent<Rigidbody2D>().isKinematic = false;   
        coinClone.AddComponent<BoxCollider2D>();
        coinClone.GetComponent<BoxCollider2D>().isTrigger = true;
        coinClone.name = "Coin(Clone)";
        coinClone.layer = 11;
        coinClone.gameObject.SetActive(true);
        coinClone.transform.position = pos;
        coinClone.transform.localScale = new Vector3(2, 2, 2);


        return coinClone;
    }

}
