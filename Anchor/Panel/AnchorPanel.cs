// Used: https://github.com/sinai-dev/UnityExplorer/blob/master/src/UI/Panels/ObjectExplorerPanel.cs
using System;
using System.Collections.Generic;
using Anchor.Pages;
using UnityEngine;
using UnityEngine.UI;
using UnityExplorer.UI;
using UnityExplorer.UI.Panels;
using UniverseLib;
using UniverseLib.UI;
using UniverseLib.UI.Models;

namespace Anchor.Panel
{
    public class AnchorPanel : UEPanel
    {
        public override UIManager.Panels PanelType => (UIManager.Panels)Constants.Anchor;
        public override string Name => "Anchor";
        public override int MinWidth => 400;
        public override int MinHeight => 300;
        public override Vector2 DefaultAnchorMin => new Vector2(0.125f, 0.175f);
        public override Vector2 DefaultAnchorMax => new Vector2(0.325f, 0.925f);

        public int SelectedTab = 0;

        public AnchorCreate AnchorCreate;
        public AnchorActive AnchorActive;

        private readonly List<UIModel> tabPages = new List<UIModel>();
        private readonly List<ButtonRef> tabButtons = new List<ButtonRef>();

        public AnchorPanel(UIBase owner) : base(owner) { }

        protected override void ConstructPanelContent()
        {
            // This is the setup for the actual bar which shows the tabs (ik cool)
            GameObject tabs = UIFactory.CreateHorizontalGroup(ContentRoot, "TabBar", true, true, true, true, 2, new Vector4(2, 2, 2, 2));
            UIFactory.SetLayoutElement(tabs, minHeight: 25, flexibleHeight: 0);

            // Anchors Active Page
            AnchorActive = new AnchorActive(this);
            AnchorActive.ConstructUI(ContentRoot);
            tabPages.Add(AnchorActive);

            // Create Anchor Page  
            AnchorCreate = new AnchorCreate(this);
            AnchorCreate.ConstructUI(ContentRoot);
            tabPages.Add(AnchorCreate);

            // This actually creates the tabs at the top of the UI
            Button(tabs, "Anchor Active");
            Button(tabs, "Anchor Create");
        }

        // This manages the tabs so they can be swapped between
        public void SetTab(int tabIndex)
        {
            if (SelectedTab != -1)
                DisableTab(SelectedTab);

            UIModel content = tabPages[tabIndex];
            content.SetActive(true);

            ButtonRef button = tabButtons[tabIndex];
            RuntimeHelper.SetColorBlock(button.Component, UniversalUI.EnabledButtonColor, UniversalUI.EnabledButtonColor * 1.2f);

            SelectedTab = tabIndex;
        }

        // This obvs disables the current tab
        private void DisableTab(int tabIndex)
        {
            tabPages[tabIndex].SetActive(false);
            RuntimeHelper.SetColorBlock(tabButtons[tabIndex].Component, UniversalUI.DisabledButtonColor, UniversalUI.DisabledButtonColor * 1.2f);
        }

        // This is the script for the button which is on the tabs
        private void Button(GameObject gameobject, string name)
        {
            ButtonRef newButton = UIFactory.CreateButton(gameobject, $"{name} Tab", name);

            int index = tabButtons.Count;
            newButton.OnClick += () => { SetTab(index); };

            tabButtons.Add(newButton);

            DisableTab(tabButtons.Count - 1);
        }
    }
}