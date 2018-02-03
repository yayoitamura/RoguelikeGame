using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon : MonoBehaviour {
	[SerializeField] GameObject wallPrefab;
	[SerializeField] GameObject floorPrefab;
	[SerializeField] GameObject stepsPrefab; // 追加：階段プレファブ
	[SerializeField] GameObject enemyPrefab;
	[SerializeField] Transform tileContainer;
	DungeonGenerator generator;

	[SerializeField] GameObject title;
	GameObject enemys;
	GameObject steps;

	// Use this for initialization
	void Start () {
		generator = GameObject.Find ("DungeonGenerator").GetComponent<DungeonGenerator> ();
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
					case 1:
						tile = Instantiate (floorPrefab);
						break;
					case 2:
						tile = Instantiate (floorPrefab);
						steps = Instantiate (stepsPrefab);
						steps.transform.SetParent (tileContainer);
						steps.transform.localPosition = new Vector2 (x, y);
						break;
					case 3:
						tile = Instantiate (floorPrefab);
						enemys = Instantiate (enemyPrefab);
						enemys.transform.SetParent (tileContainer);
						enemys.transform.localPosition = new Vector2 (x, y);
						break;
					default:
						tile = Instantiate (wallPrefab);
						break;

				}
				tile.transform.SetParent (tileContainer);
				tile.transform.localPosition = new Vector2 (x, y);
				// Debug.Log (tile);
			}
		}
	}
}