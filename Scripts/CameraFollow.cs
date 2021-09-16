using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public GameObject player;

	private GameObject pos;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		CamFollow();
		/*gameObject.transform.position = new Vector3((Mathf.RoundToInt(gameObject.transform.position.x)) - 0.7f,
		(Mathf.RoundToInt(gameObject.transform.position.y)) - 0.7f,(Mathf.RoundToInt(gameObject.transform.position.z)) - 0.7f);     //used to keep camera bounded to integers*/
	}

	private void CamFollow ()
	{
		transform.position = player.transform.position + new Vector3(0,0,-10);
	}
}
