using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestIslandSpawner : MonoBehaviour {

	public GameObject chestIsland;

	void Awake()
	{
		SpawnChestIslands();
	}

	void SpawnChestIslands ()
	{
		int randomInt = Random.Range (1,3);

		if (randomInt == 2)
		{
			Instantiate(chestIsland, transform.position, Quaternion.identity);
		}
	}
}
