using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ShopManager : MonoBehaviour {

	//public static ShopManager Instance {get; private set;}

	//public vars

	[Header("GameObjects")]
	public GameObject shopScreen;
	public GameObject shopButton;
	public GameObject speedButton;
	public GameObject damageButton;
	public GameObject healthButton;
	public GameObject cannonsButton;
	public GameObject confirmBuyBkg;
	public GameObject confirmSpeed;
	public GameObject confirmDamage;
	public GameObject confirmHealth;
	public GameObject confirmCannons;

	[Header("Text")]
	public TextMeshProUGUI incSpeedText;
	public TextMeshProUGUI incDamageText;
	public TextMeshProUGUI incHealthText;
	public TextMeshProUGUI incCannonsText;
	public Text coinCounterText;

	//private vars

	/*private int speedBought;
	private int damageBought;
	private int healthBought;
	private int cannonsBought;

	private int incSpeedCost = 100;
	private int incDamageCost = 150;
	private int incHealthCost = 250;
	private int incCannonsCost = 500;*/
	private bool inShop;
	private AudioManager audioManager;

	private void Awake ()
	{
		/*if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else 
		{
			Destroy(gameObject);
		} */
		audioManager = FindObjectOfType<AudioManager>();
	}

	void Start () {
		
	}
	
	void Update () {
		//Find ShopScreen Obj ---
		/*if (SceneManager.GetActiveScene().name != "HomeScreen" && SceneManager.GetActiveScene().name != "Loader" )
		{
			if (shopScreen == null)
			{
				shopScreen = GameObject.FindWithTag("ShopScreen");	
			}
		}*/
		if (inShop)
		{
			if (GameManager.Instance.speedBought >= 10 || GameManager.Instance.coins < GameManager.Instance.incSpeedCost)
			{
				if (GameManager.Instance.speedBought >= 10)
				{
					speedButton.transform.Find("S_MAXText").gameObject.SetActive(true);
				}
				speedButton.GetComponent<Button>().interactable = false;
			}
			else
			{
				speedButton.GetComponent<Button>().interactable = true;
			}

			if (GameManager.Instance.damageBought >= 10 || GameManager.Instance.coins < GameManager.Instance.incDamageCost)
			{
				if (GameManager.Instance.damageBought >= 10)
				{
					damageButton.transform.Find("D_MAXText").gameObject.SetActive(true);
				}
				damageButton.GetComponent<Button>().interactable = false;
			}
			else
			{
				damageButton.GetComponent<Button>().interactable = true;
			}

			if (GameManager.Instance.healthBought >= 10 || GameManager.Instance.coins < GameManager.Instance.incHealthCost)
			{
				if (GameManager.Instance.healthBought >= 10)
				{
					healthButton.transform.Find("H_MAXText").gameObject.SetActive(true);
				}
				healthButton.GetComponent<Button>().interactable = false;
			}
			else
			{
				healthButton.GetComponent<Button>().interactable = true;
			}

			if (GameManager.Instance.cannonsBought >= 3 || GameManager.Instance.coins < GameManager.Instance.incCannonsCost)
			{
				if (GameManager.Instance.cannonsBought >= 3)
				{
					cannonsButton.transform.Find("C_MAXText").gameObject.SetActive(true);
				}
				cannonsButton.GetComponent<Button>().interactable = false;
			}
			else
			{
				cannonsButton.GetComponent<Button>().interactable = true;
			}
		}
	}

	public void OpenShop ()
	{
		inShop = true;
		audioManager.Play("UI Click");
		Time.timeScale = 0f;
		shopButton.SetActive(false);
		incSpeedText.text = GameManager.Instance.incSpeedCost.ToString();
		incDamageText.text = GameManager.Instance.incDamageCost.ToString();
		incHealthText.text = GameManager.Instance.incHealthCost.ToString();
		incCannonsText.text = GameManager.Instance.incCannonsCost.ToString();

		/*if (speedBought >= 10 || GameManager.Instance.coins < incSpeedCost)
		{
			speedButton.GetComponent<Button>().interactable = false;
		}
		else
		{
			speedButton.GetComponent<Button>().interactable = true;
		}

		if (damageBought >= 10 || GameManager.Instance.coins < incDamageCost)
		{
			damageButton.GetComponent<Button>().interactable = false;
		}
		else
		{
			damageButton.GetComponent<Button>().interactable = true;
		}

		if (healthBought >= 10 || GameManager.Instance.coins < incHealthCost)
		{
			healthButton.GetComponent<Button>().interactable = false;
		}
		else
		{
			healthButton.GetComponent<Button>().interactable = true;
		}

		if (cannonsBought >= 3 || GameManager.Instance.coins < incCannonsCost)
		{
			cannonsButton.GetComponent<Button>().interactable = false;
		}
		else
		{
			cannonsButton.GetComponent<Button>().interactable = true;
		}*/

		shopScreen.SetActive(true);
	}

	public void CloseShop ()
	{
		inShop = false;
		audioManager.Play("UI Click");
		Time.timeScale = 1f;
		confirmSpeed.SetActive(false);
		confirmDamage.SetActive(false);
		confirmHealth.SetActive(false);
		confirmCannons.SetActive(false);
		confirmBuyBkg.SetActive(false);
		shopScreen.SetActive(false);
		shopButton.SetActive(true);
	}

	public void ConfirmSpeedBuy ()
	{
		confirmBuyBkg.SetActive(true);
		
		TextMeshProUGUI confirmPrice = confirmSpeed.transform.Find("ConfirmCostText").GetComponent<TextMeshProUGUI>();
		confirmPrice.text = GameManager.Instance.incSpeedCost.ToString();

		confirmSpeed.SetActive(true);
		audioManager.Play("UI Click");
	}

	public void ConfirmDamageBuy ()
	{
		confirmBuyBkg.SetActive(true);

		TextMeshProUGUI confirmPrice = confirmDamage.transform.Find("ConfirmCostText").GetComponent<TextMeshProUGUI>();
		confirmPrice.text = GameManager.Instance.incDamageCost.ToString();

		confirmDamage.SetActive(true);
		audioManager.Play("UI Click");
	}

	public void ConfirmHealthBuy ()
	{
		confirmBuyBkg.SetActive(true);

		TextMeshProUGUI confirmPrice = confirmHealth.transform.Find("ConfirmCostText").GetComponent<TextMeshProUGUI>();
		confirmPrice.text = GameManager.Instance.incHealthCost.ToString();

		confirmHealth.SetActive(true);
		audioManager.Play("UI Click");
	}

	public void ConfirmCannonsBuy ()
	{
		confirmBuyBkg.SetActive(true);

		TextMeshProUGUI confirmPrice = confirmCannons.transform.Find("ConfirmCostText").GetComponent<TextMeshProUGUI>();
		confirmPrice.text = GameManager.Instance.incCannonsCost.ToString();

		confirmCannons.SetActive(true);
		audioManager.Play("UI Click");
	}

	public void CloseConfirm ()
	{
		if (EventSystem.current.currentSelectedGameObject.transform.parent.name == "ConfirmSpeedBuy")
		{
			confirmSpeed.SetActive(false);
			confirmBuyBkg.SetActive(false);
			audioManager.Play("UI Click");
		}

		if (EventSystem.current.currentSelectedGameObject.transform.parent.name == "ConfirmDamageBuy")
		{
			confirmDamage.SetActive(false);
			confirmBuyBkg.SetActive(false);
			audioManager.Play("UI Click");
		}

		if (EventSystem.current.currentSelectedGameObject.transform.parent.name == "ConfirmHealthBuy")
		{
			confirmHealth.SetActive(false);
			confirmBuyBkg.SetActive(false);
			audioManager.Play("UI Click");
		}

		if (EventSystem.current.currentSelectedGameObject.transform.parent.name == "ConfirmCannonsBuy")
		{
			confirmCannons.SetActive(false);
			confirmBuyBkg.SetActive(false);
			audioManager.Play("UI Click");
		}
	}

	public void BuyIncSpeed ()
	{
		audioManager.Play("UI Click");
		if (GameManager.Instance.speedBought < 10 && GameManager.Instance.coins >= GameManager.Instance.incSpeedCost)
		{
			Debug.Log("Bought");
			//PlayerMovement.moveSpeed += 1f;
			GameManager.Instance.playerMoveSpeed += 1f;
			GameManager.Instance.coins -= GameManager.Instance.incSpeedCost;
			coinCounterText.text = GameManager.Instance.coins.ToString();
			GameManager.Instance.incSpeedCost += 75;
			incSpeedText.text = GameManager.Instance.incSpeedCost.ToString();
			GameManager.Instance.speedBought++;
			audioManager.Play("BuyItem");
			confirmSpeed.SetActive(false);
			confirmBuyBkg.SetActive(false);

			if (GameManager.Instance.speedBought >= 10)
			{
				GameManager.Instance.incSpeedCost -= 75;
				incSpeedText.text = GameManager.Instance.incSpeedCost.ToString();
			}
		}
		
	}

	public void BuyIncDamage ()
	{
		audioManager.Play("UI Click");
		if (GameManager.Instance.damageBought < 10 && GameManager.Instance.coins >= GameManager.Instance.incDamageCost)
		{
			Debug.Log("Bought");
			GameManager.Instance.playerCannonDamage += 1;
			GameManager.Instance.coins -= GameManager.Instance.incDamageCost;
			coinCounterText.text = GameManager.Instance.coins.ToString();
			GameManager.Instance.incDamageCost += 125;
			incDamageText.text = GameManager.Instance.incDamageCost.ToString();
			GameManager.Instance.damageBought++;
			audioManager.Play("BuyItem");
			confirmDamage.SetActive(false);
			confirmBuyBkg.SetActive(false);

			if (GameManager.Instance.damageBought >= 10)
			{
				GameManager.Instance.incDamageCost -= 125;
				incDamageText.text = GameManager.Instance.incDamageCost.ToString();
			}
		}
		/*else
		{
			incDamageCost -= 150;
			incDamageText.text = incDamageCost.ToString();

			GameObject buttonGO = EventSystem.current.currentSelectedGameObject;

			buttonGO.GetComponent<Button>().interactable = false;
			Debug.LogWarning("Not enough coins: " + incDamageCost);
		}*/
	}

	public void BuyIncHealth ()
	{
		audioManager.Play("UI Click");
		if (GameManager.Instance.healthBought < 10 && GameManager.Instance.coins >= GameManager.Instance.incHealthCost)
		{
			Debug.Log("Bought");
			GameManager.Instance.maxHealth += 10;
			GameManager.Instance.coins -= GameManager.Instance.incHealthCost;
			coinCounterText.text = GameManager.Instance.coins.ToString();
			GameManager.Instance.incHealthCost += 200;
			incHealthText.text = GameManager.Instance.incHealthCost.ToString();
			GameManager.Instance.healthBought++;
			audioManager.Play("BuyItem");
			confirmHealth.SetActive(false);
			confirmBuyBkg.SetActive(false);

			if (GameManager.Instance.healthBought >= 10)
			{
				GameManager.Instance.incHealthCost -= 200;
				incHealthText.text = GameManager.Instance.incHealthCost.ToString();
			}
		}
		/*else
		{
			incHealthCost -= 250;
			incHealthText.text = incHealthCost.ToString();

			GameObject buttonGO = EventSystem.current.currentSelectedGameObject;

			buttonGO.GetComponent<Button>().interactable = false;
			Debug.LogWarning("Not enough coins: " + incHealthCost);
		}*/
	}

	public void BuyIncCannons ()
	{
		audioManager.Play("UI Click");
		if (GameManager.Instance.cannonsBought < 3 && GameManager.Instance.coins >= GameManager.Instance.incCannonsCost)
		{
			Debug.Log("Bought Extra Cannon");
			GameManager.Instance.cannons += 1;
			GameManager.Instance.coins -= GameManager.Instance.incCannonsCost;
			coinCounterText.text = GameManager.Instance.coins.ToString();
			GameManager.Instance.incCannonsCost += 500;
			incCannonsText.text = GameManager.Instance.incCannonsCost.ToString();
			GameManager.Instance.cannonsBought++;
			audioManager.Play("BuyItem");
			confirmCannons.SetActive(false);
			confirmBuyBkg.SetActive(false);

			if (GameManager.Instance.cannonsBought >= 3)
			{
				GameManager.Instance.incCannonsCost -= 500;
				incCannonsText.text = GameManager.Instance.incCannonsCost.ToString();
			}
		}
		/*else
		{
			incCannonsCost -= 500;
			incCannonsText.text = incCannonsCost.ToString();

			GameObject buttonGO = EventSystem.current.currentSelectedGameObject;

			buttonGO.GetComponent<Button>().interactable = false;
			Debug.LogWarning("Not enough coins: " + incCannonsCost);
		}*/
	}
}
