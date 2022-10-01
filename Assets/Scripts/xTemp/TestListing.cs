using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//using Malee.List;

public class TestListing : MonoBehaviour
{
    /*[Reorderable]
    public RerList tes;*/
}


[System.Serializable]
public class TestListForTest { 
    public TestEn enu;
    public GameObject obj;
    public string s;
}

[System.Serializable]
//public class RerList : ReorderableArray<TestListForTest> { }

public enum TestEn { One, Two, Three}
