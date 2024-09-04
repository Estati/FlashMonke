using BananaOS;
using BananaOS.Pages;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.XR;
using Utilla;

namespace QuickDisconnectBananaOS
{
    internal class WatchMenu:WatchPage
    {

        public override void OnPostModSetup()
        {
            selectionHandler.maxIndex = 2;
            Force = 5;
            ForceM = 5;

        }
        public override string Title => "<color=#FFA500>FlashMonke</color>";
        public override bool DisplayOnMainMenu => true;
        public bool IsEnabled;
        public float Force;
        public float ForceM;
        
        public override string OnGetScreenContent()
        {
            var BuildMenuOptions = new StringBuilder();
            BuildMenuOptions.AppendLine("<color=red>========================</color>");
            BuildMenuOptions.AppendLine("                 <color=#FFA500>FlashMonke</color>");
            BuildMenuOptions.AppendLine("");
            BuildMenuOptions.AppendLine("                 By: <color=blue>Estatic</color>");
            BuildMenuOptions.AppendLine("<color=red>========================</color>");
            BuildMenuOptions.AppendLine("");
            BuildMenuOptions.AppendLine(selectionHandler.GetOriginalBananaOSSelectionText(0, "[Enabled : " + IsEnabled + "]"));
            BuildMenuOptions.AppendLine("");
            BuildMenuOptions.AppendLine(selectionHandler.GetOriginalBananaOSSelectionText(1, "[MaxSpeed : " + Force + "]"));
            BuildMenuOptions.AppendLine(selectionHandler.GetOriginalBananaOSSelectionText(2, "[Force Multiplier : " + ForceM + "]"));
            return BuildMenuOptions.ToString();
        }

        public override void OnButtonPressed(WatchButtonType buttonType)
        {
            switch (buttonType)
            {
                case WatchButtonType.Down:
                    selectionHandler.MoveSelectionDown();
                    break;

                case WatchButtonType.Up:
                    selectionHandler.MoveSelectionUp();
                    break;

                case WatchButtonType.Right:
                    if (selectionHandler.currentIndex == 1)
                    {
                        if (Force > 250)
                        {
                            Force -= 1f;
                        }
                        else
                        {
                            Force += 1f;
                        }
                    }
                    if (selectionHandler.currentIndex == 2)
                    {
                        if (ForceM > 250)
                        {
                            ForceM -= 1f;
                        }
                        else
                        {
                            ForceM += 1f;
                        }
                    }
                    break;
                case WatchButtonType.Left:
                    if (selectionHandler.currentIndex == 1)
                    {
                        if (Force < 1)
                        {
                            Force += 1f;
                        }
                        else
                        {
                            Force -= 1f;
                        }
                    }
                    if (selectionHandler.currentIndex == 2)
                    {
                        if (ForceM < 1)
                        {
                            ForceM += 1f;
                        }
                        else
                        {
                            ForceM -= 1f;
                        }
                    }
                    break;


                case WatchButtonType.Enter:
                    if (selectionHandler.currentIndex == 0)
                    {
                        IsEnabled = !IsEnabled;
                    }
                    break;

                case WatchButtonType.Back:
                    ReturnToMainMenu();
                    break;
            }
        }
        void Update()
        {
            // thanks for the gamemode check dean!
            if (IsEnabled && PhotonNetwork.CurrentRoom.CustomProperties["gameMode"].ToString().Contains("MODDED_"))
            {
                GorillaLocomotion.Player.Instance.maxJumpSpeed = Force;
                GorillaLocomotion.Player.Instance.jumpMultiplier = ForceM;
            }
        }
    }
}
