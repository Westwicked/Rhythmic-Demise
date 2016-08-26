﻿using UnityEngine;
using System.Collections;

public class EndResultManager : MonoBehaviour {
    public static EndResultManager erm;

    AudioSource audio;
    Animator anim;
    GameObject endStageClone;

    bool isComplete, done;
    bool isBossStage;

    bool gameOver, played;

	void Start () {
        if (erm == null) { 
            erm = this; 
        }

        gameOver = played = false;
        anim = GetComponent<Animator>();
        audio = GameObject.Find("Audio Source").GetComponent<AudioSource>();
        endStageClone = Instantiate(Resources.Load<GameObject>("Prefabs/End Stage"));
	}
    void PlaygameOver()
    {
        if (gameOver && !played)
        {
            GameObject.Find("Game Music").SendMessage("PlayGameOver");
            played = true;
        }
    }
	
	void Update () {
        if (ArmyController.armyController.army.Count <= 0)
        {
            gameOver = true;
            audio.Stop();
            anim.SetTrigger("GameOver");
            PlaygameOver();
        }
        else if (ArmyController.armyController.currPos.name == "EndPoint")
        {
            isComplete = true;
        }

        if (GameObject.Find("Boss") && !isBossStage)
        {
            isBossStage = true;
        }

        if (isBossStage)
        {
            if (GameObject.Find("Boss"))
            {
                Boss b = GameObject.Find("Boss").GetComponent<Boss>();
                if (b.IsDead)
                {
                    isComplete = true;
                }
            }
            else
            {
                isComplete = true;
            }
        }

        if (isComplete && !done)
        {
            GameObject.Find("Game Music").SendMessage("PlayVictory");
            StopGame();
        }
	}

    public void StopGame()
    {
        audio.Stop();
        
        //update resources
        endStageClone.SendMessage("UpdateData", GameController.gameController.getHighestStreak());
        anim.SetTrigger("Finish");
        done = true;
    }

    public bool isDone()
    {
        return done;
    }
}
