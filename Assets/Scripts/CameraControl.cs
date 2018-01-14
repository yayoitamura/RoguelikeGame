using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {
	const float MOVE_MAX_X = 2.5f;
	const float MOVE_MAX_Y = 4.0f;
	const float MOVE_MIN_X = -2.5f;
	const float MOVE_MIN_Y = -4.0f;
	public GameObject player;
	float z = -10f;

	private void Awake () {
		transform.position = new Vector3 (player.transform.position.x, player.transform.position.y, z);
	}

	public void Move () {
		float cameraPositionX = Mathf.Clamp (player.transform.position.x, MOVE_MIN_X, MOVE_MAX_X);
		float cameraPositionY = Mathf.Clamp (player.transform.position.y, MOVE_MIN_Y, MOVE_MAX_Y);

		// transform.position = new Vector3 (cameraPositionX, cameraPositionY, z);
		// transform.position = Vector2.MoveTowards (transform.position, new Vector2 (target.x, target.y), step * Time.deltaTime);
		transform.position = Vector3.Lerp (transform.position, new Vector3 (cameraPositionX, cameraPositionY, z), Time.deltaTime * 10f);

	}

}