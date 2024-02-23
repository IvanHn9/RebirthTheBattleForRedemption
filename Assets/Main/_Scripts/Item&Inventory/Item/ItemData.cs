using System.Text;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif
public enum ItemType
{
    Equipment,
    Consumable
}
public class ItemData : ScriptableObject
{
    public ItemType itemType;
    public string itemName;
    public string itemId;
    public Sprite itemIcon;

    protected StringBuilder sb = new StringBuilder();
    private void OnValidate()
    {
#if UNITY_EDITOR
        string path = AssetDatabase.GetAssetPath(this);
        itemId = AssetDatabase.AssetPathToGUID(path);
#endif
    }

    public virtual string GetDescription()
    {
        return "";
    }
}
