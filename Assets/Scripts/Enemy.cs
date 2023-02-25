using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour 
{
	public float speed = 0;
	public List<Transform> waypoints;

	//Declaro las variables que se usarán para especificar la distancia que recorrerá nuestro enemigo
	private int waypointIndex;
	private float range;

	//Declaro la variable de tipo AudioSource que luego asociaremos a nuestro Enemigo al tener contacto con el Jugador
	private AudioSource pop;

	// Use this for initialization
	void Start () 
	{
	waypointIndex = 0;	
	range = 1.0f;
	pop = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		Move();	

	}

	void Move()
	{
		transform.LookAt(waypoints[waypointIndex]);
		transform.Translate(Vector3.forward * speed * Time.deltaTime);
		if(Vector3.Distance(transform.position, waypoints[waypointIndex].position) < range)
			{
				waypointIndex++;
				if(waypointIndex >= waypoints.Count)
				{
					waypointIndex = 0;
				}
	     }
      }
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag ("Player"))
		{
			collision.gameObject.SetActive(false);
			pop.Play();
		}
	}
}