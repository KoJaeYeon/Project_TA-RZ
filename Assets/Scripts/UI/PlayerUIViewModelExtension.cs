
using Zenject;

namespace ViewModel.Extensions
{
    public static class PlayerUIViewModelExtension
    {
        //[TODO_J][Inject] Player _player
        public static void RefreshViewModel(this PlayerUIViewModel vm)
        {
            //_player.RefreshManaInfo(vm.OnRefreshViewModel);
        }

        public static void OnRefreshViewModel(this PlayerUIViewModel vm, float hp, float skill, float stamina, int nowbullet)
        {
            vm.Nowbullets = nowbullet;
        }

        public static void OnRefreshPopulationViewModel(this PlayerUIViewModel vm, float hp, float skill, float stamina, int nowbullet)
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

        public static void OnResponseManaChange(this PlayerUIViewModel vm, float hp, float skill, float stamina, int nowbullet)
        {
            vm.Nowbullets = nowbullet;
        }
        //public static void OnResponsePopulationChange(this PlayerUIViewModel vm, int population, int maxPopulation)
        //{
        //    vm.Nowbullets = nowbullet;
        //    vm.Nowbullets = nowbullet;
        //}

    }
}
