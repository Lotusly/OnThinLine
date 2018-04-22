using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeGenerator : MonoBehaviour
{

	[SerializeField] private GameObject node;

	[SerializeField] private float moveSpeed;

	[SerializeField] private float tempo;

	[SerializeField] private Vector3[] place; // last : pitch

	[SerializeField] private Transform parent;

	[SerializeField] private string prefix;
	// Use this for initialization
	void Start ()
	{
		float t = 0;
		int l = place.Length;
		for (int i = 0; i < l; i++)
		{
			Vector3 position;
			//position = Vector3.right * t* moveSpeed / tempo * 60 + 0.7f*Vector3.up*place[i].z;
			position = Vector3.right * t* moveSpeed / tempo * 60 + Vector3.up*(Mathf.Floor(Random.value*5)-2)*0.5f;
			t = t + place[i].x;
			GameObject newNode = Instantiate(node, position, Quaternion.identity, parent);
			newNode.name = prefix+place[i].y.ToString();
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
