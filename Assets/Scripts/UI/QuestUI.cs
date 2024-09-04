using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class QuestUI : MonoBehaviour
{
    [Inject] Player _player;
    [Inject] DataManager _dataManager;

    [SerializeField] Text UI_Quest_Text_Content;
    [SerializeField] GameObject Success;
    [SerializeField] GameObject Failure;

    private void OnEnable()
    {
        Success.SetActive(false);
        Failure.SetActive(false);

        _player.RemoveAllQuest();

        int rand = Random.Range(0, 3);
        UI_Quest_Text_Content.text = _dataManager.GetString($"UI_Quest_Text_Content_{rand}");
        switch (rand)
        {
            case 0:                
                _player.RegisterDashQuest(QuestClear);
                break;
            case 1:
                _player.RegisterHitQuest(QuestClear);
                break;
            case 2:
                _player.RegisterSkillQuest(QuestClear);
                break;
        }
    }

    public void QuestClear(bool isClear)
    {
        if (isClear)
        {
            Success.SetActive(true);
        }
        else
        {
            Failure.SetActive(true);
            _player.RemoveAllQuest();
        }
    }
}
