using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MoveToTargetBehaviour : StateMachineBehaviour {

    GameObject gameObject;

    AI aiController;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        gameObject = animator.gameObject;
        aiController = animator.gameObject.GetComponent<AI>();

        aiController.OnTarget = false;
        aiController.MoveToTarget();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        //check Patrol Target
        if (Vector3.Distance(gameObject.transform.position, aiController.getCurrentPatrolTargetPosition()) <= 1f) animator.SetBool("onTarget", true);

        //Debug.Log(Vector3.Distance(gameObject.transform.position, aiController.getCurrentPatrolTargetPosition()));

        //check player visibility
        if (aiController.IsPlayerVisible()) animator.SetBool("isPlayerVisible", true);
    }
}