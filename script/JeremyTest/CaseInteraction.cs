using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaseInteraction : MonoBehaviour {

    public List<CaseInteraction> listNeighbour;
    public bool callred = false;


    public List<CaseInteraction> getListNeighbour()
    {
        return listNeighbour;
    }


    private void OnMouseOver()
    {
        //fait que la case que l'on cible devien jaune
            foreach (Renderer r in GetComponentsInChildren<Renderer>())
                r.enabled = true;
    }

    private void OnMouseExit()
    {
        //la case qu'on l'on cible redevient normal quand on la quitte
        foreach (Renderer r in GetComponentsInChildren<Renderer>())
        {
            r.enabled = false;
        }
            GetComponent<Renderer>().enabled = true;
        /*foreach (Renderer r in GetComponentsInChildren<Renderer>())
            r.material.color = Color.green;*/
    }

    private void OnMouseUp()
    {
        //quand on a fini de cliquer les cases adjacente devienne normal
        movementCancellation();

    }

    private void OnMouseDown()
    {
        //quand on clique le debug nous dis la position de la tile
        int x = (int) this.gameObject.transform.position.x;
        int z = (int) this.gameObject.transform.position.z;
        Debug.Log("Click");
        Debug.Log("x = " + this.gameObject.transform.position.x + "y = " + this.gameObject.transform.position.z);
        //on voit l'unité cliqué
        BoardManager.getInstance().UnitClicked(x, z);
        //code pour voir les case adjacente en rouge
        movementPrevison(4);
    }

    public void movementPrevison(int pm)
    {
        //on initialise les variable nécssaire a la fonction
        Board.caseVisited.Add(this);
        int counter = 0;
        foreach (CaseInteraction c in listNeighbour)
        {
            Board.caseToVisite.Add(c);
        }
        List<CaseInteraction> listTemp = new List<CaseInteraction>();
        while (counter < pm) //tant qu'on pourrais encore se déplacé
        {
            foreach (CaseInteraction c in Board.caseToVisite)
            {
                if (!Board.caseVisited.Contains(c) & BoardManager.getInstance().UnitClicked((int)c.transform.position.x, (int)c.transform.position.z) == null)
                    //on verifie que la case a visite est visitable et n'est pas déjà visite
                {
                    Board.caseVisited.Add(c);
                    foreach (Renderer r in c.GetComponentsInChildren<Renderer>())
                    {
                        r.material.color = Color.blue;
                        r.enabled = true;
                    }
                    foreach (CaseInteraction ctv in c.listNeighbour)
                        // on prépare les prochaine case a vistié
                    {
                        if (!listTemp.Contains(ctv) & !Board.caseVisited.Contains(ctv))
                        {
                            listTemp.Add(ctv);
                        }
                    }
                }
            }
            //puis on remplie la list temporaire de la liste a visité après l'avoir vidé
            Board.caseToVisite.Clear();
            foreach (CaseInteraction cta in listTemp)
            {
                Board.caseToVisite.Add(cta);
            }
            listTemp.Clear();
            counter++;
        }
        Board.caseToVisite.Clear();

    }

    public void movementCancellation()
    {
        foreach (CaseInteraction c in Board.caseVisited)
        {
            foreach (Renderer r in c.GetComponentsInChildren<Renderer>())
            {
                r.enabled = false;
                Color color1 = new Color32(189, 177, 79, 255);
                r.material.color = color1;
            }
            c.GetComponent<Renderer>().enabled = true;
        }
        Board.caseVisited.Clear();
    }

}
