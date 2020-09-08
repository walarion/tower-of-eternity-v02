using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCounter : MonoBehaviour
{
    public Text enemyCounterTxt;
    public int totalEnemies;

    public static EnemyCounter instance;
    private void Awake()
    {
        if (instance == null) { 
            instance = this;
            
    }
    }

    // Update is called once per frame
    void Update()
    {
       enemyCounterTxt.text ="Enimies: "+ GameManager.Instance.totalEnemies.ToString(); 
    }
}
