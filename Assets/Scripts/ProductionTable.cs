using UnityEngine;

public class ProductionTable : Table
{
    [SerializeField]
    private ItemType productionItemType;

    public override ItemType takeItem()
    {
        return productionItemType;
    }

    public override bool putDownItem(ItemType givenFood)
    {
        return false;
    }
}
