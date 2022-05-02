using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Script que controla el player

    private Controles control;
    private Animator anim;
    private SpriteRenderer sprtR;

    void Start()
    {
        //Inicialización de las variables
        control = GameObject.FindObjectOfType<Controles>();
        sprtR = this.GetComponent<SpriteRenderer>();
        anim = this.GetComponent<Animator>();
    }

    void Update()
    {
        animations();
    }

    //Controla las animaciones del player
    private void animations()
    {
        //Controla la orientación del sprite
        if (control.direccion.x < 0) sprtR.flipX = true;
        else if (control.direccion.x > 0) sprtR.flipX = false;

        
        //Parametros de las animaciones

        anim.SetFloat("Xvelocity", Mathf.Abs(control.direccion.x));
        //anim.SetFloat("Yvelocity", aquiVaLaVelocidadYdelRigidBody2D);

        //Este parametro va a depender de si estás tocando el suelo o no
        anim.SetBool("Suelo", true);

        //Se debe volver true cuando presionas el doble salto (solo se necesita que sea true durante un frame)
        anim.SetBool("DJump", false);

        //Se debe volver true cuando recibes daño (solo se necesita que sea true durante un frame)
        anim.SetBool("Hit", false);

    }
}
