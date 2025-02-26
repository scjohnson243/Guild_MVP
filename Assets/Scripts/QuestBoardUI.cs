using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;
using System.Collections;

public class QuestBoardUI : MonoBehaviour
{
    private VisualElement root;
    private ScrollView questList;
    private Button assignButton;
    private Button rejectButton;

    private QuestLoader questLoader;
    private List<QuestData> activeQuests = new List<QuestData>();
    private QuestData selectedQuest;

    private void OnEnable()
    {
        var uiDocument = GetComponent<UIDocument>();
        root = uiDocument.rootVisualElement;

        questList = root.Q<ScrollView>("quest-list");
        assignButton = root.Q<Button>("assignButton");
        rejectButton = root.Q<Button>("rejectButton");

        assignButton.clicked += OnAssignClicked;
        rejectButton.clicked += OnRejectClicked;

        StartCoroutine(WaitForQuestLoader());
    }

    private IEnumerator WaitForQuestLoader()
    {
        // Wait until QuestLoader is available and has loaded quests
        while (questLoader == null || questLoader.Quests.Count == 0)
        {
            questLoader = FindObjectOfType<QuestLoader>();
            yield return new WaitForSeconds(0.1f);
        }

        Debug.Log($"Quests in QuestLoader AFTER WAITING: {questLoader.Quests.Count}");
        LoadQuests();
    }

    private void LoadQuests()
    {
        questList.Clear(); // Remove old quests

        activeQuests = questLoader.Quests;
        Debug.Log($"Quests in QuestLoader AFTER LOAD: {activeQuests.Count}");

        foreach (QuestData quest in activeQuests)
        {
            Button questButton = new Button();
            questButton.text = $"{quest.Title} ({quest.Rarity})";

            // Apply correct style class based on rarity
            switch (quest.Rarity)
            {
                case QuestRarity.Common:
                    questButton.AddToClassList("common-quest");
                    break;
                case QuestRarity.Rare:
                    questButton.AddToClassList("rare-quest");
                    break;
                case QuestRarity.Legendary:
                    questButton.AddToClassList("legendary-quest");
                    break;
            }

            questButton.clicked += () => SelectQuest(quest);
            questList.Add(questButton);
        }
    }

    private void SelectQuest(QuestData quest)
    {
        selectedQuest = quest;
        Debug.Log($"Selected Quest: {quest.Title}");
    }

    private void OnAssignClicked()
    {
        if (selectedQuest == null)
        {
            Debug.LogWarning("No quest selected.");
            return;
        }
        Debug.Log($"Assigning quest: {selectedQuest.Title}");
    }

    private void OnRejectClicked()
    {
        if (selectedQuest == null)
        {
            Debug.LogWarning("No quest selected.");
            return;
        }
        Debug.Log($"Rejecting quest: {selectedQuest.Title}");
        activeQuests.Remove(selectedQuest);
        LoadQuests();
    }
}
