using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	private float speed = 5.0f;
	Vector2 mousePosition;
	public DungeonGenerator generator;
	[SerializeField]
	GameObject wallPrefab;
	[SerializeField]
	GameObject floorPrefab;
	[SerializeField]
	Transform tileContainer;

	CameraControl cameraControl;

	void Start () {

		cameraControl = GameObject.Find ("Main Camera").GetComponent<CameraControl> ();

		var map = generator.Generate ();

		// マップを元にオブジェクト生成
		for (var x = 0; x < generator.width; x++) {
			for (var y = 0; y < generator.height; y++) {
				Debug.Log ("-----------");
				var tile = map[x, y] == 1 ? Instantiate (floorPrefab) : Instantiate (wallPrefab);
				tile.transform.SetParent (tileContainer);
				tile.transform.localPosition = new Vector2 (x, y);
			}
		}
	}

	void Update () {
		Move ();
	}
	

	void Move () {
		if (Input.GetMouseButton (0)) {
			mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			float step = speed * Time.deltaTime;
			cameraControl.Move ();

			Ray ray = new Ray (mousePosition, transform.forward);
			RaycastHit2D hit = Physics2D.Raycast ((Vector2) ray.origin, (Vector2) ray.direction, 10);

			if (hit.collider) {
				mousePosition = transform.position;
				CantMove (hit);

			}

			transform.position = Vector2.MoveTowards (transform.position, new Vector2 (mousePosition.x, mousePosition.y), step);
		}
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
		Debug.Log ("On Collision == " + other.gameObject.tag);
	}

}