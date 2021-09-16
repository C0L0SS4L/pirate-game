using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour {

	//private static GameManager _instance;

	public static GameManager Instance {get; private set;}

	/*{
		get 
		{
			if (_instance == null)
			{
				GameObject go = new GameObject("GameManager");
				go.AddComponent<GameManager>();
			}

			return _instance;
		}
	}*/

	public float playerHealth;
	public int seaLevel = 1;
	public int fortsOnScreen = 0;
	public int fortsDestroyed;
	public int maxForts;
	public int coins;
	public int maxPirates;
	public int piratesOnScreen = 0;
	public int piratesKilled;
	public int faction;
	public int chestsOpened;
	public int seasHighscore = 0;
	public int fortsLeft;
	public bool off;
	public bool moveTutorialDone;
	public bool shootTutorialDone;
	public bool removeAds;

	public bool adShowing;

	//UPGRADEABLE STATS
	public float playerMoveSpeed = 50f;
	public float playerCannonDamage = 10;
	public float maxHealth = 100;
	public int cannons = 1;

	//Shop Variables
	public int speedBought;
	public int damageBought;
	public int healthBought;
	public int cannonsBought;

	public int incSpeedCost = 75;
	public int incDamageCost = 125;
	public int incHealthCost = 200;
	public int incCannonsCost = 500;

	GameData data;

	private void Awake ()
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
		

		data = SaveLoadManager.LoadGame();

		if (data != null)
		{
			//gameInfo
			playerHealth = data.playerHealth;
			seaLevel = data.gameInfo[0];
			fortsDestroyed = data.gameInfo[1];
			coins = data.gameInfo[2];
			piratesKilled = data.gameInfo[3];
			faction = data.gameInfo[4];
			chestsOpened = data.gameInfo[5];
			seasHighscore = data.gameInfo[6];
			fortsLeft = data.gameInfo[7];

			//firstTimeObjs
			moveTutorialDone = data.firstTimeObjs[0];
			shootTutorialDone = data.firstTimeObjs[1];
			removeAds = data.firstTimeObjs[2];

			//upgradeableStats
			playerMoveSpeed = data.upgradeableStats[0];
			playerCannonDamage = data.upgradeableStats[1];
			maxHealth = data.upgradeableStats[2];
			cannons = data.upgradeableCannon;

			//shopVars
			speedBought = data.shopVars[0];
			damageBought = data.shopVars[1];
			healthBought = data.shopVars[2];
			cannonsBought = data.shopVars[3];
			incSpeedCost = data.shopVars[4];
			incDamageCost = data.shopVars[5];
			incHealthCost = data.shopVars[6];
			incCannonsCost = data.shopVars[7];
		}
		else
		{
			Debug.LogWarning("Data is null");
		}
	
		if (playerHealth == 0)
		{
			playerHealth = maxHealth;
		}

		AIPlayer.updateSeaHighText = true;

		print(Application.persistentDataPath);
	}

	void Start () {
		
	}
	
	void Update () {
		/*if (seaLevel > 3 && seaLevel < 9)
		{
			maxPirates = seaLevel;
		}
		else if (seaLevel <= 3)
		{
			maxPirates = 2;
			maxForts = 2;
		}
		else if (seaLevel >= 9)
		{
			maxPirates = 8;
		}*/

		if (seaLevel >= 26)
		{
			maxForts = 7;
			maxPirates = 7;
		}
		else if (seaLevel >= 21)
		{
			maxForts = 6;
			maxPirates = 6;
		}
		else if (seaLevel >= 16)
		{
			maxForts = 5;
			maxPirates = 5;
		}
		else if (seaLevel >= 11)
		{
			maxForts = 4;
			maxPirates = 4;
		}
		else if (seaLevel >= 6)
		{
			maxForts = 3;
			maxPirates = 3;
		}
		else
		{
			maxForts = 2;
			maxPirates = 2;
		}

		if (fortsLeft == 0)
		{
			fortsLeft = maxForts;
		}

		if (faction != 0 && !off)
		{
			GameObject shipScreenGo = GameObject.Find("ShipScreen");
			if (shipScreenGo != null)
			{
				shipScreenGo.SetActive(false);
				Time.timeScale = 1f;
				off = true;
			}
		}
	}

	void OnApplicationQuit()
	{
		//gameInfo
		data.playerHealth = playerHealth;
		data.gameInfo[0] = seaLevel;
		data.gameInfo[1] = fortsDestroyed;
		data.gameInfo[2] = coins;
		data.gameInfo[3] = piratesKilled;
		data.gameInfo[4] = faction;
		data.gameInfo[5] = chestsOpened;
		data.gameInfo[6] = seasHighscore;
		data.gameInfo[7] = fortsLeft;

		//firstTimeObjs
		data.firstTimeObjs[0] = moveTutorialDone;
		data.firstTimeObjs[1] = shootTutorialDone;
		data.firstTimeObjs[2] = removeAds;

		//upgradeableStats
		data.upgradeableStats[0] = playerMoveSpeed;
		data.upgradeableStats[1] = playerCannonDamage;
		data.upgradeableStats[2] = maxHealth;
		data.upgradeableCannon = cannons;

		//shopVars
		data.shopVars[0] = speedBought;
		data.shopVars[1] = damageBought;
		data.shopVars[2] = healthBought;
		data.shopVars[3] = cannonsBought;
		data.shopVars[4] = incSpeedCost;
		data.shopVars[5] = incDamageCost;
		data.shopVars[6] = incHealthCost;
		data.shopVars[7] = incCannonsCost;

		SaveLoadManager.SaveGame(data);
	}

	void OnApplicationPause(bool pauseStatus)
	{
		if (pauseStatus)
		{
			//gameInfo
			data.playerHealth = playerHealth;
			data.gameInfo[0] = seaLevel;
			data.gameInfo[1] = fortsDestroyed;
			data.gameInfo[2] = coins;
			data.gameInfo[3] = piratesKilled;
			data.gameInfo[4] = faction;
			data.gameInfo[5] = chestsOpened;
			data.gameInfo[6] = seasHighscore;
			data.gameInfo[7] = fortsLeft;

			//firstTimeObjs
			data.firstTimeObjs[0] = moveTutorialDone;
			data.firstTimeObjs[1] = shootTutorialDone;
			data.firstTimeObjs[2] = removeAds;

			//upgradeableStats
			data.upgradeableStats[0] = playerMoveSpeed;
			data.upgradeableStats[1] = playerCannonDamage;
			data.upgradeableStats[2] = maxHealth;
			data.upgradeableCannon = cannons;

			//shopVars
			data.shopVars[0] = speedBought;
			data.shopVars[1] = damageBought;
			data.shopVars[2] = healthBought;
			data.shopVars[3] = cannonsBought;
			data.shopVars[4] = incSpeedCost;
			data.shopVars[5] = incDamageCost;
			data.shopVars[6] = incHealthCost;
			data.shopVars[7] = incCannonsCost;

			SaveLoadManager.SaveGame(data);
		}
	}

	/*public void UpdateHighscoreText ()
	{
		GameObject seasHighText = GameObject.Find("Canvas").transform.GetChild(5).gameObject;
		seasHighText.GetComponent<TextMeshProUGUI>().text = seasHighscore.ToString();
	}*/
}
