using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontRotate : MonoBehaviour {

	Quaternion rotation;

	void Awake()
	{
		rotation = transform.rotation;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void LateUpdate()
	{
		transform.rotation = rotation;
		transform.position = transform.parent.transform.position + new Vector3(0,1.5f,0);
	}
}
