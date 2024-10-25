using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class PepperOnlyManager : MonoBehaviour
{
    [SerializeField] private Canvas welcome, auftrag, endscreen;
    [SerializeField] private TMP_Text playerNameWelcome, playerNameAuftrag;
    [SerializeField] private TMP_Text taskPoints;
    [SerializeField] private TMP_Text[] roundPoints;
    [SerializeField] private Image[] roundImgsAuftrag;
    [SerializeField] private Image[] endSceneTaskImages;
    [SerializeField] private TMP_Text[] rgbTarget0, rbgTarget1, rbgTarget2, rbgTarget3;
    [SerializeField] private TMP_Text[] rgbPlayer0, rbgPlayer1, rbgPlayer2, rbgPlayer3;
    [SerializeField] private TMP_Text[] playerPoints;
    [SerializeField] private MixObjectColor mixColorObj;
    [SerializeField] private Sprite[] spritesAuftrag;
    

    private int round = 0;


    private Session currentSession;

    // Start is called before the first frame update
    private void Start()
    {
        if(auftrag != null)
        {
            if (auftrag.gameObject.activeSelf)
            {
                auftrag.gameObject.SetActive(false);
                Debug.Log("Set Auftrag Canvas to false");
            }
            
        }
        welcome.gameObject.SetActive(true);
    }


    //WelcomeSene BTN Start. 
    public void StartSession()
    {
        DateTime currentDateTime = DateTime.Now;
        string customFormat = currentDateTime.ToString("yyyy-MM-dd-HH:mm:ss");

        currentSession = new GameObject().AddComponent<Session>();
        currentSession.SetInfos(playerNameWelcome.text, customFormat, PlayMode.PepperOnly);
        currentSession.SetFirstRound();

        SetUIRoundsUnplayed();
        EnableTaskCanvas();
        currentSession.IncreaseRound();

        //UpdateUI in Welcome-Canvas
        playerNameAuftrag.text = currentSession.PlayerName;
    }

    public void GenerateTask(int task)
    {
        currentSession.SetTask((TaskToDo)task);
        //currentSession.AddTask((TaskToDo)task);
        auftrag.gameObject.SetActive(false);
    }

    public bool IsPlayMode()
    {
        return !welcome.gameObject.activeSelf && !auftrag.gameObject.activeSelf;
    }

    public void EnableTaskCanvas()
    {
        playerNameAuftrag.text = currentSession.PlayerName;
        int currentRound = currentSession.GetCurrentRound();
        roundImgsAuftrag[currentRound].color = Color.white;
        Debug.Log("enable canvas: !!!! cr " + currentRound);
        if(currentRound >= currentSession.MaxRounds-1)
        {
            SetEndScreenInfos();
            ActivateScreen(endscreen.gameObject);
        }
        else
        {
            for (int i = 0; i < currentSession.MaxRounds; i++)
            {
                Debug.Log("poiont: " + i + " " + currentSession.GetRoundTask(i).Points);
                if (currentSession.GetRoundTask(i).Points == -1) break;
                roundPoints[i].text = currentSession.GetRoundTask(i).Points.ToString();
            }

            ActivateScreen(auftrag.gameObject);
        }

    }

    public void SetEndScreenInfos()
    {
        //for(int i = 0; i < currentSession)
    }

    public void CollectEndScreenData()
    {
        for(int i = 0; i < currentSession.MaxRounds; i++)
        {
            TaskToDo tmpTask = currentSession.GetRoundTask(i).targetTask;
        }
    }

    private void SetUIRoundsUnplayed()
    {
        for(int i = 0; i < roundImgsAuftrag.Length; i++)
        {
            roundImgsAuftrag[i].color = Color.gray;
            roundPoints[i].text = "--";
        }
    }

    private void ActivateScreen(GameObject objToActivate)
    {
        if (objToActivate.gameObject.GetComponent<Canvas>() == null) return;

        welcome.gameObject.SetActive(false);
        auftrag.gameObject.SetActive(false);
        endscreen.gameObject.SetActive(false);

        objToActivate.SetActive(true);
    }

    public void EnableEndSession()
    {
        ActivateScreen(endscreen.gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            EnableTaskCanvas();
            currentSession.IncreaseRound();
            //ActivateScreen(auftrag.gameObject);
            //mixColorObj.EvaluatePoints(currentSession.)
        }
    }
}
