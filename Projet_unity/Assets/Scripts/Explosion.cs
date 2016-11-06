using UnityEngine;
using System.Collections;
using System;
using AssemblyCSharp;

public class Explosion : MonoBehaviour
{
    private float X, Y, subX, subY;
    private GameObject explosion;
    private GameObject explosionFX;
    private GameObject cross;
    private Element element;
    private GameObject[] subCross;
    private SpriteRenderer spriteR;
    private bool Boom;
    private int nbCross;

    public static int zone;
    public static double degat;

    void Start()
    {
        nbCross = 0;

        for (int i = 0; i < zone; i++)
        {
            nbCross += 4 * ((i + 1) * 1);
        }

        subCross = new GameObject[nbCross];
    }

    void Awake()
    {
        Boom = false;
    }

    void Update()
    {
        if(!Boom)
        {
			if(Application.platform == RuntimePlatform.Android)
			{
				if(Input.GetMouseButtonDown(0))
				{
					getParameters();
					
					if(cross == null)
					{
						int compteurCross = 0;
						
						cross = new GameObject("Cross");
						cross.transform.position = new Vector2(X + 0.5f, Y + 0.5f);
						cross.transform.localScale = new Vector2(0.5f, 0.5f);
						cross.transform.parent = gameObject.transform;
						
						spriteR = cross.AddComponent<SpriteRenderer>();
						spriteR.sprite = Resources.Load<Sprite>("Textures/Effets/marqueur_centre_capa");
						spriteR.sortingLayerName = "Tir";
						
						for (int j = -zone; j < zone + 1; j++)
						{
							for (int h = -zone; h < zone + 1; h++)
							{
								if (!((j + X == X) && (h + Y == Y)) &&
								    j + h > -zone - 1 &&
								    j + h < zone + 1 &&
								    j - h > -zone - 1 &&
								    j - h < zone + 1)
								{
									subCross[compteurCross] = new GameObject("Cross");
									subCross[compteurCross].transform.parent = cross.transform;
									subCross[compteurCross].transform.localScale = new Vector2(1f, 1f);
									subCross[compteurCross].transform.position = new Vector2(X + 0.5f + j, Y + 0.5f + h);
									
									spriteR = subCross[compteurCross].AddComponent<SpriteRenderer>();
									spriteR.sprite = Resources.Load<Sprite>("Textures/Effets/marqueur_zone_capa");
									spriteR.sortingLayerName = "Tir";
									
									compteurCross++;
								}
							}
						}
					}
					else
					{
						getParameters();

						if(X == cross.transform.position.x + 0.5f &&
						   Y == cross.transform.position.y + 0.5f)
						{
							explosion = Instantiate(Resources.Load("Prefab/Effets/Explosion")) as GameObject;
							explosion.transform.parent = gameObject.GetComponent<Transform>();
							explosionFX = Instantiate(Resources.Load("Prefab/Effets/ExplosionFX")) as GameObject;
							explosionFX.transform.position = cross.transform.position;
							explosionFX.transform.parent = explosion.transform;
							Destroy(explosion, explosion.GetComponent<AudioSource>().GetComponent<AudioSource>().clip.length);
							Destroy(cross);
							
							if (Niveau.grille[(int)X, (int)Y].GetComponent<Case>().occupe &&
							    Niveau.grille[(int)X, (int)Y].GetComponent<Case>().element != null)
							{
								element = Niveau.grille[(int)X, (int)Y].GetComponent<Case>().element.GetComponent<Element>();
								element.PV -= degat;
							}
							
							for (int i = 0; i < nbCross; i++)
							{
								subX = subCross[i].GetComponent<Transform>().position.x;
								subY = subCross[i].GetComponent<Transform>().position.y;
								explosionFX = Instantiate(Resources.Load("Prefab/Effets/ExplosionFX")) as GameObject;
								explosionFX.transform.position = new Vector2(subX, subY);
								explosionFX.transform.parent = explosion.transform;
								
								if (Niveau.grille[(int)subX, (int)subY])
								{
									if (Niveau.grille[(int)subX, (int)subY].GetComponent<Case>().occupe &&
									    Niveau.grille[(int)subX, (int)subY].GetComponent<Case>().element != null)
									{
										element = Niveau.grille[(int)subX, (int)subY].GetComponent<Case>().element.GetComponent<Element>();
										element.PV -= degat;
									}
								}
							}
							Destroy(this, explosion.GetComponent<AudioSource>().GetComponent<AudioSource>().clip.length);
							Pouvoir.Launch("Exp");
							Boom = true;
						}
						else
						{
							cross.transform.position = new Vector2(X + 0.5f, Y + 0.5f);
						}

						if (Input.GetKeyDown(KeyCode.Escape))
						{
							if (cross != null)
								Destroy(cross);
							Destroy(this);
							Boom = true;
						}
					}
				}
			}
			else
			{
				getParameters();
				
				if(cross == null)
				{
					int compteurCross = 0;
					
					cross = new GameObject("Cross");
					cross.transform.position = new Vector2(X + 0.5f, Y + 0.5f);
					cross.transform.localScale = new Vector2(0.5f, 0.5f);
					cross.transform.parent = gameObject.transform;
					
					spriteR = cross.AddComponent<SpriteRenderer>();
					spriteR.sprite = Resources.Load<Sprite>("Textures/Effets/marqueur_centre_capa");
					spriteR.sortingLayerName = "Tir";
					
					for (int j = -zone; j < zone + 1; j++)
					{
						for (int h = -zone; h < zone + 1; h++)
						{
							if (!((j + X == X) && (h + Y == Y)) &&
							    j + h > -zone - 1 &&
							    j + h < zone + 1 &&
							    j - h > -zone - 1 &&
							    j - h < zone + 1)
							{
								subCross[compteurCross] = new GameObject("Cross");
								subCross[compteurCross].transform.parent = cross.transform;
								subCross[compteurCross].transform.localScale = new Vector2(1f, 1f);
								subCross[compteurCross].transform.position = new Vector2(X + 0.5f + j, Y + 0.5f + h);
								
								spriteR = subCross[compteurCross].AddComponent<SpriteRenderer>();
								spriteR.sprite = Resources.Load<Sprite>("Textures/Effets/marqueur_zone_capa");
								spriteR.sortingLayerName = "Tir";
								
								compteurCross++;
							}
						}
					}
				}
				else
				{
					cross.transform.position = new Vector2(X + 0.5f, Y + 0.5f);
				}
				
				if (Input.GetMouseButtonDown(0) && cross != null)
				{
					explosion = Instantiate(Resources.Load("Prefab/Effets/Explosion")) as GameObject;
					explosion.transform.parent = gameObject.GetComponent<Transform>();
					explosionFX = Instantiate(Resources.Load("Prefab/Effets/ExplosionFX")) as GameObject;
					explosionFX.transform.position = cross.transform.position;
					explosionFX.transform.parent = explosion.transform;
					Destroy(explosion, explosion.GetComponent<AudioSource>().GetComponent<AudioSource>().clip.length);
					Destroy(cross);
					
					if (Niveau.grille[(int)X, (int)Y].GetComponent<Case>().occupe &&
					    Niveau.grille[(int)X, (int)Y].GetComponent<Case>().element != null)
					{
						element = Niveau.grille[(int)X, (int)Y].GetComponent<Case>().element.GetComponent<Element>();
						element.PV -= degat;
					}
					
					for (int i = 0; i < nbCross; i++)
					{
						subX = subCross[i].GetComponent<Transform>().position.x;
						subY = subCross[i].GetComponent<Transform>().position.y;
						explosionFX = Instantiate(Resources.Load("Prefab/Effets/ExplosionFX")) as GameObject;
						explosionFX.transform.position = new Vector2(subX, subY);
						explosionFX.transform.parent = explosion.transform;
						
						if (Niveau.grille[(int)subX, (int)subY])
						{
							if (Niveau.grille[(int)subX, (int)subY].GetComponent<Case>().occupe &&
							    Niveau.grille[(int)subX, (int)subY].GetComponent<Case>().element != null)
							{
								element = Niveau.grille[(int)subX, (int)subY].GetComponent<Case>().element.GetComponent<Element>();
								element.PV -= degat;
							}
						}
					}
					Destroy(this, explosion.GetComponent<AudioSource>().GetComponent<AudioSource>().clip.length);
					Pouvoir.Launch("Exp");
					Boom = true;
				}
				
				if (Input.GetKeyDown(KeyCode.Escape))
				{
					if (cross != null)
						Destroy(cross);
					Destroy(this);
					Boom = true;
				}
			}
        }
    }

    public void getParameters()
    {
        RaycastHit vHit = new RaycastHit();
        Ray vRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(vRay, out vHit, 50))
        {
            X = vHit.point.x;
            if (X < Math.Truncate(X))
            {
                X = (float)Math.Truncate(X) - 1;
            }
            else
            {
                X = (float)Math.Truncate(X);
            }

            Y = vHit.point.y;
            if (Y < Math.Truncate(Y))
            {
                Y = (float)Math.Truncate(Y) - 1;
            }
            else
            {
                Y = (float)Math.Truncate(Y);
            }
        }
    }
}
