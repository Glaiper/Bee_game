using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class Management : MonoBehaviour
{

    [Header("������ ����")]

    [SerializeField] private int plusBee = 1;

    [SerializeField] private Slider maxGage;

    [SerializeField] private float maxBee = 100f;
    [SerializeField] private float gageBee;

    [SerializeField] private Image sliderColor;

    [SerializeField] private Coroutine regendelayCoroutine;
    [SerializeField] private WaitForSeconds onesec = new WaitForSeconds(1);

    [Header("���� �����")]

    [SerializeField] private TMP_Text current_beeText;
    [SerializeField] private TMP_Text current_MoneyText;
    [SerializeField] private TMP_Text current_PersecText;
    private float currentBee;
    [SerializeField] private float regenTime = 0.5f;
    private float persecMoney;
    private float current_money;
    private float beeValue;
    private float beeupgradevalue = 0.05f;
    private float honeyupgradevalue = 0.1f;
    private float honeyValue = 1f;
    private float beeGrade = 1f;
    private float honeyGrade = 1f;

    [Header("1Ƽ�� ���׷��̵�")]
    [SerializeField] private int firstUpnum = 1;
    [SerializeField] private Button firstbutton;
    [SerializeField] private float upcostone = 10f;
    [SerializeField] private TMP_Text onetext;



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

    private void CalculateMoney()
    {
        beeValue = currentBee * beeGrade * beeupgradevalue;
        honeyValue = honeyGrade * honeyupgradevalue;
        persecMoney = beeValue + honeyValue;

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


    public void PlusBee() //��ư Ŭ���� ������ ���
    {

        if (gageBee == 0)
        {
            // ���̻� ȿ���� �ߵ����� �ʵ��� ��
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

    private void CurrentGage() //���� ������ ���� ���
    {

        maxGage.value = gageBee / maxBee;

        if (maxGage.value == 0f) //0�ϰ�� ����ȭ
        {

            sliderColor.color = Color.clear;

        }

        else if (maxGage.value <= 0.25f) // 25%�����ϰ�� ������
        {
            sliderColor.color = Color.red;

        }

        else //25%�ʰ��ϰ�� �ʷϻ�
        {
            sliderColor.color = Color.green;
        }
    }


    public void SavePlayerData()
    {
        PlayerPrefs.SetFloat("CurrentMoney", current_money);
        PlayerPrefs.SetFloat("Bees", currentBee);
        PlayerPrefs.SetFloat("PersecMoney", persecMoney);
        PlayerPrefs.Save();


    }

    public void LoadPlayerData()
    {

        currentBee = PlayerPrefs.GetFloat("Bees", 0);
        current_money = PlayerPrefs.GetFloat("CurrentMoney", 0);
        persecMoney = PlayerPrefs.GetFloat("PersecMoney", 0);

    }

    public void DeletePlayerData()
    {
        PlayerPrefs.DeleteKey("CurrentMoney");
        PlayerPrefs.DeleteKey("Bees");
        PlayerPrefs.DeleteKey("PersecMoney");
        ReloadCurrentScene();
    }

    public void ReloadCurrentScene()
    { 
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    

    }

    public void Firstupgrade()
    {

        if (firstUpnum == 30)

        {

            firstbutton.interactable = false;

        }

        else if (current_money >= upcostone)

        {

            current_money -= upcostone;

            honeyupgradevalue += 0.1f;
            upcostone = upcostone + (upcostone * 0.1f);
            onetext.text = upcostone.ToString("F1");
            firstUpnum++;

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

        }


    }



}
