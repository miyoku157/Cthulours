using System.Collections;
using UnityEngine;

namespace AiRuleEngine
{
#if UNITY_EDITOR
	[ScriptName("Base Action")]
	[ScriptCategory("Base")]
#endif
	public abstract class BaseAction : BaseScript
    {
        public abstract bool Execute();
	}
}