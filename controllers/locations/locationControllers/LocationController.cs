using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationController : MonoBehaviour {

	GameObject frank;

	// Use this for initialization
	void Start () {
		frank = GameObject.Find ("Frank");
	}


	public void doInit(){
		Vector3 v = new Vector3 (0, 0, 5);
		GameObject.Find ("Main Camera").GetComponent<ActionMoveController>().doMove(frank,v,11.0f);
	}

	public void processLocations (List<waypoint> locs){
		foreach (waypoint w in locs) {

		//	print (w.xf + " " + w.yf + " " + w.zf);

		}

		GameObject.Find ("Holder").GetComponent<ActionMoveController> ().callMoveList (frank, locs, 5.0f);


	}


	// Update is called once per frame
	void Update () {
		
	}
}
