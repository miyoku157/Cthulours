using UnityEngine;
using System.Collections;
namespace AiRuleEngine
{
#if UNITY_EDITOR
	[ScriptName("nb_ennemie")]
	[ScriptCategory("Geography")]
	[ScriptReturnTypeAttribute("int")]
#endif
    public class nb_ennemie : BaseSensor
    {
        public int nbennemie;
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        public override object Execute(System.Type type)
        {
            return nbennemie;
        }
    }
}
