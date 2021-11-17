using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit; // accès aux objets du XR Interaction Toolkit
using UnityEngine.InputSystem; // pour utiliser le nouveau InputSyteme
using UnityEngine.Events;

public class tirBalle : MonoBehaviourPunCallbacks
{
    public bool peutTirer = true; //V�rifie si le joueur peut tirer
    public GameObject balle; //La balle qui est instanci�e au tir
    public GameObject boutFusil; //D�termine o� est le bout du fusil (o� la balle devrait spawn)

    // L'action du contrôleur qui active/désactive le rayon. Peut être autre chose que le grip. Action à définir dans le tableau InputAction
    [SerializeField]
    InputActionReference inputActionReference_ActiveTrigger; 

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

    private void OnEnable(){
        inputActionReference_ActiveTrigger.action.performed += ActiveTrigger;
    }

    private void OnDisable()
    {
        // s'exécute lorsque le script devient inactif (disable)
        // retire la fonction qui sera appelée lorsque l'action sera effectuée
        inputActionReference_ActiveTrigger.action.performed -= ActiveTrigger;
    }

    private void ActiveTrigger(InputAction.CallbackContext obj)
    {
       
        // Bouton enfoncé, on active le rayon
        // ReadValue<float> permet de récuperé la valeur de type float contenu dans le paramètre obj
        if (obj.ReadValue<float>() == 1f)
        {
            if (photonView.IsMine){
                GameObject nouvelleBalle = PhotonNetwork.Instantiate(balle.name, boutFusil.transform.position, boutFusil.transform.rotation, 0, null);
                //Lui appliquer une v�locit� pour la projeter vers l'avant
                nouvelleBalle.GetComponent<Rigidbody>().velocity = boutFusil.transform.forward * 40;
            }
        }
    }
}

