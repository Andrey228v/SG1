using System;

namespace Assets.Scripts.Utilites
{
    public interface IInitEvent
    {
        public event Action OnInitComplite;
    }
}
