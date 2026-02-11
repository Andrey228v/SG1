using Assets.Scripts.Services.Save;
using TMPro;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.UI._1_MenuWindow.Menu
{
    public class Debug : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textSaveLoadExist;

        private SaveLoadService _saveLoadService;

        [Inject]
        public void Construct(SaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
        }


        private void Start() 
        {

            if (_saveLoadService.HasSave())
            {
                _textSaveLoadExist.text = "YES PR";
            }
            else
            {
                _textSaveLoadExist.text = "NO PR";
            }

            
        }

    }
}
