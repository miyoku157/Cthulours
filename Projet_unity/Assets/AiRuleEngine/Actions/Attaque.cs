using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp;
namespace AiRuleEngine
{
#if UNITY_EDITOR
	[ScriptName("Attaque")]
	[ScriptCategory("Movement")]
#endif
    public class Attaque : BaseAction
    {
        private GameObject GO;
        public bool pv_neg;
        private Unite uni;
        private List<float> dist;
        // Use this for initialization
        void Start()
        {
            GO = this.gameObject;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public override bool Execute()
        {
            uni = GO.GetComponent<Unite>();
            dist = new List<float>();

            foreach (KeyValuePair<float, Vector3> entry in uni.enn_pos)
            {
                dist.Add(entry.Key);
            }
            dist.Sort();

            StartCoroutine("Projectile");
            return true;
        }

        IEnumerator Projectile()
        {

            int i = 0;
            int j = 0;
            while (i < uni.attaque.nb_attaque)
            {
                if (uni.enn_pos.Count > 0)
                {
                    while (uni.enn_pos[dist[j]] == null)
                    {
                        j++;
                    }
					if(uni.enn_pos[dist[j]] != null && uni.enn_pos != null)
					{
	                    Vector3 vec = uni.enn_pos[dist[j]];
	                    //j=0;
	                    Case cas = Niveau.grille[(int)(vec.x - 0.5), (int)(vec.y - 0.5)].GetComponent<Case>();
	                    GameObject objet = cas.element;

						if(objet != null)
						{
	                    	Element ele = objet.GetComponent<Element>();
							string pref=null;
							if(GO.GetComponent<Element>().camp=="Ours"){
								pref="_ours";
							}else{
								pref="_poulpe";
							}
							GameObject projectile = Instantiate(Resources.Load("Prefab/Effets/Tir"+pref)) as GameObject;
							AudioSource[] source=projectile.GetComponents<AudioSource>();
							source[UnityEngine.Random.Range(0,2)].Play();
							projectile.tag = "Tir";
							
							projectile.GetComponent<Projectile>().ini(GO.transform.position.x, GO.transform.position.y, vec.x, vec.y, dist[0], uni.attaque.degat, uni.attaque.redu_armure, uni.attaque.Type);

						}
					}
                }
                i++;
                yield return new WaitForSeconds(0.1f);

            }

        }

    }
}
