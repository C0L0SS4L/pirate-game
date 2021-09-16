using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SeaTextScript : MonoBehaviour {

	public Animator animator;
	public TextMeshProUGUI seaText;
	public GameObject fortsText;

	// Use this for initialization
	void Start () {
		seaText.text = "Sea " + GameManager.Instance.seaLevel;

		if (GameManager.Instance.faction != 0)
		{
			StartCoroutine(FadeSeaText());
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void StartFadeSeaText ()
	{
		StartCoroutine(FadeSeaText());
	}

	IEnumerator FadeSeaText ()
	{
		yield return new WaitForSeconds (1f);

		animator.Play("SeaTextFade_In");

		yield return new WaitForSeconds (4.5f);

		animator.Play("SeaTextFade_Out");

		fortsText.SetActive(true);
	}
}
