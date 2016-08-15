using UnityEngine;
using System.Collections;

public class WalkTarget : MonoBehaviour
{
	// Set this to your player transform (or target point)
	public Transform Player;

	// You can uncheck this in the inspector, to see how things fall apart without the awesomeness of MatchTarget.
	public bool UseTargetMatch = true;

	int State;

	void Update()
	{
		Animator animator = GetComponent<Animator>();

			// Wait for our turn state to begin
			if (animator.GetCurrentAnimatorStateInfo(0).IsName("turn"))
			{

				// boolean if statement to disable match target
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

						//want the rotation adjustments to complete at this point in the animation (50% point).
						0.75f);
				}
			}

			 //Wait for walk state to begin...
			if (animator.GetCurrentAnimatorStateInfo(0).IsName("start_walking"))
			{

				// Due to blending between turn state and walk state, the first walk loop needs to be adjusted also, otherwise we'll no longer be facing the target.
				if (UseTargetMatch)
				{
					Quaternion correctRot = Quaternion.LookRotation(Player.position - transform.position);

					// For this use 1 as the end point so that NPC is facing player at the end of first walk animation loop.
					animator.MatchTarget(animator.targetPosition, correctRot, AvatarTarget.Root, new MatchTargetWeightMask(new Vector3(0, 0, 0), 1), animator.GetCurrentAnimatorStateInfo(0).normalizedTime, 1);
				}
			}
		}
}
