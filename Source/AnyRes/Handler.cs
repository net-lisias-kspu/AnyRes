﻿using System;
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;  //Get Regex
using KSP.UI.Screens;
using ToolbarControl_NS;
using ClickThroughFix;

namespace AnyRes
{

	[KSPAddon(KSPAddon.Startup.AllGameScenes, false)]
	public class AnyRes : MonoBehaviour
	{

		public static Rect anyresWinRect = new Rect(35, 99, 400, 275);
        public Rect deleteRect = new Rect((Screen.width - 200) / 2, (Screen.height - 100) / 2, 200, 100);

        public string nameString = "";
		public string xString = "1280";
		public string yString = "720";

		public int x = 1280;
		public int y = 720;

		public bool windowEnabled = false;
		public bool fullScreen = true;
		public bool reloadScene = false;

        ToolbarControl toolbarControl;

        string[] files;
        string file = "";
        string deleteFile = "";
        string deleteFileName = "";
        Vector2 scrollViewPos;
        bool deleteEnabled = false;
        bool confirmDeleteEnabled = false;


        void Start()
        {
#if false
            if (HighLogic.LoadedScene == GameScenes.SETTINGS)
            {

                windowEnabled = true;
                anyresWinRect.x = 7;
                anyresWinRect.y = 231;

            }
            else 
#endif
            if (HighLogic.LoadedScene == GameScenes.EDITOR)
            {

                anyresWinRect.x = Screen.width - 272;
                anyresWinRect.y = Screen.height - 231;

            }

            Debug.Log ("[AnyRes] Loaded, scene: " + HighLogic.LoadedScene);

            //Debug.Log("[AnyRes] 1");

            xString = GameSettings.SCREEN_RESOLUTION_WIDTH.ToString ();
			yString = GameSettings.SCREEN_RESOLUTION_HEIGHT.ToString ();
			fullScreen = GameSettings.FULLSCREEN;
            //Debug.Log("[AnyRes] 2");


            //DontDestroyOnLoad(this);
            UpdateFilesList();

           if (HighLogic.LoadedScene == GameScenes.SPACECENTER ||
                HighLogic.LoadedScene == GameScenes.EDITOR ||
                HighLogic.LoadedScene == GameScenes.FLIGHT ||
                HighLogic.LoadedScene == GameScenes.TRACKSTATION)
            {
                Log.Info("SceneLoaded, scene: " + HighLogic.LoadedScene);
                toolbarControl = gameObject.AddComponent<ToolbarControl>();
                toolbarControl.AddToAllToolbars(OnTrue, OnFalse,
                          ApplicationLauncher.AppScenes.FLIGHT | ApplicationLauncher.AppScenes.MAPVIEW |
                          ApplicationLauncher.AppScenes.SPACECENTER | ApplicationLauncher.AppScenes.SPH |
                          ApplicationLauncher.AppScenes.TRACKSTATION | ApplicationLauncher.AppScenes.VAB,
                          MODID,
                          "AnyResButton",
                          "AnyRes/textures/Toolbar_32",
                          "AnyRes/textures/Toolbar_24",
                          MODNAME);

            }
        }
        internal const string MODID = "AnyRes_NS";
        internal const string MODNAME = "AnyRes";

        void OnTrue()
        {
            windowEnabled = true;
        }
        void OnFalse()
        {
            windowEnabled = false;
        }
        public void OnDisable ()
		{
            OnDestroy();
        }
        public void OnDestroy()
        {

            if (toolbarControl != null)
            {
                toolbarControl.OnDestroy();
                Destroy(toolbarControl);
            }
        }

