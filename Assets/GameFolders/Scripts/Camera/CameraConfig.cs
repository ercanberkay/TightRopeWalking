using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "CameraConfig", menuName = "Game/Camera/CameraConfig", order = 0)]
[InlineEditor]
public class CameraConfig : ScriptableObject
{
    [SerializeField] private Vector3 _position;
    public Vector3 Position => _position;

    [SerializeField] private Vector3 _rotation;
    public Vector3 Rotation => _rotation;

}

