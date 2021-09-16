using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

	private static UIManager _instance;

	public static UIManager Instance {get; private set;}
	/*{
		get
		{
			if (_instance == null)
			{
				GameObject go = new GameObject("UIManager");
				go.AddComponent<UIManager>();
			}

			return _instance;
		}	
	}*/

	private GameObject mainCanvasGO;

	public bool create;

	private void Awake() 
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}

		//_instance = this;

		if (mainCanvasGO == null)
		{
			mainCanvasGO = GameObject.Find("MainCanvas");
			if (mainCanvasGO != null)
			{
				Debug.Log("MainCanvas is not null");
			}
		}

		if (GameObject.Find("MainCanvas") == null)
		{
			GameObject.Instantiate(mainCanvasGO,new Vector3(960,540,0), Quaternion.identity);
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (mainCanvasGO == null && SceneManager.GetActiveScene().name == "Game")
		{
			mainCanvasGO = GameObject.Find("MainCanvas");
			if (mainCanvasGO != null)
			{
				Debug.Log("MainCanvas is not null");
			}
		}

		if (SceneManager.GetActiveScene().name == "Game") 
		{
			if (PauseManager.isPaused)
			{
				mainCanvasGO.transform.Find("PauseScreen").gameObject.SetActive(true);
			}
			else 
			{
				mainCanvasGO.transform.Find("PauseScreen").gameObject.SetActive(false);
			}
		}

		/*if (GameManager.Instance.fortsOnScreen == 0)
		{
			mainCanvasGO.transform.GetChild(11).gameObject.SetActive(true);
		}*/
	}
}
