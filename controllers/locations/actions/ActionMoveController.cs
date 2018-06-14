using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMoveController: MonoBehaviour  {

	private bool shouldMove = false;
	private GameObject target;
	private Transform start;
	public float speed = 1.0f;
	private float startTime;
	private float journeyLength;
	private float startspeed;

	Vector3 direction;  
	float distance;
	float time = 5.0f;//seconds no microsoft

	private static bool inMove = false;

	private int currentCnt = 0;

	private GameObject theObject;
	private List<waypoint> theWayPoints;
	private float theSeconds;

	public IEnumerator MoveOverSpeed (GameObject objectToMove, Vector3 end, float speed){

		while (objectToMove.transform.position != end)
		{
			objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, end, speed * Time.deltaTime);
			yield return new WaitForEndOfFrame ();
		}
		//ended
		doNext ();
		inMove = false;

	}
	public IEnumerator MoveOverSeconds (GameObject objectToMove, Vector3 end, float seconds)
	{
		float elapsedTime = 0;
		Vector3 startingPos = objectToMove.transform.position;
		bool arrived = false;
		while (elapsedTime < seconds  && !arrived)
		{
			print ("******* " + elapsedTime + "  " + seconds);
			objectToMove.transform.position = Vector3.Lerp(startingPos, end, (elapsedTime / seconds));
			elapsedTime += Time.deltaTime;
			yield return new WaitForEndOfFrame();

			direction = end - objectToMove.transform.position;
			distance = direction.magnitude;
			if (distance <= .4) {
				doNext ();
				arrived = true;
			}

		}
		if (!arrived) {
			objectToMove.transform.position = end;
			inMove = false;
		}


	}



	public void doMove (GameObject go,Vector3 end, float seconds){
		IEnumerator coroutine = MoveOverSeconds (go, end, seconds);
		StartCoroutine (coroutine);
	}


	public IEnumerator doMoveList (GameObject go,waypoint w, float seconds  ){


		 DebugDisplay.debug1 += "another ";

			Vector3 vend = new Vector3 (0,0,0);
			//IEnumerator coroutine = MoveOverSpeed (go, vend, seconds);


		int i = 0;
		while (i < 4) {

			inMove = true;
			
			IEnumerator coroutine = MoveOverSeconds (go, vend, seconds);



			StartCoroutine (coroutine);
			while (inMove)
				yield return null;
			i++;
			print ("********** i is  " + i);
		}
	

	}

	public void doNext(){
		currentCnt++;
		LineDrawer.deleteCubes ();
		if (currentCnt <= theWayPoints.Count - 1) {
			waypoint w = theWayPoints [currentCnt];
			w.yf = -1.1f;
			Vector3 vend = new Vector3 (w.xf, w.yf, w.zf);

			theObject.transform.LookAt (vend);

			Vector3 origin = theObject.transform.position;
			Vector3 dest = vend;

			GameObject sphere = GameObject.CreatePrimitive (PrimitiveType.Sphere);
			sphere.transform.localScale = new Vector3 (0f, 0f, 0f);
			sphere.GetComponent<Renderer> ().material.color = Color.green;
			sphere.transform.position = vend;


			GameObject.Find ("Holder").GetComponent<LineDrawer> ().drawLine (theObject, sphere);

			DebugDisplay.debug1 = "x: " + w.xf + " y: " + w.yf + " z: " + w.zf;
			//IEnumerator coroutine = MoveOverSeconds (theObject, vend, theSeconds);
			IEnumerator coroutine = MoveOverSpeed (theObject, vend, 0.6f);


			StartCoroutine (coroutine);
		} else {
			print ("done with the route");
		}

	}

		public void callMoveList (GameObject go,List<waypoint> locs, float seconds){
		theObject = go;
		theWayPoints = locs;
		currentCnt = 0;
		theSeconds = seconds;
		doNext ();





	}
}
	