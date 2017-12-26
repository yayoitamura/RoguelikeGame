using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	Animator animator;
	Vector2 position;
	public Vector2 SPEED = new Vector2 (0.05f, 0.05f);

	public enum InputKey {
		Left, // 左
		Up, // 上
		Right, // 右
		Down, // 下

		Empty,
		};
		public InputKey key = InputKey.Empty;

		void Start () {
		animator = GetComponent<Animator> ();
		position = transform.position;
	}

	void Update () {

		if (Input.GetKey ("left")) {
			key = InputKey.Left;

		} else if (Input.GetKey ("up")) {
			key = InputKey.Up;

		} else if (Input.GetKey ("right")) {
			key = InputKey.Right;

		} else if (Input.GetKey ("down")) {
			key = InputKey.Down;
		}
		transform.position = position;

		// Debug.Log ("key  ======  " + key);

		switch (key) {
			case InputKey.Left:
				// animator.SetTrigger ("left");
				Debug.Log ("state  left");
				position.x -= SPEED.x;
				break;
			case InputKey.Up:
				// animator.SetTrigger ("up");
				Debug.Log ("state  up");
				position.y += SPEED.y;
				break;
			case InputKey.Right:
				// animator.SetTrigger ("right");
				Debug.Log ("state  right");
				position.x += SPEED.x;
				break;
			case InputKey.Down:
				// animator.SetTrigger ("down");
				Debug.Log ("state  down");
				position.y -= SPEED.y;
				break;
		}

	}
}