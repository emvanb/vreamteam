﻿using UnityEngine;
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

    private Vector3[] colors = new Vector3[19];
    

    //This is for determining when to animate
    public float deltaT
    {
        get;
        set;
    }
    public void Start()
	{
        colors[0] = new Vector3(1.0f, 0.0f, 0.0f);
        colors[1] = new Vector3(.86f, .15f, 0.0f);
        colors[2] = new Vector3(.72f, .3f, 0);
        colors[3] = new Vector3(.58f, .44f, 0);
        colors[4] = new Vector3(.44f, .58f, 0);
        colors[5] = new Vector3(.3f, .72f, 0);
        colors[6] = new Vector3(.15f, .86f, 0.0f);
        colors[7] = new Vector3(0.0f, 1.0f, 0.0f);
        colors[8] = new Vector3(0, .86f, .15f);
        colors[9] = new Vector3(0, .72f, .3f);
        colors[10] = new Vector3(0, .58f, .44f);
        colors[11] = new Vector3(0, .44f, .58f);
        colors[12] = new Vector3(0, .3f, .72f);
        colors[13] = new Vector3(0, .15f, .86f);
        colors[14] = new Vector3(0, 0.0f, 1.0f);
        colors[15] = new Vector3(.15f, 0.0f, 1.0f);
        colors[16] = new Vector3(.3f, 0, 1.0f);
        colors[17] = new Vector3(.44f, 0, 1.0f);
        colors[18] = new Vector3(.58f, 0, 1.0f);

        deltaT = 1;
		//if (tempRenderer.materials[i]!=null && tempRenderer.materials[i].mainTexture != null)

		tempRenderer = GetComponentInChildren<MeshRenderer>();

		//If we have a MeshRenderer on our object

		if (tempRenderer != null)
		{
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
	public void UpdateStartAndEnd()
	{
		startPoint = transform.position - transform.forward * length / 2.0f;
		endPoint = transform.position + transform.forward * length / 2.0f;
		length = transform.localScale.z;
		tempRenderer.materials [0].SetFloat ("_Len", length);
		tempRenderer.materials [0].SetVector ("_Forward", new Vector4 (transform.forward.x, transform.forward.y, transform.forward.z, 0));
		animating = true;
	}
	public void Update()
	{
		if (tempRenderer != null) {
			if (animating) {
				Vector3 currentPoint = startPoint;
				currentTime += Time.deltaTime / 10.0f;
				float pointer = currentTime / length;
				currentPoint += currentTime * transform.forward;
				if (currentTime > length) {
					animating = false;
					currentTime = 0;
				}
				tempRenderer.materials [0].SetFloat ("_Sin", Mathf.Sin (Mathf.PI * pointer / 2) * 1.5f);
			} else {
				tempRenderer.materials [0].SetFloat ("_Sin", 0);
			}
		}
	}
	public Vector3 convertToRGB(int height)
	{
		return colors [Mathf.Clamp(height,0,colors.Length-1)];
	}
	public void SetHeightColor(float height)
	{
	    tempRenderer = GetComponentInChildren<MeshRenderer>();

		//If we have a MeshRenderer on our object
		Vector3 h =convertToRGB((int)height);
		if (tempRenderer != null)
		{
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

