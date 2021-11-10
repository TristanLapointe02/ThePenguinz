//**********************************************************************
//Gestion de l'avatar réseau (désactiver et activer certains componnent comme le body ou le XR rig)
//@The Penguinz
//2021-11-10
//**********************************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class GestionAvatarReseau : MonoBehaviourPunCallbacks
{
    public GameObject XRRig;
    public GameObject Avatar;
    void Start()
    {
        if (photonView.IsMine)
        {
            XRRig.SetActive(true);      //XRRig controle l'avatar local
            Avatar.SetActive(false);    //le model Avatar n'est pas visible pour le joueur local 
            //correction de bug de téléportation, XRRig n'est pas dans la scène de début
            //alors il faut l'identifier aux objets "Teleportation Area" par script
         //TeleportationArea[] teleportPlacher = GameObject.FindObjectsOfType<TeleportationArea>();
            //foreach (var plancher in teleportPlacher)
            //{
                //plancher.teleportationProvider = XRRig.GetComponent<TeleportationProvider>();
            //}
        }
        else
        {
            XRRig.SetActive(false);
        }
    }
}

