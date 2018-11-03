using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerControl : MonoBehaviour {

    //몬스터를 소환할 시간 간격을 저장할 변수
    public float SpawnTime = 50.0f;
    //마지막으로 소환한 시간을 저장할 변수
    float LastSpawnTime;
    public GameObject[] monster;
    int randomNum;
	// Use this for initialization
	void Start () {
        // Time.time : 현재 시간
        LastSpawnTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time > LastSpawnTime + SpawnTime)
        {
            LastSpawnTime = Time.time;
            Vector3 pos = new Vector3(transform.position.x + Random.Range(-50.0f, 50.0f), transform.position.y, +transform.position.z + Random.Range(-50.0f, 50.0f));
            // 0 ~ 59 중에 랜덤 range 0, 60
            randomNum = Random.Range(0, 60);
            Instantiate(monster[randomNum], pos, transform.rotation);
        }
	}
}
