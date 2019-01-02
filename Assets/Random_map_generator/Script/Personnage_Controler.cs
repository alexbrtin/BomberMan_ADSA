using UnityEngine;
using System.Collections;

public class Personnage_Controler : MonoBehaviour
{
    public Vector2 deplacement = new Vector2();

    public GameObject Prefabs_bombes;
    public GameObject Prefabs_Explosion;
    public int nombre_de_bombe = 3;
    private Transform myTransform;    
    GameObject map;
    GameObject perso;
    int nb_bombe = 0;
    // Use this for initialization
    void Start ()
    {
        map = GameObject.FindGameObjectWithTag("Map");
        perso = GameObject.FindGameObjectWithTag("Player");

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
        if(nb_bombe < nombre_de_bombe && Input.GetKeyDown(KeyCode.Space))
        { //Drop bomb
            DropBomb();
        }


    }

    public void DropBomb()
    {
        
        if(Prefabs_bombes)
        { //Check if bomb prefab is assigned first
            var Bombe = Instantiate(Prefabs_bombes); // On instancie un GameObject
            Bombe.name = "Bombe_" + nb_bombe; // Il prendra le nom "tuile" + l'indice dans le tableau 
            Bombe.transform.SetParent(map.transform); // On place le game object en tant qu'enfant de la grille dans la hierarchie 
            int position_x = Mathf.FloorToInt(perso.transform.position.x);
            int position_y = Mathf.FloorToInt(perso.transform.position.y);          
            Bombe.transform.position = new Vector3(Mathf.FloorToInt(perso.transform.position.x), Mathf.FloorToInt(perso.transform.position.y), 0); // On change la position actuelle 
            

            nb_bombe++;
            Explode(Bombe);

            //Instantiate(Prefabs_bombes, new Vector3(Mathf.FloorToInt(myTransform.position.x),
            //    Mathf.FloorToInt(myTransform.position.y),
            //    Prefabs_bombes.transform.position.z),
            //    Prefabs_bombes.transform.rotation);


        }
    }
    void Explode(GameObject Bombe)
    {       
        int x = Mathf.FloorToInt(Bombe.transform.position.x);
        int y = Mathf.FloorToInt(Bombe.transform.position.y);
        GameObject[] box = GameObject.FindGameObjectsWithTag("Destroy");
        bool haut = false, bas = false, droite = false, gauche = false;
        for (int i = 0; i < box.Length; i++)
        {
            int xbox = Mathf.FloorToInt(box[i].transform.position.x);
            int ybox = Mathf.FloorToInt(box[i].transform.position.y);

            for (int j = 0; j < 3; j++)
            {
                
                int pos_droite = x + (16*(j+1));
                int pos_haut = y + (16 * (j + 1));
                int pos_bas = y - (16 * (j + 1));
                int pos_gauche = x - (16 * (j + 1));
                
                if (droite == false)
                {
                    if (pos_droite == xbox)
                    {                        
                        Destroy(box[i], .3f);
                        Destroy(Bombe);
                        nb_bombe--;
                        Debug.Log(xbox + " r " + ybox);
                        Instantiate(Prefabs_Explosion, transform.position, Quaternion.identity); //1

                        droite = true;
                    }
                }
                if (gauche == false)
                {
                    if (pos_gauche == xbox)
                    {                        
                        Destroy(box[i], .3f);
                        Destroy(Bombe);
                        Debug.Log(xbox + " r " + ybox);
                        Instantiate(Prefabs_Explosion, transform.position, Quaternion.identity); //1
                        nb_bombe--;
                        gauche = true;
                    }
                }
                if (bas == false)
                {
                    if (ybox == pos_bas)
                    {                        
                        Destroy(box[i], .3f);
                        Destroy(Bombe);
                        Debug.Log(xbox + " r " + ybox);
                        Instantiate(Prefabs_Explosion, transform.position, Quaternion.identity); //1
                        nb_bombe--;
                        bas = true;
                    }
                }
                if(haut == false)
                { 
                    if(ybox == pos_haut)
                    {                        
                        Destroy(box[i], .3f);
                        Destroy(Bombe);
                        Debug.Log(xbox + " r " + ybox);
                        Instantiate(Prefabs_Explosion, transform.position, Quaternion.identity); //1
                        nb_bombe--;
                        haut = true;
                    }
                }


            }
        }




    }
}
