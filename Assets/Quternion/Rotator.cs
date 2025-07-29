using UnityEngine;

namespace Assets.Quternion
{
    public class Rotator : MonoBehaviour
    {
        [SerializeField] private Vector3 _angle = new Vector3(10f, 45f, 10f);

        private void Start()
        {
            Quaternion q1 = Quaternion.Euler(_angle); 
            transform.rotation = q1;
        }
    }
}
