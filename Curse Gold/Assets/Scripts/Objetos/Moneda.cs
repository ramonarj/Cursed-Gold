using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moneda : MonoBehaviour {

	//VARIABLES PÚBLICAS
	public int Valor;


	//1.UPDATE
	void Update()
	{
		transform.Rotate(new Vector3(0f, Time.deltaTime*80f, 0f));
	}


}
