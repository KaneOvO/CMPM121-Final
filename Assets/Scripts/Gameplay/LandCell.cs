using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class LandCell
{
    // public bool isPanted = false;
    // public int landPlantedType;
    // public float water;
    // public float sun;
    // public float currentStage = 0;
    public static readonly int NumBytes = GlobalValue.BYTE_NUM;
    private byte[] buffer;
    private int offset;

    public LandCell(byte[] buffer, int offset)
    {
        this.buffer = buffer;
        this.offset = offset;
    }

    public LandCell()
    {

    }

    public LandCell(LandCell other)
    {
        offset = other.offset;
        buffer = new byte[other.buffer.Length];
        Array.Copy(other.buffer, buffer, buffer.Length);
    }

    public bool isPanted
    {
        get => BitConverter.ToBoolean(buffer, offset);
        set => Array.Copy(BitConverter.GetBytes(value), GlobalValue.FIRST_INDEX, buffer, offset, GlobalValue.BOOL_BYTE_SIZE);
    }

    public PlantType landPlantedType
    {
        get => (PlantType)BitConverter.ToInt32(buffer, offset + GlobalValue.BOOL_BYTE_SIZE);
        set => Array.Copy(BitConverter.GetBytes((int)value), GlobalValue.FIRST_INDEX, buffer, offset + GlobalValue.BOOL_BYTE_SIZE, GlobalValue.ENUM_BYTE_SIZE);
    }

    public int currentStage
    {
        get => BitConverter.ToInt32(buffer, offset + GlobalValue.BOOL_BYTE_SIZE + GlobalValue.ENUM_BYTE_SIZE);
        set => Array.Copy(BitConverter.GetBytes(value), GlobalValue.FIRST_INDEX, buffer, offset + GlobalValue.BOOL_BYTE_SIZE + GlobalValue.ENUM_BYTE_SIZE, GlobalValue.FLOAT_BYTE_SIZE);
    }

    public float water
    {
        get => BitConverter.ToSingle(buffer, offset + GlobalValue.BOOL_BYTE_SIZE + GlobalValue.ENUM_BYTE_SIZE + GlobalValue.FLOAT_BYTE_SIZE);
        set => Array.Copy(BitConverter.GetBytes(value), GlobalValue.FIRST_INDEX, buffer, offset + GlobalValue.BOOL_BYTE_SIZE + GlobalValue.ENUM_BYTE_SIZE + GlobalValue.FLOAT_BYTE_SIZE, GlobalValue.FLOAT_BYTE_SIZE);
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        LandCell other = (LandCell)obj;

        if (offset != other.offset)
        {
            return false;
        }

        if (buffer.Length != other.buffer.Length)
        {
            return false;
        }

        for (int i = 0; i < buffer.Length; i++)
        {
            if (buffer[i] != other.buffer[i])
            {
                return false;
            }
        }

        return true;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}