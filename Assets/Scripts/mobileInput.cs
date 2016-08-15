using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class mobileInput : MonoBehaviour
{

	//mobile device goes to: bit.ly/tailwag

	bool refresh;
	int wagging;
	bool wagBool;
	bool colBool;
	bool isPlaying;
	bool turnedBool;
	bool lookBool = false;
	bool firstTime = false;
	public bool collectOne = false;
	public Image heartImg;
	public Sprite heartGlow;
	public Sprite heartPlain;
	bool amIBeingLookedAt;
	bool lookingAway;
	public ParticleSystem particle;

	void Start ()
	{
		refresh = true;
		wagBool = false;
		isPlaying = false;
		turnedBool = false;
		string url = "http://gyro-dev.us-west-2.elasticbeanstalk.com/get-score";
		amIBeingLookedAt = false;
		lookingAway = false;
//		WWW www = new WWW (url);
		StartCoroutine (WaitForRequest (url));
	}

	IEnumerator WaitForRequest (string url)
	{
		while (refresh) {
			WWW www = new WWW (url);
			yield return www;

			// check for errors
			if (www.error == null) {
				www.data.Split (':');
				string[] split = www.data.Split (new char [] { ':', '}' });
				string tempString = split [1];
				int score;
				int.TryParse (tempString, out score);
//				Debug.Log (Mathf.Abs (score));
				wagging = Mathf.Abs (score);
				CheckState (wagging);
			} else {
				Debug.Log ("WWW Error: " + www.error);
			}

			www.Dispose ();

			yield return new WaitForSeconds (1);
		}
	}

	void Update ()
	{
		//constructing a ray before firing a raycast
		Ray ray = new Ray (Camera.main.transform.position, Camera.main.transform.forward);
		//setting up blank variable to know where we hit
		RaycastHit rayHitInfo = new RaycastHit ();
		Debug.DrawRay (ray.origin, ray.direction * 1000f, Color.yellow);
		if (Physics.Raycast (ray, out rayHitInfo, 1000f) && rayHitInfo.collider == GetComponent<Collider> ()) {
//			Debug.Log ("Raycast hit");
			Collider tempCollider = rayHitInfo.collider;
//			Debug.Log ("Collider is: " + tempCollider);
			lookBool = true;
			//	OnLooking ();
			if (!amIBeingLookedAt) {
				OnStartLook ();
				amIBeingLookedAt = true;
				lookingAway = false;
			}
		} else {
			lookBool = false;
			amIBeingLookedAt = false;
			if (!lookingAway) {
				OnNotLooking ();
				lookingAway = true;
			}
		}
	}

	void OnStartLook ()
	{
		Debug.Log ("Start looking");
		//change image
		heartImg.sprite = heartGlow;
	}

	void OnNotLooking ()
	{
		Debug.Log ("Stopped looking");
		//change image back
		heartImg.sprite = heartPlain;
		if (particle.isPlaying) {
			particle.Stop ();
		}
	}

	void CheckState (int tailwag)
	{
		if (tailwag > 10 && lookBool) {
			wagBool = true;
			GetComponent<Animator> ().SetBool ("wagBool", true);
			turnedBool = true;
		} else { 
			wagBool = false;
			GetComponent<Animator> ().SetBool ("wagBool", false);
		}
		;

//		if (!isPlaying){
//			if (wagBool = true){
//				//start animation playing
//				isPlaying = true;
//			}
//		}
		//if wagging > number {wagging bool = true}
		//if wagBool == true { start animation 
		//check for turned
		//}
	}

	void OnTriggerEnter (Collider col)
	{
		colBool = true;
		GetComponent<Animator> ().SetBool ("colBool", true);
		Debug.Log ("this is now true");

	}
}
