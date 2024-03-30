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
        // AudioSource 컴포넌트를 찾아서 가져옴
        audioSource = audioSourceMusic;

        // 배경음악을 설정
        audioSource.clip = backgroundMusic;

        // 루프 재생
        audioSource.loop = true;

        // 볼륨 설정 (0.0 ~ 1.0 사이 값)
        audioSource.volume = 0.2f;

        // 배경음악 재생
        audioSource.Play();

        volumeSlider.onValueChanged.AddListener(VolumeChanged);
    }

    void VolumeChanged(float volume)
    {
        // 오디오 소스의 볼륨을 슬라이더의 값으로 설정
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
