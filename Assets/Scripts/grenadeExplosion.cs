using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
/*
 * Gestion de l'explosion de la grenade
 * 
 * Par : Tristan Lapointe
 * 
 * Dernière modification : 24 novembre 2021
 * 
*/
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

        //Jouer le son
        GetComponent<AudioSource>().PlayOneShot(sonExplosion);

        //Activer la particule
        particuleExplosion.gameObject.SetActive(true);

        //Détruire la grenade après 1 seconde
        Destroy(gameObject, 1.5f);

        //Explosion
        //Aller chercher tous les colliders proche
        Collider[] colliders = Physics.OverlapSphere(transform.position, rayon);
        
        //Pour chaque collider proche
        foreach(Collider nearby in colliders)
        {
            //Trouver les rigidbody
            Rigidbody rigg = nearby.GetComponent<Rigidbody>();

            //Si c'est tout sauf un ennemi, appliquer une force d'explosion provenant du centre de la grenade
            if (rigg != null && rigg.gameObject.tag != "Ennemi")
            {
                rigg.AddExplosionForce(force, transform.position, rayon);
            }
        }
    }
}
