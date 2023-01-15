using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class b2 : MonoBehaviour
{
    public GameObject bombPrefab;

    public KeyCode inputKey = KeyCode.Space;

    public int bombsRemaining;
    public int bombAmount =3;

    public int bombFuseTime = 3;
    // Start is called before the first frame update
    void OnEnable()
    {

        bombAmount =3;
        bombsRemaining = bombAmount;

    }


    // Update is called once per frame
    void Update()
    {
        if (bombsRemaining >0 && Input.GetKeyDown(inputKey)) {
            StartCoroutine(placeBomb());
        }
        
    
    }
    private IEnumerator placeBomb() {
        Vector3 position = transform.position;

        position.x = Mathf.Round(position.x);
        position.z = Mathf.Round(position.z);
        GameObject bomb = Instantiate(bombPrefab, position, Quaternion.identity);
        yield return new WaitForSeconds(bombFuseTime);
        bombsRemaining -=1;

    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Bomb")){
            other.isTrigger = false;

        }
    }
}
