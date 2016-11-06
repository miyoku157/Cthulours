using System.Collections;
using UnityEngine;

namespace AiRuleEngine
{
#if UNITY_EDITOR
	[ScriptName("Base Sensor")]
	[ScriptCategory("Base")]
	[ScriptReturnTypeAttribute("object")]
#endif
	public abstract class BaseSensor : BaseScript
	{
		public abstract object Execute(System.Type type);
	}
}