using UnityEngine;
using System.Collections;

// Ugly script, just used for demo/testing purposes!
public class WalkTarget : MonoBehaviour
{
	// Set this to your player transform (or any transform you want your Zombie to turn towards).
	public Transform Player;

	// You can uncheck this in the inspector, to see how things fall apart without the awesomeness of MatchTarget.
	public bool UseTargetMatch = true;

	int State;

	void Update()
	{
		Animator animator = GetComponent<Animator>();

		// This part just waits for the walk animation to loop two full times (4 steps), then triggers a turn.
		//  In my real project I'll trigger the turns using a series of way points, not a fixed number of steps.
//		if (State == 0)
//		{
//			if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 2)
//			{
//				State = 1;
////				animator.SetTrigger("turn");
//			}
//		}

		// We are turning...
//		else if (State == 1)
//		{
			// Wait for our turn state to begin (there is a delay because I'm using blending to make things look nice)...
			if (animator.GetCurrentAnimatorStateInfo(0).IsName("turn"))
			{
				// Change to next state
//				State = 2;

				// The magic happens here, unless disabled...
				if (UseTargetMatch)
				{
					// Calculate the correct rotation needed for us to face the player/target
					Quaternion correctRot = Quaternion.LookRotation(Player.position - transform.position);

					animator.MatchTarget(

						// position not used, we only modify rotation
						Vector3.zero,

						// make target rotation the rotation needed to face player/target
						correctRot,

						// adjust the root
						AvatarTarget.Root,

						// zero weight for the position, so it doesn't get adjusted at all.  one for rotation = 100% adjustment
						new MatchTargetWeightMask(new Vector3(0, 0, 0), 1),

						// we start adjustments at the current time, "right now"
						animator.GetCurrentAnimatorStateInfo(0).normalizedTime,

						// for my animation, this is when both feet hit the ground, so at that point it shouldn't continue to rotate
						//  so I want the rotation adjustments to complete at this point in the animation (50% point).
						0.5f);
				}
			}
//		}

		// Waiting for turn to complete...
//		else if (State == 2)
//		{
			// Wait for walk state to begin...
			if (animator.GetCurrentAnimatorStateInfo(0).IsName("start_walking"))
			{
				// Change to next state -- for this test, we are done.
//				State = 3;

				// Pretty much the same thing we did for our turn.  Due to blending between our turn state and walk state, the first walk loop needs
				//  to be adjusted also, otherwise we'll no longer be facing the target.
				if (UseTargetMatch)
				{
					Quaternion correctRot = Quaternion.LookRotation(Player.position - transform.position);

					// For this one we use 1 as the end point.  At the very end of the first walk loop, we want to be facing the target.
					//  This works correctly because our walk cycle starts and ends at the same rotation point (none, or whichever direction we were facing when it
					//   started, which is typical of a good walk animation designed for mecanim).
					animator.MatchTarget(animator.targetPosition, correctRot, AvatarTarget.Root, new MatchTargetWeightMask(new Vector3(0, 0, 0), 1), animator.GetCurrentAnimatorStateInfo(0).normalizedTime, 1);
				}
			}
		}
//	}
}
