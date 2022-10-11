using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomOnInputSpawner : MonoBehaviour
{
    [SerializeField]
    public KeyCode spawnKeyA, spawnKeyB;

    [SerializeField]
    GameObject shroomPrefabA, shroomPrefabB;

    Camera cam;
    GameManager gameManager;
    void Start()
    {
        cam = Camera.main;
        gameManager = GameManager.instance;
    }

    void Update()
    {
        GameObject pref = null;
        if(Input.GetKeyDown(spawnKeyA))
        {
            pref = shroomPrefabA;
        }
        else if (Input.GetKeyDown(spawnKeyB))
        {
             pref = shroomPrefabB;
            
        }

        if (pref != null)
        {
            Shroom shroom = pref.GetComponent<Shroom>();
            if (gameManager.money < shroom.price)
            {
                return;
            }

            var shroomClass = ShroomPool.getShroomClass(shroom.shroomType);

            if(shroomClass.shrooms.Count >= shroomClass.maxCount)
            {
                return;
            }

            gameManager.money -= shroom.price;
            Vector3 mousePos = Input.mousePosition;
            Instantiate(pref, cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10f)), Quaternion.identity);
        }
    }
}
