using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class set_twinkle : StateMachineBehaviour
{
    public GameObject twinkle;

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
