using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSaveData : MonoBehaviour
{
    public static PlayerSaveData instance;

    public int bees;
    public float currentMoney;
    public float persecMoney;


    private void Awake()
    {
        if (instance == null)
        {

            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else

        { 
        
            Destroy(gameObject);
        
        }

    }

    public void SavePlayerData()
    {
        PlayerPrefs.SetFloat("CurrentMoney", currentMoney);
        PlayerPrefs.SetInt("Bees", bees);
        PlayerPrefs.SetFloat("PersecMoney", persecMoney);
        PlayerPrefs.Save();
    
    
    }

    public void LoadPlayerData()
    {

        bees = PlayerPrefs.GetInt("Bees", 0);
        currentMoney = PlayerPrefs.GetFloat("CurrentMoney", 0);
        persecMoney = PlayerPrefs.GetFloat("PersecMoney", 0);

    }


}
