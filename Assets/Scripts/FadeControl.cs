using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeControl : MonoBehaviour {

	float speed = 0.05f; //透明化の速さ
	float alfa; //A値を操作するための変数
	float red, green, blue; //RGBを操作するための変数

	Image fade;
	public bool isFadeIn = false;
	public bool isFadeOut = false;

	void Start () {　　　　　 //Panelの色を取得
		fade
			= GetComponent<Image> ();
		red = fade.color.r;
		green = fade.color.g;
		blue = fade.color.b;
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
			fade.enabled = false; //d)パネルの表示をオフにする
		}
	}

	void FadeOut () {
		fade.enabled = true;
		alfa += speed;
		fade.color = new Color (red, green, blue, alfa);

		if (alfa >= 1) {
			isFadeOut = false;
		}
	}

}