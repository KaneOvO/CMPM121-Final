using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandAreaOperation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CallNextTurnOnChildren()
    {
        foreach (Transform child in transform)
        {
            Land land = child.GetComponent<Land>();
            if (land != null)
            {
                land.NextTurn();
            }
        }
        // gameObject.Instance.SaveCureentSituations();
    }

    public void CallUndoTurnOnChildren()
    {
        if (GameManager.Instance.Undo() == false)
        {
            return;
        }
        GameManager.Instance.currentTurn = GameManager.undoData.currentTurn;
        PlantManager.Instance.numOfCarrot = GameManager.undoData.numberOfCarrot;
        PlantManager.Instance.numOfCabbage = GameManager.undoData.numberOfCabbage;
        PlantManager.Instance.numOfOnion = GameManager.undoData.numberOfOnion;

        foreach (Transform child in transform)
        {
            Land land = child.GetComponent<Land>();
            if (land != null)
            {
                land.undoThisLand();
            }
        }

        UIManager.Instance.ChangeText();

    }

    public void CallRedoTurnOnChildren()
    {
        if (GameManager.Instance.Redo() == false)
        {
            return;
        }
        GameManager.Instance.currentTurn = GameManager.redoData.currentTurn;
        PlantManager.Instance.numOfCarrot = GameManager.redoData.numberOfCarrot;
        PlantManager.Instance.numOfCabbage = GameManager.redoData.numberOfCabbage;
        PlantManager.Instance.numOfOnion = GameManager.redoData.numberOfOnion;

        foreach (Transform child in transform)
        {
            Land land = child.GetComponent<Land>();
            if (land != null)
            {
                land.redoThisLand();
            }
        }

        UIManager.Instance.ChangeText();
    }

    public void loadSavedLandArea()
    {
        foreach (Transform child in transform)
        {
            Land land = child.GetComponent<Land>();
            if (land != null)
            {
                land.loadThisLand();
            }
        }
    }
}
