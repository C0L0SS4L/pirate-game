using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FortIslandSpawner : MonoBehaviour {

	public GameObject island1;
	public GameObject island2;
	public GameObject island3;
	public GameObject island4;
	public GameObject island5;
	public GameObject island6;
	public GameObject island7;

	private bool spawnedIsland;

	void Awake()
	{
		SpawnPirate();
	}

	/*void OnEnable()
	{
		SceneManager.sceneLoaded += SpawnPirate;
	}

	void OnDisable()
	{
		SceneManager.sceneLoaded -= SpawnPirate;
	}*/

	void SpawnPirate ()
	{

		int randomInt = Random.Range (1,6);

		int randomIsland = Random.Range(1,8);

		if (GameManager.Instance.fortsOnScreen < GameManager.Instance.fortsLeft)
		{
			if (randomInt == 1)
			{
				if (randomIsland == 1 && GameObject.FindWithTag("Island1") == null)
				{
					Instantiate(island1, transform.position, Quaternion.identity);
					GameManager.Instance.fortsOnScreen += 1;
					spawnedIsland = true;
				}
				else if (randomIsland == 2 && GameObject.FindWithTag("Island2") == null)
				{
					Instantiate(island2, transform.position, Quaternion.identity);
					GameManager.Instance.fortsOnScreen += 1;
					spawnedIsland = true;
				}
				else if (randomIsland == 3 && GameObject.FindWithTag("Island3") == null)
				{
					Instantiate(island3, transform.position, Quaternion.identity);
					GameManager.Instance.fortsOnScreen += 1;
					spawnedIsland = true;
				}
				else if (randomIsland == 4 && GameObject.FindWithTag("Island4") == null)
				{
					Instantiate(island4, transform.position, Quaternion.identity);
					GameManager.Instance.fortsOnScreen += 1;
					spawnedIsland = true;
				}
				else if (randomIsland == 5 && GameObject.FindWithTag("Island5") == null)
				{
					Instantiate(island5, transform.position, Quaternion.identity);
					GameManager.Instance.fortsOnScreen += 1;
					spawnedIsland = true;
				}
				else if (randomIsland == 6 && GameObject.FindWithTag("Island6") == null)
				{
					Instantiate(island6, transform.position, Quaternion.identity);
					GameManager.Instance.fortsOnScreen += 1;
					spawnedIsland = true;
				}
				else if (randomIsland == 7 && GameObject.FindWithTag("Island7") == null)
				{
					Instantiate(island7, transform.position, Quaternion.identity);
					GameManager.Instance.fortsOnScreen += 1;
					spawnedIsland = true;
				}
			}
			/*else if (GameManager.Instance.fortsOnScreen != GameManager.Instance.maxForts)
			{
				if (randomIsland == 1)
				{
					Instantiate(island1, transform.position, Quaternion.identity);
					GameManager.Instance.fortsOnScreen += 1;
				}
				else if (randomIsland == 2)
				{
					Instantiate(island2, transform.position, Quaternion.identity);
					GameManager.Instance.fortsOnScreen += 1;
				}
			}*/
		}
		
	}

	void Start()
	{
		if (GameManager.Instance.fortsOnScreen != GameManager.Instance.fortsLeft && !spawnedIsland)
		{
			int randomIsland = Random.Range(1,8);

			if (randomIsland == 1)
			{
				Instantiate(island1, transform.position, Quaternion.identity);
				GameManager.Instance.fortsOnScreen += 1;
			}
			else if (randomIsland == 2)
			{
				Instantiate(island2, transform.position, Quaternion.identity);
				GameManager.Instance.fortsOnScreen += 1;
			}
			else if (randomIsland == 3)
			{
				Instantiate(island3, transform.position, Quaternion.identity);
				GameManager.Instance.fortsOnScreen += 1;
			}
			else if (randomIsland == 4)
			{
				Instantiate(island4, transform.position, Quaternion.identity);
				GameManager.Instance.fortsOnScreen += 1;
			}
			else if (randomIsland == 5)
			{
				Instantiate(island5, transform.position, Quaternion.identity);
				GameManager.Instance.fortsOnScreen += 1;
			}
			else if (randomIsland == 6)
			{
				Instantiate(island6, transform.position, Quaternion.identity);
				GameManager.Instance.fortsOnScreen += 1;
			}
			else if (randomIsland == 7)
			{
				Instantiate(island7, transform.position, Quaternion.identity);
				GameManager.Instance.fortsOnScreen += 1;
			}
		}
	}
}
