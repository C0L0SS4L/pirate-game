using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinBehavior : MonoBehaviour {

	public float coinMoveSpeed = 2f;


	void Start () {
		
	}
	
	void Update () {
		
	}

	void OnTriggerStay2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player") {
			transform.position = Vector2.Lerp (transform.position, col.gameObject.transform.position, Time.deltaTime * coinMoveSpeed);
		}
	}

}
