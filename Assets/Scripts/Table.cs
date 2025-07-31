using System.Collections.Generic;
using UnityEngine;

public enum ItemType : ushort
{
    None,
    Tomato = 1,
    Cucumber = 2,
    CutTomato = Tomato | Table.isCutOff,
    CutCucumber = Cucumber | Table.isCutOff,
    FinishedDish = Tomato | Cucumber | Table.isFinished //잘린 음식 시그널은 넣지않음
}

public abstract class Table : MonoBehaviour
{
    private const float selectedColorAlpha = 0.3f;
    private const float unSelectedColorAlpha = 1.0f;

    public const int isCutOff = 1 << 10;
    public const int isFinished = 1 << 11;

    public static Dictionary<ItemType, string> itemTypeNameMap = new Dictionary<ItemType, string>()
    {
        {ItemType.Tomato, "Tomato" },
        {ItemType.CutTomato, "CutTomato" },
        {ItemType.Cucumber, "Cucumber" },
        {ItemType.CutCucumber, "CutCucumber" },
        {ItemType.FinishedDish, "FinishedDish" }
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
