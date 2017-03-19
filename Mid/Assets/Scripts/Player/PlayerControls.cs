using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ChrsUtils.ChrsCamera;
using ChrsUtils.ChrsPrefabManager;

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
	//	Public Variabels
	public float moveSpeed = 10.0f;					//	Default movement speed of character
	public float leftLimit = -12.4f;
	public float rightLimit = 16.2f;
	public KeyCode shakeScreen = KeyCode.Space;

	//	Private Variables
	private SpriteRenderer _MyRenderer;
	private Rigidbody2D _Rigidbody2D;				//	Reference to player's rigidbody
	private TrailRenderer _MyTrail;
	private GameObject _ArcTriangle;					//	Shows were the player is aiming
	private Transform _Muzzle;						//	Where the bullets spawn from
	private Particle _ThisParticle;

	private const string ENEMY_WAVE_DESTROYED = "EnemyWaveDestroyed";

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

	protected virtual void OnPlayerIsDead()
	{

	}



	/*--------------------------------------------------------------------------------------*/
	/*																						*/
	/*	Move: moves the player in a direction x and/or y based on axis input				*/
	/*		param:																			*/
	/*			float dx - horizontal axis input											*/
	/*			float dy - vertical axis input												*/
	/*																						*/
	/*--------------------------------------------------------------------------------------*/
	private void Move (float dx, float dy)
	{
		_Rigidbody2D.velocity = new Vector2 (dx * moveSpeed, dy * moveSpeed);

		if (transform.position.x < leftLimit)
		{
			transform.position = new Vector3(leftLimit, transform.position.y, transform.position.z);
		}

		if (transform.position.x > rightLimit)
		{
			transform.position = new Vector3(rightLimit, transform.position.y, transform.position.z);
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
		_MyRenderer.color = Color.white;
		_MyTrail.startColor = Color.white;
		_MyTrail.endColor = Color.white;
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
			Debug.Log("Hit");
		}
	}
	
	/*--------------------------------------------------------------------------------------*/
	/*																						*/
	/*	Update: Called once per frame														*/
	/*																						*/
	/*--------------------------------------------------------------------------------------*/
	private void Update () 
	{
		//	Take in a float value from Input axes
		float x = Input.GetAxis ("Horizontal");
		float y = Input.GetAxis ("Vertical");

		//	Apply the values in here.
		Move (x, y);

		//	If Left or Right mouse button clicked, shoot
		if (Input.GetKeyDown(shakeScreen))
		{
			ReversePolarity ();
		}

	}
}
