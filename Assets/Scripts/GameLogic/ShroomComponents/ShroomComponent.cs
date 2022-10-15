using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShroomComponent : MonoBehaviour
{
    protected Shroom shroom;
    protected virtual void Start()
    {
        shroom = GetComponent<Shroom>();
    }
}
