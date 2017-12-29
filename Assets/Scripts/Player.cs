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
			position.x -= SPEED.x;

		} else if (Input.GetKey ("up")) {
			key = InputKey.Up;
			position.y += SPEED.y;

		} else if (Input.GetKey ("right")) {
			key = InputKey.Right;
			position.x += SPEED.x;

		} else if (Input.GetKey ("down")) {
			key = InputKey.Down;
			position.y -= SPEED.y;
		}

		transform.position = position;

		switch (key) {
			case InputKey.Left:

				break;
			case InputKey.Up:

				break;
			case InputKey.Right:

				break;
			case InputKey.Down:

				break;
		}

	}
}