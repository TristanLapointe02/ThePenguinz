using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

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

    public void reloadScene()
    {
        //Recharger la scène
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
