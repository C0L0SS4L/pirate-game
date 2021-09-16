using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapScript : MonoBehaviour {

	public Transform player;

	void LateUpdate()
	{
		Vector3 newPos = player.position;
		newPos.z = transform.position.z;
		transform.position = newPos;
	}
}
