using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    // Сигнал завершения загрузки
    public class BootCompleteSignal
    {
        public float LoadTime;
        public bool HasSave;
    }

    // Сигналы состояния приложения
    public class ApplicationFocusSignal
    {
        public bool HasFocus;
    }

    public class ApplicationPauseSignal
    {
        public bool IsPaused;
    }

    public class ApplicationQuitSignal { }

    // Сигналы ошибок
    public class BootErrorSignal
    {
        public string ErrorMessage;
        public bool IsCritical;
    }

    public class ProgressLoadSignal
    {
        public bool Success;
        public string ErrorMessage;
    }
}
