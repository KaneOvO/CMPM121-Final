using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using System;

[System.Serializable]
public enum PlantType
{
    EMPTY,
    CABBAGE,
    CARROT,
    ONION
}

public class Plant
{
    public PlantType plantType { get; set; }
    public int level { get; set; }
    public int consumingWater { get; set; }
    public Func<GrowthContext, bool> GrowthCondition { get; set; }

    public Plant(PlantType plantType, int level, int consumingWater, Func<GrowthContext, bool> growthCondition)
    {
        this.plantType = plantType;
        this.level = level;
        this.consumingWater = consumingWater;
        GrowthCondition = growthCondition;

        PlantDefinition.RegisterPlant(this);
    }

    public bool CheckGrowth(GrowthContext context)
    {
        return GrowthCondition(context) && (context.water > this.consumingWater);
    }
}

public class GrowthContext
{
    public float water { get; set; }
    public float sunlight { get; set; }
    public bool leftIsPlanted { get; set; }
    public bool rightIsPlanted { get; set; }

    public GrowthContext(float water, float sunlight, bool leftIsPlanted, bool rightIsPlanted)
    {
        this.water = water;
        this.sunlight = sunlight;
        this.leftIsPlanted = leftIsPlanted;
        this.rightIsPlanted = rightIsPlanted;
    }
}

public static class PlantDefinition
{
    public static void RegisterPlant(Plant plant)
    {
        if (!Plants.ContainsKey(plant.plantType))
        {
            Plants[plant.plantType] = new List<Plant>();
        }
        Plants[plant.plantType].Add(plant);
    }

    public static Dictionary<PlantType, List<Plant>> Plants = new Dictionary<PlantType, List<Plant>>();

    public static Plant CarrotLevel0 = new Plant(
        PlantType.CARROT,
        0,
        20,
        ctx => ctx.water >= 20 && ctx.sunlight >= 10
    );

    public static Plant CarrotLevel1 = new Plant(
        PlantType.CARROT,
        1,
        40,
        ctx => ctx.water >= 40 && ctx.sunlight >= 20
    );

    public static Plant CabbageLevel0 = new Plant(
        PlantType.CABBAGE,
        0,
        10,
        ctx => ctx.water >= 10 && ctx.sunlight >= 30
    );

    public static Plant CabbageLevel1 = new Plant(
        PlantType.CABBAGE,
        1,
        30,
        ctx => ctx.water >= 30 && ctx.sunlight >= 40
    );


    public static Plant OnionLevel0 = new Plant(
        PlantType.ONION,
        0,
        0,
        ctx => !ctx.leftIsPlanted && !ctx.rightIsPlanted
    );

    public static Plant OnionLevel1 = new Plant(
        PlantType.ONION,
        1,
        0,
        ctx => !ctx.leftIsPlanted && !ctx.rightIsPlanted
    );

}


