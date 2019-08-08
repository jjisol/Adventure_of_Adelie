using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boss{
	public GameObject boss;
	public Vector3 bossSpawnValues;
	public float bossSpawnWait;
	public float bossStartWait;
	public float bossWaveWait;
}

public class GameController : MonoBehaviour
{
	public GameObject[] hazards;
	public int hazardCount;
	public Vector3 spawnValues;
	public float spawnWait;
	public float startWait;
	public float waveWait;
	public Boss boss1;

	public GUIText scoreText;
	public GUIText restartText;
	public GUIText gameOverText;
	public GUIText stageClearText;

	[HideInInspector] public Animator animColorFade;
	public AnimationClip fadeColorAnimationClip;

	private bool gameOver;
	private bool restart;
	[HideInInspector] public bool stageClear;
	private int score;
	public int bossCount;

	void Start ()
	{
		gameOver = false;
		restart = false;
		stageClear = false;
		gameOverText.text = "";
		restartText.text = "";
		stageClearText.text = "";
		score = 0;
		UpdateScore ();
		StartCoroutine (SpawnWaves ());
	}

	void FixedUpdate ()
	{
		if (score >= 300) {
			stageClearText.text = "Stage Clear!";
			stageClear = true;
		}

	}

	void Update ()
	{
		if (restart)
		{
			if (Input.GetKeyDown (KeyCode.R))
			{
				Application.LoadLevel (Application.loadedLevel);
			}
		}

	}

	IEnumerator SpawnWaves ()
	{
		if (Application.loadedLevel == 1)
		{
		yield return new WaitForSeconds (startWait);
			while (true) {
				for (int i = 0; i < hazardCount; i++) {
					GameObject hazard = hazards [Random.Range (0, hazards.Length - 1)];
					Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
					Quaternion spawnRotation = Quaternion.identity;
					Instantiate (hazard, spawnPosition, spawnRotation);
					yield return new WaitForSeconds (spawnWait);
				}
				yield return new WaitForSeconds (waveWait);

				if (stageClear) {
					LoadDelayed ();
				}

				if (gameOver) {
					restartText.text = "Press 'R' for Restart";
					restart = true;
					break;
				}
			}
		}

		if (Application.loadedLevel == 2)
		{
			yield return new WaitForSeconds (startWait);
			while (true) {
				for (int i = 0; i < hazardCount; i++) {
					GameObject hazard = hazards [Random.Range (0, hazards.Length)];
					Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
					Quaternion spawnRotation = Quaternion.identity;
					Instantiate (hazard, spawnPosition, spawnRotation);
					yield return new WaitForSeconds (spawnWait);
				}
				yield return new WaitForSeconds (waveWait);

				if (stageClear) {
					LoadDelayed ();
				}

				if (gameOver) {
					restartText.text = "Press 'R' for Restart";
					restart = true;
					break;
				}
			}
		}

		if (Application.loadedLevel == 3)
		{
			yield return new WaitForSeconds (startWait);
			//yield return new WaitForSeconds (boss1.bossStartWait);
			Vector3 bossSpawnPosition = new Vector3 (Random.Range (-boss1.bossSpawnValues.x, boss1.bossSpawnValues.x), boss1.bossSpawnValues.y, boss1.bossSpawnValues.z);
			Quaternion bossSpawnRotation = Quaternion.identity;
			Instantiate (boss1.boss, bossSpawnPosition, bossSpawnRotation);
			//yield return new WaitForSeconds (boss1.bossSpawnWait);
			//count++;
			while (true) {

				for (int i = 0; i < hazardCount; i++) {
					GameObject hazard = hazards [Random.Range (0, hazards.Length)];
					Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
					Quaternion spawnRotation = Quaternion.identity;
					Instantiate (hazard, spawnPosition, spawnRotation);
					yield return new WaitForSeconds (spawnWait);

				} yield return new WaitForSeconds (waveWait);

				if (stageClear) {
					stageClearText.text = "Conglatulation!";
					//animColorFade.SetTrigger ("fade");
					break;
				}

				if (gameOver) {
					restartText.text = "Press 'R' for Restart";
					restart = true;
					break;
				}
			}
		}
	}

	public void LoadDelayed()
	{	
		Application.LoadLevel (Application.loadedLevel + 1);
	}

	public void AddScore (int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore ();
	}

	void UpdateScore ()
	{
		scoreText.text = "Score: " + score;
	}

	public void GameOver ()
	{
		gameOverText.text = "Game Over!";
		gameOver = true;
	}

}