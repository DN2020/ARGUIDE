using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using System.Diagnostics;

using System.Text;

public class WeatherCall :  MonoBehaviour, IDataConsumer
{
	/* the java program reads the play script.
	 * It puts it in a json format of actions.
	 * 
	 * Then this class iterates through the pieces and builds the nester objects
*/

	private string thePayLoad;
	private string theRequest;



	public void callWeather(){
		RequestData ("getjunk", "say this");
	}


	public void callMP3(){
		RequestData ("getjunk", "say this");
	}

	public void callMP3WithString(string txt){
		RequestData ("getjunk", txt);
	}

	public void getPaths(){
		RequestData ("getjunk", "all");
	}

	public void getPath(string id){
		RequestData ("getjunk", id);
	}

	public void getAllMuseumLocations(){
		RequestData ("getallmuseumlocations", "");
	}



	public void getExhibitLocations(string json){
		RequestData ("getlocations", json);
	}

	public void RequestData (string request, string payload)
	{

		theRequest = request;
		thePayLoad = payload;
		GameObject.Find ("Main Camera").GetComponent<WeatherAPI> ().SendAndReceiveData (this, theRequest, thePayLoad, "get");

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
		GameObject.Find ("Holder").GetComponent<AudioController> ().PlayClipByCharacterMethod ("Frank", payload);
		print (payload);

	}
}