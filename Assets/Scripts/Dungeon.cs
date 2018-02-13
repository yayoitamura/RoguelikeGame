using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon : MonoBehaviour {
	DungeonGenerator generator;
	[SerializeField] GameObject wallPrefab;
	[SerializeField] GameObject floorPrefab;
	[SerializeField] GameObject stepsPrefab;
	[SerializeField] GameObject enemyPrefab;
	[SerializeField] GameObject playerPrefab;
	[SerializeField] Transform tileContainer;
	[SerializeField] GameObject title;

	GameObject steps;
	GameObject enemys;
	GameObject player;
	private enum MAP {
		WALL,
		FLOOR,
		STEPS,
		ENEMYS,
		PLAYER
	}
	// Use this for initialization
	void Start () {
		generator = GameObject.Find ("DungeonGenerator").GetComponent<DungeonGenerator> ();
		player = GameObject.Find ("Man");
	}

	// Update is called once per frame
	void Update () {

	}
	public void DungeonGenerate () {

		var map = generator.Generate ();
		// マップを元にオブジェクト生成
		for (var x = 0; x < generator.width; x++) {
			// 【修正】：↓を書き換え
			// var tile = map[x, y] == 1 ? Instantiate (floorPrefab) : Instantiate (wallPrefab);
			for (var y = 0; y < generator.height; y++) {
				GameObject tile = null;
				switch (map[x, y]) {
					case (int) MAP.WALL:
						tile = Instantiate (wallPrefab);
						break;
					case (int) MAP.FLOOR:
						tile = Instantiate (floorPrefab);
						break;
					case (int) MAP.STEPS:
						tile = Instantiate (floorPrefab);
						steps = Instantiate (stepsPrefab);
						steps.transform.SetParent (tileContainer);
						steps.transform.localPosition = new Vector2 (x, y);
						break;
					case (int) MAP.ENEMYS:
						tile = Instantiate (floorPrefab);
						enemys = Instantiate (enemyPrefab);
						enemys.transform.SetParent (tileContainer);
						enemys.transform.localPosition = new Vector2 (x, y);
						break;
					case (int) MAP.PLAYER:
						tile = Instantiate (floorPrefab);
						// player = Instantiate (playerPrefab);
						// player.transform.SetParent (tileContainer);
						// player.transform.localPosition = new Vector2 (x, y);
						player.transform.position = transform.TransformPoint (new Vector2 (x, y));
						break;
					default:
						break;

				}
				tile.transform.SetParent (tileContainer);
				tile.transform.localPosition = new Vector2 (x, y);
			}
		}
	}
}