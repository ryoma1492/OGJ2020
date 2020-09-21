using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using UnityEngine;
using Unity.Mathematics;
using System.Linq;

public class FlowerManager : MonoBehaviour
{
    [SerializeField] private bool isChallengeMode;
    [SerializeField] private GameObject prefabYellowFlower;
    [SerializeField] private GameObject prefabRedFlower;
    [SerializeField] private int[] flowerOrder;
    [SerializeField] private int currOrder;
    [SerializeField] private int foundFlowers;
    [SerializeField] private GameObject[] Flowers;
    private GameObject currentFlower;
    private int randInt;

    private void Start()
    {

    }
    public bool isCurrrentFlower(int flower)
    {
        return flower == foundFlowers;
    }

    public int flowerCount()
    {
        return foundFlowers;
    }
    public int collectFlower()
    {
        foundFlowers += 1;
        Flowers[foundFlowers - 1].SetActive(false);
        if (Flowers.Length > foundFlowers)
        {
            Flowers[foundFlowers].SetActive(true);
            return foundFlowers;
        }
        else
        {
            return -1;
        }

    }
    public GameObject GetFlower()
    {
        return Flowers[foundFlowers];
    }
}
