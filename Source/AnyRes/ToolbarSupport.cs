/*
	This file is part of AnyRes /L Unleashed
		© 2021 Lisias T : http://lisias.net <support@lisias.net>
		© 2017-2020 LinuxGuruGamer
		© 2015-2016 CriftonM (Crifton Marien)

	AnyRes /L Unleashed is licensed as follows:

		* CC BY-NC-SA 4.0i : https://creativecommons.org/licenses/by-nc-sa/4.0/

	Connected Living Space /L Unleashed is distributed in the hope that
	it will be useful, but WITHOUT ANY WARRANTY; without even the implied
	warranty of	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.

*/
using System.Collections.Generic;

using UnityEngine;
using KSP.UI.Screens;

using KSPe.Annotations;
using Toolbar = KSPe.UI.Toolbar;
using GUI = KSPe.UI.GUI;
using GUILayout = KSPe.UI.GUILayout;

namespace AnyRes
{
	[KSPAddon(KSPAddon.Startup.MainMenu, true)]
	public class ToolbarController : MonoBehaviour
	{
		internal static KSPe.UI.Toolbar.Toolbar Instance => KSPe.UI.Toolbar.Controller.Instance.Get<ToolbarController>();

		[UsedImplicitly]
		private void Start()
		{
			KSPe.UI.Toolbar.Controller.Instance.Register<ToolbarController>(Version.FriendlyName);
		}
	}
}
