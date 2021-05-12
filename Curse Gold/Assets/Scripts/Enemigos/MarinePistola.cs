using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarinePistola : MonoBehaviour
{
	//Variables públicas (transforms y GO)
	public Transform Jugador;
	public GameObject bala;

	//Variables públicas (números)
	public float RangoDeVisión;
	public float frecuenciaDisparo;
	[HideInInspector] public bool muere = false;
	public int vida=2;

	public AudioClip daño, disparo;

	//Variables privadas
	float tiempo = 0;
	Animator animacion;
	Transform Pistola, Cañón;

	//1.START
	void Start()
	{
		animacion = GetComponent<Animator>();
		Pistola = transform.GetChild(0);
		Cañón = Pistola.GetChild(0);
	}

	//2.UPDATE
	void Update()
	{
		float distanciaX, distanciaY;

		//Apunta al jugador
		if (JugadorCerca(out distanciaX, out distanciaY) && !muere)
			ApuntaJugador(distanciaX, distanciaY);

		else if (!muere)
		{
			animacion.SetBool("Apuntando", false);
			Pistola.gameObject.SetActive(false);
			tiempo = 0;
		}
	}

	//3.COMPRUEBA SI EL JUGADOR ESTÁ CERCA
	bool JugadorCerca(out float disX, out float disY)
	{
		bool cerca = false;
		disX = Mathf.Abs(Jugador.position.x - transform.position.x);
		disY = Mathf.Abs(Jugador.position.y - transform.position.y);

		if (disX < RangoDeVisión)
			cerca = true;

		return (cerca);
	}

	//4.APUNTA AL JUGADOR
	void ApuntaJugador(float distanciaX, float distanciaY)
	{
		animacion.SetBool("Apuntando", true);
		Pistola.gameObject.SetActive(true);

		//Se rota
		if (Jugador.position.x < transform.position.x)
			transform.localScale = new Vector3(1f, 1f, 1f);
		else
			transform.localScale = new Vector3(-1f, 1f, 1f);

		//Calculamos el ángulo
		float angulo = (Mathf.Atan(distanciaY / distanciaX) * (180 / Mathf.PI));

		//Apunta si no hace mucho ángulo
		if (Mathf.Abs(angulo) < 50)
		{
			tiempo += Time.deltaTime;
			if ((Jugador.position.x < transform.position.x && Jugador.position.y > transform.position.y)
				|| (Jugador.position.x > transform.position.x && Jugador.position.y < transform.position.y))
				Pistola.rotation = Quaternion.AngleAxis(-angulo, new Vector3(0f, 0f, 1f));
			else
				Pistola.rotation = Quaternion.AngleAxis(angulo, new Vector3(0f, 0f, 1f));

			//Dispara al jugador
			if (tiempo > frecuenciaDisparo)
			{
				DisparaJugador();
				tiempo = 0;
			}
		}

		//Hace mucho ángulo
		else
		{
			tiempo = 0;
			animacion.SetBool("Apuntando", false);
			animacion.SetBool("Disparando", false);
			Pistola.gameObject.SetActive(false);
		}
	}

	//5.DISPARA AL JUGADOR
	void DisparaJugador()
	{
		animacion.SetBool("Disparando", true);
		GetComponent<AudioSource>().PlayOneShot(disparo);
		GameObject balita = Instantiate(bala);
		balita.transform.position = Cañón.position;
		balita.transform.rotation = Pistola.rotation;
		balita.transform.localScale = transform.localScale;
		Invoke ("ParaDisparo", 0.3f);
	}
		
	//6.PARA LA ANIMACIÓN DE DISPARO
	void ParaDisparo()
	{
		animacion.SetBool("Disparando", false);
	}

	//7.COLISIÓN CON LA BALA
	public void OnTriggerEnter2D(Collider2D other)
	{
		Disparo compDisparo = other.GetComponent<Disparo>();
		if (compDisparo != null && compDisparo.TipoDeBala==Disparo.TipoBala.BalaJugador)
		{
			vida--;
			GetComponent<AudioSource>().PlayOneShot(daño);
			Destroy(other.gameObject);
			if (vida < 1)
			{
				animacion.SetBool("Muere", true);
				muere = true;
				Destroy(Pistola.gameObject);
				Invoke("Muere", 0.45f);
			}
		}
	}

	//8.MUERTE
	public void Muere()
	{
		Destroy(gameObject);
	}
}