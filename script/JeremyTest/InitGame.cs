using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InitGame : MonoBehaviour {

    public string testTrigger = "z";
    public string testPhase = "p";
    public Board board;
    public List<Unit> list;

    // Use this for initialization
    void Start () {
	    GameObject boardobject = new GameObject("Board");
        boardobject.transform.parent = transform; //le parent du boardobject  est le transform sur 
        //lequ'elle est appliquer le scripte ici jeu
        board = boardobject.AddComponent<Board>(); // ceci ajoute au board (boardobjet) le script Board
        board.DirtPrefab = (GameObject) Resources.Load("Tile"); //mais le préfab néccéssaire au scripte Boards
        // dans le gameobject board
        board.Water1Prefab = (GameObject)Resources.Load("Water1");
        board.Water0Prefab = (GameObject)Resources.Load("Water0");
        board.Water2Prefab = (GameObject)Resources.Load("Water2");
        board.tile1Prefab = (GameObject)Resources.Load("tile1");
        board.tile0Prefab = (GameObject)Resources.Load("tile0");
        board.tile2Prefab = (GameObject)Resources.Load("tile2");
        board.WaterJeu = (GameObject)Resources.Load("WaterJeu");
        board.casePrefab = (GameObject)Resources.Load("case");


        //Instanciation d'un personnage
        createUnit("PAUL");
        createUnit("GARLAND");

        //setFirstPhase
        BoardManager boardManager = BoardManager.getInstance();

    }
	
	// Update is called once per frame
	void Update ()
    {
        if(list.Count != 0)
        {
            list.Clear();
        }

        if(Input.GetKeyDown(testTrigger))
        {
            if(BoardManager.getInstance().getActivePhase() is PlayPhase)
            {
                foreach (Unit u in BoardManager.getInstance().getListUnits())
                {
                    u.SetHp(u.GetHp() - 2);
                    if (u.GetHp() < 0)
                    {
                        u.die();
                        //u.gameObject.SetActive(false);
                        Destroy(u);
                        list.Add(u);

                    }
                }
                foreach (Unit u2 in list)
                {
                    BoardManager.getInstance().getListUnits().Remove(u2);
                    u2.gameObject.SetActive(false);
                    Destroy(u2.gameObject);
                }
            }
            else
            {
                Debug.Log("You can't do that because you are not in PlayPhase");
            }
        }
        if(Input.GetKeyDown(testPhase))
        {
            BoardManager.getInstance().getActivePhase().doAction();
            BoardManager.getInstance().changeActivePhase();
        }
      
	}


    public void createUnit(string n)
    {

        UnitData test = Resources.Load<UnitData>("Enemies/" + n);
        GameObject unit = test.retrieve(n);
        //On load le sprite et l'animation pour l'unité
        GameObject prefab = (GameObject)Resources.Load("Anim/" + n);
        Animator anim = prefab.GetComponent<Animator>();
        SpriteRenderer sprite = prefab.GetComponent<SpriteRenderer>();
        unit.AddComponent<SpriteRenderer>();
        unit.GetComponent<SpriteRenderer>().sprite = sprite.sprite;
        unit.AddComponent<Animator>();
        unit.GetComponent<Animator>().runtimeAnimatorController = anim.runtimeAnimatorController;
        int x = Random.Range(0, 8);
        int z = Random.Range(0, 8);
        unit.name = n;
        unit.transform.position = new Vector3(x, 1, z);
        unit.transform.Rotate(new Vector3(0, -45, 0));
        BoardManager.getInstance().getListUnits().Add(unit.GetComponent<Unit>());


        /*Debug.Log("Bla");
        GameObject unitTest = (GameObject)Resources.Load("Units/braid-run-sprite_0");

        Unit unit;

        if(unitTest.GetComponent<Unit>() != null)
        {
            unit = unitTest.GetComponent<Unit>();
        }
        else
        {
            unit = unitTest.AddComponent<Unit>();
        }

        Unit unitInstance = Instantiate<Unit>(unit);
        unitInstance.setSprite(unitTest.GetComponent<SpriteRenderer>());
        unitInstance.setAnimator(unitTest.GetComponent<Animator>());
        int x = Random.Range(0, 8);
        int z = Random.Range(0, 8);
        unitInstance.name = n;
        unitInstance.transform.position = new Vector3(x, 1, z);
        BoardManager.getInstance().getListUnits().Add(unitInstance);
        Debug.Log("Test instance");
       */

    }
}
