using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorBomb : MonoBehaviour
{
    public Material nMaterial;

    public void setColorToBlack(){
        nMaterial.SetColor("_Color", Color.black);

    }
    public void setColorToRed(){
        nMaterial.SetColor("_Color", Color.red);
        
    }
}
