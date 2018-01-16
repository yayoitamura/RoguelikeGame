using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	[SerializeField] GameObject wallPrefab;
	[SerializeField] GameObject floorPrefab;
	[SerializeField] GameObject stepsPrefab; // 追加：階段プレファブ
	[SerializeField] Transform tileContainer;
	DungeonGenerator generator;

	void Start () {
		generator = GameObject.Find ("DungeonGenerator").GetComponent<DungeonGenerator> ();

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
						tile = Instantiate (stepsPrefab);
						break;
					default:
						tile = Instantiate (wallPrefab);
						break;

				}
				tile.transform.SetParent (tileContainer);
				tile.transform.localPosition = new Vector2 (x, y);
			}
		}

	}

	void Update () { }

	public void LoadScenes (int index) {
		SceneManager.LoadScene (index);
	}
}