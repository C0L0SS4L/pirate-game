using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Advertisements;

public class PauseManager : MonoBehaviour {

	public Text coinCounter;
	public GameObject seaClearedScreenGO;
	public TextMeshProUGUI seaClearedText;
	public TextMeshProUGUI piratesKilledText;
	public TextMeshProUGUI fortsDestroyedText;
	public TextMeshProUGUI coinsCollectedText;
	public TextMeshProUGUI chestsOpenedText;

	public static bool isPaused;
	public static bool nextSeaClick;
	public static bool gameOverClick;

	private bool seaScreenOpen;

	void Awake()
	{
		UIManager.Instance.create = true;
		isPaused = false;
	}

	void Update()
	{
		if (GameManager.Instance.fortsOnScreen == 0 && !seaClearedScreenGO.activeInHierarchy && !seaScreenOpen)
		{
			/*seaClearedText.text = "Sea " + GameManager.Instance.seaLevel + " Cleared!";
			piratesKilledText.text = "Pirates killed: " + GameManager.Instance.piratesKilled;
			fortsDestroyedText.text = "Forts destroyed: " + GameManager.Instance.fortsDestroyed;
			coinsCollectedText.text = "Coins collected: " + GameManager.Instance.coins;
			chestsOpenedText.text = "Chests opened: " + GameManager.Instance.chestsOpened;*/
			seaScreenOpen = true;
			StartCoroutine(OpenSeaClearedScreen());

			/*if (AdManager.Instance.sea_adDone)
			{
				AdManager.Instance.sea_adDone = false;
				Invoke("ChangeSceneAfterAd", 0.15f);
			}*/
		}
	}

	public void Pause () 
	{
		if (!GameManager.Instance.adShowing)
		{
			isPaused = true;
			FindObjectOfType<AudioManager>().Play("UI Click");
			Time.timeScale = 0f;
		}

	}

	public void Resume ()
	{
		isPaused = false;
		//getDevCoins = 0;
		FindObjectOfType<AudioManager>().Play("UI Click");
		Time.timeScale = 1f;
	}

	public void Home ()
	{
		FindObjectOfType<AudioManager>().Play("UI Click");
		Time.timeScale = 1f;
		GameManager.Instance.piratesOnScreen = 0;
		GameManager.Instance.fortsOnScreen = 0;
		AIPlayer.updateSeaHighText = true;
		SceneManager.LoadScene("HomeScreen");
		
	}

	/*public void Restart ()
	{
		FindObjectOfType<AudioManager>().Play("UI Click");
		GameManager.Instance.piratesOnScreen = 0;
		GameManager.Instance.fortsOnScreen = 0;
		GameManager.Instance.coins = 0;
		GameManager.Instance.piratesKilled = 0;
		GameManager.Instance.fortsDestroyed = 0;
		GameManager.Instance.chestsOpened = 0;
		GameManager.Instance.cannons = 1;
		GameManager.Instance.maxHealth = 100;
		GameManager.Instance.playerMoveSpeed = 45f;
		GameManager.Instance.playerCannonDamage = 10;
		Time.timeScale = 1f;
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}*/

	public void NextSea ()
	{
		if (!GameManager.Instance.adShowing)
		{
			GameManager.Instance.piratesOnScreen = 0;
			GameManager.Instance.seaLevel++;
			nextSeaClick = true;
			if (!GameManager.Instance.removeAds)
			{
				AdManager.Instance.ShowAd();
			}
			else
			{
				Time.timeScale = 1;
				SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			}
			
			//Time.timeScale = 1f;
			//seaClearedScreenGO.SetActive(false);
			//SceneManager.LoadScene(SceneManager.GetActiveScene().name);	
		}

	}

	public void PlayerDied ()
	{
		if (!GameManager.Instance.adShowing)
		{
			/*GameManager.Instance.piratesOnScreen = 0;
			GameManager.Instance.fortsOnScreen = 0;
			GameManager.Instance.coins = 0;
			GameManager.Instance.piratesKilled = 0;
			GameManager.Instance.fortsDestroyed = 0;
			GameManager.Instance.chestsOpened = 0;
			GameManager.Instance.cannons = 1;
			GameManager.Instance.maxHealth = 100;
			GameManager.Instance.playerMoveSpeed = 45f;
			GameManager.Instance.playerCannonDamage = 10;
			GameManager.Instance.seaLevel = 1;
			GameManager.Instance.playerHealth = 100;*/
			gameOverClick = true;
			if (!GameManager.Instance.removeAds && Advertisement.IsReady())
			{
				AdManager.Instance.ShowAd();
			}
			else
			{
				Time.timeScale = 1f;
				SceneManager.LoadScene("HomeScreen");
			}
			
			//Time.timeScale = 1f;
			AIPlayer.updateSeaHighText = true;
			//SceneManager.LoadScene("HomeScreen");
		}

	}

	public void ShowRewardAd ()
	{
		AdManager.Instance.ShowRewardAd();
	}

	IEnumerator OpenSeaClearedScreen ()
	{
		yield return new WaitForSeconds(1.6f);

		seaClearedText.text = "Sea " + GameManager.Instance.seaLevel + " Cleared!";
		piratesKilledText.text = "Pirates killed: " + GameManager.Instance.piratesKilled;
		fortsDestroyedText.text = "Forts destroyed: " + GameManager.Instance.fortsDestroyed;
		coinsCollectedText.text = "Coins collected: " + GameManager.Instance.coins;
		chestsOpenedText.text = "Chests opened: " + GameManager.Instance.chestsOpened;
		seaClearedScreenGO.SetActive(true);
		Time.timeScale = 0f;
		seaScreenOpen = false;
	}
}
