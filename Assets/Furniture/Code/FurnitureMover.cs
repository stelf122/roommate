using UnityEngine;
using System.Collections;
using System;

public class FurnitureMover : MonoBehaviour
{
    public event Action<bool> MovingChanged = delegate { };

    public Vector3[] LeftLinePoints() => _leftLinePoints;
    public Vector3[] RightLinePoints() => _rightLinePoints;

    [SerializeField] private CameraFocus _cameraFocus;
    [SerializeField] private LineRenderer _leftLine;
    [SerializeField] private LineRenderer _rightLine;
    [SerializeField] private LayerMask _raycastlayers;
    
    private GameObject _furniture;
    private GameObject _wall;
    private bool _moving = false;
    
    private Vector3[] _leftLinePoints;
    private Vector3[] _rightLinePoints;

    private void Start()
    {
        _leftLine.enabled = false;
        _rightLine.enabled = false;
    }

    public void StartMoving(GameObject furniture, GameObject wall)
    {
        _moving = true;
        _furniture = furniture;
        _wall = wall;

        _leftLinePoints = new Vector3[] { new Vector3(), new Vector3() };
        _rightLinePoints = new Vector3[] { new Vector3(), new Vector3() };

        _leftLine.enabled = true;
        _rightLine.enabled = true;

        MovingChanged(_moving);
    }
    
    public void EndMoving()
    {
        _moving = false;
        _cameraFocus.ChangeFocus(CameraFocus.FocusType.Free);

        _leftLine.enabled = false;
        _rightLine.enabled = false;

        MovingChanged(_moving);
    }

    private void Update()
    {
        if (_moving)
        {
            if (Input.GetMouseButton(0))
            {
                Vector2 moveInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

                _furniture.transform.position += _furniture.transform.right * -moveInput.x;
                _furniture.transform.position += _furniture.transform.up * moveInput.y;

                DrawLine(_rightLine, _rightLinePoints, _furniture.transform.right);
                DrawLine(_leftLine, _leftLinePoints, -_furniture.transform.right);
            }
        }
    }

    private void DrawLine(LineRenderer line, Vector3[] points, Vector3 direction)
    {
        Ray ray = new Ray();
        RaycastHit hit;

        ray.origin = _furniture.transform.position;
        ray.direction = direction;
        
        ray.origin += direction * _furniture.transform.localScale.x / 2;
        ray.origin -= _furniture.transform.forward * (_furniture.transform.localScale.z / 2 - 0.25f);
        ray.origin -= _furniture.transform.up * _furniture.transform.localScale.y / 2;

        if (Physics.Raycast(ray, out hit, 10f, _raycastlayers))
        {
            points[0] = ray.origin;
            points[1] = hit.point;

            line.SetPositions(points);
        }
    }
}
