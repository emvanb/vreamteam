using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class LinePoint: MonoBehaviour {
	private MeshRenderer tempRenderer;
	public bool animating = false;
	private float currentTime = 0;
	private Vector3 startPoint;
	private Vector3 endPoint;
	private float length;

    //This is for determining when to animate
    public float deltaT
    {
        get;
        set;
    }
    public void Start()
	{
        deltaT = 1;
		//if (tempRenderer.materials[i]!=null && tempRenderer.materials[i].mainTexture != null)

		tempRenderer = GetComponentInChildren<MeshRenderer>();

		//If we have a MeshRenderer on our object

		if (tempRenderer != null)
		{
			Debug.Log ("swap materials");
			Material quickSwapMaterial = Instantiate((tempRenderer as Renderer).materials[0]) as Material;

			//Then, set the value that we want

			tempRenderer.materials[0] = quickSwapMaterial;
		}
		animating = true;
		length = transform.localScale.z;
		currentTime = 0;
		startPoint = transform.position - transform.forward * length / 2.0f;
		endPoint = transform.position + transform.forward * length / 2.0f;
		tempRenderer.materials [0].SetFloat ("_Len", length);
		tempRenderer.materials [0].SetVector ("_Forward", new Vector4 (transform.forward.x, transform.forward.y, transform.forward.z, 0));
	}
	public void Update()
	{
		if (animating) {
			Vector3 currentPoint = startPoint;
			currentTime += Time.deltaTime/deltaT;
			currentPoint += currentTime * transform.forward;
			if (currentTime > length) {
				animating = false;
				currentTime = 0;
			}
			tempRenderer.materials [0].SetVector ("_RefPos", new Vector4 (currentPoint.x, currentPoint.y, currentPoint.z, 0));
		} else {
			tempRenderer.materials [0].SetVector ("_RefPos", new Vector4 (1000,1000,1000,0));
		}
	}
	public Vector3 convertToRGB(float height)
	{
		float h = height * 6 / 20.0f;
		float v = .5f;
		float s = .5f;
		float i = 0;
		i = (h);
		float f = (h-i);
		if(i %2==0)
		{
			f = 1-f;
		}
		float m = (float)(v * (1 - s));
		float n = (float)(v * (1 - s * f));
		switch ((int)i) {
		case 6:
			return new Vector3(0,0,0);
		case 0:
			return new Vector3 (v, n, m);
		case 1:
			return new Vector3 (n, v, m);
		case 2:
			return new Vector3 (m, v, n);
		case 3:
			return new Vector3 (m, n, v);
		case 4:
			return new Vector3 (n, m, v);
		case 5:
			return new Vector3 (v, m, n);
		}
		return new Vector3 (0, 0, 0);
	}
	public void SetHeightColor(float height)
	{
	    tempRenderer = GetComponentInChildren<MeshRenderer>();

		//If we have a MeshRenderer on our object
		Vector3 h =convertToRGB(height);
		if (tempRenderer != null)
		{
			Debug.Log ("Change color");
			Debug.Log (height / 20);
			tempRenderer.materials[0].SetVector("_OutlineColor", new Vector4(h.x,h.y,h.z,1));
			//_Color
		}
	}
	public AudioSource sample;
	// contains vector3 to for ponit location adn vector3 for velocity
    /// <summary>
    /// Thoughts: I need a list of all points, their creation times, then the shader needs to know the reference point for shading and the length. The reference point is to follow the  length of  the object,
    /// and as it  moves nearby points should color more strongly  based on distance. Normalize distance to 1- dist/length, which should give us just about what we want. Clamp at zero.
    /// </summary>
	public Vector3 pointLocation{
		get;
		set;
	}

	public Vector3 pointVelocity{ // velocity of point
		get;
		set;

	}

	public float creationTime { // creation time 
		get;
		set;

	}

}

