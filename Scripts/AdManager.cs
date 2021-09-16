using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class AdManager : MonoBehaviour {

	public static AdManager Instance {get; private set;}

	public bool sea_adDone;
	public bool gOver_adDone;
	public GameObject adCantBeLoadTxt;

	private bool rewardAdDone;

    #if UNITY_IOS
    private string gameId = "2726730";
    #elif UNITY_ANDROID
    private string gameId = "2726731";
    #elif UNITY_EDITOR
	private string gameId = "unexpected_platform";
	#endif

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else 
		{
			Destroy(gameObject);
		}
	}

	void Start()
	{
		if (Advertisement.isSupported)
		{
			Advertisement.Initialize(gameId, false);
			Debug.Log("Ads Initialized");
		}
	}

	public void ShowAd ()
	{
		if (!GameManager.Instance.removeAds)
		{
			if (Advertisement.IsReady("video"))
			{
				Advertisement.Show("video", new ShowOptions(){resultCallback = HandleAdResult});
				GameManager.Instance.adShowing = true;
			}
		}

	}

	private void HandleAdResult(ShowResult result)
	{
		switch (result)
		{
			case ShowResult.Finished:
				//Time.timeScale = 1f;
				Debug.Log("Ad finished");
				//GameManager.Instance.adShowing = false;
				Invoke("TurnOffAdShow", 0.07f);
				/*if (PauseManager.nextSeaClick)
				{
					PauseManager.nextSeaClick = false;
					SceneManager.LoadScene(SceneManager.GetActiveScene().name);
				}
				else if (PauseManager.gameOverClick)
				{
					PauseManager.gameOverClick = false;
					SceneManager.LoadScene("HomeScreen");
				}*/
				/*if (PauseManager.nextSeaClick)
				{
					sea_adDone = true;
				}
				else if (PauseManager.gameOverClick)
				{
					gOver_adDone = true;
				}*/
				
				Time.timeScale = 1f;
				break;
			case ShowResult.Skipped:
				//Time.timeScale = 1f;
				Debug.Log("Ad skipped");
				//GameManager.Instance.adShowing = false;
				Invoke("TurnOffAdShow", 0.07f);
				/*if (PauseManager.nextSeaClick)
				{
					PauseManager.nextSeaClick = false;
					SceneManager.LoadScene(SceneManager.GetActiveScene().name);
				}
				else if (PauseManager.gameOverClick)
				{
					PauseManager.gameOverClick = false;
					SceneManager.LoadScene("HomeScreen");
				}*/
				/*if (PauseManager.nextSeaClick)
				{
					sea_adDone = true;
				}
				else if (PauseManager.gameOverClick)
				{
					gOver_adDone = true;
				}*/
				
				Time.timeScale = 1f;
				break;
			case ShowResult.Failed:
				//Time.timeScale = 1f;
				Debug.Log("Ad failed");
				//GameManager.Instance.adShowing = false;
				Invoke("TurnOffAdShow", 0.07f);
				/*if (PauseManager.nextSeaClick)
				{
					PauseManager.nextSeaClick = false;
					SceneManager.LoadScene(SceneManager.GetActiveScene().name);
				}
				else if (PauseManager.gameOverClick)
				{
					PauseManager.gameOverClick = false;
					SceneManager.LoadScene("HomeScreen");
				}*/
				/*if (PauseManager.nextSeaClick)
				{
					sea_adDone = true;
				}
				else if (PauseManager.gameOverClick)
				{
					gOver_adDone = true;
				}*/
				
				Time.timeScale = 1f;
				break;
		}
	}

	public void ShowRewardAd ()
	{
		if (Advertisement.IsReady("rewardedVideo"))
		{
			Advertisement.Show("rewardedVideo", new ShowOptions(){resultCallback = HandleRewardAdResult});
			GameManager.Instance.adShowing = true;
			Time.timeScale = 0f;
		}
	}

	private void HandleRewardAdResult(ShowResult result)
	{
		switch (result)
		{
			case ShowResult.Finished:
				//Time.timeScale = 1f;
				Debug.Log("Ad finished");
				rewardAdDone = true;
				//GameManager.Instance.adShowing = false;
				Invoke("TurnOffAdShow", 0.07f);
				/*if (PauseManager.nextSeaClick)
				{
					PauseManager.nextSeaClick = false;
					SceneManager.LoadScene(SceneManager.GetActiveScene().name);
				}
				else if (PauseManager.gameOverClick)
				{
					PauseManager.gameOverClick = false;
					SceneManager.LoadScene("HomeScreen");
				}*/
				/*if (PauseManager.nextSeaClick)
				{
					sea_adDone = true;
				}
				else if (PauseManager.gameOverClick)
				{
					gOver_adDone = true;
				}*/
				
				Time.timeScale = 1f;
				break;
			case ShowResult.Failed:
				//Time.timeScale = 1f;
				Debug.Log("Ad failed");
				//GameManager.Instance.adShowing = false;
				StartCoroutine(AdCantLoad());
				/*if (PauseManager.nextSeaClick)
				{
					PauseManager.nextSeaClick = false;
					SceneManager.LoadScene(SceneManager.GetActiveScene().name);
				}
				else if (PauseManager.gameOverClick)
				{
					PauseManager.gameOverClick = false;
					SceneManager.LoadScene("HomeScreen");
				}*/
				/*if (PauseManager.nextSeaClick)
				{
					sea_adDone = true;
				}
				else if (PauseManager.gameOverClick)
				{
					gOver_adDone = true;
				}*/
				
				Time.timeScale = 1f;
				break;
		}
	}

	private void TurnOffAdShow()
	{
		GameManager.Instance.adShowing = false;

		if (!rewardAdDone)
		{
			if (PauseManager.nextSeaClick)
			{
				PauseManager.nextSeaClick = false;
				SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			}
			else if (PauseManager.gameOverClick)
			{
				PauseManager.gameOverClick = false;
				SceneManager.LoadScene("HomeScreen");
			}		
		}
		else
		{
			GameManager.Instance.playerHealth = GameManager.Instance.maxHealth;
			GameManager.Instance.piratesOnScreen = 0;
			GameManager.Instance.fortsOnScreen = 0;
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
	}

	IEnumerator AdCantLoad ()
	{
		adCantBeLoadTxt.SetActive(true);
		yield return new WaitForSeconds (3);
		adCantBeLoadTxt.SetActive(false);
	}
}
