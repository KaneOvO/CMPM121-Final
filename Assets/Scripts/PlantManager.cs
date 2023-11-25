using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantManager : MonoBehaviour
{
    public static PlantManager Instance { get; private set; }
    public int plantSelected;
    public int numOfCarrot;
    public int numOfCabbage;
    public int numOfOnion;

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
        plantSelected = 0;
        numOfCarrot = 0;
        numOfCabbage = 0;
        numOfOnion = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }



}
