using UnityEngine;
using ToolbarControl_NS;

namespace AnyRes
{
    [KSPAddon(KSPAddon.Startup.MainMenu, true)]
    public class RegisterToolbar : MonoBehaviour
    {
        void Start()
        {
            ToolbarControl.RegisterMod(AnyRes.MODID, AnyRes.MODNAME);
        }
    }
}