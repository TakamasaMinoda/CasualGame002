using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float speed;
	public VariableJoystick variableJoystick;
	public Rigidbody2D rb;

	public void FixedUpdate()
	{
		Vector2 direction = Vector2.up * variableJoystick.Vertical + Vector2.right * variableJoystick.Horizontal;
		rb.AddForce(direction * speed * Time.fixedDeltaTime, ForceMode2D.Force);

		//if(Input.GetMouseButtonDown(0))
		//{
		//	rb.AddForce(Vector2.up * speed * Time.fixedDeltaTime, ForceMode2D.Force);
		//}
	}
}
