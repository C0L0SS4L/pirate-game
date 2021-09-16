using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FortLOS : MonoBehaviour {

	public bool playerInSight;


	private void Start() {
		playerInSight = false;
	}

	private void OnTriggerStay2D(Collider2D col) {
		
		if (col.gameObject.tag == "Player") {
			playerInSight = true;
		}

	}

	private void OnTriggerExit2D(Collider2D col) {
		
		if (col.gameObject.tag == "Player") {
			playerInSight = false;
		}

	}
}
