using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Scoreboard : MonoBehaviour
{
    [SerializeField]
    GameObject playerScoreboardItem;

    [SerializeField]
    Transform playerScoreboardList;

    private void OnEnable()
    {
       Player[] players = GameManager.GetAllPlayers();
        foreach (Player player in players) {
          GameObject itemGo = (GameObject)Instantiate(playerScoreboardItem,playerScoreboardList);
            PlayerScoreboardItem item = itemGo.GetComponent<PlayerScoreboardItem>();
            if (item != null) {
                item.Setup(player.username, player.kills, player.deaths);
            }
        }
    }

    private void OnDisable()
    {
        foreach (Transform child in playerScoreboardList) {
            Destroy(child.gameObject);
        }
    }
}
