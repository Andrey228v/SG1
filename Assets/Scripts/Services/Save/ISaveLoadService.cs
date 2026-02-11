using System;

namespace Assets.Scripts.Services.Save
{
    public interface ISaveLoadService
    {
        //public bool HasSave { get; }
        public GameSaveData CurrentSave { get; }

        public void SaveGame();
        public void LoadGame();
        public void DeleteSave();
        public void CreateNewSave();
        public bool HasSave();
        public bool IsFirstLoad();

        public event Action OnGameSaved;
        public event Action OnGameLoaded;
    }
}
