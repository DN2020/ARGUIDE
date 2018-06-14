using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechController : MonoBehaviour
{

	public System.DateTime startTime;
	public System.DateTime currentTime;
	public bool isEnoughPause = false;
	public string theText;
	public bool inListenMode;


	public string theSentence;
	public int theLength;
	public bool inPlayMode = false;
	public System.DateTime playStartTime;

	public static string theCallerMicSentence;

	public static bool isDiaglogueMode = false;

	public IEnumerator coroutine;

	public static string lastSentence;

	void OnGUI ()
	{


		GUI.skin.box.fontSize = 32;
		GUIStyle customButton = new GUIStyle ("button");
		customButton.fontSize = 32;


		if (inListenMode) {

			if (theText != "") {			
				GUI.Box (new Rect (10, 0, 900, 100), theText);  
			}



		}
	}



	void ReturnSpeech (string speech)
	{

		Debug.Log ("*******************  back ************************");
		print ("*****the speech is " + speech);
		speech = speech.ToLower ();
		theText = speech;




	}


	public void listenUp ()
	{



		if (inListenMode) {
			inListenMode = false;
			Debug.Log (" listen up 2");
			processSentence ();
			theText = "";
			theSentence = "";
			stopListening ();
			return;
		}
		Debug.Log (" listen up 3");
		inListenMode = true;
		Debug.Log ("*******************  listening up ************************");

		GameObject bh = GameObject.Find ("Holder");
		//bh.GetComponent<AudioClipController> ().stopAllClips ();
		theText = "";
		theSentence = "";
		//SpeechHandler.theCallerMicSentence = "";
		inPlayMode = false;
		makeTheCall ();
	}

	public void makeTheCall ()
	{


		Debug.Log ("making the first call");

		Bonjour.StartLookup ("listen", "second");
		Debug.Log ("making the first call");

	}

	public void stopListening ()
	{


		Debug.Log ("stop listening");

		Bonjour.StartLookup ("stoplisten", "second");
		Debug.Log ("making the first call");

	}

	public  void testSpeech (string speech)
	{
		ReturnSpeech (speech);
	}








	public void updateText (string txt)
	{

		startTime = System.DateTime.Now;
		/*	if (!inListenMode) {
			theText = txt;
			startListen (txt);
		}
*/

	}




	public void processSentence ()
	{
		theSentence = theText;
		handleSentence ();

	}

	public void  handleSentence ()
	{

		Debug.Log ("************in handle sentence 1 ************ with: " + theText);
		theSentence = theText;
		GameObject.Find ("Holder").GetComponent<GetLUISData> ().getLuis (theSentence);


		lastSentence = theSentence;
		theSentence = "";



	}










	/*public void undoSpeech(){
		GameObject.Find ("Main Camera").GetComponent<TheCaller> ().undo ();
	}
	*/
}




