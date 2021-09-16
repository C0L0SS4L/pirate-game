using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestBehavior : MonoBehaviour {

	[SerializeField] private GameObject chestCoinGO;

	public GameObject coinGO10;
	public GameObject coinGO20;

	/*private GameObject clone1;
	private GameObject clone2;
	private GameObject clone3;
	private GameObject clone4;
	private GameObject clone5;
	private GameObject clone6;
	private GameObject clone7;
	private GameObject clone8;
	private GameObject clone9;
	private GameObject clone10;
	private GameObject clone11;
	private GameObject clone12;
	private GameObject clone13;
	private GameObject clone14;
	private GameObject clone15;
	private GameObject clone16;
	private GameObject clone17;
	private GameObject clone18;
	private GameObject clone19;
	private GameObject clone20;*/

	void OnTriggerEnter2D (Collider2D col) 
	{
		if (this.gameObject.tag == "SmallChest")
		{
			if (col.gameObject.tag == "Player") {
				GetComponent<SpriteRenderer>().enabled = false;

				Instantiate(coinGO10, transform.position, Quaternion.identity);
				GameManager.Instance.chestsOpened++;

				/*Vector3 newCoinPos1 = new Vector2(transform.position.x, transform.position.y + 1);
				Vector3 newCoinPos2 = new Vector2(transform.position.x, transform.position.y - 1);
				Vector3 newCoinPos3 = new Vector2(transform.position.x + 1, transform.position.y);
				Vector3 newCoinPos4 = new Vector2(transform.position.x - 1, transform.position.y);
				Vector3 newCoinPos5 = new Vector2(transform.position.x + 0.7f, transform.position.y + 0.7f);
				Vector3 newCoinPos6 = new Vector2(transform.position.x - 0.7f, transform.position.y - 0.7f);
				Vector3 newCoinPos7 = new Vector2(transform.position.x + 0.7f, transform.position.y - 0.7f);
				Vector3 newCoinPos8 = new Vector2(transform.position.x - 0.7f, transform.position.y + 0.7f);
				Vector3 newCoinPos9 = new Vector2(transform.position.x + 0.5f, transform.position.y);
				Vector3 newCoinPos10 = new Vector2(transform.position.x - 0.5f, transform.position.y);

				clone1 = Instantiate (chestCoinGO, newCoinPos1, Quaternion.identity);
				clone2 = Instantiate (chestCoinGO, newCoinPos2, Quaternion.identity);
				clone3 = Instantiate (chestCoinGO, newCoinPos3, Quaternion.identity);
				clone4 = Instantiate (chestCoinGO, newCoinPos4, Quaternion.identity);
				clone5 = Instantiate (chestCoinGO, newCoinPos5, Quaternion.identity);
				clone6 = Instantiate (chestCoinGO, newCoinPos6, Quaternion.identity);
				clone7 = Instantiate (chestCoinGO, newCoinPos7, Quaternion.identity);
				clone8 = Instantiate (chestCoinGO, newCoinPos8, Quaternion.identity);
				clone9 = Instantiate (chestCoinGO, newCoinPos9, Quaternion.identity);
				clone10 = Instantiate (chestCoinGO, newCoinPos10, Quaternion.identity);

				clone1.SetActive(true);
				clone2.SetActive(true);
				clone3.SetActive(true);
				clone4.SetActive(true);
				clone5.SetActive(true);
				clone6.SetActive(true);
				clone7.SetActive(true);
				clone8.SetActive(true);
				clone9.SetActive(true);
				clone10.SetActive(true);*/

				Destroy(this.gameObject);

			}			
		}

	}

	public void FortDestroyed (Transform fort)
	{
		GameManager.Instance.fortsOnScreen -= 1;
		GameManager.Instance.fortsDestroyed++;
		GameManager.Instance.fortsLeft -= 1;

		if (fort.gameObject.name == "Fort1")
		{
			print("fort1");
			if (gameObject.name == "PirateTreasure1")
			{
				print ("priateTreasure1");
				TreasureCoinSpawner();
			}
		}

		if (fort.gameObject.name == "Fort2")
		{
			if (gameObject.name == "PirateTreasure2")
			{
				TreasureCoinSpawner();
			}
		}

		if (fort.gameObject.name == "Fort3")
		{
			TreasureCoinSpawner();
		}

		if (fort.gameObject.name == "Fort4")
		{
			TreasureCoinSpawner();
		}

		if (fort.gameObject.name == "Fort5")
		{
			TreasureCoinSpawner();
		}

		if (fort.gameObject.name == "Fort6")
		{
			TreasureCoinSpawner();
		}

		if (fort.gameObject.name == "Fort7")
		{
			TreasureCoinSpawner();
		}
	}

	void TreasureCoinSpawner ()
	{
		print("treasureSpawnWOrked");
		GetComponent<SpriteRenderer>().enabled = false;

		Instantiate(coinGO20, transform.position, Quaternion.identity);

		/*Vector3 newCoinPos1 = new Vector2(transform.position.x, transform.position.y + 1);
		Vector3 newCoinPos2 = new Vector2(transform.position.x, transform.position.y - 1);
		Vector3 newCoinPos3 = new Vector2(transform.position.x + 1, transform.position.y);
		Vector3 newCoinPos4 = new Vector2(transform.position.x - 1, transform.position.y);
		Vector3 newCoinPos5 = new Vector2(transform.position.x + 0.7f, transform.position.y + 0.7f);
		Vector3 newCoinPos6 = new Vector2(transform.position.x - 0.7f, transform.position.y - 0.7f);
		Vector3 newCoinPos7 = new Vector2(transform.position.x + 0.7f, transform.position.y - 0.7f);
		Vector3 newCoinPos8 = new Vector2(transform.position.x - 0.7f, transform.position.y + 0.7f);
		Vector3 newCoinPos9 = new Vector2(transform.position.x + 0.5f, transform.position.y);
		Vector3 newCoinPos10 = new Vector2(transform.position.x - 0.5f, transform.position.y);
		Vector3 newCoinPos11 = new Vector2(transform.position.x + 1.6f, transform.position.y);
		Vector3 newCoinPos12 = new Vector2(transform.position.x - 1.6f, transform.position.y);
		Vector3 newCoinPos13 = new Vector2(transform.position.x, transform.position.y + 1.6f);
		Vector3 newCoinPos14 = new Vector2(transform.position.x, transform.position.y - 1.6f);
		Vector3 newCoinPos15 = new Vector2(transform.position.x + 1.3f, transform.position.y + 1.3f);
		Vector3 newCoinPos16 = new Vector2(transform.position.x - 1.3f, transform.position.y - 1.3f);
		Vector3 newCoinPos17 = new Vector2(transform.position.x - 1.3f, transform.position.y + 1.3f);
		Vector3 newCoinPos18 = new Vector2(transform.position.x + 1.3f, transform.position.y - 1.3f);
		Vector3 newCoinPos19 = new Vector2(transform.position.x, transform.position.y + 0.5f);
		Vector3 newCoinPos20 = new Vector2(transform.position.x, transform.position.y - 0.5f);

		clone1 = Instantiate (chestCoinGO, newCoinPos1, Quaternion.identity);
		clone2 = Instantiate (chestCoinGO, newCoinPos2, Quaternion.identity);
		clone3 = Instantiate (chestCoinGO, newCoinPos3, Quaternion.identity);
		clone4 = Instantiate (chestCoinGO, newCoinPos4, Quaternion.identity);
		clone5 = Instantiate (chestCoinGO, newCoinPos5, Quaternion.identity);
		clone6 = Instantiate (chestCoinGO, newCoinPos6, Quaternion.identity);
		clone7 = Instantiate (chestCoinGO, newCoinPos7, Quaternion.identity);
		clone8 = Instantiate (chestCoinGO, newCoinPos8, Quaternion.identity);
		clone9 = Instantiate (chestCoinGO, newCoinPos9, Quaternion.identity);
		clone10 = Instantiate (chestCoinGO, newCoinPos10, Quaternion.identity);
		clone11 = Instantiate (chestCoinGO, newCoinPos11, Quaternion.identity);
		clone12 = Instantiate (chestCoinGO, newCoinPos12, Quaternion.identity);
		clone13 = Instantiate (chestCoinGO, newCoinPos13, Quaternion.identity);
		clone14 = Instantiate (chestCoinGO, newCoinPos14, Quaternion.identity);
		clone15 = Instantiate (chestCoinGO, newCoinPos15, Quaternion.identity);
		clone16 = Instantiate (chestCoinGO, newCoinPos16, Quaternion.identity);
		clone17 = Instantiate (chestCoinGO, newCoinPos17, Quaternion.identity);
		clone18 = Instantiate (chestCoinGO, newCoinPos18, Quaternion.identity);
		clone19 = Instantiate (chestCoinGO, newCoinPos19, Quaternion.identity);
		clone20 = Instantiate (chestCoinGO, newCoinPos20, Quaternion.identity);

		clone1.SetActive(true);
		clone2.SetActive(true);
		clone3.SetActive(true);
		clone4.SetActive(true);
		clone5.SetActive(true);
		clone6.SetActive(true);
		clone7.SetActive(true);
		clone8.SetActive(true);
		clone9.SetActive(true);
		clone10.SetActive(true);
		clone11.SetActive(true);
		clone12.SetActive(true);
		clone13.SetActive(true);
		clone14.SetActive(true);
		clone15.SetActive(true);
		clone16.SetActive(true);
		clone17.SetActive(true);
		clone18.SetActive(true);
		clone19.SetActive(true);
		clone20.SetActive(true);*/

		Destroy(this.gameObject);
	}
}
