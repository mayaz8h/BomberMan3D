using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public GameObject bombPrefab;
    public KeyCode inputKey = KeyCode.Space;
    //Time taken for bomb to explode
    public float bombFuseTime = 3f;
    public AudioSource animewow;

    //No. of bombs player can drop at once: default 1
    public int bombAmount = 5;

    //How many bombs do the player have remaining
    private int bombsRemaining;

    public bool glove = false;


    private void OnEnable() {
        bombAmount = 3;
        // bombsRemaining = bombAmount;
        //Debug.Log(bombAmount.ToString());

    }

    private void Update() {
        if (Input.GetKeyDown(inputKey)) {
            StartCoroutine(PlaceBomb());
            //PlaceBomb()
        }
    }

    //Placing bomb
    private IEnumerator PlaceBomb() {
        
        Vector3 position = transform.position;
        
        //snapping to grid
        position.x = Mathf.Round(position.x);
        position.y = 1.1f;
        
        //position.y = Mathf.Round(position.y); 
        position.z = Mathf.Round(position.z);

        GameObject bomb = Instantiate(bombPrefab, position, Quaternion.identity);
        animewow.Play();

        // bombsRemaining -= 1;

        //Debug.Log(bombsRemaining.ToString());
        //suspend the function while the bomb gets ready to explode
        yield return new WaitForSeconds(bombFuseTime);

        // bombsRemaining++;

    }

    //turn off trigger when player walks away from bomb
    private void OnTriggerExit(Collider other) {

        //check if the layer of the gameObject Player just exited is Bomb
        if (other.gameObject.layer == LayerMask.NameToLayer("Bomb")) {
            
            //other aka bomb is no longer a trigger
            other.isTrigger = false;

        }
    }

    // private void gloveDisable() {
    //     if (!glove) {
    //         var goArray = FindObjectsOfType(GameObject);
    //         var goList = new System.Collections.Generic.List.<GameObject>();
    //         for (var i = 0; i < goArray.Length; i++) {
    //             if (goArray[i].layer == layer) {
    //                 goList.Add(goArray[i]);
    //             }
    //         }
    //     }
    // }



}
