using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Dialogos : MonoBehaviour {
	static int nivelActual = 0;
	public GameObject[] escenasDialogos = new GameObject[4];
	public GameObject[] escenasDiario = new GameObject[4];
	public GameObject cargando;
	private string[] dialogosTutorial = {
		"ProtaT.1", "Enano1", "ProtaT.2", "Enano2", "ProtaT.3", "Enano3", "ProtaT.4", "Enano4", "ProtaT.5", "Enano5"
	};

	private string[] dialogosNivel1 = {
		"Prota1.1", "Chica1", "Prota1.2", "Chica2", "Prota1.3", "Chica3", "Prota1.4", "Chica4"
	};

	private string[] dialogosNivel2 = {
		"Prota2.1", "Papu1", "Prota2.2", "Papu2", "Prota2.3", "Papu3","Prota2.4", "Papu4"
	};

	private string[] dialogosFinal = {
		"Prota3.1", "Boss1", "Prota3.2", "Boss2", "Prota3.3", "Boss3"
	};

	public GameObject[] tiposEscenas = new GameObject[2];
	public GameObject[] dialogos;
	public GameObject[] nombres = new GameObject[5];
	public GameObject[] personajes = new GameObject[5];
	//public float tiempoLetra = 0.02f;
	//float tiempoOri;
	int k = 0;
	bool textoTerminado = false;
	//bool fin = false;
	//Text compTexto;

	void Start ()
	{
		nivelActual = GameManager.instance.NivelActual ();
		//nivelActual = 1;
		cargando.gameObject.SetActive (false);
		tiposEscenas [1].gameObject.SetActive (false);
		for (int i = 0; i < escenasDialogos.Length; i++)
		{	//Ddesacitva todas las escenas
			escenasDialogos [i].gameObject.SetActive (false);
			escenasDiario [i].gameObject.SetActive (false);
		}

		for (int i = 0; i < personajes.Length; i++)
		{	//Desactiva los sprites de los personajes
			personajes [i].gameObject.SetActive (false);
		}

		personajes [0].gameObject.SetActive (true); //El protagonista siempre esta activo
		if (nivelActual == 0)
		{
			escenasDialogos [0].gameObject.SetActive (true);
			dialogos = new GameObject[dialogosTutorial.Length];

			for (int i = 0; i < dialogosTutorial.Length; i++)
			{
				dialogos [i] = GameObject.Find (dialogosTutorial [i]);
			}

			for (int i = 0; i < dialogosTutorial.Length; i++)
			{
				dialogos [i].gameObject.SetActive (false);
			}
		} 

		else if (nivelActual == 1) 
		{
			personajes [2].gameObject.SetActive (true);
			escenasDialogos [1].gameObject.SetActive (true);
			dialogos = new GameObject[dialogosNivel1.Length];

			for (int i = 0; i < dialogosNivel1.Length; i++)
			{
				dialogos [i] = GameObject.Find (dialogosNivel1 [i]);
			}

			for (int i = 0; i < dialogosNivel1.Length; i++)
			{
				dialogos [i].gameObject.SetActive (false);
			}
		} 

		else if (nivelActual == 2)
		{
			escenasDialogos [2].gameObject.SetActive (true);
			dialogos = new GameObject[dialogosNivel2.Length];

			for (int i = 0; i < dialogosNivel2.Length; i++) 
			{
				dialogos [i] = GameObject.Find (dialogosNivel2 [i]);
			}

			for (int i = 0; i < dialogosNivel2.Length; i++) 
			{
				dialogos [i].gameObject.SetActive (false);
			}
		} 

		else 
		{	
			personajes [4].gameObject.SetActive (true);
			escenasDialogos [3].gameObject.SetActive (true);
			dialogos = new GameObject[dialogosFinal.Length];

			for (int i = 0; i < dialogosFinal.Length; i++)
			{
				dialogos [i] = GameObject.Find (dialogosFinal [i]);
			}

			for (int i = 0; i < dialogosFinal.Length; i++) 
			{
				dialogos [i].gameObject.SetActive (false);
			}
		} 
			
		nombres[0].gameObject.SetActive (false);
		nombres[1].gameObject.SetActive (false);
		nombres[2].gameObject.SetActive (false);
		nombres[3].gameObject.SetActive (false);
		nombres[4].gameObject.SetActive (false);
		//tiempoOri = tiempoLetra;
		//StartCoroutine (dictado (txt[i].text));
	}

	void Update()
	{
		//La parte de los dialogos
		if (k < dialogos.Length) {
			if (k > 0)	//Si hay un diálogo antes, desactivar
				dialogos [k - 1].gameObject.SetActive (false);
			
			dialogos [k].gameObject.SetActive (true);	//Activa el dialogo correspondiente

			if ((k % 2) == 0)
			{	//Esto sirve para cambiar los nombres activos, cuidado, los numeros impares corresponden al jugador y los pares a los otros
				nombres [0].gameObject.SetActive (true);
				if (nivelActual == 0)
					nombres [1].gameObject.SetActive (false);
				else if (nivelActual == 1)
					nombres [2].gameObject.SetActive (false);
				else if (nivelActual == 2) {
					if (k > 1)personajes [3].gameObject.SetActive (true);
					nombres [3].gameObject.SetActive (false);
				}
				else
					nombres [4].gameObject.SetActive (false);
			} 
			else 
			{
				nombres [0].gameObject.SetActive (false);
				if (nivelActual == 0) {
					nombres [1].gameObject.SetActive (true);
					if (k > 2)
						personajes [1].gameObject.SetActive (true);
				} else if (nivelActual == 1)
					nombres [2].gameObject.SetActive (true);
				else if (nivelActual == 2)
					nombres [3].gameObject.SetActive (true);
				else
					nombres [4].gameObject.SetActive (true);
			}

			textoTerminado = siguienteTexto (textoTerminado);
			if (textoTerminado) 
			{
				k++;
				textoTerminado = false;
			}
		} 
		else {	//La parte del diario
			tiposEscenas [0].gameObject.SetActive (false);
			tiposEscenas [1].gameObject.SetActive (true);
			if (nivelActual == 0)
				escenasDiario [0].gameObject.SetActive (true);
			else if (nivelActual == 1)
				escenasDiario [1].gameObject.SetActive (true);
			else if (nivelActual == 2)
				escenasDiario [2].gameObject.SetActive (true);
			else
				escenasDiario [3].gameObject.SetActive (true);

			textoTerminado = siguienteTexto (textoTerminado);
			if (textoTerminado) {
				cargando.gameObject.SetActive (true);
				if (nivelActual != 3) {
					GameManager.instance.aumentaNivel ();
					SceneManager.LoadScene ("MovBarco");
				}
				else
					SceneManager.LoadScene ("Créditos");
			}
		}
	}

	static bool siguienteTexto (bool siguiente)
	{
		if (Input.GetKeyDown(KeyCode.Space)) siguiente = true;
		return siguiente;
	}
}
