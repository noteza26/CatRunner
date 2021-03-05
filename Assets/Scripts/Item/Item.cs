using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeItemEnum
{
    Coin,
    Item,
}
public enum TypeItemAddEnum
{
    None,
    AddHealth,
    FullHealth,
    ExtraScore,
    SpeedBoost,
}
[CreateAssetMenu(fileName = "New Item ", menuName = "Item/Create Item", order = 0)]
public class Item : ScriptableObject
{
    public bool IsExtra;
    public TypeItemEnum Type;

    public TypeItemAddEnum TypeItem;

    public float AddTo;

}
