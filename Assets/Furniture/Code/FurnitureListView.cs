using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class FurnitureListView : MonoBehaviour
{
    [SerializeField] private FurnitureListConfig _furnitureListConfig;
    [SerializeField] private FurniturePlacer _furniturePlacer;
    [SerializeField] private FurnitureMover _furnitureMover;

    [SerializeField] private Text _moveAroundTip;
    [SerializeField] private Text _selectWallTip;

    [SerializeField] private Button _showFurnitureList;
    [SerializeField] private Button _hideFurnitreList;

    [SerializeField] private Transform _furnitureListRect;
    [SerializeField] private Button _furnitureButton;

    private void Start()
    {
        _showFurnitureList.onClick.AddListener(ShowFurnitureList);
        _hideFurnitreList.onClick.AddListener(HideFurnitureList);

        HideFurnitureList();
        CreateList();

        _selectWallTip.gameObject.SetActive(false);
        _furnitureMover.MovingChanged += MovingChanged;
    }

    private void CreateList()
    {
        var furniture = _furnitureListConfig.Furnitures();

        for (int i = 0; i < furniture.Length; i++)
        {
            int index = i;

            Button furnitureButton = Instantiate(_furnitureButton, _furnitureButton.transform.parent);

            furnitureButton.GetComponentInChildren<Text>().text = furniture[i].Name();

            furnitureButton.onClick.AddListener(() => 
            {
                _furniturePlacer.SelectFurniture(index);

                ShowSelectingWall();
            });
        }

        _furnitureButton.gameObject.SetActive(false);
    }

    private void MovingChanged(bool show)
    {
        if (!show)
        {
            _showFurnitureList.gameObject.SetActive(true);
            _moveAroundTip.gameObject.SetActive(true);
            _selectWallTip.gameObject.SetActive(false);
        }
    }

    private void ShowSelectingWall()
    {
        HideFurnitureList();

        _showFurnitureList.gameObject.SetActive(false);
        _moveAroundTip.gameObject.SetActive(false);
        _selectWallTip.gameObject.SetActive(true);
    }

    private void HideFurnitureList()
    {
        _furnitureListRect.gameObject.SetActive(false);
    }

    private void ShowFurnitureList()
    {
        _furnitureListRect.gameObject.SetActive(true);
    }
}
