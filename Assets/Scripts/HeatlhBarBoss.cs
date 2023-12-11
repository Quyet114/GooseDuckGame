using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeatlhBarBoss : MonoBehaviour
{
    public Image fillBar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void UpdateHealth(int health, int maxHealth)
    {

        fillBar.fillAmount = (float)health / (float)maxHealth;

    }

    public void UpdateBar(int value, int maxValue)
    {
        fillBar.fillAmount = (float)value / (float)maxValue;
    }
}
