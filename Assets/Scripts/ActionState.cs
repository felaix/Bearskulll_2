using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionState : MonoBehaviour
{
   
    IAction currentAction;
    public void StartAction(IAction action)
    {
        if (currentAction == action) return;

        if (currentAction != null)
        {
            currentAction.Cancel();

        }
        currentAction = action;
    }
    public void CancelCurrentAction()
    {
        if (currentAction != null)
        {
            currentAction.Cancel();
        }
    }


}
