using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class HealthComponent : MonoBehaviour, IHealthy
{
    public float health;

    public void DamageHealth(float damage)
    {
        this.health -= damage;

        OnHealthChanged();
    }

    private void OnHealthChanged()
    {
        if(health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
