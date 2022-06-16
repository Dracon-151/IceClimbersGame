 //Script para controlar el player
//Creado por Alexis Alvarado, Daniel Sepulveda y Eduardo Gonzalez
//Fecha: 02/05/2022

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //Declaración de variables serializadas para aparecer en el editor.
    [SerializeField] private LayerMask floor;
    [SerializeField] private AudioClip[] clips;

    //Declaración de variables privadas.
    private AudioSource audios;
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
    private int alturainicial;
    private int calculoAltura;
    private int aumento;
    private float bonusAltura = 0;
    private int printScore;
    private bool hit = false;

    //Declaración de variables publicas.
    public Text scoreText;
    public Text alturaText;
    public float score = 0;
    public int altura = 0;
    public int frutas = 0;
    public int tiempo = 0;
    
    //A los componentes inicializar arriba y en la funcion start localizarlos, en el control busca dentro de la escena un objeto
    //de tipo control que es el script
    void Start()
    {
        //Se ignoran las coliciones del player con las frutas para que no altere el movimiento.
        Physics2D.IgnoreLayerCollision(7, 9);

        //Inicializaci�n de las variables
        control = GameObject.FindObjectOfType<Controls>();
        sprtR = this.GetComponent<SpriteRenderer>();
        anim = this.GetComponent<Animator>();
        hitboxFloor = this.GetComponentInChildren<CapsuleCollider2D>();
        body = this.GetComponent<Rigidbody2D>();
        alturainicial = (int) transform.position.y;
        audios = this.GetComponent<AudioSource>();
    }

    void Update()
    {
        //Llamadas a funciones de movimiento y sus respectivas animaciones.
        animations();
        movement();

        //Declaracion del tiempo de las acciones.
        delayDoubleJump -= Time.deltaTime;
        delayHit -= Time.deltaTime;
        tiempo += (int)(Time.deltaTime * 1000);

        //Tiempo de enfiramiento para el movimiento despues de un golpe.
        if (delayHit < 0.25f && hit) hit = false;

        //Calcula el valor de la altura que se mostrara en la UI.
        calculoAltura = (int)transform.position.y - alturainicial;
        if (calculoAltura > altura)
        {
            bonusAltura += 0.05f;
            aumento = calculoAltura - altura;
            score += (aumento * 8) + bonusAltura;
            altura = calculoAltura;
            alturaText.text = altura.ToString();
            //Imprime la altura.
        }

        //Imprime la puntuación.
        printScore = ((int)Mathf.Floor(score));
        scoreText.text = "" + printScore;
    }

    //Funcion para el movimiento
    private void movement()
    {
        //Verifica que no exista un tiempo de enfriamiento que impida el movimiento.
        if (!hit)
        {
            //Si detecta que se indica una direccion, asigna la velocidad de movimiento.
            if (control.direction.magnitude > 0.01f)
            {
                body.velocity = new Vector2(control.direction.x * speed, body.velocity.y);
            }
            else
            {
                //Verifica si esta tocando una superficie.
                if (isTouchingFloor)
                {
                    if (control.direction.x == 0)
                    {
                        body.velocity = new Vector2(Mathf.Lerp(body.velocity.x, 0, 0.2f), body.velocity.y);
                    }
                }
            }

            //Habilita el doble salto cuando se esta tocando el suelo.
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

            //Si detecta que la direccion es hacia arriba inicia el salto.
            if (control.direction.y > 0)
            {
                jump();
            }
        }
    }

    //Función para el salto.
    private void jump()
    {
        //Si esta tocando el suelo realiza el salto.
        if (isTouchingFloor)
        {
            audios.PlayOneShot(clips[0]);
            body.velocity = new Vector2(body.velocity.x, speed * 3);
            delayDoubleJump = 0.25f;
        }
        //Si no esta tocando el suelo realiza el doble salto si es que este esta habilitado.
        else if(canDoubleJump && !isTouchingFloor && delayDoubleJump < 0)
        {
            audios.PlayOneShot(clips[0]);
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

    //Función que evalua las colisiones.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Si detecta que es un obstaculo asigna la fuerza y la animacion del golpe.
        if (collision.gameObject.layer == 6 && delayHit < 0)
        {
            audios.PlayOneShot(clips[3]);
            body.velocity = new Vector2(transform.position.x - collision.transform.position.x,
                transform.position.y - collision.transform.position.y).normalized * speed * 2;
            //Se asigna el tiempo de enfriamiento antes de poder ser golpeado nuevamente.
            delayHit = 0.5f;
            hit = true;
            anim.SetBool("Hit", true);
        }
        else
        {
            anim.SetBool("Hit", false);
        }

        //Detecta si la colisión que con un obstaculo que finalice el juego.
        if(collision.gameObject.layer == 8)
        {
            audios.PlayOneShot(clips[2]);
            //Muestra la pantalla de muerte.
            GameObject.FindObjectOfType<Overlays>().death(altura, printScore);

            //Saca al player de la pantalla.
            transform.position = new Vector3(8000, -100, 0);

            //Aumenta la variable prefab de la cantidad de muertes.
            PlayerPrefs.SetInt("Deaths", PlayerPrefs.GetInt("Deaths")+1);
            PlayerPrefs.Save();
            LBManager.updateScores(3);

            //Calcula si se rompio algun record anterior y lo carga en las tablas de clasificación.
            if (PlayerPrefs.GetInt("BestScore") < printScore)
            {
                PlayerPrefs.SetInt("BestScore", printScore);
                PlayerPrefs.Save();
                LBManager.updateScores(1);
            }
            if (PlayerPrefs.GetInt("BestTime") < tiempo)
            {
                PlayerPrefs.SetInt("BestTime", tiempo);
                PlayerPrefs.Save();
                LBManager.updateScores(2);
            }
            if (PlayerPrefs.GetInt("BestHeight") < altura)
            {
                PlayerPrefs.SetInt("BestHeight", altura);
                PlayerPrefs.Save();
                LBManager.updateScores(5);
            }
            if (PlayerPrefs.GetInt("BestFruits") < frutas)
            {
                PlayerPrefs.SetInt("BestFruits", frutas);
                PlayerPrefs.Save();
                LBManager.updateScores(4);
            }

        }

    }

    //Función para las frutas.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Evalua si la colición fue con una fruta.
        if (collision.gameObject.layer == 7)
        {
            audios.PlayOneShot(clips[1]);
            frutas++;

            //Determina el tipo de fruta y aumenta la puntuacion en base a eso.
            if (collision.gameObject.name == "Apple")
            {
                score += 100;
            }
            else if (collision.gameObject.name == "Pineapple")
            {
                score += 250;
            }
            else if (collision.gameObject.name == "Melon")
            {
                score += 500;
            }

            Destroy(collision.gameObject);
        }
    }
}
