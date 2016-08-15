using UnityEngine;
using System.Collections;

using UnityEngine.VR;

public class VRUtility : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		//set render settings lower or higher; by default = 1; lower will improve frame rate
		//.50 might be good
		VRSettings.renderScale = 0.5f;

	
	}
	
	// Update is called once per frame
	void Update ()
	{
		//sets forward wherever you're looking
		if (Input.GetKeyDown (KeyCode.R)) {
			InputTracking.Recenter ();
		}
	}
}