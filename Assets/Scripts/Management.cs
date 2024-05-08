using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class Management : MonoBehaviour
{
    private static Management instance;

    [Header("게이지 관련")]

    [SerializeField] private float plusBee = 1f;

    [SerializeField] private Slider maxGage;

    private float maxBee = 100f;
    private float gageBee;

    [SerializeField] private Image sliderColor;
    [SerializeField] private Coroutine regendelayCoroutine;
    [SerializeField] private WaitForSeconds onesec = new WaitForSeconds(1);

    [Header("메인 돈계산")]

    [SerializeField] private TMP_Text current_beeText;
    [SerializeField] private TMP_Text current_MoneyText;
    [SerializeField] private TMP_Text current_PersecText;
    private float currentBee;
    [SerializeField] private float regenTime = 0.5f;
    private float persecMoney;
    [SerializeField] private float current_money;
    private float beeValue;
    private float beeupgradevalue = 0.05f;
    private float honeyupgradevalue = 0.1f;
    private float honeyValue = 1f;
    private float beeGrade = 1f;
    private float honeyGrade = 1f;

    [Header("업적 관련")]

    private int upgradecount;
    private float playTimeInSeconds = 0f;
    [SerializeField] private TMP_Text totalPlaytime;
 

    public static Management Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject("Management");
                instance = obj.AddComponent<Management>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }



    void Start()
    {
        LoadPlayerData();
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

    private void CalculateMoney() //돈 계산 함수
    {
        beeValue = currentBee * beeGrade * beeupgradevalue; 
        honeyValue = honeyGrade * honeyupgradevalue;
        persecMoney = beeValue + honeyValue;
    }


    //이 밑으로는 불러오기를 위한 메서드
    public float GetCurrentMoney() => current_money;

    public void SetCurrentMoney(float money) => current_money = money;

    public float Gethoneyupgradevalue() => honeyupgradevalue;

    public void Sethoneyupgradevalue(float honeyupval) => honeyupgradevalue = honeyupval;

    public float GetBeeUpVal() => beeupgradevalue;

    public void SetBeeUpVal(float beeupval) => beeupgradevalue = beeupval;

    public float GetPlusbee() => plusBee;

    public void SetPlusbee(float plusbee) => plusBee = plusbee;

    public float GetCurrentBee() => currentBee;

    public void SetCurrentBee(float curbee) => currentBee = curbee;

    public int GetUpgradeCount() => upgradecount;

    public void SetUpgradeCount(int upcount) => upgradecount = upcount;

    public float GetPlayTime() => playTimeInSeconds;

    public void SetPlayTime(float playTime) => playTimeInSeconds = playTime;

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
            gageBee -= 1;

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
    
    
    public void SavePlayerData()
    {
        // 기본 세팅값
        PlayerPrefs.SetFloat("CurrentMoney", current_money);
        PlayerPrefs.SetFloat("Bees", currentBee);
        PlayerPrefs.SetFloat("PersecMoney", persecMoney);
        PlayerPrefs.SetFloat("HoneyUpvalue", honeyupgradevalue);
        PlayerPrefs.SetFloat("BeeupValue", beeupgradevalue);
        PlayerPrefs.SetFloat("PlayTime", playTimeInSeconds);

        // 업그레이드 수치



        PlayerPrefs.Save();

    }

    public void LoadPlayerData()
    {
        // 기본 세팅값들
        currentBee = PlayerPrefs.GetInt("Bees", 0);
        current_money = PlayerPrefs.GetFloat("CurrentMoney", 0);
        persecMoney = PlayerPrefs.GetFloat("PersecMoney", 0);
        beeupgradevalue = PlayerPrefs.GetFloat("BeeupValue", 0.05f);
        honeyupgradevalue = PlayerPrefs.GetFloat("HoneyUpValue", 0.1f);
        playTimeInSeconds = PlayerPrefs.GetFloat("PlayTime", 0f);

    }

    public void DeletePlayerData()
    {

        PlayerPrefs.DeleteAll();
        Application.Quit();
    }

    public void TimeCheck()
    {

        int hours = Mathf.FloorToInt(playTimeInSeconds / 3600);
        int minutes = Mathf.FloorToInt((playTimeInSeconds % 3600) / 60);
        int seconds = Mathf.FloorToInt(playTimeInSeconds % 60);

        string formattedPlayTime = string.Format("{0}:{1:D2}:{2:D2}", hours, minutes, seconds);
        totalPlaytime.text = formattedPlayTime;
    }



    IEnumerator Regendelay() //클릭 후 리젠할때 딜레이를 주는 함수
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
    IEnumerator PersecondMoney() // 1초마다 현소지금에 초당 돈을 더해주는 함수

    {
        while (true)
        {
            yield return onesec;
            current_money += persecMoney;
            playTimeInSeconds += 1;
            TimeCheck();
            
        }
    }



}
