using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using Unity.VisualScripting;

public static class Utils
{
    public static bool TryParseToCSV(DataBase dataBase,string CSVFileName)
{
        try
        {
            if (dataBase == null || CSVFileName == "") return false;
            string filepathCSV = Path.Combine(Application.dataPath,"Resources",CSVFileName+".csv");
            using(StreamWriter writer = new(filepathCSV))
            {
                writer.WriteLine("Name, Description, Volume,");
                foreach (Data data in dataBase.DataLists)
                {
                    string volumetext = data.Volume.ToSafeString();
                    if (volumetext.Contains(',')) volumetext.Replace(',','.');
                    writer.WriteLine($"{data.Name},{data.STRDescription},{volumetext},");
                }
                writer.Close();
            }
            return true;
        }
        catch (System.Exception)
        {
            
            throw;
        }
    }
    public static bool TryParseFromCSV(string CSVFileName,out DataBase dataBase)
    {
        try
        {
            dataBase = null;
            string filepathCSV = Path.Combine(Application.dataPath,"Resources",CSVFileName+".csv");
            if (!File.Exists(filepathCSV)) return false;
            string[] lines = File.ReadAllLines(filepathCSV);
            dataBase = new();
            if (lines.Length > 1)
            {
                for (int i = 1; i < lines.Length; i++)
                {
                    Data inclusion = new();
                    string[] fields = lines[i].Split(",");
                    if (fields.Length > 0)
                    {
                        for (int subI = 0; subI < fields.Length; subI++)
                        {
                            string field = fields[subI].Trim();
                            switch (subI)
                            {
                                case 0:
                                    inclusion.Name = field;
                                    break;
                                case 1:
                                    inclusion.STRDescription = field;
                                    break;
                                case 2:
                                    if (field.Contains('.')) field.Replace('.',',');
                                    float newvol = 0.5f;
                                    float.TryParse(field,out newvol);
                                    inclusion.Volume = newvol;
                                    break;

                            }
                        }
                    }
                    if (inclusion != null && inclusion.Name != "")
                    {
                        dataBase.DataLists.Add(inclusion);
                    }
                }
            }
            if (dataBase != null && dataBase.DataLists.Count > 0)
            {
                return true;
            }
        }
        catch (System.Exception expect)
        {
            Debug.LogWarning(expect);
            throw;
        }
        return false;
    }
    // JSON related
    public static bool TryConvertFromCSVToJson(string CSVFileName,string JSONFileName) // equal to ParveCSV in task.
    {
        try
        {
            DataBase dataBase = null;
            bool succ = TryParseFromCSV(CSVFileName,out dataBase);
            if (succ && dataBase.DataLists.Count>0)
            {
                string filepathJSN = Path.Combine(Application.dataPath,"Resources",JSONFileName+".json");
                string converted = JsonConvert.SerializeObject(dataBase);
                File.WriteAllText(filepathJSN,converted);
                return true;
            } else 
            {
                Debug.Log("Could not find "+CSVFileName);
            }
        }
        catch (System.Exception expect)
        {
            Debug.LogWarning(expect);
            throw;
        }
        return false;
    }
    // end of CSV related
    public static void SaveToJson(object data,string fileName)
    {
        try
        {
            string filepathJSON = Path.Combine(Application.dataPath,"Resources",fileName+".json");
            File.WriteAllText(filepathJSON,JsonConvert.SerializeObject(data));
        }
        catch (System.Exception)
        {
            
            throw;
        }
    }
    public static T ReadJSON<T>(string FilePathName) where T: class // equal to deserializer
    {
        try
        {
            string filepathJSN = Path.Combine(Application.dataPath,"Resources",FilePathName+".json");
            if (File.Exists(filepathJSN))
            {
                T output = JsonConvert.DeserializeObject<T>(File.ReadAllText(filepathJSN));
                return output;
            }
        }
        catch (System.Exception expect)
        {
            Debug.LogWarning(expect);
            throw;
        }
        return default;
    }
}
