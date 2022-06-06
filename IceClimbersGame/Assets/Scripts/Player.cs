//Script para controlar el player
//Creado por Alexis Alvarado. Modificado por Daniel Sepulveda
//Fecha: 02/05/2022

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private LayerMask floor;

    private Controls control;
    private Animator anim;
    private SpriteRenderer sprtR;
    private Rigidbody2D body;
    private bool isTouchingFloor = false;
    private bool canDoubleJump = true;
    private float delayDoubleJump = 0.25f;
    private float delayHit = 0.5f;
    private float speed = 1.6f;
    private CapsuleCollider2D hitboxFloor;
    public Text scoreText;

    private int score = 0;

    //a los componentes inicializar arriba y en la funcion start localizarlos, en el control busca dentro de la escena un objeto
    //de tipo control que es el script

    void Start()
    {
        //Inicializaci�n de las variables
        control = GameObject.FindObjectOfType<Controls>();
        sprtR = this.GetComponent<SpriteRenderer>();
        anim = this.GetComponent<Animator>();
        hitboxFloor = this.GetComponent<CapsuleCollider2D>();
        body = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        animations();
        movement();

        delayDoubleJump -= Time.deltaTime;
        delayHit -= Time.deltaTime;
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
            body.velocity = new Vector2(body.velocity.x, speed * 3);
            delayDoubleJump = 0.25f;
        }
        else if(canDoubleJump && !isTouchingFloor && delayDoubleJump < 0)
        {
            body.velocity = new Vector2(body.velocity.x, speed * 3);
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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6 && delayHit < 0)
        {
            body.velocity = body.velocity + (new Vector2(transform.position.x - collision.transform.position.x,
                transform.position.y - collision.transform.position.y).normalized * speed / 2);
            delayHit = 0.5f;
            anim.SetBool("Hit", true);
        }
        else
        {
            anim.SetBool("Hit", false);
        }

        if(collision.gameObject.layer == 7)
        {
            if(collision.gameObject.name == "Apple")
            {
                score += 1;
            }
            else if (collision.gameObject.name == "Pineapple")
            {
                score += 3;
            }
            else if (collision.gameObject.name == "Melon")
            {
                score += 5;
            }

            scoreText.text = score.ToString();
             
            Destroy(collision.gameObject);
        }
    }
}
