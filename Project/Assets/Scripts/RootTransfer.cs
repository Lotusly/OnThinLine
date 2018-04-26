using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootTransfer : MonoBehaviour
{


	public float Speed;

	public static RootTransfer instance;

	private bool inGame = false;

	[SerializeField] private Transform origin;
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
		if (inGame) transform.position += -Vector3.left * Speed * Time.deltaTime;
		else
		{
			transform.RotateAround(origin.position,Vector3.up,-Speed*180/Mathf.PI/100*Time.deltaTime);
			origin.RotateAround(origin.position, Vector3.up, -Speed*180/Mathf.PI/100 * Time.deltaTime);
		}
	}

	public void EnterGame()
	{
		inGame = true;
	}
}
