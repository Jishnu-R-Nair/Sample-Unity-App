using UnityEngine;
using System.Collections;

public class EvasiveManeuver : MonoBehaviour {

	public float dodge;
	public float smoothing;
	public float tilt;

	public Vector2 startWait;
	public Vector2 maneuverTime;
	public Vector2 maneuverWait;
	public Boundary boundary;

	private Transform playerTransform;

	private Rigidbody rb;
	private float targetManuever;
	private float currentSpeed;

	void Start () 
	{
		rb = GetComponent<Rigidbody> ();
		try{
			playerTransform = GameObject.FindGameObjectWithTag ("Player").transform;
		}
		catch(System.NullReferenceException e1) {
		}
		
		currentSpeed = rb.velocity.z;
		StartCoroutine (Evade ());
	}

	IEnumerator Evade ()
	{
		yield return new WaitForSeconds (Random.Range (startWait.x, startWait.y));

		while (true) 
		{
			
			if (playerTransform != null) {
				targetManuever = playerTransform.position.x;
			} else {
				playerTransform = transform;//bullshit code
				targetManuever = Random.Range (1, dodge) * -Mathf.Sign(transform.position.x);
			}
			yield return new WaitForSeconds (Random.Range(maneuverTime.x, maneuverTime.y));
			targetManuever = 0;
			yield return new WaitForSeconds (Random.Range(maneuverWait.x, maneuverWait.y));
		}
	}

	void FixedUpdate () 
	{
		float newManuever = Mathf.MoveTowards (rb.velocity.x, targetManuever, Time.deltaTime * smoothing);
		rb.velocity = new Vector3 (newManuever, 0.0f, currentSpeed);
		rb.position = new Vector3 (
			Mathf.Clamp (rb.position.x, boundary.xMin, boundary.xMax),
			0.0f,
			Mathf.Clamp (rb.position.z, boundary.zMin, boundary.zMax)
		);
		rb.rotation = Quaternion.Euler (0.0f, 0.0f, rb.velocity.x * -tilt);
	}
}
