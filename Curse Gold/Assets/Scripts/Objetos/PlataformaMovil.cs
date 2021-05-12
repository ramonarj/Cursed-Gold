using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaMovil : MonoBehaviour {
	public float velX, velY, max, min; // Velocidad maxima 7 para que no se buguee
	float plaX, plaY;
	Rigidbody2D Rigid;

	void Start ()
	{
		Rigid = GetComponent<Rigidbody2D> ();

		plaX = transform.position.x;
		plaY = transform.position.y;
	}
	void Update () 
	{
		Rigid.velocity = (new Vector3 (velX , velY , 0));
		if (velY == 0) 
		{
			if (transform.position.x >= plaX + max) 
			{
				transform.position = new Vector3 (plaX + max -0.2f, transform.position.y, transform.position.z);
				velX = -velX;

			} 

			else if (transform.position.x <= plaX + min) 
			{
				transform.position = new Vector3 (plaX + min + 0.2f, transform.position.y, transform.position.z);
				velX = -velX;
			}
		}

		else if (velX == 0) 
		{
			if (transform.position.y >= plaY + max) 
			{
				transform.position = new Vector3 (transform.position.x, plaY + max - 0.1f, transform.position.z);
				velY = -velY;
			} 

			else if (transform.position.y <= plaY + min) 
			{
				transform.position = new Vector3 (transform.position.x, plaY + min + 0.1f, transform.position.z);
				velY = -velY;
			}
		}
	}
}
