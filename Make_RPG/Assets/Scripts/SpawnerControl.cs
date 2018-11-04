using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerControl : MonoBehaviour {

    public GameObject[] monster;
    int randomNum;
    

    // Use this for initialization
    void Start () {
        
        Vector3 pos = new Vector3(transform.position.x + Random.Range(-50.0f, 50.0f), transform.position.y, +transform.position.z + Random.Range(-50.0f, 50.0f));
        // 0 ~ 59 중에 랜덤 range 0, 60
        randomNum = Random.Range(0, 1);
        //Instantiate(monster[randomNum], pos, transform.rotation);
        monster[0].GetComponent<AcolyteControl>().SetParameter(340, 0, 23.75);
        monster[0].name = "0";

        Instantiate(monster[0], pos, transform.rotation);
        
        
    }
	
	// Update is called once per frame
	void Update () {

        if(AcolyteControl.flagnum == true)
        {
            Vector3 pos = new Vector3(transform.position.x + Random.Range(-50.0f, 50.0f), transform.position.y, +transform.position.z + Random.Range(-50.0f, 50.0f));  
            Instantiate(monster[0], pos, transform.rotation);

            AcolyteControl.flagnum = false;
        }


    }
}
