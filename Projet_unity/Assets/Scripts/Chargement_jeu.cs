using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AiRuleEngine;
using System;
using UnityEngine.UI;

namespace AssemblyCSharp
{
    public class Chargement_jeu : MonoBehaviour
    {
        public float Y;
        public float turnTime;
        public static bool pause = false;
        public static float turnTimeStatic;
        public static bool waiting;
        public static bool buildTurn;
        public static int[] bornes;

        private float X;
        private bool changeTurn;
        private Batiment[] spawners;
	
        void Start()
        {
            Production.Initialize();
            spawners = new Batiment[2];
            X = (Mathf.Round((Y / 9f) * 16));
            bornes = new int[2];
            bornes[0] = (int)X;
            bornes[1] = (int)Y;
            Niveau.grille = new GameObject[(int)X, (int)Y];
            GameObject grille = new GameObject("Grille");
            int alea, rotationAlea;

            Niveau.list_pouvoir = new Pouvoir[4];
            
            // Génération de la carte et des textures des différents terrains
            for (double i = 0.5; i < X + 0.5; i++)
            {
                for (double j = 0.5; j < Y + 0.5; j++)
                {
                    GameObject nouvelleCase;
                    rotationAlea = UnityEngine.Random.Range(1, 4);

                    if (i < (X / 2) - 1.0)
                    {
                        if (i < (X / 2) - 2.0)
                        {
                            alea = UnityEngine.Random.Range(1, 5);
                            nouvelleCase = Instantiate(Resources.Load("Prefab/Ours/Case_ours" + alea)) as GameObject;
                            nouvelleCase.transform.Rotate(0, 0, rotationAlea * 90);
                        }
                        else
                        {
                            alea = UnityEngine.Random.Range(1, 3);
                            nouvelleCase = Instantiate(Resources.Load("Prefab/Trans/Case_trans_ours_" + alea)) as GameObject;
                        }

                    }

                    else if (i > (X / 2) + 1.0)
                    {
                        if (i > (X / 2) + 2.0)
                        {
                            alea = UnityEngine.Random.Range(1, 5);
                            nouvelleCase = Instantiate(Resources.Load("Prefab/Poulpe/Case_poulpe" + alea)) as GameObject;
                            nouvelleCase.transform.Rotate(0, 0, rotationAlea * 90);
                        }
                        else
                        {
                            alea = UnityEngine.Random.Range(1, 3);
                            nouvelleCase = Instantiate(Resources.Load("Prefab/Trans/Case_trans_poulpe_" + alea)) as GameObject;
                        }
                    }
                    else
                    {
                        alea = UnityEngine.Random.Range(1, 5);
                        nouvelleCase = Instantiate(Resources.Load("Prefab/Neutre/Case_neutre" + alea)) as GameObject;
                    }

                    nouvelleCase.AddComponent<Case>();
                    nouvelleCase.transform.localScale = new Vector2(0.4f, 0.4f);
                    nouvelleCase.transform.position = new Vector2((float)i, (float)j);
                    Niveau.grille[(int)(i - 0.5), (int)(j - 0.5)] = nouvelleCase;
                    nouvelleCase.transform.parent = grille.transform;
                    nouvelleCase.GetComponent<Case>().occupe = false;
                    nouvelleCase.AddComponent<BoxCollider>();
					waiting=false;
                }
            }
            InitializeDebugSpawners();
            initializePlayer();
        }

