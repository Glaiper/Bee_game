using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManage : MonoBehaviour
{
    public AudioClip backgroundMusic;

    [SerializeField] private GameObject check1;
    private AudioSource audioSource;

    public Slider volumeSlider;
    public AudioSource audioSourceMusic;

    void Start()
    {
        
        // AudioSource ������Ʈ�� ã�Ƽ� ������
        audioSource = audioSourceMusic;

        // ��������� ����
        audioSource.clip = backgroundMusic;

        // ���� ���
        audioSource.loop = true;
        // ������� ���
        audioSource.Play();

        volumeSlider.onValueChanged.AddListener(VolumeChanged);

        LoadPlayerData();
    }

    void VolumeChanged(float volume)
    {
        // ����� �ҽ��� ������ �����̴��� ������ ����
        audioSource.volume = volume;
    }

    public void Setmute()

    {
        if (audioSource.volume == 0)
        {
            audioSource.volume = volumeSlider.value;
            check1.SetActive(true);
            volumeSlider.interactable = true;
        }
        else
        {
            audioSource.volume = 0;
            check1.SetActive(false);
            volumeSlider.interactable = false;
        }

    }

    public void PlayerSaveData()

    {
         PlayerPrefs.SetFloat("Voulume", audioSource.volume);
         PlayerPrefs.SetFloat("Slider", volumeSlider.value);
    }

    public void LoadPlayerData()
    {
        audioSource.volume = PlayerPrefs.GetFloat("Voulume", 0.1f);
        volumeSlider.value = PlayerPrefs.GetFloat("Slider", 0.1f);
    }

}
