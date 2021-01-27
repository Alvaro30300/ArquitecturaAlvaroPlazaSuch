using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    //Triggers que activan las corrutinas cuando el player colisiona con los objetos correspondientes(iman e invulnerable)
    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("MagnetPower"))
        {
            StopCoroutine(ActivarIman());
            StartCoroutine(ActivarIman());
        }
        if (col.gameObject.layer == LayerMask.NameToLayer("InvulnerablePower"))
        {
            StopCoroutine(ActivarGhost());
            StartCoroutine(ActivarGhost());
        }
    }
    //Corrutina del funcionamiento del iman
    public IEnumerator ActivarIman()
    {
        PlayerController.instance.Magnet.SetActive(true);
        PlayerController.instance.MagnetAnim.SetActive(true);
        yield return new WaitForSeconds(PlayerController.instance.timer);
        PlayerController.instance.Magnet.SetActive(false);
        PlayerController.instance.MagnetAnim.SetActive(false);
    }
    //Corrutina del funcionamiento del invulnerable
    public IEnumerator ActivarGhost()
    {

        this.gameObject.layer = LayerMask.NameToLayer("Invulnerable");
        PlayerController.instance.anim.SetBool("isInvulnerable", true);
        yield return new WaitForSeconds(PlayerController.instance.timer);
        this.gameObject.layer = LayerMask.NameToLayer("Player");
        PlayerController.instance.anim.SetBool("isInvulnerable", false);
    }
}
