using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FortAI : MonoBehaviour {

	//private enum CannonBallState {Moving, Collided};

	[SerializeField] private GameObject canBallPrefab;
	[SerializeField] private GameObject explosionPrefab;
	[SerializeField] private Transform treasureChest;
	[SerializeField] private Sprite d_horz_wall;
	[SerializeField] private Sprite d_vert_wall;
	[SerializeField] private Sprite vd_horz_wall;
	[SerializeField] private Sprite vd_vert_wall;

	public float fireRate = 1f;
	public Image healthBar;

	private float fireCountdown = 0f;
	private float explosionCountdown = 0f;
	private float startHealth = 150;
	private AudioManager audioManager;
	//private GameObject rPlayerCOF;
	//private GameObject lPlayerCOF;

	public float health;
	public bool dead;

	void Awake()
	{
		healthBar.gameObject.transform.parent.gameObject.transform.parent.gameObject.SetActive(false);
		health = startHealth;	
		audioManager = FindObjectOfType<AudioManager>();
		/*if (SceneManager.GetActiveScene().name == "Game")
		{
			fortsText = GameObject.FindWithTag("FortsText").GetComponent<TextMeshProUGUI>();
			fortsText.text = "Forts: " + GameManager.Instance.fortsOnScreen + "/" + GameManager.Instance.maxForts;
		}*/
		

		//rPlayerCOF = GameObject.Find("Player").transform.Find("r_cof").gameObject;
		//lPlayerCOF = GameObject.Find("Player").transform.Find("r_cof").gameObject;
	}

	void Update () {
		CannonAI();
		fireCountdown -= Time.deltaTime;
		explosionCountdown -= Time.deltaTime;
	}

	public void TakeDamage (float amount)
	{
		healthBar.gameObject.transform.parent.gameObject.transform.parent.gameObject.SetActive(true);
		health -= amount;

		healthBar.fillAmount = health / startHealth;
		if (health > 0)
		{
			audioManager.Play("FortHit");
		}

		if (health <= 100) {
			if (transform.parent.name == "Fort1") 
			{
				transform.parent.GetChild(5).GetComponent<SpriteRenderer>().sprite = d_vert_wall;
				transform.parent.GetChild(6).GetComponent<SpriteRenderer>().sprite = d_vert_wall;
			} 
			else if (transform.parent.name == "Fort2") 
			{
				transform.parent.GetChild(3).GetComponent<SpriteRenderer>().sprite = d_horz_wall;
				transform.parent.GetChild(4).GetComponent<SpriteRenderer>().sprite = d_horz_wall;
				transform.parent.GetChild(5).GetComponent<SpriteRenderer>().sprite = d_horz_wall;
				transform.parent.GetChild(6).GetComponent<SpriteRenderer>().sprite = d_vert_wall;
				transform.parent.GetChild(7).GetComponent<SpriteRenderer>().sprite = d_vert_wall;
			}
			else if (transform.parent.name == "Fort3")
			{
				transform.parent.GetChild(0).GetComponent<SpriteRenderer>().sprite = d_vert_wall;
				transform.parent.GetChild(1).GetComponent<SpriteRenderer>().sprite = d_horz_wall;
				transform.parent.GetChild(2).GetComponent<SpriteRenderer>().sprite = d_horz_wall;
			}
			else if (transform.parent.name == "Fort4")
			{
				transform.parent.GetChild(0).GetComponent<SpriteRenderer>().sprite = d_vert_wall;
				transform.parent.GetChild(1).GetComponent<SpriteRenderer>().sprite = d_horz_wall;
				transform.parent.GetChild(2).GetComponent<SpriteRenderer>().sprite = d_horz_wall;
			}
			else if (transform.parent.name == "Fort5")
			{
				transform.parent.GetChild(8).GetComponent<SpriteRenderer>().sprite = d_vert_wall;
				transform.parent.GetChild(9).GetComponent<SpriteRenderer>().sprite = d_vert_wall;
				transform.parent.GetChild(10).GetComponent<SpriteRenderer>().sprite = d_horz_wall;
				transform.parent.GetChild(11).GetComponent<SpriteRenderer>().sprite = d_horz_wall;
				transform.parent.GetChild(12).GetComponent<SpriteRenderer>().sprite = d_horz_wall;
				transform.parent.GetChild(13).GetComponent<SpriteRenderer>().sprite = d_horz_wall;
				transform.parent.GetChild(14).GetComponent<SpriteRenderer>().sprite = d_horz_wall;
			}
			else if (transform.parent.name == "Fort6")
			{
				transform.parent.GetChild(0).GetComponent<SpriteRenderer>().sprite = d_vert_wall;
				transform.parent.GetChild(1).GetComponent<SpriteRenderer>().sprite = d_vert_wall;
				transform.parent.GetChild(2).GetComponent<SpriteRenderer>().sprite = d_vert_wall;
				transform.parent.GetChild(3).GetComponent<SpriteRenderer>().sprite = d_horz_wall;
				transform.parent.GetChild(4).GetComponent<SpriteRenderer>().sprite = d_horz_wall;
			}
			else if (transform.parent.name == "Fort7")
			{
				transform.parent.GetChild(0).GetComponent<SpriteRenderer>().sprite = d_vert_wall;
				transform.parent.GetChild(1).GetComponent<SpriteRenderer>().sprite = d_horz_wall;
			}
		}

		if (health <= 50) {
			if (transform.parent.name == "Fort1") 
			{
				transform.parent.GetChild(5).GetComponent<SpriteRenderer>().sprite = vd_vert_wall;
				transform.parent.GetChild(6).GetComponent<SpriteRenderer>().sprite = vd_vert_wall;
			} 
			else if (transform.parent.name == "Fort2") 
			{
				transform.parent.GetChild(3).GetComponent<SpriteRenderer>().sprite = vd_horz_wall;
				transform.parent.GetChild(4).GetComponent<SpriteRenderer>().sprite = vd_horz_wall;
				transform.parent.GetChild(5).GetComponent<SpriteRenderer>().sprite = vd_horz_wall;
				transform.parent.GetChild(6).GetComponent<SpriteRenderer>().sprite = vd_vert_wall;
				transform.parent.GetChild(7).GetComponent<SpriteRenderer>().sprite = vd_vert_wall;
			}
			else if (transform.parent.name == "Fort3")
			{
				transform.parent.GetChild(0).GetComponent<SpriteRenderer>().sprite = vd_vert_wall;
				transform.parent.GetChild(1).GetComponent<SpriteRenderer>().sprite = vd_horz_wall;
				transform.parent.GetChild(2).GetComponent<SpriteRenderer>().sprite = vd_horz_wall;
			}
			else if (transform.parent.name == "Fort4")
			{
				transform.parent.GetChild(0).GetComponent<SpriteRenderer>().sprite = vd_vert_wall;
				transform.parent.GetChild(1).GetComponent<SpriteRenderer>().sprite = vd_horz_wall;
				transform.parent.GetChild(2).GetComponent<SpriteRenderer>().sprite = vd_horz_wall;
			}
			else if (transform.parent.name == "Fort5")
			{
				transform.parent.GetChild(8).GetComponent<SpriteRenderer>().sprite = vd_vert_wall;
				transform.parent.GetChild(9).GetComponent<SpriteRenderer>().sprite = vd_vert_wall;
				transform.parent.GetChild(10).GetComponent<SpriteRenderer>().sprite = vd_horz_wall;
				transform.parent.GetChild(11).GetComponent<SpriteRenderer>().sprite = vd_horz_wall;
				transform.parent.GetChild(12).GetComponent<SpriteRenderer>().sprite = vd_horz_wall;
				transform.parent.GetChild(13).GetComponent<SpriteRenderer>().sprite = vd_horz_wall;
				transform.parent.GetChild(14).GetComponent<SpriteRenderer>().sprite = vd_horz_wall;
			}
			else if (transform.parent.name == "Fort6")
			{
				transform.parent.GetChild(0).GetComponent<SpriteRenderer>().sprite = vd_vert_wall;
				transform.parent.GetChild(1).GetComponent<SpriteRenderer>().sprite = vd_vert_wall;
				transform.parent.GetChild(2).GetComponent<SpriteRenderer>().sprite = vd_vert_wall;
				transform.parent.GetChild(3).GetComponent<SpriteRenderer>().sprite = vd_horz_wall;
				transform.parent.GetChild(4).GetComponent<SpriteRenderer>().sprite = vd_horz_wall;
			}
			else if (transform.parent.name == "Fort7")
			{
				transform.parent.GetChild(0).GetComponent<SpriteRenderer>().sprite = vd_vert_wall;
				transform.parent.GetChild(1).GetComponent<SpriteRenderer>().sprite = vd_horz_wall;
			}
		}

		if (health <= 0) 
		{
			Die();
		}
	}

	void Die ()
	{
		/*int i = 0;
		while (i < 11)
		{
			if (explosionCountdown <= 0f)
			{
				
				float randomX = Random.Range(-18.5f,-15.5f);
				float randomY = Random.Range(-8.5f,-5.5f);
				Vector2 randomPos = new Vector2 (randomX, randomY);
				Instantiate(explosionPrefab,randomPos,Quaternion.identity);

				explosionCountdown = 0.25f / 1;

				i++;			
				

			} else {
				yield return null;
			}
		} */

		dead = true;
		//transform.parent.tag = "Dead";
		FindObjectOfType<AudioManager>().Play("FortDestroy");

		ChestBehavior tcScript = treasureChest.GetComponent<ChestBehavior>();
		tcScript.FortDestroyed(gameObject.transform.parent.transform);

		//rPlayerCOF.GetComponent<PlayerCOF>().rEnemyInSight = false;
		//lPlayerCOF.GetComponent<PlayerCOF>().lEnemyInSight = false;

		//transform.parent.GetComponent<SimpleFortScript>().SubFortsOnScreen();
		//fortsText.text = "Forts: " + GameManager.Instance.fortsOnScreen + "/" + GameManager.Instance.maxForts;
		//transform.parent.GetComponent<SimpleFortScript>().ChangeFortsText();

		

		Destroy(gameObject.transform.parent.gameObject);
		return;
	}

	private void CannonAI ()
	{
		if (GetComponentInChildren<FortLOS>().playerInSight == true) {
			if (this.gameObject.name == "R_Cannon" && fireCountdown <= 0f) {
				RCannonFire();
				fireCountdown = 3.5f / fireRate;
			}

			if (this.gameObject.name == "L_Cannon" && fireCountdown <= 0f) {
				LCannonFire();
				fireCountdown = 3.5f / fireRate;
			}

			if (this.gameObject.name == "U_Cannon" && fireCountdown <= 0f) {
				UCannonFire();
				fireCountdown = 3.5f / fireRate;
			}

			if (this.gameObject.name == "D_Cannon" && fireCountdown <= 0f) {
				DCannonFire();
				fireCountdown = 3.5f / fireRate;
			}

		}
	}

	void RCannonFire()
	{

		Vector2 newCanBallPos = new Vector2(transform.position.x + 0.3f, transform.position.y);

		GameObject newCannonBall = Instantiate(canBallPrefab, newCanBallPos, Quaternion.identity);

		newCannonBall.transform.parent = gameObject.transform;

		newCannonBall.GetComponent<CannonFire>().fortShot = true;


		audioManager.Play("CannonFire");
		
	}

	void LCannonFire()
	{

		Vector2 newCanBallPos = new Vector2(transform.position.x - 0.3f, transform.position.y);

		GameObject newCannonBall = Instantiate(canBallPrefab, newCanBallPos, Quaternion.identity);

		newCannonBall.transform.parent = gameObject.transform;

		newCannonBall.GetComponent<CannonFire>().fortShot = true;

		audioManager.Play("CannonFire");
		
	}

	void UCannonFire()
	{

		Vector2 newCanBallPos = new Vector2(transform.position.x, transform.position.y + 0.3f);

		GameObject newCannonBall = Instantiate(canBallPrefab, newCanBallPos, Quaternion.identity);

		newCannonBall.transform.parent = gameObject.transform;

		newCannonBall.GetComponent<CannonFire>().fortShot = true;

		audioManager.Play("CannonFire");
		
	}	

	void DCannonFire()
	{

		Vector2 newCanBallPos = new Vector2(transform.position.x, transform.position.y - 0.3f);

		GameObject newCannonBall = Instantiate(canBallPrefab, newCanBallPos, Quaternion.identity);

		newCannonBall.transform.parent = gameObject.transform;

		newCannonBall.GetComponent<CannonFire>().fortShot = true;

		audioManager.Play("CannonFire");
		
	}

	
}
