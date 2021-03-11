using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerAI : BaseAI
{
    private enum TaskType
    {
        CutTrees,
        Repair,
        Build
    }

    private TaskType currentTask = TaskType.CutTrees;
    private BaseObject currentInteractObj;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected override void OnUpdate()
    {
        base.OnUpdate();
        HandleCutTrees();
    }

    private void HandleCutTrees()
    {
        if (currentInteractObj == null || !(currentInteractObj is Tree))
        {
            Stop();
            currentInteractObj = FindCloseObject<Tree>();
        }
        else
        {
            Tree tree = (Tree)currentInteractObj;

            if (!tree.IsAlive)
            {
                currentInteractObj = null;
            }
            else
            {
                if (attack.WithinRange(tree.Health))
                {
                    Stop();
                    attack.Attack(tree.Health);
                }
                else
                {
                    if (IsBusy == false)
                    {
                        MoveTo(tree.transform.position);
                    }
                }
            }
        }
    }

    private void FindTask()
    {

    }
}
