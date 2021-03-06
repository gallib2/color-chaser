﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserGameManager : MonoBehaviour
{
    public Transform platformGenrator;
    private Vector3 platformStartPoint;

    public PlayerControll thePlayer;
    private Vector3 playerStartPoint;

    private PlatforDestroyer[] platformList;

    // Start is called before the first frame update
    void Start()
    {
        platformStartPoint = platformGenrator.position;
        playerStartPoint = thePlayer.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RestartGame()
    {
        StartCoroutine("RestartGameCo");
    }

    public IEnumerator RestartGameCo()
    {
        thePlayer.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        platformList = FindObjectsOfType<PlatforDestroyer>();
        for(int i = 0; i < platformList.Length; i++)
        {
            platformList[i].gameObject.SetActive(false);
        }

        thePlayer.transform.position = playerStartPoint;
        platformGenrator.position = platformStartPoint;
        thePlayer.gameObject.SetActive(true);
    }
}
