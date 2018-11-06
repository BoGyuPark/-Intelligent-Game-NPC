using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonstersControl : MonoBehaviour {

    public Vector3 targetPos = Vector3.zero;
    public float MoveSpeed = 5.0f;
    public GameObject HitEffect;
    public GameObject DeadEffect;
    public string MonsterName;
    public double HP;
    public double ARMOR;
    public double DPS;
    public static bool flagnum = false;
    private Animation animation;
    public static double deadHp = 0;

    public enum MonsterState
    {
        IDLE = 0,
        WALK = 1,
        ATTACK = 2,
        HIT = 3,
        SIZE
    }

    private MonsterState state = MonsterState.IDLE;

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
            //Debug.Log("sword");
            state = MonsterState.HIT;
            Instantiate(HitEffect, other.transform.position, transform.rotation);
            //10~50 랜덤 데미지
            //CheckDead(Random.Range(10, 50));
            CheckDead(PlayerControl.DPS * (1 - ARMOR));
            //Debug.Log("HITTED");

            PlayerControl.attackFlag = false;
            if (PlayerControl.attackFlag == false)
            {
                animation.Play("attack1");
                PlayerControl.attackFlag = true;

                if (HP > 0)
                {
                    double dag = DPS * (1 - PlayerControl.ARMOR);
                    PlayerControl.HP -= dag;
                    //플레이어가 죽은 경우, 플레이어는 계속하여 전투를 하고 마이너스 체력과 3000*10을 더하여 우선순위를 뒤로 미룬다.
                    if (PlayerControl.HP < 0) 
                    {
                        deadHp = PlayerControl.HP;
                        //ga.Population[(NumOfMon / dnaSize) - 1].Fitness = PlayerControl.HP;
                    }
                    //Debug.Log("Player HP : " + PlayerControl.HP.ToString());
                }
                //Debug.Log("flag change");
            }

        }
    }

    //몬스터가 받은 데미지를 계산하여 HP가 0보다 작거나 같다면 죽는 이펙트를 생성하고 몬스터를 삭제
    void CheckDead(double damage)
    {
        GameObject dmgObj = Instantiate(Resources.Load("Prefabs/DamageText"), Vector3.zero, Quaternion.identity) as GameObject;
        dmgObj.SendMessage("SetText", damage.ToString());
        dmgObj.SendMessage("SetTarget", gameObject);
        dmgObj.SendMessage("SetColor", new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f)));
        HP -= damage;
        //Debug.Log("HP :" + HP.ToString());
        if (HP <= 0)
        {
            //Instantiate(DeadEffect, transform.position, transform.rotation);
            Destroy(gameObject);

            //flagnum = true면 게임오브젝트가 죽었다는 뜻
            flagnum = true;
            //몬스터 죽은 경우 죽은 몬스터 수 카운트한다.
            SpawnerControl.NumOfMon++;

            //한 던전의 전투가 끝났을 경우
            if ((SpawnerControl.NumOfMon % SpawnerControl.dnaSize) == 0) 
            {
                SpawnerControl.numCheck = true;
            }
            else
            {
                SpawnerControl.numCheck = false;
            }

            //Debug.Log("The number of dead monster : " + SpawnerControl.NumOfMon);


            // Destroy(DeadEffect);
        }


    }

    public void SetParameter(string name, double hp, double armor, double dps)    
    {
        this.name = name;
        this.MonsterName = name;
        this.HP = hp;
        this.ARMOR = armor;
        this.DPS = dps;
    }

}

