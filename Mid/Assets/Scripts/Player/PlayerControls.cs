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
	public float moveSpeed = 1.0f;					//	Default movement speed of character

	//	Private Variables
	private Rigidbody2D _Rigidbody2D;				//	Reference to player's rigidbody
	private GameObject _ArcTriangle;					//	Shows were the player is aiming
	private Transform _Muzzle;						//	Where the bullets spawn from

	private const string ENEMY_WAVE_DESTROYED = "EnemyWaveDestroyed";

	/*--------------------------------------------------------------------------------------*/
	/*																						*/
	/*	Start: Runs once at the begining of the game. Initalizes variables.					*/
	/*																						*/
	/*--------------------------------------------------------------------------------------*/
	private void Start () 
	{
		_Rigidbody2D = GetComponent<Rigidbody2D> ();
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
	}

	/*--------------------------------------------------------------------------------------*/
	/*																						*/
	/*	Shoot: Tells Basic Bullet class to get active										*/
	/*																						*/
	/*--------------------------------------------------------------------------------------*/
	private void Shoot ()
	{
		//	Shakes camera when 
		CameraShake.CameraShakeEffect.Shake(0.1f, 0.25f);

		//	Creates a new bullet from prefab using the position of the muzzle and rotation of the reticle
		//GameObject bullet = (GameObject)Instantiate (PrefabManager.Instance.bulletPrefab, _Muzzle.position, _Reticle.transform.rotation);

		//	Adjusts the trajectory of bullet shot to account for unity's coordinates system
		//Transform adjustBullettrajectory = _Reticle.transform;
		//	Rotates
		//adjustBullettrajectory.Rotate (0.0f, 0.0f, 90.0f);
		//float theta = adjustBullettrajectory.rotation.eulerAngles.z;

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
		if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
		{
			Shoot ();
		}

	}
}
