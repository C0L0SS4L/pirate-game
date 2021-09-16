using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour {

	public Animator anim;

	void Start () {

		if (this.gameObject.name == "Explosion(Clone)") {

			Destroy(gameObject, 0.25f);
			return;
		}

		if (this.gameObject.name == "WaterRipple(Clone)") {


			Destroy(gameObject, 0.25f);
			return;
		}

	}
}
