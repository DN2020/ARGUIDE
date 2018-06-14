using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;


public class WeatherAPI : MonoBehaviour {






	string urlFormBase = "https://clampsspeech.azurewebsites.net/weather";
	string url;
	string urlBase;



	private delegate void TextDelegate(string text);
	public System.DateTime startTime;
	public System.DateTime currentTime;
	double pauseSeconds = 2;
	Coroutine coroutine;


	IDataConsumer theCaller;

	string  theRequest;
	string thePayLoad;

	bool doneProcessing = false;
	string leftover = "";

	const int MAX_LENGTH = 2000;



	void Start(){
		//	if (Application.platform == RuntimePlatform.OSXEditor) {
		//		urlBase = "http://localhost:8080/resti3/general/executeString?request=";
		//	}
	}

	public  void SendAndReceiveData(IDataConsumer caller, string request,string payload, string method){
		theCaller = caller;
		theRequest = theCaller.getRequest();

		int numChunks = payload.Length / MAX_LENGTH + 1;
		//	Debug.Log ("the length of payload is: " + payload.Length);
		thePayLoad = theCaller.getPayLoad();


		if (method.ToLower() == "post") {
			postData ();
			return;
		}

		if (payload.Length < MAX_LENGTH) {
			doneProcessing = true;
		} else {
			leftover = payload.Substring (MAX_LENGTH, payload.Length - MAX_LENGTH);
			payload = payload.Substring (0, MAX_LENGTH);
			//payload += "LARGE-" + numChunks.ToString()+ "-" + payload;
		}

		url = urlFormBase;
		print ("url: " + url);
		coroutine = StartCoroutine(DownloadTest(url, PrintText));
	}



	void postData() {
		StartCoroutine(Upload());
	}

	IEnumerator Upload() {
		//WWWForm form = new WWWForm();



		Dictionary <string, string> dict = new Dictionary<string, string>();
		dict.Add("Content-Type","application/json");


		//	form.AddField("say", thePayLoad);

		string json = "{\"say\":\"" + thePayLoad + "\"}";

		byte[] bArray = System.Text.Encoding.ASCII.GetBytes(json);

		Dictionary<string, string> headers = new Dictionary<string,string>();
		headers.Add("Content-Type", "application/json");
		WWW www = new WWW(urlFormBase, bArray, headers);
		yield return www;
		Debug.Log (www.text);
		GameObject.Find ("Holder").GetComponent<AudioController> ().PlayClipByCharacterMethod ("Frank", www.text);



		/* //	UnityWebRequest www = UnityWebRequest.Post(urlFormBase, form);
		//	yield return www.SendWebRequest();

		if(www.isNetworkError || www.isHttpError) {
			Debug.Log(www.error);
		}
		else {
			Debug.Log(www.downloadHandler.text);
			print (www.downloadHandler.text);
			Debug.Log("Form upload complete!");
		}

		*/
	}



	private IEnumerator DownloadTest(string url, TextDelegate output)
	{
		WWW www = new WWW(url);
		yield return www;
		output(www.text);
	}

	private void PrintText(string text)
	{
		if (!doneProcessing) {
			SendAndReceiveData (theCaller, theRequest, leftover,"get");
		}

		theCaller.ProcessPayload (text);

	}
}
