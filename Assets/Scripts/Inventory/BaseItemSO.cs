using UnityEngine;
using Sirenix.OdinInspector;

public abstract class BaseItemSO : SerializedScriptableObject
{
    [BoxGroup("General Info")]
    [PreviewField(64), HideLabel]
    public Sprite icon;

    [BoxGroup("General Info")]
    [HorizontalGroup("General Info/Split", 70)]
    [VerticalGroup("General Info/Split/Left")]
    [HideLabel]
    [ReadOnly]
    [PreviewField(70)]
    public Sprite previewIcon;

    [VerticalGroup("General Info/Split/Right")]
    [LabelText("Name"), Required]
    public string itemName;

    [BoxGroup("General Info")]
    [LabelText("Cost"), MinValue(0)]
    public int cost;
}