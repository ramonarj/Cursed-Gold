using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss2 : MonoBehaviour {

	Rigidbody2D rigid; //componente de fisicas
	public GameObject Jugador;
	float velocidad = 3f, MasVel; 
	bool sigue = true; //ESTA VARIABLE SE PONDRA A FALSE CUANDO EL BOSS SE CANSE Y VAYA A SALTAR
	int vidas = 8;
	bool vulnerable = false;
	bool disparando = false;
	Animator anim;
	public LayerMask CapaSuelo;
	private bool enSuelo = true;
	public Transform TocaSuelo;
	bool animSalto = false;
	bool animCansado = false;
	bool animMuerto = false;

	Color c;
	Renderer render;

	public AudioClip SonidoOnda, SonidoDaño;
	private AudioSource Sonido;

	// Use this for initialization
	void Start () 
	{
		Sonido = GetComponent<AudioSource> ();
		rigid = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		render = GetComponent<Renderer> ();
		c = render.material.color;
		MasVel = velocidad * 1.2f;
	}

	public float DelayDisparo = 4;
	private float TiempoDisparo = 0;

	void FixedUpdate ()
	{
		enSuelo = Physics2D.OverlapBox (TocaSuelo.transform.position,  new Vector2(0.6f, 0.1f), 0.0f, CapaSuelo);
		if (!enSuelo || animSalto == true) 
		{
			anim.SetBool ("cae", true);
			anim.SetBool ("Anda", false);
		} 
		else
		{
			anim.SetBool ("Anda", sigue);
			anim.SetBool ("cae", false);
		}

		anim.SetBool ("Cansado", animCansado);
		anim.SetBool ("dispara", disparando);
		anim.SetBool ("Muere", animMuerto);
	}

	// Update is called once per frame
	void Update () 
	{
		if (sigue)
			siguiendo ();

		Invoke ("cansado", 7f);

		TiempoDisparo += Time.deltaTime;
		if (TiempoDisparo >= DelayDisparo) 
		{
			sigue = false;
			disparando = true;
			rigid.velocity = new Vector3(0.0f, rigid.velocity.y);
			Invoke ("dispara", 1f);
			TiempoDisparo = 0;
		}
		//Debug.Log (vidas + " " + vulnerable);

		if (vidas <= 0)
		{
			TiempoDisparo = 0;
			sigue = false;
			animMuerto = true;
			Invoke ("muere", 1.5f);
		} 
		else if (vidas < 3)
		{
			velocidad = MasVel;
			c.b = 0.6f;
			c.g = 0.6f;
			render.material.color = c;
		}
	}

	void muere()
	{
		Destroy (this.gameObject);
		SceneManager.LoadScene("Dialogos");

	}

	public GameObject ZonaDisparo;
	public GameObject Onda;

	void dispara()
	{
		Sonido.PlayOneShot (SonidoOnda);
		GameObject bala = Instantiate (Onda);
		bala.transform.position = ZonaDisparo.transform.position;
		bala.transform.localScale = transform.localScale;
		disparando = false;
		sigue = true;
	}

	void siguiendo ()
	{
		if (Jugador.transform.position.x <= transform.position.x) {
			rigid.velocity = new Vector3 (-velocidad, rigid.velocity.y, 0);
			transform.localScale = new Vector3 (1f, 1f, 1f);
		} else if (Jugador.transform.position.x >= transform.position.x) {
			rigid.velocity = new Vector3 (velocidad, rigid.velocity.y, 0);
			transform.localScale = new Vector3 (-1f, 1f, 1f);
		}
	}
		
	void cansado()
	{
		animCansado = true;
		TiempoDisparo = 0;
		vulnerable = true;
		sigue = false;
		Invoke ("salto", 3.5f);
	}

	void salto()
	{
		if (!animMuerto)
		{
			animCansado = false;
			vulnerable = false;
			rigid.AddForce(new Vector3(0f, 400f, 0.0f));
			animSalto = true;
			sigue = true;
			CancelInvoke();
			Invoke("CancelaAnimSalto", 1f);
		}
	}

	void CancelaAnimSalto()
	{
		animSalto = false;
	}

	public void OnTriggerEnter2D(Collider2D other)
	{
		Disparo compDisparo = other.GetComponent<Disparo>();
		if (compDisparo != null && compDisparo.TipoDeBala == Disparo.TipoBala.BalaJugador && vulnerable == true)
		{
			Sonido.PlayOneShot (SonidoDaño);
			vidas--;
			GameManager.instance.BossHerido(vidas);
			Destroy(other.gameObject);
		}
	}

	bool atacando = false;
	void OnCollisionStay2D(Collision2D other)
	{
		AtacaJugador(other.gameObject);
	}

	void AtacaJugador(GameObject go) 
	{
		MovJugador Jugador = go.GetComponent<MovJugador>();
		if (Jugador != null && !atacando)
		{
			atacando = true;
			anim.SetBool("Anda", false);
			anim.SetBool("Golpea", true);
			Invoke("DejaDeAtacar", 0.5f);
		}


	}
	void DejaDeAtacar() 
	{
		anim.SetBool("Anda", true);
		anim.SetBool("Golpea", false);
		atacando = false;
	}
}
