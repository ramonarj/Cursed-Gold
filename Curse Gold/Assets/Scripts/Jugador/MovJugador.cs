using UnityEngine;
using System.Collections;

public class MovJugador : MonoBehaviour {

	//VARIABLES DE MOVIMIENTO
	public float Vel = 1f;
	public float Salto = 500f;
	private Rigidbody2D Rigid;
	public bool ActivaDobleSalto = false;
	private bool enSuelo = true, TocaPared = false, enBarril=false;
	public Transform TocaSuelo, TocandoPared;
	private Transform TocaBarril;
	float Radio = 0.2f;
	public LayerMask CapaSuelo, CapaBarril;
	private int DobleSalto = 0;

	//VARIABLES PARA COLISIÓN CON ENEMIGOS
	float tiempo;
	public float tiempoDeInmunidad;
	bool herido = false;
	bool muerto = false;

	//VARIABLES DE DISPARO
	public GameObject Bala;
	public GameObject pistola;
	public float DelayDisparo = 3;
	private float TiempoDisparo = 3;

	//VARIABLES DE ANIMACIÓN
	private Animator animacion;
	private bool corriendo = false;
	Renderer render;
	Color c;


	//VARIABLES DE SONIDOS
	public AudioClip SonidoBala, SonidoSalto1, SonidoSalto2, SonidoDaño, cogepocion;
	private AudioSource Sonido;

	//1.START
	void Start ()
	{
		animacion = GetComponent<Animator>();
		Rigid = GetComponent<Rigidbody2D>();
		Sonido = GetComponent<AudioSource>();
		render = GetComponent<Renderer>();
		c = render.material.color;

		tiempo = tiempoDeInmunidad;
		TocaBarril = TocaSuelo;
	}

	//2.UPDATE
	void Update () 
	{
		corriendo = false;
		if (!herido && !muerto)
			Movimiento();
		else if (tiempo > 0.5)
			herido = false;
		
		TiempoDisparo += Time.deltaTime;
		if (Input.GetKeyDown (KeyCode.Space)) 
		{
			if (TiempoDisparo >= DelayDisparo) 
			{
				Dispara ();
				TiempoDisparo = 0;
			}
		}
		//Tiempo de inmunidad
		tiempo += Time.deltaTime;
	}

	//3.FIXEDUPDATE
	void FixedUpdate () {
		enSuelo = Physics2D.OverlapBox (TocaSuelo.transform.position,  new Vector2(0.6f, 0.1f), 0.0f, CapaSuelo);
		//Comprobacion de que sta en barril
		enBarril = Physics2D.OverlapCircle (TocaBarril.position, Radio, CapaBarril);
		//TocaPared = Physics2D.OverlapBox (TocandoPared.position, new Vector2(0.2f, 0.5f), 0f, CapaSuelo);
		TocaPared = Physics2D.OverlapBox (TocandoPared.transform.position, new Vector2(0.3f, 0.8f), 0.0f, CapaSuelo);
		//Misma animacion si se encuentra en el barril como en el suelo.
		if(herido == false)
		animacion.SetBool ("TocandoElSuelo", (enSuelo || enBarril));
		animacion.SetBool ("Movimiento", corriendo);
	}

	//4.MÉTODO PARA MOVER AL JUGADOR
	void Movimiento()
	{
		//Moverse hacia la derecha
		if (Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.RightArrow)) 
		{
			Rigid.velocity = new Vector3(Vel, Rigid.velocity.y, 0.0f);
			corriendo = true;
			transform.localScale = (new Vector3 (-1f, 1f, 1f));
		}

