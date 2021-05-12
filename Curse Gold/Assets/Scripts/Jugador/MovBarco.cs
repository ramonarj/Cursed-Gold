using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovBarco : MonoBehaviour {

	public float Velocidad = 10f;
	public float Rotacion = 3f;
	public GameObject Bala;
	public GameObject[] cañones = new GameObject[6];
	public float DelayDisparo = 3;
	private float TiempoDisparo = 3;
	public GameObject timon;
	public AudioClip cancion1, cancion2, cancion3;
	private bool sonando;
	AudioSource sonido;
	public GameObject [] hudCanciones = new GameObject[2];
	public Text nombreCancion;
	// Use this for initialization
	void Start ()
	{
		hudCanciones [1].gameObject.SetActive (false);
		hudCanciones [0].gameObject.SetActive (true);

		int nivel = GameManager.instance.NivelActual();
		Colocate(nivel);
		sonido = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		MueveBarco ();

		TiempoDisparo += Time.deltaTime;
		if (Input.GetKeyDown (KeyCode.Space))
		{
			if (TiempoDisparo >= DelayDisparo) 
			{
				dispara ();
				TiempoDisparo = 0;
			}
		}
	}
	void MueveBarco()
	{
		if (Input.GetKey (KeyCode.W))  
			transform.Translate (new Vector3 (0.0f, -Velocidad*Time.deltaTime, 0.0f));

		if (Input.GetKey(KeyCode.D))
		{
			transform.Rotate(new Vector3(0.0f, 0.0f, -Rotacion*Time.deltaTime));
			timon.transform.Rotate(new Vector3(0.0f, 0.0f, -Rotacion*Time.deltaTime));
		}
		else if (Input.GetKey(KeyCode.A))
		{
			transform.Rotate(new Vector3(0.0f, 0.0f, Rotacion*Time.deltaTime));
			timon.transform.Rotate(new Vector3(0.0f, 0.0f, Rotacion*Time.deltaTime));
		}

		if (Input.GetKeyDown (KeyCode.C))
		{
			if (sonando == false) 
			{
				nombreCancion.gameObject.SetActive (true);
				hudCanciones [0].gameObject.SetActive (false);
				hudCanciones [1].gameObject.SetActive (true);
				int cancion = Random.Range (1, 4);
				if (cancion == 1) {
					nombreCancion.text = "Eliza Lee";
					sonido.volume = 0.5f;
					GetComponent<AudioSource> ().PlayOneShot (cancion1);
				}
				else if (cancion == 2) {
					GetComponent<AudioSource> ().PlayOneShot (cancion2);
					nombreCancion.text = "Leave her johnny";
				} 
				else if (cancion == 3) {
					GetComponent<AudioSource> ().PlayOneShot (cancion3);
					nombreCancion.text = "Where am I to go M'Jonnies";
				}
				sonando = true;
			} 
			else 
			{
				nombreCancion.gameObject.SetActive (false);
				hudCanciones [1].gameObject.SetActive (false);
				hudCanciones [0].gameObject.SetActive (true);
				GetComponent<AudioSource> ().Stop ();
				sonando = false;
				sonido.volume = 1f;
			}
		}
	}

	void dispara()
	{
		for (int i = 0; i < 6; i++) 
		{
			GameObject bala = Instantiate (Bala);
			bala.transform.rotation = cañones [i].transform.rotation;
			bala.transform.position = cañones [i].transform.position;
		}
	}

	void OnTriggerEnter2D(Collider2D Other)
	{
	GameManager.instance.ActivaPanelIslas (Other);
	}

	void Colocate(int nivel) 
	{
		switch (nivel)
		{
			case 1:
				transform.position = new Vector3(-43f, 53f, 0f);
				break;
			case 2:
				transform.position = new Vector3(-11f, 66f, 0f);
				transform.rotation = Quaternion.AngleAxis(310f, new Vector3(0f, 0f, 1f));
				break;
			case 3:
				transform.position = new Vector3(-38.5f, 10f, 0f);
				transform.rotation = Quaternion.AngleAxis(180f, new Vector3(0f, 0f, 1f));
				break;
		}
	}
}
