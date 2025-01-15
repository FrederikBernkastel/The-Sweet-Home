#if ENABLE_ERRORS

using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSystem
{
    [SerializeField]
    [Required] private Transform storageHouses;
    [SerializeField]
    [Required] private Camera mainCamera;
    [SerializeField] private LayerMask layerMaskGround;
    [SerializeField] private LayerMask layerMaskHouse;
    [SerializeField] private LayerMask layerMaskUnit;
    [SerializeField] private LayerMask layerMaskForest;

    private GameObject currentHouse;
    private BoxCollider boxCollider;
    private MeshRenderer meshRenderer;

    public void StartBuilding()
    {
        //currentHouse = GameObject.Instantiate(house.house, storageHouses);

        //currentHouse.layer = LayerMask.NameToLayer("Default");

        //boxCollider = currentHouse.GetComponentInChildren<BoxCollider>();
        //meshRenderer = currentHouse.GetComponentInChildren<MeshRenderer>();


        //OnController();

        //ApplicationGameEntry.application.gameStorage.machineController.SetTrigger("ContextBuilding");
    }

    public void OnUpdate()
    {
        //if (!currentHouse) return;

        //meshRenderer.material = houseData.matHouseSelected;

        //var ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        //if (Physics.Raycast(ray, out var hitInfo, float.MaxValue, layerMaskGround))
        //{
        //    currentHouse.transform.position = hitInfo.point;
        //}

        //if (Physics.CheckBox(
        //    boxCollider.bounds.center,
        //    boxCollider.bounds.size * 0.5f,
        //    Quaternion.identity,
        //    layerMaskHouse))
        //{
        //    meshRenderer.material = houseData.matCloseHouse;
        //}
        //else if (Physics.CheckBox(
        //    boxCollider.bounds.center,
        //    boxCollider.bounds.size * 0.5f,
        //    Quaternion.identity,
        //    layerMaskUnit))
        //{
        //    meshRenderer.material = houseData.matCloseHouse;
        //}
        //else if (Physics.CheckBox(
        //    boxCollider.bounds.center,
        //    boxCollider.bounds.size * 0.5f,
        //    Quaternion.identity,
        //    layerMaskForest))
        //{
        //    meshRenderer.material = houseData.matCloseHouse;
        //}
    }
}

#endif