//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.34014
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
namespace AssemblyCSharp
{
    public class Obstacle : Element
    {
        void OnDestroy()
        {
            float X, Y;
            X = transform.position.x - 0.5f;
            Y = transform.position.y - 0.5f;

            Niveau.grille[(int)X, (int)Y].GetComponent<Case>().occupe = false;
            Niveau.grille[(int)X, (int)Y].GetComponent<Case>().element = null;
        }
    }
}

