using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardAndMoveUp : MonoBehaviour {

	private TextMesh textmesh;
	public float UpSpeed = 1.0f;
	public Color color = Color.yellow;
	public GameObject target_;
	
	// Use this for initialization
	void Start () {
		textmesh = gameObject.GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (target_ != null) {
			transform.Translate(Vector3.up * UpSpeed * Time.deltaTime);
		}
		
		transform.LookAt(Camera.main.transform.position);
		transform.Rotate(new Vector3(0.0f,180.0f,0.0f));
		
	}
	
	public void SetText(string str)
	{
		if(textmesh == null)
		{
			textmesh = gameObject.GetComponent<TextMesh>();
		}
		
		textmesh.text = str;
	}
	
	public void SetTarget(GameObject target)
	{
		target_ = target;
		transform.position = target.transform.position;
	}
	
	public void SetColor(Color c)
	{
		color = c;
        textmesh.GetComponent<Renderer>().material.color = c;
	}
}
