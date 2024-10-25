using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum TaskToDo
{
    Bear = 0,
    Cat = 1,
    Owl = 2,
    One = 3,
    Two = 4,
    Three = 5,
    None = 6
}

public class PepperOnlyTask : MonoBehaviour
{
    private ItemData peppersGhostData;
    private Color32[] taskColors;
    private string[] feedback;
    private float euclidDistance = -1;
    private string resultFeedback = "";

    public TaskToDo targetTask;
    public Color playerColor;
    public int Points=-1;


    private void Start()
    {
        peppersGhostData = Resources.Load<ItemData>("PeppersGhostData");
        Points = -1;
        if (peppersGhostData != null)
        {
            Debug.Log("Scriptable Object erfolgreich geladen!");
            // Hier kannst du auf die Eigenschaften deines Scriptable Objects zugreifen
            Debug.Log("Name: " + peppersGhostData.name);
            taskColors = peppersGhostData.taskColors;
            feedback = peppersGhostData.feedback;
            Debug.Log("points: " + Points);
        }
        else
        {
            Debug.LogWarning("Scriptable Object konnte nicht geladen werden.");
        }
    }

    public void SetTask(int task)
    {
        targetTask = (TaskToDo)task;
    }

    private int CalculatePoints(float euclidDistance)
    {
        int points = -1;

        //Die Farben sind komplett identisch.
        if (euclidDistance >= 0 && euclidDistance <= 10)
        {
            points = 4;
        }
        //�hnlich, fast nicht zu unterscheiden.
        else if (euclidDistance >= 11 && euclidDistance <= 40)
        {
            points = 3;
        }
        //�hnlich, aber erkennbar unterschiedlich
        else if (euclidDistance >= 41 && euclidDistance <= 100)
        {
            points = 2;
        }
        //Ziemlich unterschiedlich
        else if (euclidDistance >= 101 && euclidDistance <= 200)
        {
            points = 1;
        }
        //Maximal unterschiedlich, maximale Distanz von etwa 441.67
        else if (euclidDistance >= 201)
        {
            points = 0;
        }

        return points;
    }

    private void SetFeedback(int points)
    {
        resultFeedback = feedback[points];
    }

    public int EvaluatePoints(TaskToDo cTask, Color32 mixedColor)
    {
        int distance = -1;

        switch (cTask)
        {
            case TaskToDo.Bear:
                distance = (int)CalculateColorDistance(GetTargetColor(TaskToDo.Bear), mixedColor);
                break;
            case TaskToDo.Cat:
                distance = (int)CalculateColorDistance(GetTargetColor(TaskToDo.Cat), mixedColor);
                break;
            case TaskToDo.Owl:
                distance = (int)CalculateColorDistance(GetTargetColor(TaskToDo.Owl), mixedColor);
                break;
            case TaskToDo.One:
                distance = (int)CalculateColorDistance(GetTargetColor(TaskToDo.One), mixedColor);
                break;
            case TaskToDo.Two:
                distance = (int)CalculateColorDistance(GetTargetColor(TaskToDo.Two), mixedColor);
                break;
            case TaskToDo.Three:
                distance = (int)CalculateColorDistance(GetTargetColor(TaskToDo.Three), mixedColor);
                break;
        }

        int points = CalculatePoints(distance);
        return points;
    }

    public float CalculateColorDistance(Color32 color1, Color32 color2)
    {
        // Differenzen der Farbkan�le
        int rDifference = color2.r - color1.r;
        int gDifference = color2.g - color1.g;
        int bDifference = color2.b - color1.b;

        // Euklidische Distanz berechnen
        float euclidDistance = Mathf.Sqrt(rDifference * rDifference + gDifference * gDifference + bDifference * bDifference);

        return euclidDistance;
    }

    public Color32 GetTargetColor(TaskToDo task)
    {
        switch(task)
        {
            case TaskToDo.Bear: 
                return taskColors[(int)TaskToDo.Bear];
            case TaskToDo.Cat:
                return taskColors[(int)TaskToDo.Cat]; 
            case TaskToDo.Owl:
                return taskColors[(int)TaskToDo.Owl];
            case TaskToDo.One:
                return taskColors[(int)TaskToDo.One];
            case TaskToDo.Two:
                return taskColors[(int)TaskToDo.Two];
            case TaskToDo.Three:
                return taskColors[(int)TaskToDo.Three];
            default:
                return Color.black;
        }
    }
}
