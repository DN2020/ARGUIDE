using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {




	public static void stopClip(string goname){

		GameObject go = GameObject.Find(goname);
		if (go != null) {

			AudioSource theAudio = go.GetComponent<AudioSource> ();
			theAudio.Stop ();
		}


	}

	/*public static void stopAllClips(){

		AudioController.stopStartUps ();
		if (currentAudioList != null) {

			foreach (AudioStruct audioStruct in currentAudioList) {
				{
					if (audioStruct == null) {
						continue;
					}
					GameObject go = GameObject.Find (audioStruct.theGameObject);
					if (go == null) {
						continue;
					}
					AudioSource theAudio = go.GetComponent<AudioSource> ();
					if (theAudio != null) {
						theAudio.Stop ();
					}

				}

			}
		}
	}

*/




	public void PlayClip(string goname, string theClip){


		print ("trying to play clip " + theClip + " for " + goname);
		GameObject go = GameObject.Find(goname);
		if (go == null) {
			return;
		}
		AudioSource theAudio = go.GetComponent<AudioSource> ();
		if (theClip == "stop") {
			theAudio.Stop ();
		}
		AudioClip myClip;
		myClip  = (AudioClip)Resources.Load("audio/" + theClip);
		theAudio.clip = myClip;
		theAudio.Play ();

	}










	public void PlayClipByCharacterMethod(string charactername, string theClip){

		IEnumerator coroutine = PlayClipByCharacter (charactername, theClip);
		StartCoroutine (coroutine);
	}


	public IEnumerator PlayClipByCharacter(string charactername, string theClip) {

		GameObject frank = GameObject.Find ("Frank");
		string url = theClip;
		AudioSource audio = frank.GetComponent<AudioSource> ();


	

		using (var www = new WWW(url))
		{
			//yield return new WaitForSeconds(1.3f);

			while( !www.isDone )
				yield return null;

			//	ActionStruct actionStruct = CommandProcessor.phraseMap ["talking"];
			//		GameObject.Find ("Holder").GetComponent<AnimationUtilities> ().setAnimationByName (charactername, actionStruct);


			audio.clip = www.GetAudioClip();
			audio.Play ();
		//	print (charactername + " playing audo for " + theClip + " " + charactername + " for " + audio.clip.length + " seconds." + System.DateTime.Now);
			audio.loop = false;


		


			//yield return new WaitForSeconds(audio.clip.length);
			//yield return new WaitForSeconds(2.5f);
			print (charactername + " ended audo for " + theClip + " " + charactername + " for " + audio.clip.length + " seconds." + System.DateTime.Now);


			print (charactername + " done yielding.");

			//GlobalManager.isWorkingOnAction = false;

		}


	}








}