        void Update()
        {
            /***** Gestion du temps *****/
            changeTurn = true;

            if (!waiting)
            {
                if (InferenceEngine.tour == "Ours")
                {
                    foreach (GameObject Go in Niveau.list_element[0])
                    {
                        if (Go != null && Go.GetComponent<Element>().aJoue == false)
                        {
                            changeTurn = false;
                        }
                    }
                    foreach (GameObject Go in Niveau.list_element[1])
                    {
                        if (Go != null)
                        {
                            Go.GetComponent<Element>().flagIsDetected = false;
                        }
                    }
                }
                else
                {
                    foreach (GameObject Go in Niveau.list_element[1])
                    {
                        if (Go != null && Go.GetComponent<Element>().aJoue == false)
                        {
                            changeTurn = false;
                        }
                    }
                    foreach (GameObject Go in Niveau.list_element[0])
                    {
                        if (Go != null)
                        {
                            Go.GetComponent<Element>().flagIsDetected = false;
                        }
                    }
                }

                if (changeTurn == true)
                {
                    if (InferenceEngine.tour == "Ours")
                    {
                        foreach (GameObject Go in Niveau.list_element[0])
                        {
                            if (Go != null)
                            {
                                Go.GetComponent<Element>().aJoue = false;
                            }
                        }
                    }
                    else
                    {
                        foreach (GameObject Go in Niveau.list_element[1])
                        {
                            if (Go != null)
                            {
                                Go.GetComponent<Element>().aJoue = false;
                            }
                        }
                    }
                    StartCoroutine("ChangeTurn");
                }
            }
            /***** Fin Gestion du temps *****/

			/***** Gestion de la pause *****/
            if(Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
            {
                if(pause)
                {
                    pause = false;
                    Time.timeScale = 1;
                    GameObject.Find("Canvas").SetActive(false);
                }
                else
                {
                    pause = true;
                    Time.timeScale = 0;
                    GameObject.Find("GameObject").transform.GetChild(0).gameObject.SetActive(true);
                }
            }
			/***** Fin Gestion de la pause *****/

			/***** Gestion de la santé *****/
			if(GameObject.FindGameObjectWithTag("Bunker") != null)
				GameObject.Find("HealthValue").GetComponent<Image>().fillAmount = (float)(GameObject.FindGameObjectWithTag("Bunker").GetComponent<Element>().PV / 100);
			/***** Fin Gestion de la santé *****/

			if(Input.GetKeyDown(KeyCode.K) && GameObject.FindGameObjectWithTag("Bunker") != null)
			{
				GameObject.FindGameObjectWithTag("Bunker").GetComponent<Element>().PV -= 5;
			}
			else if(GameObject.FindGameObjectWithTag("Bunker") == null)
			{
				GameObject.Find("HealthValue").GetComponent<Image>().fillAmount = 0;
			}
        }

        IEnumerator ChangeTurn()
        {
            Niveau.turnCount++;
			Score.score_ia ();
            InferenceEngine.Change();

            waiting = true;
            if (InferenceEngine.tour == "Poulpe")
            {
                yield return new WaitForSeconds(turnTime);
            }
            waiting = false;

            if (Niveau.turnCount > 10)
            {
                Niveau.turnCount = 0;
                buildTurn = true;
            }
            else
                buildTurn = false;
        }

        void Awake()
        {
            turnTimeStatic = turnTime;
            Niveau.list_element[0] = new List<GameObject>();
            Niveau.list_element[1] = new List<GameObject>();
            Niveau.list_element[2] = new List<GameObject>();
			for (int i = 0; i < 2; i++)
			{
				Score.ressourcediv_IA[i] = new double[3];
			}	
        }

        private void initializePlayer()
        {
            GameObject bunker = Instantiate(Resources.Load("Prefab/Neutre/Bunker")) as GameObject;
            bunker.transform.position = new Vector2((float)(bornes[0] / 2) + 0.5f, (float)(bornes[1] / 2) + 0.5f);
            bunker.transform.localScale = new Vector2(0.5f, 0.5f);

            int X = (int)bunker.transform.position.x;
            int Y = (int)bunker.transform.position.y;

            Element SCbatiment = bunker.GetComponent<Batiment>();
            SCbatiment.PV = 100;
            SCbatiment.hauteur = 1;
            SCbatiment.largeur = 1;
            SCbatiment.camp = "Obstacle";
            SCbatiment.nom = "Bunker";
            SCbatiment.list_case = new GameObject[SCbatiment.hauteur, SCbatiment.largeur];

            SCbatiment.list_case[0, 0] = Niveau.grille[(int)(X + 0.5), (int)(Y + 0.5)];
            SCbatiment.list_case[0, 0].GetComponent<Case>().occupe = true;
            SCbatiment.list_case[0, 0].GetComponent<Case>().element = bunker;
            Niveau.list_element[2].Add(bunker);
        }

        private void InitializeDebugSpawners()
        {
            spawners = new Batiment[16];
            int z = 0;

            Batiment SCbatiment;
            GameObject batiment;

            batiment = Instantiate(Resources.Load("Prefab/Ours/Mine")) as GameObject;
            batiment.transform.position = new Vector2(9f + 0.5f, 26f + 0.5f);
            batiment.transform.localScale = new Vector2(0.5f, 0.5f);
            SCbatiment = batiment.AddComponent<Batiment>();
            SCbatiment.hauteur = 1;
            SCbatiment.largeur = 1;
            SCbatiment.PV = 10000;
			SCbatiment.camp = "Obstacle";
            SCbatiment.nom = "Mine1";
            SCbatiment.list_case = new GameObject[SCbatiment.hauteur, SCbatiment.largeur];
            for (int i = 0; i < SCbatiment.hauteur; i++)
            {
                for (int j = 0; j < SCbatiment.largeur; j++)
                {
                    SCbatiment.list_case[i, j] = Niveau.grille[(int)(i + batiment.transform.position.x - 0.5), (int)(j + batiment.transform.position.y - 0.5)];
                    SCbatiment.list_case[i, j].GetComponent<Case>().occupe = true;
                    SCbatiment.list_case[i, j].GetComponent<Case>().element = batiment;
                }
            }

			batiment = Instantiate(Resources.Load("Prefab/Ours/Mine")) as GameObject;
			batiment.transform.position = new Vector2(10f + 0.5f, 8f + 0.5f);
			batiment.transform.localScale = new Vector2(0.5f, 0.5f);
			SCbatiment = batiment.AddComponent<Batiment>();
			SCbatiment.hauteur = 1;
			SCbatiment.largeur = 1;
			SCbatiment.PV = 10000;
			SCbatiment.camp = "Obstacle";
			SCbatiment.nom = "Mine2";
			SCbatiment.list_case = new GameObject[SCbatiment.hauteur, SCbatiment.largeur];
			for (int i = 0; i < SCbatiment.hauteur; i++)
			{
				for (int j = 0; j < SCbatiment.largeur; j++)
				{
					SCbatiment.list_case[i, j] = Niveau.grille[(int)(i + batiment.transform.position.x - 0.5), (int)(j + batiment.transform.position.y - 0.5)];
					SCbatiment.list_case[i, j].GetComponent<Case>().occupe = true;
					SCbatiment.list_case[i, j].GetComponent<Case>().element = batiment;
				}
			}

			batiment= Instantiate(Resources.Load("Prefab/Poulpe/Mine")) as GameObject;
			batiment.transform.position= new Vector2(42f + 0.5f, 7f + 0.5f);
			batiment.transform.localScale=new Vector2(0.5f,0.5f);
			SCbatiment=batiment.AddComponent<Batiment>();
			SCbatiment.hauteur=1;
			SCbatiment.largeur=1;
			SCbatiment.PV=10000;
			SCbatiment.camp = "Obstacle";
			SCbatiment.nom = "Mine3";
			SCbatiment.list_case = new GameObject[SCbatiment.hauteur, SCbatiment.largeur];
			for (int i = 0; i < SCbatiment.hauteur; i++)
			{
				for (int j = 0; j < SCbatiment.largeur; j++)
				{
					SCbatiment.list_case[i,j] = Niveau.grille[(int)(i + batiment.transform.position.x - 0.5),(int)(j + batiment.transform.position.y - 0.5)];
					SCbatiment.list_case[i,j].GetComponent<Case>().occupe = true;
					SCbatiment.list_case[i,j].GetComponent<Case>().element = batiment;
				}
			}

			batiment= Instantiate(Resources.Load("Prefab/Poulpe/Mine")) as GameObject;
			batiment.transform.position= new Vector2(41f + 0.5f, 25f + 0.5f);
			batiment.transform.localScale=new Vector2(0.5f,0.5f);
			SCbatiment=batiment.AddComponent<Batiment>();
			SCbatiment.hauteur=1;
			SCbatiment.largeur=1;
			SCbatiment.PV=10000;
			SCbatiment.camp = "Obstacle";
			SCbatiment.nom = "Mine4";
			SCbatiment.list_case = new GameObject[SCbatiment.hauteur, SCbatiment.largeur];
			for (int i = 0; i < SCbatiment.hauteur; i++)
			{
				for (int j = 0; j < SCbatiment.largeur; j++)
				{
					SCbatiment.list_case[i,j] = Niveau.grille[(int)(i + batiment.transform.position.x - 0.5),(int)(j + batiment.transform.position.y - 0.5)];
					SCbatiment.list_case[i,j].GetComponent<Case>().occupe = true;
					SCbatiment.list_case[i,j].GetComponent<Case>().element = batiment;
				}
			}

			batiment = Instantiate(Resources.Load("Prefab/Ours/usine_ours")) as GameObject;
			batiment.transform.position = new Vector2(4f, 26f);
			batiment.transform.localScale = new Vector2(0.8f, 0.8f);
	        SCbatiment = batiment.GetComponent<Batiment>();
	        SCbatiment.hauteur = 2;
	        SCbatiment.largeur = 2;
	        SCbatiment.PV = 10;
	        SCbatiment.camp = "Ours";
	        SCbatiment.nom = "USINE" + z;
	        SCbatiment.list_case = new GameObject[SCbatiment.hauteur, SCbatiment.largeur];
			batiment.AddComponent<State>();
			batiment.AddComponent<RuleBase>();
			batiment.AddComponent<InferenceEngine>().m_RuleBaseFilePath = "Batiment";
			batiment.GetComponent<InferenceEngine>().m_RuleBaseTick = (int)(Chargement_jeu.turnTimeStatic * 2000f);

	        Niveau.list_element[0].Add(batiment);

	        for (int i = 0; i < SCbatiment.hauteur; i++)
	        {
	            for (int j = 0; j < SCbatiment.largeur; j++)
	            {
	                SCbatiment.list_case[i, j] = Niveau.grille[(int)(i + batiment.transform.position.x - 0.5), (int)(j + batiment.transform.position.y - 0.5)];
	                SCbatiment.list_case[i, j].GetComponent<Case>().occupe = true;
	                SCbatiment.list_case[i, j].GetComponent<Case>().element = batiment;
	            }
	        }

	        spawners[z] = SCbatiment;
	        SCbatiment.vision = 3;
	        SCbatiment.Produire("Collecteur_ours", Production.is_done);
			z++;

			batiment = Instantiate(Resources.Load("Prefab/Ours/usine_ours")) as GameObject;
			batiment.transform.position = new Vector2(6f, 20f);
			batiment.transform.localScale = new Vector2(0.8f, 0.8f);
			SCbatiment = batiment.GetComponent<Batiment>();
			SCbatiment.hauteur = 2;
			SCbatiment.largeur = 2;
			SCbatiment.PV = 10;
			SCbatiment.camp = "Ours";
			SCbatiment.nom = "USINE" + z;
			SCbatiment.list_case = new GameObject[SCbatiment.hauteur, SCbatiment.largeur];
			
			Niveau.list_element[0].Add(batiment);
			
			for (int i = 0; i < SCbatiment.hauteur; i++)
			{
				for (int j = 0; j < SCbatiment.largeur; j++)
				{
					SCbatiment.list_case[i, j] = Niveau.grille[(int)(i + batiment.transform.position.x - 0.5), (int)(j + batiment.transform.position.y - 0.5)];
					SCbatiment.list_case[i, j].GetComponent<Case>().occupe = true;
					SCbatiment.list_case[i, j].GetComponent<Case>().element = batiment;
				}
			}
			
			spawners[z] = SCbatiment;
			SCbatiment.vision = 3;
			SCbatiment.Produire("Collecteur_ours", Production.is_done);
			z++;

			batiment = Instantiate(Resources.Load("Prefab/Ours/usine_ours")) as GameObject;
			batiment.transform.position = new Vector2(6f, 14f);
			batiment.transform.localScale = new Vector2(0.8f, 0.8f);
			SCbatiment = batiment.GetComponent<Batiment>();
			SCbatiment.hauteur = 2;
			SCbatiment.largeur = 2;
			SCbatiment.PV = 10;
			SCbatiment.camp = "Ours";
			SCbatiment.nom = "USINE" + z;
			SCbatiment.list_case = new GameObject[SCbatiment.hauteur, SCbatiment.largeur];
			
			Niveau.list_element[0].Add(batiment);
			
			for (int i = 0; i < SCbatiment.hauteur; i++)
			{
				for (int j = 0; j < SCbatiment.largeur; j++)
				{
					SCbatiment.list_case[i, j] = Niveau.grille[(int)(i + batiment.transform.position.x - 0.5), (int)(j + batiment.transform.position.y - 0.5)];
					SCbatiment.list_case[i, j].GetComponent<Case>().occupe = true;
					SCbatiment.list_case[i, j].GetComponent<Case>().element = batiment;
				}
			}
			
			spawners[z] = SCbatiment;
			SCbatiment.vision = 3;
			SCbatiment.Produire("Collecteur_ours", Production.is_done);
			z++;

			batiment = Instantiate(Resources.Load("Prefab/Ours/usine_ours")) as GameObject;
			batiment.transform.position = new Vector2(4f, 8f);
			batiment.transform.localScale = new Vector2(0.8f, 0.8f);
			SCbatiment = batiment.GetComponent<Batiment>();
			SCbatiment.hauteur = 2;
			SCbatiment.largeur = 2;
			SCbatiment.PV = 10;
			SCbatiment.camp = "Ours";
			SCbatiment.nom = "USINE" + z;
			SCbatiment.list_case = new GameObject[SCbatiment.hauteur, SCbatiment.largeur];
			
			Niveau.list_element[0].Add(batiment);
			
			for (int i = 0; i < SCbatiment.hauteur; i++)
			{
				for (int j = 0; j < SCbatiment.largeur; j++)
				{
					SCbatiment.list_case[i, j] = Niveau.grille[(int)(i + batiment.transform.position.x - 0.5), (int)(j + batiment.transform.position.y - 0.5)];
					SCbatiment.list_case[i, j].GetComponent<Case>().occupe = true;
					SCbatiment.list_case[i, j].GetComponent<Case>().element = batiment;
				}
			}
			
			spawners[z] = SCbatiment;
			SCbatiment.vision = 3;
			SCbatiment.Produire("Collecteur_ours", Production.is_done);
			z++;

	        batiment = Instantiate(Resources.Load("Prefab/Poulpe/usine_poulpe")) as GameObject;
	        batiment.transform.position = new Vector2(49f, 26f);
	        batiment.transform.localScale = new Vector2(0.8f, 0.8f);
	        SCbatiment = batiment.GetComponent<Batiment>();
	        SCbatiment.hauteur = 2;
	        SCbatiment.largeur = 2;
	        SCbatiment.PV = 10;
	        SCbatiment.camp = "Poulpe";
	        SCbatiment.nom = "USINE" + z;
	        SCbatiment.list_case = new GameObject[SCbatiment.hauteur, SCbatiment.largeur];
			batiment.AddComponent<State>();
	        batiment.AddComponent<RuleBase>();
	        batiment.AddComponent<InferenceEngine>().m_RuleBaseFilePath = "Batiment";
		    batiment.GetComponent<InferenceEngine>().m_RuleBaseTick = (int)(Chargement_jeu.turnTimeStatic * 2000f);
			Niveau.list_element[1].Add(batiment);

	        for (int i = 0; i < SCbatiment.hauteur; i++)
	        {
	            for (int j = 0; j < SCbatiment.largeur; j++)
	            {
	                SCbatiment.list_case[i, j] = Niveau.grille[(int)(i + batiment.transform.position.x - 0.5), (int)(j + batiment.transform.position.y - 0.5)];
	                SCbatiment.list_case[i, j].GetComponent<Case>().occupe = true;
	                SCbatiment.list_case[i, j].GetComponent<Case>().element = batiment;
	            }
	        }

	        SCbatiment.vision = 3;
	        spawners[z] = SCbatiment;
			SCbatiment.Produire("Collecteur_Poulpe",Production.is_done);
			z++;

			batiment = Instantiate(Resources.Load("Prefab/Poulpe/usine_poulpe")) as GameObject;
			batiment.transform.position = new Vector2(50f, 20f);
			batiment.transform.localScale = new Vector2(0.8f, 0.8f);
			SCbatiment = batiment.GetComponent<Batiment>();
			SCbatiment.hauteur = 2;
			SCbatiment.largeur = 2;
			SCbatiment.PV = 10;
			SCbatiment.camp = "Poulpe";
			SCbatiment.nom = "USINE" + z;
			SCbatiment.list_case = new GameObject[SCbatiment.hauteur, SCbatiment.largeur];
			Niveau.list_element[1].Add(batiment);
			
			for (int i = 0; i < SCbatiment.hauteur; i++)
			{
				for (int j = 0; j < SCbatiment.largeur; j++)
				{
					SCbatiment.list_case[i, j] = Niveau.grille[(int)(i + batiment.transform.position.x - 0.5), (int)(j + batiment.transform.position.y - 0.5)];
					SCbatiment.list_case[i, j].GetComponent<Case>().occupe = true;
					SCbatiment.list_case[i, j].GetComponent<Case>().element = batiment;
				}
			}
			
			SCbatiment.vision = 3;
			spawners[z] = SCbatiment;
			SCbatiment.Produire("Collecteur_Poulpe",Production.is_done);
			z++;

			batiment = Instantiate(Resources.Load("Prefab/Poulpe/usine_poulpe")) as GameObject;
			batiment.transform.position = new Vector2(51f, 14f);
			batiment.transform.localScale = new Vector2(0.8f, 0.8f);
			SCbatiment = batiment.GetComponent<Batiment>();
			SCbatiment.hauteur = 2;
			SCbatiment.largeur = 2;
			SCbatiment.PV = 10;
			SCbatiment.camp = "Poulpe";
			SCbatiment.nom = "USINE" + z;
			SCbatiment.list_case = new GameObject[SCbatiment.hauteur, SCbatiment.largeur];
			Niveau.list_element[1].Add(batiment);
			
			for (int i = 0; i < SCbatiment.hauteur; i++)
			{
				for (int j = 0; j < SCbatiment.largeur; j++)
				{
					SCbatiment.list_case[i, j] = Niveau.grille[(int)(i + batiment.transform.position.x - 0.5), (int)(j + batiment.transform.position.y - 0.5)];
					SCbatiment.list_case[i, j].GetComponent<Case>().occupe = true;
					SCbatiment.list_case[i, j].GetComponent<Case>().element = batiment;
				}
			}
			
			SCbatiment.vision = 3;
			spawners[z] = SCbatiment;
			SCbatiment.Produire("Collecteur_Poulpe",Production.is_done);
			z++;

			batiment = Instantiate(Resources.Load("Prefab/Poulpe/usine_poulpe")) as GameObject;
			batiment.transform.position = new Vector2(49f, 8f);
			batiment.transform.localScale = new Vector2(0.8f, 0.8f);
			SCbatiment = batiment.GetComponent<Batiment>();
			SCbatiment.hauteur = 2;
			SCbatiment.largeur = 2;
			SCbatiment.PV = 10;
			SCbatiment.camp = "Poulpe";
			SCbatiment.nom = "USINE" + z;
			SCbatiment.list_case = new GameObject[SCbatiment.hauteur, SCbatiment.largeur];
			Niveau.list_element[1].Add(batiment);
			
			for (int i = 0; i < SCbatiment.hauteur; i++)
			{
				for (int j = 0; j < SCbatiment.largeur; j++)
				{
					SCbatiment.list_case[i, j] = Niveau.grille[(int)(i + batiment.transform.position.x - 0.5), (int)(j + batiment.transform.position.y - 0.5)];
					SCbatiment.list_case[i, j].GetComponent<Case>().occupe = true;
					SCbatiment.list_case[i, j].GetComponent<Case>().element = batiment;
				}
			}
			
			SCbatiment.vision = 3;
			spawners[z] = SCbatiment;
			SCbatiment.Produire("Collecteur_Poulpe",Production.is_done);
			z++;
		}
    }
}
