using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeButtonManager : MonoBehaviour {

	public Image audioButImg;
	public Sprite audioOn;
	public Sprite audioOff;
	public GameObject removeAdsBut;
	public GameObject restoreBut;

	private void Awake() {
		//#if UNITY_IOS
			/*removeAdsBut.SetActive(false);
			Vector3 position = new Vector3(0, audioButImg.gameObject.transform.position.y,0);
			audioButImg.gameObject.transform.position = position + new Vector3(640,0,0);*/

			/*restoreBut.SetActive(true);
			Vector3 rAdsYPos = new Vector3(433.5f, removeAdsBut.transform.position.y, 0);
			removeAdsBut.transform.position = rAdsYPos + new Vector3 (0, 0, 0);

			Vector3 audYPos = new Vector3(433.5f, audioButImg.gameObject.transform.position.y, 0);
			audioButImg.gameObject.transform.position = audYPos + new Vector3(200, 0, 0);*/
		/*#elif UNITY_EDITOR
			removeAdsBut.SetActive(false);
			Vector3 position = new Vector3(0, audioButImg.gameObject.transform.position.y,0);
			audioButImg.gameObject.transform.position = position + new Vector3(640,0,0);*/
		//#endif
	}

	void Start()
	{
		if (!AudioListener.pause)
		{
			audioButImg.sprite = audioOn;
		}
		else 
		{
			audioButImg.sprite = audioOff;
		}
	}

	public void ToggleSound ()
	{
		if (!AudioListener.pause)
		{
			//GameManager.Instance.soundEnabled = false;
			AudioListener.pause = true;
			audioButImg.sprite = audioOff;
		}
		else
		{
			//GameManager.Instance.soundEnabled = true;
			AudioListener.pause = false;
			audioButImg.sprite = audioOn;
		}
	}
}
