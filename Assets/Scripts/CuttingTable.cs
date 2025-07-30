using System.Collections;
using UnityEngine;

public class CuttingTable : DefaultTable
{
    public override bool putDownItem(ItemType givenFood)
    {
        if (currentItem != ItemType.None) return false;

        currentItem = givenFood;
        var obj = Resources.Load<GameObject>($"Prefabs/{Table.itemTypeNameMap[currentItem]}");
        Instantiate(obj, transform);
        return true;
    }

    private IEnumerator cuttingRoutine()
    {
        yield return null;
    }
}
