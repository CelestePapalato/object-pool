using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField]
    float modificarVelocidad = 1f;

    void Update()
    {
        if(transform.position.x <= -10)
        {
            Debug.Log(Time.realtimeSinceStartup);
            gameObject.SetActive(false);
        }
        transform.Translate(GameManager.instance.Velocidad * modificarVelocidad * Time.deltaTime * Vector2.left);
    }
}
