using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
 * Gestion de la vie du totem
 * 
 * Par : Tristan Lapointe
 * 
 * Dernière modification : 08 décembre 2021
 * 
*/
public class TotemVie : MonoBehaviour
{
    public float vieTotem = 1f; //Vie du totem
    public Slider sliderTotem; //Barre de vie du totem
    public float degatEnnemi; //Dégât qu'un ennemi normal va faire
    public float degatBoss; //Dégât qu'un boss va faire
    public AudioClip sonDefaite; //Son de défaite
    public bool delai; //Petit délai pour régler le bug de la caméra
    public bool peutJouerSon; //Condition pour pas que le son se joue en boucle
    public static bool defaite; //Détermine quand le jeu est terminé

    //Petit délai pour gérer le bug de l'orientation de la barre de vie du totem avec la main caméra
    void Start()
    {
        Invoke("delaiFonction", 2f);
    }

    void Update()
    {
        if (delai == true)
        {
            //Ajuster la rotation de la barre de vie avec la rotation de la caméra du joueur
            sliderTotem.gameObject.transform.rotation = Camera.main.transform.rotation;
        }      

        //Si le totem n'a plus de vie...
        if (vieTotem <= 0f && peutJouerSon == false && defaite == false)
        {
            //Jouer le son de défaite
            GetComponent<AudioSource>().PlayOneShot(sonDefaite);

            //Condition pour pas que le son se joue en boucle
            peutJouerSon = true;

            //Détruire le totem
            Destroy(gameObject, 2f);

            //Indiquer que c'est la défaite
            defaite = true;
        }
    }
    
    IEnumerator OnTriggerEnter(Collider infoCollision){
        //Si c'est un ennemi qui a touché le totem
        if (infoCollision.gameObject.tag == "Ennemi"){
            //Attendre 0.5 secondes pour être synchro avec l'animation d'attaque de l'ennemi
            yield return new WaitForSeconds(0.5f);

            //Collision de l'ennemi avec le totem
            if (infoCollision.gameObject.name == "Ennemi(Clone)"){
                //Enlever un peu de vie
                vieTotem -= degatEnnemi;
                //Rafraîchir la barre de vie
                sliderTotem.value = vieTotem;  
            }
            //Collision du boss avec le totem
            else if (infoCollision.gameObject.name == "Boss(Clone)"){
                //Enlever un peu de vie
                vieTotem -= degatBoss;
                //Rafraîchir la barre de vie
                sliderTotem.value = vieTotem;
            } 
            //Changer la main color du totem pour indiquer qu'il a pris des dégâts
            GetComponent<Renderer>().material.SetColor("_Color", Color.red);

            //Attendre 0.15 secondes
            yield return new WaitForSeconds(0.15f);

            //Remettre la couleur de base
            GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        }
    }

    //Fonction qui gère le délai de correction
    public void delaiFonction()
    {
        delai = true;
    }
}
