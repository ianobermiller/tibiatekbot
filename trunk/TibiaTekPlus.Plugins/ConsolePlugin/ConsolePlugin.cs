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
        private string[] supportedVersions = { "8.40" };
        private string supportedKernel = @"1\.\d+\.\d+\.\d+";
        private Client client;
        private Player player;

        #region Initialization/Finalization

        public ConsolePlugin()
        {
            client = this.Host.Client;
            client.Proxy.PlayerLogin += PlayerLogin;
            client.Proxy.ReceivedPlayerSpeechOutgoingPacket += ReceivedPlayerSpeechOutgoingPacket;
        }

        ~ConsolePlugin()
        {
        }

        private void PlayerLogin()
        {
            player = client.GetPlayer();
            Tibia.Packets.Incoming.ChannelOpenPacket.Send(client, ChatChannel.Custom, "TibiaAPI");
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
                CreatureSpeechPacket.Send(
                    client,
                    ">",
                    0,
                    p.Message,
                    SpeechType.ChannelYellow,
                    ChatChannel.Custom);
                return false;
            }
            else
                return true;
        }

        #endregion

        #region Configuration Settings

        public override bool Load(string path)
        {
            MessageBox.Show("Finished loading.");
            return true;
        }
        public override bool Save(string path)
        {
            MessageBox.Show("Finished saving.");
            return true;
        }

        #endregion

        #region Graphic User Interface

        public override void Show()
        {

        }

        public override void Hide()
        {

        }

        public override string Category
        {
            get
            {
                return "Console";
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