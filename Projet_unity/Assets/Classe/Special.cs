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
    public class Special
    {
        protected string nom { get; set; }
        protected double zone { get; set; }
        protected Boolean isCible { get; set; }
        protected string Type { get; set; }
        protected int Valeur { get; set; }
        protected Boolean isActif { get; set; }
        public Special(String nom, double zone, Boolean isCible, String type, int Valeur, Boolean isActif)
        {
            this.nom = nom;
            this.zone = zone;
            this.isCible = isCible;
            this.Type = type;
            this.Valeur = Valeur;
            this.isActif = isActif;
        }
    }
}

