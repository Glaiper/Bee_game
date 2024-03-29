using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class Management : MonoBehaviour
{
    [SerializeField] private int plusBee = 1;


    [SerializeField] public static Management instance;

    [SerializeField] private TMP_Text current_beeText;
    [SerializeField] private TMP_Text current_MoneyText;
    [SerializeField] private TMP_Text current_PersecText;

    [SerializeField] private Slider maxGage;

    [SerializeField] private float maxBee = 100f;
    [SerializeField] private float gageBee;
    [SerializeField] private float currentBee;
    [SerializeField] private float regenTime = 0.5f;
    [SerializeField] private float persecMoney;
    [SerializeField] private float current_money = 0;
    [SerializeField] private float beeValue;
    private float upgradevalue = 0.05f;

    [SerializeField] private Image sliderColor;

    [SerializeField] private bool regenCooldown;

    [SerializeField] private Coroutine regendelayCoroutine;
    [SerializeField] private WaitForSeconds onesec = new WaitForSeconds(1);




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

    void Start()
    {
        LoadMoney();
        gageBee = maxBee;
        StartCoroutine(PersecondMoney());
    }

    // Update is called once per frame
    void Update()
    {
        CurrentGage();
        CalculateMoney();
        CurrentTextshow();
    }

    private void CalculateMoney()
    {
        beeValue = currentBee * upgradevalue;
        persecMoney = beeValue;

    }

    private void CurrentTextshow()
    {
        current_beeText.text = currentBee.ToString();

        current_MoneyText.text = current_money.ToString("F3");
        string moneyString = current_MoneyText.text;
        if (moneyString.EndsWith(".00") || moneyString.EndsWith(".0"))
        {
            current_MoneyText.text = current_money.ToString("F0");
        }
        else if (moneyString.EndsWith("00"))
        {
            current_MoneyText.text = current_money.ToString("F1");
        }
        else if (moneyString.EndsWith("0"))
        {
            current_MoneyText.text = current_money.ToString("F2");
        }

        current_PersecText.text = persecMoney.ToString("F3") + " /per sec";
        string persecString = persecMoney.ToString("F3") + " /per sec";
        if (persecString.EndsWith(".00 /per sec") || persecString.EndsWith(".0 /per sec"))
        {
            current_PersecText.text = persecMoney.ToString("F0") + " /per sec";
        }
        else if (persecString.EndsWith("00 /per sec"))
        {
            current_PersecText.text = persecMoney.ToString("F1") + " /per sec";
        }
        else if (persecString.EndsWith("0 /per sec"))
        {
            current_PersecText.text = persecMoney.ToString("F2") + " /per sec";
        }
    }


    public void PlusBee() //버튼 클릭시 전투력 상승
    {

        if (gageBee == 0)
        {
            // 더이상 효과가 발동하지 않도록 함
        }
        else
        {
            currentBee += plusBee;
            gageBee -= 1f;

            if (regendelayCoroutine != null)
            {

                StopCoroutine(regendelayCoroutine);

            }
            regendelayCoroutine = StartCoroutine(Regendelay());

        }

    }

    private void CurrentGage() //남은 게이지 양을 계산
    {

        maxGage.value = gageBee / maxBee;

        if (maxGage.value == 0f) //0일경우 투명화
        {

            sliderColor.color = Color.clear;

        }

        else if (maxGage.value <= 0.25f) // 25%이하일경우 붉은색
        {
            sliderColor.color = Color.red;

        }

        else //25%초과일경우 초록색
        {
            sliderColor.color = Color.green;
        }
    }

    private void SaveMoney()
    {

        PlayerPrefs.SetFloat("CurrentMoney", current_money);
        PlayerPrefs.Save();

    }

    private void LoadMoney()
    {

        if (PlayerPrefs.HasKey("CurrentMoney"))
        {

            current_money = PlayerPrefs.GetFloat("CurrentMoney");
        }
        else
        {

            current_money = 0;

        }
    
    }

    IEnumerator Regendelay() 
    {
        float regentime = 0f;
        while (regentime < 0.5f)
        { 
        
            regentime += Time.deltaTime;
            yield return null;

        }

        while (gageBee < maxBee)
        {

            gageBee += 1f;
            yield return new WaitForSeconds(regenTime);

        }


    }
    IEnumerator PersecondMoney()

    {
        while (true)
        {
            yield return onesec;
            current_money += persecMoney;
            SaveMoney();
        }


    }



}
