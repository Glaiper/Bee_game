using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UImanage : MonoBehaviour
{
    [SerializeField] private GameObject storeCanvas;
    [SerializeField] private GameObject menuCanvas;
    [SerializeField] private GameObject mainCanvas;
    // Start is called before the first frame update

    private void Awake()
    {
        DontDestroyOnLoad(this);
        DontDestroyOnLoad(storeCanvas);
        DontDestroyOnLoad(menuCanvas);
        DontDestroyOnLoad(mainCanvas);

    }



    public void StoreExit() //상점 비활성화
    { 
        storeCanvas.SetActive(false);
    }

    public void StoreIn() //상점 활성화
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
}
