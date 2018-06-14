using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;


public class SendWWW : MonoBehaviour {



	// older private string url = "http://www.injdex.com:8080/servlets-examples/servlet/RequestInfoExample?type=executeString&request=getTarget&payload=nodata";


	//var url ="http://" + document.domain + ":8080/resti3/general/" + this.ws+ "?request=" + task + "&payload=" + payload;
	string url = "http://localhost:8080/resti3/general/executeString?request=store&payload=";

	//string urlBase = "http://localhost:8080/resti3/general/executeString?request=";

	//string urlFormBase = "http://localhost:8080/resti3/general/add";

	string urlFormBase = "http://www.injdex.com:8080/servlets-examples/servlet/RequestInfoExample";


	//string urlBase = "http://clampslocation-env.eizuv4iykq.us-east-2.elasticbeanstalk.com/location/";


	//"http://clampslocation-env.eizuv4iykq.us-east-2.elasticbeanstalk.com/paths";  //all

	string urlBase = "http://clampslocation-env.eizuv4iykq.us-east-2.elasticbeanstalk.com/path";


	//   var  url = "http://www.injdex.com:8080/servlets-examples/servlet/RequestInfoExample?type=executeString&request=" + task + "&payload=" + payload;


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

		url = urlBase + payload;
		print ("url: " + url);
		coroutine = StartCoroutine(DownloadTest(url, PrintText));
	}



	void postData() {
		StartCoroutine(Upload());
	}

	IEnumerator Upload() {
		WWWForm form = new WWWForm();



		form.AddField("type", "executestring");
		form.AddField("request", theRequest);
		string fullpayload = thePayLoad;
		form.AddField("payload", fullpayload);

		UnityWebRequest www = UnityWebRequest.Post(urlFormBase, form);
		yield return www.SendWebRequest();

		if(www.isNetworkError || www.isHttpError) {
			Debug.Log(www.error);
		}
		else {
			Debug.Log("Form upload complete!");
		}
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
