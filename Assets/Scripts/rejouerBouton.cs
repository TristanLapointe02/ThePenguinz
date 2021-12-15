using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
/*
 * Gestion de la scène de défaite et du faux bouton permettant de recommencer le jeu
 * 
 * Par : Tristan Lapointe
 * 
 * Dernière modification : 14 décembre 2021
 * 
*/

public class rejouerBouton : MonoBehaviourPunCallbacks
{
    // Update is called once per frame
    void Update()
    {
        //Si le joueur est le Master client...
        if (PhotonNetwork.LocalPlayer.IsMasterClient == true)
        {
            //Activer le bouton
            gameObject.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider infoCollision)
    {
        if (infoCollision.gameObject.tag == "Player")
        {
            //Relancer la scène du début
            PhotonNetwork.LoadLevel("Penguinz_Ingame");
        }
    }
}
