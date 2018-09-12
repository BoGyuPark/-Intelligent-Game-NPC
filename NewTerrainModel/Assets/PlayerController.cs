using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float movementSpeed;
    public float velocity;
    public Rigidbody rb;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        GetInput();
        Move();
	}

    void GetInput()
    {
        //move left
        if (Input.GetKey(KeyCode.A))
        {
            SetVelocity(-1);
        }
        else if(Input.GetKeyUp(KeyCode.A))
        {
            SetVelocity(0);
        }
        //move right
        if (Input.GetKey(KeyCode.D))
        {
            SetVelocity(1);
        }
        else if(Input.GetKeyUp(KeyCode.D))
        {
            SetVelocity(0);

        }
    }

    void Move()
    {
        rb.MovePosition(transform.position + (Vector3.right * velocity * movementSpeed * Time.deltaTime));
    }
    void SetVelocity(float dir)
    {
        velocity = dir;
    }
}
