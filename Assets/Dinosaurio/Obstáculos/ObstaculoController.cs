using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaculoController : MonoBehaviour
{
    [SerializeField]
    float modificarVelocidad = 1f;

    void Update()
    {
        if(transform.position.x <= -10)
        {
            gameObject.SetActive(false);
        }
        transform.Translate(Vector2.left * GameManager.instance.getVelocidad() * modificarVelocidad * Time.deltaTime);
    }
}
