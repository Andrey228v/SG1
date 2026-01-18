using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

namespace Assets.Scripts.Services.Save
{

    /// <summary>
    /// Данный код - область для связывания UI со всеми остальными сервисами. Потом доделать если руки дойдут ....
    /// </summary>
    public class PlayerSaveHandler : IInitializable, IDisposable
    {
        private SignalBus _signalBus;
        private ISaveLoadService _saveService;
        private PlayerController _playerController;

        //[Inject]
        //public PlayerSaveHandler(
        //    SignalBus signalBus,
        //    ISaveLoadService saveService,
        //    PlayerController playerController)
        //{
        //    _signalBus = signalBus;
        //    _saveService = saveService;
        //    _playerController = playerController;
        //}

        public void Initialize()
        {
            //SubscribeToSignals();
            //LoadPlayerData();
        }

        public void Dispose()
        {
            //UnsubscribeFromSignals();
            //SavePlayerData();
        }

        //private void SubscribeToSignals()
        //{
        //    _signalBus.Subscribe<GameLoadedSignal>(OnGameLoaded);
        //    //_signalBus.Subscribe<PlayerPositionChangedSignal>(OnPositionChanged);
        //    //_signalBus.Subscribe<CoinCollectedSignal>(OnCoinCollected);
        //}

        //private void UnsubscribeFromSignals()
        //{
        //    _signalBus.Unsubscribe<GameLoadedSignal>(OnGameLoaded);
        //    //_signalBus.Unsubscribe<PlayerPositionChangedSignal>(OnPositionChanged);
        //    //_signalBus.Unsubscribe<CoinCollectedSignal>(OnCoinCollected);
        //}

        //private void OnGameLoaded(GameLoadedSignal signal)
        //{
        //    //LoadPlayerData();
        //}

        //private void OnPositionChanged(PlayerPositionChangedSignal signal)
        //{
        //    if (_saveService.CurrentSave != null)
        //    {
        //        _saveService.CurrentSave.Player.Position = signal.Position;
        //        _signalBus.Fire(new DataChangedSignal());
        //    }
        //}

        //private void OnCoinCollected(CoinCollectedSignal signal)
        //{
        //    //if (_saveService.CurrentSave != null)
        //    //{
        //    //    _saveService.CurrentSave.Player.Coins += signal.Value;
        //    //    _signalBus.Fire(new DataChangedSignal());
        //    //}
        //}

        //private void LoadPlayerData()
        //{
        //    //if (_saveService.CurrentSave == null) return;

        //    //var playerData = _saveService.CurrentSave.Player;
        //    //_playerController.transform.position = playerData.Position.ToVector3();
        //    //_playerController.SetCoins(playerData.Coins);
        //}

        //private void SavePlayerData()
        //{
        //    //if (_saveService.CurrentSave == null) return;

        //    //var playerData = _saveService.CurrentSave.Player;
        //    //playerData.Position = _playerController.transform.position;
        //    //playerData.Coins = _playerController.GetCoins();

        //    //_signalBus.Fire(new DataChangedSignal());
        //}
    }
}
