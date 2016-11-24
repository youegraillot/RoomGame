using UnityEngine;
using System.Collections;

public class SMB_Digit : StateMachineBehaviour {

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.Rotate(new Vector3(1,0,0) * 36);
    }
}
