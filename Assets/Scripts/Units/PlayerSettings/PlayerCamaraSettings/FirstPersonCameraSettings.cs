using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Units.PlayerSettings.PlayerCamaraSettings
{
    [CreateAssetMenu(fileName = "FirstPersonCameraSettings", menuName = "ScriptableObjects/FirstPersonCameraSettings")]
    public class FirstPersonCameraSettings : CinemachineCameraSettings
    {
        [Header("First Person Specific")]
        [SerializeField] private Vector3 _firstPersonOffset = new Vector3(0, 1.7f, 0.2f);
        [SerializeField] private float _verticalClampAngle = 80f;
        [SerializeField] private float _mouseSensitivity = 2f;
        [SerializeField] private bool _enableHeadBob = true;
        [SerializeField] private float _headBobFrequency = 1f;
        [SerializeField] private float _headBobAmplitude = 0.1f;
        [SerializeField] private float _headBobSmoothness = 5f;

        public Vector3 FirstPersonOffset => _firstPersonOffset;
        public float VerticalClampAngle => _verticalClampAngle;
        public float MouseSensitivity => _mouseSensitivity;
        public bool EnableHeadBob => _enableHeadBob;
        public float HeadBobFrequency => _headBobFrequency;
        public float HeadBobAmplitude => _headBobAmplitude;
        public float HeadBobSmoothness => _headBobSmoothness;
    }
}
