using System.Collections;
using UnityEngine;

public class SearchCharacter : MonoBehaviour {
	GameObject target;
	GameObject enemy;

	void Start () {
		target = GameObject.Find ("Man");
		enemy = GameObject.Find ("Enemy");
	}

	// void OnTriggerStay2D (Collider2D other) {
	// 	if (other.tag == "player") {
	// 		Vector2 targetPosition = target.transform.position;
	// 		Debug.Log (targetPosition);
	// 		enemy.transform.position = targetPosition;

	// 	}
	// }

	private void Update () {
		Move ();
	}

	void Move () {
		Vector2 Predetor = target.transform.position;

		float x = Predetor.x;
		float y = Predetor.y;
		//追跡方向の決定
		Vector2 direction = new Vector2 (x - enemy.transform.position.x, y - enemy.transform.position.y).normalized;
		//ターゲット方向に力を加える
		enemy.GetComponent<Rigidbody2D> ().velocity = (direction * 4);

	}

}