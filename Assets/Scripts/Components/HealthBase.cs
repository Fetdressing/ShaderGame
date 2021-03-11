using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBase : MonoBehaviour
{
    [SerializeField]
    protected int maxHealth = 100;


    [SerializeField]
    private GameObject[] spawnOnDeath;

    [SerializeField]
    private TransformFeedback animateTransform;

    public int CurrentHealth { get; protected set; }

    public bool IsAlive
    {
        get
        {
            return CurrentHealth > 0;
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        CurrentHealth = maxHealth;
    }

    public void AddHealth(int health, AttackBase affector = null)
    {
        CurrentHealth += health;
        CurrentHealth = Mathf.Min(maxHealth, CurrentHealth);

        if (health < 0)
        {
            OnDamaged(affector);
        }

        if (CurrentHealth <= 0)
        {
            Dead();
        }
    }

    private void Dead()
    {
        if (spawnOnDeath != null && spawnOnDeath.Length > 0)
        {
            for (int i = 0; i < spawnOnDeath.Length; i++)
            {
                GameObject spawned = GameObject.Instantiate(spawnOnDeath[i]);
                spawned.transform.position = this.transform.position;
                spawned.transform.localScale = this.transform.localScale;
                spawned.transform.rotation = this.transform.rotation;
            }
        }

        Destroy(this.gameObject);
    }

    private void OnDamaged(AttackBase affector)
    {
        if (affector != null)
        {
            animateTransform.DisplayHit((this.transform.position - affector.transform.position).normalized, 0.3f);
        }
    }
}
