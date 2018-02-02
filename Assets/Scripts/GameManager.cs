using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	[SerializeField] GameObject wallPrefab;
	[SerializeField] GameObject floorPrefab;
	[SerializeField] GameObject stepsPrefab; // 追加：階段プレファブ
	[SerializeField] GameObject enemyPrefab;
	[SerializeField] Transform tileContainer;
	DungeonGenerator generator;
	GameObject fade;
	GameObject enemys;
	GameObject steps;

	static int stage;
	Text stageText;

	public static GameManager instance;
	void Awake () {
		if (instance == null) {

			instance = this;
			DontDestroyOnLoad (gameObject);
		} else {

			Destroy (gameObject);
		}

	}

	void Start () {

		fade = GameObject.Find ("Fade");

		generator = GameObject.Find ("DungeonGenerator").GetComponent<DungeonGenerator> ();
		stageText = GameObject.Find ("Stage").GetComponent<Text> ();

		stageText.text = "stage : " + stage;

		SetFade ();
		DungeonGenerate ();
	}

	void Update () {

	}

	public void PushPlayButton () {
		GameObject.Find ("Title").SetActive (false);
	}

	void SetFade () {
		fade.GetComponent<FadeControl> ().isFadeIn = true;
	}

	void DungeonGenerate () {

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
			}
		}
	}
	public void NextStage () {

		fade.SetActive (true);
		fade.GetComponent<FadeControl> ().isFadeOut = true;

		const int nextStage = 0;
		stage++;

		LoadScene (nextStage);

	}

	void LoadScene (int sceneIndex) {
		SceneManager.LoadScene (sceneIndex);
	}

	// public void LoadScene () {
	// 	fade.SetActive (true);
	// 	fade.GetComponent<FadeControl> ().isFadeOut = true;

	// 	const int nextStage = 0;
	// 	stage++;
	// 	SceneManager.LoadScene (nextStage);

	// }
}