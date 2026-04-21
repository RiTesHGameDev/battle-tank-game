using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int numRoundsToWin = 3;
    public float startDelay = 3f;
    public float endDelay = 3f;
    public CameraControl cameraControl;
    public TextMeshProUGUI messageText;
    public GameObject tankPrefab;
    public TankManager[] tanks;

    private int roundNumber;
    private WaitForSeconds startWait;
    private WaitForSeconds endWait;
    private TankManager roundWinner;
    private TankManager gameWinner;

    void Start()
    {
        startWait = new WaitForSeconds(startDelay);
        endWait = new WaitForSeconds(endDelay);
        SpawnAllTanks();
        SetCameraTargets();
        StartCoroutine(GameLoop());
    }

    private IEnumerator GameLoop()
    {
        while (gameWinner == null)
        {
            yield return StartCoroutine(RoundStarting());
            yield return StartCoroutine(RoundPlaying());
            yield return StartCoroutine(RoundEnding());
        }

        SceneManager.LoadScene(0);
    }

    private IEnumerator RoundEnding()
    {
        DisableTankControl();

        roundWinner = GetRoundWinner();

        if (roundWinner != null)
        {
            roundWinner.wins++;
        }

        gameWinner = GetGameWinner();

        string message = EndMessage();
        messageText.text = message;

        yield return endWait;
    }

    private string EndMessage()
    {
        string message = "DRAW!";

        if (roundWinner != null)
        {
            message = roundWinner.coloredPlayerText + " WINS THE ROUND!";
        }

        message += "\n\n\n\n";

        for(int i = 0; i < tanks.Length; i++)
        {
            message += tanks[i].coloredPlayerText + ": " + tanks[i].wins + " WINS\n";
        }

        if (gameWinner != null)
        {
            message = gameWinner.coloredPlayerText + " WINS THE GAME!";
        }

        return message;
    }

    private TankManager GetGameWinner()
    {
        for (int i = 0; i < tanks.Length; i++)
        {
            if (tanks[i].wins == numRoundsToWin)
            {
                return tanks[i];
            }
        }
        return null;
    }

    private TankManager GetRoundWinner()
    {
        for (int i = 0; i < tanks.Length; i++)
        {
            if (tanks[i].instance.activeSelf)
            {
                return tanks[i];
            }
        }
        return null;
    }

    private IEnumerator RoundPlaying()
    {
        EnableTankControl();
        messageText.text = string.Empty;

        while (!OneTankLeft())
        {
            yield return null;
        }
    }

    private bool OneTankLeft()
    {
        int numTanksLeft = 0;
        for (int i = 0; i < tanks.Length; i++)
        {
            if (tanks[i].instance.activeSelf)
                numTanksLeft++;
        }
        return numTanksLeft <= 1;
    }

    private void EnableTankControl()
    {
        for (int i = 0; i < tanks.Length; i++)
        {
            tanks[i].EnableControl();
        }
    }

    private IEnumerator RoundStarting()
    {
        ResetAllTanks();
        DisableTankControl();

        cameraControl.SetStartPositionAndSize();
        roundNumber++;
        messageText.text = "ROUND " + roundNumber;
        yield return startWait;
    }

    private void DisableTankControl()
    {
        for (int i = 0; i < tanks.Length; i++)
        {
            tanks[i].DisableControl();
        }

    }

    private void ResetAllTanks()
    {
        for (int i = 0; i < tanks.Length; i++)
        {
            tanks[i].Reset();
        }

    }

    private void SetCameraTargets()
    {
        Transform[] targets = new Transform[tanks.Length];
        for (int i = 0; i < targets.Length; i++)
        {
            targets[i] = tanks[i].instance.transform;
        }
        cameraControl.targets = targets;
    }

    private void SpawnAllTanks()
    {
        for(int i = 0;i < tanks.Length; i++)
        {
            tanks[i].instance = Instantiate(tankPrefab, tanks[i].spawnPoint.position, tanks[i].spawnPoint.rotation) as GameObject;
            tanks[i].playerNumber = i + 1;
            tanks[i].Setup();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
