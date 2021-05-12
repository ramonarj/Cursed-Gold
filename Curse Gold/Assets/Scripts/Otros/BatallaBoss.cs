using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatallaBoss : MonoBehaviour {	
	void OnTriggerExit2D(Collider2D other)
	{
		Collider2D iniciobat = GetComponent<BoxCollider2D>();
		MovJugador prueba = other.GetComponent<MovJugador> ();
		Pajaro mueve = GetComponent<Pajaro> ();
		Boss2 Ataca = GetComponent<Boss2> ();
		if (prueba != null) 
		{
			Ataca.enabled = true;
			iniciobat.isTrigger = false;
			mueve.enabled = true;
		}
		}
}
