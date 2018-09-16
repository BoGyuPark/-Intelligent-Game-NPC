using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeetleControl : MonoBehaviour {


    public Vector3 targetPos = Vector3.zero;
    public float MoveSpeed = 5.0f;
    public GameObject HitEffect;
    public GameObject DeadEffect;
    public int HP = 300;
    private Animation animation;


    public enum BeetleState
    {
        IDLE = 0,
        WALK = 1,
        ATTACK = 2,
        HIT = 3,
        SIZE
    }

    private BeetleState state = BeetleState.IDLE;

	// Use this for initialization
	void Start () {
        animation.wrapMode = WrapMode.Loop;
        animation.Play("move");
        HP = 300;
	}
	
	// Update is called once per frame
	void Update () {
        SearchTarget();

        Vector3 currentPos = transform.position;
        Vector3 diffPos = targetPos - currentPos;

        if(diffPos.magnitude < 2.0f)
        {
            return;
        }

        diffPos = diffPos.normalized;

        transform.Translate(diffPos * Time.deltaTime * MoveSpeed, Space.World);

        transform.LookAt(targetPos);
	}

    void SearchTarget()
    {
        GameObject target = GameObject.FindWithTag("Player");
        targetPos = target.transform.position;

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "sword")
        {
            state = BeetleState.HIT;
            Instantiate(HitEffect, other.transform.position, transform.rotation);
            CheckDead(50);
        }
    }

    void CheckDead(int damage)
    {
        HP -= damage;
        Debug.Log("HP : " + HP.ToString());
        if (HP <= 0)
        {
            Instantiate(DeadEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }

    }

    //new, PlyaerControl.cs 참고
    void CheckState()
    {

    }


}
