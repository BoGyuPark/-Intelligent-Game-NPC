using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    public float MoveSpeed = 10.0f;
    public float RotateSpeed = 500.0f;
    public float VerticalSpeed = 0.0f;
    private float gravity = 9.8f;

    private CharacterController charactercontroller;
    private Animation animation;

    private Vector3 MoveDirection = Vector3.zero;
    private CollisionFlags collisionflags;

    public AnimationClip idleAnim;
    public AnimationClip walkAnim;
    public AnimationClip attackAnim;
    public AnimationClip skillAnim;
    public enum CharacterState
    {
        IDLE = 0,
        WALK = 1,
        ATTACK = 2,
        SKILL = 3,
        SIZE
    }
    private CharacterState state = CharacterState.IDLE;

    // Use this for initialization
    void Start()
    {
        charactercontroller = GetComponent<CharacterController>();
        animation = GetComponent<Animation>();

        animation.wrapMode = WrapMode.Loop;
        animation.Stop();

        animation[attackAnim.name].wrapMode = WrapMode.Once;
        animation[attackAnim.name].layer = 1;

        animation[skillAnim.name].wrapMode = WrapMode.Once;
        animation[skillAnim.name].layer = 1;

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CheckState();
        AnimationControl();
        BodyDirection();
        ApplyGravity();
    }

    void Move()
    {
        Transform cameraTransform = Camera.main.transform;

        Vector3 forward = cameraTransform.TransformDirection(Vector3.forward);
        forward = forward.normalized; // 벡터의 정규화
        //좌우를 위한 벡터
        Vector3 right = new Vector3(forward.z, 0.0f, -forward.x);

        //키보드 방향키
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");

        Vector3 targetVector = v * forward + h * right;
        targetVector = targetVector.normalized; //법선 벡터

        MoveDirection = Vector3.RotateTowards(MoveDirection, targetVector, RotateSpeed * Mathf.Deg2Rad * Time.deltaTime, 500.0f);
        MoveDirection = MoveDirection.normalized;

        Vector3 grav = new Vector3(0.0f, VerticalSpeed, 0.0f);

        Vector3 movementAmt = (MoveDirection * MoveSpeed * Time.deltaTime) + grav;
        collisionflags = charactercontroller.Move(movementAmt);

    }

    void CheckState()
    {
        if (state == CharacterState.ATTACK || state == CharacterState.SKILL)
        {
            return;
        }

        if (charactercontroller.velocity.sqrMagnitude > 0.1f)
        {
            //move
            state = CharacterState.WALK;
        }
        else
        {
            //stand
            state = CharacterState.IDLE;
        }

        // 0: left, 1:  right, 2: wheel
        if (Input.GetMouseButtonDown(0))
        {
            state = CharacterState.ATTACK;
        }
        if (Input.GetMouseButtonDown(1))
        {
            state = CharacterState.SKILL;
        }

    }

    void AnimationControl()
    {
        switch (state)
        {
            case CharacterState.IDLE:
                animation.CrossFade(idleAnim.name);             // 애니메이션 클립을 재생하면서 앞서 재생되고 있는 애니메이션과 블렌딩하는 함수.
                break;
            case CharacterState.WALK:
                animation.CrossFade(walkAnim.name);
                break;
            case CharacterState.ATTACK:
                if (animation[attackAnim.name].normalizedTime > 0.9f)
                {
                    animation[attackAnim.name].normalizedTime = 0.0f;
                    state = CharacterState.IDLE;
                }
                else
                {
                    animation.CrossFade(attackAnim.name);
                }
                break;
            case CharacterState.SKILL:
                if (animation[skillAnim.name].normalizedTime > 0.9f)
                {
                    animation[skillAnim.name].normalizedTime = 0.0f;
                    state = CharacterState.IDLE;
                }
                else
                {
                    animation.CrossFade(skillAnim.name);
                }
                break;

        }
    }

    void BodyDirection()
    {
        Vector3 horizontalVelocity = charactercontroller.velocity;
        horizontalVelocity.y = 0.0f;
        if (horizontalVelocity.magnitude > 0.0f)
        {
            Vector3 trans = horizontalVelocity.normalized;
            Vector3 wantedVector = Vector3.Lerp(transform.forward, trans, 0.5f);
            if (wantedVector != Vector3.zero)
            {
                transform.forward = wantedVector;
            }
        }
    }

    //위에서 캐릭터가 내려올때 공중에 있음을 방지
    void ApplyGravity()
    {
        if (charactercontroller.isGrounded == true)
        {
            VerticalSpeed = 0.0f;
        }
        else
        {
            //on air
            VerticalSpeed -= gravity * Time.deltaTime;
        }
    }


}