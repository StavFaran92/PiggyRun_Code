[System.Serializable]
public class LevelItem
{

    public int stars;
    public int diamonds;

    public LevelItem(int stars, int diamonds)
    {
        this.stars = stars;
        this.diamonds = diamonds;
    }
}