using System.Collections.Generic;
using UnityEngine;

public class SaveController : MonoBehaviour
{
    [SerializeField] private Transform _savePointParent;
    [SerializeField] private int _saveIndex = 0;

    private List<Transform> _savePoints;

    private void Awake()
    {
        
    }
}