		//Moverse hacia la izquierda
		else if (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.LeftArrow))
		{
			Rigid.velocity = new Vector3(-Vel, Rigid.velocity.y, 0.0f);
			corriendo = true;
			transform.localScale = (new Vector3 (1f, 1f, 1f));
		}

		//Beber poción
		if (Input.GetKeyDown (KeyCode.C)) 
			GameManager.instance.Curación ();
		//salto
		if (ActivaDobleSalto == false) 
			DobleSalto = 1;

		if (((DobleSalto < 1 || enSuelo)||(DobleSalto < 1 || enBarril)) && 
		    (Input.GetKeyDown (KeyCode.W) || Input.GetKeyDown (KeyCode.UpArrow)))
		{
			Rigid.velocity = new Vector3 (Rigid.velocity.x, 0.0f, 0.0f);
			Rigid.AddForce (new Vector2 (0.0f, Salto));
			//EJECUCIÓN DEL AUDIO DE SALTO
			Sonido.volume = 1;
			if(enSuelo)
				GetComponent<AudioSource>().PlayOneShot(SonidoSalto1);
			else
			GetComponent<AudioSource>().PlayOneShot(SonidoSalto2);

			DobleSalto++;
		} 

		else if (enSuelo || enBarril)
			DobleSalto = 0;

		if (TocaPared) 
			Rigid.velocity = new Vector3 (0.0f, Rigid.velocity.y, 0.0f);
	}

	public GameObject Chispas;
	GameObject c2;
	//5.MÉTODO PARA DISPARAR
	void Dispara()
	{
		
		//PARTICULAS
		if (transform.localScale.x == -1) {
			c2 =	Instantiate (Chispas, pistola.transform.position, Chispas.transform.rotation = Quaternion.AngleAxis (90, new Vector3 (0f, 1f, 0f)));
		}
		else 
			c2 = Instantiate (Chispas, pistola.transform.position, Chispas.transform.rotation = Quaternion.AngleAxis(-90, new Vector3(0f, 1f, 0f)));
		//SONIDOS DE LA BALA
        GameManager.instance.Disparo(DelayDisparo);
		Sonido.volume = 0.1f;
		GetComponent<AudioSource>().PlayOneShot(SonidoBala);
		//APARICIÓN DE LA BALA
 		GameObject bala = Instantiate (Bala);
		bala.transform.localScale = transform.localScale;
		bala.transform.position = pistola.transform.position;

		Invoke ("DestruyeParticula", 1f);
	}

	void DestruyeParticula()
	{
		Destroy (c2);
	}

	//6.COLISIONES CON TRIGGERS
	void OnTriggerEnter2D(Collider2D other)
	{
		Disparo compBala = other.GetComponent<Disparo>();
		Moneda compMoneda = other.GetComponent<Moneda> ();
		MovimientoCocodrilo compCoco = other.GetComponent<MovimientoCocodrilo> ();
		Cofre compCofre = other.GetComponent<Cofre> ();

		//Colisión con bala enemiga
		if (compBala != null && compBala.TipoDeBala == Disparo.TipoBala.BalaEnemigo)
		{
			Destroy(other.gameObject);
			if (tiempo > tiempoDeInmunidad)
				RecibeDaño(2, other.gameObject);
		}


		//Colisión con pociones
		else if (other.gameObject.tag == "Poción")
		{
			GetComponent<AudioSource>().PlayOneShot(cogepocion);
			GameManager.instance.GanaPoción();
			Destroy(other.gameObject);
		}
		else if (other.gameObject.tag == "Finish")
			GameManager.instance.FinNivel();
		else if (other.gameObject.tag == "Checkpoint")
		{
			if (GameManager.instance.NivelActual() != 3)
				GameManager.instance.Checkpoint();
			else 
			{
				GameManager.instance.FijaCamara();
				Destroy(other.gameObject);
			}

		}
		else if (other.gameObject.tag == "DeadZone")
		{
			GameManager.instance.RecibeDaño(10, true);
		}
		

		//Colisión con monedas
		else if (compMoneda != null) {
			GameManager.instance.GanaMonedas (compMoneda.Valor);
			Destroy (other.gameObject);
		} 

		//Colisión con cocodrilos
		else if (compCoco != null && tiempo > tiempoDeInmunidad)
			RecibeDaño (1, other.gameObject);


		//Colisión con cofres (enemigos)
		else if (compCofre != null && tiempo > tiempoDeInmunidad
		         && compCofre.enemigo && compCofre.abierto)
			RecibeDaño (2, other.gameObject);
			else
			ColisionPichos(other.gameObject);
	}

	//7.COLISIONES CONSTANTES CON TRIGGERS 
	void OnTriggerStay2D(Collider2D other) 
	{
		//Cofre
		Cofre compCofre = other.GetComponent<Cofre>();
		MovimientoCocodrilo compCoco = other.GetComponent<MovimientoCocodrilo>();
		if (compCofre != null && tiempo > tiempoDeInmunidad && compCofre.enemigo && compCofre.abierto) {
			RecibeDaño (2, other.gameObject);
		}
		//Cocodrilos
		else if (compCoco!=null && tiempo>tiempoDeInmunidad)
			RecibeDaño(1, other.gameObject);
		//Pinchos
		else
			ColisionPichos(other.gameObject);
	}

	//8.COLISIONES QUE NO SON TRIGGERS (MARINES)
	void OnCollisionEnter2D(Collision2D other)
	{
		ColisiónMarines(other.gameObject);
	}

	void OnCollisionStay2D(Collision2D other)
	{
		ColisiónMarines(other.gameObject);
	}

	//9.HIEREN AL JUGADOR
	void RecibeDaño(int n, GameObject go)
	{
		Rigid.velocity = new Vector3(0f, 0f, 0f);
		animacion.SetBool ("Golpeado", true);
		herido = true;
		tiempo = 0;

		Transparenta();
		Knockback(go);
		Sonido.volume = 1;
		GetComponent<AudioSource>().PlayOneShot(SonidoDaño);
		Invoke ("paraGolpeado", 0.2f);

		if (GameManager.instance.RecibeDaño(n, false))
		{
			muerto = true;
			animacion.SetBool("Muerto", true);
			Invoke ("paraGolpeado", 1f);
		}
		
	}

	//10.MÉTODO PARA RECIBIR KNOCKBACK
	void Knockback(GameObject enem)
	{
		if (enem.transform.position.x > transform.position.x)
			Rigid.AddForce(new Vector2(-180f, 200f));
		else
			Rigid.AddForce(new Vector2(180f, 200f));
	}

	//11.MÉTODO PARA HACER TRASLÚCIDO AL JUGADOR
	void Transparenta()
	{
		c.a = 0.6f;
		render.material.color = c;
		Invoke("Opaco", tiempoDeInmunidad-0.1f);
	}

	//12.MÉTODO PARA HACER OPACO AL JUGADOR
	void Opaco() 
	{
		c.a = 1f;
		render.material.color = c;
	}

	//13.RECONOCE SI SE ESTÁ CHOCANDO CON UN MARINE
	void ColisiónMarines(GameObject go)
	{
		Boss2 jefe = go.GetComponent<Boss2> ();
		Pajaro bird = go.GetComponent<Pajaro> ();
		MarineEspada MarineEsp = go.GetComponent<MarineEspada>();
		MarinePistola MarinePis = go.GetComponent<MarinePistola>();


		if ((MarineEsp != null && MarineEsp.muere == false) || (MarinePis != null && MarinePis.muere == false) ||
		    jefe != null || bird != null)

		if (tiempo > tiempoDeInmunidad) {
			if(jefe != null)
			RecibeDaño (2, go);
			else RecibeDaño (1, go);
		}
	}

	//14.RECONOCE SI SE ESTÁ CHOCANDO CON UN PINCHO
	void ColisionPichos(GameObject pinchos)
	{
		pinchos pin = pinchos.GetComponent<pinchos>();
		if (pin != null && tiempo > tiempoDeInmunidad)
			RecibeDaño (1, pinchos);
	}

	void paraGolpeado()
	{
		animacion.SetBool ("Golpeado", false);
		animacion.SetBool("Muerto", false);
		muerto = false;
	}
}