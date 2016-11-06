using UnityEngine;
using System.Collections;
namespace AssemblyCSharp
{
    public static class Score
    {
        public static int[] score_IA = new int[2];
        public static int score_j = 0;
        public static double[] ressource_IA = new double[2];
        public static double[][] ressourcediv_IA = new double[2][];
        // Use this for initialization


        public static void score_ia()
        {
			score_IA [0] = 0;
			score_IA [1] = 0;
            foreach (GameObject game in Niveau.list_element[0])
            {
                score_IA[0] += game.GetComponent<Element>().score;
            }
            foreach (GameObject game in Niveau.list_element[1])
            {
                score_IA[1] += game.GetComponent<Element>().score;
            }

        }


    }
}