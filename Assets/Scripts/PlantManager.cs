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

    

}
