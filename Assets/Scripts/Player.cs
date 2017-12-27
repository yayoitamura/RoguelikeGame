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
			animator.SetTrigger ("left");
			key = InputKey.Left;
			position.x -= SPEED.x;

		} else if (Input.GetKey ("up")) {
			animator.SetTrigger ("up");
			key = InputKey.Up;
			position.y += SPEED.y;

		} else if (Input.GetKey ("right")) {
			animator.SetTrigger ("right");
			key = InputKey.Right;
			position.x += SPEED.x;

		} else if (Input.GetKey ("down")) {
			animator.SetTrigger ("down");
			key = InputKey.Down;
			position.y -= SPEED.y;
		}

		transform.position = position;

		switch (key) {
			case InputKey.Left:
				// animator.SetTrigger ("left");
				Debug.Log ("state  left");

				break;
			case InputKey.Up:
				// animator.SetTrigger ("up");
				Debug.Log ("state  up");

				break;
			case InputKey.Right:
				// animator.SetTrigger ("right");
				Debug.Log ("state  right");

				break;
			case InputKey.Down:
				// animator.SetTrigger ("down");
				Debug.Log ("state  down");

				break;
		}
		Debug.Log ("key  ======  " + key);

	}
}