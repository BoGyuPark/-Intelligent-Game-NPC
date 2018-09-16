using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {


    //third view point var
    public float distance = 15.0f;
    public float height = 10.0f;

    //Damping (자연스러운 카메라 이동을 위한 Damping(지연)
    public float heightDamping = 2.0f;
    public float rotationDamping = 3.0f;

    public GameObject target; //Player


    //LateUpdate : 현재 씬에 존재하는 모든 게임 오브젝트의 
    //스크립트 컴포넌트 안의 Update()함수들이 호출되고 나서 호출되는 함수
    // 캐릭터 이동후에 카메라가 쫓아가야 자연스러움
    private void LateUpdate()
    {
        ThirdView();
    }

    void ThirdView()
    {
        if(target == null)
        {
            target = GameObject.FindWithTag("Player");
        }
        else
        {
            float wantedRotationAngle = target.transform.eulerAngles.y;
            float wantedHeight = target.transform.position.y + height;

            float currentRotationAngle = transform.eulerAngles.y;
            float currentHeight = transform.position.y;

            currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
            currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

            Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

            //player position
            transform.position = target.transform.position;

            //move back
            transform.position -= currentRotation * Vector3.forward * distance;

            transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);

            transform.LookAt(target.transform);
        }
    }
    
}
