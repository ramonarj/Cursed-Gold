using UnityEngine;
using System.Collections;

public class TargetCamara : MonoBehaviour {

	public GameObject Target;
	public float distX = 1.0f;
	public float distY = 1.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		SigueJugador ();
	}


	void SigueJugador()
	{
		transform.position = new Vector3 (Target.transform.position.x + distX, 
			Target.transform.position.y + distY, transform.position.z);
	}
}
