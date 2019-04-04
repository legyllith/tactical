using UnityEngine;
using System.Collections.Generic; // permet d'utilisé les dictionnaire
using System.Collections;

public class Board : MonoBehaviour {

    public int size = 9; // fait actuelement du 9x9
    public int sommetsize = 81;
    //public Dictionary<int, Tile> tiles = new Dictionary<int, Tile>();
    public GameObject DirtPrefab;
    public GameObject Water1Prefab;
    public GameObject Water0Prefab;
    public GameObject Water2Prefab;
    public GameObject tile0Prefab;
    public GameObject tile1Prefab;
    public GameObject tile2Prefab;
    public GameObject WaterJeu;
    public GameObject casePrefab;
    //public int [,] Map = Map.map1;
    Map map = new Map();
    public List<GameObject> ListTile; //liste de toute les tiles du plateau

    public static List<CaseInteraction> caseVisited = new List<CaseInteraction>();//list des case visite par la fonction caseInteraction.movementprevision
    public static List<CaseInteraction> caseToVisite = new List<CaseInteraction>();// list des case a visite par la fonction caseInteraction.movementprevision
    

    private void fillList(GameObject TileObject)
    {
        ListTile.Add(TileObject);
    }

    // Use this for initialization
    void Start () {
        ListTile = new List<GameObject>();
        Instantiate(WaterJeu);
        int id = 0;


        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                id++;
                int creaTile = map.map1[j, i];
                if (creaTile == 0)
                {
                    GameObject TileObject = (GameObject)Instantiate(DirtPrefab);
                    TileObject.transform.position = new Vector3(i, 0, j);
                    string name = "tile" + i + j;
                    TileObject.name = name;
                    fillList(TileObject);
                }
                if (creaTile == 1)
                {
                    GameObject TileObject = (GameObject)Instantiate(tile0Prefab);
                    float ztile0 = (float)-0.125;
                    TileObject.transform.position = new Vector3(i, ztile0, j);
                    string name = "tile" + i + j;
                    TileObject.name = name;
                    fillList(TileObject);
                }
                if (creaTile == 2)
                {
                    GameObject TileObject = (GameObject)Instantiate(tile1Prefab);
                    float ztile1 = (float)-0.25;
                    TileObject.transform.position = new Vector3(i, ztile1, j);
                    string name = "tile" + i + j;
                    TileObject.name = name;
                    fillList(TileObject);
                }
                if (creaTile == 3)
                {
                    GameObject TileObject = (GameObject)Instantiate(tile2Prefab);
                    float ztile2 = (float)-0.375;
                    TileObject.transform.position = new Vector3(i, ztile2, j);
                    string name = "tile" + i + j;
                    TileObject.name = name;
                    fillList(TileObject);
                }
            }
        }

        //cette double boucle créer la liste des voisin pour chaque case
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (i-1 >= 0)
                {
                    //ListTile[pos].transform.Find("case") donne l'enfant case
                    ListTile[i+j*size].transform.Find("case").gameObject.GetComponent<CaseInteraction>().getListNeighbour()
                        .Add(ListTile[i-1+j*size].transform.Find("case").gameObject.GetComponent<CaseInteraction>());
                }
                if (j - 1 >= 0)
                {
                    //ListTile[pos].transform.Find("case") donne l'enfant case
                    ListTile[i + j * size].transform.Find("case").gameObject.GetComponent<CaseInteraction>().getListNeighbour()
                        .Add(ListTile[i + (j-1)* size].transform.Find("case").gameObject.GetComponent<CaseInteraction>());
                }
                if (i+1 < size)
                {
                    //ListTile[pos].transform.Find("case") donne l'enfant case
                    ListTile[i + j * size].transform.Find("case").gameObject.GetComponent<CaseInteraction>().getListNeighbour()
                        .Add(ListTile[i+1 + j * size].transform.Find("case").gameObject.GetComponent<CaseInteraction>());
                }
                if (j + 1 < size)
                {
                    //ListTile[pos].transform.Find("case") donne l'enfant case
                    ListTile[i + j * size].transform.Find("case").gameObject.GetComponent<CaseInteraction>().getListNeighbour()
                        .Add(ListTile[i + (j + 1) * size].transform.Find("case").gameObject.GetComponent<CaseInteraction>());
                }
            }
        }


    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
