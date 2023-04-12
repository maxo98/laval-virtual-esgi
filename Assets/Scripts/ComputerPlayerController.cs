using System;
using System.Collections;
using System.Collections.Generic;
using DragAndDropObjects;
using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;

public class ComputerPlayerController : MonoBehaviour
{
    [SerializeField] private DragAndDropManager dragAndDropManager;
    [SerializeField] private Camera camera;
    public GameObject target;
    public Generator mapGenerator;
    [SerializeField] private InputActionReference rotateAction, zoomAction, mouseLeftClick, mousePosition;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float zoomSpeed;
    [SerializeField] private List<GameObject> generatorPrefabs;
    public bool IsSinglePlayer { get; set; }
    public bool DraggedObjectCanBePlaced { private get; set; }
    
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
        var plane = new Plane(Vector3.up, new Vector3(0 ,0,0));
        var ray = camera.ScreenPointToRay(mousePosition.action.ReadValue<Vector2>());
       if (!plane.Raycast(ray, out var hit)) return;
        //if (!Physics.Raycast(ray, out var hit, 100)) return;
        var position = ray.GetPoint(hit);
        position.y = 1;
        _grabbedObject.transform.position = position;
    }

    void OnMouseLeftClickOn(InputAction.CallbackContext context)
    {
        if (_objectGrabbed && DraggedObjectCanBePlaced)
        {
            _objectGrabbed = false;
            dragAndDropManager.placedGenerators.Add(_grabbedObject.GetComponent<GeneratorBehavior>());
        }
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

    public void CreateNewGenerator(int type)
    {
        if (IsSinglePlayer)
        {
            _grabbedObject = Instantiate(generatorPrefabs[type]); // instanciation des objets a placé dans la scene
            _grabbedObject.transform.position = new Vector3(0, 1, 0);
            var generatorBehavior = _grabbedObject.GetComponent<GeneratorBehavior>();
            generatorBehavior.controller = this;
            generatorBehavior.dragAndDropManager = dragAndDropManager;
        }
        else
        {
            _grabbedObject = PhotonNetwork.Instantiate(generatorPrefabs[type].name, new Vector3(0, 1, 0), Quaternion.identity);
            _objectGrabbed = true;
            var generatorBehavior = _grabbedObject.GetComponent<GeneratorBehavior>();
            generatorBehavior.controller = this;
            generatorBehavior.dragAndDropManager = dragAndDropManager;
            generatorBehavior.mapGenerator = mapGenerator;
        }
        
    }
}
