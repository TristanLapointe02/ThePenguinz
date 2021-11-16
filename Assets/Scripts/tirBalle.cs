using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class tirBalle : MonoBehaviourPunCallbacks
{
    public bool peutTirer = true; //V�rifie si le joueur peut tirer
    public GameObject balle; //La balle qui est instanci�e au tir
    public GameObject boutFusil; //D�termine o� est le bout du fusil (o� la balle devrait spawn)

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
                GameObject nouvelleBalle = PhotonNetwork.Instantiate(balle.name, boutFusil.transform.position, boutFusil.transform.rotation, 0, null);

                //Lui appliquer une v�locit� pour la projeter vers l'avant
                nouvelleBalle.GetComponent<Rigidbody>().velocity = boutFusil.transform.forward * 40;
            }
        }
    }
}
