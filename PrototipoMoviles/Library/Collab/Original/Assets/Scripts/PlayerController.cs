using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    SpriteRenderer sr;
    public Collider2D colliderPlayer;
    public float changeSpeed;
    public float fallingSpeed;
    public float actualSpeed;
    float acceleration = 0.001f;

    public AnimationCurve animationSlide;
    public float animationDurationSlide;

    public AnimationCurve animationFall;
    public float animationDurationFall;

    public bool isFalling;
    public bool isDead;
    public bool isSwitching;
    public bool isMagnet;
    public bool isInvulnerable;
    public static PlayerController instance;

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
        sr = GetComponent<SpriteRenderer>();
        StartCoroutine(Deslizar(false));
        sr.flipX = false;
        isFalling = false;
        isSwitching = false;
        isDead = false;
        isMagnet = false;
        isInvulnerable = false;
    }
    // Update is called once per frame
    void Update()
    {
       actualSpeed = changeSpeed + UIController.instance.distance * acceleration;
        while (isMagnet == true)
        {

        }
        while (isInvulnerable == true)
        {

        }

#if UNITY_EDITOR
        if (isDead != true)
        {
            if ((Input.GetKeyDown(KeyCode.LeftArrow)) && (isSwitching != true))
            {
                sr.flipX = true;
                isSwitching = true;
                StartCoroutine(Deslizar(true));
            }
            if ((Input.GetKeyDown(KeyCode.RightArrow)) && (isSwitching != true))
            {
                sr.flipX = false;
                isSwitching = true;
                StartCoroutine(Deslizar(false));
            }
        }
#endif
    }
    public void Drag(bool izquierda)
    {
         if (isDead != true)
        {
        if ((izquierda) && (isSwitching != true))
        {
                Debug.Log("izq");
            sr.flipX = true;
            isSwitching = true;
            StartCoroutine(Deslizar(true));
        }
        else if ((!izquierda) && (isSwitching != true)){
                Debug.Log("der");
                sr.flipX = false;
            isSwitching = true;
            StartCoroutine(Deslizar(false));
        }}
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        //Debug.Log(col.gameObject.name);
        isSwitching = false;
        StopAllCoroutines();
        if (col.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            //Debug.Log("Abajo");
            StopAllCoroutines();
            StartCoroutine(Caida());
            colliderPlayer.enabled = false;
            isDead = true;
            isFalling = true;
            CameraController.instance.Stop();
            UIController.instance.Stop();
            //StopAllCoroutines();
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Level"))
            {
                obj.SendMessage("Stop", SendMessageOptions.DontRequireReceiver);
            }
        }
    }
    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("MagnetPower"))
        {

        }
    }
    IEnumerator Deslizar(bool izquierda)
    {
        float t = 0;
        while (t / animationDurationFall <= 1)
        {

            t += Time.deltaTime;
            if (izquierda)
                transform.position += Vector3.left * actualSpeed * Time.deltaTime * animationSlide.Evaluate(t / animationDurationSlide);
            else
                transform.position -= Vector3.left * actualSpeed * Time.deltaTime * animationSlide.Evaluate(t / animationDurationSlide);
            //Debug.Log(animacionDeslizar.Evaluate(t / animationDuration));
            yield return null;
        }
    }
    IEnumerator Caida()
    {
        float t = 0;
        while (t / animationDurationFall <= 1)
        {
            t += Time.deltaTime;
            transform.position += Vector3.down * fallingSpeed * Time.deltaTime * animationFall.Evaluate(t / animationDurationFall);
            yield return null;
        }
        t = 0;
        while (t < 2)
        {
            transform.position += Vector3.down * fallingSpeed * Time.deltaTime * animationFall.Evaluate(1);
            yield return null;
        }
    }
}
