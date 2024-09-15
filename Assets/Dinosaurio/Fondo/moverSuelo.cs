using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moverSuelo : MonoBehaviour
{
    private List<Transform> suelos = new List<Transform>();
    private float tamañoSuelo;

    void Start()
    {
        this.enabled = false;
        var aux1 = GetComponentsInChildren<Transform>();
        suelos.Add(aux1[1]);
        suelos.Add(aux1[2]);
        SpriteRenderer aux2 = GetComponentInChildren<SpriteRenderer>();
        tamañoSuelo = aux2.bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {

        // Revisamos si un suelo está fuera de pantalla

        Transform suelo1 = suelos[0];
        Transform suelo2 = suelos[1];
        if (suelo1.position.x <= -10)
        {
            float nuevoX = suelo2.position.x + tamañoSuelo;
            suelo1.position = new Vector3(nuevoX, suelo1.position.y, suelo1.position.z);
            suelos[0] = suelo2;
            suelos[1] = suelo1;
        }

        // Movemos los suelos
        
        foreach(Transform suelo in suelos)
        {
            suelo.Translate(Vector2.left * GameManager.instance.getVelocidad() * Time.deltaTime);
        }

    }
}
