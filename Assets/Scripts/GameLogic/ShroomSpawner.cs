using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomSpawner : MonoBehaviour
{
    //[SerializeField]
    //int count;
    //List<int> availableBeats;
    //Shroom[] beatUnits;
    //GameObject shroom;

    //private void Awake()
    //{
    //    availableBeats = new List<int>();
    //    beatUnits = new Shroom[count];
    //    for(int i = 0; i < count; i++)
    //    {
    //        availableBeats.Add(i);
    //    }
    //}

    [SerializeField]
    int shroomsCount;

    [SerializeField]
    GameObject shroomPrefab;

    [SerializeField]
    Transform[] positions;

    int[] shroomsBeats;
    int[] shroomsShowtimes;
    int[] shroomsPos;

    private void Awake()
    {
        shroomsBeats = new int[shroomsCount];
        shroomsShowtimes = new int[shroomsCount];
        shroomsPos = new int[shroomsCount];
        List<int> availableBeats = new List<int>();
        for (int i = 0; i < shroomsCount; i++)
        {
            availableBeats.Add(i);
        }
        for(int i = 0; i < shroomsCount; i++)
        {
            int r = Random.Range(0, availableBeats.Count);
            shroomsBeats[i] = availableBeats[r];
        }

        availableBeats = new List<int>();
        for (int i = 0; i < shroomsCount; i++)
        {
            availableBeats.Add(i);
        }
        for (int i = 0; i < shroomsCount; i++)
        {
            int r = Random.Range(0, availableBeats.Count);
            shroomsPos[i] = availableBeats[r];
        }



        int n = 0;
        for(int i = 0; i < shroomsCount; i++)
        {
            shroomsShowtimes[i] = n;
            n += Random.Range(1, 2);
        }

        //TODO: macke timerTick global across all timers
        int tick = 10;

        for (int i = 0; i < shroomsCount; i++)
        {
            StartCoroutine(createShroom(i, tick));
        }
    }

    IEnumerator createShroom(int index, int tick)
    {
        yield return new WaitForSeconds(shroomsBeats[index] + tick * shroomsShowtimes[index]);
        Instantiate(shroomPrefab, positions[shroomsPos[index]]);
    }
}
