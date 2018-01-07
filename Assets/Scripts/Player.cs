using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class Player : MonoBehaviour {

	[SerializeField] GameObject wallPrefab;
	[SerializeField] GameObject floorPrefab;
	[SerializeField] Transform tileContainer;

	DungeonGenerator generator;
	CameraControl cameraControl;

	Animator animator;
	const float STEP = 0.5f;
	Vector2 movePosition;

	void Start () {

		movePosition = transform.position;

		cameraControl = GameObject.Find ("Main Camera").GetComponent<CameraControl> ();
		generator = GameObject.Find ("DungeonGenerator").GetComponent<DungeonGenerator> ();
		animator = GetComponent<Animator> ();

		var map = generator.Generate ();
		// マップを元にオブジェクト生成
		for (var x = 0; x < generator.width; x++) {
			for (var y = 0; y < generator.height; y++) {
				var tile = map[x, y] == 1 ? Instantiate (floorPrefab) : Instantiate (wallPrefab);
				tile.transform.SetParent (tileContainer);
				tile.transform.localPosition = new Vector2 (x, y);
			}
		}
	}

	public void SetTargetPosition (string key) {

		switch (key) {
			case "left":
				// animator.SetTrigger ("left");
				movePosition.x -= STEP;
				Debug.Log ("state  left");
				break;
			case "up":
				// animator.SetTrigger ("up");
				movePosition.y += STEP;
				Debug.Log ("state  up");
				break;
			case "right":
				// animator.SetTrigger ("right");
				movePosition.x += STEP;
				Debug.Log ("state  right");
				break;
			case "down":
				// animator.SetTrigger ("down");
				movePosition.y -= STEP;
				Debug.Log ("state  down");
				break;
		}

		SetAnimationParam (key);
		Move ();
	}

	void SetAnimationParam (string key) {

		if (key == "left") {
			animator.Play ("chaLeft");
			return;
		}

		if (key == "right") {
			animator.Play ("chaRight");
			return;
		}

		if (key == "down") {
			animator.Play ("chaDown");
			return;
		}

		if (key == "up") {
			animator.Play ("chaUp");
			return;
		}
	}

	void Move () {

		cameraControl.Move ();

		Ray ray = new Ray (movePosition, transform.forward);
		RaycastHit2D hit = Physics2D.Raycast ((Vector2) ray.origin, (Vector2) ray.direction, 10);

		if (hit.collider) {
			movePosition = transform.position;
			CantMove (hit);
		} else {
			transform.position = movePosition;
		}
	}

	void CantMove (RaycastHit2D hit) {

		if (hit.collider.gameObject.name == "Enemy") { }

		if (hit.collider.gameObject.tag == "wall") { }

	}

	void OnCollisionEnter2D (Collision2D other) {

		Debug.Log ("On Collision == " + other.gameObject.tag);
		movePosition = transform.position;
	}

}