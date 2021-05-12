using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Transparenta : MonoBehaviour {

	public GameObject[] fondos = new GameObject[4];
	public float tiempo = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		tiempo += Time.deltaTime;
		//Difuminar primera imagen
		if (tiempo > 10.25f) {
			fondos [0].GetComponent<Image> ().color = new Color32 (255, 255, 255, 225);
		}
		if (tiempo > 10.5f) {
			fondos [0].GetComponent<Image> ().color = new Color32 (255, 255, 255, 200);
		}
		if (tiempo > 10.75f) {
			fondos [0].GetComponent<Image> ().color = new Color32 (255, 255, 255, 175);
		}
		if (tiempo > 11f) {
			fondos [0].GetComponent<Image> ().color = new Color32 (255, 255, 255, 150);
		}
		if (tiempo > 11.25f) {
			fondos [0].GetComponent<Image> ().color = new Color32 (255, 255, 255, 125);
		}
		if (tiempo > 11.5f) {
			fondos [0].GetComponent<Image> ().color = new Color32 (255, 255, 255, 100);
		}
		if (tiempo > 11.75f) {
			fondos [0].GetComponent<Image> ().color = new Color32 (255, 255, 255, 75);
		}
		if (tiempo > 12f) {
			fondos [0].GetComponent<Image> ().color = new Color32 (255, 255, 255, 50);
		}
		if (tiempo > 12.25f) {
			fondos [0].GetComponent<Image> ().color = new Color32 (255, 255, 255, 25);
		}
		if (tiempo > 12.5f) {
			fondos [0].GetComponent<Image> ().color = new Color32 (255, 255, 255, 0);
		}


		//Difuminar segunda imagen
		if (tiempo > 22.75f) {
			fondos [1].GetComponent<Image> ().color = new Color32 (255, 255, 255, 225);
		}
		if (tiempo > 23f) {
			fondos [1].GetComponent<Image> ().color = new Color32 (255, 255, 255, 200);
		}
		if (tiempo > 23.25f) {
			fondos [1].GetComponent<Image> ().color = new Color32 (255, 255, 255, 175);
		}
		if (tiempo > 23.5f) {
			fondos [1].GetComponent<Image> ().color = new Color32 (255, 255, 255, 150);
		}
		if (tiempo > 23.75f) {
			fondos [1].GetComponent<Image> ().color = new Color32 (255, 255, 255, 125);
		}
		if (tiempo > 24f) {
			fondos [1].GetComponent<Image> ().color = new Color32 (255, 255, 255, 100);
		}
		if (tiempo > 24.25f) {
			fondos [1].GetComponent<Image> ().color = new Color32 (255, 255, 255, 75);
		}
		if (tiempo > 24.5f) {
			fondos [1].GetComponent<Image> ().color = new Color32 (255, 255, 255, 50);
		}
		if (tiempo > 24.75f) {
			fondos [1].GetComponent<Image> ().color = new Color32 (255, 255, 255, 25);
		}
		if (tiempo > 25f) {
			fondos [1].GetComponent<Image> ().color = new Color32 (255, 255, 255, 0);
		}


		//Difuminar tercera imagen
		if (tiempo > 34.25f) {
			fondos [2].GetComponent<Image> ().color = new Color32 (255, 255, 255, 225);
		}
		if (tiempo > 34.5f) {
			fondos [2].GetComponent<Image> ().color = new Color32 (255, 255, 255, 200);
		}
		if (tiempo > 34.75f) {
			fondos [2].GetComponent<Image> ().color = new Color32 (255, 255, 255, 175);
		}
		if (tiempo > 35f) {
			fondos [2].GetComponent<Image> ().color = new Color32 (255, 255, 255, 150);
		}
		if (tiempo > 35.25f) {
			fondos [2].GetComponent<Image> ().color = new Color32 (255, 255, 255, 125);
		}
		if (tiempo > 35.5f) {
			fondos [2].GetComponent<Image> ().color = new Color32 (255, 255, 255, 100);
		}
		if (tiempo > 35.75f) {
			fondos [2].GetComponent<Image> ().color = new Color32 (255, 255, 255, 75);
		}
		if (tiempo > 36f) {
			fondos [2].GetComponent<Image> ().color = new Color32 (255, 255, 255, 50);
		}
		if (tiempo > 36.25f) {
			fondos [2].GetComponent<Image> ().color = new Color32 (255, 255, 255, 25);
		}
		if (tiempo > 36.5f) {
			fondos [2].GetComponent<Image> ().color = new Color32 (255, 255, 255, 0);
		}


		//Difuminar cuarta imagen
		if (tiempo > 46.75f) {
			fondos [3].GetComponent<Image> ().color = new Color32 (255, 255, 255, 225);
		}
		if (tiempo > 47f) {
			fondos [3].GetComponent<Image> ().color = new Color32 (255, 255, 255, 200);
		}
		if (tiempo > 47.25f) {
			fondos [3].GetComponent<Image> ().color = new Color32 (255, 255, 255, 175);
		}
		if (tiempo > 47.5f) {
			fondos [3].GetComponent<Image> ().color = new Color32 (255, 255, 255, 150);
		}
		if (tiempo > 47.75f) {
			fondos [3].GetComponent<Image> ().color = new Color32 (255, 255, 255, 125);
		}
		if (tiempo > 48f) {
			fondos [3].GetComponent<Image> ().color = new Color32 (255, 255, 255, 100);
		}
		if (tiempo > 48.25f) {
			fondos [3].GetComponent<Image> ().color = new Color32 (255, 255, 255, 75);
		}
		if (tiempo > 48.5f) {
			fondos [3].GetComponent<Image> ().color = new Color32 (255, 255, 255, 50);
		}
		if (tiempo > 48.75f) {
			fondos [3].GetComponent<Image> ().color = new Color32 (255, 255, 255, 25);
		}
		if (tiempo > 49f) {
			fondos [3].GetComponent<Image> ().color = new Color32 (255, 255, 255, 0);
		}
	}
}
