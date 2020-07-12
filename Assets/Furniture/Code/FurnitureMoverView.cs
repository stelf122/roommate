using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class FurnitureMoverView : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private FurnitureMover _furnitureMover;
    [SerializeField] private Transform _movingView;
    [SerializeField] private Transform _tipView;
    [SerializeField] private Button _done;
    [SerializeField] private InputField _leftDistance;
    [SerializeField] private InputField _rightDistance;

    private bool _showing;

    private void Start()
    {
        _done.onClick.AddListener(CompleteMoving);
        _furnitureMover.MovingChanged += ChangeView;

        ChangeView(false);
    }

    private void Update()
    {
        if (_showing)
        {
            ShowDistance(_leftDistance, _furnitureMover.LeftLinePoints());
            ShowDistance(_rightDistance, _furnitureMover.RightLinePoints());
        }
    }

    private void ShowDistance(InputField input, Vector3[] points)
    {
        Vector3 midPoint = (points[0] + points[1]) / 2;

        float distance = Vector3.Distance(points[0], points[1]);

        Vector2 screenPoint = _camera.WorldToScreenPoint(midPoint);

        input.GetComponent<RectTransform>().anchoredPosition = screenPoint + new Vector2(0, 30f);
        input.text = distance.ToString("0.0");
    }

    private void ChangeView(bool show)
    {
        _showing = show;
        _movingView.gameObject.SetActive(show);
        _tipView.gameObject.SetActive(!show);
    }

    private void CompleteMoving()
    {
        _furnitureMover.EndMoving();
    }
}
