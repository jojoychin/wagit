using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HeartFeedback : MonoBehaviour
{

	//raycast
	//register tag (people)
	//change UI image size when raycast hits people tag GameObjects
	bool amIBeingLookedAt = false;

	// Use this for initialization
	void Start ()
	{
		Debug.Log ("Here!");
	}
	
	// Update is called once per frame
	void Update ()
	{
		Debug.Log ("Got to HeartFeedback");
		//constructing a ray before firing a raycast
		Ray ray = new Ray (transform.position, transform.forward);
		//setting up blank variable to know where we hit
		RaycastHit rayHitInfo = new RaycastHit ();
		Debug.DrawRay (ray.origin, ray.direction * 1000f, Color.yellow);
		if (Physics.Raycast (ray, out rayHitInfo, 1000f) && rayHitInfo.collider == GetComponent<Collider> ()) {
			Debug.Log ("Raycast hit");
			OnLooking ();
			if (!amIBeingLookedAt) {
				OnStartLook ();
				amIBeingLookedAt = true;
			}
		} else {
			OnNotLooking ();
			amIBeingLookedAt = false;
		}
	}

	void OnStartLook ()
	{
		Debug.Log ("OnStartLook");
	}

	void OnLooking ()
	{ //should happen every update as long as its being looked at
		Debug.Log ("OnLooking");
	}

	void OnNotLooking ()
	{
		Debug.Log ("OnNotLooking");
	}
}
