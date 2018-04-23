

using UnityEngine;

public class Missle : MonoBehaviour
{

	private Transform target;
	private float speed = 0;
	private float maxSpeed = 5;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (target == null) return;
		Vector3 d = target.position - transform.position;
		if (d.magnitude > 0.1f)
		{
			speed = Mathf.Min(maxSpeed, speed + maxSpeed * Time.deltaTime);
			transform.position += speed*d.normalized * Time.deltaTime;
		}
	}

	public void SetTarget(Transform t)
	{
		target = t;
		
	}

	public void SetMaxSpeed(float s)
	{
		maxSpeed = s;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer.Equals(LayerMask.NameToLayer("Hitable")) && !other.tag.Equals(tag))
		{
			ScoreManager.instance.GetHit(other.gameObject.tag);
			Explode();
		}
	}

	public void Explode()
	{
		Destroy(gameObject);
	}
}
