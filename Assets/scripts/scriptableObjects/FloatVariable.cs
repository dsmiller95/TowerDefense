using System;
using UnityEngine;

[CreateAssetMenu(fileName = "floatObject", menuName = "Scriptable/Float", order = 0)]
public class FloatVariable : ScriptableObject
{
    private float _value;
    public float Value
    {
        get => _value; set
        {
            if (_value == value)
            {
                return;
            }
            _value = value;
            onValueChanged?.Invoke();
        }
    }
    public event Action onValueChanged;

}
