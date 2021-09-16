using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour {

	public GameObject shoreTilemap;

	void Awake ()
	{
		
		foreach (Transform child in shoreTilemap.transform)
		{
			child.gameObject.SetActive(true);

			int randomInt = Random.Range (1,5);

		
			if (randomInt == 1) {
				child.gameObject.SetActive(true);
			} else {
				child.gameObject.SetActive(false);
			}
		}
		
	}
}
