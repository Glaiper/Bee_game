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

        // ���� ���� (0.0 ~ 1.0 ���� ��)
        audioSource.volume = 0.2f;

        // ������� ���
        audioSource.Play();

        volumeSlider.onValueChanged.AddListener(VolumeChanged);
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

}
