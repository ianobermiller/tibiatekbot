using System;
using System.Windows.Forms;
using TibiaTekPlus.Plugins;
using Tibia;
using Tibia.Objects;
using Tibia.Packets;
using Tibia.Packets.Incoming;
using Tibia.Packets.Outgoing;

namespace TibiaTekPlus.Plugins
{
    public class ConsolePlugin : TibiaTekPlus.Plugins.Plugin
    {
        #region Variables

        private string[] supportedVersions = { "8.40" };
        private string supportedKernel = @"1\.\d+\.\d+\.\d+";
        private Client client;
        private Player player;

        #endregion

        #region Initialization/Finalization

        public ConsolePlugin()
        {

        }

        ~ConsolePlugin()
        {

        }

        public override void Enable()
        {
            client = this.Host.Client;
            client.Proxy.PlayerLogin += PlayerLogin;
            client.Proxy.ReceivedPlayerSpeechOutgoingPacket += ReceivedPlayerSpeechOutgoingPacket;
        }

        public override void Disable()
        {
            client.Proxy.PlayerLogin -= PlayerLogin;
            client.Proxy.ReceivedPlayerSpeechOutgoingPacket -= ReceivedPlayerSpeechOutgoingPacket;
        }

        public override void Pause()
        {
            OutWhite("Console paused.");
        }

        public override void  Resume()
        {
            OutWhite("Console resumed.");
        }

        private void PlayerLogin()
        {
            player = client.GetPlayer();
            Tibia.Packets.Incoming.ChannelOpenPacket.Send(client, ChatChannel.Custom, "TT+");
            System.Threading.Thread.Sleep(100);
            OutWhite("Console started.");
        }

        #endregion

        #region Console

        private bool ReceivedPlayerSpeechOutgoingPacket(OutgoingPacket packet)
        {
            PlayerSpeechPacket p = (PlayerSpeechPacket)packet;

            if (p.SpeechType == SpeechType.ChannelYellow &&
                     p.ChannelId == ChatChannel.Custom)
            {
                CreatureSpeechPacket.Send(
                    client,
                    "$",
                    0,
                    p.Message,
                    SpeechType.ChannelOrange,
                    ChatChannel.Custom);
                Out(p.Message);
                return false;
            }
            else
                return true;
        }

        private void Out(string message)
        {
            CreatureSpeechPacket.Send(
                client,
                "",
                0,
                message,
                SpeechType.ChannelYellow,
                ChatChannel.Custom);
        }

        private void OutWhite(string message)
        {
            CreatureSpeechPacket.Send(
                client,
                "",
                0,
                message,
                SpeechType.ChannelWhite,
                ChatChannel.Custom);
        }

        #endregion

        #region Configuration Settings

        public override bool LoadConfig(string path)
        {
            MessageBox.Show("Finished loading.");
            return true;
        }
        public override bool SaveConfig(string path)
        {
            MessageBox.Show("Finished saving.");
            return true;
        }

        #endregion

        #region Graphic User Interface

        public override void ShowGui()
        {

        }

        public override void HideGui()
        {

        }

        public override string Category
        {
            get
            {
                return "Communication";
            }
        }

        #endregion

        #region Dependencies & Support

        public override string[] SupportedTibiaVersions
        {
            get
            {
                return supportedVersions;
            }
        }
        public override string[] PluginDependencies
        {
            get
            {
                return new string[] { };
            }
        }
        public override string SupportedKernel
        {
            get
            {
                return supportedKernel;
            }
        }

        #endregion
    }
}