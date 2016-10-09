using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class LinePoint: MonoBehaviour {

	public void Start()
	{
		//if (tempRenderer.materials[i]!=null && tempRenderer.materials[i].mainTexture != null)

		MeshRenderer tempRenderer = GetComponentInChildren<MeshRenderer>();

		//If we have a MeshRenderer on our object

		if (tempRenderer != null)

		{
			Debug.Log ("swap materials");
			Material quickSwapMaterial = Instantiate((tempRenderer as Renderer).materials[0]) as Material;

			//Then, set the value that we want

			quickSwapMaterial.SetFloat("_viw", 0);
			//And stick it back into our renderer. We'll do the SetVector thing every frame.

			tempRenderer.materials[0] = quickSwapMaterial;
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
		MeshRenderer tempRenderer = GetComponentInChildren<MeshRenderer>();

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

