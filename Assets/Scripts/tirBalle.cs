using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class tirBalle : MonoBehaviourPunCallbacks
{
    public bool peutTirer = true; //Vérifie si le joueur peut tirer
    public GameObject balle; //La balle qui est instanciée au tir
    public GameObject boutFusil; //Détermine où est le bout du fusil (où la balle devrait spawn)

    private void Start()
    {
        boutFusil = GameObject.Find("BoutFusil");
    }
    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            //Instancier une balle si le joueur appuie sur clique gauche
            if (Input.GetKeyDown(KeyCode.Mouse0) && peutTirer == true)
            {
                //Instancier la balle
                GameObject nouvelleBalle = PhotonNetwork.Instantiate(balle.name, boutFusil.transform.position, transform.rotation, 0, null);

                //Lui appliquer une vélocité pour la projeter vers l'avant
                nouvelleBalle.GetComponent<Rigidbody>().velocity = transform.forward * 10;
            }
        }
    }
}
