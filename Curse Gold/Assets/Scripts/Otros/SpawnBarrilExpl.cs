using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBarrilExpl : MonoBehaviour {
	public GameObject barrilexpl;
	public GameObject tagw;
	public Transform spawnt;

	void Update(){
		tagw = GameObject.FindWithTag ("BarrilExpl");
		if (tagw == null)
			Instantiate (barrilexpl);
		barrilexpl.transform.position = spawnt.position;
	}

	void spawn()
	{
		tagw = GameObject.FindWithTag ("BarrilExpl");
		if (tagw == null)
			Instantiate (barrilexpl);
	}
}
