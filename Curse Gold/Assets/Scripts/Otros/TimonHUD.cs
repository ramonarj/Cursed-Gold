using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimonHUD : MonoBehaviour {

	public float velRotacion;
	// Update is called once per frame
	void Update () {
		transform.Rotate (new Vector3 (0, 0, (velRotacion * -1) * Time.deltaTime));
	}
}
