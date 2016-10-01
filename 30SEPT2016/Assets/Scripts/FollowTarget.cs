using UnityEngine;
using System.Collections;

public class FollowTarget : MonoBehaviour {

	public Transform target;

	private Vector3 offset;
	private Vector3 originalPosition;

	void Start()
	{
		offset = transform.position - target.position;
		originalPosition = transform.position;
	}

	void Update ()
	{
		transform.position = target.position + offset;
	}
}
