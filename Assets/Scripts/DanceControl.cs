using UnityEngine;
using System.Collections;

public class DanceControl : MonoBehaviour
{
	
	//state 1 = turning
	//state2 = walking towards
	//state 3 = stopping in front of
	//state4 = kneeling down and petting

	int state = 0;
	public int wagging;
	float timer;

	//if

	// Update is called once per frame
	void Update ()
	{
		if (state == 0) {
			//default animation
			if (wagging > 20) {
				//start timer
			} else {
				timer = 0;
			}
			;

			if (timer > 5) {
				state = 1;
			}
			;
		}
		;

		if (state == 1) {
			//do turning
			if (wagging > 20) {
				//start timer
			} else {
				timer = 0;
			}
			;

			if (timer > 5) {
				state = 2;
			}
			;
		}
		;

		if (state == 2) {
			//do turning
			if (wagging > 20) {
				//start timer
			} else {
				timer = 0;
			}
			;

			if (timer > 5) {
				state = 3;
			}
			;
		}
		;

		if (state == 3) {
			//do turning
			if (wagging > 20) {
				//start timer
			} else {
				timer = 0;
			}
			;

			if (timer > 5) {
				state = 4;
			}
			;
		}
		;

//		if (Input.GetKey (KeyCode.Space)) {
//			//then dance
//			GetComponent<Animator> ().SetBool ("IsDancing", true);
//
//		} else {
//			//don't dance
//			GetComponent<Animator> ().SetBool ("IsDancing", false);
//		}
	}
}
