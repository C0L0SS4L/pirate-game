using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonFire : MonoBehaviour {

	[SerializeField] private float canBallSpeed = 3f;
	[SerializeField] private GameObject explosionPrefab;
	[SerializeField] private GameObject ripplePrefab;
	[SerializeField] private GameObject ship;

	private float fortCanBallSpeed = 2.3f;
	private float fortCannonDamage = 10;
	//public static float playerCannonDamage = 10;

	public bool pirateShot = false;
	public bool fortShot = false;

	private bool rCannonShot;
	private bool lCannonShot;
	private bool uCannonShot;
	private bool dCannonShot;

	// Use this for initialization
	void Start () {
		//rb = GetComponent<Rigidbody2D>();

		StartCoroutine(CannonballLifespan());

		/*if (gameObject.transform.parent.tag == "Right Side Cannons") {
			gameObject.transform.eulerAngles = ship.transform.GetChild(2).transform.eulerAngles;
			rSideFired = true;
			
		}

		if (gameObject.transform.parent.tag == "Left Side Cannons") {
			gameObject.transform.eulerAngles = ship.transform.GetChild(3).transform.eulerAngles;
			lSideFired = true;
			
		} */

	}
	
	// Update is called once per frame
	void Update () {
		
		if (gameObject.transform.parent != null/* || rCannonShot || lCannonShot || uCannonShot || dCannonShot*/)
		{
			if (gameObject.transform.parent.name == "R_Cannon"/* || rCannonShot*/) {
				transform.Translate(Vector2.right * Time.deltaTime * fortCanBallSpeed);
				/*rCannonShot = true;
				transform.parent = null;*/
			}

			if (gameObject.transform.parent.name == "L_Cannon"/* || lCannonShot*/) {
				transform.Translate(Vector2.left * Time.deltaTime * fortCanBallSpeed);
				/*lCannonShot = true;
				transform.parent = null;*/
			}

			if (gameObject.transform.parent.name == "U_Cannon"/* || uCannonShot*/) {
				transform.Translate(Vector2.up * Time.deltaTime * fortCanBallSpeed);
				/*uCannonShot = true;
				transform.parent = null;*/
			}

			if (gameObject.transform.parent.name == "D_Cannon"/* || dCannonShot*/) {
				transform.Translate(Vector2.down * Time.deltaTime * fortCanBallSpeed);
				/*dCannonShot = true;
				transform.parent = null;*/
			}

			if (gameObject.transform.parent.tag == "Right Side Cannons") {
				transform.Translate(Vector2.right * Time.deltaTime * canBallSpeed);
			}

			if (gameObject.transform.parent.tag == "Left Side Cannons") {
				transform.Translate(Vector2.right * Time.deltaTime * canBallSpeed);
			}
		} else {
			transform.Translate(Vector2.left * Time.deltaTime * canBallSpeed);
		}
		
		
	}

	private void OnCollisionEnter2D(Collision2D col) {

		if (col.gameObject.tag == "Player" && transform.parent != null/*&& ship.GetComponent<PlayerBehavior>().rSideCanball && ship.GetComponent<PlayerBehavior>().lSideCanball*/) {
			Instantiate(explosionPrefab, transform.position, Quaternion.identity);

			Damage(col.transform);

			Destroy(gameObject);
			return;
		}

		if (col.gameObject.tag == "Player" && transform.parent == null)
		{
			Instantiate(explosionPrefab, transform.position, Quaternion.identity);

			Damage(col.transform);

			Destroy(gameObject);
			return;
		}

		if (col.gameObject.tag == "FortMain" && transform.parent == null/* && !rCannonShot && !lCannonShot && !uCannonShot && !dCannonShot*/) {
			if (pirateShot)
			{
				Instantiate(explosionPrefab, transform.position, Quaternion.identity);

				pirateShot = false;
				Destroy(gameObject);
				return;
			}

			Instantiate(explosionPrefab, transform.position, Quaternion.identity);

			FortDamage(col.transform);

			Destroy(gameObject);
			return;
		}

		if (col.gameObject.tag == "Pirate"/* && transform.parent == null */)
		{
			if (pirateShot)
			{
				Instantiate(explosionPrefab, transform.position, Quaternion.identity);

				pirateShot = false;
				Destroy(gameObject);
				return;
			}

			if (fortShot)
			{
				Instantiate(explosionPrefab, transform.position, Quaternion.identity);

				fortShot = false;
				Destroy(gameObject);
				return;
			}
			Instantiate(explosionPrefab, transform.position, Quaternion.identity);

			PirateDamage(col.transform);

			Destroy(gameObject);
			return;
		}
	}

	void Damage (Transform player)
	{
		PlayerBehavior p = player.GetComponent<PlayerBehavior>();

		if (p != null) {
			p.TakeDamage(fortCannonDamage);
		}
	}

	void FortDamage (Transform fort)
	{
		FortAI fAI = fort.GetComponentInChildren<FortAI>();

		if (fAI != null) {
			fAI.TakeDamage(GameManager.Instance.playerCannonDamage);
		}
	}

	void PirateDamage (Transform pirate)
	{
		PirateAI pAI = pirate.GetComponent<PirateAI>();

		if (pAI != null)
		{
			pAI.TakeDamage(GameManager.Instance.playerCannonDamage);
		}
	}

	IEnumerator CannonballLifespan ()
	{
		yield return new WaitForSeconds(3.2f);

		Instantiate(ripplePrefab, transform.position, Quaternion.identity);

		Destroy(gameObject);
		yield return null;
	}
}
