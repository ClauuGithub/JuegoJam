using UnityEngine;

[System.Serializable]
public struct SeasoningIconData
{
    public Seasoning seasoning;
    public Sprite icon;
}

[CreateAssetMenu(fileName = "SeasoningIcons", menuName = "Cooking/Seasoning Icons")]
public class SeasoningIconDatabase : ScriptableObject
{
    public SeasoningIconData[] icons;

    public Sprite GetIcon(Seasoning seasoning)
    {
        foreach (var data in icons)
        {
            if (data.seasoning == seasoning)
                return data.icon;
        }
        return null;
    }
}
