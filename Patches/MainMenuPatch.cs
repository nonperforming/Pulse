#if BEPINEX5
using BepInEx.Bootstrap;
#elif BEPINEX6
using BepInEx.Unity.Mono.Bootstrap;
#endif

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

#if BEPINEX5
    int pluginsLoaded = Chainloader.PluginInfos.Count - 1;
    int pluginsErrored = Chainloader.DependencyErrors.Count;
#elif BEPINEX6
    int pluginsLoaded = UnityChainloader.Instance.Plugins.Count - 1;
    int pluginsErrored = UnityChainloader.Instance.DependencyErrors.Count;
#endif
    modLabel.text = $"Pulse v{MyPluginInfo.PLUGIN_VERSION} / {pluginsLoaded} mods loaded";
    if (pluginsErrored != 0)
    {
      modLabel.text += $" / {pluginsErrored} mods failed to load";
    }
    modLabelObject.transform.localPosition = new Vector3(-167, -82, 0);
  }
}
