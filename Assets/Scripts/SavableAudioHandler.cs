using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SavableAudioHandler : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private DatabaseHandler handler;
    float oldVolume;
    void Start()
    {
        if (handler.dataBase.DataLists.Count > 0)
        {
            source.volume = handler.dataBase.DataLists.First().Volume;
            Debug.Log(handler.dataBase.DataLists.First().Volume);
        }
        oldVolume = source.volume;
    }
    void Update()
    {
        if (source.volume != oldVolume)
        {
            handler.dataBase.DataLists[0].Volume = source.volume;
            oldVolume = source.volume;
        }
    }
}
