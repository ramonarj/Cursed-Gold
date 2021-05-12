using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparo : MonoBehaviour
{
	//VARIABLES PÚBLICAS
	public float velocidadBala = 1;
	public enum TipoBala {BalaJugador, BalaEnemigo}
	public TipoBala TipoDeBala;
	public bool balaBoss = false;
	Color c;
	Renderer render;
	float incr;

	void Start()
	{
		if (balaBoss) 
		{
			render = GetComponent<Renderer> ();
			c = render.material.color;
		}
			
	}
	//1.UPDATE
	void Update()
	{
		transform.Translate(new Vector3(Time.deltaTime * (-transform.localScale.x * velocidadBala), 0f, 0.0f));
		if (balaBoss)
		{
			if (c.a >= 1)
				incr = -Time.deltaTime*3.5f;
			else if (c.a <= 0)
				incr = Time.deltaTime*3.5f;
			
			c.a += incr;
			render.material.color = c;
		}
	}

	//2.MÉTODO PARA VOLVERSE INVISIBLE AL SALIR DE LA PANTALLA
	void OnBecameInvisible()
	{
		Destroy(gameObject);
	}

	//3.DESTRUCCIÓN AL COLISIONAR
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.layer == 8 || other.gameObject.layer == 9) {
			Destroy (this.gameObject);
		}
	}
}
