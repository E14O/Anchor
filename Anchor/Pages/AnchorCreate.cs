using System;
// Used: https://github.com/sinai-dev/UnityExplorer/blob/master/src/ObjectExplorer/ObjectSearch.cs
using System.Collections.Generic;
using System.Text;
using Anchor.Panel;
using UnityEngine;
using UnityExplorer.UI.Panels;
using UniverseLib.UI;
using UniverseLib.UI.Models;

namespace Anchor.Pages
{
    public class AnchorCreate : UIModel
    {
        public AnchorPanel Parent { get; }
        public override GameObject UIRoot => uiRoot;
        public GameObject uiRoot;

        public AnchorCreate(AnchorPanel parent) => Parent = parent;

        public override void ConstructUI(GameObject parent)
        {
            uiRoot = UIFactory.CreateVerticalGroup(parent, "Anchor Create", true, true, true, true, 2, new Vector4(2, 2, 2, 2));
            UIFactory.SetLayoutElement(uiRoot, flexibleHeight: 9999);
        }
    }
}
