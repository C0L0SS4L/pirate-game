using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class PlayerBehavior : MonoBehaviour {

	public Text coinCounterText;
	public Image fireButton;

	private GameObject coinGO;
	public bool dead;

	private int coinCounter;

	//private float startHealth = 100;

	private float health;

	[Header ("Unity Stuff")]
	public Image healthBar;
	public GameObject canBallPrefab;
	public GameObject rCannon1;
	public GameObject lCannon1;
	public GameObject rCannon2;
	public GameObject lCannon2;
	public GameObject rCannon3;
	public GameObject lCannon3;
	public GameObject rCannon4;
	public GameObject lCannon4;
	public Transform DeathScreen;
	public GameObject seaTextGO;
	public GameObject tutorialManagerGO;
	public GameObject moveTutorial;
	public GameObject shootTutorial;
	public TextMeshProUGUI fortsText;
	public GameObject reviveScreen;
	public Image reviveCircle;

	[Header("Objects")]
	public Sprite sp_damagedPlayer;
	public Sprite sp_nearDeadPlayer;
	public Sprite sp_deadPlayer;
	public Sprite eng_damagedPlayer;
	public Sprite eng_nearDeadPlayer;
	public Sprite eng_deadPlayer;
	public Sprite fr_damagedPlayer;
	public Sprite fr_nearDeadPlayer;
	public Sprite fr_deadPlayer;
	public Sprite it_damagedPlayer;
	public Sprite it_nearDeadPlayer;
	public Sprite it_deadPlayer;
	public Sprite spainFac;
	public Sprite englandFac;
	public Sprite franceFac;
	public Sprite italyFac;
	public GameObject NotInAreaScreen;

	[Header ("Game Over Screen")]
	public GameObject gameOverScreenGO;
	public GameObject newBestGO;
	public TextMeshProUGUI seasClearedText;
	public TextMeshProUGUI piratesKilledText;
	public TextMeshProUGUI fortsDestroyedText;
	public TextMeshProUGUI coinsCollectedText;
	public TextMeshProUGUI chestsOpenedText;

	private float reloadTime = 2f;
	private float reloadCountdown;
	private float notInAreaCountdown = 10;
	private AudioManager audioManager;
	private bool startRegenTimer;
	private float regenCountdown;
	private float regenSpeed = 2f;
	private float reviveCountdown = 7f;
	private bool gameOverScreenOpen;


	public static bool notInPlayArea;
	//public static float maxHealth = 100;
	//public static int cannons = 1;

	

	void Awake () {
		GameManager.Instance.off = false;
		reloadCountdown = 2f;
		//GameManager.Instance.playerHealth = GameManager.Instance.maxHealth;
		coinCounterText.text = GameManager.Instance.coins.ToString();
		audioManager = FindObjectOfType<AudioManager>();

		if (GameManager.Instance.faction != 0)
		{
			if (GameManager.Instance.faction == 1)
			{
				GetComponent<SpriteRenderer>().sprite = spainFac;
			}
			else if (GameManager.Instance.faction == 2)
			{
				GetComponent<SpriteRenderer>().sprite = englandFac;
			}
			else if (GameManager.Instance.faction == 3)
			{
				GetComponent<SpriteRenderer>().sprite = franceFac;	
			}
			else if (GameManager.Instance.faction == 4)
			{
				GetComponent<SpriteRenderer>().sprite = italyFac;
			}
		}
	}

	void Update()
	{
		regenCountdown -= Time.deltaTime;
		if (notInPlayArea)
		{
			NotInPlayAreaDeathCount();
		}
		else
		{
			notInAreaCountdown = 10;
			NotInAreaScreen.SetActive(false);
		}
		if (GameManager.Instance.playerHealth > 0 && GameManager.Instance.playerHealth < GameManager.Instance.maxHealth && regenCountdown <= 0)
		{
			GameManager.Instance.playerHealth += Time.deltaTime * regenSpeed;
		}

		if (!dead)
		{
			healthBar.fillAmount = GameManager.Instance.playerHealth / GameManager.Instance.maxHealth;
		}
		

		reloadCountdown += Time.deltaTime;
		fireButton.fillAmount = reloadCountdown / reloadTime;

		if (GameManager.Instance.playerHealth <= (GameManager.Instance.maxHealth * 0.33333333f) && !dead)
		{
			if (GameManager.Instance.faction == 1)
			{
				GetComponent<SpriteRenderer>().sprite = sp_nearDeadPlayer;
			}
			else if (GameManager.Instance.faction == 2)
			{
				GetComponent<SpriteRenderer>().sprite = eng_nearDeadPlayer;
			}
			else if (GameManager.Instance.faction == 3)
			{
				GetComponent<SpriteRenderer>().sprite = fr_nearDeadPlayer;
			}
			else if (GameManager.Instance.faction == 4)
			{
				GetComponent<SpriteRenderer>().sprite = it_nearDeadPlayer;
			}
			
		}
		else if (GameManager.Instance.playerHealth <= (GameManager.Instance.maxHealth * 0.66666666f) && !dead)
		{
			if (GameManager.Instance.faction == 1)
			{
				GetComponent<SpriteRenderer>().sprite = sp_damagedPlayer;
			}
			else if (GameManager.Instance.faction == 2)
			{
				GetComponent<SpriteRenderer>().sprite = eng_damagedPlayer;
			}
			else if (GameManager.Instance.faction == 3)
			{
				GetComponent<SpriteRenderer>().sprite = fr_damagedPlayer;
			}
			else if (GameManager.Instance.faction == 4)
			{
				GetComponent<SpriteRenderer>().sprite = it_damagedPlayer;
			}
		}
		else if (GameManager.Instance.playerHealth > (GameManager.Instance.maxHealth * 0.66666666f) && !dead)
		{
			if (GameManager.Instance.faction == 1)
			{
				GetComponent<SpriteRenderer>().sprite = spainFac;
			}
			else if (GameManager.Instance.faction == 2)
			{
				GetComponent<SpriteRenderer>().sprite = englandFac;
			}
			else if (GameManager.Instance.faction == 3)
			{
				GetComponent<SpriteRenderer>().sprite = franceFac;	
			}
			else if (GameManager.Instance.faction == 4)
			{
				GetComponent<SpriteRenderer>().sprite = italyFac;
			}
		}

		if (Input.GetKeyDown(KeyCode.Space))
		{
			BothSideFire();
			RightSideFire();
			LeftSideFire();
		}

		/*if (transform.position == Vector3.zero && GameManager.Instance.firstTime && GameManager.Instance.faction != 0 && !moveTutorialDone)
		{

			StartCoroutine(MoveTutorialArrowBounce());
			
		}*/
		if (transform.position != Vector3.zero || GameManager.Instance.moveTutorialDone)
		{
			if (moveTutorial.activeInHierarchy)
			{
				moveTutorial.SetActive(false);
				GameManager.Instance.moveTutorialDone = true;
			}
		}

		if (!GameManager.Instance.shootTutorialDone)
		{
			if (transform.GetChild(0).GetComponent<PlayerCOF>().rEnemyInSight == true || transform.GetChild(1).GetComponent<PlayerCOF>().lEnemyInSight == true && !shootTutorial.activeInHierarchy)
			{
				Time.timeScale = 0.4f;
				shootTutorial.SetActive(true);
			}
		}
		else
		{
			if (shootTutorial.activeInHierarchy)
			{
				Time.timeScale = 1f;
				shootTutorial.SetActive(false);
			}
			
		}

		/*if (gameOverScreenGO.activeInHierarchy && AdManager.Instance.gOver_adDone)
		{
			AdManager.Instance.gOver_adDone = false;
			Invoke("ChangeSeaAfterAd", 0.15f);
		}*/

		if (dead && Advertisement.IsReady() && !Advertisement.isShowing && !gameOverScreenOpen)
		{
			//countdown
			reviveScreen.SetActive(true);

			reviveCountdown -= Time.deltaTime;
			reviveCircle.fillAmount = reviveCountdown / 7f;

		}
		else if (dead && !gameOverScreenOpen && !Advertisement.isShowing)
		{
			//gameOver Screen
			gameOverScreenOpen = true;
			reviveScreen.SetActive(false);
			StartOpeningGameOverScreen();
		}

		if (reviveCountdown <= 0 && !gameOverScreenOpen && !Advertisement.isShowing)
		{
			gameOverScreenOpen = true;
			reviveScreen.SetActive(false);
			StartOpeningGameOverScreen();
		}
		
	}

	private void LateUpdate() {
		fortsText.text = "Forts: " + GameManager.Instance.fortsOnScreen + "/" + GameManager.Instance.maxForts;
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Coin") {
			GameManager.Instance.coins += 1;
			coinCounterText.text = GameManager.Instance.coins.ToString();

			audioManager.Play("CoinPickup");

			coinGO = col.gameObject;

			Destroy(coinGO, 1.5f);
		}

		if (col.gameObject.tag == "10 Coin")
		{
			GameManager.Instance.coins += 10;
			coinCounterText.text = GameManager.Instance.coins.ToString();

			audioManager.Play("BigCoinPickup");

			transform.GetChild(0).GetComponent<PlayerCOF>().ResetEnemyInSight();
			transform.GetChild(1).GetComponent<PlayerCOF>().ResetEnemyInSight();

			coinGO = col.gameObject;

			Destroy(coinGO, 1.5f);
		}

		if (col.gameObject.tag == "20 Coin")
		{
			GameManager.Instance.coins += 20;
			coinCounterText.text = GameManager.Instance.coins.ToString();

			audioManager.Play("BigCoinPickup");

			transform.GetChild(0).GetComponent<PlayerCOF>().ResetEnemyInSight();
			transform.GetChild(1).GetComponent<PlayerCOF>().ResetEnemyInSight();

			coinGO = col.gameObject;

			Destroy(coinGO, 1.5f);
		}
	}

	public void TakeDamage (float amount)
	{
		GameManager.Instance.playerHealth -= amount;

		regenCountdown = 5;

		//healthBar.fillAmount = GameManager.Instance.playerHealth / maxHealth;

		audioManager.Play("ShipHit");

		if (GameManager.Instance.playerHealth <= 0) 
		{
			if (!dead)
			{
				Die();
			}
		}
		/*else if (GameManager.Instance.playerHealth <= (maxHealth * 0.33333333f))
		{
			if (GameManager.Instance.faction == 1)
			{
				GetComponent<SpriteRenderer>().sprite = sp_nearDeadPlayer;
			}
			else if (GameManager.Instance.faction == 2)
			{
				GetComponent<SpriteRenderer>().sprite = eng_nearDeadPlayer;
			}
			else if (GameManager.Instance.faction == 3)
			{
				GetComponent<SpriteRenderer>().sprite = fr_nearDeadPlayer;
			}
			else if (GameManager.Instance.faction == 4)
			{
				GetComponent<SpriteRenderer>().sprite = it_nearDeadPlayer;
			}
			
		}
		else if (GameManager.Instance.playerHealth <= (maxHealth * 0.66666666f))
		{
			if (GameManager.Instance.faction == 1)
			{
				GetComponent<SpriteRenderer>().sprite = sp_damagedPlayer;
			}
			else if (GameManager.Instance.faction == 2)
			{
				GetComponent<SpriteRenderer>().sprite = eng_damagedPlayer;
			}
			else if (GameManager.Instance.faction == 3)
			{
				GetComponent<SpriteRenderer>().sprite = fr_damagedPlayer;
			}
			else if (GameManager.Instance.faction == 4)
			{
				GetComponent<SpriteRenderer>().sprite = it_damagedPlayer;
			}
		}*/
	}

	private void Die ()
	{
		dead = true;
		//gameObject.tag = "Dead";

		if (GameManager.Instance.faction == 1)
		{
			GetComponent<SpriteRenderer>().sprite = sp_deadPlayer;
		}
		else if (GameManager.Instance.faction == 2)
		{
			GetComponent<SpriteRenderer>().sprite = eng_deadPlayer;
		}
		else if (GameManager.Instance.faction == 3)
		{
			GetComponent<SpriteRenderer>().sprite = fr_deadPlayer;	
		}
		else if (GameManager.Instance.faction == 4)
		{
			GetComponent<SpriteRenderer>().sprite = it_deadPlayer;
		}

		healthBar.fillAmount = 0;

		/*int seasCleared = GameManager.Instance.seaLevel - 1;

		if (seasCleared > GameManager.Instance.seasHighscore)
		{
			GameManager.Instance.seasHighscore = seasCleared;
		}

		audioManager.Play("ShipDestroy");
		seasClearedText.text = "Seas cleared: " + seasCleared;
		piratesKilledText.text = "Pirates killed: " + GameManager.Instance.piratesKilled;
		fortsDestroyedText.text = "Forts destroyed: " + GameManager.Instance.fortsDestroyed;
		coinsCollectedText.text = "Coins collected: " + GameManager.Instance.coins;
		chestsOpenedText.text = "Chests opened: " + GameManager.Instance.chestsOpened;

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

		StartCoroutine(OpenGameOverScreen());*/
	}

	public void BothSideFire ()
	{
		if (transform.GetChild(0).GetComponent<PlayerCOF>().rEnemyInSight == true && transform.GetChild(1).GetComponent<PlayerCOF>().lEnemyInSight == true && reloadCountdown >= reloadTime && !dead)
		{
			regenCountdown = 5;
			GameManager.Instance.shootTutorialDone = true;

			if (GameManager.Instance.cannons == 1)
			{
				Vector2 newCanBallPos1 = new Vector2(rCannon1.transform.position.x, rCannon1.transform.position.y);

				Instantiate(canBallPrefab, newCanBallPos1, gameObject.transform.GetChild(2).transform.rotation);

				Vector2 newCanBallPos2 = new Vector2(lCannon1.transform.position.x, lCannon1.transform.position.y);

				Instantiate(canBallPrefab, newCanBallPos2, gameObject.transform.GetChild(3).transform.rotation);

				audioManager.Play("CannonFire");

				reloadCountdown = 0;				
			}
			else if (GameManager.Instance.cannons == 2)
			{
				Vector2 newCanBallPos1 = new Vector2(rCannon1.transform.position.x, rCannon1.transform.position.y);

				Instantiate(canBallPrefab, newCanBallPos1, gameObject.transform.GetChild(2).transform.rotation);

				Vector2 newCanBallPos2 = new Vector2(lCannon1.transform.position.x, lCannon1.transform.position.y);

				Instantiate(canBallPrefab, newCanBallPos2, gameObject.transform.GetChild(3).transform.rotation);



				Vector2 newCanBallPos3 = new Vector2(rCannon2.transform.position.x, rCannon2.transform.position.y);

				Instantiate(canBallPrefab, newCanBallPos3, gameObject.transform.GetChild(4).transform.rotation);

				Vector2 newCanBallPos4 = new Vector2(lCannon2.transform.position.x, lCannon2.transform.position.y);

				Instantiate(canBallPrefab, newCanBallPos4, gameObject.transform.GetChild(5).transform.rotation);

				audioManager.Play("CannonFire");

				reloadCountdown = 0;	
			}
			else if (GameManager.Instance.cannons == 3)
			{
				Vector2 newCanBallPos1 = new Vector2(rCannon1.transform.position.x, rCannon1.transform.position.y);

				Instantiate(canBallPrefab, newCanBallPos1, gameObject.transform.GetChild(2).transform.rotation);

				Vector2 newCanBallPos2 = new Vector2(lCannon1.transform.position.x, lCannon1.transform.position.y);

				Instantiate(canBallPrefab, newCanBallPos2, gameObject.transform.GetChild(3).transform.rotation);



				Vector2 newCanBallPos3 = new Vector2(rCannon2.transform.position.x, rCannon2.transform.position.y);

				Instantiate(canBallPrefab, newCanBallPos3, gameObject.transform.GetChild(4).transform.rotation);

				Vector2 newCanBallPos4 = new Vector2(lCannon2.transform.position.x, lCannon2.transform.position.y);

				Instantiate(canBallPrefab, newCanBallPos4, gameObject.transform.GetChild(5).transform.rotation);


				Vector2 newCanBallPos5 = new Vector2(rCannon3.transform.position.x, rCannon3.transform.position.y);

				Instantiate(canBallPrefab, newCanBallPos5, gameObject.transform.GetChild(6).transform.rotation);

				Vector2 newCanBallPos6 = new Vector2(lCannon3.transform.position.x, lCannon3.transform.position.y);

				Instantiate(canBallPrefab, newCanBallPos6, gameObject.transform.GetChild(7).transform.rotation);


				audioManager.Play("CannonFire");

				reloadCountdown = 0;
			}
			else if (GameManager.Instance.cannons == 4)
			{
				Vector2 newCanBallPos1 = new Vector2(rCannon1.transform.position.x, rCannon1.transform.position.y);

				Instantiate(canBallPrefab, newCanBallPos1, gameObject.transform.GetChild(2).transform.rotation);

				Vector2 newCanBallPos2 = new Vector2(lCannon1.transform.position.x, lCannon1.transform.position.y);

				Instantiate(canBallPrefab, newCanBallPos2, gameObject.transform.GetChild(3).transform.rotation);



				Vector2 newCanBallPos3 = new Vector2(rCannon2.transform.position.x, rCannon2.transform.position.y);

				Instantiate(canBallPrefab, newCanBallPos3, gameObject.transform.GetChild(4).transform.rotation);

				Vector2 newCanBallPos4 = new Vector2(lCannon2.transform.position.x, lCannon2.transform.position.y);

				Instantiate(canBallPrefab, newCanBallPos4, gameObject.transform.GetChild(5).transform.rotation);


				Vector2 newCanBallPos5 = new Vector2(rCannon3.transform.position.x, rCannon3.transform.position.y);

				Instantiate(canBallPrefab, newCanBallPos5, gameObject.transform.GetChild(6).transform.rotation);

				Vector2 newCanBallPos6 = new Vector2(lCannon3.transform.position.x, lCannon3.transform.position.y);

				Instantiate(canBallPrefab, newCanBallPos6, gameObject.transform.GetChild(7).transform.rotation);


				Vector2 newCanBallPos7 = new Vector2(rCannon4.transform.position.x, rCannon4.transform.position.y);

				Instantiate(canBallPrefab, newCanBallPos7, gameObject.transform.GetChild(8).transform.rotation);

				Vector2 newCanBallPos8 = new Vector2(lCannon4.transform.position.x, lCannon4.transform.position.y);

				Instantiate(canBallPrefab, newCanBallPos8, gameObject.transform.GetChild(9).transform.rotation);


				audioManager.Play("CannonFire");

				reloadCountdown = 0;
			}
		}
	}

	public void RightSideFire ()
	{
		regenCountdown = 5;
		GameManager.Instance.shootTutorialDone = true;

		if (transform.GetChild(0).GetComponent<PlayerCOF>().rEnemyInSight == true && reloadCountdown >= reloadTime && !dead)
		{
			if (GameManager.Instance.cannons == 1)
			{
				Vector2 newCanBallPos = new Vector2(rCannon1.transform.position.x, rCannon1.transform.position.y);

				Instantiate(canBallPrefab, newCanBallPos, gameObject.transform.GetChild(2).transform.rotation);

				audioManager.Play("CannonFire");

				//canBallPrefab.GetComponent<CannonFire>().rSideCannonball = true;

				reloadCountdown = 0;				
			}
			else if (GameManager.Instance.cannons == 2)
			{
				Vector2 newCanBallPos = new Vector2(rCannon1.transform.position.x, rCannon1.transform.position.y);

				Instantiate(canBallPrefab, newCanBallPos, gameObject.transform.GetChild(2).transform.rotation);

				//canBallPrefab.GetComponent<CannonFire>().rSideCannonball = true;


				Vector2 newCanBallPos2 = new Vector2(rCannon2.transform.position.x, rCannon2.transform.position.y);

				Instantiate(canBallPrefab, newCanBallPos2, gameObject.transform.GetChild(4).transform.rotation);

				audioManager.Play("CannonFire");

				//canBallPrefab.GetComponent<CannonFire>().rSideCannonball = true;

				reloadCountdown = 0;
			}
			else if (GameManager.Instance.cannons == 3)
			{
				Vector2 newCanBallPos = new Vector2(rCannon1.transform.position.x, rCannon1.transform.position.y);

				Instantiate(canBallPrefab, newCanBallPos, gameObject.transform.GetChild(2).transform.rotation);

				//canBallPrefab.GetComponent<CannonFire>().rSideCannonball = true;


				Vector2 newCanBallPos2 = new Vector2(rCannon2.transform.position.x, rCannon2.transform.position.y);

				Instantiate(canBallPrefab, newCanBallPos2, gameObject.transform.GetChild(4).transform.rotation);


				Vector2 newCanBallPos3 = new Vector2(rCannon3.transform.position.x, rCannon3.transform.position.y);

				Instantiate(canBallPrefab, newCanBallPos3, gameObject.transform.GetChild(6).transform.rotation);

				audioManager.Play("CannonFire");

				//canBallPrefab.GetComponent<CannonFire>().rSideCannonball = true;

				reloadCountdown = 0;
			}
			else if (GameManager.Instance.cannons == 4)
			{
				Vector2 newCanBallPos = new Vector2(rCannon1.transform.position.x, rCannon1.transform.position.y);

				Instantiate(canBallPrefab, newCanBallPos, gameObject.transform.GetChild(2).transform.rotation);

				//canBallPrefab.GetComponent<CannonFire>().rSideCannonball = true;


				Vector2 newCanBallPos2 = new Vector2(rCannon2.transform.position.x, rCannon2.transform.position.y);

				Instantiate(canBallPrefab, newCanBallPos2, gameObject.transform.GetChild(4).transform.rotation);


				Vector2 newCanBallPos3 = new Vector2(rCannon3.transform.position.x, rCannon3.transform.position.y);

				Instantiate(canBallPrefab, newCanBallPos3, gameObject.transform.GetChild(6).transform.rotation);

				Vector2 newCanBallPos4 = new Vector2(rCannon4.transform.position.x, rCannon4.transform.position.y);

				Instantiate(canBallPrefab, newCanBallPos4, gameObject.transform.GetChild(8).transform.rotation);

				audioManager.Play("CannonFire");

				//canBallPrefab.GetComponent<CannonFire>().rSideCannonball = true;

				reloadCountdown = 0;
			}
		}

	}

	public void LeftSideFire ()
	{
		regenCountdown = 5;
		GameManager.Instance.shootTutorialDone = true;
		
		if (transform.GetChild(1).GetComponent<PlayerCOF>().lEnemyInSight == true && reloadCountdown >= reloadTime && !dead)
		{

			if (GameManager.Instance.cannons == 1)
			{
				Vector2 newCanBallPos = new Vector2(lCannon1.transform.position.x, lCannon1.transform.position.y);

				Instantiate(canBallPrefab, newCanBallPos, gameObject.transform.GetChild(3).transform.rotation);

				audioManager.Play("CannonFire");

				//canBallPrefab.GetComponent<CannonFire>().lSideCannonball = true;

				reloadCountdown = 0;			
			}
			else if (GameManager.Instance.cannons == 2)
			{
				Vector2 newCanBallPos = new Vector2(lCannon1.transform.position.x, lCannon1.transform.position.y);

				Instantiate(canBallPrefab, newCanBallPos, gameObject.transform.GetChild(3).transform.rotation);

				//canBallPrefab.GetComponent<CannonFire>().lSideCannonball = true;

				Vector2 newCanBallPos2 = new Vector2(lCannon2.transform.position.x, lCannon2.transform.position.y);

				Instantiate(canBallPrefab, newCanBallPos2, gameObject.transform.GetChild(5).transform.rotation);

				audioManager.Play("CannonFire");

				//canBallPrefab.GetComponent<CannonFire>().lSideCannonball = true;

				reloadCountdown = 0;
			}
			else if (GameManager.Instance.cannons == 3)
			{
				Vector2 newCanBallPos = new Vector2(lCannon1.transform.position.x, lCannon1.transform.position.y);

				Instantiate(canBallPrefab, newCanBallPos, gameObject.transform.GetChild(3).transform.rotation);

				//canBallPrefab.GetComponent<CannonFire>().lSideCannonball = true;

				Vector2 newCanBallPos2 = new Vector2(lCannon2.transform.position.x, lCannon2.transform.position.y);

				Instantiate(canBallPrefab, newCanBallPos2, gameObject.transform.GetChild(5).transform.rotation);

				//canBallPrefab.GetComponent<CannonFire>().lSideCannonball = true;

				Vector2 newCanBallPos3 = new Vector2(lCannon3.transform.position.x, lCannon3.transform.position.y);

				Instantiate(canBallPrefab, newCanBallPos3, gameObject.transform.GetChild(7).transform.rotation);

				audioManager.Play("CannonFire");

				reloadCountdown = 0;
			}
			else if (GameManager.Instance.cannons == 4)
			{
				Vector2 newCanBallPos = new Vector2(lCannon1.transform.position.x, lCannon1.transform.position.y);

				Instantiate(canBallPrefab, newCanBallPos, gameObject.transform.GetChild(3).transform.rotation);

				//canBallPrefab.GetComponent<CannonFire>().lSideCannonball = true;

				Vector2 newCanBallPos2 = new Vector2(lCannon2.transform.position.x, lCannon2.transform.position.y);

				Instantiate(canBallPrefab, newCanBallPos2, gameObject.transform.GetChild(5).transform.rotation);

				//canBallPrefab.GetComponent<CannonFire>().lSideCannonball = true;

				Vector2 newCanBallPos3 = new Vector2(lCannon3.transform.position.x, lCannon3.transform.position.y);

				Instantiate(canBallPrefab, newCanBallPos3, gameObject.transform.GetChild(7).transform.rotation);

				Vector2 newCanBallPos4 = new Vector2(lCannon4.transform.position.x, lCannon4.transform.position.y);

				Instantiate(canBallPrefab, newCanBallPos4, gameObject.transform.GetChild(9).transform.rotation);

				audioManager.Play("CannonFire");

				reloadCountdown = 0;
			}
		}

	}

	void NotInPlayAreaDeathCount ()
	{
		if (!dead)
		{	
			Image deathTimer = NotInAreaScreen.transform.GetChild(0).GetComponent<Image>();
			Text timerText = NotInAreaScreen.transform.GetChild(1).transform.GetChild(0).GetComponent<Text>();

			NotInAreaScreen.SetActive(true);
			
			notInAreaCountdown -= Time.deltaTime;
			deathTimer.fillAmount = notInAreaCountdown / 10;
			timerText.text = notInAreaCountdown.ToString();

			if (notInAreaCountdown <= 0 && !dead)
			{
				Die();
			}
		}
		else
		{
			Text timerText = NotInAreaScreen.transform.GetChild(1).transform.GetChild(0).GetComponent<Text>();
			notInAreaCountdown = 0;
			timerText.text = notInAreaCountdown.ToString();
		}
	}

	public void ChooseSpainFaction ()
	{
		FindObjectOfType<AudioManager>().Play("UI Click");
		seaTextGO.GetComponent<SeaTextScript>().StartFadeSeaText();
		GameManager.Instance.faction = 1;
		GetComponent<SpriteRenderer>().sprite = spainFac;
	}

	public void ChooseEnglandFaction ()
	{
		FindObjectOfType<AudioManager>().Play("UI Click");
		seaTextGO.GetComponent<SeaTextScript>().StartFadeSeaText();
		GameManager.Instance.faction = 2;
		GetComponent<SpriteRenderer>().sprite = englandFac;
	}

	public void ChooseFranceFaction ()
	{
		FindObjectOfType<AudioManager>().Play("UI Click");
		seaTextGO.GetComponent<SeaTextScript>().StartFadeSeaText();
		GameManager.Instance.faction = 3;
		GetComponent<SpriteRenderer>().sprite = franceFac;
	}

	public void ChooseItalyFaction ()
	{
		FindObjectOfType<AudioManager>().Play("UI Click");
		seaTextGO.GetComponent<SeaTextScript>().StartFadeSeaText();
		GameManager.Instance.faction = 4;
		GetComponent<SpriteRenderer>().sprite = italyFac;
	}

	private void StartOpeningGameOverScreen ()
	{
		int seasCleared = GameManager.Instance.seaLevel - 1;

		if (seasCleared > GameManager.Instance.seasHighscore)
		{
			GameManager.Instance.seasHighscore = seasCleared;
		}

		audioManager.Play("ShipDestroy");
		seasClearedText.text = "Seas cleared: " + seasCleared;
		piratesKilledText.text = "Pirates killed: " + GameManager.Instance.piratesKilled;
		fortsDestroyedText.text = "Forts destroyed: " + GameManager.Instance.fortsDestroyed;
		coinsCollectedText.text = "Coins collected: " + GameManager.Instance.coins;
		chestsOpenedText.text = "Chests opened: " + GameManager.Instance.chestsOpened;

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
		GameManager.Instance.speedBought = 0;
		GameManager.Instance.damageBought = 0;
		GameManager.Instance.healthBought = 0;
		GameManager.Instance.cannonsBought = 0;

		GameManager.Instance.incSpeedCost = 75;
		GameManager.Instance.incDamageCost = 125;
		GameManager.Instance.incHealthCost = 200;
		GameManager.Instance.incCannonsCost = 500;

		StartCoroutine(OpenGameOverScreen());
	}

	IEnumerator OpenGameOverScreen ()
	{
		yield return new WaitForSeconds(0.3f);

		NotInAreaScreen.SetActive(false);
		gameOverScreenGO.SetActive(true);

		int seasCleared = GameManager.Instance.seaLevel - 1;
		
		if (seasCleared >= GameManager.Instance.seasHighscore && seasCleared != 0)
		{
			newBestGO.SetActive(true);
		}
		else
		{
			print("Less than");
			print(seasCleared);
			newBestGO.SetActive(false);
		}

		Time.timeScale = 0f;
		GameManager.Instance.seaLevel = 1;
		GameManager.Instance.playerHealth = 100;
		healthBar.fillAmount = 0;
	}

	IEnumerator MoveTutorialArrowBounce ()
	{
		//tutorialManagerGO.transform.Find("TutorialPointerArrow").position = new Vector2 ()
		Animator anim = tutorialManagerGO.GetComponent<Animator>();

		yield return new WaitForSeconds(2f);

		anim.Play("ArrowBounce");

		yield return new WaitForSeconds(2f);
	}
}
