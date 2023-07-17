using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapCube : MonoBehaviour
{

    public GameObject BulidEffect;
    private Renderer render;


    [HideInInspector]
    public GameObject TurretGO;//当前放置在CUBE的炮台
    public bool isUpgraded;
    public TurretData TurretData;
    private void Start()
    {
        render = GetComponent<Renderer>();
    }
    public void TurretBuild(TurretData SelectedTurret)
    {
        TurretData = SelectedTurret;
        isUpgraded = false;
        TurretGO =  GameObject.Instantiate(SelectedTurret.TurretPrefab, transform.position, Quaternion.identity);
        GameObject Effect = GameObject.Instantiate(BulidEffect, transform.position, Quaternion.identity);
        Destroy(Effect, 1);

    }

    public void UpgradeTurret()
    {
        if (isUpgraded) return;
        Destroy(TurretGO);
        isUpgraded = true;
        TurretGO =  GameObject.Instantiate(TurretData.TurretUpgradePrefebs, transform.position, Quaternion.identity);
        GameObject Effect = GameObject.Instantiate(BulidEffect, transform.position, Quaternion.identity);
        Destroy(Effect, 1);
    }

    public void DestoryTurret()
    {
        Destroy(TurretGO);
        isUpgraded = false;
        TurretGO = null;
        TurretData = null;
        GameObject Effect = GameObject.Instantiate(BulidEffect, transform.position, Quaternion.identity);
        Destroy(Effect, 1);
    }
    private void OnMouseEnter()
    {
        Debug.Log("OnMouseEnter");
        if (TurretGO == null && EventSystem.current.IsPointerOverGameObject() == false)
        {
            render.material.color = Color.red;
        }
    }

    private void OnMouseExit()
    {
        render.material.color = Color.white;
    }
}


