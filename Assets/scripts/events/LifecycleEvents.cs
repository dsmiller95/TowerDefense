using UnityEngine;
using UnityEngine.Events;

public class LifecycleEvents : MonoBehaviour
{
    public UnityEvent onStart;
    public UnityEvent onDestroy;

    private void Start()
    {
        onStart?.Invoke();
    }
    private void OnDestroy()
    {
        onDestroy?.Invoke();
    }
}
