using NUnit.Framework;
using UnityEngine;

public enum E_cheeseType
{
    Swiss = 1,
    Brie = 2
}

public class CheeseData
{
    public float cheeseSize = 1f;
    public E_cheeseType cheeseType = E_cheeseType.Swiss;


    public CheeseData(float cheeseSize, int cheeseType)
    {
        this.cheeseSize = cheeseSize;

        switch (cheeseType) 
        {
            case 1:
                this.cheeseType = E_cheeseType.Swiss;
                break;
            case 2:
                this.cheeseType = E_cheeseType.Brie;
                break;

        }

    }
}
