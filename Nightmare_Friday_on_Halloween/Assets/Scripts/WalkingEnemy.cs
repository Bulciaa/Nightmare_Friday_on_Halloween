using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingEnemy : MonoBehaviour
{
    public List<Transform> points;

    public int nextID=0;

    int idChangeValue = 1;
    
    public float moveSpeed = 1;


    private void Update()
    {
        MoveToNextPoint();
    }


    void MoveToNextPoint()
    {

        Transform goalPoint = points[nextID];

        if (goalPoint.transform.position.x > transform.position.x)
            transform.localScale = new Vector3(-1, 1, 1);
        else
            transform.localScale = new Vector3(1, 1, 1);
 
        transform.position = Vector2.MoveTowards(transform.position, goalPoint.position, moveSpeed * Time.deltaTime);

        if(Vector2.Distance(transform.position, goalPoint.position) < 0.2f)
        {

            if (nextID == points.Count - 1)
                idChangeValue = -1;

            if (nextID == 0)
                idChangeValue = 1;

            nextID += idChangeValue;
        }
    }
}
