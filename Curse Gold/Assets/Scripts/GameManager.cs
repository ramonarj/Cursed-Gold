using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(MovJugador))]

public class GameManager : MonoBehaviour {

	//Visibles al editor (GO)
	public static GameManager instance;
	public Transform BarraDeVida, BarraCooldown;
	public Text textoMonedas, textoPociones, textoVidas;
	public Image cara1, cara2, cara3;
	public GameObject controles;
	public GameObject jugador;
	public GameObject video;
	public GameObject panelPausa;
	public Transform CheckTrans;
	public GameObject dialogos;
	public GameObject camara;
	public GameObject limiteBoss;
	public GameObject bordeBoss;
	public GameObject boss;
	public GameObject[] panelIslas = new GameObject[6];
	public GameObject Panel1;
	public Text textoIslas;
	public GameObject barraBoss;



	//audio
	public AudioClip cura, Saco;
	AudioSource musica;

	//No visibles
	//Estáticas
	static int numVidas, numMonedas, numPociones, nivelActual;
	static bool checkpoint;

	//No estáticas
	int vida, dañorec;
	float tiempoRecarga, anchuraVida, anchuraCool, anchuraBoss, tiempoCool, tiempoVida;
	bool recargando, activarControles, perdiendoVida;
	bool pausa = false;
	bool enCombateJefe = false;
	Vector3 zonaCheckpoint;


    
	//1.START
	void Awake() 
	{
		/*tiempoPantalla = 0;
		pantallaNegra.gameObject.SetActive (false);*/

		musica = camara.GetComponent<AudioSource>();


		tiempoCool = 0;
		tiempoVida = 0;
		vida = 10;

		instance = this;
		if (BarraDeVida != null)
			anchuraVida = BarraDeVida.localScale.x;
		if (BarraCooldown != null)
			anchuraCool = BarraCooldown.localScale.x;

		for (int i = 0; i < panelIslas.Length; i++) 
		{
			panelIslas [i].gameObject.SetActive (false);
		}

		recargando = false;
		activarControles = false;
		perdiendoVida = false;

		ActualizaGUI();
	}

	//2.UPDATE
    void Update() 
    {
		//tiempoPantalla += Time.deltaTime;
		if(panelPausa!=null)
			Pausa ();

        if(recargando)
        {
            tiempoCool += Time.deltaTime;
			if (tiempoCool < tiempoRecarga)
				BarraCooldown.localScale += new Vector3(Time.deltaTime * (anchuraCool / tiempoRecarga), 0f, 0f);

			else
			{
				tiempoCool = 0;
                recargando = false;
            }  
        }	

		if (perdiendoVida)
		{
			tiempoVida += Time.deltaTime;
			if (tiempoVida < 1 && BarraDeVida.localScale.x>0)
				BarraDeVida.localScale -= new Vector3(Time.deltaTime*(anchuraVida/10*dañorec), 0f, 0f);

			else 
			{
				tiempoVida = 0;
				perdiendoVida = false;
			}  
		}
    }

	//3.RECIBE DAÑO
	public bool RecibeDaño(int daño, bool deadzone) 
	{
		bool muerto = false;

		dañorec = daño;
		vida-=daño;
		if (deadzone)
			perdiendoVida = false;
		else
			perdiendoVida = true;

		//El jugador se muere
		if (vida < 1)
		{
			muerto = true;

			//Fin de la partida (vidas=1)
			if (numVidas == 1) 
			{
				if (deadzone)
					GameOver();
				else 
					Invoke("GameOver", 1f);
			}

			//Se reinicia el nivel(vidas>1)
			else
			{
				if (deadzone)
					ReiniciaNivel();
				else 
				{
					perdiendoVida = true;
					Invoke("ReiniciaNivel", 1.0f);
				}

			}
		}
		ActualizaGUI();

		return(muerto);
	}

	//4.REINICIA EL NIVEL ACTUAL
	void ReiniciaNivel()
	{
		numVidas--;
		vida = 10;
		ActualizaGUI();
		BarraDeVida.localScale = new Vector3(anchuraVida, 1f, 0f);

		if (checkpoint)
			jugador.transform.position = zonaCheckpoint - new Vector3(0f, 1f, 0f);
		else
			SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}

	//5.CURACIÓN (5 uds)
	public void Curación() 
	{
		if (numPociones > 0 && vida<10)
		{
			vida += 5;
			BarraDeVida.localScale += new Vector3
				(anchuraVida / 2, 0f, 0f);

			if (vida > 10)
			{
				vida = 10;
				BarraDeVida.localScale = new Vector3(anchuraVida,
													1f, BarraDeVida.localScale.z);
			}

			numPociones--;
			ActualizaGUI();
			GetComponent<AudioSource>().PlayOneShot(cura);
		}
	}

	//6.GANA MONEDAS
	public void GanaMonedas(int n)
	{
		numMonedas += n;
		if (numMonedas >= 10)
		{
			numMonedas -= 10;
			numVidas++;
		}
		GetComponent<AudioSource>().PlayOneShot(Saco);

		ActualizaGUI();
	}

