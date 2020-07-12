using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurniturePlacer : MonoBehaviour
{
    [SerializeField] private FurnitureListConfig _list;
    [SerializeField] private CameraFocus _cameraFocus;
    [SerializeField] private FurnitureMover _furnitureMover;

    private bool _selectingWall;
    private GameObject _furniturePrefab;

    private void Start()
    {
        WallSelection.Selected += SelectWall;
    }

    private void OnDestroy()
    {
        WallSelection.Selected -= SelectWall;
    }

    private void SelectWall(WallSelection wall)
    {
        if (!_selectingWall)
            return;

        _selectingWall = false;

        GameObject furnitureInstance = SpawnFurniture(wall.transform);
        
        _furnitureMover.StartMoving(furnitureInstance, wall.gameObject);
        _cameraFocus.ChangeFocus(CameraFocus.FocusType.Target, wall.transform);
    }

    private GameObject SpawnFurniture(Transform wall)
    {
        var instance = Instantiate(_furniturePrefab, wall.position, wall.transform.rotation);

        // Offset furniture size
        instance.transform.position += instance.transform.forward * (instance.transform.localScale.z / 2);

        // Offset wall size
        instance.transform.position += instance.transform.forward * (Mathf.Min(wall.transform.localScale.x, wall.transform.localScale.z) / 2);

        return instance;
    }

    public void SelectFurniture(int index)
    {
        _furniturePrefab = _list.GetFurniturePrefab(index);

        _cameraFocus.ChangeFocus(CameraFocus.FocusType.Walls);
        _selectingWall = true;
    }

    [ContextMenu("Select First Furniture")]
    private void SelectFirstFurniture()
    {
        SelectFurniture(0);
    }
}
