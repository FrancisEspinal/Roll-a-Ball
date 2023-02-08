using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// Agregamos
using UnityEngine.UI;

public class JugadorController : MonoBehaviour {

    //Declaro la variable de tipo AudioSource que luego asociaremos a nuestro Jugador
    private AudioSource pop;

    //Declaro la variable de tipo RigidBody que luego asociaremos a nuestro Jugador
    private Rigidbody rb;

    //Inicializo el contador de coleccionables recogidos
    private int contador;

    //Inicializo variables para los textos
    public Text textoContador, textoGanar;

    //Declaro la variable pública velocidad para poder modificarla desde la Inspector window
    public float velocidad;

    // Use this for initialization
    void Start () {
    
     //Capturo esa variable al colectar monedas
     pop = GetComponent<AudioSource>();

     //Capturo esa variable al iniciar el juego
     rb = GetComponent<Rigidbody>();

     //Inicio el contador a 0
     contador = 0;

     //Actualizo el texto del contador por primera vez
     setTextoContador();

     //Inicio el texto de ganar a vacío
     textoGanar.text = "";

    }

    // Para que se sincronice con los frames de física del motor

    // FixedUpdate, ya que se ejecutará 0, una o varias veces por frame en función de los frames de física del motor de Unity e irá más sincronizado
    void FixedUpdate () {

    //Estas variables nos capturan el movimiento en horizontal y vertical de nuestro teclado
    float movimientoH = Input.GetAxis("Horizontal");
    float movimientoV = Input.GetAxis("Vertical");

    //Un vector 3 es un trío de posiciones en el espacio XYZ, en este caso el que corresponde al movimiento

    Vector3 movimiento = new Vector3(movimientoH, 0.0f, movimientoV);

    //Asigno ese movimiento o desplazamiento a mi RigidBody, multiplicado por la velocidad que quiera darle
    rb.AddForce(movimiento * velocidad);

    }

    //Se ejecuta al entrar a un objeto con la opción isTrigger seleccionada
    void OnTriggerEnter(Collider other)

    {

        if (other.gameObject.CompareTag("Coleccionable"))

        {

     //Desactivo el objeto
     other.gameObject.SetActive(false);

     //Incremento el contador en uno (también se puede hacer como contador++)
     contador = contador + 1;
     pop.Play();

     //Actualizo el texto del contador
     setTextoContador();

        }
    }
    void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
    //Actualizo el texto del contador (O muestro el de ganar si las ha cogido todas)
    void setTextoContador(){

     textoContador.text = "Contador: " + contador.ToString();
     if (contador >= 12){
     textoGanar.text = "¡Ganaste!";

     Invoke("RestartGame", 5);

        }
        }
    }