using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombExpl : MonoBehaviour
{
    // public AudioSource windowsStartup;
    //creating GameObject to assign Explosion effect to bomb
    public GameObject explosionEffect;
    
    //layerMask for walls and stuff that cannot be destroyed
    public LayerMask levelMask;

    //layerMask for crates and stuff that can be destroyed
    public LayerMask destMask;


    //create time delay before explosion
    public float delay = 3f;
    float countdown;

    //checking if it has exploded
    bool hasExploded = false;
    

    // Start is called before the first frame update
    void Start()
    {
        ////calling on explode function, but delaying the function by 3f seconds
        //Invoke("Explode", delay);

        //setting countdown time
        countdown = delay;
    }

    // Update is called once per frame
    void Update()
    {
        //droppping the time 
        countdown -= Time.deltaTime;

        //explode 
        if (countdown <= 0f && !hasExploded) {
            Explode();
            // windowsStartup.Play();
            hasExploded = true;
        }
        
    }

    
    //explode function
    void Explode() {
        
        
        //Adds the explosion effect at bomb location
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        //Instantiate(explosionEffect, transform.position, transform.rotation);

        //radius of explosion 
        int len = 2;


        //calls on ExpandExplode IEnumerator once for every direction
        StartCoroutine(ExpandExplode(Vector3.forward, len, transform.position));
        StartCoroutine(ExpandExplode(Vector3.right, len, transform.position));
        StartCoroutine(ExpandExplode(Vector3.back, len, transform.position));
        StartCoroutine(ExpandExplode(Vector3.left, len, transform.position));  



        //Debug.Log("BOOM");

        //disable collider so that player can walk into an explosion
        //transform.Find("Sphere Collider").gameObject.SetActive(false);

        //destroy bomb in 0.3f seconds
        Destroy(gameObject);

    }

    //creating explosions in different directions
    private IEnumerator ExpandExplode(Vector3 direction, int length, Vector3 pos) {
        // for (int i = 0; i < length; i++) {
            
        //     RaycastHit hit; 

        //     // pos += direction;
        //     // Instantiate(explosionEffect, pos, Quaternion.identity);


        //     // if (!Physics.Raycast(pos, direction, Mathf.Infinity, levelMask)) {
        //     //     pos += direction;
        //     //     Instantiate(explosionEffect, pos, Quaternion.identity);
        //     // } else {
        //     //     break;
        //     // }
            
        // }
        // yield return new WaitForSeconds(.05f); 


        for (int i = 1; i < length; i++) 
        { 
            //walls hit
            RaycastHit hit; 
            
            //destructables hit
            RaycastHit hit2;

            //use raycast to check for what is hit from origin of bomb
            Physics.Raycast(pos, direction, out hit, i, levelMask); 
            Physics.Raycast(pos, direction, out hit2, i, destMask); 

            //if hits nothing, then display explosion
            if (!hit.collider && !hit2.collider) { 
                Instantiate(explosionEffect, pos + (i * direction), Quaternion.identity); 
            } else if (!hit.collider) {

                //only hit crate, explosion one, destory object, break 
                Instantiate(explosionEffect, pos + (i * direction), Quaternion.identity); 
                Debug.Log("crate!");
                Destroy(hit2.transform.gameObject);
                break;
            }
            else 
            { 
                //hit wall, break explosion
                break; 
            }
        }

        yield return new WaitForSeconds(.05f); 
    }
}
