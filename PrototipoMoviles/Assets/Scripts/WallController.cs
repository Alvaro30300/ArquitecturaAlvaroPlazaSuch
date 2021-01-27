using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{
    //Se establecen variables de velocidad de movimiento
    public float speed = 5;
    public bool isMoving;
    public float acceleration = 0.001f;

    private void Start()
    {
        //Se pone el booleano en verdadero para confirmar que las paredes se deben mover
        isMoving = true;
    }
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        //Aqui se aumenta la velocidad de bajada
        float actualSpeed = speed + UIController.instance.distance  * acceleration;

        //Mientras el booleano este en verdadero, el movimiento continua
        if (isMoving)
        {
            transform.position += Vector3.down * actualSpeed * Time.deltaTime;
        }
    }
  
    public void Stop()
    {
        //Se cancela el movimiento
        isMoving = false;
    }
}
