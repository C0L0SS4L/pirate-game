using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AIPlayer : MonoBehaviour {


	[Header("Objects")]

	public Sprite spainFac;
	public Sprite englandFac;
	public Sprite franceFac;
	public Sprite italyFac;
	public TextMeshProUGUI seasHighText;

	private int aiFaction;



	[SerializeField] private GameObject sea1_waypointsHolder;

	
	private float speed = 1.3f;
	private int randomSpot;
	private float rotationSpeed = 50f;
	private Vector3 movePoint;
	private bool avoiding;
	private bool onPatrolPoint = false;

	[Header("Sensors")]
	public float sensorLength = 3f;
	public float frontSensorPos = 0.5f;
	public float frontSideSensorPos = 0.2f;
	public float frontSensorAngle = 20.3f;
	public float maxRotateAngle = 75f;

	public static bool updateSeaHighText;

	

	void Awake () {
		aiFaction = Random.Range(1, 5);

		randomSpot = Random.Range(0,6);

		if (aiFaction != 0)
		{
			if (aiFaction == 1)
			{
				GetComponent<SpriteRenderer>().sprite = spainFac;
			}
			else if (aiFaction == 2)
			{
				GetComponent<SpriteRenderer>().sprite = englandFac;
			}
			else if (aiFaction == 3)
			{
				GetComponent<SpriteRenderer>().sprite = franceFac;	
			}
			else if (aiFaction == 4)
			{
				GetComponent<SpriteRenderer>().sprite = italyFac;
			}
		}
	}

	void Update()
	{
	
		movePoint = sea1_waypointsHolder.transform.GetChild(randomSpot).position;

		Sensors();

		if (!avoiding)
		{
			Patrol();		
		}

		if (updateSeaHighText)
		{	
			seasHighText.text = GameManager.Instance.seasHighscore.ToString();
			updateSeaHighText = false;
		}
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

		if (avoiding && !onPatrolPoint)
		{
			//transform.Rotate(0,0,maxRotateAngle * avoidMultiplier);
			transform.Translate(Vector2.down * Time.deltaTime * speed);
			Quaternion qt = Quaternion.AngleAxis(maxRotateAngle * avoidMultiplier, Vector3.forward);
			Quaternion qtAdded = Quaternion.Euler(0,0,90);
			transform.rotation = Quaternion.RotateTowards(transform.rotation, transform.rotation * qt * qtAdded, Time.deltaTime * rotationSpeed);
		}
	}

	public void Patrol ()
	{
		speed = 1.3f;

		//Collider2D areaColliderHit = Physics2D.OverlapCircle(transform.position, 12, pirateLayer);
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
			randomSpot = Random.Range(0,6);
		}
		else
		{
			onPatrolPoint = false;
		}
		
	}



}
