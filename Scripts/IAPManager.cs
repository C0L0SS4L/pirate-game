using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAPManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
			
	}

	public void RemoveAds ()
	{
		if (!GameManager.Instance.removeAds)
		{
			GameManager.Instance.removeAds = true;
		}
		
	}
}
