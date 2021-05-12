using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimapa : MonoBehaviour {

	public Transform[] islas = new Transform[4];
	int nivel;
	Vector3 posIsla, posBarco;

	// Use this for initialization
	void Start () 
	{
		nivel = GameManager.instance.NivelActual ();
		//nivel = 1;
		posIsla = islas[nivel-1].position;
	}
	
	// Update is called once per frame
	void Update ()
	{
		posBarco = islas [3].position;
		Gira (posIsla, posBarco);
	}

	void Gira(Vector3 posIsla, Vector3 posbarco)
	{
		float disX = posIsla.x - posBarco.x;
		float disY = posIsla.y - posBarco.y;
		float angulo = (Mathf.Atan(disY / disX) * (180 / Mathf.PI));
		if(disX>0)
			transform.rotation = Quaternion.AngleAxis(angulo-90, new Vector3(0f, 0f, 1f));
		else
			transform.rotation = Quaternion.AngleAxis(angulo+90, new Vector3(0f, 0f, 1f));
	}

}
