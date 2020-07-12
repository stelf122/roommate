using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Config/FurnitureList")]
public class FurnitureListConfig : ScriptableObject
{
    [SerializeField] private FurnitureInfo[] _furniture;

    public FurnitureInfo[] Furnitures() => _furniture;

    public GameObject GetFurniturePrefab(int index)
    {
        return _furniture[index].Prefab();
    }

    [Serializable]
    public struct FurnitureInfo
    {
        [SerializeField] private string _name;
        [SerializeField] private GameObject _prefab;

        public string Name() => _name;
        public GameObject Prefab() => _prefab;
    }
}
