using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class LinePoint
{

	public AudioSource sample;
	// contains vector3 to for ponit location adn vector3 for velocity
	public Vector3 PointLocation{
		get;
		set;
	}

	public Vector3 Velocity{ // velocity of point
		get;
		set;

	}

	public float CreationTime {
		get;
		set;

	}
		
}
