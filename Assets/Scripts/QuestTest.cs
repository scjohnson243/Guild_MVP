using UnityEngine;
using System.Collections.Generic;

public class QuestTest : MonoBehaviour
{
    private QuestLoader questLoader;

    private void Start()
    {
        questLoader = GetComponent<QuestLoader>();

        // Fake some hero levels for testing
        questLoader.AverageHeroLevel = 5;
        questLoader.HighestHeroLevel = 10;

        // Load the quests
        questLoader.LoadQuests();

        // Test if a random quest is correctly loaded and scaled
        TestRandomQuest();
    }

    private void TestRandomQuest()
    {
        QuestData testQuest = questLoader.GetRandomQuest();
        if (testQuest == null)
        {
            Debug.LogError("No quests were loaded.");
            return;
        }

        Debug.Log($"QUEST LOADED: {testQuest.Title}");
        Debug.Log($"Description: {testQuest.Description}");
        Debug.Log($"Offered By: {testQuest.OfferedBy}");
        Debug.Log($"Rarity: {testQuest.Rarity}");
        Debug.Log($"Base Gold Reward: {testQuest.BaseGoldReward}");
        Debug.Log($"Scaled Gold Reward (Level 5 Hero): {testQuest.GetScaledReward(5)}");
        Debug.Log($"Scaled Min Level: {testQuest.MinLevel}");
        Debug.Log($"Scaled Max Level: {testQuest.MaxLevel}");
    }
}