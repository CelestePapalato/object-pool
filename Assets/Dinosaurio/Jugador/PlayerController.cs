using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Constantes
    [SerializeField]
    float fuerzaDeSalto = 5f;
    [SerializeField]
    float fuerzaDeSaltoBuff = 3f;
    [SerializeField]
    float fuerzaHaciaAbajo = 7f;
    [SerializeField]
    float tiempoParaAumentarSalto = 1.1f;

    // Componentes
    CapsuleCollider2D col;
    Rigidbody2D rb;
    Animator animator;

    //Variables
    bool onFloor = false;
    bool pulsoSaltar = false;
    bool pulsoBajar = false;
    float tiempoBuffSalto = 0f;

    void Start()
    {
        col = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        this.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.empezoLaPartida() && !animator.GetBool("Start"))
        {
            animation_move();
        }
        ControlInput();
    }

    void ControlInput()
    {
        // Saltar
        if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKey(KeyCode.UpArrow))
        {
            Saltar();
        }
        // Tirarse
        if (Input.GetKey(KeyCode.DownArrow))
        {
            TirarseAbajo();
        }
        // Salto sostenido
        if (Input.GetKey(KeyCode.UpArrow))
        {
            tiempoBuffSalto += Time.deltaTime;
        }
        else
        {
            tiempoBuffSalto = 0f;
            pulsoSaltar = false;
        }
        if (tiempoBuffSalto >= tiempoParaAumentarSalto)
        {
            BuffSalto();
            pulsoSaltar = false;
        }
    }

    void Saltar()
    {
        if (onFloor)
        {
            pulsoSaltar = true;
            rb.AddForce(Vector2.up * fuerzaDeSalto, ForceMode2D.Impulse);
            onFloor = false;
            SoundManager.instance.reproducirSonido("salto");
        }
    }

    void BuffSalto()
    {
        if (pulsoSaltar && !onFloor)
        {
            rb.AddForce(Vector2.up * fuerzaDeSaltoBuff, ForceMode2D.Impulse);
        }
    }

    void TirarseAbajo()
    {
        // Si el jugador pulsa la flecha abajo, tiramos al dinosaurio al piso con un impulso
        if (!onFloor && !pulsoBajar)
        {
            pulsoBajar = true;
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * -1 * fuerzaHaciaAbajo, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        onFloor = true;
        pulsoBajar = false;
    }

    public void ContactoConObstaculo()
    {
        animation_lose();
        GameManager.instance.perder();
    }
    void animation_move()
    {
        animator.SetBool("Start", true);
    }

    void animation_lose()
    {
        animator.SetBool("Lost", true);
    }
}
