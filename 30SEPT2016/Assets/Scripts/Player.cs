using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

	Rigidbody2D rbody;
	private float distToGround;
	public LayerMask ground;
	private Renderer render;

	public GameObject explosion;
	private Vector3 startPos;
	private GameObject explosionInstance;

	bool dead = false;

	public float respawnDelay = 1f;

	void Start ()
	{
		rbody = GetComponent<Rigidbody2D> ();
		distToGround = GetComponent<Collider2D> ().bounds.extents.y;
		render = GetComponent<Renderer> ();

		startPos = transform.position;
	}
	
	void Update () 
	{
		rbody.AddForce (Vector3.right * 2);

		if((Input.touchCount > 0 || Input.GetMouseButton(0)) && isGrounded())
		{
			print ("grounded");
			rbody.AddForce (Vector3.up * 100);
		}
	}

	bool isGrounded()
	{
		bool grounded = Physics2D.OverlapArea(new Vector2(transform.position.x - .3f, transform.position.y + .1f), new Vector2(transform.position.x + .3f, transform.position.y - .3f - distToGround), ground); 
		return grounded;
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		print ("here");
		if (collision.gameObject.layer == 9 && dead == false)
		{
			render.enabled = false;
			explosionInstance = GameObject.Instantiate (explosion, transform.position, transform.rotation) as GameObject;
			Invoke ("Respawn", respawnDelay);
			dead = true;
		}

		if (collision.gameObject.layer == 10)
		{
			SceneManager.LoadScene (2);
		}
	}

	void Respawn()
	{
		dead = false;
		render.enabled = true;
		transform.position = startPos;
		Destroy (explosionInstance);
	}
}
