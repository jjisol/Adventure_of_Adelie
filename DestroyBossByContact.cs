using UnityEngine;
using System.Collections;

public class DestroyBossByContact : MonoBehaviour
{
	public GameObject explosion;
	public GameObject playerExplosion;
	public int hp;
	public int scoreValue;

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
			
		if (other.CompareTag ("Boundary") || other.CompareTag ("Enemy") || other.CompareTag ("EnemyBolt"))
		{
			return;
		}

		if (explosion != null)
		{
			Instantiate (explosion, transform.position, transform.rotation);
		}
	
		if (other.CompareTag ("Player"))
		{
			Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
			gameController.GameOver();
		}

		if (hp > 0) 
		{
			hp -= 1;
			Destroy(other.gameObject);
		}
		else
		{
			gameController.AddScore (scoreValue);
			Destroy(other.gameObject);
			Destroy(gameObject);
			//gameController.count--;
		}
	}
}