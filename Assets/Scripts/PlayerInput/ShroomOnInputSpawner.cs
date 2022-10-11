using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomOnInputSpawner : MonoBehaviour
{
    [SerializeField]
    public KeyCode spawnKey;

    [SerializeField]
    GameObject shroomPrefab;

    Camera cam;
    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if(Input.GetKeyDown(spawnKey))
        {
            Vector3 mousePos = Input.mousePosition;
            Instantiate(shroomPrefab, cam.ScreenToWorldPoint(new Vector3(mousePos.x,mousePos.y,10f)) ,Quaternion.identity);
        }
    }
}
