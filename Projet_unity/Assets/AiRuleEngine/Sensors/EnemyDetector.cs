using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AssemblyCSharp;

namespace AiRuleEngine
{
#if UNITY_EDITOR
	[ScriptName("EnemyDetector")]
	[ScriptCategory("Geography")]
	[ScriptReturnTypeAttribute("int")]
#endif
    public class EnemyDetector : BaseSensor
    {
        public int compteurDetection = 0;
        private GameObject Go;
        private Element unite;
        private List<string> listeNoms;

        void Start()
        {
            Go = this.gameObject;
            unite = Go.GetComponent<Element>();
            listeNoms = new List<string>();
        }

        public override object Execute(System.Type type)
        {
            int compteurRapport = 0;
            compteurDetection = 0;
            int decalage = 0;
            Vector2 depart;
            Vector2 caseTest = new Vector2();
            string nomPrecedent = "";

            if (unite.largeur == 1 && unite.hauteur == 1)
            {
                depart = new Vector2(Go.transform.position.x, Go.transform.position.y);
            }
            else
            {
                depart = new Vector2(Go.transform.position.x - unite.largeur / 2 + 0.5f,
                                      Go.transform.position.y - unite.hauteur / 2 + 0.5f);
            }

            caseTest = new Vector2(depart.x - unite.vision, depart.y - unite.vision);

            if (unite.vision == 1)
            {
                decalage = 1;
            }

            for (int j = (int)caseTest.x; j < (int)caseTest.x + 1 + unite.vision * 2; j++)
            {
                for (int h = (int)caseTest.y; h < (int)caseTest.y + 1 + unite.vision * 2; h++)
                {
                    if (j + h > Go.transform.position.x + Go.transform.position.y - 3 - decalage - unite.vision &&
                        j + h < Go.transform.position.x + Go.transform.position.y + 1 + decalage + unite.vision &&
                        h - j > Go.transform.position.y - Go.transform.position.x - 2 - decalage - unite.vision &&
                        h - j < Go.transform.position.y - Go.transform.position.x + 2 + decalage + unite.vision &&
                        j > 0 && j < Chargement_jeu.bornes[0] && h > -1 && h < Chargement_jeu.bornes[1])
                    {


						if (Niveau.grille[j, h].GetComponent<Case>().occupe&&Niveau.grille[j, h].GetComponent<Case>().element!=null)//cache misère
                        {
							if(Niveau.grille[j, h].GetComponent<Case>().element.GetComponent<Element>().nom != unite.nom){
                            if (Niveau.grille[j, h].GetComponent<Case>().element.GetComponent<Element>().camp == unite.camp
                               && listeNoms.Find(x => x == nomPrecedent) != nomPrecedent)
                            {
                                nomPrecedent = Niveau.grille[j, h].GetComponent<Case>().element.GetComponent<Element>().nom;
                                listeNoms.Add(Niveau.grille[j, h].GetComponent<Case>().element.GetComponent<Element>().nom);
                                compteurRapport--;
                            }
                            else if (Niveau.grille[j, h].GetComponent<Case>().element.GetComponent<Element>().camp != null &&
                                    Niveau.grille[j, h].GetComponent<Case>().element.GetComponent<Element>().camp != unite.camp &&
                                    Niveau.grille[j, h].GetComponent<Case>().element.GetComponent<Element>().camp != "Obstacle")
                            {
                                listeNoms.Add(Niveau.grille[j, h].GetComponent<Case>().element.GetComponent<Element>().nom);
                                if (nomPrecedent == "")
                                {
                                    nomPrecedent = Niveau.grille[j, h].GetComponent<Case>().element.GetComponent<Element>().nom;
                                    Niveau.grille[j, h].GetComponent<Case>().element.GetComponent<Element>().flagIsDetected = true;
                                    compteurRapport++;
                                    compteurDetection++;
                                }
                                else if (nomPrecedent != Niveau.grille[j, h].GetComponent<Case>().element.GetComponent<Element>().nom
                                        && listeNoms.Find(x => x == nomPrecedent) != nomPrecedent)
                                {
                                    Niveau.grille[j, h].GetComponent<Case>().element.GetComponent<Element>().flagIsDetected = true;
                                    nomPrecedent = Niveau.grille[j, h].GetComponent<Case>().element.GetComponent<Element>().nom;
                                    compteurRapport++;
                                    compteurDetection++;
                                }
                            }
                        }
						}
                    }
                }
            }

            Niveau.setVariable(Go, "EnemyDetector");
            return compteurRapport;
        }
    }
}