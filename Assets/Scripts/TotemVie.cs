using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotemVie : MonoBehaviour
{
    public float vieTotem = 1f; //Vie du totem
    public Slider sliderTotem; //Barre de vie du totem
    public float degatEnnemi; //Dégât qu'un ennemi normal va faire
    public float degatBoss; //Dégât qu'un boss va faire
    public AudioClip sonDefaite; //Son de défaite
    public bool delai; //Petit délai pour régler le bug de la caméra
    public bool peutJouerSon; //Condition pour pas que le son se joue en boucle

    // Start is called before the first frame update
    void Start()
    {
        Invoke("delaiFonction", 2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (delai == true)
        {
            sliderTotem.gameObject.transform.rotation = Camera.main.transform.rotation;
        }      

        if (vieTotem <= 0f && peutJouerSon == false)
        {
            //Jouer le son de défaite
            GetComponent<AudioSource>().PlayOneShot(sonDefaite, 1f);

            //Condition pour pas que le son se joue en boucle
            peutJouerSon = true;

            //Détruire le totem
            Destroy(gameObject);
        }
    }
    
        IEnumerator OnTriggerEnter(Collider infoCollision){
        //Attendre 1 seconde pour être synchro avec l'animation d'attaque de l'ennemi
        yield return new WaitForSeconds(1f);

        if (infoCollision.gameObject.tag == "Ennemi"){

            //Collision de l'ennemi avec le totem
            if (infoCollision.gameObject.name == "Ennemi(Clone)"){
                //Enlever un peu de vie
                vieTotem -= degatEnnemi;
                //Rafraîchir la barre de vie
                sliderTotem.value = vieTotem;  
            }
            //Collision du boss avec le totem
            else if (infoCollision.gameObject.name == "Boss(Clone)"){
                vieTotem -= degatBoss;
                sliderTotem.value = vieTotem;
            } 
            //Changer la main color du totem
            GetComponent<Renderer>().material.SetColor("_Color", Color.red);
            yield return new WaitForSeconds(0.1f);
            //Remettre la couleur de base
            GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        }
    }

    public void delaiFonction()
    {
        delai = true;
    }
}
