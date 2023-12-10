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
    }

    public bool CheckGrowth(GrowthContext context)
    {
        return GrowthCondition(context) && (context.water > this.consumingWater);
    }
}

public class GrowthContext
{
    public int level { get; set; }
    public float water { get; set; }
    public float sunlight { get; set; }

    public GrowthContext(int level, float water, float sunlight)
    {
        this.level = level;
        this.water = water;
        this.sunlight = sunlight;
    }
}

public static class PlantDefinition
{
    public static Plant CarrotLevel0 = new Plant(
        PlantType.CARROT,
        0,
        20,
        ctx => ctx.water >= 20 && ctx.sunlight >= 30 + 10 * ctx.level
    );

    public static Plant CarrotLevel1 = new Plant(
        PlantType.CARROT,
        1,
        40,
        ctx => ctx.level == 1 && ctx.water >= 40 && ctx.sunlight >= 40
    );

    public static Plant CabbageLevel0 = new Plant(
        PlantType.CABBAGE,
        0,
        10,
        ctx => ctx.level == 0 && ctx.water >= 10 && ctx.sunlight >= 30
    );

    public static Plant CabbageLevel1 = new Plant(
        PlantType.CABBAGE,
        1,
        30,
        ctx => ctx.level == 1 && ctx.water >= 30 && ctx.sunlight >= 40
    );


    public static Plant OnionLevel0 = new Plant(
        PlantType.ONION,
        0,
        25,
        ctx => ctx.level == 0 && ctx.water >= 25 && ctx.sunlight >= 30
    );

    public static Plant OnionLevel1 = new Plant(
        PlantType.ONION,
        1,
        50,
        ctx => ctx.level == 1 && ctx.water >= 50 && ctx.sunlight >= 40
    );

    public static Dictionary<PlantType, List<Plant>> Plants = new Dictionary<PlantType, List<Plant>>
    {
        {PlantType.CARROT, new List<Plant> {CarrotLevel0, CarrotLevel1} },
        {PlantType.CABBAGE, new List<Plant> {CabbageLevel0, CabbageLevel1} },
        {PlantType.ONION, new List<Plant> {OnionLevel0, OnionLevel1} }
    };

}

