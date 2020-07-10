using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurniturePlacer : MonoBehaviour
{
    [SerializeField] private FurnitureListConfig _list;
    [SerializeField] private CameraFocus _cameraFocus;

    [ContextMenu("Start Select Wall")]
    private void StartSelectWall()
    {
        _cameraFocus.ChangeFocus(CameraFocus.FocusType.Walls);
    }

    [ContextMenu("End Select Wall")]
    private void EndSelectWall()
    {
        _cameraFocus.ChangeFocus(CameraFocus.FocusType.Free);
    }
}
