using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	[SerializeField] GameObject wallPrefab;
	[SerializeField] GameObject floorPrefab;
	[SerializeField] GameObject stepsPrefab; // 追加：階段プレファブ
	[SerializeField] GameObject playerPrefab;
	[SerializeField] Transform tileContainer;
	DungeonGenerator generator;
	static int stage;
	Text stageText;

	void Start () {
		generator = GameObject.Find ("DungeonGenerator").GetComponent<DungeonGenerator> ();
		stageText = GameObject.Find ("Stage").GetComponent<Text> ();
		stageText.text = "stage : " + stage;

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
					case 3:
						tile = Instantiate (playerPrefab);
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
		stage++;
		SceneManager.LoadScene (index);
	}
}