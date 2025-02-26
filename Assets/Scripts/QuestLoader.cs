using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

[XmlRoot("Quests")]
public class QuestDatabase
{
    [XmlElement("Quest")]
    public List<QuestData> Quests { get; set; }
}

public class QuestLoader : MonoBehaviour
{
    private const string FILE_PATH = "Assets/QuestData/Quests.xml";


    public List<QuestData> Quests { get; private set; } = new List<QuestData>();

    public int AverageHeroLevel { get; set; }
    public int HighestHeroLevel { get; set; }

    private void Start()
    {
        LoadQuests();
    }

    public void LoadQuests()
    {
        if (!File.Exists(FILE_PATH))
        {
            Debug.LogError("Quest XML file not found: " + FILE_PATH);
            return;
        }

        XmlSerializer serializer = new XmlSerializer(typeof(QuestDatabase));
        using (StreamReader reader = new StreamReader(FILE_PATH))
        {
            QuestDatabase database = (QuestDatabase)serializer.Deserialize(reader);
            Quests = database.Quests;
        }

        Debug.Log($"Loaded {Quests.Count} quests from XML.");
    }

    public QuestData GetRandomQuest()
    {
        if (Quests == null || Quests.Count == 0) return null;

        int randomIndex = Random.Range(0, Quests.Count);
        QuestData selectedQuest = Quests[randomIndex];

        selectedQuest.CalculateQuestLevel(AverageHeroLevel, HighestHeroLevel);

        return selectedQuest;
    }
}