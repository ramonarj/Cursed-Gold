using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogosEscenas : MonoBehaviour {

	public GameObject dialogos;
	public GameObject[] textos = new GameObject[5];
	public GameObject fondo;
	public GameObject nombre;
	public GameObject gameManager;
	public string escena;
	bool finDialogo;
	bool textoTerminado;
	int k = 0;
	public AudioClip cañonazo;
	private AudioSource sonido;

	// Use this for initialization
	void Start () {
		sonido = GetComponent<AudioSource> ();
		finDialogo = false;
		GameManager.instance.desactivaComp(finDialogo);
		gameManager.GetComponent<GameManager>() .enabled = false;

		if (escena != "Tutorial") {
			fondo.gameObject.SetActive (false);
		}

		for (int i = 0; i < textos.Length; i++) {
			textos [i].gameObject.SetActive (false);
		}

		nombre.gameObject.SetActive (false);
	}

	// Update is called once per frame
	void Update () {
		if (k < textos.Length) {
			textos [k].gameObject.SetActive (true);
			if (k > 0) {
				textos [k - 1].gameObject.SetActive (false);
			}
			if (escena == "Tutorial") {
				if (k != 1)
					nombre.gameObject.SetActive (true);
				else
					nombre.gameObject.SetActive (false);
				if (k > 1)
					fondo.gameObject.SetActive (false);
			} else 
			{
				nombre.gameObject.SetActive (true);

				if (k == textos.Length - 1) {
					nombre.gameObject.SetActive (false);
				}
			}

			textoTerminado = siguienteTexto (textoTerminado);
			if (textoTerminado) 
			{
				k++;
				if (escena == "Tutorial") {
					if (k == 1)
						efectoSonido (sonido, cañonazo);
				}
				textoTerminado = false;
			}
		}
		else 
		{
			dialogos.gameObject.SetActive (false);
			finDialogo = true;
			gameManager.GetComponent<GameManager>() .enabled = true;
			GameManager.instance.desactivaComp (finDialogo);
		}
	}

	public static void efectoSonido(AudioSource sonido, AudioClip cañonazo)
	{
		sonido.volume = 0.5f;
		sonido.PlayOneShot (cañonazo);
	}

	static bool siguienteTexto (bool siguiente)
	{
		if (Input.GetKeyDown(KeyCode.Space)) siguiente = true;
		return siguiente;
	}
}

