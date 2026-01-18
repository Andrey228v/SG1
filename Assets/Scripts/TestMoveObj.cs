using ECM2;
using UnityEngine;

namespace Assets.Scripts
{
    public class TestMoveObj : MonoBehaviour
    {

        public Character CharacterView { get; private set; }

        private Vector3 _moveDirection = Vector3.forward;

        private void Awake()
        {
            CharacterView = GetComponent<Character>();
        }

        private void Update()
        {
            CharacterView.SetMovementDirection(_moveDirection);
        }

    }
}
