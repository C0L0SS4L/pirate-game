using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScript : MonoBehaviour {

	public GameObject shopButton;

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Player")
		{
			shopButton.SetActive(true);
		}
		
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.tag == "Player")
		{
			shopButton.SetActive(false);
		}
		
	}
}
