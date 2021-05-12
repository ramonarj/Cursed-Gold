
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pinchos : MonoBehaviour {

	//VARIABLES VISIBLES AL EDITOR
	public float tiempo=3;
	public Transform posicion;
	public float velpinchos, posIntermedia, posArriba, posAbajo, espaciosubidaintermedia=0.15f, espaciosubidaarriba=0.3f;
	//VARIABLES NO VISIBLES
	float velocidador, activar;
	BoxCollider2D coll;
	bool sale = true, bajas = true;

	//1.START
	void Start ()
	{
		velocidador = velpinchos;
		coll = GetComponent<BoxCollider2D> ();
		coll.enabled = false;
		posAbajo = posicion.position.y;
		posArriba= posicion.position.y+espaciosubidaarriba;
		posIntermedia=posicion.position.y+espaciosubidaintermedia;
	}
	
	//2.UPDATE
	void Update () 
	{
		activar += Time.deltaTime;
		if (activar >= tiempo)
		{
			transform.Translate (new Vector3 (0, velpinchos, 0));

			if((transform.position.y >= posIntermedia) && sale)
			{
				velpinchos = 0;
				transform.position = new Vector3 (transform.position.x,posIntermedia, transform.position.z);
				Invoke ("avisa", 2);
				sale = false;
			}

			else if((transform.position.y >= posArriba)&& bajas)
			{
				coll.enabled = true;
				velpinchos = 0;
				transform.position = new Vector3 (transform.position.x,posArriba, transform.position.z);
				Invoke ("baja", 2); 
				bajas = false;
			}

			else if(transform.position.y <= posAbajo) 
			{
				bajas = true;
				sale = true;
				velpinchos = velocidador;
				activar = 0;
			}
		}
	}

	//3.AVISA
	void avisa()
	{
		velpinchos = velocidador;
		transform.Translate (new Vector3 (0f, velpinchos, 0f));
	}

	//4.BAJA
	void baja()
	{
		velpinchos = -velocidador;
		coll.enabled = false;
	}
}
