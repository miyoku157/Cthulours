using UnityEngine;
using System.Collections.Generic;
using AssemblyCSharp;
namespace AiRuleEngine
{
#if UNITY_EDITOR
[ScriptName("Deplacement_ennemie")]
[ScriptCategory("Movement")]
#endif
    public class Deplacement_ennemie : BaseAction
    {
        private GameObject Go;
        // Use this for initialization
        void Start()
        {
            Go = this.gameObject;
        }

        // Update is called once per frame
        void Update()
        {

        }
        public override bool Execute()
        {
            List<GameObject> ennemi = new List<GameObject>();

            if (Go.GetComponent<Element>().camp == "Ours")
            {
                if (Niveau.list_element[1].Count > 0)
                {
                    ennemi = Niveau.list_element[1];
                }
            }
            else
            {
                if (Niveau.list_element[0].Count > 0)
                {
                    ennemi = Niveau.list_element[0];
                }
            }
            float distance = 1000;
            bool ispassed = false;
            Vector3 proche = new Vector3();
            if (ennemi.Count > 0)
            {
                foreach (GameObject game in ennemi)
                {
                    if (Vector3.Distance(Go.transform.position, game.transform.position) < distance)
                    {
                        distance = Vector3.Distance(Go.transform.position, game.transform.position);
                        proche = game.transform.position;
                        ispassed = true;
                    }
                }
            }
            if (Niveau.list_element[2].Count > 0)
            {
                ennemi = Niveau.list_element[2];
                foreach (GameObject game in ennemi)
                {
                    if (Vector3.Distance(Go.transform.position, game.transform.position) < distance)
                    {
                        distance = Vector3.Distance(Go.transform.position, game.transform.position);
                        proche = game.transform.position;
                        ispassed = true;
                    }
                }
            }
            if (ispassed)
            {
                Go.GetComponent<Unite>().deplacement = Niveau.boucle_path((int)Go.transform.position.x, (int)Go.transform.position.y, (int)proche.x, (int)proche.y);
            }
            return true;
        }
    }
}
