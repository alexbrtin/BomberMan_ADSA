using UnityEngine;
using System.Collections;

public class Personnage_Controler : MonoBehaviour {
    public Vector2 deplacement = new Vector2();
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        deplacement.x = deplacement.y = 0; // pour réinitiliaser les donnée quand on conecte les mouvement 

        if(Input.GetKey("right"))
        {
            deplacement.x = 1;
        }
        else if(Input.GetKey("left"))
        {
            deplacement.x = -1;
        }

        if(Input.GetKey("up"))
        {
            deplacement.y = 1;
        }
        else if(Input.GetKey("down"))
        {
            deplacement.y = -1;
        }



    }
}
