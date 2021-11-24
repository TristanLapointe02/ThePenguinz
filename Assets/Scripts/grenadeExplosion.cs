using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class grenadeExplosion : MonoBehaviourPunCallbacks
{
    public GameObject particuleExplosion; //Référence à la particule
    public GameObject corpsGrenade; //Référence à la grenade en soi
    public GameObject espaceCollision; //Référence à la hitbox d'explosion
    public AudioClip sonExplosion; //Son d'explosion de la grenade
    public float force; //Force de l'explosion
    public float rayon; //Rayon de l'explosion

    public void OnCollisionEnter(Collision collision)
    {
        //Si la grenade touche le layer du plancher...
        if (collision.gameObject.layer == 3)
        {
            //Appeler l'explosion
            Invoke("explosion", 2f);   
        }
    }

    public void explosion()
    {
        //Cacher le corps de la grenade
        corpsGrenade.gameObject.SetActive(false);

        //Activer la hitbox d'explosion
        espaceCollision.gameObject.SetActive(true);

        //Jouer le son en RPC pour tous
        GetComponent<AudioSource>().PlayOneShot(sonExplosion);

        //Activer la particule
        particuleExplosion.gameObject.SetActive(true);

        //Détruire la grenade après 1 seconde
        Destroy(gameObject, 1.5f);
        /*int pvID = GetComponent<PhotonView>().ViewID;
        photonView.RPC("DetruireObjet", RpcTarget.MasterClient, pvID, 2);*/

        //Explosion
        //Aller chercher tous les colliders proche
        Collider[] colliders = Physics.OverlapSphere(transform.position, rayon);
        
        //Pour chaque collider, trouver les rigidbody et appliquer une force d'explosion provenant du centre de la grenade
        foreach(Collider nearby in colliders)
        {
            Rigidbody rigg = nearby.GetComponent<Rigidbody>();
            if(rigg != null && rigg.gameObject.tag != "Ennemi")
            {
                rigg.AddExplosionForce(force, transform.position, rayon);
            }
        }
    }
    /*[PunRPC]
    IEnumerator DetruireObjetDelais(int pvID, int delais)
    {
        yield return new WaitForSeconds(delais);
        PhotonNetwork.Destroy(PhotonView.Find(pvID));
    }*/
}
