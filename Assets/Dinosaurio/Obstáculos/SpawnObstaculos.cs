using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstaculos : MonoBehaviour
{
    //Constantes
    [SerializeField]
    float tiempoInstanciamiento = 4f;
    [SerializeField]
    float tiempoRepeticion = 3.5f;
    [SerializeField]
    string obstaculoTag = "Obstaculo";
    [SerializeField]
    bool randomizarPosicionY = false;
    [SerializeField]
    float posicionYMIN = 2f;
    [SerializeField]
    float posicionYMAX = 4f;
    [SerializeField]
    bool randomizarTiempoRepeticion = false;
    [SerializeField]
    float tiempoRepeticionMIN = 2f;
    [SerializeField]
    float tiempoRepeticionMAX = 4f;

    // Variables
    int cantInstanciamientos;
    int longitud;

    private void Awake()
    {
        if (this.CompareTag("Spawner"))
        {
            this.enabled = false;
        }
    }

    void Start()
    {
        Invoke(nameof(Instanciar), tiempoInstanciamiento);
    }

    void Instanciar()
    {
        GameObject obstaculo = ObjectPool.instance.GetRandomObject(obstaculoTag);

        if(obstaculo == null) { return; }

        Vector3 posicion = obstaculo.transform.position;

        if (randomizarPosicionY)
        {
            posicion.y = Random.Range(posicionYMIN, posicionYMAX);
        }

        obstaculo.SetActive(true);
        obstaculo.transform.position = posicion;

        cantInstanciamientos++;

        if (randomizarTiempoRepeticion)
        {
            actualizarTiempoRepeticion();
        }
        Invoke(nameof(Instanciar), tiempoRepeticion);
    }

    void actualizarTiempoRepeticion()
    {
        tiempoRepeticion = Random.Range(tiempoRepeticionMIN, tiempoRepeticionMAX);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}
