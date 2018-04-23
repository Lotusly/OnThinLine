using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissJudge : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other)
	{

		if (other.gameObject.GetComponent<Node>() != null &&other.gameObject.GetComponent<Node>().enabled && !other.gameObject.GetComponent<Node>().hit)
		{
			ScoreManager.instance.Miss();
			Destroy(other.gameObject);
		}

	}
}
