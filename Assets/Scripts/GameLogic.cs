using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour {

    private float StartDistance;
    private float WinningDistance;
    private float LosingHealth;
    [SerializeField] public Transform WinningBlock;
    [SerializeField] public Transform Player; 

	// Use this for initialization
	void Start () {
        StartDistance = Vector3.Distance(WinningBlock.position, Player.position);
    }
	
	// Update is called once per frame
	void Update () {
        WinningDistance = Vector3.Distance(WinningBlock.position, Player.position);

        GameWin(WinningDistance);
	}

    private void GameWin(float distance)
    {
        if (distance < 5)
        {
            SceneManager.LoadScene(2);
        }
    }
}
