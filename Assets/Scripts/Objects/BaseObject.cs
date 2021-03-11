using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObject : MonoBehaviour
{
    [SerializeField]
    protected HealthBase health;

    public HealthBase Health
    {
        get
        {
            return health;
        }
    }

    public bool IsAlive
    {
        get
        {
            return health.IsAlive;
        }
    }
}
