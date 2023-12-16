using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingEnemy : MonoBehaviour
{
	 public Transform[] patrolPoints;
   	 public float moveSpeed;
   	 public int patrolDestination;

   	 public Transform playerTransform;
   	 public bool isChasing;
    	 public float chaseDistance;
	
	private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Granica"))
        {

		Walking();
        }
    }

	
	void Update()
        {

        	if (isChasing)
        	{
		moveSpeed = 4;
            		
            		if (transform.position.x > playerTransform.position.x)
            		{			
			transform.localScale = new Vector3(1,1,1);
                	transform.position += Vector3.left * moveSpeed * Time.deltaTime;
            		}
			
                	if (transform.position.x < playerTransform.position.x)
            		{
			transform.localScale = new Vector3(-1,1,1);
                	transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            		}

			if (transform.position.y < playerTransform.position.y)
            		{
				isChasing = false;
				Walking();
            		}
			
			if (Vector2.Distance(transform.position, playerTransform.position) > 4)
			{
   				 isChasing = false;
    					moveSpeed = 2;
			}

			

        	}

        	else
					

        	{
            		if (Vector2.Distance(transform.position, playerTransform.position) < chaseDistance)
            		{
                		isChasing = true;
            		}
		
			Walking();
		}
	 
}

void Walking()
	{
           	if (patrolDestination == 0)
            	{
                transform.position = Vector2.MoveTowards(transform.position, patrolPoints[0].position, moveSpeed * Time.deltaTime);
                if(Vector2.Distance(transform.position, patrolPoints[0].position) < .2f)
                {
                    patrolDestination = 1;
                    transform.localScale = new Vector3(-1,1,1);
                }
            	}
        
            	if (patrolDestination == 1)
            	{
                transform.position = Vector2.MoveTowards(transform.position, patrolPoints[1].position, moveSpeed * Time.deltaTime);
                if(Vector2.Distance(transform.position, patrolPoints[1].position) < .2f)
                {
                    patrolDestination = 0;
                    transform.localScale = new Vector3(1,1,1);
                    
                }

       	   	}
	}

}
