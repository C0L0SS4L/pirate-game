using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PirateAI : MonoBehaviour {

	public LayerMask layer;

	[SerializeField] private GameObject sea1_waypointsHolder;

	
	private float speed = 1.45f;
	private int randomSpot;

	private float waitTime;
	private float startWaitTime = 3f;
	private float rotationSpeed = 50f;
	private Vector3 movePoint;
	private Quaternion _lookRotation;
	private Vector2 _direction;
	private Collider2D inAttackRange;
	private bool avoiding;
	private bool onPatrolPoint = false;
	private float health = 100;
	private float hBarTime = 5f;
	private float reloadTime = 3f;
	private float reloadCountdown;
	//private bool startTimer;
	private	float hBarTimeLast = 0f;
	
	public bool dead;

	[Header("Objects")]
	public Image healthBar;
	public GameObject coinGO;
	public GameObject coin10GO;
	public GameObject smallFire;
	public GameObject bigFire;
	public Sprite damagedPirate;
	public Sprite nearDeadPirate;
	public Sprite deadPirate;
	public GameObject canBallPrefab;
	public GameObject rCannon1;
	public GameObject lCannon1;

	//private GameObject rPlayerCOF;
	//private GameObject lPlayerCOF;
	private AudioManager audManager;


	[Header("Debug")]
	public bool move = true;

	[Header("Sensors")]
	public float sensorLength = 3f;
	public float frontSensorPos = 0.5f;
	public float frontSideSensorPos = 0.2f;
	public float frontSensorAngle = 20.3f;
	public float maxRotateAngle = 75f;
	//private Transform waypointTrans;

	void Start () {
		dead = false;
		reloadCountdown = 3f;
		waitTime = startWaitTime;
		audManager = FindObjectOfType<AudioManager>();

		//startTimer = false;
		randomSpot = Random.Range(0,6);
		
		healthBar.gameObject.transform.parent.gameObject.transform.parent.gameObject.SetActive(false);

		//rPlayerCOF = GameObject.Find("Player").transform.Find("r_cof").gameObject;
		//lPlayerCOF = GameObject.Find("Player").transform.Find("r_cof").gameObject;
	}

	void Update () {
		reloadCountdown += Time.deltaTime;
		movePoint = sea1_waypointsHolder.transform.GetChild(randomSpot).position;
		//waypointTrans = sea1_waypointsHolder.transform.GetChild(randomSpot);

		Sensors();
		
		if (move && !avoiding && !dead)
		{
			Patrol();		
		}
		//hBarTime += Time.deltaTime;

		/*if (startTimer)
		{
			if (hBarTime - hBarTimeLast >= 5f)
			{
				healthBar.gameObject.transform.parent.gameObject.transform.parent.gameObject.SetActive(false);
				hBarTimeLast = hBarTime;
				startTimer = false;
			}
		}*/

		/*if (startTimer)
		{
			if (hBarTime > 0)
			{
				hBarTime -= Time.deltaTime;
			}
			
		}
		else if (!startTimer)
		{
			hBarTime = 5f;
		}

		if (hBarTime <= 0)
		{
			healthBar.gameObject.transform.parent.gameObject.transform.parent.gameObject.SetActive(false);
		}
		else
		{
			healthBar.gameObject.transform.parent.gameObject.transform.parent.gameObject.SetActive(true);
		}*/
		

	}

	void Sensors ()
	{
		//RaycastHit hit;
		Vector3 sensorStartPos = transform.position;
		//sensorStartPos += -transform.up * frontSensorPos.y;
		//sensorStartPos.y += frontSensorPos;
		sensorStartPos += -transform.up * frontSensorPos;
		float avoidMultiplier = 0f;
		//avoiding = false;



		//front right sensor
		//sensorStartPos.x += frontSideSensorPos;
		sensorStartPos -= transform.right * frontSideSensorPos;

		RaycastHit2D frightSensorHit = Physics2D.Raycast(sensorStartPos, -transform.up, sensorLength, 1 << LayerMask.NameToLayer("Environment"));
		Vector3 rightSensorPos = sensorStartPos;

		RaycastHit2D frAngleSensorHit = Physics2D.Raycast(sensorStartPos, Quaternion.AngleAxis(-frontSensorAngle, transform.forward) * -transform.up, sensorLength, 1 << LayerMask.NameToLayer("Environment"));

		sensorStartPos += transform.right * frontSideSensorPos * 2;

		RaycastHit2D fleftSensorHit = Physics2D.Raycast(sensorStartPos, -transform.up, sensorLength, 1 << LayerMask.NameToLayer("Environment"));
		Vector3 leftSensorPos = sensorStartPos;

		RaycastHit2D flAngleSensorHit = Physics2D.Raycast(sensorStartPos, Quaternion.AngleAxis(frontSensorAngle, transform.forward) * -transform.up, sensorLength, 1 << LayerMask.NameToLayer("Environment"));


		if (frightSensorHit.collider != null)
		{
			if (frightSensorHit.collider.CompareTag("Terrain"))
			{
				avoiding = true;
				avoidMultiplier += 1f;
				//Debug.DrawLine(rightSensorPos, frightSensorHit.point);
			}
		}

		//front right angle sensor
		//RaycastHit2D frAngleSensorHit = Physics2D.Raycast(sensorStartPos, Quaternion.AngleAxis(-frontSensorAngle, transform.forward) * -transform.up, sensorLength, 1 << LayerMask.NameToLayer("Environment"));
		else if (frAngleSensorHit.collider != null/*Physics.Raycast(sensorStartPos, Quaternion.AngleAxis(-frontSensorAngle, transform.forward) * -transform.up, out hit, sensorLength)*/) 
		{
			avoiding = true;
			avoidMultiplier += 0.5f;
			//Debug.DrawLine(rightSensorPos, frAngleSensorHit.point);
		}

		//front left sensor
		//sensorStartPos.x -= 2 * frontSideSensorPos;
		//sensorStartPos += transform.right * frontSideSensorPos * 2;

		//RaycastHit2D fleftSensorHit = Physics2D.Raycast(sensorStartPos, -transform.up, sensorLength, 1 << LayerMask.NameToLayer("Environment"));
		if (fleftSensorHit.collider != null)
		{
			if (fleftSensorHit.collider.CompareTag("Terrain"))
			{
				avoiding = true;
				avoidMultiplier -= 1f;
				//Debug.DrawLine(leftSensorPos, fleftSensorHit.point);
			}
		}

		//front left angle sensor
		//RaycastHit2D flAngleSensorHit = Physics2D.Raycast(sensorStartPos, Quaternion.AngleAxis(frontSensorAngle, transform.forward) * -transform.up, sensorLength, 1 << LayerMask.NameToLayer("Environment"));
		else if (flAngleSensorHit.collider != null/*Physics.Raycast(sensorStartPos, Quaternion.AngleAxis(frontSensorAngle, transform.forward) * -transform.up, out hit, sensorLength)*/)
		{
			avoiding = true;
			avoidMultiplier -= 0.5f;
			//Debug.DrawLine(leftSensorPos, flAngleSensorHit.point);
		}

		sensorStartPos = transform.position;
		sensorStartPos += -transform.up * frontSensorPos;
		//front center sensor
		RaycastHit2D centerSensorHit = Physics2D.Raycast(sensorStartPos, -transform.up, sensorLength, 1 << LayerMask.NameToLayer("Environment"));
		if (avoidMultiplier == 0)
		{
			if (centerSensorHit.collider != null/*Physics.Raycast(sensorStartPos, -transform.up, out hit, sensorLength)*/)
			{
				if (centerSensorHit.collider.CompareTag("Terrain"))
				{
					avoiding = true;
					if(centerSensorHit.normal.x < 0)
					{
						avoidMultiplier += 1f;
					}
					else
					{
						avoidMultiplier += -1f;
					}
					//Debug.DrawLine(sensorStartPos, centerSensorHit.point);
				}
				
			}
		}

		if (frightSensorHit.collider == null && frAngleSensorHit.collider == null && fleftSensorHit.collider == null && flAngleSensorHit.collider == null && centerSensorHit.collider == null)
		{
			avoiding = false;
		}

		if (avoiding && !onPatrolPoint && move)
		{
			//transform.Rotate(0,0,maxRotateAngle * avoidMultiplier);
			transform.Translate(Vector2.down * Time.deltaTime * speed);
			Quaternion qt = Quaternion.AngleAxis(maxRotateAngle * avoidMultiplier, Vector3.forward);
			Quaternion qtAdded = Quaternion.Euler(0,0,90);
			transform.rotation = Quaternion.RotateTowards(transform.rotation, transform.rotation * qt * qtAdded, Time.deltaTime * rotationSpeed);
		}
	}

	void Patrol ()
	{
		speed = 1.45f;

		Collider2D areaColliderHit = Physics2D.OverlapCircle(transform.position, 12, layer);
		//List<Collider2D> areaColliderHitsList = areaColliderHits.ToList();
		//Collider2D hitPlayer = Physics2D.OverlapCircle(transform.position,10);


		/*foreach (Collider2D col in areaColliderHits)
		{
			if (col.tag == "Player")
			{
				playerHit = true;
			}
			else
			{
				playerHit = false;
			}
		}*/

		//print (areaColliderHit.tag);

		if (areaColliderHit == null) {

			Vector3 vectorToTarget = movePoint - transform.position;
			float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
			Quaternion qt = Quaternion.AngleAxis(angle,Vector3.forward);
			Quaternion qtAdded = Quaternion.Euler(0,0,90);
			transform.rotation = Quaternion.RotateTowards(transform.rotation, qt * qtAdded, Time.deltaTime * rotationSpeed);

			//transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, rotationSpeed * Time.deltaTime);
			//transform.Translate(Vector2.up * speed * Time.deltaTime);
			//transform.rotation
			//transform.LookAt(waypointTrans);
			if (!onPatrolPoint)
			{
				transform.Translate(Vector2.down * Time.deltaTime * speed);
			}
			//transform.position = Vector2.MoveTowards(transform.position, movePoint, speed * Time.deltaTime);

			if (Vector2.Distance(transform.position, movePoint) < 0.2f) {
				onPatrolPoint = true;
				if (waitTime <= 0) {
					randomSpot = Random.Range(0,6);
					waitTime = startWaitTime;
				} else {
					waitTime -= Time.deltaTime;
				}
			}
			else
			{
				onPatrolPoint = false;
			}		
		}
		else if (areaColliderHit.tag == "Player" && !avoiding) 
		{
			Hunt(areaColliderHit.gameObject);
		}
	}

	void Hunt (GameObject playerGO)
	{
		speed = 1.45f;
		inAttackRange = Physics2D.OverlapCircle(transform.position, 6, layer);

		if (inAttackRange == null && !avoiding)
		{
			Vector3 vectorToTarget = playerGO.transform.position - transform.position;
			float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
			Quaternion qt = Quaternion.AngleAxis(angle,Vector3.forward);
			Quaternion qtAdded = Quaternion.Euler(0,0,90);
			transform.rotation = Quaternion.RotateTowards(transform.rotation, qt * qtAdded, Time.deltaTime * rotationSpeed);

			transform.position = Vector2.MoveTowards(transform.position, playerGO.transform.position, speed * Time.deltaTime);		
		}
		else if (inAttackRange.tag == "Player" && !avoiding && !dead)
		{
			Attack(inAttackRange.gameObject);
		}


		
	}

	void Attack (GameObject playerGO)
	{
		if (!avoiding)
		{
			speed = 1f;
		}

		//float distanceToShoot = 6f;

		transform.Translate(Vector2.down * Time.deltaTime * speed);

		//Debug.Log(playerPosChecker.collider.name);
		if (inAttackRange.tag == "Player")
		{
			if (!avoiding && !dead)
			{
				
				//float angle = Mathf.Atan2(playerGO.transform.position.y, playerGO.transform.position.x) * Mathf.Rad2Deg;
				//Quaternion qt = Quaternion.AngleAxis(angle,Vector3.forward);
				//transform.rotation = Quaternion.RotateTowards(transform.rotation, qt, Time.deltaTime * rotationSpeed);
				/*Vector3 targetDir = playerGO.transform.position - transform.position;
				float step = speed * Time.deltaTime;
				Vector3 newDir = Vector3.RotateTowards(transform.down, targetDir, step, 0f);
				Debug.DrawRay(transform.position, newDir, Color.red);
				transform.rotation = Quaternion.LookRotation(newDir);*/

				Vector3 vectorToTarget = playerGO.transform.position - transform.position;
				float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
				Quaternion qt = Quaternion.AngleAxis(angle,Vector3.forward);
				transform.rotation = Quaternion.RotateTowards(transform.rotation, qt, Time.deltaTime * rotationSpeed);			
			}
		}
		else if (inAttackRange == null)
		{
			Hunt(playerGO);
		}
	}

	public void TakeDamage (float amount)
	{
		healthBar.gameObject.transform.parent.gameObject.transform.parent.gameObject.SetActive(true);
		health -= amount;
		healthBar.fillAmount = health / 100;
		if (health > 0)
		{
			audManager.Play("ShipHit");
		}
		

		//startTimer = true;

		//hBarTime = 5f;

		/*if (startTimer)
		{
			startTimer = false;
		}

		startTimer = true;*/

		if (health <= 0)
		{
			if (!dead)
			{
				Die();
			}
		} 
		else if (health <= 33)
		{
			GetComponent<SpriteRenderer>().sprite = nearDeadPirate;
		}
		else if (health <= 66)
		{
			GetComponent<SpriteRenderer>().sprite = damagedPirate;

			/*Vector3 smallFirePos1 = new Vector3 (0, 1, 0);
			GameObject smallFire1;
			smallFire1 = Instantiate(smallFire, transform.position + smallFirePos1, Quaternion.identity);
			smallFire1.transform.parent = gameObject.transform;

			Vector3 smallFirePos2 = new Vector3 (0, -1, 0);
			GameObject smallFire2;
			smallFire2 = Instantiate(smallFire, transform.position + smallFirePos2, Quaternion.identity);
			smallFire2.transform.parent = gameObject.transform;*/

		}
	}

	public void Die ()
	{
		//gameObject.tag = "Dead";
		GetComponent<SpriteRenderer>().sprite = deadPirate;
		dead = true;
		healthBar.gameObject.transform.parent.gameObject.transform.parent.gameObject.SetActive(false);

		GameManager.Instance.piratesOnScreen -= 1;
		GameManager.Instance.piratesKilled++;
		Debug.Log(GameManager.Instance.piratesKilled);

		audManager.Play("ShipDestroy");


		Vector3 newCoinPos = new Vector2(transform.position.x, transform.position.y);
		/*Vector3 newCoinPos2 = new Vector2(transform.position.x, transform.position.y - 1);
		Vector3 newCoinPos3 = new Vector2(transform.position.x + 1, transform.position.y);
		Vector3 newCoinPos4 = new Vector2(transform.position.x - 1, transform.position.y);
		Vector3 newCoinPos5 = new Vector2(transform.position.x, transform.position.y);*/

		Instantiate (coin10GO, newCoinPos, Quaternion.identity);
		/*Instantiate (coinGO, newCoinPos2, Quaternion.identity);
		Instantiate (coinGO, newCoinPos3, Quaternion.identity);
		Instantiate (coinGO, newCoinPos4, Quaternion.identity);
		Instantiate (coinGO, newCoinPos5, Quaternion.identity);*/

		//rPlayerCOF.GetComponent<PlayerCOF>().rEnemyInSight = false;
		//lPlayerCOF.GetComponent<PlayerCOF>().lEnemyInSight = false;

		Destroy(gameObject, 5);
	}

	public void RightSideFire ()
	{
		if (reloadCountdown >= reloadTime && !dead)
		{
			GameObject cannonBallClone;

			Vector2 newCanBallPos = new Vector2(rCannon1.transform.position.x, rCannon1.transform.position.y);

			cannonBallClone = Instantiate(canBallPrefab, newCanBallPos, gameObject.transform.GetChild(4).transform.rotation);

			cannonBallClone.GetComponent<CannonFire>().pirateShot = true;

			audManager.Play("CannonFire");

			reloadCountdown = 0;
		}

	}

	public void LeftSideFire ()
	{
		if (reloadCountdown >= reloadTime && !dead)
		{
			GameObject cannonBallClone;

			Vector2 newCanBallPos = new Vector2(lCannon1.transform.position.x, lCannon1.transform.position.y);

			cannonBallClone = Instantiate(canBallPrefab, newCanBallPos, gameObject.transform.GetChild(5).transform.rotation);

			cannonBallClone.GetComponent<CannonFire>().pirateShot = true;

			audManager.Play("CannonFire");

			reloadCountdown = 0;
		}

	}
}
