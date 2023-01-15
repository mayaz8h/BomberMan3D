using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BC2 : MonoBehaviour
{
    //bomb prefab 
    public GameObject bombPrefab;

    //define key that we want to use to drop bomb
    public KeyCode inputKey = KeyCode.Space;

    //number of bombs dropped each time
    private int bombAmount = 3;

    //number of bombs remaining
    private int bRemaining;


    //bombFuseTime
    public int bombFuseTime = 3;


    // OnEnable is called everytime script is activated
    //define # bombs before the player drops any
    void OnEnable()
    {
        bombAmount = 3;
        bRemaining = bombAmount;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (bRemaining > 0 && Input.GetKeyDown(inputKey)) {
            StartCoroutine(placeBomb());    
        }
            
    }

    //placebomb
    private IEnumerator placeBomb() {
        Vector3 position = transform.position;
        //snap
        position.x = Mathf.Round(position.x);
        position.z = Mathf.Round(position.z);


        // make bomb appear       
        GameObject bomb = Instantiate(bombPrefab, position, Quaternion.identity);

        bRemaining -= 1;

        //Debug.Log(bombsRemaining.ToString());
        //suspend the function while the bomb gets ready to explode
        yield return new WaitForSeconds(bombFuseTime);

        bRemaining ++;

    }

    //turn off trigger when player walks away from bomb
    private void OnTriggerExit(Collider other) {

        //check if the layer of the gameObject Player just exited is Bomb
        if (other.gameObject.layer == LayerMask.NameToLayer("Bomb")) {
            
            //other aka bomb is no longer a trigger
            other.isTrigger = false;

        }
    }
}
