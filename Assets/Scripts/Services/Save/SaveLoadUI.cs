using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.Services.Save
{
    //1)При активации чекпоинта необходимо обновлять UI...
    public class SaveLoadUI : MonoBehaviour
    {
        [Inject] private readonly ISaveLoadService _saveLoadService;
        [Inject] private readonly SignalBus _signalBus;

        [SerializeField] private PlayerController _playerController; // затычка ... 
        [SerializeField] private Button _saveButton;
        [SerializeField] private Button _loadButton;
        [SerializeField] private Button _deleteButton;
        [SerializeField] private Button _testButton;

        private void Start()
        {
            //NewGame();
            UpdateUI();
        }

        private void OnEnable()
        {
            _saveButton.onClick.AddListener(SaveGame);
            _loadButton.onClick.AddListener(LoadGame);
            _deleteButton.onClick.AddListener(DeleteSave);
            _testButton.onClick.AddListener(TestInvoke);

            _signalBus.Subscribe<CheckpointActivatedSignal>(UpdateUI);
        }

        private void OnDisable()
        {
            _saveButton.onClick.RemoveListener(SaveGame);
            _loadButton.onClick.RemoveListener(LoadGame);
            _deleteButton.onClick.RemoveListener(DeleteSave);
            _testButton.onClick.RemoveListener(TestInvoke);

            _signalBus.Unsubscribe<CheckpointActivatedSignal>(UpdateUI);
        }

        public void SaveGame()
        {
            _saveLoadService.SaveGame();
            UpdateUI();
        }

        public void LoadGame()
        {
            Debug.Log("LoadGame");
            _saveLoadService.LoadGame();
            UpdateUI();
        }

        public void DeleteSave()
        {
            _saveLoadService.DeleteSave();
            UpdateUI();
        }

        public void TestInvoke()
        {
            _playerController.Test();
        }

        //public void NewGame()
        //{
        //    _saveLoadService.CreateNewSave();
        //    //_saveLoadService.LoadGame();
        //    UpdateUI();
        //}

        private void UpdateUI()
        {
            bool hasSave = _saveLoadService.HasSave();
            _loadButton.gameObject.SetActive(hasSave);
            _deleteButton.gameObject.SetActive(hasSave);
        }
    }
}
