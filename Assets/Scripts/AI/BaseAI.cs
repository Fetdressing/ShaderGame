using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseAI : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent agent;
    [SerializeField]
    private Rigidbody body;

    [SerializeField]
    protected AttackBase attack;
    [SerializeField]
    protected HealthBase health;

    [SerializeField]
    protected float speed = 10;


    // States
    public bool IsBusy { get; protected set; }

    public Rigidbody Body
    {
        get
        {
            return body;
        }
    }

    public Collider Collider { get; private set; }

    public Bounds Bounds
    {
        get
        {
            return Collider.bounds;
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        Collider = Body.GetComponent<Collider>();
    }

    private void Update()
    {
        OnUpdate();
    }

    protected virtual void OnUpdate()
    {
        float distanceToAgent = Vector3.Distance(body.transform.position, agent.transform.position) + 0.0001f;
        agent.speed = speed * (1 - Mathf.Min(1, distanceToAgent / 3f)); // Decrease agent speed depending on how far away it is. So the agent doesn't run off.

        Vector3 rigidbodyMoveDir = (agent.transform.position - body.transform.position).normalized;
        body.velocity = (distanceToAgent > 0.1f ? rigidbodyMoveDir : Vector3.zero) * speed;

        IsBusy = agent.hasPath;
        if (!IsBusy)
        {
            MoveTo(new Vector3(Random.Range(-20, 20), Random.Range(-20, 20), Random.Range(-20, 20)));
        }
    }

    protected T FindCloseObject<T>(bool onlyAlive = true) where T : BaseObject
    {
        T potObject = null;
        float checkRadius = 10f;
        int nrTries = 0;
        const int maxTries = 10;
        while (potObject == null && nrTries < maxTries)
        {
            Collider[] potCols = Physics.OverlapSphere(this.transform.position, checkRadius);
            for (int i = 0; i < potCols.Length; i++)
            {
                potObject = potCols[i].GetComponent<T>();
                if (potObject != null && (!onlyAlive || potObject.IsAlive))
                {
                    break;
                }
            }

            checkRadius += 15f;
            nrTries++;
        }

        return potObject;
    }

    private void OnValidate()
    {
        if (agent != null && agent.transform == this.transform)
        {
            Debug.LogError("Agent transform is the same as this transform, not allowed.");
            agent = null;
        }

        if (body != null && body.transform == this.transform)
        {
            Debug.LogError("Rigidbody transform is the same as this transform, not allowed.");
            body = null;
        }
    }

    public void MoveTo(Vector3 pos)
    {
        agent.SetDestination(pos);
    }

    public void Stop()
    {
        agent.ResetPath();
        body.velocity = Vector3.zero;
    }
}
