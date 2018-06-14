using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IOSCaller : MonoBehaviour {

	public IEnumerator coroutine;

	private static string theFileName;


	private static bool isMac;

	void Start(){	

		if (Application.platform == RuntimePlatform.OSXEditor) {
			isMac = true;
		} else {
			isMac = true;
		}
	}






	public void makeTheCall(string service, string domain){
		//if (!isMac) {

		Bonjour.StartLookup (service, domain);
		//}
	}




	public void recordWithName(string filename){
		theFileName = filename;
		string txt;
		makeTheCall ("record", filename);


	}

	public void stopandUpLoadwithName(){
		IEnumerator coroutine;

		coroutine = Wait (1f);
		StartCoroutine (coroutine);

	}

	IEnumerator Wait(float duration)
	{
		//This is a coroutine
		Debug.Log("Start Wait() function. The time is: "+Time.time);
		Debug.Log( "Float duration = "+duration);
		makeTheCall ("stoprecording", "domain");
		yield return new WaitForSeconds(duration);   //Wait
		Debug.Log("End Wait() function and the time is: "+Time.time);


	}

	public void turnOnVideoRecord(){


		Bonjour.StartLookup ("VideoRecordOn", "second");

	}

	public void turnOffVideoRecord(){

		Bonjour.StartLookup ("VideoRecordOff", "second");

	}



}
