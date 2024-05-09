using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using static ChallangeManager;

public class ChallangeManager : MonoBehaviour
{
    public static ChallangeManager instance;
    public ChallngePopup challengePopup;

    public List<Challenge> allChallenges = new List<Challenge>();

    [Header("도전과제 아이콘 지정")]
    public Sprite c1Icon;
    public Sprite c2Icon;
    public Sprite c3Icon;

    public enum ConditionType
    {
        BeeCount,
        MoneyAmount,
        UpgradeCount,
        PlayTime
    }

    public class ChallengeCondition
    {
        public ConditionType conditionType;
        public int targetValue;

        public ChallengeCondition(ConditionType type, int value)
        {
            conditionType = type;
            targetValue = value;
        }

        public bool IsCompleted(int currentValue)
        {
            switch (conditionType)
            {
                case ConditionType.BeeCount:
                    return currentValue >= targetValue;
                case ConditionType.MoneyAmount:
                    return currentValue >= targetValue;
                case ConditionType.UpgradeCount:
                    return currentValue >= targetValue;
                case ConditionType.PlayTime:
                    return currentValue >= targetValue;
                default:
                    return false;
            }
        }

    }

    public class Challenge
    {
        public string title;
        public string description;
        public bool isCompleted;
        public List<ChallengeCondition> conditions;
        public Sprite challengeIcon;

        public Challenge(string title, string description, Sprite icon)
        {
            this.title = title;
            this.description = description;
            isCompleted = false;
            conditions = new List<ChallengeCondition>();
            challengeIcon = icon;
        }

        public Sprite GetChallengeIcon()
        {
            return challengeIcon;
        }

        public void AddCondition(ChallengeCondition condition)
        {
            conditions.Add(condition);
        }

        public bool IsCompleted(float beeCount, float moneyAmount, int upgradeCount, float playTime)
        {
            foreach (var condition in conditions)
            {
                switch (condition.conditionType)
                {
                    case ConditionType.BeeCount:
                        if (!condition.IsCompleted((int)beeCount))
                            return false;
                        break;
                    case ConditionType.MoneyAmount:
                        if (!condition.IsCompleted((int)moneyAmount))
                            return false;
                        break;
                    case ConditionType.UpgradeCount:
                        if (!condition.IsCompleted(upgradeCount))
                            return false;
                        break;
                    case ConditionType.PlayTime:
                        if (!condition.IsCompleted((int)playTime))
                            return false;
                        break;
                    default:
                        return false;
                }
            }

            return true;
        }

    }

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

    private void Start()
    {
        LoadIcon();
        CreateChallenge();
        StartCoroutine(CheckTime()); // 코루틴 시작
    }
    private void LoadIcon()
    {
        c1Icon = Resources.Load<Sprite>("Image/Cutebee");
        c2Icon = Resources.Load<Sprite>("Image/Coin");
        c3Icon = Resources.Load<Sprite>("Image/Honey");

    }

    private void CreateChallenge()
    {
        Challenge challenge1 = new Challenge("Your First Bee!", "Reach 1 bees", c1Icon);
        challenge1.AddCondition(new ChallengeCondition(ConditionType.BeeCount, 1));
        allChallenges.Add(challenge1);

        Challenge challenge2 = new Challenge("Wow, It's Money!", "Earn 10 money", c2Icon);
        challenge2.AddCondition(new ChallengeCondition(ConditionType.MoneyAmount, 10));
        allChallenges.Add(challenge2);

        Challenge challenge3 = new Challenge("Young, Rich.", "Reach 10 bees And Earn 15 Money", c3Icon);
        challenge3.AddCondition(new ChallengeCondition(ConditionType.BeeCount, 10));
        challenge3.AddCondition(new ChallengeCondition(ConditionType.MoneyAmount, 15));
        allChallenges.Add(challenge3);


    }
    IEnumerator CheckTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            Management manager = Management.Instance;
            float current_money = manager.GetCurrentMoney();
            float currentBee = manager.GetCurrentBee();
            int upgradecount = manager.GetUpgradeCount();
            float playTime = manager.GetPlayTime();
            CheckChallenges(currentBee, current_money, upgradecount, playTime); // challenges 인스턴스를 통해 CheckChallenges 호출
            Debug.Log(currentBee + " / " + current_money + " / " + upgradecount + " / " + playTime);
        }
    }

    public void CheckChallenges(float beeCount, float moneyAmount, int upgradeCount, float playTime)
    {
        foreach (var challenge in allChallenges)
        {
            if (!challenge.isCompleted && challenge.IsCompleted(beeCount, moneyAmount, upgradeCount, playTime))
            {
                Debug.Log(challenge.title + " completed!");
                challenge.isCompleted = true;
                challengePopup.ShowPopup(challenge.title, challenge.description, challenge.GetChallengeIcon());
            }
        }
    }
}
