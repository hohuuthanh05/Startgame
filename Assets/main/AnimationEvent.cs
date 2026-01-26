using UnityEngine;
using System;
using System.Collections;

//Just for demonstration, you can replace it with your own code logic.
public class AnimationEvent : MonoBehaviour {

	public GameObject boss;

	private int atkTimes = 0;

	public void AttackStart () {
		Debug.Log ("Attack Start");

		//Just for demonstration, you can replace it with your own code logic.
		atkTimes++;
		if (boss && atkTimes <= 3) {
			Animator bossAnimator = boss.GetComponent<Animator> ();
			if (atkTimes == 1) {
				bossAnimator.SetTrigger ("hit_1");
			} else if (atkTimes == 2) {
				bossAnimator.SetTrigger ("hit_2");
			} else if (atkTimes == 3) {
				bossAnimator.SetTrigger ("hit_2");
				bossAnimator.SetTrigger ("death");
			} 
		}
	}

	public void AttackStartEffectObject () {
		Debug.Log ("Fire Effect Object");
	}

}