        void Update() {

            //Thanks bananashavings http://forum.kerbalspaceprogram.com/index.php?/profile/156147-bananashavings/ - https://gist.github.com/bananashavings/e698f4359e1628b5d6ef
            //Also thanks to Crzyrndm for the fix to that code!
            //(HighLogic.LoadedScene == GameScenes.TRACKSTATION || HighLogic.LoadedScene == GameScenes.SPACECENTER || HighLogic.LoadedScene == GameScenes.FLIGHT || HighLogic.LoadedScene == GameScenes.EDITOR)

            if ((GameSettings.MODIFIER_KEY.GetKey() ) && Input.GetKeyDown (KeyCode.Slash)) {

				windowEnabled = !windowEnabled;
				if (ApplicationLauncher.Ready) {

					if (toolbarControl != null){
						if (windowEnabled) {

                            toolbarControl.SetTrue (true);

						} else {

                            toolbarControl.SetFalse (true);

						}
					}

				}

			}

            //			Meant for debugging
            //			Debug.Log ("X: " + anyresWinRect.x.ToString ());
            //			Debug.Log ("Y: " + anyresWinRect.y.ToString ());


        }

		void OnGUI()
        {
            if (toolbarControl != null)
            {
                if (HighLogic.CurrentGame.Parameters.CustomParams<AR>().useKSPSkin)
                    GUI.skin = HighLogic.Skin;
            }
			if (windowEnabled)
            {

                if (anyresWinRect.x + anyresWinRect.width > Screen.width)
                    anyresWinRect.x = Screen.width - anyresWinRect.width ;
                if (anyresWinRect.y + anyresWinRect.height > Screen.height)
                    anyresWinRect.y = Screen.height -anyresWinRect.height;


                anyresWinRect.x = Math.Max(anyresWinRect.x, 0);
                anyresWinRect.y = Math.Max(anyresWinRect.y, 0);

                anyresWinRect = ClickThruBlocker.GUIWindow (09271, anyresWinRect, GUIActive, "AnyRes");

			}
            if (confirmDeleteEnabled)
                deleteRect = ClickThruBlocker.GUIWindow(09276, deleteRect, ConfirmDelete, "Confirm");
        }

