using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotemVie : MonoBehaviour
{
    public float vieTotem = 1f; 
    public Slider sliderJoueur;
    public float degatEnnemi;
    public float degatBoss;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        sliderJoueur.gameObject.transform.rotation = Camera.main.transform.rotation;


    }
    
    IEnumerator OnTrigger(Collider infoCollision){

        if(infoCollision.gameObject.tag == "Ennemi" && infoCollision.gameObject.GetComponent<deplacementEnnemi>().peutAttaquer == true){
            //Indiquer à l'ennemi qu'il ne peut plus attaquer...
            infoCollision.gameObject.GetComponent<deplacementEnnemi>().peutAttaquer = false;

            yield return new WaitForSeconds(3f);

            infoCollision.gameObject.GetComponent<deplacementEnnemi>().peutAttaquer = true;

            //..Et qu'il peut réattaquer le totem après 3 secondes
            Invoke("reactiverAttaque", 3f);
            //Collision de l'ennemi avec le totem
            if(infoCollision.gameObject.name == "Ennemi(Clone)"){
                vieTotem -= degatEnnemi;
                sliderJoueur.value = vieTotem;
            }
            //Collision du boss avec le totem
            else if (infoCollision.gameObject.name == "Boss(Clone)"){
                vieTotem -= degatBoss;
                sliderJoueur.value = vieTotem;
            }
        }
    }
}
