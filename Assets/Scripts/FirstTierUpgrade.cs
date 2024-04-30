using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UI;

public class FirstTierUpgrade : MonoBehaviour
{
    [Header("1단계 업그레이드")]
    [SerializeField] private float firstUpnum = 1f;
    [SerializeField] private Button firstbutton;
    [SerializeField] private float upcostone = 10f;
    [SerializeField] private TMP_Text onetext;
    [SerializeField] private Slider oneSlider;
    private bool oneover = false;


    [Header("2단계 업그레이드")]
    [SerializeField] private float secUpnum = 1f;
    [SerializeField] private Button secbutton;
    [SerializeField] private float upcosttwo = 100f;
    [SerializeField] private TMP_Text twotext;
    [SerializeField] private Slider twoSlider;
    private bool twoover = false;

    [Header("3단계 업그레이드")]
    [SerializeField] private float thirdUpnum = 1f;
    [SerializeField] private Button thirdbutton;
    [SerializeField] private float upcostthree = 1500f;
    [SerializeField] private TMP_Text thirdtext;
    [SerializeField] private Slider thirdSlider;
    private bool thirdover = false;

    void Start()
    {
        LoadPlayerData();
        FirstUpgradeCheck();
        SecondeUpgradeCheck();
        ThirdUpgradeCheck();

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Firstupgrade() // 1티어 1번째 업그레이드
    {
        Management manager = Management.Instance;
        float current_money = manager.GetCurrentMoney();
        float honeyupgradevalue = manager.Gethoneyupgradevalue();

        oneSlider.value = firstUpnum / 50f;

        if (oneover)

        {
            firstbutton.interactable = false;
            onetext.text = "Max!";
        }

        else if (current_money >= upcostone)

        {
            current_money -= upcostone;
            manager.SetCurrentMoney(current_money);
            honeyupgradevalue += 0.1f;
            manager.Sethoneyupgradevalue(honeyupgradevalue);
            upcostone = upcostone + (upcostone * 0.1f);
            onetext.text = upcostone.ToString("F1");
            firstUpnum++;

            if (firstUpnum == 50)
            { oneover = true; }
        }
    }

    public void Secondepgrade() // 1티어 2번째 업그레이드
    {
        twoSlider.value = secUpnum / 30f;
        Management manager = Management.Instance;
        float current_money = manager.GetCurrentMoney();
        float beeupgradevalue = manager.GetBeeUpVal();
        if (twoover)
        {
            secbutton.interactable = false;
            twotext.text = "Max!";

        }

        else if (current_money >= upcosttwo)

        {
            current_money -= upcosttwo;
            manager.SetCurrentMoney(current_money);
            beeupgradevalue += 0.1f;
            manager.SetBeeUpVal(beeupgradevalue);
            upcosttwo = upcosttwo + (upcosttwo * 0.2f);
            twotext.text = upcosttwo.ToString("F1");
            secUpnum++;

            if (secUpnum == 30)
            { twoover = true; }
        }
    }

    public void Thirdpgrade() // 1티어 3번째 업그레이드
    {
        Management manager = Management.Instance;
        float current_money = manager.GetCurrentMoney();
        float plusBee = manager.GetPlusbee();
        thirdSlider.value = thirdUpnum / 8f;

        if (thirdover)

        {
            thirdbutton.interactable = false;
            thirdtext.text = "Max!";

        }

        else if (current_money >= upcostthree)

        {
            current_money -= upcostthree;
            manager.SetCurrentMoney(current_money);
            plusBee += 0.1f;
            manager.SetPlusbee(plusBee);
            upcostthree = upcostthree + (upcostthree * 0.2f);
            thirdtext.text = upcostthree.ToString("F1");
            thirdUpnum++;

            if (thirdUpnum == 8)

            { thirdover = true; }
        }

    }

    public void FirstUpgradeCheck()
    {
        oneSlider.value = firstUpnum / 50f;
        onetext.text = upcostone.ToString("F1");
        if (oneover)
        {
            firstbutton.interactable = false;
            onetext.text = "Max!";
        }
    }

    public void SecondeUpgradeCheck()
    {
        twoSlider.value = secUpnum / 30f;
        twotext.text = upcosttwo.ToString("F1");
        if (twoover)
        {
            secbutton.interactable = false;
            twotext.text = "Max!";
        }
    }

    public void ThirdUpgradeCheck()
    {
        thirdSlider.value = thirdUpnum / 8f;
        thirdtext.text = upcostthree.ToString("F1");
        if (thirdover)
        {
            thirdbutton.interactable = false;
            thirdtext.text = "Max!";
        }
    }

    public void SavePlayerData()

    {
        
        PlayerPrefs.SetFloat("FirstUpnum", firstUpnum);
        PlayerPrefs.SetFloat("Upcostone", upcostone);
        PlayerPrefs.SetFloat("Secupnum", secUpnum);
        PlayerPrefs.SetFloat("Upcosttow", upcosttwo);
        PlayerPrefs.SetFloat("ThirdUpnum", thirdUpnum);
        PlayerPrefs.SetFloat("UpcostThree", upcostthree);
        PlayerPrefs.SetInt("OneOver", oneover ? 1 : 0); // 1은 true , 0은 false
        PlayerPrefs.SetInt("TwoOver", twoover ? 1 : 0);
        PlayerPrefs.SetInt("ThreeOver", thirdover ? 1 : 0);

        PlayerPrefs.Save();

    }
    public void LoadPlayerData()
    {

        //업그레이드 수치 
        firstUpnum = PlayerPrefs.GetFloat("FirstUpnum", 1);
        upcostone = PlayerPrefs.GetFloat("Upcostone", 1);
        secUpnum = PlayerPrefs.GetFloat("Secupnum", 1);
        upcosttwo = PlayerPrefs.GetFloat("Upcosttow", 100);
        thirdUpnum = PlayerPrefs.GetFloat("ThirdUpnum", 1);
        upcostthree = PlayerPrefs.GetFloat("UpcostThree", 1500);
        oneover = PlayerPrefs.GetInt("OneOver", 0) == 1;
        twoover = PlayerPrefs.GetInt("TwoOver", 0) == 1;
        thirdover = PlayerPrefs.GetInt("ThreeOver", 0) == 1;

    }

}
