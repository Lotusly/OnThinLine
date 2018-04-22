using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFly : MonoBehaviour
{

	[SerializeField] private Transform newTransform;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Fly()
	{
		StartCoroutine(fly());
	}

	private IEnumerator fly()
	{
		yield return null;
		Vector3 targetPosition = newTransform.localPosition;
		Quaternion targetRotation = newTransform.rotation;
		bool changePosition, changeRotation;
		changePosition = Vector3.Distance(transform.localPosition, targetPosition) > 0.1;
		changeRotation = Quaternion.Angle(transform.rotation, targetRotation) > 5;
		while (changePosition || changeRotation)
		{
			if (changePosition)
			{
				transform.localPosition += (targetPosition - transform.localPosition) * Time.deltaTime;
			}
			if (changeRotation)
			{
				transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x+(targetRotation.eulerAngles.x-transform.rotation.eulerAngles.x)*Time.deltaTime,
					transform.rotation.eulerAngles.y+(targetRotation.eulerAngles.y - transform.rotation.eulerAngles.y)*Time.deltaTime,
					transform.rotation.eulerAngles.z+(targetRotation.eulerAngles.z-transform.rotation.eulerAngles.z)*Time.deltaTime);
			}
			changePosition = Vector3.Distance(transform.localPosition, targetPosition) > 0.1;
			changeRotation = Quaternion.Angle(transform.rotation, targetRotation) > 5;
			yield return new WaitForEndOfFrame();
		}
	}
}
