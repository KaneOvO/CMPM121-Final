using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlantManager : MonoBehaviour
{
    public static PlantManager Instance { get; private set; }
    public bool packSelected;
    public int numOfCarrot;
    public int numOfCabbage;
    public int numOfOnion;
    private GameObject land;

    public static LandArea landArea = new LandArea();

    private int INITIAL_QUANTITY = 0;

    void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        CreatLandAOS();
    }


    // Start is called before the first frame update
    void Start()
    {
        numOfCarrot = INITIAL_QUANTITY;
        numOfCabbage = INITIAL_QUANTITY;
        numOfOnion = INITIAL_QUANTITY;
        GameManager.Instance.SaveCureentSituations();
    }


    public void setLand(GameObject land)
    {
        this.land = land;
    }

    public void Sow()
    {
        if(land == null) return;
        Land landComponent = land.GetComponent<Land>();
        Growable growable = land.GetComponentInChildren<Growable>();

        //Debug.Log(growable);
        if (growable == null || growable.getStage() != GlobalValue.MAX_STAGE) return;

        GameManager.Instance.SaveCureentSituations();
        
        GameObject plantObject = growable.gameObject;
        PlantType plantedType = landArea.GetLandCell(landComponent.FindID()).landPlantedType;


        Dictionary<PlantType, System.Action> updateActions = new Dictionary<PlantType, System.Action>()
        {
            {PlantType.CARROT, () => {numOfCarrot += 1;}},
            {PlantType.CABBAGE, () => {numOfCabbage += 1;}},
            {PlantType.ONION, () => {numOfOnion += 1;}},
        };
    
        if (updateActions.ContainsKey(plantedType))
        {
            updateActions[plantedType].Invoke();
        }

        UIManager.Instance.ChangeText();

        Destroy(plantObject);
        landArea.GetLandCell(landComponent.FindID()).isPanted = false;
        landArea.GetLandCell(landComponent.FindID()).landPlantedType = PlantType.EMPTY;
        landArea.GetLandCell(landComponent.FindID()).currentStage = GlobalValue.INITIAL_STAGE;
        
        GameManager.Instance.ClearRedoStack();
    }

    public void CreatLandAOS()
    {
        int lands = GlobalValue.LAND_NUM;
        byte[] landBuffer = new byte[lands * LandCell.NumBytes];

        //landCells = new LandCell[lands];
        for (int i = 0; i < lands; i++)
        {
            landArea.addCell(new LandCell(landBuffer, i * LandCell.NumBytes)
            {
                isPanted = false,
                landPlantedType = PlantType.EMPTY,
                currentStage = GlobalValue.INITIAL_STAGE,
                water = RandomResources.GetRandom(),
            });
        }
    }

}