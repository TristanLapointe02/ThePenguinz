using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

/*
 * 
 * Spawn les joueurs dans la scène de fin
 * 
 * Par : Jérémy Emond-Lapierre
 * 
 * Dernière modification : 15 décembre 2021
 * 
*/

public class SceneFin : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.Instantiate("AvatarReseau",new Vector3(Random.Range(-5f, 5f),3f,Random.Range(-5f, 5f)),Quaternion.identity, 0,null );
    }
}
