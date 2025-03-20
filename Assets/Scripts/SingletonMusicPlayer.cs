using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SingletonMusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    private static SingletonMusicPlayer _instance;
    public static SingletonMusicPlayer Instance => _instance;
    private float CurrentVolume = 0.5f;
    internal float Volume => CurrentVolume;
    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);
        if (PlayerPrefs.HasKey("M_Volume")) CurrentVolume = PlayerPrefs.GetFloat("M_Volume");
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        float newvolume = Mathf.Lerp(-80,0,CurrentVolume);
        if (mixer.GetFloat("Volume",out float _vol) && newvolume != _vol)
        {
            mixer.SetFloat("Volume",newvolume);
        }
    }
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetFloat("M_Volume",CurrentVolume);
    }
    internal void SetVolume(float newvolume){
        CurrentVolume = Mathf.Clamp01(newvolume);
    }
}
