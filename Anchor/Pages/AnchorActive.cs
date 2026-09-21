// Used: https://github.com/sinai-dev/UnityExplorer/blob/master/src/ObjectExplorer/ObjectSearch.cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Anchor.Panel;
using Anchor.Tools;
using UnityEngine;
using UnityExplorer.ObjectExplorer;
using UniverseLib.UI;
using UniverseLib.UI.Models;
using UniverseLib.UI.Widgets.ButtonList;
using UniverseLib.UI.Widgets.ScrollView;

namespace Anchor.Pages
{
    public class AnchorActive : UIModel
    {
        public AnchorPanel Parent { get; }
        public override GameObject UIRoot => uiRoot;
        public GameObject uiRoot;
        public AnchorActive(AnchorPanel parent) => Parent = parent;

        public static string location = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "anchor.json");

        private ButtonListHandler<AnchorData, ButtonCell> dataHandler;
        private ScrollPool<ButtonCell> scrollPool;

        private List<AnchorData> anchors = new List<AnchorData>();

        private void LoadAnchors()
        {
            string directory = Path.GetDirectoryName(location);

            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            if (!File.Exists(location))
            {
                AnchorDataList empty = new AnchorDataList
                {
                    Anchors = new List<AnchorData>()
                };

                File.WriteAllText(location, JsonUtility.ToJson(empty, true));
                anchors = empty.Anchors;
                return;
            }

            string json = File.ReadAllText(location);

            if (string.IsNullOrWhiteSpace(json))
            {
                anchors = new List<AnchorData>();
                return;
            }

            AnchorDataList data = JsonUtility.FromJson<AnchorDataList>(json);

            anchors = data.Anchors ?? new List<AnchorData>();
        }

        public override void ConstructUI(GameObject parent)
        {
            LoadAnchors();
            uiRoot = UIFactory.CreateVerticalGroup(parent, "Anchor Active", true, true, true, true, 2, new Vector4(2, 2, 2, 2));
            UIFactory.SetLayoutElement(uiRoot, flexibleHeight: 9999);

            dataHandler = new ButtonListHandler<AnchorData, ButtonCell>(scrollPool, () => anchors, SetCell, ShouldDisplayCell, OnCellClicked);

            scrollPool = UIFactory.CreateScrollPool<ButtonCell>(uiRoot,"AnchorList", out GameObject scrollObj, out GameObject scrollContent);

            scrollPool.Initialize(dataHandler);

            UIFactory.SetLayoutElement(scrollObj, flexibleHeight: 9999);
        }

        private void SetCell(ButtonCell cell, int index)
        {
            AnchorData anchor = anchors[index];

            cell.Button.ButtonText.text = anchor.Location;
        }

        private bool ShouldDisplayCell(AnchorData anchor, string search)
        {
            return true;
        }

        private void OnCellClicked(int index)
        {
            AnchorData anchor = anchors[index];

            Debug.Log(anchor.Location);
        }
    }
}
