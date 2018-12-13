using UnityEngine;
using System.Collections;

public class Personnage : MonoBehaviour
{

    [SerializeField]
    public float vitesse = 1f;
    public Vector2 vitesse_max = new Vector2(60, 100);
    private Rigidbody2D corps;
    private SpriteRenderer rendu_2D; // pour pouvoir accéder aux propriété de l'objet 
    private Personnage_Controler pc;
    private Animator anim;
    void Start()
    {
        corps = GetComponent<Rigidbody2D>();
        rendu_2D = GetComponent<SpriteRenderer>();
        pc = GetComponent<Personnage_Controler>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        var absolute_velocityX = Mathf.Abs(corps.velocity.x);
        var absolute_velocityY = Mathf.Abs(corps.velocity.y);

        var forceX = 0f;
        var forceY = 0f;

        if(pc.deplacement.x != 0)
        {
            if(absolute_velocityX < vitesse_max.x)
            {
                var new_vitesse = vitesse * pc.deplacement.x;
                forceX = new_vitesse;
            }
            if(pc.deplacement.x>0)
            {
                anim.SetInteger("AnimState", 2);
            }
            else
            {
                anim.SetInteger("AnimState", 4);
            }
           
        }            
        else if (pc.deplacement.y != 0)
        {
            if (absolute_velocityX < vitesse_max.x)
            {
                var new_vitesse = vitesse * pc.deplacement.y;
                forceY = new_vitesse;
            }
            if (pc.deplacement.y > 0)
            {
                anim.SetInteger("AnimState", 1);
            }
            else
            {
                anim.SetInteger("AnimState", 3);
            }

        }
        else
        {
            anim.SetInteger("AnimState", 0);
        }




        corps.transform.Translate(new Vector2(forceX, forceY));
    }
    
}