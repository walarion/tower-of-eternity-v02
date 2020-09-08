using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float totalPlayerHealth = 100;
    public Image healthBarFiller;
    public Text healthBarPercentage;
    public void Init(float tHealth)
    {
        totalPlayerHealth = tHealth;
        healthBarFiller.fillAmount = 1f;
        healthBarPercentage.text = tHealth.ToString() + "%";
    }
    public void UpdateHealthBar(float fillAmount) {
        healthBarFiller.fillAmount =fillAmount;
        healthBarPercentage.text = (healthBarFiller.fillAmount * 100f).ToString() + "%";
    }
}
