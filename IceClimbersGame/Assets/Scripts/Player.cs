//Script para controlar el player
//Creado por Alexis Alvarado. Modificado por Daniel Sepulveda
//Fecha: 02/05/2022

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Controls control;
    private Animator anim;
    private SpriteRenderer sprtR;
    private Rigidbody2D body;
    [SerializeField] private LayerMask floor;
    private bool isTouchingFloor = false;
    private bool canDoubleJump = true;
    private float delay = 0.25f;
    private float speed = 1.6f;
    private CircleCollider2D hitboxFloor;

    //a los componentes inicializar arriba y en la funcion start localizarlos, en el control busca dentro de la escena un objeto
    //de tipo control que es el script

    void Start()
    {
        //Inicializaci�n de las variables
        control = GameObject.FindObjectOfType<Controls>();
        sprtR = this.GetComponent<SpriteRenderer>();
        anim = this.GetComponent<Animator>();
        hitboxFloor = this.GetComponent<CircleCollider2D>();
        body = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        animations();
        movement();

        delay -= Time.deltaTime;
    }

    //Funcion para el movimiento
    private void movement()
    {
        if (control.direction.magnitude > 0.01f)
        {
            body.velocity = new Vector2(control.direction.x * speed, body.velocity.y);
        }
        else
        {
            if (isTouchingFloor)
            {
                if (control.direction.x == 0)
                {
                    body.velocity = new Vector2(Mathf.Lerp(body.velocity.x, 0, 0.15f), body.velocity.y);
                }
            }
        }

        if (hitboxFloor.IsTouchingLayers(floor))
        {
            isTouchingFloor = true;
            canDoubleJump = true;
            anim.SetBool("DJump", false);
        }
        else
        {
            isTouchingFloor = false;
        }

        if (control.direction.y > 0)
        {
            jump();
        }
    }

    private void jump()
    {
        if (isTouchingFloor)
        {
            body.velocity = new Vector2(body.velocity.x, speed * 2);
            delay = 0.25f;
        }
        else if(canDoubleJump && !isTouchingFloor && delay < 0)
        {
            body.velocity = new Vector2(body.velocity.x, speed * 2);
            canDoubleJump = false;
            anim.SetBool("DJump", true);
        }
    }

    //Controla las animaciones del player
    private void animations()
    {
        //Controla la orientaci�n del sprite
        if (control.direction.x < 0) sprtR.flipX = true;
        else if (control.direction.x > 0) sprtR.flipX = false;
        
        //Parametros de las animaciones
        anim.SetFloat("Xvelocity", Mathf.Abs(control.direction.x));
        anim.SetFloat("Yvelocity", body.velocity.y);

        //Este parametro va a depender de si est�s tocando el suelo o no
        anim.SetBool("Floor", isTouchingFloor);

        //Se debe volver true cuando recibes da�o (solo se necesita que sea true durante un frame)
        anim.SetBool("Hit", false);
    }
}
