using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using System.Diagnostics;

using System.Text;

public class GetLUISData :  MonoBehaviour, IDataConsumer
{
	/* the java program reads the play script.
	 * It puts it in a json format of actions.
	 * 
	 * Then this class iterates through the pieces and builds the nester objects
*/

	private string thePayLoad;
	private string theRequest;

	private List<LuisStruct> luisStructs = new List<LuisStruct>();
	private Dictionary<string,LuisStruct> luisDictionary = new Dictionary<string,LuisStruct>();

	public void initLuisStucts(){

		GameObject.Find ("Holder").GetComponent<GetNMLocations> ().getNMLocationsAll ();

		LuisStruct luisStruct = new LuisStruct ();
		luisStruct.intent = "UWM - Snacks";
		luisStruct.routeID = "1";
		luisStruct.sentence = "Follow me to the treats";
		luisStructs.Add (luisStruct);
		luisDictionary [luisStruct.intent] = luisStruct;


		luisStruct = new LuisStruct ();
		luisStruct.intent = "UWM - Food Tables";
		luisStruct.routeID = "2";
		luisStruct.sentence = "Let me take you to lunch.";
		luisStructs.Add (luisStruct);
		luisDictionary [luisStruct.intent] = luisStruct;


		luisStruct = new LuisStruct ();
		luisStruct.intent = "NMW - Security to Fast Track";
		luisStruct.sentence = "Follow me to Fast Track";
		luisStruct.routeID = "3";
		luisStructs.Add (luisStruct);
		luisDictionary [luisStruct.intent] = luisStruct;


		luisStruct = new LuisStruct ();
		luisStruct.intent = "NMW - Fast Track to Restroom";
		luisStruct.sentence = "I will show you the way to the restrooms";
		luisStruct.routeID = "4";
		luisStructs.Add (luisStruct);
		luisDictionary [luisStruct.intent] = luisStruct;



		luisStruct = new LuisStruct ();
		luisStruct.intent = "NMW - Fast Track to Cafeteria";
		luisStruct.sentence = "Follow me to the cafeteria";
		luisStruct.routeID = "5";
		luisStructs.Add (luisStruct);
		luisDictionary [luisStruct.intent] = luisStruct;


		luisStruct = new LuisStruct ();
		luisStruct.intent = "NWM - Fast Track to Security";
		luisStruct.sentence = "This way to Security";
		luisStruct.routeID = "6";
		luisStructs.Add (luisStruct);
		luisDictionary [luisStruct.intent] = luisStruct;
	}

	void Start(){
		initLuisStucts ();
	}


	public void getLuisInit(){
		RequestData ("getjunk", "take me to the cafeteria");
	}

	public void getLuis(string sentence){
		RequestData ("getjunk",sentence);
	}




	public void RequestData (string request, string payload)
	{

		theRequest = request;
		thePayLoad = payload;
		GameObject.Find ("Main Camera").GetComponent<SendLUISWWW> ().SendAndReceiveData (this, theRequest, thePayLoad, "get");

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



		string top = JsonHelper2.GetJsonObject (payload, "topScoringIntent");

	//	string theintent = JsonHelper2.GetJsonObject (top, "intent".IndexOf);

		//int pos = top.Substring
	
		string[] parts = top.Split(":"[0]);

		string[] pts = parts [1].Split (","[0]);

		string intent = pts [0];
		intent = intent.Substring (2);
		intent = intent.Substring (0, intent.Length - 1);

		LuisStruct luisStruct = luisDictionary [intent];

		if (luisStruct.routeID == "5") {

			GlobalManager.lunchcube.SetActive (true);
		} else {
			GlobalManager.lunchcube.SetActive (false);
		}

		if (luisStruct.routeID == "3") {

			GlobalManager.videocuber.SetActive (true);
		} else {
			GlobalManager.videocuber.SetActive (false);
		}


		print("**********the Luis data is********: " +  luisStruct.sentence);
		//call mp3
		GameObject.Find ("Holder").GetComponent<GetNMLocations> ().getNMLocationByID (luisStruct.routeID);

		GameObject.Find ("Holder").GetComponent<MP3Call> ().callMP3WithString (luisStruct.sentence);


		//print("**********the Luis intent is********: " +  theintent);

	

	}
}