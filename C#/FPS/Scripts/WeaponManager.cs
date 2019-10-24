﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WeaponManager : NetworkBehaviour
{
    [SerializeField]
    private string weaponLayerName = "Weapon";

    [SerializeField]
    private Transform weaponHolder;

    [SerializeField]
    private PlayerWeapon primaryWeapon;

    private PlayerWeapon currentWeapon;
    private WeaponGraphics currentGraphics;

    [HideInInspector]
    public bool isReloading = false;

    // Start is called before the first frame update
    void Start()
    {
        EquipWeapon(primaryWeapon);  
    }

    void EquipWeapon(PlayerWeapon _weapon) {
        currentWeapon = _weapon;

        GameObject _weaponIns = Instantiate(_weapon.graphics,weaponHolder.position,weaponHolder.rotation);

        _weaponIns.transform.SetParent(weaponHolder);

        currentGraphics = _weaponIns.GetComponent<WeaponGraphics>();
        if (currentGraphics == null) {
            Debug.LogError("No WeaponGraphics component on the weapon object: "+_weaponIns.name);
        }
        if (isLocalPlayer) {
            Util.SetLayerRecursively(_weaponIns, LayerMask.NameToLayer(weaponLayerName));
        }
    }

    public PlayerWeapon GetCurrentWeapon() {
        return currentWeapon;
    }

    public WeaponGraphics GetCurrentGraphics()
    {
        return currentGraphics;
    }
    public void Reload() {
        if (isReloading)
            return;

        StartCoroutine(Reload_Coroutine());
    }

    private IEnumerator Reload_Coroutine() {
        isReloading = true;

        CmdOnReload();

        yield return new WaitForSeconds(currentWeapon.reloadTime);

        currentWeapon.bullets = currentWeapon.maxBullets;


        isReloading = false;
    }

    [Command]
    void CmdOnReload() {
        RpcOnReload();
    }

    [ClientRpc]
    void RpcOnReload() {
      Animator anim = currentGraphics.GetComponent<Animator>();
        if (anim != null) {
            anim.SetTrigger("Reload");
        }
    }
}
