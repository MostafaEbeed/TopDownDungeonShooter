using UnityEngine;
using Sirenix.OdinInspector;

public abstract class BaseItemSO : SerializedScriptableObject
{
    [BoxGroup("General Info")]
    [PreviewField(64), HideLabel]
    public Sprite icon;

    [VerticalGroup("General Info/Split/Right")]
    [LabelText("Name"), Required]
    public string itemName;

    [BoxGroup("General Info")]
    [LabelText("Cost"), MinValue(0)]
    public int cost;
    
    [BoxGroup("Upgrade Info")]
    [LabelText("Upgrade Cost")]
    public int upgradeCost;
    
    [BoxGroup("Runtime")]
    [ReadOnly]
    public bool isOwned;

    public virtual void Upgrade()
    {
        // Logic for upgrade goes here (will be defined per type later)
        upgradeCost += Mathf.CeilToInt(upgradeCost * 0.5f); // Example: increase upgrade cost
    }
}