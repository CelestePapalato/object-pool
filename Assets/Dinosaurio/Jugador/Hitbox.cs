using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstaculo"))
        {
            PlayerController player = transform.parent.GetComponent<PlayerController>();
            player.ContactoConObstaculo();
        }
    }
}
