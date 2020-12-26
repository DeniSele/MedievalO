using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesLoader
{
    private static ResourcesLoader instance = null;

    public static ResourcesLoader Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ResourcesLoader();
            }
            return instance;
        }
    }

    public Sprite GetResourceByName(string path)
    {
        return Resources.Load<Sprite>(path);
    }
}
