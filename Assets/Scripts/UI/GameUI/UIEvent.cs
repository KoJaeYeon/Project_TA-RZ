using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;
using Zenject;

public class UIEvent
{
    [Inject] Player player { get; }

    private Action<float> _progressCallBack;
    private Action<StageType> _stageCallBack;
    private GameUI _gameUI;
    private MapManager _mapManager;
    public PlayerUIView PlayerUIView { get; private set; }
    public LoadingUI _loadUI { get; private set; }
    public BlueChipUI BlueChipUI { get; private set; }
    public ShopUI ShopUI { get; private set; }
    public PassiveShopUI PassiveShopUI { get; private set; }
    public InteractUI InteractUI { get; private set; }

    public AchievementUI AchievementUI { get; private set; }
    public QuestUI QuestUI { get; private set; }
    public MenuUI MenuUI { get; private set; }

    #region PlayerUIEvent
    public void RegisterPlayerUI(PlayerUIView playerUI)
    {
        PlayerUIView = playerUI;
    }

    public void SetActivePlayerUI(bool isActive)
    {
        PlayerUIView.gameObject.SetActive(isActive);
    }
    #endregion
    #region ChoiceEvent
    public void RegisterGameUI(GameUI gameUI)
    {
        _gameUI = gameUI;
    }

    public void OnGameUI()
    {
        _gameUI.gameObject.SetActive(true);
    }

    public void RequestChangeStage(StageType type)
    {
        _stageCallBack?.Invoke(type);
    }

    public void RegisterChangeStage(MapManager mapManager)
    {
        _mapManager = mapManager;
        _stageCallBack += mapManager.ChangeMap;
    }

    public void UnRegisterChangeStage()
    {
        _stageCallBack = null;
    }

    public float GetProgressValue()
    {
        return _mapManager.ProgressValue;
    }

    #endregion
    #region ProgressUIEvent
    //이 메서드 호출
    public void RequestChangeProgressBar(float currentValue)
    {
        _progressCallBack?.Invoke(currentValue);
    }

    public void RegisterChangeProgressUI(ProgressUIViewModel viewModel)
    {
        _progressCallBack += viewModel.OnResponseChangeCurrentStage;
    }

    public void UnRegisterChangeProgressUI(ProgressUIViewModel viewModel)
    {
        _progressCallBack -= viewModel.OnResponseChangeCurrentStage;
    }
    #endregion
    #region BlueChipEvent
    public void RegisterBlueChipUI(BlueChipUI blueChipUI)
    {
        BlueChipUI = blueChipUI;
    }

    public void ActiveBlueChipUI(Chest chest)
    {
        BlueChipUI.gameObject.SetActive(true);
        InteractUI.gameObject.SetActive(false);
        BlueChipUI.chest = chest;
        if (QuestUI.isSuccess == true)
        {
            BlueChipUI.QuestCleared();
            QuestUI.isSuccess = false;
        }
    }

    public void DeActiveBlueChipUI()
    {
        BlueChipUI.gameObject.SetActive(false);
        InteractUI.gameObject.SetActive(true);
    }
    #endregion
    #region ShopEvent
    public void RegisterShopUI(ShopUI shopUI)
    {
        ShopUI = shopUI;
    }

    public void ActiveShopUI()
    {
        InteractUI.gameObject.SetActive(false);
        MenuUI.gameObject.SetActive(false);
        ShopUI.gameObject.SetActive(true);
    }

    public void DeActiveShopUI()
    {
        InteractUI.gameObject.SetActive(true);
        MenuUI.gameObject.SetActive(true);
        ShopUI.gameObject.SetActive(false);
    }
    #endregion
    #region PassiveShopEvent
    public void RegisterPassiveShopUI(PassiveShopUI passiveshopUI)
    {
        PassiveShopUI = passiveshopUI;
    }

    public void ActivePassiveShopUI()
    {
        InteractUI.gameObject.SetActive(false);
        MenuUI.gameObject.SetActive(false);
        PassiveShopUI.gameObject.SetActive(true);
    }

    public void DeActivePassiveShopUI()
    {
        InteractUI.gameObject.SetActive(true);
        MenuUI.gameObject.SetActive(true);
        PassiveShopUI.gameObject.SetActive(false);
    }
    #endregion
    #region InteractEvent
    public void RegisterInteractUI(InteractUI interactUI)
    {
        InteractUI = interactUI;
    }

    public void ActiveInteractUI(string interactStr)
    {
        InteractUI.gameObject.SetActive(true);
        InteractUI.OnSetText(interactStr);
    }

    public void DeActiveInteractUI()
    {
        InteractUI.gameObject.SetActive(false);
    }
    #endregion
    #region AchievementEvent
    public void RegisterAchievementUI(AchievementUI achievementUI)
    {
        AchievementUI = achievementUI;
    }

    public void ActiveAchievementUI(string achieveStr)
    {
        AchievementUI.gameObject.SetActive(true);
        AchievementUI.OnSetText(achieveStr);
    }
    #endregion
    #region QuestEvent
    public void RegisterQuestUI(QuestUI questUI)
    {
        QuestUI = questUI;
    }

    public void ActiveQuestUI()
    {
        QuestUI.gameObject.SetActive(true);
    }

    public void DeactiveQuestUI()
    {
        QuestUI.gameObject.SetActive(false);
    }
    #endregion
    #region MenuEvent
    public void RegisterMenuUI(MenuUI menuUI)
    {
        MenuUI = menuUI;
    }

    public void SetActiveMenuUI()
    {
        MenuUI.OnEnableMenuUI();
    }

    public void OutLobbyMenuUI()
    {
        MenuUI.OutLobby();
    }
    #endregion


    #region PlayerControl
    public void SetActivePlayerControl(bool isActive)
    {
        var input = player.GetComponent<PlayerInput>();
        input.enabled = isActive;
    }
    #endregion

    #region LoadUI
    public void RegisterLoadUI(LoadingUI loadingUI)
    {
        _loadUI = loadingUI;
    }
    #endregion
}


