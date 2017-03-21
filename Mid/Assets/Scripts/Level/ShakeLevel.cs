using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChrsUtils.ChrsCamera;

public class ShakeLevel : MonoBehaviour 
{

	public const string PLAYER = "Player";
	// Use this for initialization
	void Start () 
	{
		
	}

	/// <summary>
	/// Sent when another object enters a trigger collider attached to this
	/// object (2D physics only).
	/// </summary>
	/// <param name="other">The other Collider2D involved in this collision.</param>
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag.Contains(PLAYER))
		{
			CameraShake.CameraShakeEffect.Shake(0.1f, 0.25f);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
