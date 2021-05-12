using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoCocodrilo : MonoBehaviour {

	public Animator anim;

	public float velocidadY, max, min, tiempoDeEspera, tiempoQuieto;
	float velocidadOriginalY, tiempo, cocoY;

	// Use this for initialization
	void Start () 
	{
		velocidadOriginalY = velocidadY;
		cocoY = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () 
	{
		tiempo += Time.deltaTime; // Va sumando el tiempo a la variable tiempo
		{
			if (tiempo >= tiempoDeEspera) //Si el tiempo desde que se inicia el componente es mayor o igual que el tiempo de espera que hemos aginado, empieza a moverse
			{
				transform.Translate (new Vector3 (0, velocidadY * Time.deltaTime, 0));
				if (transform.position.y > cocoY + max) 
				{
					transform.position = new Vector3 (transform.position.x, cocoY + max, transform.position.z);
					anim.SetBool ("Abierto", true);
					Invoke ("CocodriloQuieto", 0); //Cuando llega al máximo, el prefab se queda quieto totalmente
				} 
				else if (transform.position.y < cocoY + min) 
				{
					transform.position = new Vector3 (transform.position.x, cocoY + min, transform.position.z);
					velocidadY = -velocidadY; //Cuando llega al mínimo, cambia su dirección hacia arriba
				}
			}
		}
	}

	void CocodriloQuieto()
	{
		velocidadY = 0; // Su velocidad se vuelve 0
		Invoke ("CocodriloBajando", tiempoQuieto); //Espera un segundo antes de bajar, para que quede perfecto con la animación, tiempoQuieto = 1.1 (2 bocados), 1.8 (3 bocados), 2.4(4 bocados)
	}

	void CocodriloBajando()
	{
		anim.SetBool ("Abierto", false);
		velocidadY = -velocidadOriginalY;
	}
}
