using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameOverController : MonoBehaviour
{
    public FloatVariable playerHealth;
    public GameObject gameOverDisplay;

    private void Awake()
    {
        playerHealth.onValueChanged += HealthChanged;
        this.HealthChanged();
    }

    private void OnDestroy()
    {
        playerHealth.onValueChanged -= HealthChanged;
    }


    private void HealthChanged()
    {
        gameOverDisplay.SetActive(playerHealth.Value <= 0);
    }
}
