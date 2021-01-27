using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonedasController : MonoBehaviour
{
    Animator anim;
    public float magnetForce;
    public float duration;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((collision.gameObject.layer == LayerMask.NameToLayer("Player") || collision.gameObject.layer == LayerMask.NameToLayer("Invulnerable"))))
        {
            //Debug.Log("Dinero");
            Destroy(this.gameObject);
            UIController.instance.distance = UIController.instance.distance + 10;
        }
        if ((collision.gameObject.layer == LayerMask.NameToLayer("Magnet")))
        {
            //Debug.Log("Iman");
            StartCoroutine(AttractToPlayer());
            
        }
    }
     IEnumerator AttractToPlayer()
    {
        transform.parent = null;
        float t = 0;
        while (t/duration<1)
        {
            t += Time.deltaTime;
            transform.position = Vector2.Lerp(this.transform.position, PlayerController.instance.transform.position, t / duration);
            yield return null;
        }
        
    }
}
