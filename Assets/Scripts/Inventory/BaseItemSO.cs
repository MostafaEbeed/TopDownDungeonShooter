using UnityEngine;
using Sirenix.OdinInspector;

public abstract class BaseItemSO : SerializedScriptableObject
{
    [BoxGroup("General Info")]
    [PreviewField(64), HideLabel]
    public Sprite icon;

    [BoxGroup("General Info")]
    [LabelText("Name"), Required]
    public string itemName;

    [BoxGroup("General Info")]
    [LabelText("Cost"), MinValue(0)]
    public int cost;

    [BoxGroup("Upgrade Info")]
    [LabelText("Upgrade Cost"), MinValue(0)]
    public int upgradeCost;

    [BoxGroup("Progression")]
    [LabelText("Level"), MinValue(1)]
    public int level = 1;

    public virtual void Upgrade()
    {
        level++;
        upgradeCost = Mathf.CeilToInt(upgradeCost * 1.5f); 
    }
}
