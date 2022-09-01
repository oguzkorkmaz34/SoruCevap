using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    GameObject Player;

    GameManager GameManager;

    public string Ad;

    private void Awake()
    {
        GameManager = Object.FindObjectOfType<GameManager>();
        Player = GameObject.Find("Player");
    }

    private void OnMouseDown()
    {
        if (!GameManager.soruCevaplansinmi)
        {
            return;
        }

        if (this.transform.position.z > Player.transform.position.z && this.transform.position.z < Player.transform.position.z + 2)
        {
            Vector3 mousePos = this.transform.position;
            Player.GetComponent<PlayerController>().HareketEt(mousePos, 0.5f);
            GameManager.SonucuKontrolEt(Ad);
            GameManager.soruCevaplansinmi = false;
        }

    }

    

}
