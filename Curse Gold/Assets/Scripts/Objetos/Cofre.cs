using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cofre : MonoBehaviour {

	//VARIABLES CONFIGURABLES
	public bool enemigo;
	public GameObject Premio;

	//VARIABLES NO CONFIGURABLES
	[HideInInspector]public bool abierto = false;
	Animator anim;
	public AudioClip abrecofre;

	//1.START
	void Start () 
	{
		anim = GetComponent<Animator> ();
	}

	//2.COLISIONES CON EL JUGADOR
	void OnTriggerEnter2D(Collider2D other)
	{
		MovJugador compJugador= other.GetComponent<MovJugador> ();
		if (compJugador != null && !abierto)
		{
			if (enemigo)
				anim.SetBool("Enemigo", true);

			else 
				anim.SetBool("Abierto", true);
			if(abrecofre!=null)
				GetComponent<AudioSource>().PlayOneShot(abrecofre);
			Invoke("Abrir", 0.5f);
		}
	}

	//3.MÉTODO PARA ABRIR EL COFRE
	void Abrir()
	{
		if (!abierto)
		{
			abierto = true;
			if (enemigo)
				anim.SetBool("Abierto", true);
			
			else
			{
				GameObject prem = Instantiate(Premio);
				prem.transform.position = transform.position + new Vector3(0f, 1f, 0f);
	     	}
		}
	}
}
