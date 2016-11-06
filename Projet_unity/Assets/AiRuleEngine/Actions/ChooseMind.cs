using UnityEngine;
using System.Collections.Generic;
using AssemblyCSharp;
namespace AiRuleEngine
{
#if UNITY_EDITOR
	[ScriptName("ChooseMind")]
	[ScriptCategory("Movement")]
#endif
    public class ChooseMind : BaseAction
    {
        private GameObject Go;
        // Use this for initialization
        void Start()
        {
            this.Go = this.gameObject;
        }

        // Update is called once per frame
        void Update()
        {

        }
        public override bool Execute()
        {
            Variable var = new Variable();
            Go.GetComponent<State>().GetVariable("Agressivite", out var);
            float retour = (float)var.GetValue();
            int race = 0;
            if (Go.GetComponent<Element>().camp == "Ours")
            {
                race = 0;
            }
            else
            {
                race = 1;
            }

            if (Chargement_jeu.buildTurn)
            {

                if (retour < 40)
                {
                    Batiment.aggressivite = retour;
                    Batiment.defense = 75;
                    Batiment.technologie = 25;
                }
                else if (retour < 70 && retour >= 40)
                {
                    Batiment.aggressivite = retour;
                    Batiment.defense = 50;
                    Batiment.technologie = 50;
                }
                else if (retour > 70)
                {
                    Batiment.aggressivite = retour;
                    Batiment.defense = 25;
                    Batiment.technologie = 75;
                }

            }
            Production prod = null;
            Production rech = null;
            if (race == 0)
            {
                Score.ressourcediv_IA[race][0] += Score.ressourcediv_IA[race][1];
                Score.ressourcediv_IA[race][1] -= Score.ressourcediv_IA[race][1];
                if (retour <= 33)
                {
                    if (Score.ressourcediv_IA[race][0] > 350)
                    {
                        prod = new Production("Ours de guerre", this.gameObject);
                    }
                    else if (Score.ressourcediv_IA[race][0] > 200)
                    {
                        prod = new Production("Ours Blanc", this.gameObject);
                    }
                    else if (Score.ressourcediv_IA[race][0] > 50)
                    {
                        prod = new Production("Ourson", this.gameObject);
                    }
                    if (Production.is_done[race][0] && Production.is_done[race][1])
                    {
                        Score.ressourcediv_IA[race][0] += Score.ressourcediv_IA[race][2];
                        Score.ressourcediv_IA[race][2] -= Score.ressourcediv_IA[race][2];
                    }
                    else
                    {
                        if (!Production.is_done[race][1] && Score.ressourcediv_IA[race][2] >= 750)
                        {
                            rech = new Production("Endurance Mystique", this.gameObject);
                            Production.is_done[race][1] = true;
                        }
                        else if (!Production.is_done[race][0] && Score.ressourcediv_IA[race][2] >= 500)
                        {
                            rech = new Production("Griffes Enchantees", this.gameObject);
                            Production.is_done[race][0] = true;
                        }
                    }
                }
                else if (retour > 33 && retour < 40)
                {
                    if (Score.ressourcediv_IA[race][0] > 350)
                    {
                        prod = new Production("Ours de guerre", this.gameObject);
                    }
                    else if (Score.ressourcediv_IA[race][0] > 200)
                    {
                        prod = new Production("Ours Blanc", this.gameObject);
                    }
                    else if (Score.ressourcediv_IA[race][0] > 50)
                    {
                        prod = new Production("Ourson", this.gameObject);
                    }
                    if (Production.is_done[race][0] && Production.is_done[race][1])
                    {
                        Score.ressourcediv_IA[race][0] += Score.ressourcediv_IA[race][2];
                        Score.ressourcediv_IA[race][2] -= Score.ressourcediv_IA[race][2];
                    }
                    else
                    {
                        if (!Production.is_done[race][1] && Score.ressourcediv_IA[race][2] >= 750)
                        {
                            rech = new Production("Endurance Mystique", this.gameObject);
                            Production.is_done[race][1] = true;
                        }
                        else if (!Production.is_done[race][0] && Score.ressourcediv_IA[race][2] >= 500)
                        {
                            rech = new Production("Griffes Enchantees", this.gameObject);
                            Production.is_done[race][0] = true;
                        }
                    }
                }
                else if (retour >= 40 && retour < 70)
                {
                    if (Score.ressourcediv_IA[race][0] > 200)
                    {
                        prod = new Production("Ours Blanc", this.gameObject);
                    }
                    else if (Score.ressourcediv_IA[race][0] > 350)
                    {
                        prod = new Production("Ours de guerre", this.gameObject);
                    }
                    if (Score.ressourcediv_IA[race][2] <= 750)
                    {
                        if (Production.is_done[race][0] && Production.is_done[race][1])
                        {
                            Score.ressourcediv_IA[race][0] += Score.ressourcediv_IA[race][2];
                            Score.ressourcediv_IA[race][2] -= Score.ressourcediv_IA[race][2];

                        }
                        else
                        {
							if (!Production.is_done[race][1]&& Score.ressourcediv_IA[race][2] >= 750)
                            {
                                rech = new Production("Endurance Mystique", this.gameObject);
                                Production.is_done[race][1] = true;
                            }
							else if (!Production.is_done[race][0]&& Score.ressourcediv_IA[race][2] >= 500)
                            {
                                rech = new Production("Griffes Enchantees", this.gameObject);
                                Production.is_done[race][0] = true;
                            }
                        }
                    }
                }
                else if (retour >= 70)
                {
                    if (Score.ressourcediv_IA[race][0] > 350)
                    {
                        prod = new Production("Ours de guerre", this.gameObject);
                    }
                    if (Score.ressourcediv_IA[race][2] <= 750)
                    {
                        if (Production.is_done[race][0] && Production.is_done[race][1])
                        {
                            Score.ressourcediv_IA[race][0] += Score.ressourcediv_IA[race][2];
                            Score.ressourcediv_IA[race][2] -= Score.ressourcediv_IA[race][2];
                        }
                        else
                        {
                            if (!Production.is_done[race][1] && Score.ressourcediv_IA[race][2] >= 750)
                            {
                                rech = new Production("Endurance Mystique", this.gameObject);
                                Production.is_done[race][1] = true;
                            }
                            else if (!Production.is_done[race][0] && Score.ressourcediv_IA[race][2] >= 500)
                            {
                                rech = new Production("Griffes Enchantees", this.gameObject);
                                Production.is_done[race][0] = true;
                            }
                        }
                    }
                }
            }
            else if (race == 1)
            {
                Score.ressourcediv_IA[race][0] += Score.ressourcediv_IA[race][1];
				Score.ressourcediv_IA[race][1] -= Score.ressourcediv_IA[race][1];
                if (retour < 33)
                {
                    if (Score.ressourcediv_IA[race][0] <= 50)
                    {
                        prod = new Production("Poulpy", this.gameObject);
                    }
                    else if (Score.ressourcediv_IA[race][0] <= 200)
                    {
                        prod = new Production("Octopus", this.gameObject);
                    }
                    else if (Score.ressourcediv_IA[race][0] <= 350)
                    {
                        prod = new Production("Cyber-Poulpe", this.gameObject);
                    }
                    if (Production.is_done[race][0] && Production.is_done[race][1])
                    {
                        Score.ressourcediv_IA[race][0] += Score.ressourcediv_IA[race][2];
                        Score.ressourcediv_IA[race][2] -= Score.ressourcediv_IA[race][2];

                    }
                    else
                    {
                        if (!Production.is_done[race][1] && Score.ressourcediv_IA[race][2] >= 750)
                        {
                            rech = new Production("Plasma Defensif", this.gameObject);
                            Production.is_done[race][1] = true;
                        }
                        else if (!Production.is_done[race][0] && Score.ressourcediv_IA[race][2] >= 500)
                        {
                            rech = new Production("Ventouses Laser", this.gameObject);
                            Production.is_done[race][0] = true;
                        }
                    }

                }
                else if (retour >= 33 && retour < 40)
                {
                    if (Score.ressourcediv_IA[race][0] > 350)
                    {
                        prod = new Production("Cyber-Poulpe", this.gameObject);
                    }
                    else if (Score.ressourcediv_IA[race][0] > 200)
                    {
                        prod = new Production("Octopus", this.gameObject);
                    }
                    else if (Score.ressourcediv_IA[race][0] > 50)
                    {
                        prod = new Production("Poulpy", this.gameObject);
                    }
                    if (Production.is_done[race][0] && Production.is_done[race][1])
                    {
                        Score.ressourcediv_IA[race][0] += Score.ressourcediv_IA[race][2];
                        Score.ressourcediv_IA[race][2] -= Score.ressourcediv_IA[race][2];

                    }
                    else
                    {
                        if (!Production.is_done[race][1] && Score.ressourcediv_IA[race][2] >= 750)
                        {
                            rech = new Production("Plasma Defensif", this.gameObject);
                            Production.is_done[race][1] = true;
                        }
                        else if (!Production.is_done[race][0] && Score.ressourcediv_IA[race][2] >= 500)
                        {
                            rech = new Production("Ventouses Laser", this.gameObject);
                            Production.is_done[race][0] = true;
                        }
                    }
                }
                else if (retour >= 40 && retour < 70)
                {
                    if (Score.ressourcediv_IA[race][0] > 200)
                    {
                        prod = new Production("Octopus", this.gameObject);
                    }
                    else if (Score.ressourcediv_IA[race][0] > 350)
                    {
                        prod = new Production("Cyber-Poulpe", this.gameObject);
                    }
                    if (Production.is_done[race][0] && Production.is_done[race][1])
                    {
                        Score.ressourcediv_IA[race][0] += Score.ressourcediv_IA[race][2];
                        Score.ressourcediv_IA[race][2] -= Score.ressourcediv_IA[race][2];

                    }
                    else
                    {
                        if (!Production.is_done[race][1] && Score.ressourcediv_IA[race][2] >= 750)
                        {
                            rech = new Production("Plasma Defensif", this.gameObject);
                            Production.is_done[race][1] = true;
                        }
                        else if (!Production.is_done[race][0] && Score.ressourcediv_IA[race][2] >= 500)
                        {
                            rech = new Production("Ventouses Laser", this.gameObject);
                            Production.is_done[race][0] = true;
                        }
                    }
                }
                else if (retour >= 70)
                {
                    if (Score.ressourcediv_IA[race][0] > 350)
                    {
                        prod = new Production("Cyber-Poulpe", this.gameObject);
                    }
                    if (Score.ressourcediv_IA[race][2] <= 750)
                    {
                        if (Production.is_done[race][0] && Production.is_done[race][1])
                        {
                            Score.ressourcediv_IA[race][0] += Score.ressourcediv_IA[race][2];
                            Score.ressourcediv_IA[race][2] -= Score.ressourcediv_IA[race][2];

                        }
                        else
                        {
                            if (!Production.is_done[race][1])
                            {
                                rech = new Production("Plasma Defensif", this.gameObject);
								Production.is_done[race][1] = true;
                            }
                            else
                            {
                                rech = new Production("Ventouses Laser", this.gameObject);
								Production.is_done[race][0] = true;
                            }
                        }
                    }
                }
            }
            if (prod != null)
            {
                Score.ressourcediv_IA[race][0] -= prod.cout;

                prod.launchProduce();
            }
            if (rech != null)
            {
                Score.ressourcediv_IA[race][2] -= rech.cout;

                rech.launchProduce();
            }
            return true;

        }

    }
}
