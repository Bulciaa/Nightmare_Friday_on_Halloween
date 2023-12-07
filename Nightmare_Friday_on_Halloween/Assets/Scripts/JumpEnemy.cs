using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpEnemy : MonoBehaviour
{
 public Transform player;
public Transform enemy;
public float jumpDistance = 10f;
public float jumpForce = 5f;
public float jumpDelay = 2f;

private float nextJumpTime = 0f;

private bool isJumping;

public Animator anim;

void Start()
{

anim = GetComponent<Animator>();
isJumping = false;
}

void Update()
{
    if (Time.time > nextJumpTime)
    {
        if (Vector2.Distance(player.position, enemy.position) < jumpDistance)
        {
		anim.SetBool("isJump", true);
            Jump();
            nextJumpTime = Time.time + jumpDelay;
        }
	
	else if (Vector2.Distance(player.position, enemy.position) > jumpDistance)
	{
		anim.SetBool("isJump", false);
		isJumping = false;
	}
    }
}

void Jump()
{
	if(isJumping = true)	
	{
   	 enemy.GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
	}

}
}
