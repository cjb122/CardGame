using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMovement : MonoBehaviour
{
    public Vector3 destination;
    public bool moving;
    public float speed;

    private void Start()
    {
        speed = 15;
    }

    public void moveCard(float xPos, float yPos)
    {
        destination = new Vector3(xPos, yPos, this.gameObject.transform.position.z);
        moving = true;
    }

    private void Update()
    {
        if (moving)
        {
            this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position,
                                                                     destination,
                                                                     speed * Time.deltaTime);
            if (this.gameObject.transform.position == destination)
                moving = false;
        }
    }
}
