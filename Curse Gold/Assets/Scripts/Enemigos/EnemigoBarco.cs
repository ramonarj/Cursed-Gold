using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoBarco : MonoBehaviour {
	public GameObject barcoen;
	public float velbe = 0.1f;
	public float rotation = 3f;
	// Update is called once per frame
	void Update () {
		transform.Translate (new Vector3 (0f, velbe, 0f));
	    transform.Rotate (new Vector3 (0f, 0f, rotation));
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		MovBarco prueba = other.GetComponent<MovBarco> ();
		prueba.enabled = false;
	}

}
