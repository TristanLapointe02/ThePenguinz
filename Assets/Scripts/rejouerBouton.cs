using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
/*
 * Gestion de la sc�ne de d�faite et du faux bouton permettant de recommencer le jeu
 * 
 * Par : Tristan Lapointe
 * 
 * Derni�re modification : 14 d�cembre 2021
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
            //Relancer la sc�ne du d�but
            Application.Quit();
        }
    }
}
