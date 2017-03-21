using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*--------------------------------------------------------------------------------------*/
/*																						*/
/*	PlayerControls: Handles player state in N-gon										*/
/*			Functions:																	*/
/*					public:																*/
/*																						*/
/*					private:															*/
/*						void Start ()													*/
/*						void Move (float dx, float dy)									*/
/*						void Shoot ()													*/
/*						void Update ()													*/
/*																						*/
/*--------------------------------------------------------------------------------------*/
public class PlayerControls : MonoBehaviour 
{
	public const string PLAYER1 = "Player1";
	public const string PLAYER2 = "Player2";

	//	Public Variabels
	public float moveSpeed = 10.0f;					//	Default movement speed of character
	public float leftLimit = -12.4f;
	public float rightLimit = 16.2f;
	public float lowerLimit = -2.455001f;
	public float upperLimit = 13.39f;
	public KeyCode reversePolarity = KeyCode.Space;
	public KeyCode upKey = KeyCode.W;
	public KeyCode downKey = KeyCode.S;
	public KeyCode leftKey = KeyCode.A;
	public KeyCode rightKey = KeyCode.D;

	//	Private Variables
	private float x = 0;
	private float y = 0;
	private SpriteRenderer _MyRenderer;
	private Rigidbody2D _Rigidbody2D;				//	Reference to player's rigidbody
	private TrailRenderer _MyTrail;
	private Particle _ThisParticle;

	/*--------------------------------------------------------------------------------------*/
	/*																						*/
	/*	Start: Runs once at the begining of the game. Initalizes variables.					*/
	/*																						*/
	/*--------------------------------------------------------------------------------------*/
	private void Start () 
	{
		_Rigidbody2D = GetComponent<Rigidbody2D> ();
		_ThisParticle = GetComponent<Particle>();
		_MyRenderer = GetComponent<SpriteRenderer>();
		_MyTrail = GetComponent<TrailRenderer>();
		//_ArcTriangle = GameObject.FindGameObjectWithTag ("Reticle");
		//_Muzzle = GameObject.FindGameObjectWithTag ("Muzzle").transform;
		
	}

	/*--------------------------------------------------------------------------------------*/
	/*																						*/
	/*	Move: moves the player in a direction x and/or y based on axis input				*/
	/*		param:																			*/
	/*			float dx - horizontal axis input											*/
	/*			float dy - vertical axis input												*/
	/*																						*/
	/*--------------------------------------------------------------------------------------*/
	private void Move (KeyCode key,float dx, float dy)
	{
		if(Input.GetKey(key))
		{
			_Rigidbody2D.velocity = new Vector2(x * moveSpeed, dy * moveSpeed);
		}

		if (transform.position.x < leftLimit)
		{
			transform.position = new Vector3(leftLimit, transform.position.y, transform.position.z);
		}

		if (transform.position.x > rightLimit)
		{
			transform.position = new Vector3(rightLimit, transform.position.y, transform.position.z);
		}

		if (transform.position.y > upperLimit)
		{
			transform.position = new Vector3(transform.position.x, upperLimit, transform.position.z);
		}

		if (transform.position.y < lowerLimit)
		{
			transform.position = new Vector3(transform.position.x, lowerLimit, transform.position.z);
		}
	}

	/*--------------------------------------------------------------------------------------*/
	/*																						*/
	/*	ReversePolarity: Tells Basic Bullet class to get active								*/
	/*																						*/
	/*--------------------------------------------------------------------------------------*/
	private void ReversePolarity ()
	{
		StartCoroutine(NormalizePolarity());
	}

	IEnumerator NormalizePolarity()
	{
		_ThisParticle.charge = _ThisParticle.charge * -1.0f;
		_MyRenderer.color = _ThisParticle.nodeDischarged;
		_MyTrail.startColor = _ThisParticle.nodeDischarged;
		_MyTrail.endColor = _ThisParticle.nodeDischarged;
		yield return new WaitForSeconds(0.25f);
		_MyRenderer.color = _ThisParticle.nodeCharged;
		_MyTrail.startColor = _ThisParticle.nodeCharged;
		_MyTrail.endColor = _ThisParticle.nodeCharged;
		_ThisParticle.charge = _ThisParticle.charge * -1.0f;
	}

	/// <summary>
	/// Sent when an incoming collider makes contact with this object's
	/// collider (2D physics only).
	/// </summary>
	/// <param name="other">The Collision2D data associated with this collision.</param>
	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "MovingParticle")
		{
			
		}
	}
	
	/*--------------------------------------------------------------------------------------*/
	/*																						*/
	/*	Update: Called once per frame														*/
	/*																						*/
	/*--------------------------------------------------------------------------------------*/
	private void Update () 
	{
		switch(gameObject.tag)
		{
			case PLAYER1:
				x = Input.GetAxis ("Horizontal");
				y = Input.GetAxis ("Vertical");
				break;
			case PLAYER2:
				x = Input.GetAxis ("Player2_Horizontal");
				y = Input.GetAxis ("Player2_Vertical");
				break;
		}		

		//	Apply the values in here.
		Move (upKey, x, y);
		Move (downKey, x, y);
		Move (leftKey, x, y);
		Move (rightKey, x ,y);

		//	If Left or Right mouse button clicked, shoot
		if (Input.GetKeyDown(reversePolarity))
		{
			ReversePolarity ();
		}
	}
}