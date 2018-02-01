using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeControl : MonoBehaviour {

	float speed = 0.05f;
	float alfa;
	float red, green, blue;

	Image fade;
	public bool isFadeIn = false;
	public bool isFadeOut = false;

	void Awake () {
		fade = GetComponent<Image> ();
		red = fade.color.r;
		green = fade.color.g;
		blue = fade.color.b;
		alfa = fade.color.a;
	}

	void Update () {
		if (isFadeIn) {
			FadeIn ();
		}

		if (isFadeOut) {
			FadeOut ();
		}
	}

	void FadeIn () {
		alfa -= speed;
		fade.color = new Color (red, green, blue, alfa);
		if (alfa <= 0) {
			isFadeIn = false;
			gameObject.SetActive (false);
		}
	}

	// public bool FadeIn () {
	// 	alfa -= speed;
	// 	fade.color = new Color (red, green, blue, alfa);
	// 	if (alfa <= 0) {
	// 		isFadeIn = false;
	// 		gameObject.SetActive (false);
	// 	}
	// 	return isFadeIn;
	// }

	void FadeOut () {
		fade.enabled = true;
		alfa += speed;
		fade.color = new Color (red, green, blue, alfa);

		if (alfa >= 1) {
			isFadeOut = false;
		}
	}

}