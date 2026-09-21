using System;
using System.Collections.Generic;
using System.Reflection;
using Anchor.Panel;
using BepInEx;
using UnityEngine.UI;
using UnityExplorer.UI;
using UnityExplorer.UI.Panels;
using UniverseLib.UI;

namespace Anchor
{
    [BepInPlugin(Constants.GUID, Constants.Name, Constants.Version)]
    public class Plugin : BaseUnityPlugin
    {
        private AnchorPanel panel;
        private bool initialized;

        private void Update()
        {
            if (initialized || UIManager.Initializing)
                return;

            try
            {
                if (UIManager.NavbarTabButtonHolder == null)
                    return;

                // The following is my amazing attempt at making the "Anchor" button show at the top with the others I did refrence multiple parts of UE to make this work.

                HorizontalLayoutGroup layout = UIManager.NavbarTabButtonHolder.GetComponent<HorizontalLayoutGroup>();
                layout.SetChildControlWidth(false);

                UIManager.NavbarTabButtonHolder.transform.parent.Find("CloseButton").gameObject.SetActive(false);
                PropertyInfo PI = typeof(UIManager).GetProperty("UiBase", BindingFlags.Static | BindingFlags.NonPublic);

                UIBase UI = PI.GetValue(null) as UIBase;
                panel = new AnchorPanel(UI);

                FieldInfo FI = typeof(UIManager).GetField("UIPanels", BindingFlags.Static | BindingFlags.NonPublic);

                var panels = FI.GetValue(null) as Dictionary<UIManager.Panels, UEPanel>;
                panels.Add((UIManager.Panels)Constants.Anchor, panel);

                initialized = true;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
            }
        }

    }
}
