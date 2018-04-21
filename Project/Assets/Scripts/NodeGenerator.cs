using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeGenerator : MonoBehaviour
{

	[SerializeField] private GameObject node;

	[SerializeField] private float moveSpeed;

	[SerializeField] private float tempo;

	[SerializeField] private Vector2[] place; // last : pitch

	[SerializeField] private Transform parent;
	// Use this for initialization
	void Start ()
	{
		float t = 0;
		int l = place.Length;
		for (int i = 0; i < l; i++)
		{
			Vector3 position;
			position = Vector3.right * t* moveSpeed / tempo * 60;
			t = t + place[i].x;
			Instantiate(node, position, Quaternion.identity, parent);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