	//7.GANA POCIÓN
	public void GanaPoción() 
	{
		numPociones++;
		ActualizaGUI();
	}

	//8.GAME OVER
	public void GameOver()
	{
		if(File.Exists("partida"))
			File.Delete("partida");
		SceneManager.LoadScene ("PantallaMuerte");
	}

	//9.ACTUALIZA EL GUI
	public void ActualizaGUI()
	{
		if (cara1 != null)
		{
			cara1.gameObject.SetActive(vida > 6);
			cara2.gameObject.SetActive(vida > 2 && vida <= 6);
			cara3.gameObject.SetActive(vida<=2);
		}

		if (textoVidas != null)
		{
			textoVidas.text = "x " + numVidas;
			textoMonedas.text = "x " + numMonedas;
			textoPociones.text = "x " + numPociones;
		}

		if(controles!=null)
			controles.SetActive (activarControles == true);
		if(panelPausa!=null)
			panelPausa.SetActive (pausa == true);
	}

	 //10.RECARGA LA BARRA DE COOLDOWN
    public void Disparo(float tiemporec)
	{
		tiempoRecarga = tiemporec;
		BarraCooldown.localScale = new Vector3(0f, 1f, 0f);
		recargando = true;
	}

	//11.INDICA QUE HEMOS LLEGADO AL CHECKPOINT DEL NIVEL
	public void Checkpoint()
	{
		zonaCheckpoint = CheckTrans.position;
		Destroy(CheckTrans.gameObject);
		checkpoint = true;
	}

	//10.BOTONES
	//10.1.Nueva Partida
	public void NuevaPartida()
	{
		if (File.Exists("partida"))
			File.Delete("partida");
		SceneManager.LoadScene ("Tutorial");

		numVidas = 3;
		numMonedas = 0;
		numPociones = 0;
		nivelActual = 0;
		checkpoint = false;
	}

	//10.2.Continuar (lee un archivo)
	public void Continuar() 
	{
		if (File.Exists("partida"))
		{
			StreamReader entrada = new StreamReader("partida");
			nivelActual = int.Parse(entrada.ReadLine());
			numVidas = int.Parse(entrada.ReadLine());
			numMonedas = int.Parse(entrada.ReadLine());
			numPociones = int.Parse(entrada.ReadLine());

			if (nivelActual > 0)
				SceneManager.LoadScene ("MovBarco");
			else
				SceneManager.LoadScene ("Tutorial");
		}
	}

	//10.3.Controles
	public void BotónControles()
	{
		activarControles = !activarControles;
		ActualizaGUI ();
	}

	//10.4.Creditos
	public void Créditos() 
	{
		SceneManager.LoadScene ("Créditos");
	}
	//10.5.Salir
	public void Salir() 
	{
		Application.Quit();
	}

	//11.NOS LLEVA AL MENÚ DEL BARCO
	public void FinNivel()
	{
		checkpoint = false;
		SceneManager.LoadScene ("Dialogos");
	}

	//INTERACCIONES CON ISLAS


	public void ActivaPanelIslas(Collider2D other)
	{
		MovBarco compBarco = jugador.GetComponent<MovBarco>();
		compBarco.enabled = false;

		Panel1.gameObject.SetActive (true);

		if (other.gameObject.tag == "tutorial") {
			panelIslas [1].gameObject.SetActive (true);
			panelIslas [2].gameObject.SetActive (true);
			panelIslas [3].gameObject.SetActive (false);
			panelIslas [4].gameObject.SetActive (false);
			panelIslas [5].gameObject.SetActive (false);
			panelIslas [0].gameObject.SetActive (false);
			textoIslas.text = "Pardiez, ¿quién es el que está tomando el rumbo?, he dicho a la siguiente" +
			"isla, como se vuelva a equivocar le haré pasar por la tabla.";
		}

		else if (other.gameObject.tag == "isla1") {
			panelIslas [3].gameObject.SetActive (true);
			panelIslas [2].gameObject.SetActive (false);
			panelIslas [4].gameObject.SetActive (false);
			panelIslas [5].gameObject.SetActive (false);
			if (nivelActual == 1) {
				panelIslas [0].gameObject.SetActive (true);
				panelIslas [1].gameObject.SetActive (false);
			} 
			else {
				panelIslas [1].gameObject.SetActive (true);
				panelIslas [0].gameObject.SetActive (false);
				textoIslas.text = "No es el momento de desembarcar en esta isla.";
			}
		} 

		else if (other.gameObject.tag == "isla2") {
			panelIslas [4].gameObject.SetActive (true);
			panelIslas [2].gameObject.SetActive (false);
			panelIslas [3].gameObject.SetActive (false);
			panelIslas [5].gameObject.SetActive (false);
			if (nivelActual == 2) {
				panelIslas [0].gameObject.SetActive (true);
				panelIslas [1].gameObject.SetActive (false);
			} 
			else {
				panelIslas [1].gameObject.SetActive (true);
				panelIslas [0].gameObject.SetActive (false);
				textoIslas.text = "Por todos los dioses, nos hemos equivocado de isla grumetes.";
			}
		} 

		else {
			panelIslas [5].gameObject.SetActive (true);
			panelIslas [2].gameObject.SetActive (false);
			panelIslas [3].gameObject.SetActive (false);
			panelIslas [4].gameObject.SetActive (false);
			if (nivelActual == 3) {
				panelIslas [0].gameObject.SetActive (true);
				panelIslas [1].gameObject.SetActive (false);
			} 
			else {
				panelIslas [1].gameObject.SetActive (true);
				panelIslas [0].gameObject.SetActive (false);
				textoIslas.text = "Mi padre no se adentraría en una isla sin razón, y yo tampoco";
			}
		}
	}

