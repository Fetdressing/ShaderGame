using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBase : MonoBehaviour
{
    [SerializeField]
    private int baseDamage = 3;

    [SerializeField]
    private float cooldown = 1f;
    private float nextTimeReady = 0f;

    [SerializeField]
    private float attackRange = 3f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public virtual bool WithinRange(HealthBase targetHealth)
    {
        float distanceNoY = Vector3.Distance(new Vector3(this.transform.position.x, 0, this.transform.position.z), new Vector3(targetHealth.transform.position.x, 0, targetHealth.transform.position.z));
        return distanceNoY < attackRange;
    }

    public virtual void Attack(HealthBase targetHealth)
    {
        if (nextTimeReady > Time.time)
        {
            return;
        }

        targetHealth.AddHealth(-baseDamage, this);
        nextTimeReady = Time.time + cooldown;
    }
}
