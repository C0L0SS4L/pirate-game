using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour {

	public Animator animator;

	public void FadeToLevel ()
	{
		animator.SetTrigger("FadeOut");
	}

	public void StartGame ()
	{
		//GameManager.Instance.playerHealth = GameManager.Instance.maxHealth;
		SceneManager.LoadScene("Game");
	}

	public void FreezeTimeForShipScreen () {
		if (GameManager.Instance.faction == 0 && SceneManager.GetActiveScene().name != "HomeScreen")
		{
			Time.timeScale = 0f;
		}
		
	}
}
