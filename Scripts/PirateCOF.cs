using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateCOF : MonoBehaviour {

	private float firstShotCountdown = 0;

	// Use this for initialization
	void Start () {
	}

	private void Update() {
		firstShotCountdown -= Time.deltaTime;
	}

	private void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag == "Player")
		{
			firstShotCountdown = 2f;
		}
	}

	void OnTriggerStay2D(Collider2D col) {
		
		if (col.gameObject.tag == "Player")
		{
			if (col.gameObject.tag != "CannonBall" && firstShotCountdown <= 0/*&& col.gameObject.name != "r_cannon los" && col.gameObject.name != "d_cannon los" && col.gameObject.name != "l_cannon los" && col.gameObject.name != "u_cannon los"*//*col.gameObject.name != "CannonBall(Clone)"*/) {
				if (this.gameObject.name == "r_cof")
				{
					transform.parent.GetComponent<PirateAI>().RightSideFire();
				}
				if (this.gameObject.name == "L_cof")
				{
					transform.parent.GetComponent<PirateAI>().LeftSideFire();
				}
			}

		}

	}
}
