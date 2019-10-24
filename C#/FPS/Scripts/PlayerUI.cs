using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    RectTransform thrusterFuelFill;

    [SerializeField]
    RectTransform HealthBarFill;

    [SerializeField]
    Text ammoText;

    [SerializeField]
    GameObject pauseMenu;

    [SerializeField]
    GameObject scoreboard;

    private Player player;
    private PlayerController controller;
    private WeaponManager weaponManager;

    public void SetPlayer(Player _player) {
        player = _player;
        controller = player.GetComponent<PlayerController>();
        weaponManager = player.GetComponent<WeaponManager>();
    }

    void SetFeulAmount(float _amount) {
        thrusterFuelFill.localScale = new Vector3(1f, _amount, 1f);
    }

    private void Start()
    {
        PauseMenu.IsOn = false;
    }

    private void Update()
    {
        SetFeulAmount(controller.GetThusterFuelAmount());
        SetHealthAmount(player.GetHealthPct());
        SetAmmoAmount(weaponManager.GetCurrentWeapon().bullets, weaponManager.GetCurrentWeapon().maxBullets);

        if (Input.GetKeyDown(KeyCode.Escape)) {
            TogglePauseMenu();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            scoreboard.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.Tab)) {
            scoreboard.SetActive(false);
        }
    }

    public void TogglePauseMenu() {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        PauseMenu.IsOn = pauseMenu.activeSelf;
    }

    void SetHealthAmount(float _amount) {
        HealthBarFill.localScale = new Vector3(1f, _amount, 1f);
    }

    void SetAmmoAmount(int _amount,int max) {
        ammoText.text = _amount.ToString()+"/"+max;
    }
}
