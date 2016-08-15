using UnityEngine;
using System.Collections;

public class RaycastGaze : MonoBehaviour
{

	bool amIBeingLookedAt = false;
	AudioSource audio;

	// Use this for initialization
	void Start ()
	{
		audio = gameObject.GetComponent<AudioSource> ();
	}

	// Update is called once per frame
	void Update ()
	{
		//constructing a ray before firing a raycast
		Ray ray = new Ray (Camera.main.transform.position, Camera.main.transform.forward);
		//setting up blank variable to know where we hit
		RaycastHit rayHitInfo = new RaycastHit ();
		Debug.DrawRay (ray.origin, ray.direction * 1000f, Color.yellow);
		if (Physics.Raycast (ray, out rayHitInfo, 1000f) && rayHitInfo.collider == GetComponent<Collider> ()) {
//			Debug.Log ("Raycast hit");
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
		if (!audio.isPlaying) {
			audio.volume = 0.2f;
			audio.Play ();
		} else if (audio.isPlaying) {
			while (audio.volume > 0.0f) {
				audio.volume -= 0.002f;
				Debug.Log (audio.volume);
			}
			if (audio.volume <= 0.0f) {
				audio.Pause ();
			}
		}
	}

	void OnLooking ()
	{ //should happen every update as long as its being looked at
		if (audio.volume < 1.0f) {
			audio.volume += 0.001f;
		}
		transform.Rotate (0f, 10f, 0f);
//		transform.localScale *= 1.05f;
	}

	void OnNotLooking ()
	{
		if (audio.isPlaying) {
			transform.Rotate (5f, 5f, 0f);
		}
//		transform.Rotate (0f, 20f, 0f);
	}
}
