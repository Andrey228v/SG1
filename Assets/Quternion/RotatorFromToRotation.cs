using UnityEngine;

namespace Assets.Quternion
{
    public class RotatorFromToRotation : MonoBehaviour
    {
        [SerializeField] private Transform _obj2;
        [SerializeField] private Transform _obj3;

        private void Start()
        {
            Debug.Log($"P: {transform.position} LP: {transform.localPosition}, tf: {transform.forward}");
            //Debug.DrawRay(transform.position, transform.forward, Color.red);
            //Debug.DrawRay(transform.position, _obj2.position - transform.position);
            //Debug.DrawRay(transform.position, _obj3.position - transform.position);

            //transform.rotation = Quaternion.FromToRotation(_obj2.position - transform.position, _obj3.position - transform.position);
            transform.rotation = Quaternion.FromToRotation(transform.up, _obj2.position - transform.position);
            _obj3.rotation = Quaternion.FromToRotation(_obj3.forward, transform.position - _obj3.position);
            Debug.DrawRay(transform.position, transform.up * 2, Color.red, 10f);
            Debug.DrawRay(transform.position, _obj2.position - transform.position, Color.green, 10f);
        }
    }
}
