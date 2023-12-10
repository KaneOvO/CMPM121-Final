using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public int currentTurn;
    public static Stack<Savedata> undoStack = new Stack<Savedata>();
    public static Stack<Savedata> redoStack = new Stack<Savedata>();

    public static Savedata undoData;
    public static Savedata redoData;
    public static Savedata currentData;

    public static int temp = 0;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }
    }

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        currentTurn = GlobalValue.INITIAL_TURN;
    }

    public void MoveToNextTurn()
    {
        ClearRedoStack();
        SaveCureentSituations();
        currentTurn++;
        
        
    }

    public void SaveCureentSituations()
    {
        currentData = new Savedata(PlantManager.landArea, currentTurn, PlantManager.Instance.numOfCarrot,
            PlantManager.Instance.numOfCabbage, PlantManager.Instance.numOfOnion);
        undoStack.Push(currentData);
        //Debug.Log("Save"+ ++temp);
    }


    public bool Undo()
    {
        if (undoStack.Count == 0) return false;
        redoData = new Savedata(PlantManager.landArea, currentTurn, PlantManager.Instance.numOfCarrot,
            PlantManager.Instance.numOfCabbage, PlantManager.Instance.numOfOnion);
        redoStack.Push(redoData);
        undoData = undoStack.Pop();
        redoData = new Savedata(undoData);
        

        return true;
    }

    public bool Redo()
    {
        if (redoStack.Count == 0) return false;
        undoData = new Savedata(PlantManager.landArea, currentTurn, PlantManager.Instance.numOfCarrot,
            PlantManager.Instance.numOfCabbage, PlantManager.Instance.numOfOnion);
        undoStack.Push(undoData);
        redoData = redoStack.Pop();
        undoData = new Savedata(redoData);
        

        return true;
    }

    public void ClearRedoStack()
    {
        redoStack.Clear();
    }

}
