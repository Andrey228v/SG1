using System;
using UnityEngine;

namespace Assets.Scripts.DetectorProperties.GroundCheckerStrategy
{
    
    public abstract class AGroundCheckerStrategy : ScriptableObject
    {
        public abstract event Action<bool> OnGround;
        public abstract event Action<bool, RaycastHit> OnGroundNormal;

        public abstract Vector3 GetGroundNormal();

        public abstract float GetGroundDistance();

        public abstract void CheckGround(Transform unit);

        public abstract void OnDrawGizmos(Transform unit);

    }
}
