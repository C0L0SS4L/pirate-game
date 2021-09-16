using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PirateSpawner : MonoBehaviour {

[SerializeField] private GameObject piratePrefab;
public GameObject pirateGO;

private float spawnCountdown = 0f;
private Camera cam;

	void Start () {
		cam = Camera.main;
	}
	
	void Update () {
		spawnCountdown -= Time.deltaTime;

		Vector3 viewPos = cam.WorldToViewportPoint(transform.position);

		if (pirateGO.GetComponent<PirateAI>().dead)
		{
			spawnCountdown = 15;
			pirateGO.GetComponent<PirateAI>().dead = false;
		}

		if (viewPos.x < 0 || viewPos.x > 1 || viewPos.y < 0 || viewPos.y > 1)
		{
			if (spawnCountdown <= 0)
			{
				SpawnPirate();

				spawnCountdown = 15f;
			}
		}


	}

	void SpawnPirate ()
	{

		int randomInt = Random.Range (1,6);

		if (GameManager.Instance.piratesOnScreen < GameManager.Instance.maxPirates)
		{
			if (randomInt == 1)
			{
				Instantiate(piratePrefab, transform.position, Quaternion.identity);

				GameManager.Instance.piratesOnScreen += 1;
			}
		}
		
	}

	public void EditorSpawnPirate ()
	{
		Instantiate(piratePrefab, transform.position, Quaternion.identity);
		GameManager.Instance.piratesOnScreen += 1;
	}
}
