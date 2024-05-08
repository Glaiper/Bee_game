using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ChallangeManager;

public class ChallangeManager : MonoBehaviour
{
    public static ChallangeManager instance;

    public List<Challenge> allChallenges = new List<Challenge>();

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

        public Challenge(string title, string description)
        {
            this.title = title;
            this.description = description;
            isCompleted = false;
            conditions = new List<ChallengeCondition>();
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
        Challenge challenge1 = new Challenge("Your First Bee!", "Reach 1 bees");
        challenge1.AddCondition(new ChallengeCondition(ConditionType.BeeCount, 1));
        allChallenges.Add(challenge1);

        Challenge challenge2 = new Challenge("Wow, It's Money!", "Earn 10 money");
        challenge2.AddCondition(new ChallengeCondition(ConditionType.MoneyAmount, 10));
        allChallenges.Add(challenge2);

        Challenge challenge3 = new Challenge("Young, Rich.", "Reach 10 bees And Earn 15 Money");
        challenge3.AddCondition(new ChallengeCondition(ConditionType.BeeCount, 10));
        challenge3.AddCondition(new ChallengeCondition(ConditionType.MoneyAmount, 15));
        allChallenges.Add(challenge3);


        StartCoroutine(CheckTime()); // �ڷ�ƾ ����
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
            CheckChallenges(currentBee, current_money, upgradecount, playTime); // challenges �ν��Ͻ��� ���� CheckChallenges ȣ��
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
            }
        }
    }
}
