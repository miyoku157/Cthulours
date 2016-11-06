using UnityEngine;
using System.Collections;
using AssemblyCSharp;
namespace AiRuleEngine
{
#if UNITY_EDITOR
	[ScriptName("AgressSensor")]
	[ScriptCategory("Geography")]
	[ScriptReturnTypeAttribute("float")]
#endif
    public class AgressSensor : BaseSensor
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
        public override object Execute(System.Type type)
        {
            int score_ia = 0;
            double ressource_ia = 0;
			float retour = 0;

			if (Go != null) {
								if (Go.GetComponent<Element> ().camp == "Ours") {
										score_ia = Score.score_IA [0];
								} else {
										score_ia = Score.score_IA [1];
								}
								if (Go.GetComponent<Element> ().camp == "Ours") {
										ressource_ia = Score.ressource_IA [0];
								} else {
										ressource_ia = Score.ressource_IA [1];
								}
								if (score_ia < 80) {
										retour += 1;
								} else if (score_ia >= 120 && score_ia < 180) {
										retour += 2;
								} else if (score_ia >= 220) {
										retour += 3;
								}
								if (score_ia >= 80 && score_ia < 120) {
										score_ia -= 80;
										retour += score_ia / 40 * 2 + (1 - score_ia / 40) * 1;
								}
								if (score_ia >= 180 && score_ia < 220) {
										score_ia -= 180;
										retour += score_ia / 40 * 3 + (1 - score_ia / 40) * 2;
								}
								if (ressource_ia < 700) {
										retour += 1;
								} else if (ressource_ia >= 1300 && ressource_ia < 1700) {
										retour += 2;
								} else if (ressource_ia >= 2300) {
										retour += 3;
								}
								if (ressource_ia >= 700 && ressource_ia < 1300) {
										ressource_ia -= 700;
										retour += (float)(ressource_ia / 700 * 2 + (1 - ressource_ia / 700) * 1);
								} else if (ressource_ia >= 1700 && ressource_ia < 2300) {
										ressource_ia -= 1700;
										retour += (float)(ressource_ia / 1700 * 3 + (1 - ressource_ia / 1700) * 2);
								}
								retour = retour / 6 * 100;
						}
            return retour;
        }

    }

}