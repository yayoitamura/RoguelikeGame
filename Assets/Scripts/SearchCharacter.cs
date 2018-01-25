using System.Collections;
using UnityEngine;

public class SearchCharacter : MonoBehaviour {
	GameObject target;
	GameObject enemy;

	Vector2 targetPsition;

	void Start () {
		target = GameObject.Find ("Man");
		enemy = transform.parent.gameObject;
	}

	void OnTriggerStay2D (Collider2D other) {
		if (other.tag == "player") {
			enemy.GetComponent<Enemy> ().ChasePlayer (other.gameObject); 
		}
	}

	private void OnTriggerExit2D (Collider2D other) {
		if (other.tag == "player") {
			enemy.GetComponent<Enemy> ().dontChase ();
		}
	}

}