	//Cierra el panel
	public void cierraPanel()
	{
		jugador.GetComponent<MovBarco>().enabled = true;
		jugador.transform.RotateAround(jugador.transform.position, new Vector3(0f,0f, 1f), 180f);

		Panel1.transform.GetChild (0).gameObject.SetActive(false);
		Panel1.transform.GetChild (1).gameObject.SetActive(false);
		Panel1.SetActive (false);
	}

	//Carga el nivel
	public void cargaNivel()
	{
		if (nivelActual == 1)
			SceneManager.LoadScene ("Nivel 1");
		else if (nivelActual == 2)
			SceneManager.LoadScene ("Nivel 2");
		else if (nivelActual == 3)
			SceneManager.LoadScene ("NivelBoss");
	}
		
	//11. PAUSA
	public void Pausa()
	{
		int escena = SceneManager.GetActiveScene ().buildIndex - 2;
		if (escena > -1 && escena < 4 )
		{
			if (Input.GetKeyDown ("p"))
			{
				pausa = !pausa;
				if (pausa)
					musica.Pause();
				else if (!(nivelActual==3 && !enCombateJefe))
					musica.Play();

				jugador.GetComponent<MovJugador> ().enabled = !jugador.GetComponent<MovJugador> ().enabled;
				Time.timeScale = Time.timeScale == 0 ? 1 : 0;
				ActualizaGUI ();
			}
		}
	}

	//11.1 REANUDAR
	public void Reanudar()
	{
		if (!(nivelActual==3 && !enCombateJefe))
			musica.Play();
		pausa = false;
		jugador.GetComponent<MovJugador> ().enabled = true;
		Time.timeScale = Time.timeScale == 0 ? 1 : 0;
		ActualizaGUI ();
	}

	//11.2 VOLVER A MENU
	public void Menu()
	{
		//Venimos desde un nivel
		if (SceneManager.GetActiveScene().buildIndex != 9)
		{
			if (File.Exists("partida"))
				File.Delete("partida");
			StreamWriter salida = new StreamWriter("partida");
			salida.WriteLine(SceneManager.GetActiveScene().buildIndex - 2);
			salida.WriteLine(numVidas);
			salida.WriteLine(numMonedas);
			salida.WriteLine(numPociones);
			salida.Close();

			Time.timeScale = Time.timeScale == 0 ? 1 : 0;
			SceneManager.LoadScene("Menu");
			ActualizaGUI();
		}

		//Venimos desde los créditos
		else
			SceneManager.LoadScene("Menu");
	}

	//12.DIALOGOS
	public void desactivaComp(bool finDialogo)
	{
		if (!finDialogo) 
		{
			musica.Stop();
			jugador.GetComponent<MovJugador> ().enabled = false;
		}
		else 
		{
			if(nivelActual!=3)
				musica.Play();
			jugador.GetComponent<MovJugador> ().enabled = true;
			dialogos.GetComponent<DialogosEscenas> ().enabled = false;
		}
	}

	//13.DEVUELVE EL NIVEL ACTUAL
	public int NivelActual()
	{
		return nivelActual;
	}

	//14.AUMENTA EL NIVEL ACTUAL
	public void aumentaNivel()
	{
		nivelActual++;
	}

	//15.FIJA LA CÁMARA PARA LA PELEA CONTRA EL BOSS
	public void FijaCamara() 
	{
		enCombateJefe = true;
		musica.Play();

		boss.GetComponent<Boss2> ().enabled = true;
		barraBoss.SetActive(true);
		bordeBoss.SetActive(true);
		anchuraBoss = barraBoss.transform.localScale.x;

		camara.GetComponent<TargetCamara>().enabled = false;
		camara.transform.position = new Vector3(85f, -12f, camara.transform.position.z);
		camara.GetComponent<Camera> ().orthographicSize = 7;

		Transform cielo = camara.transform.GetChild(0).transform;
		cielo.localScale = new Vector3(cielo.localScale.x * 1.8f, cielo.localScale.y * 1.8f, 0f);
		limiteBoss.SetActive(true);
	}

	//16.ACTUALIZA LA VIDA DEL BOSS
	public void BossHerido(int vidaJohn)
	{
		barraBoss.transform.localScale = new Vector3((anchuraBoss/8) * vidaJohn,
													 barraBoss.transform.localScale.y, barraBoss.transform.localScale.z);
	}
}