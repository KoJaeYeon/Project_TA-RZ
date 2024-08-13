
using Zenject;

namespace ViewModel.Extensions
{
    public static class PlayerUIViewModelExtension
    {
        public static void RefreshViewModel(this PlayerUIViewModel vm)
        {
            //_player.RefreshStatDataInfo(vm.OnRefreshViewModel);
        }

        public static void OnRefreshViewModel(this PlayerUIViewModel vm, float hp, float stamina, float skill, int nowbullet, float maxHp)
        {
            vm.Nowbullets = nowbullet;
        }

        public static void OnRefreshPopulationViewModel(this PlayerUIViewModel vm, float hp, float stamina, float skill, int nowbullet, float maxHp)
        {
            vm.Nowbullets = nowbullet;
            vm.Nowbullets = nowbullet;
        }

        public static void RegisterEventsOnEnable(this PlayerUIViewModel vm)
        {
            //GameManager.Instance.RegisterManaChangeCallback(vm.OnResponseManaChange);
        }

        public static void UnRegisterOnDisable(this PlayerUIViewModel vm)
        {
            //GameManager.Instance.UnRegisterManaChangeCallback(vm.OnResponseManaChange);
        }

        public static void OnResponseManaChange(this PlayerUIViewModel vm, float hp, float stamina, float skill, int nowbullet, float maxhp, float maxHp)
        {
            vm.Nowbullets = nowbullet;
        }
    }
}
