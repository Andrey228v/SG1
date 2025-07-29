using UnityEngine;

namespace Assets.Quternion
{
    public class AngleBeetwen : MonoBehaviour
    {
        [SerializeField] private Transform obj1;
        [SerializeField] private Transform obj2;

        private void Start()
        {
            float align = Vector3.Angle(obj1.position, obj2.position);
            
        }

        private void Update()
        {
            float align = Vector3.Angle(obj1.position, obj2.position);
            //print("Align: " + align);
            //Debug.DrawRay(new Vector3(0,0,0), obj1.transform.position, Color.yellow);
            //Debug.DrawRay(new Vector3(0,0,0), obj2.transform.position, Color.black);
        }


    }
}
