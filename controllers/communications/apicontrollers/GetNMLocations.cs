using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using System.Diagnostics;

using System.Text;

public class GetNMLocations :  MonoBehaviour, IDataConsumer
{
	/* the java program reads the play script.
	 * It puts it in a json format of actions.
	 * 
	 * Then this class iterates through the pieces and builds the nester objects
*/

	private string thePayLoad;
	private string theRequest;

	private string theCall;



	public void getNMLocations(){
		theCall = "byID";
		RequestData ("getjunk", "/1");
	}

	public void getNMLocationByID(string id){
		theCall = "byID";
		id = "/" + id;
		RequestData ("getjunk", id);
	}

	public void getNMLocationsAll(){
		theCall = "all";
		RequestData ("getjunk", "s");
	}

	public void getPaths(){
		RequestData ("getjunk", "all");
	}

	public void getPath(string id){
		RequestData ("getjunk", id);
	}




	public void getExhibitLocations(string json){
		RequestData ("getlocations", json);
	}

	public void RequestData (string request, string payload)
	{

		theRequest = request;
		thePayLoad = payload;
		GameObject.Find ("Main Camera").GetComponent<SendWWW> ().SendAndReceiveData (this, theRequest, thePayLoad, "get");

	}

	public string getPayLoad ()
	{
		return thePayLoad;
	}

	public string getRequest ()
	{
		return theRequest;
	}




	public void ProcessPayload (string payload)
	{

		//string theData = "{\"Items\":" + payload + "}";
		print (payload);

		if (theCall == "all") {
			processList (payload);
			return;
		}

		payload = payload.Replace ("items", "Items");
		string theData = payload;
		waypoint[] datalist = JsonHelper.FromJson<waypoint>(theData);

		List<waypoint> waypoints = new List<waypoint>();
		foreach (waypoint w in datalist) {
			waypoint wa = new waypoint ();
			wa.xf = float.Parse(w.x);
			wa.yf = float.Parse(w.y);
			wa.zf = float.Parse(w.z);
			waypoints.Add(wa);
		}


		//print (theData);

//	List<waypoint> waypoints = new List<waypoint>();
	/*	waypoint wa = new waypoint ();
		wa.xf = 0f;
		wa.yf = -1.1f;
		wa.zf = 0f;
		waypoints.Add(wa);

		wa = new waypoint ();
		wa.xf = 0f;
		wa.yf = -1.1f;
		wa.zf = 2.0f;
		waypoints.Add(wa);


		wa = new waypoint ();
		wa.xf = 5f;
		wa.yf = -1.1f;
		wa.zf = 2.0f;
		waypoints.Add(wa);
*/
		GameObject.Find ("Holder").GetComponent<LocationController> ().processLocations (waypoints);

	}


	private void processList(string payload){

		print (payload);
	}
}