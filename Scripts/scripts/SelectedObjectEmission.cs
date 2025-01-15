using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class InfoHitObject
{
    public Material material;
    public ListInteractions listInteractions;
}
public class SelectedObjectEmission : MonoBehaviour
{
    private bool _isFlag;
    private bool _isHover;
    private InfoHitObject _selectObject;
    private List<GameObject> _listObjects = new(10);

    [SerializeField] private Color colorEnterStartEmission;
    [SerializeField] private Color colorEnterEndEmission;
    [SerializeField] private Color colorExitEmission;

    [Space(15)]

    [SerializeField] private float intensity;
    [SerializeField] private float maxDistance;
    [SerializeField] private LayerMask layerMask;

    [Space(15)]

    [SerializeField] private Camera mainCamera;

    private void Update()
    {
        if (_isFlag)
        {
            Color currentColor = Color.Lerp(colorEnterStartEmission, colorEnterEndEmission, (Mathf.Sin(Time.time) + 1) / 2);

            _selectObject.material.SetColor("_EmissionColor", currentColor * intensity);

            foreach (var s in _selectObject.listInteractions.inputInteractions)
            {
                if (s.reference.action.triggered)
                {
                    s.unityEvent.Invoke();
                    break;
                }
            }
        }
    }
    private void FixedUpdate()
    {
        Ray ray = new(mainCamera.transform.position, mainCamera.transform.forward);

        bool flag = Physics.Raycast(ray, out var hit, maxDistance, layerMask);

        if (flag && !_isHover)
        {
            _isFlag = true;
            _isHover = true;

            var temp = hit.transform.GetComponent<ListInteractions>();
            _selectObject = new()
            {
                material = temp.meshRenderer.material,
                listInteractions = temp,
            };

            foreach (var s in _selectObject.listInteractions.inputInteractions)
            {
                s.reference.action.Enable();
            }

            _selectObject.listInteractions.unityEventHover.Invoke();

            foreach (var s in _selectObject.listInteractions.texts)
            {
                _listObjects.Add(GameObject.Instantiate(s, _selectObject.listInteractions.storage.transform).gameObject);
            }
        }
        if (!flag && _isHover)
        {
            _isHover = false;
            _isFlag = false;

            _selectObject.material.SetColor("_EmissionColor", colorExitEmission);

            foreach (var s in _selectObject.listInteractions.inputInteractions)
            {
                s.reference.action.Disable();
            }

            _selectObject.listInteractions.unityEventLeave.Invoke();

            foreach (var s in _listObjects)
            {
                GameObject.Destroy(s);
            }

            _selectObject = null;
        }
    }
}