		void GUIActive(int windowID) {
#if false
            if (HighLogic.LoadedScene == GameScenes.SETTINGS) {

				GUI.BringWindowToFront (09271);

			}
#endif
            GUILayout.BeginHorizontal();

            GUILayout.BeginVertical ();
            GUILayout.BeginHorizontal();
            GUILayout.Label("Name: ");
            nameString = GUILayout.TextField(nameString);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal ();
			GUILayout.Label ("Width: ");
			xString = GUILayout.TextField (xString);
			xString = Regex.Replace (xString, @"[^0-9]", "");
			GUILayout.EndHorizontal ();
			GUILayout.BeginHorizontal ();
			GUILayout.Label ("Height: ");
			yString = GUILayout.TextField (yString);
			yString = Regex.Replace (yString, @"[^0-9]", "");
			GUILayout.EndHorizontal ();
			fullScreen = GUILayout.Toggle (fullScreen, "Fullscreen");
//			reloadScene = GUILayout.Toggle (reloadScene, "Reload scene");
			if (GUILayout.Button("Set Screen Resolution")) {

				if (xString != null && yString != null) {

					x = Convert.ToInt32(xString);
					y = Convert.ToInt32(yString);

					if (x > 0 && y > 0) {

						GameSettings.SCREEN_RESOLUTION_HEIGHT = y;
						GameSettings.SCREEN_RESOLUTION_WIDTH = x;
						GameSettings.FULLSCREEN = fullScreen;
						GameSettings.SaveSettings ();
						Screen.SetResolution(x, y, fullScreen);
						Debug.Log ("[AnyRes] Set screen resolution");
#if false
                        if (reloadScene) {

							if (HighLogic.LoadedScene != GameScenes.LOADING) {
								HighLogic.LoadScene (HighLogic.LoadedScene);
							} else {

								ScreenMessages.PostScreenMessage("You cannot reload the scene while loading the game!", 1);

							}

						}
#endif
					} else {

						ScreenMessages.PostScreenMessage("One or both of your values is too small.  Please enter a valid value.", 1, ScreenMessageStyle.UPPER_CENTER);

					}

				} else {

					ScreenMessages.PostScreenMessage("The values you have set are invalid.  Please set a valid value.", 1, ScreenMessageStyle.UPPER_CENTER);

				}

			}

            if (nameString == "")
                GUI.enabled = false;
            if (GUILayout.Button("Save"))
            {
                var newName = nameString;
                var newX = xString;
                var newY = yString;
                var newFullscreen = fullScreen;
                ConfigNode config = new ConfigNode(newName);
                config.AddValue("name", newName);
                config.AddValue("x", newX);
                config.AddValue("y", newY);
                config.AddValue("fullscreen", newFullscreen.ToString());
                config.Save(KSPUtil.ApplicationRootPath.Replace("\\", "/") + "GameData/AnyRes/PluginData/" + newName + ".cfg");

                ScreenMessages.PostScreenMessage("Preset saved.  You can change the preset later by using the same name in this editor.", 5, ScreenMessageStyle.UPPER_CENTER);
                UpdateFilesList();

            }


            if (files.Length == 0)
                GUI.enabled = false;
            else
                GUI.enabled = true;
            if (deleteEnabled)
            {
                if (GUILayout.Button("Disable Delete"))
                {
                    deleteEnabled = false;
                }

            }
            else
            {
                if (GUILayout.Button("Enable Delete"))
                {
                    deleteEnabled = true;
                }
            }
            if (GUILayout.Button("Close"))
            {
                toolbarControl.SetFalse(true);
            }
            GUILayout.EndVertical();
            GUILayout.BeginVertical();
         
            scrollViewPos = GUILayout.BeginScrollView(scrollViewPos);
            for (int i = files.Length - 1; i >= 0; --i)
            {

                file = files[i];

                ConfigNode config = ConfigNode.Load(file);
                if (deleteEnabled)
                {
                    if (GUILayout.Button("Delete " + config.GetValue("name")))
                    {
                        confirmDeleteEnabled = true;
                        deleteFile = file;
                        deleteFileName = config.GetValue("name");
                    }
                   
                }
                else
                {
                    if (GUILayout.Button(config.GetValue("name")))
                    {

                        int xVal;
                        int.TryParse(config.GetValue("x"), out xVal);
                        int yVal;
                        int.TryParse(config.GetValue("y"), out yVal);
                        bool fullscreen;
                        bool.TryParse(config.GetValue("fullscreen"), out fullscreen);
                        GameSettings.SCREEN_RESOLUTION_HEIGHT = yVal;
                        GameSettings.SCREEN_RESOLUTION_WIDTH = xVal;
                        GameSettings.FULLSCREEN = fullscreen;
                        GameSettings.SaveSettings();
                        Screen.SetResolution(xVal, yVal, fullscreen);
                        Debug.Log("[AnyRes] Set screen resolution from preset");

                    }
                }
            }
            GUILayout.EndScrollView();
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();

            if (GUI.Button(new Rect(anyresWinRect.width - 18, 3f, 15f, 15f), new GUIContent("X")))
            {
                toolbarControl.SetFalse(true);
            }

            GUI.DragWindow ();

		}
        void ConfirmDelete(int id)
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label("Confirm delete of " + deleteFileName);
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Cancel"))
            {
                deleteEnabled = false;
                confirmDeleteEnabled = false;
            }
            if (GUILayout.Button("Yes"))
            {

                //deleteEnabled = false;
                confirmDeleteEnabled = false;
                System.IO.File.Delete(deleteFile);
                UpdateFilesList();
            }
            GUILayout.EndHorizontal();
        }

        void UpdateFilesList()
        {
            files = Directory.GetFiles(KSPUtil.ApplicationRootPath.Replace("\\", "/") + "GameData/AnyRes/PluginData/", "*.cfg");
        }

    }
   
}

