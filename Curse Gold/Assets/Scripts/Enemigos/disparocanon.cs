using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disparocanon : MonoBehaviour {

	//VARIABLES PÚBLICAS
	public GameObject bala;
	public float tiempo, tiempoanim;
	public AudioClip disparo;

	//VARIABLES PRIVADAS
	Transform angdisp;
	Animator animacion;
	float contador = 0;

	//1.START
	void Start()
	{
		animacion = GetComponent<Animator> ();
		angdisp = transform.GetChild(0);
	}

	//2.UPDATE
	void Update () 
	{
		contador += Time.deltaTime;
		if (contador >= tiempo)
			Anima();
	}

	//3.ANIMACIÓN DE DISPARO
	 void Anima()
	{
		animacion.SetBool ("dispara", true);
		Invoke ("Dispara", tiempoanim);
		contador = 0;
	}

	//4.DISPARO
	void Dispara()
	{
		GameObject nuevo = Instantiate (bala);
		GetComponent<AudioSource>().PlayOneShot(disparo);
		nuevo.transform.position = angdisp.position;
		nuevo.transform.rotation = angdisp.rotation;
		animacion.SetBool("dispara", false);
		Invoke("ParaDisparar", 0.5f);
	}

	void ParaDisparar(){
		animacion.SetBool ("dispara", false);
	}
}