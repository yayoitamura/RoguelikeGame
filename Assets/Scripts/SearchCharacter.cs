using System.Collections;
using UnityEngine;

public class SearchCharacter : MonoBehaviour {

	bool isTrigger = false;
	GameObject enemy;

	void Start () {
		enemy = transform.parent.gameObject;
	}

	void OnTriggerStay2D (Collider2D other) {
		if (other.tag == "player") {
			isTrigger = true;
			enemy.GetComponent<Enemy> ().GetPlayerPosition (other.gameObject.transform.position, isTrigger);
		}
	}

	void OnTriggerExit2D (Collider2D other) {
		if (other.tag == "player") {
			enemy.GetComponent<Enemy> ().abortChase ();
			isTrigger = false;
		}
	}

}