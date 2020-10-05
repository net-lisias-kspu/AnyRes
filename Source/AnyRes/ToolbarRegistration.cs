using UnityEngine;
using ToolbarControl_NS;

namespace AnyRes
{
	[KSPAddon(KSPAddon.Startup.MainMenu, true)]
	public class RegisterToolbar : MonoBehaviour
	{
		internal const string MODID = "AnyRes_NS";
		internal const string MODNAME = "AnyRes";

		void Start()
		{
			ToolbarControl.RegisterMod(MODID, MODNAME);
		}
	}
}