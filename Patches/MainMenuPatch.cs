namespace PulseLib.Patches;

[HarmonyPatch(typeof(scnMenu))]
internal class MainMenuPatch
{
  [HarmonyPatch(nameof(scnMenu.Start))]
  [HarmonyPostfix]
  static void AddModCountLabelPatch()
  {
    Plugin.Logger.LogInfo("Creating mod count label");
    GameObject versionLabelObject = GameObject.Find("/scnMenu/mainMenu/version");
    GameObject modLabelObject = Object.Instantiate(versionLabelObject, versionLabelObject.transform.parent, true);
    modLabelObject.name = "PULSE - mod count";
    Object.Destroy(modLabelObject.transform.GetChild(0));
    Object.Destroy(modLabelObject.GetComponent<RDVersionText>());
    Text modLabel = modLabelObject.GetComponent<Text>();
    modLabel.fontSize = 5;
    modLabel.text = $"Pulse v{MyPluginInfo.PLUGIN_VERSION} / {Chainloader.PluginInfos.Count - 1} mods loaded";
    if (Chainloader.DependencyErrors.Count != 0)
    {
      modLabel.text += $" / {Chainloader.DependencyErrors.Count} mods failed to load";
    }
    modLabelObject.transform.localPosition = new Vector3(-167, -82, 0);
  }
}
