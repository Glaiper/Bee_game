using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UImanage : MonoBehaviour
{

    [Header("ĵ���� ���")]
    [SerializeField] private GameObject storeCanvas;
    [SerializeField] private GameObject menuCanvas;
    [SerializeField] private GameObject mainCanvas;
    [SerializeField] private GameObject challengesCanvas;

    [Header("�������� �˾�")]
    [SerializeField] private TMP_Text challHeader;
    [SerializeField] private TMP_Text descriptions;
    [SerializeField] private Image challIcon;
    // Start is called before the first frame update

    private void Awake()
    {
        DontDestroyOnLoad(this);
        DontDestroyOnLoad(storeCanvas);
        DontDestroyOnLoad(menuCanvas);
        DontDestroyOnLoad(mainCanvas);
        DontDestroyOnLoad(challengesCanvas);

    }


    public void StoreExit() //���� ��Ȱ��ȭ
    { 
        storeCanvas.SetActive(false);
    }

    public void StoreIn() //���� Ȱ��ȭ
    { 
        storeCanvas.SetActive(true); 
    }

    public void Menuin()
    {
        menuCanvas.SetActive(true);
    }
    public void Menuout()
    {
        menuCanvas.SetActive(false);
    }

    public void Challengein()
    { 
        challengesCanvas.SetActive(true);
    }

    public void ChallengeOut()
    { 
        challengesCanvas?.SetActive(false);
    }
}
