using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class ScriptableHealthComponent : MonoBehaviour, IHealthy
{
    public FloatVariable health;
    public float initialHealth = 10;

    private void Start()
    {
        // make changes to state in Start, after all listeners have been hooked up in Awake. ideally.
        health.Value = initialHealth;
    }

    public void DamageHealth(float damage)
    {
        this.health.Value -= damage;
    }
}
