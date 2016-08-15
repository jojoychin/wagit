using UnityEngine;
using System.Collections;

public class ScoreKeeper : MonoBehaviour
{
	int tempScore;
	int scoreKeeper;
	bool counted;
	string tempName;
	public ParticleSystem particles;
	float tempTime;
	float timeLeft = 5.0f;

	// Use this for initialization
	void Start ()
	{
		counted = false;
		tempScore = 0;
		scoreKeeper = 0;
		tempTime = 0;
	}

	//keeping score of who has been wooed by tail wagging
	void OnTriggerEnter (Collider col)
	{
		if (col.gameObject.tag == "people") {
			if (col.gameObject.name != tempName) {
				if (!counted) {
					scoreKeeper++;
					Debug.Log ("score is: " + scoreKeeper + " | tempName is: " + col.gameObject.name);
					tempName = col.gameObject.name;
					counted = true;
					particles.Play ();
					tempTime = Time.deltaTime;
					Debug.Log ("tempTime: "+tempTime);
				}
			} else {
				counted = false;
			}
		}
	}

	// Update is called once per frame
	void Update ()
	{

//		if (Time.deltaTime - tempTime > 10.0f && Time.deltaTime - tempTime < 0f) {
//			Debug.Log ("Particles should stop here");
//			particles.Stop ();
//		}
//		GameObject[] people = GameObject.FindGameObjectsWithTag ("people");
//		for (int i = 0; i < people.Length; i++) {
//			mobileInput mobileInput = people [i].GetComponent<mobileInput> ();
////		scoreKeeper = scoreKeeper + mobileInput.score;
////			if (mobileInput.collectOne == true) {
//			if (!counted) {
//				scoreKeeper = tempScore + mobileInput.score;
//				tempScore = 0;
//				Debug.Log ("Score got here: " + scoreKeeper);
//				counted = true;
//			}
////			}
////			Debug.Log ("Score: " + scoreKeeper);
//		}
	}
}