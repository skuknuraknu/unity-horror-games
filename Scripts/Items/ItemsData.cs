using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType {
    equipable,
    consumable,
    resource
}

[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemsData : ScriptableObject{
    [Header("Information")]
    public string ItemName;
    public string ItemDescription;
    public ItemType type;
    public Sprite icon;
    public GameObject dropPrefab;

    [Header("Staking Items")]
    public bool canStack;
    public int maxStackAmount;
}
