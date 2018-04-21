using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class PlayerControl : MonoBehaviour
{

	private float original;
	[SerializeField] private LineRenderer history;

	// Use this for initialization
	void Start ()
	{
		//original = Input.GetAxis("Vertical");
		original=0;
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.localPosition = Vector3.up * (Input.GetAxis("Vertical")-original);
		history.positionCount++;
		history.SetPosition(history.positionCount-1,transform.position);
	
	}
}
