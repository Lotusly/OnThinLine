

using UnityEngine;

public class Missle : MonoBehaviour
{

	private Transform target;
	private float speed = 0;
	private float maxSpeed = 5;
	public bool inUse = false;
	[SerializeField] private int attack=1;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (inUse)
		{
			if (target == null) return;
			Vector3 d = target.position - transform.position;
			if (d.magnitude > 0.1f)
			{
				speed = Mathf.Min(maxSpeed, speed + maxSpeed * Time.deltaTime);
				transform.position += speed * d.normalized * Time.deltaTime;
			}
		}
	}

	public void SetTarget(Transform t)
	{
		target = t;
		
	}

	public void SetInUse()
	{
		inUse = true;
	}

	public void SetMaxSpeed(float s)
	{
		maxSpeed = s;
	}

	public void SetAttack(int i)
	{
		attack = i;
	}

	void OnTriggerEnter(Collider other)
	{
		if (inUse)
		{
			if (other.gameObject.layer.Equals(target.gameObject.layer) )
			{
				ScoreManager.instance.GetHit(other.gameObject.tag,attack);
				Explode();
			}
		}
	}

	public void Explode()
	{
		if (tag == "Player")
		{
			PlayerControl.instance.Die();
			ScoreManager.instance.ClearLife();
		}
		else Destroy(gameObject);
	}
}
