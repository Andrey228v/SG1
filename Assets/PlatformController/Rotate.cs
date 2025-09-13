using DG.Tweening;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private float _durationRotate;
    [SerializeField] private float _angle;
    [SerializeField] private int _loop = -1;
    [SerializeField] private Vector3 _direction = Vector3.forward;
    [SerializeField] private RotateMode _rotateMode = RotateMode.WorldAxisAdd;
    [SerializeField] private Ease _easeMode = Ease.Linear;

    private void Start()
    {
        transform.DORotate(_direction * _angle, _durationRotate, _rotateMode)
            .SetLoops(_loop)
            .SetEase(_easeMode);
    }
}
