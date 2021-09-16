using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	//public static float moveSpeed = 45f;
	public float rotationSpeed = 0.01f;
	public float drag = 0.7f;
	public Vector3 MoveVector{set;get;}
	public VirtualJoystick joystick;

	private Rigidbody2D rb;
	private PlayerBehavior playerBehaviorScript;

	private void Start () 
	{
		rb = gameObject.GetComponent<Rigidbody2D>();
		rb.drag = drag;
		playerBehaviorScript = gameObject.GetComponent<PlayerBehavior>();
	}
	
	private void Update () 
	{	
		MoveVector = Input ();

		if (!playerBehaviorScript.dead)
		{
			Move();
			Rotate();
		}
			
		
	}

	private void Move ()
	{
		rb.AddForce((MoveVector * GameManager.Instance.playerMoveSpeed * Time.deltaTime));
		
	}

	private void Rotate ()
	{
		if (joystick.InputDirection.sqrMagnitude > 0.1) {
        float angle = Mathf.Atan2 (joystick.InputDirection.x, -joystick.InputDirection.z) * Mathf.Rad2Deg; 
        transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.Euler (new Vector3 (0, 0, angle)), Time.deltaTime );
    	}
	}

	private Vector2 Input ()
	{
		Vector2 dir = Vector2.zero;

		dir.x = joystick.Horizontal();
		dir.y = joystick.Vertical();

		if(dir.magnitude > 1) {
			dir.Normalize();
		}

		return dir;
	}
}
