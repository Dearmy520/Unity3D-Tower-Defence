using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class TurretData
{
    public GameObject TurretPrefab;
    public int Cost;
    public GameObject TurretUpgradePrefebs;
    public int CostUpgrade;
    public TurretType type;


}
public enum TurretType
{
    LaserTurret, MissileTurret, StandardTurret
}