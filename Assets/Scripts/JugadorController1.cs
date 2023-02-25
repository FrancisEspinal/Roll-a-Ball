using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// Agregamos
using UnityEngine.UI;

public class JugadorController1 : MonoBehaviour {

    //Declaro la variable de tipo AudioSource que luego asociaremos a nuestro Jugador al colectar monedas
    private AudioSource pop;

	//Declaro la variable de tipo AudioSource que luego asociaremos a nuestro Jugador al caer
	public AudioSource death;

	//Declaro la variable de tipo AudioSource que luego asociaremos a nuestro Jugador al saltar
	public AudioSource jump;

    //Declaro la variable de tipo RigidBody que luego asociaremos a nuestro Jugador
    private Rigidbody rb;

    //Inicializo el contador de coleccionables recogidos
    private int contador;

    //Inicializo variables para los textos
	public Text textoContador, textoGanar, textoPerder;

    //Declaro la variable pública velocidad para poder modificarla desde la Inspector window
    public float velocidad;

	//Declaro la variable pública saltar para poder modificarla desde la Inspector window
	public float jumpForce;

	//Declaro la variable pública para el punto de respawn
	public Transform respawnPoint;

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

	 //Inicio el texto de perder a vacío
	 textoPerder.text = "";

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
        SceneManager.LoadScene(6);
    }

    //Actualizo el texto del contador (O muestro el de ganar si las ha cogido todas)
    void setTextoContador(){

     textoContador.text = "Contador: " + contador.ToString();
     if (contador >= 26){
     textoGanar.text = "¡Ganaste!";

     Invoke("RestartGame", 5);

           }
        }

	void ResetGame()
	{
		SceneManager.LoadScene(1);
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag ("Enemy"))
		{
			textoPerder.text = "¡Mejor suerte la próxima!";
			velocidad=0;
			jumpForce=0;
			Invoke("ResetGame", 5);
		}
	}

	private void Update()
	{
		//Tecla a utilizar para el salto
		if (Input.GetKeyDown(KeyCode.Space))
		{
			jump.Play();
			rb.velocity = new Vector3 (rb.velocity.x,jumpForce,rb.velocity.z);
		}
		if (transform.position.y < -10)
		{
			death.Play ();
			textoPerder.text = "¡Mejor suerte la próxima!";
			velocidad=0;
			jumpForce=0;
			Invoke("ResetGame", 5);
			Respawn();
		}
	}

	void Respawn()
	{
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
		rb.Sleep();
		transform.position = respawnPoint.position;

	}

    }