using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Establezco variables para que el player funcione como debe


    //Funcionamiento del iman
    public GameObject Magnet;
    public GameObject MagnetAnim;
    public Animator anim;
    //Collider para el player
    public Collider2D colliderPlayer;
    //Velocidades del player
    public float changeSpeed;
    public float fallingSpeed;
    public float actualSpeed;
    public float timer;
    float acceleration = 0.001f;

    //Animaciones para el cambio de pared
    [Header("Animacion")]
    public AnimationCurve animationSlide;
    public float animationDurationSlide;
    public AnimationCurve animationFall;
    public float animationDurationFall;

    //Debug
    [Header("Debug")]
    public bool isFalling;
    public bool isDead;
    public bool isSwitching;

    //VARIABLE ESTATICA PARA EL SINGLETON, DE MANERA QUE TODAS LAS CLASES PUEDAN ACCEDER A ESTA
    public static PlayerController instance;

    //Booleano para saber en qué pared esta el jugador
    [SerializeField]
    private bool isRight;

    //PATRON SINGLETON
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
        //Asigno un valor inicial a las variables y dar el componente de animator a la variable "anim"
        isRight = true;
        anim = GetComponent<Animator>();
        StartCoroutine(Deslizar(false));
        isFalling = false;
        isSwitching = true;
        isDead = false;
        Magnet.SetActive(false);
        MagnetAnim.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        //Aumenta la velocidad segun pasa el tiempo en la partida, para aumentar la dificultad
       actualSpeed = changeSpeed + UIController.instance.distance * acceleration;

#if UNITY_ANDROID
        //Input para que funcione cuando se compila para Android
        if (isDead != true)
        {
            if (Input.touchCount > 0 && isSwitching != true)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    if (isRight)
                    {
                        isSwitching = true;
                        anim.SetTrigger("Left");
                        StartCoroutine(Deslizar(true));
                        isRight = false;
                    }
                    else
                    {
                        isSwitching = true;
                        anim.SetTrigger("Right");
                        StartCoroutine(Deslizar(false));
                        isRight = true;
                    }
                }
            }
        }
#endif


#if UNITY_EDITOR
        //Input para que funcione en Pc
        if (isDead != true)
        {
            if (Input.anyKey && (isSwitching != true))
            {
                if (isRight)
                {
                    isSwitching = true;
                    isRight = false;
                    anim.SetTrigger("Left");
                    StartCoroutine(Deslizar(true));
                }
                else
                {
                    isSwitching = true;
                    anim.SetTrigger("Right");
                    isRight = true;
                    StartCoroutine(Deslizar(false));
                }
            }
          
        }
#endif
    }
    public void Drag(bool izquierda)
    {
        //if (isDead != true)
        //{
        //    if ((izquierda) && (isSwitching != true))
        //    {
        //        isSwitching = true;
        //        anim.SetTrigger("Left");
        //        StartCoroutine(Deslizar(true));
        //    }
        //    else if ((!izquierda) && (isSwitching != true))
        //    {
        //        isSwitching = true;
        //        anim.SetTrigger("Right");
        //        StartCoroutine(Deslizar(false));
        //    }
        //}
    }


    //Detector de colision y lo que pasa
    public void OnCollisionEnter2D(Collision2D col)
    {
        isSwitching = false;
        anim.SetTrigger("Up");
        StopAllCoroutines();
        if (col.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            StopAllCoroutines();
            StartCoroutine(Caida());
            colliderPlayer.enabled = false;
            isDead = true;
            isFalling = true;
            UIController.instance.Stop();
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Level"))
            {
                obj.SendMessage("Stop", SendMessageOptions.DontRequireReceiver);
            }
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Coins"))
        {
            //Debug.Log("Monedita");
            Destroy(collision.gameObject);
        }
    }

    //Movimiento entre paredes
    IEnumerator Deslizar(bool izquierda)
    {
        float t = 0;
        while (t / animationDurationFall <= 1)
        {

            t += Time.deltaTime;
            if (izquierda)
            {
                transform.position += Vector3.left * actualSpeed * Time.deltaTime * animationSlide.Evaluate(t / animationDurationSlide);
            }

            else
            {
                transform.position -= Vector3.left * actualSpeed * Time.deltaTime * animationSlide.Evaluate(t / animationDurationSlide);
            }
            yield return null;
        }
    }
    //Animacion de caida
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
    //Cuando el cubo sale de la pantalla(se ha caido) cambio de escena(a la de Game Over)
    public void OnBecameInvisible()
    {
        GameManager.instance.EndGame();
    }
}
