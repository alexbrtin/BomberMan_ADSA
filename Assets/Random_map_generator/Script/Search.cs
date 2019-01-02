using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Search
{
    public Map map;
    public List<Tile> acceptable; // tuile où l'on peut passer
    public List<Tile> vue; // tuile sur lesquelles on est passé
    public List<Tile> chemin; // chemin viable 
    public Tile destination_finale; //tuile à atteindre 
    public int iterations; // nb d'itteration 
    public bool fini; // si l'algo a trouvé 

    public Search(Map map)
    {
        this.map = map;
    }
    
    public void Start(Tile start,Tile destination) // pour pouvoir changer dynamiquement le chemin 
    {
       
        acceptable = new List<Tile>();
        vue = new List<Tile>();
        chemin = new List<Tile>();
        vider();
        acceptable.Add(start);// La case de départ sera toujours viable
        destination_finale = destination;
        
        iterations = 0;
        
    }
    public void Step()
    {
        if(chemin.Count>0) // chemin déjà trouvé
        {
            return;
        }
        if(acceptable.Count == 0)
        {
            fini = true;
            return;
        }
        iterations++;

        var t = Node_choisi();
        if(t == destination_finale)
        {
            while(t!=null)
            {
                chemin.Insert(0, t);
                t = t.precedente;
            }
            fini = true;
            return;
        }
        acceptable.Remove(t);
        vue.Add(t);
        for(int i =0;i<t.adjacente.Count;i++)
        {
            Ajout_Adjacent(t, t.adjacente[i]);
        }
    }
    public void Ajout_Adjacent(Tile t,Tile adjacente)
    {
        if(trouver_node(adjacente, vue)||trouver_node(adjacente,acceptable))
        {
            return;
        }
        adjacente.precedente = t;
        acceptable.Add(adjacente);
    }
    
    public bool trouver_node(Tile t,List<Tile>t_list)
    {
        return index_tile(t,t_list)>=0;
    }
    public int index_tile(Tile t, List<Tile> t_list)
    {
        for (var i = 0; i < t_list.Count; i++)
        {
            if (t == t_list[i])
            {
                return i;
            }
        }
        return -1;
    }
    public Tile Node_choisi()
    {
        return acceptable[Random.Range(0, acceptable.Count)];
    }
    public void vider()
    {
        acceptable.Clear();
        chemin.Clear();
        vue.Clear();
    }
}
