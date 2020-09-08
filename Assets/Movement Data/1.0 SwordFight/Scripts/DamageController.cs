using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageController : MonoBehaviour
{
    public HealthBar healthBar;
    [Range(50,200)]
    public float totalHealth = 100f;

    private float myTotalHealth;
    public bool isDead = false;

    private void Start()
    {
        Init();
    }
    void Init() {
        myTotalHealth = totalHealth;
        healthBar.Init(totalHealth);
        isDead = false;
    }

    public void Damage(float DmgValue) {
        if (myTotalHealth < 0)
        {
            return;
        }
        myTotalHealth -= DmgValue;
        this.SendMessage("OnAware", SendMessageOptions.RequireReceiver);
        if (myTotalHealth < 0) { 
            myTotalHealth = 0;
            Death();
        }

        float fillerAmount = 1.0f - (totalHealth - myTotalHealth) / totalHealth;
        healthBar.UpdateHealthBar(fillerAmount);
    }
    void Death() {
        // destroy heathbar
        // add count if enemy
        isDead = true;
        // play death animation and remove player.
        this.SendMessage("Dead", SendMessageOptions.RequireReceiver);
        healthBar.HideBar();
    }
    
}
