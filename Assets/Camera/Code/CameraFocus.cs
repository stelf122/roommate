using UnityEngine;
using System.Collections;

public class CameraFocus : MonoBehaviour
{
    [SerializeField] private CameraMovement _cameraMovement;
    [SerializeField] private Transform _lookWalls;

    private SavedState wallsState;
    private SavedState freeState;

    private void Start()
    {
        wallsState = new SavedState(_lookWalls);
    }

    public void ChangeFocus(FocusType focus)
    {
        switch (focus)
        {
            case FocusType.Free:
                freeState.Load(transform);

                _cameraMovement.enabled = true;
                break;

            case FocusType.Walls:
                freeState = new SavedState(transform);

                wallsState.Load(transform);

                _cameraMovement.enabled = false;
                break;
        }
    }

    public enum FocusType
    {
        Free,
        Walls
    }

    private struct SavedState
    {
        private Vector3 _position;
        private Quaternion _rotation;

        public SavedState(Transform transform)
        {
            _position = transform.transform.position;
            _rotation = transform.transform.rotation;
        }

        public void Load(Transform transform)
        {
            transform.position = _position;
            transform.rotation = _rotation;
        }
    }
}
