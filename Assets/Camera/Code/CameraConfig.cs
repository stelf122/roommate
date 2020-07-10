using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Config/Camera")]
public class CameraConfig : ScriptableObject
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _viewSpeed;

    public float MoveSpeed() => _moveSpeed;
    public float ViewSpeed() => _viewSpeed;
}
