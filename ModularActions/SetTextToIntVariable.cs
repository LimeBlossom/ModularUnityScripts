using System.Collections;
using System.Collections.Generic;
<<<<<<< Updated upstream
using TMPro;
=======
>>>>>>> Stashed changes
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SetTextToIntVariable : MonoBehaviour, IActivatable
{
    [SerializeField] private bool onUpdate;
    [SerializeField] private Text toChange;
	[SerializeField] private IntVariable value;
    [SerializeField] private StringReference preValue;
    [SerializeField] private StringReference postValue;
	[SerializeField] private TextMeshProUGUI tmpToChange;

    public void Activate()
    {
        if (toChange != null)
        {
            toChange.text =
				preValue.value +
				value.value.ToString() +
				postValue.value;
        }
        if (tmpToChange != null)
        {
            tmpToChange.text =
				preValue.value +
				value.value.ToString() +
				postValue.value;
        }
    }

    void Update()
    {
        if (onUpdate)
        {
            Activate();
        }
    }
}
