using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCOF : MonoBehaviour {

	public bool rEnemyInSight;
	public bool lEnemyInSight;

	public GameObject shootTutorial;


	private void Start() {
		rEnemyInSight = false;
		lEnemyInSight = false;
		Color tmp = GetComponent<SpriteRenderer>().color;
		tmp.a = 0.1372549f;
		GetComponent<SpriteRenderer>().color = tmp;
	}

	void Update()
	{
		/*print(rEnemyInSight);
		print(lEnemyInSight);

		if (!rEnemyInSight && !lEnemyInSight)
		{
			Color tmp = GetComponent<SpriteRenderer>().color;
			if (tmp.a != 0.1372549f)
			{
				tmp.a = 0.1372549f;
				GetComponent<SpriteRenderer>().color = tmp;
			}
		}*/
	}

	void OnTriggerStay2D(Collider2D col) {
		
		if (col.gameObject.tag == "FortMain" || col.gameObject.tag == "Pirate")
		{
			if (col.gameObject.tag != "CannonBall" /*&& col.gameObject.name != "r_cannon los" && col.gameObject.name != "d_cannon los" && col.gameObject.name != "l_cannon los" && col.gameObject.name != "u_cannon los"*//*col.gameObject.name != "CannonBall(Clone)"*/) {
				if (this.gameObject.name == "r_cof")
				{
					rEnemyInSight = true;
					Color tmp = GetComponent<SpriteRenderer>().color;
					tmp.a = 0.41372549f;
					GetComponent<SpriteRenderer>().color = tmp;

					/*if (GameManager.Instance.shootTutorialDone)
					{
						if (shootTutorial.activeInHierarchy)
						{
							shootTutorial.SetActive(false);
							GameManager.Instance.shootTutorialDone = true;
						}
					}*/
				}
				if (this.gameObject.name == "L_cof")
				{
					lEnemyInSight = true;
					Color tmp = GetComponent<SpriteRenderer>().color;
					tmp.a = 0.41372549f;
					GetComponent<SpriteRenderer>().color = tmp;
				}
			}

		}

	}

	void OnTriggerExit2D(Collider2D col) {
		
		if (col.gameObject.tag == "FortMain" || col.gameObject.tag == "Pirate") 
		{
			if (this.gameObject.name == "r_cof")
			{
				rEnemyInSight = false;
				Color tmp = GetComponent<SpriteRenderer>().color;
				tmp.a = 0.1372549f;
				GetComponent<SpriteRenderer>().color = tmp;

			}

			if (this.gameObject.name == "L_cof")
			{
				lEnemyInSight = false;
				Color tmp = GetComponent<SpriteRenderer>().color;
				tmp.a = 0.1372549f;
				GetComponent<SpriteRenderer>().color = tmp;
			}
		}

	}

	public void ResetEnemyInSight ()
	{
		rEnemyInSight = false;
		lEnemyInSight = false;
		Color tmp = GetComponent<SpriteRenderer>().color;
		tmp.a = 0.1372549f;
		GetComponent<SpriteRenderer>().color = tmp;
	}
}
