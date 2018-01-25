using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeControl : MonoBehaviour {

	public float speed = 0.03f; //透明化の速さ
	float alfa = 1; //A値を操作するための変数
	float red, green, blue; //RGBを操作するための変数

	void Start () {　　　　　 //Panelの色を取得
		red = GetComponent<Image> ().color.r;
		green = GetComponent<Image> ().color.g;
		blue = GetComponent<Image> ().color.b;
	}

	void Update () {
		GetComponent<Image> ().color = new Color (red, green, blue, alfa);
		alfa -= speed;
	}

}