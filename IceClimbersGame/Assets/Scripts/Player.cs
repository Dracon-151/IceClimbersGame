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
    private bool isTouchingFloor;
    private float speed = 1.6f;

    //a los componentes inicializar arriba y en la funcion start localizarlos, en el control busca dentro de la escena un objeto
    //de tipo control que es el script

    void Start()
    {
        //Inicializaci�n de las variables
        control = GameObject.FindObjectOfType<Controls>();
        sprtR = this.GetComponent<SpriteRenderer>();
        anim = this.GetComponent<Animator>();
        body = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        animations();
        movement();
    }

    //Funcion para el movimiento
    private void movement()
    {
        body.velocity = new Vector2(control.direction.x, body.velocity.y) * speed;
        Vector2 position = new Vector2(transform.position.x, transform.position.y - 0.3f);
        Vector2 size = new Vector2(0.1f, 0.1f);

        if (Physics2D.OverlapBox(position, size, 0).IsTouchingLayers(floor))
        {
            isTouchingFloor = true;
        }
        else
        {
            isTouchingFloor = false;
        }

        jump();
    }

    private void jump()
    {
        if (isTouchingFloor && control.direction.y>0)
        {
            body.velocity = Vector2.up * speed;
        }
        else
        {

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

        //Se debe volver true cuando presionas el doble salto (solo se necesita que sea true durante un frame)
        anim.SetBool("DJump", false);

        //Se debe volver true cuando recibes da�o (solo se necesita que sea true durante un frame)
        anim.SetBool("Hit", false);
    }
}
