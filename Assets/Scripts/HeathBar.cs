using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class HeathBar : MonoBehaviour
{
    public Image fillBar;
    public TextMeshProUGUI valueText1;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void UpdateHealth(int health, int maxHealth)
    {
        valueText1.text = health.ToString() + " / " + maxHealth.ToString();
        fillBar.fillAmount = (float)health / (float)maxHealth;

    }

    public void UpdateBar(int value, int maxValue, string text)
    {
        valueText1.text = text;
        fillBar.fillAmount = (float)value / (float)maxValue;
    }
}
