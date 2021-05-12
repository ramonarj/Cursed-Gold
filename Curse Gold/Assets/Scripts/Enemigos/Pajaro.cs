using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pajaro : MonoBehaviour {

	public int VelocidadX = 1;
	public float Max, Min;
	public float Tdisparo = 3;
	float Delay = 0;
	public GameObject proyectil;
	public GameObject zonaDisparo;
	float posicionINI;
	Rigidbody2D Rigid;

	// Use this for initialization
	void Start () {
		Rigid = GetComponent<Rigidbody2D> ();
		posicionINI = transform.position.x;
	}
	
	// Update is called once per frame
	void Update () {
	
		Rigid.velocity = new Vector2 (VelocidadX, 0.0f);



		if (transform.position.x > posicionINI + Max)
		{
			transform.position = new Vector3 (posicionINI + (Max -0.1f), transform.position.y, transform.position.z);
			VelocidadX = -VelocidadX;
			transform.localScale = new Vector2 (-transform.localScale.x, transform.localScale.y);
		}
		else if (transform.position.x < posicionINI + Min) {
			transform.position = new Vector3 (posicionINI + (Min +0.1f), transform.position.y, transform.position.z);
			VelocidadX = -VelocidadX;
			transform.localScale = new Vector2 (-transform.localScale.x, transform.localScale.y);
		}

	Delay += Time.deltaTime;

		if (Delay >= Tdisparo) {
			Dispara ();
			Delay = 0; 
		}

	}

	void Dispara()
	{
		GameObject huevo = Instantiate (proyectil);
		huevo.transform.position = zonaDisparo.transform.position;
	}
}
