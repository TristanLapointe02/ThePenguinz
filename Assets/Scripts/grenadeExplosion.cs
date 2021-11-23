using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grenadeExplosion : MonoBehaviour
{
    public GameObject particuleExplosion; //R�f�rence � la particule

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
        //Activer la particule
        particuleExplosion.gameObject.SetActive(true);

        //D�sactiver le loop
        //particuleExplosion.GetComponent<ParticleSystem>().main.loop = false;

        //D�sactiver le mesh de la grenade
        gameObject.GetComponent<Renderer>().enabled = false;

        //D�truire la grenade
        Destroy(particuleExplosion, 2f);
    }
}
