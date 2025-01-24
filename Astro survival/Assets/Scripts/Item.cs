using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Scriptable object/item")]
public class Item : ScriptableObject {
    [Header("Only gameplay")]
    public Transform prefab;
    public ItemType type;
    public ActionType actionType;
    public Vector2Int range = new Vector2Int(5,4);
    public string Name = "ItemName";

    [Header("Only UI")]
    public bool stackable = true;
    [Header("Both")]
    public Sprite image;
}

public enum ItemType {
    Building,
    Resource
}

public enum ActionType {
    Move,
    Extract
}
