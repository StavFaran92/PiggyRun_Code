using System.Collections.Generic;

[System.Serializable]
public class WorldItem
{

    public List<LevelItem> levels;
    public string name;

    public WorldItem(List<LevelItem> levels, string name)
    {
        this.name = name;
        this.levels = levels;
    }
}