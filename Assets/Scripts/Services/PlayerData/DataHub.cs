using UnityEngine;

public class DataHub
{
    private static DataHub instance = null;

    public static DataHub Instance {
        get
        {
            if (instance == null)
            {
                instance = new DataHub();
            }
            return instance;
        }
    }


    public bool HasKey(string key)
    {
        return PlayerPrefs.HasKey(key);
    }


    public int GetIntValue(string valueName, int defaultValue = 0)
    {
        return PlayerPrefs.GetInt(valueName, defaultValue);
    }


    public float GetFloatValue(string valueName, float defaultValue = 0)
    {
        return PlayerPrefs.GetFloat(valueName, defaultValue);
    }


    public string GetStringValue(string valueName, string defaultValue = null)
    {
        return PlayerPrefs.GetString(valueName, defaultValue);
    }


    public void SetIntValue(string valueName, int value)
    {
        PlayerPrefs.SetInt(valueName, value);
    }


    public void SetFloatValue(string valueName, float value)
    {
        PlayerPrefs.SetFloat(valueName, value);
    }


    public void SetStringValue(string valueName, string value)
    {
        PlayerPrefs.SetString(valueName, value);
    }
}
