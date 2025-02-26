using System;
using System.Xml.Serialization;

[Serializable]
public class QuestData
{
    [XmlAttribute("Title")] 
    public string Title { get; set; }  // Added `{ get; set; }` to match XML attributes

    [XmlElement("Description")] 
    public string Description { get; set; }

    [XmlElement("OfferedBy")] 
    public string OfferedBy { get; set; }

    [XmlElement("Rarity")] 
    public QuestRarity Rarity { get; set; }

    [XmlElement("BaseGoldReward")] 
    public int BaseGoldReward { get; set; }

    [XmlElement("MinLevel")] 
    public int MinLevel { get; set; }  

    [XmlElement("MaxLevel")] 
    public int MaxLevel { get; set; }  

    public void CalculateQuestLevel(int averageHeroLevel, int highestHeroLevel)
    {
        MinLevel = Math.Max(1, averageHeroLevel - 2);
        MaxLevel = Math.Min(20, highestHeroLevel + 2);
    }

    public int GetScaledReward(int heroLevel)
    {
        float multiplier = GetRarityMultiplier();
        int scaledGold = (heroLevel * BaseGoldReward) * (int)multiplier;
        return scaledGold;
    }

    private float GetRarityMultiplier()
    {
        switch (Rarity)
        {
            case QuestRarity.Common: return 1.0f;
            case QuestRarity.Rare: return 1.5f;
            case QuestRarity.Legendary: return 3.0f;
            default: return 1.0f;
        }
    }
}

public enum QuestRarity
{
    Common,
    Rare,
    Legendary
}