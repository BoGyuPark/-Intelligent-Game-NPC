using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcolyteControl : MonoBehaviour {

    public Vector3 targetPos = Vector3.zero;
    public float MoveSpeed = 5.0f;
    public GameObject HitEffect;
    public GameObject DeadEffect;
    public double HP;
    public double ARMOR;
    public double DPS;
    public static bool flagnum = false;
    private Animation animation;

    public enum AcolyteState
    {
        IDLE = 0,
        WALK = 1,
        ATTACK = 2,
        HIT = 3,
        SIZE
    }

    private AcolyteState state = AcolyteState.IDLE;

    // Use this for initialization
    void Start()
    {
        animation = GetComponent<Animation>();
        animation.wrapMode = WrapMode.Loop;
        animation.Play("run");
        //HP = 300;
    }

    // Update is called once per frame
    void Update()
    {
        SearchTarget();

        Vector3 currentPos = transform.position;
        Vector3 diffPos = targetPos - currentPos;

        if (diffPos.magnitude < 4.0f)
        {
            return;
        }

        diffPos = diffPos.normalized;

        transform.Translate(diffPos * Time.deltaTime * MoveSpeed, Space.World);

        //움직이는 방향을 바라보도록 LookAt함수
        transform.LookAt(targetPos);

        //공격 및 다른 state 추가
        
       

    }


    void SearchTarget()
    {
        GameObject target = GameObject.FindWithTag("Player");
        targetPos = target.transform.position;

    }

    //sword라는 태그를 가진 오브젝트가 몬스터의 박스 충돌체에 충돌한다면 몬스터의 상태를 HIT이라는 상태로 바꾸고 HitEffect를 생성해(instantiate)준다.
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "sword")
        {
            Debug.Log("sword");
            state = AcolyteState.HIT;
            Instantiate(HitEffect, other.transform.position, transform.rotation);
            //10~50 랜덤 데미지
            //CheckDead(Random.Range(10, 50));
            CheckDead(34);
            Debug.Log("HITTED");

            PlayerControl.attackFlag = false;
            if (PlayerControl.attackFlag == false)
            {
                animation.Play("attack1");
                PlayerControl.attackFlag = true;
                //여기가 정상작동 안함
                Debug.Log("flag change");
            }
            //animation.Play("idle");

        }
    }

    //몬스터가 받은 데미지를 계산하여 HP가 0보다 작거나 같다면 죽는 이펙트를 생성하고 몬스터를 삭제
    void CheckDead(int damage)
    {
        GameObject dmgObj = Instantiate(Resources.Load("Prefabs/DamageText"), Vector3.zero, Quaternion.identity) as GameObject;
        dmgObj.SendMessage("SetText", damage.ToString());
        dmgObj.SendMessage("SetTarget", gameObject);
        dmgObj.SendMessage("SetColor", new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f)));
        HP -= damage;
        Debug.Log("HP :" + HP.ToString());
        if (HP <= 0)
        {
            Instantiate(DeadEffect, transform.position, transform.rotation);
            //gameObject.SetActive(false);
            Destroy(gameObject);
            //flagnum = true면 게임오브젝트가 죽었다는 뜻
            flagnum = true;
        }
    }

    public void SetParameter(double hp, double armor, double dps)
    {
        this.HP = hp;
        this.ARMOR = armor;
        this.DPS = dps;
    }

}

