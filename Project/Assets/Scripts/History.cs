using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class History : MonoBehaviour
{

	private LineRenderer line;
	
	//----------test--------
	public Vector3 point;

	// Use this for initialization
	void Start ()
	{
		line = GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void RotateToFaceFront()
	{
		int count = line.positionCount;
		Vector3 oldPosition = line.GetPosition(count - 1);
		Vector3 u = line.GetPosition(count - 2) - oldPosition;
		u= new Vector3(u.x,0,u.z).normalized;
		for (int i = 0; i < count - 1; i++)
		{
			point = line.GetPosition(i);
			line.SetPosition(i,new Vector3(oldPosition.x-Vector3.Dot(point-oldPosition,u),point.y,oldPosition.z));
		}

	}

	public void MoveTo(Vector3 newPosition)
	{
		int count = line.positionCount;
		Vector3 oldPosition = line.GetPosition(count - 1);
		oldPosition = new Vector3(oldPosition.x,-995,oldPosition.z);
		Vector3 shift = newPosition - oldPosition;
		for (int i = 0; i < count; i++)
		{
			line.SetPosition(i, line.GetPosition(i) + shift);
		}
	}
}
