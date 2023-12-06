using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor.ShaderGraph.Internal;
public class LandCell
{
    // public bool isPanted = false;
    // public int landPlantedType;
    // public float water;
    // public float sun;
    // public float currentStage = 0;
    public static readonly int NumBytes = 16;
    private byte[] buffer;
    private int offset;
    
    public LandCell(byte[] buffer, int offset) {
        this.buffer = buffer;
        this.offset = offset;
    }

    public bool isPanted {
        get => BitConverter.ToBoolean(buffer, offset);
        set => Array.Copy(BitConverter.GetBytes(value), 0, buffer, offset, 1);
    }

    public PlantType landPlantedType {
        //get => BitConverter.ToSingle(buffer, offset + 4);
        get => (PlantType)BitConverter.ToInt32(buffer, offset + 1);
        set => Array.Copy(BitConverter.GetBytes((int)value), 0, buffer, offset + 1, 4);
    }

    public int currentStage {
        get => BitConverter.ToInt32(buffer, offset + 5);
        set => Array.Copy(BitConverter.GetBytes(value), 0, buffer, offset + 5, 4);
    }

    public float water {
        get => BitConverter.ToSingle(buffer, offset + 9);
        set => Array.Copy(BitConverter.GetBytes(value), 0, buffer, offset + 9, 4);
    }
}