using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	Animator animator;

	public enum InputKey {
		Left, // 左
		Up, // 上
		Right, // 右
		Down, // 下
		};
		public InputKey key = InputKey.Down;

		void Start () {
		animator = GetComponent<Animator> ();
	}

	void Update () {
		if (Input.GetKey (KeyCode.LeftArrow)) {
			key = InputKey.Left;
		}
		if (Input.GetKey (KeyCode.UpArrow)) {
			key = InputKey.Up;
		}
		if (Input.GetKey (KeyCode.RightArrow)) {
			key = InputKey.Right;
		}
		if (Input.GetKey (KeyCode.DownArrow)) {
			key = InputKey.Down;
		}

		switch (key) {
			case InputKey.Left:
				animator.SetTrigger ("left");
				break;
			case InputKey.Up:
				animator.SetTrigger ("up");
				break;
			case InputKey.Right:
				animator.SetTrigger ("right");
				break;
			case InputKey.Down:
				animator.SetTrigger ("down");
				break;
		}

	}
}