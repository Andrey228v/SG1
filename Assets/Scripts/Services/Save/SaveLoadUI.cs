using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.Services.Save
{
    public class SaveLoadUI : MonoBehaviour
    {
        [Inject] private readonly ISaveLoadService _saveLoadService;
        [Inject] private readonly SignalBus _signalBus;

        [SerializeField] private Button _saveButton;
        [SerializeField] private Button _loadButton;
        [SerializeField] private Button _deleteButton;

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
        }

        private void OnDisable()
        {
            _saveButton.onClick.RemoveListener(SaveGame);
            _loadButton.onClick.RemoveListener(LoadGame);
            _deleteButton.onClick.RemoveListener(DeleteSave);
        }

        public void SaveGame()
        {
            Debug.Log("TEST");
            _saveLoadService.SaveGame();
            UpdateUI();
        }

        public void LoadGame()
        {
            _saveLoadService.LoadGame();
            UpdateUI();
        }

        public void DeleteSave()
        {
            _saveLoadService.DeleteSave();
            UpdateUI();
        }

        //public void NewGame()
        //{
        //    _saveLoadService.CreateNewSave();
        //    //_saveLoadService.LoadGame();
        //    UpdateUI();
        //}

        private void UpdateUI()
        {
            bool hasSave = _saveLoadService.HasSave;
            _loadButton.gameObject.SetActive(hasSave);
            _deleteButton.gameObject.SetActive(hasSave);
        }
    }
}
