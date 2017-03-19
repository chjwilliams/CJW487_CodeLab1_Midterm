using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingParticle : Particle 
{
	public float mass = 1.0f;
	public Rigidbody2D myRigidbody;

	// Use this for initialization
	void Start () 
	{
		UpdateColor();
		myRigidbody = gameObject.AddComponent<Rigidbody2D>();
		myRigidbody.mass = mass;
		myRigidbody.gravityScale = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
