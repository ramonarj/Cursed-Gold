using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrilExplosivo : MonoBehaviour {

	public GameObject zonaadestruir;
	Animator animacion;
	Rigidbody2D prueba;
	public Transform posicionspawn;
	public GameObject spawnear;
	public Transform tocazonacorrecta;
	public LayerMask capadestruye;
	public float radio=1;

	public AudioClip explosion;
	private AudioSource Sonido;

	void Start() 
	{
		animacion = GetComponent<Animator>();
		Sonido = GetComponent<AudioSource> ();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		zonaadestruir = GameObject.FindWithTag ("Muro");
		prueba = this.GetComponent<Rigidbody2D> ();
		Disparo compDisparo = other.GetComponent<Disparo>();
		bool explota = Physics2D.OverlapCircle (tocazonacorrecta.position, radio, capadestruye);
		if (compDisparo != null && compDisparo.TipoDeBala == Disparo.TipoBala.BalaJugador) 
		{
			Sonido.PlayOneShot (explosion);
			BoxCollider2D coll = GetComponent<BoxCollider2D> ();
			coll.enabled = false;
			prueba.isKinematic = true;
			animacion.SetBool("alcanzado", true);
			Invoke ("destruyete", 1.15f);
			if (explota) 
				Invoke ("destruyezona", 0.7f);
		}
	}
	void destruyete()
	{
		Destroy (this.gameObject);
	}
	void destruyezona()
	{
		Destroy (zonaadestruir);
	}
	void spawn()
	{
		Instantiate (spawnear);
		spawnear.transform.position = posicionspawn.transform.position;
	}
}
