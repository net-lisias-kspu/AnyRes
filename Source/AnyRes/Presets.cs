using System.Collections.Generic;

using UnityEngine;

using File = KSPe.IO.File<AnyRes.Startup>;
using Asset = KSPe.IO.Asset<AnyRes.Startup>;
using Data = KSPe.IO.Data<AnyRes.Startup>;

namespace AnyRes.Util
{

	public class Presets : MonoBehaviour
	{
		internal readonly List<Data.ConfigNode> files = new List<Data.ConfigNode>();

		private Data.ConfigNode deleteFile = null;

		void Start () {
			Log.detail("Presets Starting");

			if (!System.IO.Directory.Exists(File.Data.Solve("presets")))
			{
				string[] files = File.Asset.List("*.cfg", false, "presets");
				foreach (string f in files)
				{
					Asset.ConfigNode source = Asset.ConfigNode.For(null, "presets", f).Load();
					Data.ConfigNode target = Data.ConfigNode.For(null, "presets", f);
					target.Save(source.Node);
					this.files.Add(target);
				}
			}
			else
				this.ReloadFiles();
			Log.detail("Presets Started");
		}

		internal void ReloadFiles()
		{
            Log.detail("Presets reloading...");
			this.files.Clear();
			string[] files = File.Data.List("*.cfg", false, "presets");
			foreach (string f in files)
			{
				Log.detail(f);
				Data.ConfigNode configNode = Data.ConfigNode.For(null, "presets", f).Load();
				this.files.Add(configNode);
			}
            Log.detail("Presets reloaded {0}", this.files.Count);
		}

		internal void MarkForDeletion(Data.ConfigNode configNode)
		{
			this.deleteFile = configNode;
		}

		internal string GetVictimName()
		{
			return null == this.deleteFile
				? "<NULL>"
				: this.deleteFile.Node.GetValue("name")
			;
		}

		internal void Commit()
		{
            Log.detail("Preset removing {0}", deleteFile.Node.GetValues("name"), this.files.Count);
			this.files.Remove(this.deleteFile);
			this.deleteFile.Destroy();
			this.deleteFile = null;
            Log.detail("Preset removed. {0} left", this.files.Count);
		}

		internal void Create(ConfigNode config)
		{
			Data.ConfigNode configNode = Data.ConfigNode.For(null, "presets", config.GetValues("name") + ".cfg");
			configNode.Save(config);
			this.files.Add(configNode);
			Log.detail("Preset created {0}, {1} total", config.GetValues("name"), this.files.Count);
		}
	}
}
