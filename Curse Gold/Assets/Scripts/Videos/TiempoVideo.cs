using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TiempoVideo : MonoBehaviour {

	public float tiempo = 0;
	public float finVideo = 0;
	public string siguienteEscena;
	// Use this for initialization
	void Start () 
	{
		tiempo = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{
		tiempo += Time.deltaTime;
		if (tiempo >= finVideo || Input.GetKeyDown(KeyCode.Space))
			SceneManager.LoadScene (siguienteEscena);		
	}
}
