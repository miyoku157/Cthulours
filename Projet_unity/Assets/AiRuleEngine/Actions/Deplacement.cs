using UnityEngine;
using System;
using System.Collections.Generic;
using AssemblyCSharp;
using System.Collections;

namespace AiRuleEngine
{
#if UNITY_EDITOR
	[ScriptName("Deplacement")]
	[ScriptCategory("Movement")]
#endif

    public class Deplacement : BaseAction
    {
        private GameObject GO;
        private bool move;
		private List<char> deplacement;
		private Unite ele;
		private int mouvement;

        void Start()
        {
            GO = this.gameObject;
            move = true;
        }

        void Update()
        {

        }

        public override bool Execute()
        {
            StartCoroutine("Move");
            return true;
        }

        private IEnumerator Move()
        {
            ele = GO.GetComponent<Unite>();
            deplacement = ele.deplacement;
            mouvement = ele.mouvement;

            if (deplacement != null)
            {
	            if (deplacement.Count == 0)
	            {
	                ele.deplacement = null;
	                mouvement = 0;
	            }
	            else
	            {
					StartCoroutine("MoveTowards");
	       		}
            }
            else
            {
                int rand = UnityEngine.Random.Range(7, 19);

                if (ele.camp.Equals("Ours"))
                {
                    ele.deplacement = Niveau.boucle_path((int)this.gameObject.transform.position.x, (int)this.gameObject.transform.position.y, 50, rand);
                }
                else
                {
                    ele.deplacement = Niveau.boucle_path((int)this.gameObject.transform.position.x, (int)this.gameObject.transform.position.y, 2, rand);
                }
            }
            yield break;
        }

		private IEnumerator MoveTowards()
		{
			float tParam = 0;
			Vector3 pos = GO.transform.position;
			char direction = deplacement[0];

			for (int i = 0; i < ele.hauteur; i++)
			{
				for (int j = 0; j < ele.largeur; j++)
				{
					ele.list_case[i, j] = Niveau.grille[(int)(i + GO.transform.position.x - 0.5), (int)(j + GO.transform.position.y - 0.5)];
					ele.list_case[i, j].GetComponent<Case>().occupe = false;
					ele.list_case[i, j].GetComponent<Case>().element = null;
				}
			}

			if(deplacement.Count != 0)
			{
				switch (direction)
				{
					case 'D':
						if (!Niveau.grille[(int)(GO.transform.position.x + 0.5), (int)(GO.transform.position.y - 0.5)].GetComponent<Case>().occupe)
						{
							for (int i = 0; i < ele.hauteur; i++)
							{
								for (int j = 0; j < ele.largeur; j++)
								{
									ele.list_case[i, j] = Niveau.grille[(int)(i + GO.transform.position.x + 0.5), (int)(j + GO.transform.position.y - 0.5)];
									ele.list_case[i, j].GetComponent<Case>().occupe = true;
									ele.list_case[i, j].GetComponent<Case>().element = GO.gameObject;
								}
							}

							GO.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 270));

							while(GO.transform.position.x < pos.x + 1.0f)
							{
								tParam += Time.deltaTime;
								GO.transform.position = new Vector3(Mathf.Lerp(pos.x, pos.x + 1f, tParam), pos.y, pos.z);
								yield return new WaitForSeconds(0.01f);
							}
							deplacement.RemoveAt(0);
						}
						else
						{
							int rand = UnityEngine.Random.Range(7, 19);
							
							if (ele.camp.Equals("Ours"))
							{
								ele.deplacement = Niveau.boucle_path((int)this.gameObject.transform.position.x, (int)this.gameObject.transform.position.y, 50, rand);
							}
							else
							{
								ele.deplacement = Niveau.boucle_path((int)this.gameObject.transform.position.x, (int)this.gameObject.transform.position.y, 2, rand);
							}
						}
						break;
					case 'G':
						if (!Niveau.grille[(int)(GO.transform.position.x - 1.5), (int)(GO.transform.position.y - 0.5)].GetComponent<Case>().occupe)
						{
							for (int i = 0; i < ele.hauteur; i++)
							{
								for (int j = 0; j < ele.largeur; j++)
								{
									ele.list_case[i, j] = Niveau.grille[(int)(i + GO.transform.position.x - 1.5), (int)(j + GO.transform.position.y - 0.5)];
									ele.list_case[i, j].GetComponent<Case>().occupe = true;
									ele.list_case[i, j].GetComponent<Case>().element = GO.gameObject;
								}
							}
							GO.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
							
							while(GO.transform.position.x > pos.x - 1.0f)
							{
								tParam += Time.deltaTime;
								GO.transform.position = new Vector3(Mathf.Lerp(pos.x, pos.x - 1f, tParam), pos.y, pos.z);
								yield return new WaitForSeconds(0.01f);
							}
							
							deplacement.RemoveAt(0);
						}
						else
						{
							int rand = UnityEngine.Random.Range(7, 19);
							
							if (ele.camp.Equals("Ours"))
							{
								ele.deplacement = Niveau.boucle_path((int)this.gameObject.transform.position.x, (int)this.gameObject.transform.position.y, 50, rand);
							}
							else
							{
								ele.deplacement = Niveau.boucle_path((int)this.gameObject.transform.position.x, (int)this.gameObject.transform.position.y, 2, rand);
							}
						}
						break;
					case 'H':
						if (!Niveau.grille[(int)(GO.transform.position.x - 0.5), (int)(GO.transform.position.y + 0.5)].GetComponent<Case>().occupe)
						{
							for (int i = 0; i < ele.hauteur; i++)
							{
								for (int j = 0; j < ele.largeur; j++)
								{
									ele.list_case[i, j] = Niveau.grille[(int)(i + GO.transform.position.x - 0.5), (int)(j + GO.transform.position.y + 0.5)];
									ele.list_case[i, j].GetComponent<Case>().occupe = true;
									ele.list_case[i, j].GetComponent<Case>().element = GO.gameObject;
								}
							}
							GO.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

							while(GO.transform.position.y < pos.y + 1.0f)
							{
								tParam += Time.deltaTime;
								GO.transform.position = new Vector3(pos.x, Mathf.Lerp(pos.y, pos.y + 1f, tParam), pos.z);
								yield return new WaitForSeconds(0.01f);
							}

							deplacement.RemoveAt(0);
						}
						else
						{
							int rand = UnityEngine.Random.Range(7, 19);
							
							if (ele.camp.Equals("Ours"))
							{
								ele.deplacement = Niveau.boucle_path((int)this.gameObject.transform.position.x, (int)this.gameObject.transform.position.y, 50, rand);
							}
							else
							{
								ele.deplacement = Niveau.boucle_path((int)this.gameObject.transform.position.x, (int)this.gameObject.transform.position.y, 2, rand);
							}
						}
						break;
					case 'B':
						if (!Niveau.grille[(int)(GO.transform.position.x - 0.5), (int)(GO.transform.position.y - 1.5)].GetComponent<Case>().occupe)
						{
							for (int i = 0; i < ele.hauteur; i++)
							{
								for (int j = 0; j < ele.largeur; j++)
								{
									ele.list_case[i, j] = Niveau.grille[(int)(i + GO.transform.position.x - 0.5), (int)(j + GO.transform.position.y - 1.5)];
									ele.list_case[i, j].GetComponent<Case>().occupe = true;
									ele.list_case[i, j].GetComponent<Case>().element = GO.gameObject;
								}
							}
							GO.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));

