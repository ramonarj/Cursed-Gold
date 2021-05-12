using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ecalera : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other)
	{
		MovJugador prueba = other.gameObject.GetComponent<MovJugador>();
		if (prueba != null) {
			Physics2D.gravity = new Vector2 (0, 0);
			prueba.enabled = false;
		}
	}
	void OnTriggerStay2D(Collider2D other)
	{
		MovJugador prueba = other.gameObject.GetComponent<MovJugador>();
		Rigidbody2D rigid = other.gameObject.GetComponent<Rigidbody2D>();
		if (prueba.enabled==false && Input.GetKey (KeyCode.W))
			rigid.velocity = new Vector3 (0, 3f,0);
		else if (prueba.enabled==false && Input.GetKey (KeyCode.S))
			rigid.velocity = new Vector3 (0, -3f,0);
		else if (prueba.enabled==false && Input.GetKey (KeyCode.D))
			rigid.velocity = new Vector3 (2f, 0,0);
		else if (prueba.enabled==false && Input.GetKey (KeyCode.A))
			rigid.velocity = new Vector3 (-2f, 0,0);
	}
	void OnTriggerExit2D(Collider2D other)
	{
		MovJugador prueba = other.gameObject.GetComponent<MovJugador>();
		Physics2D.gravity = new Vector2 (0, -9);
		prueba.enabled = true;
	}

}
		