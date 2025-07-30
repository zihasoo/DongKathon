using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public enum ItemType : ushort
{
    None,
    Tomato = 10,
    CutTomato = 20,
    Cucumber = 100,
    CutCucumber = 200
}

public abstract class Table : MonoBehaviour
{
    public const float selectedColorAlpha = 0.3f;
    private const float unSelectedColorAlpha = 1.0f;

    public static Dictionary<ItemType, string> itemTypeNameMap = new Dictionary<ItemType, string>()
    {
        {ItemType.Tomato, "Tomato" },
        {ItemType.Cucumber, "Cucumber" },
    };

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

    public abstract ItemType takeItem();

    public abstract bool putDownItem(ItemType givenFood);

}
