using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarineEspada : MonoBehaviour
{
	//Variables públicas
	public Transform jugador;
	public int rangoDeVisión;
	public int velocidad;

	//Variables privadas
	Animator animacion;
	bool atacando = false;
	[HideInInspector] public bool muere = false;
	float distanciaX, distanciaY;
	public int vida = 2;

	public AudioClip daño;

	//1.START
	void Start () 
	{
		animacion = GetComponent<Animator>();
	}
	
	//2.UPDATE
	void Update () 
	{
		//Si el jugador se encuentra en el rango de visión, lo perseguimos
		if (JugadorCerca(out distanciaX, out distanciaY))
		{
			if (!atacando && !muere)
				PersigueJugador(distanciaX, distanciaY);
		}
			else 
				animacion.SetBool("Andando", false);
	}

	//3.COMPRUEBA SI EL JUGADOR ESTÁ CERCA
	bool JugadorCerca(out float distanciaX, out float distanciaY)
	{
		bool cerca = false;
		distanciaX = Mathf.Abs(jugador.position.x - transform.position.x);
		distanciaY = Mathf.Abs(jugador.position.y - transform.position.y);

		if (Mathf.Sqrt(Mathf.Pow(distanciaX,2.0f)+Mathf.Pow(distanciaY,2)) < rangoDeVisión)
			cerca = true;

		return (cerca);
	}

	//4.PERSIGUE AL JUGADOR
	void PersigueJugador(float distanciaX, float distanciaY)
	{
		//El jugador está a una altura asequible
		if (distanciaY < 1) 
		{
			//Perseguirlo hacia la derecha
			if (transform.position.x < jugador.position.x) 
				Corre(1);

			//Perseguirlo hacia la izquierda
			else if (transform.position.x > jugador.position.x)
				Corre(-1);
		} 

		//El jugador no está a una altura asequible pero está cerca
		else if (distanciaX > 1) 
		{
			//Perseguirlo un poco
			if (transform.position.x < jugador.position.x + 0.75) 
				Corre(1);

			//Perseguirlo un poco hacia la izquierda
			else if (transform.position.x > jugador.position.x - 0.75) 
				Corre(-1);
		}

		//Se para
		else 
			animacion.SetBool ("Andando", false);
	}

	//5.DEJA DE ATACAR
	void DejaDeAtacar() 
	{
		animacion.SetBool("Andando", true);
		animacion.SetBool("Ataca", false);
		atacando = false;
    }

	//6.COLISIÓN CON LA BALA
	public void OnTriggerEnter2D(Collider2D other)
	{
		Disparo compDisparo = other.GetComponent<Disparo>();
		if (compDisparo != null && compDisparo.TipoDeBala == Disparo.TipoBala.BalaJugador)
		{
			vida--;
			GetComponent<AudioSource>().PlayOneShot(daño);
			Destroy(other.gameObject);
			//Muere
			if (vida < 1)
			{
				animacion.SetBool("Muere", true);
				muere = true;
				Invoke("Muere", 0.5f);
			}
			//Knockback
			else 
			{
				Rigidbody2D rigid = GetComponent<Rigidbody2D>();
				if (other.transform.position.x > transform.position.x)
						rigid.AddForce(new Vector2(-150f, 100f));
				else
						rigid.AddForce(new Vector2(150f, 100f));
				
			}
		}
	}

	//7.COLISiÓN CON EL JUGADOR
	void OnCollisionEnter2D(Collision2D other)
	{
		AtacaJugador(other.gameObject);
	}

	void OnCollisionStay2D(Collision2D other)
	{
		AtacaJugador(other.gameObject);
	}

	//8.MUERTE
	public void Muere()
	{
		Destroy(gameObject);
	}

	//9.CORRE HACIA UN LADO; 1=DCHA, -1=IZDA
	public void Corre(int dir) 
	{
		transform.position += new Vector3(dir* velocidad * Time.deltaTime, 0f, 0f);
		animacion.SetBool("Andando", true);
		transform.localScale = new Vector3(dir* -1f, 1f, 1f);
	}

	//10.ATACA AL JUGADOR
	void AtacaJugador(GameObject go) 
	{
		MovJugador Jugador = go.GetComponent<MovJugador>();
		if (Jugador != null && !atacando)
		{
			atacando = true;
			animacion.SetBool("Andando", false);
			animacion.SetBool("Ataca", true);
			Invoke("DejaDeAtacar", 0.5f);
		}
	}
}