using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootTransfer : MonoBehaviour
{


	public float Speed;

	public static RootTransfer instance;
	// Use this for initialization
	private void Awake()
	{
		if (instance == null) instance = this;
	}

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.position += -Vector3.left * Speed * Time.deltaTime;
	}
}
