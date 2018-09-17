using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayAndDestroy : MonoBehaviour {

    public float delayTime = 0.0f;
    public float destroyTime = 1.0f;
    private float createTime;
    // Use this for initialization
    void Start()
    {
        createTime = Time.time;
        RenderOnOFF(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (createTime + delayTime < Time.time)
        {
            RenderOnOFF(true);
        }
        if (createTime + delayTime + destroyTime < Time.time)
        {
            Destroy(gameObject);
        }

    }
    void RenderOnOFF(bool onoff)
    {
        MeshRenderer[] render = GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer ren in render)
        {
            ren.enabled = onoff;
        }
    }

}
