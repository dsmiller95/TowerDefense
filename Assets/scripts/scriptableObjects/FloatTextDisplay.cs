using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class FloatTextDisplay: MonoBehaviour
{
    public FloatVariable dataSource;
    public Text textComponent;
    public string textFormat = "# shmeckles";

    private void Awake()
    {
        dataSource.onValueChanged += OnDataChanged;
    }

    private void OnDestroy()
    {
        // must remove the listener here. if not, then the next change will try to 
        //  invoke a method on this object after it has been destroyed
        //  this displeases the Unity
        dataSource.onValueChanged -= OnDataChanged;
    }

    private void OnDataChanged()
    {
        this.textComponent.text = textFormat.Replace("#", $"{dataSource.Value:F1}");
    }
}
