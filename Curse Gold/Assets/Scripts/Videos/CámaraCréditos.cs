using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CámaraCréditos : MonoBehaviour {

	float tiempo = 0f;
	public float tiempoMax = 0f;
	public GameObject gracias;

	//Start
	void Start ()
	{
		gracias.gameObject.SetActive (false);
	}
	
	//Update
	void Update () 
	{
		tiempo += Time.deltaTime;
		if (tiempo < tiempoMax) 
		{
			transform.Translate (new Vector3 (0f, Time.deltaTime * 70, 0.0f));
		} 
		else
		{
			transform.position = new Vector3 (transform.position.x, 5000f, transform.position.z);
			gracias.gameObject.SetActive (true);
			if (tiempo > 55) SceneManager.LoadScene ("Menu");
		}
	}
}
