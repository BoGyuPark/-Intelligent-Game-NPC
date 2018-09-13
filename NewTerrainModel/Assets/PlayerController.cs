using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator anim;

    [Header("Movement")]
    private bool canMove;
    public float movementSpeed;
    public float velocity;
    public Rigidbody rb;

    [Header("Combat")]
    private bool canAttack = true;
    private bool attacking;
    public float attackDamage;
    public float attackSpeed;
    public float attackRange;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        Move();
    }

    void GetInput()
    {
        //Attack
        if (Input.GetMouseButtonDown(0))
        {
            print("Attacking");
            Attack();
        }

        //move left
        if (Input.GetKey(KeyCode.A))
        {
            SetVelocity(-1);
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            SetVelocity(0);
            anim.SetInteger("Condition", 0);
        }
        //move right
        if (Input.GetKey(KeyCode.D))
        {
            SetVelocity(1);
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            SetVelocity(0);
            anim.SetInteger("Condition", 0);
        }
    }
    #region MOVEMENT
    void Move()
    {
        if (velocity == 0)
        {
            //anim.SetInteger("Condition", 0);
            return;
        }
        else
        {
            //we can oly move if we are not attacking
            if (canMove)
            {
                anim.SetInteger("Condition", 1);
                rb.MovePosition(transform.position + (Vector3.right * velocity * movementSpeed * Time.deltaTime));
            }
        }
    }
    void SetVelocity(float dir)
    {
        //look left or right depending on the (- +) of dir
        if (dir < 0)    transform.LookAt(transform.position + Vector3.left);
        else if (dir > 0)   transform.LookAt(transform.position + Vector3.right);
        velocity = dir;
    }
    #endregion

    #region COMBAT
    void Attack()
    {
        if (!canAttack) return;
        anim.SetInteger("Condition", 2);
        StartCoroutine(AttackRoutine());
        StartCoroutine(AttackCooldown());


    }

    IEnumerator AttackRoutine()
    {
        canMove = false;
        yield return new WaitForSeconds(0.1f);
        anim.SetInteger("Condition", 0);
        yield return new WaitForSeconds(0.65f);
        canMove = true;
    }

    IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(1/attackSpeed);
        canAttack = true;
    }

    void GetEnemiesInRange()
    {

    }
    #endregion 

}