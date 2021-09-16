using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestSpawner : MonoBehaviour {

	//public GameObject onIslandTilemap;
	public GameObject chestGO;

	void Awake ()
	{
		
		/*foreach (Transform child in onIslandTilemap.transform)
		{
			if(child.tag == "SmallChest") {
				child.gameObject.SetActive(true);

				int randomInt = Random.Range (1,3);

			
				if (randomInt == 1) {
					child.gameObject.SetActive(true);
				} else {
					child.gameObject.SetActive(false);
				}
			}
			
		}*/

		int randomInt = Random.Range (1,3);

		if (randomInt == 1)
		{
			Instantiate(chestGO, transform.position, Quaternion.identity);
		}
		
	}
}
