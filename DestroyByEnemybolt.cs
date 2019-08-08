using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByEnemybolt : MonoBehaviour {

	public GameObject explosion;
	public GameObject playerExplosion;
	private GameController gameController;

	void Start ()
	{
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent <GameController>();
		}
		if (gameController == null)
		{
			Debug.Log ("Cannot find 'GameController' script");
		}
	}

	void OnTriggerEnter(Collider other) 
	{
		if (other.CompareTag ("Boundary") || other.CompareTag("PlayerBolt"))
		{
			return;
		}

		if (explosion != null)
		{
			Instantiate (explosion, transform.position, transform.rotation);
		}

		if (other.CompareTag ("EnemyBolt"))
		{
			Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
			gameController.GameOver();
		}
		Destroy(gameObject);
	}
}
