using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class DatabaseHandler : MonoBehaviour
{
    [SerializeField] private List<string> randomNames = new();
    [SerializeField] internal DataBase dataBase = new();
    void Awake()
    {

        if (File.Exists(Path.Combine(Application.dataPath,"Resources","JSONFile"+".json"))||Utils.TryConvertFromCSVToJson("CSV_Data","JSONFile"))
        {
            DataBase newbase = Utils.ReadJSON<DataBase>("JSONFile");
            if (newbase != default)
            {
                dataBase = newbase;
                foreach(Data data in dataBase.DataLists){
                    Debug.Log($"{data.Name} | {data.STRDescription} | (vol: {data.Volume})");
                }
            }
        }
    }
    void OnApplicationQuit()
    {
        Utils.SaveToJson(dataBase,"JSONFile");
    }
}
