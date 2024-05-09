using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
public class ChallngePopup : MonoBehaviour
{

    public static ChallngePopup instance;

    public TMP_Text titleText;
    public TMP_Text descriptionText;
    public Image iconImage;
    public GameObject popupPanel;

    private RectTransform popupRectTransform;
    private Vector3 targetPosition;
    private Vector3 startPosition;
    private float moveSpeed = 1000f;
    private bool moving = false;

    private void Awake()
    {
        instance = this;
        popupRectTransform = popupPanel.GetComponent<RectTransform>();
        targetPosition = popupRectTransform.anchoredPosition;
        startPosition = new Vector3(-1000f, targetPosition.y, 0f);
        popupRectTransform.anchoredPosition = startPosition;
    }

    public void ShowPopup(string title, string description, Sprite icon)
    {
        titleText.text = title;
        descriptionText.text = description;
        iconImage.sprite = icon;
        StartCoroutine(ShowAnimation());
    }
    private IEnumerator ShowAnimation()
    {
        moving = true;
        float distance = Vector3.Distance(startPosition, targetPosition);
        float duration = distance / moveSpeed;
        float startTime = Time.time;
        popupPanel.SetActive(true);

        while (Time.time < startTime + duration)
        {
            float fraction = (Time.time - startTime) / duration;
            popupRectTransform.anchoredPosition = Vector3.Lerp(startPosition, targetPosition, fraction);
            yield return null;
        }

        popupRectTransform.anchoredPosition = targetPosition;
        yield return new WaitForSeconds(3f); // ÀÏÁ¤ ½Ã°£ ¸ØÃã

        StartCoroutine(HideAnimation());
    }
    private IEnumerator HideAnimation()
    {
        Vector3 hidePosition = new Vector3(1000f, targetPosition.y, 0f);
        float distance = Vector3.Distance(targetPosition, hidePosition);
        float duration = distance / (moveSpeed*2);
        float startTime = Time.time;

        while (Time.time < startTime + duration)
        {
            float fraction = (Time.time - startTime) / duration;
            popupRectTransform.anchoredPosition = Vector3.Lerp(targetPosition, hidePosition, fraction);
            yield return null;
        }

        popupRectTransform.anchoredPosition = hidePosition;
        moving = false;
        popupPanel.SetActive(false);
    }
    public void ClosePopup()
    {
        if (!moving)
        {
            StopAllCoroutines();
            StartCoroutine(HideAnimation());
        }
    }
}
