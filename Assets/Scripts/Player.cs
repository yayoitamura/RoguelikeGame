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

	float step = 5f;
	Vector3 target;
	Animator animator;

	void Start () {
		target = transform.position;

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

	void Update () {

		if (transform.position.x == target.x || transform.position.y == target.y) {
			SetTargetPosition ();
		}
		Move ();
	}

	void SetTargetPosition () {
		if (Input.GetMouseButtonUp (0)) {
			target = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			SetAnimationParam (target);
			return;
		}

	}

	// WalkParam  0;下移動　1;右移動　2:左移動　3:上移動
	void SetAnimationParam (Vector3 position) {

		bool upDown = Mathf.Abs (target.x - transform.position.x) > Mathf.Abs (target.y - transform.position.y);

		if (target.x - transform.position.x <= 0 && upDown) {
			animator.Play ("chaLeft");
			return;
		}

		if (target.x - transform.position.x >= 0 && upDown) {
			animator.Play ("chaRight");
			return;
		}

		if (target.y - transform.position.y <= 0 && !upDown) {
			animator.Play ("chaDown");
			return;
		}

		if (target.y - transform.position.y >= 0 && !upDown) {
			animator.Play ("chaUp");
			return;
		}
	}

	void Move () {
		cameraControl.Move ();

		Ray ray = new Ray (target, transform.forward);
		RaycastHit2D hit = Physics2D.Raycast ((Vector2) ray.origin, (Vector2) ray.direction, 10);

		if (hit.collider) {
			target = transform.position;
			CantMove (hit);

		}

		transform.position = Vector2.MoveTowards (transform.position, new Vector2 (target.x, target.y), step * Time.deltaTime);
	}

	void CantMove (RaycastHit2D hit) {
		if (hit.collider.gameObject.name == "Enemy") {
			Debug.Log ("Enemy hit!!!!!!!!!!!!!!!!!!!!!");
		}
		if (hit.collider.gameObject.name == "crate") {
			Debug.Log ("crate hit!!!!!!!!!!!!!!!!!!!!!");
		}

	}

	void OnCollisionEnter2D (Collision2D other) {
		// Debug.Log ("On Collision == " + other.gameObject.tag);
	}

}