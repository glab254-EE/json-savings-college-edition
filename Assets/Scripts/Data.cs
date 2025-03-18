using System;

[System.Serializable]
public class Data 
{
    public string Name = "";
    public string STRDescription = "";
    public float Volume = 0.5f;
    public Data(){} // to remove errors.
    public Data(string _name)
    {
        Name = _name;
        STRDescription = "";
    }
    public Data(string _name,string _description)
    {
        Name = _name;
        STRDescription = _description;
    }
}
