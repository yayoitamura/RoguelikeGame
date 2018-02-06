using System.Collections;
using UnityEngine;

public class SearchCharacter : MonoBehaviour {

	bool isTrigger = false;
	GameObject enemy;

	void Start () {
		enemy = transform.parent.gameObject;
	}

	void Update () {
		if (!isTrigger) {
			enemy.GetComponent<Enemy> ().StandBy (isTrigger);
		}
	}

	void OnTriggerStay2D (Collider2D other) {
		if (other.tag == "player") {
			Debug.Log ("player");
			enemy.GetComponent<Enemy> ().GetPlayerPosition (other.gameObject);
			isTrigger = true;
		}
	}

	private void OnTriggerExit2D (Collider2D other) {
		if (other.tag == "player") {
			enemy.GetComponent<Enemy> ().abortChase ();
			isTrigger = false;
		}
	}

}