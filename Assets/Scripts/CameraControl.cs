using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Completed {

	public class CameraControl : MonoBehaviour { //-1 -2.5,4 5.5
		const float MOVE_MAX_X = 4.0f;
		const float MOVE_MAX_Y = 5.5f;
		const float MOVE_MIN_X = -1.0f;
		const float MOVE_MIN_Y = -2.5f;
		public GameObject player;

		void Start () {

		}

		void Update () {
			float cameraPositionX = Mathf.Clamp (player.transform.position.x, MOVE_MIN_X, MOVE_MAX_X);
			float cameraPositionY = Mathf.Clamp (player.transform.position.y, MOVE_MIN_Y, MOVE_MAX_Y);
			float z = -10f;
			transform.position = new Vector3 (cameraPositionX, cameraPositionY, z);
		}

	}
}