							while(GO.transform.position.y > pos.y - 1.0f)
							{
								tParam += Time.deltaTime;
								GO.transform.position = new Vector3(pos.x, Mathf.Lerp(pos.y, pos.y - 1f, tParam), pos.z);
								yield return new WaitForSeconds(0.01f);
							}

							deplacement.RemoveAt(0);
						}
						else
						{
							int rand = UnityEngine.Random.Range(7, 19);
							
							if (ele.camp.Equals("Ours"))
							{
								ele.deplacement = Niveau.boucle_path((int)this.gameObject.transform.position.x, (int)this.gameObject.transform.position.y, 50, rand);
							}
							else
							{
								ele.deplacement = Niveau.boucle_path((int)this.gameObject.transform.position.x, (int)this.gameObject.transform.position.y, 2, rand);
							}
						}
						break;
				}
				mouvement--;
			}
			else
			{
				for (int i = 0; i < ele.hauteur; i++)
				{
					for (int j = 0; j < ele.largeur; j++)
					{
						ele.list_case[i, j] = Niveau.grille[(int)(i + GO.transform.position.x - 0.5), (int)(j + GO.transform.position.y - 0.5)];
						ele.list_case[i, j].GetComponent<Case>().occupe = true;
						ele.list_case[i, j].GetComponent<Case>().element = this.gameObject;
					}
				}
			}

			if(mouvement > 0 && deplacement[0] != null)
				StartCoroutine("MoveTowards");

			yield break;
		}

        private IEnumerator WaitMovement()
        {
            yield return new WaitForSeconds(0.3f);
            move = true;
            yield break;
        }

        private IEnumerator PathDebug(IEnumerable<Char> deplacement)
        {
            /*GameObject marqueur = new GameObject("DebugPath");
            marqueur.transform.parent = GO.transform;
            marqueur.transform.position = GO.transform.position;
            marqueur.transform.localScale = new Vector2(0.4f, 0.4f);
            marqueur.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/Effets/Cross");
			
            foreach(Char dir in deplacement)
            {
                switch(dir)
                {
                case 'H' :
                    marqueur.transform.Translate(1.0f, 0.0f,0.0f);
                    break;
					
                case 'B' :
                    marqueur.transform.Translate(-1.0f, 0.0f,0.0f);
                    break;
					
                case 'G' :
                    marqueur.transform.Translate(0.0f, -1.0f,0.0f);
                    break;
					
                case 'D' :
                    marqueur.transform.Translate(0.0f, 1.0f,0.0f);;
                    break;
                }
                */
            yield return new WaitForSeconds(0.1f);
        }
    }
}