using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour {

    public static BuildManager instance;

    public NodeUI nodeUI;

    

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one BuildManager in scene");
            return;
        }
        instance = this;
    }

    public GameObject standardTurretPrefab;

    public GameObject missleLauncherPrefab;

    public GameObject buildEffect;

    public GameObject sellEffect;

    private TurretBlueprint turretToBuild;
    private Node selectedNode;

    public bool CanBuild { get { return turretToBuild != null; } }  //watch "get"&"return" here, this line can't be set!
    public bool HasMoney { get { return PlayerStats.Money >= turretToBuild.cost; } }


    public void SelectNode(Node node) {

        if (selectedNode == node) {
            DeselectNode();
            return;
        }
        selectedNode = node;
        turretToBuild = null;

        nodeUI.SetTarget(node);
    }

    public void DeselectNode() {
        selectedNode = null;
        nodeUI.Hide();
    }

    public void SelectTurretToBuild(TurretBlueprint turret) {

        turretToBuild = turret;
        selectedNode = null;

        DeselectNode();
    }

    public TurretBlueprint GetTurretToBuild() {
        return turretToBuild;
    }
}
