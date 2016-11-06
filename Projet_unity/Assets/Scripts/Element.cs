using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using AiRuleEngine;


namespace AssemblyCSharp
{
    public class Element : MonoBehaviour
    {
        public string nom { get; set; }

        public string camp { get; set; }

        public string type { get; set; }

        public int hauteur { get; set; }

        public int largeur { get; set; }

        public double PV { get; set; }

        public double armure { get; set; }

        public double priorites { get; set; }

        public int vision { get; set; }

        public bool flagIsDetected { get; set; }

        public bool flagIsUnlock { get; set; }

        public int score { get; set; }

        public GameObject[,] list_case { get; set; }

        public bool aJoue;

        public void Initialize(string nom, string camp, string type, int hauteur, double pv,
                               double armure, int largeur, double priorites, int vision, bool flagIsDetected/*, bool flagIsunlock*/, int score)
        {
            this.nom = nom;
            this.camp = camp;
            this.type = type;
            this.hauteur = hauteur;
            this.PV = pv;
            this.armure = armure;
            this.largeur = largeur;
            this.priorites = priorites;
            this.vision = vision;
            this.flagIsDetected = flagIsDetected;
            //this.flagIsUnlock = flagIsUnlock;
            this.score = score;
            this.list_case = new GameObject[hauteur, largeur];
            this.aJoue = false;
        }

        public void destroy()
        {
            if (PV <= 0)
            {
				if(Vector2.Distance(GameObject.FindGameObjectWithTag("Bunker").transform.position, gameObject.transform.position) < 3.0f)
					GameObject.FindGameObjectWithTag("Bunker").GetComponent<Element>().PV -= 5;

                for (int i = 0; i < largeur; i++)
                {
                    for (int j = 0; j < hauteur; j++)
                    {
                        list_case[i, j].GetComponent<Case>().occupe = false;
                        list_case[i, j].GetComponent<Case>().element = null;
                    }
                }
                
                if (camp == "Ours")
                {
                    Niveau.list_element[0].Remove(this.gameObject);
					Defaite(0);
					if(this.gameObject.GetComponent<InferenceEngine>()!=null){
						bool exist= false;
						foreach(GameObject game in Niveau.list_element[0]){
							if(game.GetComponent<Batiment>()!=null&&!exist){
								game.AddComponent<State>();
								game.AddComponent<RuleBase>();
								game.AddComponent<InferenceEngine>().m_RuleBaseFilePath = "Batiment";
								game.GetComponent<InferenceEngine>().m_RuleBaseTick = (int)(Chargement_jeu.turnTimeStatic * 2000f);
								exist=true;
							}
						}
					}
                }
                else if (camp == "Poulpe")
                {
                    Niveau.list_element[1].Remove(this.gameObject);
					Defaite(1);
					if(this.gameObject.GetComponent<InferenceEngine>()!=null){
						bool exist= false;
						foreach(GameObject game in Niveau.list_element[1]){
							if(game.GetComponent<Batiment>()!=null&&!exist){
								game.AddComponent<State>();
								game.AddComponent<RuleBase>();
								game.AddComponent<InferenceEngine>().m_RuleBaseFilePath = "Batiment";
								game.GetComponent<InferenceEngine>().m_RuleBaseTick = (int)(Chargement_jeu.turnTimeStatic * 2000f);
								exist=true;
							}
						}

					}
                }
                else if (camp == "Obstacle")
                {
                    Niveau.list_element[2].Remove(this.gameObject);
					Defaite(2);

                }

				GameObject.Destroy(this.gameObject);
            }
        }
		public void Defaite (int camp){
			bool defaite=true;
			foreach(GameObject game in Niveau.list_element[camp]){
				if(game.GetComponent<Batiment>()!=null){
					defaite=false;
				}
			}
			if( defaite){
				Time.timeScale = 0;

				GameObject.Find("Main Camera").GetComponent<AudioSource>().enabled=false;
				string text= Timer.getTime().ToString();
				GameObject.Find("Defaite").transform.GetChild(0).GetChild(3).GetComponent<Text>().text="Bravo, vous avez tenu pendant "+text +" secondes";
				GameObject.Find("Defaite").transform.GetChild(0).gameObject.SetActive(true);
				GameObject.Find("Defaite").GetComponent<AudioSource>().Play();
				Score.ressource_IA[0]=0;
				Score.ressource_IA[1]=0;
				for (int i = 0; i < 2; i++)
				{
					for(int j= 0;j<3;j++){
						Score.ressourcediv_IA[i][j]=0;

					}
				}
				Timer.Initialize();
				Pouvoir.Initialize();
				StopAllCoroutines();
			}		
		}
        public Element()
        {

        }

        // Use this for initialization
        protected virtual void Start()
        {

        }

        // Update is called once per frame
        protected virtual void Update()
        {
            destroy();
        }
    }
}
