using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

//Singleton
public class ShroomPool : MonoBehaviour
{

    public static ShroomPool instance = null;

    public List<ShroomClass> shroomClasses;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    public static ShroomClass getShroomClass(ShroomType type)
    {
        return instance.shroomClasses.Find(x => x.shroomType == type);
    }

}

[Serializable]
public struct ShroomClass {
    public ShroomType shroomType;
    public int maxCount;
    public List<Shroom> shrooms;
}
