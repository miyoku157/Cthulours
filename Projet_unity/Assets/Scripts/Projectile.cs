using UnityEngine;
using System.Collections;
using AssemblyCSharp;
public class Projectile : MonoBehaviour
{
    private float x { get; set; }
    private float y { get; set; }
    private float x_enn { get; set; }
    private float y_enn { get; set; }
    private float dist_two_object { get; set; }
    private double degat { get; set; }
    private double redu_armure { get; set; }
    private string Type { get; set; }
    private Vector3 pos_enn;
    private GameObject Tireur { get; set; }

    void Start()
    {
        transform.position = new Vector2(x, y);
        Case cas = Niveau.grille[(int)(x - 0.5), (int)(y - 0.5)].GetComponent<Case>();
        Tireur = cas.element;
        pos_enn = new Vector3(x_enn, y_enn);
    }

    void Update()
    {
        Physics2D.IgnoreLayerCollision(1, 4);
        if (Tireur != null)
        {
            Physics2D.IgnoreCollision(Tireur.GetComponent<BoxCollider2D>(), this.GetComponent<Collider2D>());
        }
        transform.position = Vector3.MoveTowards(transform.position, pos_enn, 15 * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        GameObject objet = coll.gameObject;
        Element ele = objet.GetComponent<Element>();

        if(Tireur != null && ele.camp != Tireur.GetComponent<Element>().camp)
        {
            ele.PV -= degat;

            if (ele.PV <= 0)
            {
				ele.destroy();
                Unite uni = Tireur.GetComponent<Unite>();
                uni.enn_pos.Remove(dist_two_object);
                Debug.Log("Un élément " + ele.camp + " a été détruit par un élément " + Tireur.GetComponent<Element>().camp);
            }

            GameObject.Destroy(this.gameObject);
        }
    }

    public void ini(float _x, float _y, float _x_enn, float _y_enn, float _dist_two_object,
                    double _degat, double _redu_armure, string _type)
    {
        x = _x;
        y = _y;
        x_enn = _x_enn;
        y_enn = _y_enn;
        dist_two_object = _dist_two_object;
        degat = _degat;
        redu_armure = _redu_armure;
        Type = _type;
    }
}

