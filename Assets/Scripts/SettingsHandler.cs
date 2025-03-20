using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsHandler : MonoBehaviour
{
    [SerializeField] private Slider slider;
    private SingletonMusicPlayer singletonMusicPlayer;
    void Start()
    {
        do
        {
        singletonMusicPlayer = FindAnyObjectByType<SingletonMusicPlayer>();
        } while (singletonMusicPlayer == null);
        slider.value = singletonMusicPlayer.Volume;
        slider.onValueChanged.AddListener(OnValueChange);
    }
    void OnValueChange(float newvalue)
    {
        singletonMusicPlayer.SetVolume(newvalue);
    }
}
