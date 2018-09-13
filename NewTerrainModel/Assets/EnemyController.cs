using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public Animator anim;
    public float totalHealth;
    public float currentHealth;
    public float expGranted;
    public float atkDamage;
    public float atkSpeed;
    public float moveSpeed;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(0))
        {
            GetHit();
        }
	}

    public void GetHit()
    {
        anim.SetInteger("Condition", 3);
        StartCoroutine(RecoverFromHit());
    }

    IEnumerator RecoverFromHit()
    {
        yield return new WaitForSeconds(0.1f);
        anim.SetInteger("Condition", 0);
    }
}
