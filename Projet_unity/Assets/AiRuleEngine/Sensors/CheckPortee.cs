using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp;
namespace AiRuleEngine
{
#if UNITY_EDITOR
	[ScriptName("CheckPortee")]
	[ScriptCategory("Movement")]
#endif

    public class CheckPortee : BaseSensor
    {
        private GameObject Go;

        void Start()
        {
            Go = this.gameObject;
        }

        public override object Execute(System.Type Type)
        {
            Element Ele = Go.GetComponent<Element>();
            Unite unit = Go.GetComponent<Unite>();
            int decalage = 0;
            Vector2 depart;
            Vector2 caseTest = new Vector2();
            unit.enn_pos = new Dictionary<float, Vector3>();
            List<string> nom = new List<string>();

            if (unit.largeur == 1 && unit.hauteur == 1)
            {
                depart = new Vector2(Go.transform.position.x, Go.transform.position.y);
            }
            else
            {
                depart = new Vector2(Go.transform.position.x - unit.largeur / 2 + 0.5f,
                                      Go.transform.position.y - unit.hauteur / 2 + 0.5f);
            }

            caseTest = new Vector2(depart.x - unit.attaque.portee, depart.y - unit.attaque.portee);

            if (unit.attaque.portee == 1)
            {
                decalage = 1;
            }
            int compteur = 0;
            for (int j = (int)caseTest.x; j < (int)caseTest.x + 1 + unit.attaque.portee * 2; j++)
            {
                for (int h = (int)caseTest.y; h < (int)caseTest.y + 1 + unit.attaque.portee * 2; h++)
                {
                    if (j + h > Go.transform.position.x + Go.transform.position.y - 3 - decalage - unit.attaque.portee &&
                        j + h < Go.transform.position.x + Go.transform.position.y + 1 + decalage + unit.attaque.portee &&
                        h - j > Go.transform.position.y - Go.transform.position.x - 2 - decalage - unit.attaque.portee &&
                        h - j < Go.transform.position.y - Go.transform.position.x + 2 + decalage + unit.attaque.portee &&
                        j > 0 && j < Chargement_jeu.bornes[0] && h > -1 && h < Chargement_jeu.bornes[1])
                    {
						if (Niveau.grille[j, h].GetComponent<Case>().occupe&&Niveau.grille[j, h].GetComponent<Case>().element!=null)//cache misère
                        {
                            Element objet = Niveau.grille[j, h].GetComponent<Case>().element.GetComponent<Element>();
                            if (objet.flagIsDetected)
                            {
                                if (objet != null)
                                {
                                    if (objet.camp != unit.camp && Niveau.grille[j, h].GetComponent<Case>().occupe
                                       && !nom.Contains(objet.nom))
                                    {
                                        nom.Add(objet.nom);
                                        compteur++;
                                        unit.enn_pos.Add(Vector3.Distance(unit.transform.position, objet.transform.position), objet.transform.position);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return compteur;
        }
    }
}