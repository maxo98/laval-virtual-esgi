using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ComputerPlayerController : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private GameObject target;
    [SerializeField] private InputActionReference rotateAction, zoomAction, mouseLeftClick, mousePosition;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float zoomSpeed;
    
    // camera movements
    private float _rotation;
    private float _zoom;
    private bool _objectGrabbed;
    private GameObject _grabbedObject;
    
    // Start is called before the first frame update
    void Start()
    {
        mouseLeftClick.action.Enable();
        mouseLeftClick.action.performed += OnMouseLeftClickOn;
        mouseLeftClick.action.canceled += OnMouseLeftClickOff;
    }

    // Update is called once per frame
    void Update()
    {
        _rotation = rotateAction.action.ReadValue<float>();
        target.transform.Rotate(Vector3.up, _rotation * rotationSpeed * Time.deltaTime);
        _zoom = zoomAction.action.ReadValue<float>();
        var transform1 = camera.transform;
        transform1.position += transform1.forward * zoomSpeed * _zoom * Time.deltaTime;
        if (!_objectGrabbed) return;
        DragObject();
    }

    void DragObject()
    {
        var ray = camera.ScreenPointToRay(mousePosition.action.ReadValue<Vector2>());
        if (!Physics.Raycast(ray, out var hit, 100)) return;
        if (!hit.collider.gameObject.CompareTag("Map")) return;
        _grabbedObject.transform.position = hit.point;
    }

    void OnMouseLeftClickOn(InputAction.CallbackContext context)
    {
        if (_objectGrabbed)
            _objectGrabbed = false;
        else
        {
            var ray = camera.ScreenPointToRay(mousePosition.action.ReadValue<Vector2>());
            if (!Physics.Raycast(ray, out var hit, 100)) return;
            if (!hit.collider.gameObject.CompareTag("Generator")) return;
            _grabbedObject = hit.collider.gameObject;
            _objectGrabbed = true;
        }
    }

    void OnMouseLeftClickOff(InputAction.CallbackContext context)
    {
        
    }
}
