using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(PlayerController))]
public class PlayerSetup : NetworkBehaviour
{
    [SerializeField]
    Behaviour[] componentsToBeDisabled;

    [SerializeField]
    string remoteLayerName = "RemotePlayer";

    [SerializeField]
    string dontDrawLayerName = "DontDraw";
    [SerializeField]
    GameObject playerGraphics;

    [SerializeField]
    GameObject playerUIPrefab;
    [HideInInspector]
    public GameObject playerUIInstance;

    // Start is called before the first frame update
    void Start()
    {
        if (!isLocalPlayer)
        {
            DisableComponents();
            AssignRemoteLayer();
        }
        else {

            SetLayerRecursively(playerGraphics,LayerMask.NameToLayer(dontDrawLayerName));

            playerUIInstance=Instantiate(playerUIPrefab);
            playerUIInstance.name = playerUIPrefab.name;

            PlayerUI ui = playerUIInstance.GetComponent<PlayerUI>();
            if (ui == null)
            {
                Debug.LogError("No playerUI component on PlayerUI prefab");
            }
            else
            {
                ui.SetPlayer(GetComponent<Player>());
            }

            GetComponent<Player>().SetupPlayer();

            string _username = "Loading...";
            if (UserAccountManager.IsLoggedIn) {
                _username = UserAccountManager.LoggedIn_Username;
            }
            else {
                _username = transform.name;
                Debug.LogError("Error: Didn't Find Player Account!");
            }
            CmdSetUsername(transform.name, _username);
        }
    }

    [Command]
    void CmdSetUsername(string playerID, string username) {
        Player player =  GameManager.GetPlayer(playerID);
        if (player == null) {
            Debug.LogError("Error: No Player!");
        }
        else
        {
            Debug.Log(username + " has joined!");
            player.username = username;
        }
    }

    void SetLayerRecursively(GameObject obj, int newLayer) {
        obj.layer = newLayer;
        foreach (Transform child in obj.transform) {
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        string _netID = GetComponent<NetworkIdentity>().netId.ToString();
        Player _player = GetComponent<Player>();

        GameManager.RegisterPlayer(_netID,_player);
    }

    void AssignRemoteLayer() {
        gameObject.layer = LayerMask.NameToLayer(remoteLayerName);
    }

    void DisableComponents() {
        for (int i = 0; i < componentsToBeDisabled.Length; i++)
        {
            componentsToBeDisabled[i].enabled = false;
        }
    }



    private void OnDisable()
    {
        Destroy(playerUIInstance);
        if (isLocalPlayer)
        {
            GameManager.instance.SetSceneCameraActive(true);
        }
        GameManager.UnRegisterPlayer(transform.name);
    }
}
