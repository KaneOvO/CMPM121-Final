using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
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
    }


    // Start is called before the first frame update
    void Start()
    {
        numOfCarrot = 0;
        numOfCabbage = 0;
        numOfOnion = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setLand(GameObject land)
    {
        this.land = land;
    }

    public void Cow(){
        Growable growable = land.GetComponentInChildren<Growable>();
       if (growable != null)
        {
            // Debug.Log(land.GetComponent<Land>().landPantedType);
            // Debug.Log(growable.getStage());
            if(growable.getStage() == 2){
                // Debug.Log(land.GetComponent<Land>().landPantedType);
                // Debug.Log(growable.getStage());
                GameObject plantObject = growable.gameObject;
                
                
                if(land.GetComponent<Land>().landPantedType == "Carrot"){
                    numOfCarrot+=1;
                    UIManager.Instance.ChangeCarrotText(numOfCarrot);
                }else if(land.GetComponent<Land>().landPantedType == "Cabbage"){
                    numOfCabbage+=1;
                    UIManager.Instance.ChangeCabbageText(numOfCabbage);
                }else if(land.GetComponent<Land>().landPantedType == "Onion"){
                    numOfOnion+=1;
                    UIManager.Instance.ChangeOnionText(numOfOnion);
                }
                // Debug.Log(numOfCarrot);
                Destroy(plantObject);
                land.GetComponent<Land>().isPanted=false;
                
            }
        }
    }

    

}
