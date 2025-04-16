using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class UIHealthBarManager : UIBarManager
{

    protected override void UpdateBar()
    {
        _percentage = (float)_protagonistStats.CurrentHealth / _protagonistStats.MaxHealth;
        Debug.Log(_percentage);
        StartCoroutine(SmoothBar());

        _text.SetText(Mathf.FloorToInt((float)_protagonistStats.CurrentHealth).ToString());
    }

}
