using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	Vector2 position;
	private const int STEP = 1;

	public enum InputKey {
		Left, // 左
		Up, // 上
		Right, // 右
		Down, // 下

		Empty,
		};
		public InputKey key = InputKey.Empty;

		void Start () {
		position = transform.position;
	}

	void Update () {

		if (Input.GetKeyDown ("left")) {
			key = InputKey.Left;
			position.x -= STEP;

		} else if (Input.GetKeyDown ("up")) {
			key = InputKey.Up;
			position.y += STEP;

		} else if (Input.GetKeyDown ("right")) {
			key = InputKey.Right;
			position.x += STEP;

		} else if (Input.GetKeyDown ("down")) {
			key = InputKey.Down;
			position.y -= STEP;
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

	void OnCollisionEnter2D (Collision2D other) {
		Debug.Log (other);
	}

}