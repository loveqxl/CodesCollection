﻿
using UnityEngine;

public class Shop : MonoBehaviour {

    BuildManager buildManager;

    public TurretBlueprint standardTurret;
    public TurretBlueprint missleLauncher;
    public TurretBlueprint laserBeamer;

    private void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void SelectStandardTurret() {

        Debug.Log("Standard Turret Selected");
        buildManager.SelectTurretToBuild(standardTurret);

    }

    public void SelectMissleLauncher()
    {

        Debug.Log("Missle Launcher Selected");
        buildManager.SelectTurretToBuild(missleLauncher);

    }

    public void SelectLaserBeamer() {
        Debug.Log("Laser Beamer Selected");
        buildManager.SelectTurretToBuild(laserBeamer);
    }
}
