using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp;
namespace AiRuleEngine
{
#if UNITY_EDITOR
	[ScriptName("Test_proximite")]
	[ScriptCategory("Movement")]
	[ScriptReturnTypeAttribute("int")]

#endif

    public class Test_proximite : BaseSensor
    {
        private GameObject Go;
        void Start()
        {
            Go = this.gameObject;
        }
        public override object Execute(System.Type type)
        {
            double distance = 1000;
            GameObject resultat = null;
			string tag=null;

            if (Go != null) {
				if (Go.GetComponent<Element> ().camp == "Ours") {
					tag="_Ours";
				} else {
					tag="_Poulpe";	
				}
				GameObject[] ressources = GameObject.FindGameObjectsWithTag("ressources"+tag);

								foreach (GameObject res in ressources) {

										if (distance > Vector3.Distance (Go.transform.position, res.transform.position)) {
												distance = Vector3.Distance (Go.transform.position, res.transform.position);
												resultat = res;
										}
								}
								GameObject[,] test = resultat.GetComponent<Element> ().list_case;
								foreach (GameObject game in resultat.GetComponent<Element>().list_case) {
										if (Vector3.Distance (Go.transform.position, game.transform.position) <= 1.5) {

												return 1;

										}
								}
								distance = 1000;
								ressources = GameObject.FindGameObjectsWithTag ("QG"+tag);
								foreach (GameObject res in ressources) {

										if (distance > Vector3.Distance (Go.transform.position, res.transform.position)) {
												distance = Vector3.Distance (Go.transform.position, res.transform.position);
												resultat = res;
										}
								}
								foreach (GameObject game in resultat.GetComponent<Element>().list_case) {
										if (Vector3.Distance (Go.transform.position, game.transform.position) <= 1.5) {

												return 2;

										}
								}
						}
            return 0;
        }
    }
}