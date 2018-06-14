using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;


public class SendLUISWWW : MonoBehaviour {





	string urlFormBase ="https://westus.api.cognitive.microsoft.com/luis/v2.0/apps/b59c997d-ccfe-4e72-8eae-a5562c8cc6fe?subscription-key=c31fd94c85a642068cac376a30df1bd1&verbose=true&timezoneOffset=0&q=";
	string url;
	//string urlFormBase ="https://westus.api.cognitive.microsoft.com/luis/v2.0/apps/b59c997d-ccfe-4e72-8eae-a5562c8cc6fe?subscription-key=c31fd94c85a642068cac376a30df1bd1&verbose=true&timezoneOffset=0&q=";


	string urlBase ="https://westus.api.cognitive.microsoft.com/luis/v2.0/apps/da01e038-9e4a-4bcc-b9f2-b5836d9a3047?subscription-key=ee48342b72c14f0b9b9488afe8b7727d&verbose=true&timezoneOffset=-360&q=";


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

		url = urlBase  + payload;
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
		print (text);
		theCaller.ProcessPayload (text);

	}
}
