using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDrawer : MonoBehaviour {

	private Transform origin;
	private Transform destination;
	//private LineRenderer lineRenderer;
	private float counter=0;
	private float dist;
	private float lineDrawSpeed = 1f;
	private bool canDrawLine = false;

	public static List<GameObject> cubeList = new List<GameObject>();  

	// Use this for initialization
	public void drawLine (GameObject baggie,GameObject target) {
		cubeList = new List<GameObject>();
		GameObject go =  GameObject.Find("Frank");
		origin = go.transform;
		destination = target.transform;
		//lineRenderer = GetComponent<LineRenderer>();
		//lineRenderer.SetPosition(0,origin.position);
		//lineRenderer.SetWidth (.45f, .45f);
		canDrawLine = true;
		counter = 0;
		dist = Vector3.Distance (origin.position, destination.position);

	}

	public void calcHalf(){
		float x = dist / 2;
		Vector3 pointA = origin.position;
		//pointA = new Vector3(pointA.x,-10f,pointA.y);
		Vector3 pointB = destination.position;
		Vector3 pointAlongLine = x * Vector3.Normalize (pointB - pointA) + pointA;
		GameObject a = GameObject.Find ("arrows");
		a.transform.localPosition = pointAlongLine;

	}

	public static void deleteCubes(){
		foreach (GameObject cube in cubeList) {
			Destroy (cube);
		}

	}

	// Update is called once per frame
	void LateUpdate () {
		if (canDrawLine) {

			if (counter < dist) {

				counter += .1f / lineDrawSpeed;
				float x = Mathf.Lerp (0, dist, counter);
				Vector3 pointA = origin.position;
				//pointA = new Vector3(pointA.x,-10f,pointA.y);
				Vector3 pointB = destination.position;
				//	pointB = new Vector3(pointB.x,-10f,pointB.y);

				Vector3 pointAlongLine = x * Vector3.Normalize (pointB - pointA) + pointA;

				//	GameObject a = GameObject.Find ("arrows");
				//	a.transform.position = pointAlongLine;
				//transform.Rotate(Time.deltaTime, 0, 0);

				GameObject cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
				cube.GetComponent<MeshRenderer> ().material.color = Color.red;
				cube.transform.localScale = new Vector3 (.1f, .1f, .1f);
				cube.transform.position = pointAlongLine;
				cubeList.Add (cube);

				/*	Vector3 direction = destination.position - origin.position;
				Quaternion qd = Quaternion.LookRotation(direction);
				a.transform.rotation = qd;
				a.transform.localScale = new Vector3(1,1,direction.magnitude);
				a.transform.Rotate (0, 0, 90);
*/


				//	lineRenderer.SetPosition (1, pointAlongLine);



			} 
		}
	}
}
