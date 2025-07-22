using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public enum FoodType : ushort
{
    None,
    Tomato,
    Lettuce
}

public abstract class Table : MonoBehaviour
{
    public const float selectedColorAlpha = 0.3f;
    private const float unSelectedColorAlpha = 1.0f;

    public static Dictionary<FoodType, string> foodTypeNameMap = new Dictionary<FoodType, string>()
    {
        {FoodType.Tomato, "Tomato" },
        {FoodType.Lettuce, "Lettuce" },
    };

    protected int _tableType ;
    public int tableType { 
        get { 
            return _tableType;  
        } 
    }

    public void select()
    {
        var mat = GetComponent<MeshRenderer>().material;
        Color c = mat.color;
        mat.color = new Color(c.r, c.g, c.b, selectedColorAlpha);
    }

    public void unSelect()
    {
        var mat = GetComponent<MeshRenderer>().material;
        Color c = mat.color;
        mat.color = new Color(c.r, c.g, c.b, unSelectedColorAlpha);
    }

    public abstract FoodType interAct(FoodType givenFood);

}
