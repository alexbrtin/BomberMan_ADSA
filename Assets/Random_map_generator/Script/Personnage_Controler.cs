using UnityEngine;
using System.Collections;

public class Personnage_Controler : MonoBehaviour {
    public Vector2 deplacement = new Vector2();

    public GameObject Prefabs_bombes;

    public bool canDropBombs = true;

    private Transform myTransform;
    // Use this for initialization
    void Start () {
        myTransform = transform;

    }
	
	// Update is called once per frame
	void Update ()
    {
        deplacement.x = deplacement.y = 0; // pour réinitiliaser les donnée quand on conecte les mouvement 

        if(Input.GetKey("right"))
        {
            deplacement.x = 1;
        }
        if(Input.GetKey("left"))
        {
            deplacement.x = -1;
        }

        if(Input.GetKey("up"))
        {
            deplacement.y = 1;
        }
        if(Input.GetKey("down"))
        {
            deplacement.y = -1;
        }
        if(canDropBombs && Input.GetKeyDown(KeyCode.Space))
        { //Drop bomb
            DropBomb();
        }


    }

    private void DropBomb()
    {
        if(Prefabs_bombes)
        { //Check if bomb prefab is assigned first
            Instantiate(Prefabs_bombes, new Vector3(Mathf.RoundToInt(myTransform.position.x),
                Mathf.RoundToInt(myTransform.position.y),
                Prefabs_bombes.transform.position.z),
                Prefabs_bombes.transform.rotation);
        }
    }
}
