'    Copyright (C) 2007 TibiaTek Development Team
'
'    This file is part of TibiaTek Bot.
'
'    TibiaTek Bot is free software: you can redistribute it and/or modify
'    it under the terms of the GNU General Public License as published by
'    the Free Software Foundation, either version 3 of the License, or
'    (at your option) any later version.
'
'    TibiaTek Bot is distributed in the hope that it will be useful,
'    but WITHOUT ANY WARRANTY; without even the implied warranty of
'    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
'    GNU General Public License for more details.
'
'    You should have received a copy of the GNU General Public License
'    along with TibiaTek Bot. If not, see http://www.gnu.org/licenses/gpl.txt
'    or write to the Free Software Foundation, 59 Temple Place - Suite 330,
'    Boston, MA 02111-1307, USA.Imports System.Math

Imports TibiaTekBot.Constants, System.Xml, TibiaTekBot.MiscUtils, TibiaTekBot.Constantsmodule, _
 System.Text.RegularExpressions, Microsoft.VisualBasic.Devices, TibiaTekBot.CoreModule

Public Class frmAlarms
    Dim BLMessagePlayerInterval As Integer = 0
    Dim ITMessagePlayerInterval As Integer = 0
    Dim STMessagePlayerInterval As Integer = 0
    Dim AlarmsActivated As Boolean = False
    Dim CanUpdate As Boolean = True

    Private FoodCond As ItemCondition
    Private BlankRunesCond As ItemCondition
    Private WormsCond As ItemCondition
    Private ThrowablesCond As ItemCondition
    Private AmmunitionCond As ItemCondition

    Private Enum LogicConditions
        Equal
        LessThan
        MoreThan
        LessOrEqualThan
        MoreOrEqualThan
        NotEqual
    End Enum

    Private Structure ItemCondition
        Public Active As Boolean
        Public Condition As LogicConditions
        Public CheckFloor As Boolean
        Public CheckInventory As Boolean
        Public Count As Decimal
    End Structure

    Private Sub Alarms_CanClose(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            e.Cancel = True
            Me.Hide()
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AlarmsHide.Click
        Try
            Me.Hide()
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AlarmsLoad.Click

        Tabs.Enabled = False
        Try
            If Not IO.File.Exists(Core.GetProfileDirectory & "\Alarms.xml") Then
                Tabs.Enabled = True
                Exit Sub
            End If

            BattlelistIgnoredPlayers.Items.Clear()
            MessageIgnoredPlayers.Items.Clear()

            Dim Document As New XmlDocument
            Document.Load(Core.GetProfileDirectory & "\Alarms.xml")
            Dim AlarmsNode As XmlNode = Document.Item("Alarms")
            'battlelist alarms
            Dim BattlelistNode As XmlNode = AlarmsNode.Item("Battlelist")
            BattlelistPlayer.Checked = CBool(BattlelistNode.Item("ActivateIfPlayer").InnerText)
            BattlelistMonsterNPC.Checked = CBool(BattlelistNode.Item("ActivateIfMonsterNPC").InnerText)
            BattlelistPlayerKiller.Checked = CBool(BattlelistNode.Item("ActivateIfPlayerKiller").InnerText)
            BattlelistMultiFloor.Checked = CBool(BattlelistNode.Item("MultiFloor").InnerText)
            BattlelistMultiFloorBelow.Checked = BattlelistNode.Item("MultiFloor").GetAttribute("Below").Equals("True")
            BattlelistMultiFloorAbove.Checked = BattlelistNode.Item("MultiFloor").GetAttribute("Above").Equals("True")
            MultiFloorGroupBox.Enabled = BattlelistMultiFloor.Checked
            BattlelistPlaySound.Checked = CBool(BattlelistNode.Item("PlaySound").InnerText)
            BattlelistLogout.Checked = CBool(BattlelistNode.Item("LogOut").InnerText)
            BattlelistMessagePlayerInput.Text = BattlelistNode.Item("MessagePlayer").GetAttribute("Player")
            BattlelistMessagePlayer.Checked = CBool(BattlelistNode.Item("MessagePlayer").InnerText)
            Dim BattlelistIgnoredPlayersNode As XmlNode = BattlelistNode.Item("IgnoredPlayers")
            For Each Player As XmlElement In BattlelistIgnoredPlayersNode
                BattlelistIgnoredPlayers.Items.Add(Player.InnerText)
            Next
            'message alarms
            Dim MessageNode As XmlNode = AlarmsNode.Item("Message")
            MessagePublic.Checked = CBool(MessageNode.Item("Public").InnerText)
            MessagePrivate.Checked = CBool(MessageNode.Item("Private").InnerText)
            MessagePlaySound.Checked = CBool(MessageNode.Item("PlaySound").InnerText)
            MessageLogOut.Checked = CBool(MessageNode.Item("LogOut").InnerText)
            MessageForwardMessageInput.Text = MessageNode.Item("ForwardMessage").GetAttribute("Player")
            MessageForwardMessage.Checked = CBool(MessageNode.Item("ForwardMessage").InnerText)
            Dim ForwardMessagePlayersNode As XmlNode = MessageNode.Item("IgnoredPlayers")
            For Each Player As XmlElement In ForwardMessagePlayersNode
                MessageIgnoredPlayers.Items.Add(Player.InnerText)
            Next
            'status
            Dim StatusNode As XmlNode = AlarmsNode.Item("Status")
            StatusHitPoints.Value = CDec(StatusNode.Item("HitPoints").InnerText)
            StatusManaPoints.Value = CDec(StatusNode.Item("ManaPoints").InnerText)
            StatusSoulPoints.Value = CDec(StatusNode.Item("SoulPoints").InnerText)
            StatusCapacity.Value = CDec(StatusNode.Item("Capacity").InnerText)
            StatusPlaySound.Checked = CBool(StatusNode.Item("PlaySound").InnerText)
            StatusLogOut.Checked = CBool(StatusNode.Item("LogOut").InnerText)
            StatusMessagePlayer.Checked = CBool(StatusNode.Item("MessagePlayer").InnerText)
            StatusMessagePlayerName.Text = StatusNode.Item("MessagePlayer").GetAttribute("Player")
            Dim ConditionsNode As XmlNode = StatusNode.Item("Conditions")
            StatusConditionCombatSign.Checked = CBool(ConditionsNode.Item("CombatSign").InnerText)
            StatusConditionBurnt.Checked = CBool(ConditionsNode.Item("Burnt").InnerText)
            StatusConditionPoisoned.Checked = CBool(ConditionsNode.Item("Poisoned").InnerText)
            StatusConditionDrowning.Checked = CBool(ConditionsNode.Item("Drowning").InnerText)
            StatusConditionParalized.Checked = CBool(ConditionsNode.Item("Paralized").InnerText)
            StatusConditionElectrified.Checked = CBool(ConditionsNode.Item("Electrified").InnerText)
            'items
            Dim ItemsNode As XmlNode = AlarmsNode.Item("Items")
            ItemsPlaySound.Checked = CBool(ItemsNode.Item("PlaySound").InnerText)
            ItemsLogOut.Checked = CBool(ItemsNode.Item("LogOut").InnerText)
            ItemsMessagePlayer.Checked = CBool(ItemsNode.Item("MessagePlayer").InnerText)
            ItemsMessagePlayerName.Text = ItemsNode.Item("MessagePlayer").GetAttribute("Player")
            Dim ListNode As XmlNode = ItemsNode.Item("List")

            Dim FoodNode As XmlElement = ListNode.Item("Food")
            FoodCond.Active = CBool(FoodNode.InnerText)
            ItemsList.SetItemChecked(0, FoodCond.Active)
            FoodCond.Condition = CType(FoodNode.GetAttribute("Condition"), LogicConditions)
            FoodCond.Count = CDec(FoodNode.GetAttribute("Count"))
            FoodCond.CheckFloor = CBool(FoodNode.GetAttribute("CheckFloor"))
            FoodCond.CheckInventory = CBool(FoodNode.GetAttribute("CheckInventory"))

            Dim BlankRunesNode As XmlElement = ListNode.Item("BlankRunes")
            BlankRunesCond.Active = CBool(BlankRunesNode.InnerText)
            ItemsList.SetItemChecked(1, BlankRunesCond.Active)
            BlankRunesCond.Condition = CType(BlankRunesNode.GetAttribute("Condition"), LogicConditions)
            BlankRunesCond.Count = CDec(BlankRunesNode.GetAttribute("Count"))
            BlankRunesCond.CheckFloor = CBool(BlankRunesNode.GetAttribute("CheckFloor"))
            BlankRunesCond.CheckInventory = CBool(BlankRunesNode.GetAttribute("CheckInventory"))

            Dim WormsNode As XmlElement = ListNode.Item("Worms")
            WormsCond.Active = CBool(WormsNode.InnerText)
            ItemsList.SetItemChecked(2, WormsCond.Active)
            WormsCond.Condition = CType(WormsNode.GetAttribute("Condition"), LogicConditions)
            WormsCond.Count = CDec(WormsNode.GetAttribute("Count"))
            WormsCond.CheckFloor = CBool(WormsNode.GetAttribute("CheckFloor"))
            WormsCond.CheckInventory = CBool(WormsNode.GetAttribute("CheckInventory"))

            Dim ThrowablesNode As XmlElement = ListNode.Item("Throwables")
            ThrowablesCond.Active = CBool(ThrowablesNode.InnerText)
            ItemsList.SetItemChecked(3, ThrowablesCond.Active)
            ThrowablesCond.Condition = CType(ThrowablesNode.GetAttribute("Condition"), LogicConditions)
            ThrowablesCond.Count = CDec(ThrowablesNode.GetAttribute("Count"))
            ThrowablesCond.CheckFloor = CBool(ThrowablesNode.GetAttribute("CheckFloor"))
            ThrowablesCond.CheckInventory = CBool(ThrowablesNode.GetAttribute("CheckInventory"))

            Dim AmmunitionNode As XmlElement = ListNode.Item("Ammunition")
            AmmunitionCond.Active = CBool(AmmunitionNode.InnerText)
            ItemsList.SetItemChecked(4, AmmunitionCond.Active)
            AmmunitionCond.Condition = CType(AmmunitionNode.GetAttribute("Condition"), LogicConditions)
            AmmunitionCond.Count = CDec(AmmunitionNode.GetAttribute("Count"))
            AmmunitionCond.CheckFloor = CBool(AmmunitionNode.GetAttribute("CheckFloor").Equals("True"))
            AmmunitionCond.CheckInventory = CBool(AmmunitionNode.GetAttribute("CheckInventory").Equals("True"))
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        Finally
            Tabs.Enabled = True
        End Try
    End Sub

    Private Sub BattlelistIgnoredPlayerAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BattlelistIgnoredPlayerAdd.Click
        If Not BattlelistIgnoredPlayersInput.Text Is Nothing Then
            Try
                If BattlelistIgnoredPlayersInput.Text.Length = 0 Then
                    MessageBox.Show("Invalid regular expression: " & BattlelistIgnoredPlayersInput.Text & ".", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
                    Exit Sub
                End If
                Dim Regexp As New Regex("^" & BattlelistIgnoredPlayersInput.Text & "$")
                BattlelistIgnoredPlayers.Items.Add(BattlelistIgnoredPlayersInput.Text)
                BattlelistIgnoredPlayersInput.Text = ""
            Catch
                MessageBox.Show("Invalid regular expression: " & BattlelistIgnoredPlayersInput.Text & ".", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            End Try
        End If
    End Sub

    Private Sub BattlelisstIgnoredPlayerRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BattlelistIgnoredPlayerRemove.Click
        Try
            If BattlelistIgnoredPlayers.SelectedIndex > -1 Then
                BattlelistIgnoredPlayersInput.Text = BattlelistIgnoredPlayers.Items.Item(BattlelistIgnoredPlayers.SelectedIndex).ToString
                BattlelistIgnoredPlayers.Items.RemoveAt(BattlelistIgnoredPlayers.SelectedIndex)
            End If
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub AlarmsSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AlarmsSave.Click
        Try
            Dim xmlFile As New XmlDocument()
            'alarms
            Dim xmlAlarms As XmlElement = xmlFile.CreateElement("Alarms")
            Dim xmlBattlelist As XmlElement = xmlFile.CreateElement("Battlelist")
            Dim xmlBLActivateIfPlayer As XmlElement = xmlFile.CreateElement("ActivateIfPlayer")
            Dim xmlBLActivateIfMonsterNPC As XmlElement = xmlFile.CreateElement("ActivateIfMonsterNPC")
            Dim xmlBLActivateIfPlayerKiller As XmlElement = xmlFile.CreateElement("ActivateIfPlayerKiller")
            Dim xmlBLMultiFloor As XmlElement = xmlFile.CreateElement("MultiFloor")
            Dim xmlBLMultiFloorBelow As XmlAttribute = xmlFile.CreateAttribute("Below")
            Dim xmlBLMultiFloorAbove As XmlAttribute = xmlFile.CreateAttribute("Above")
            Dim xmlBLAlertRemoteAdmins As XmlElement = xmlFile.CreateElement("AlertRemoteAdmins")
            Dim xmlBLPlaySound As XmlElement = xmlFile.CreateElement("PlaySound")
            Dim xmlBLLogOut As XmlElement = xmlFile.CreateElement("LogOut")
            Dim xmlBLMessagePlayer As XmlElement = xmlFile.CreateElement("MessagePlayer")
            Dim xmlBLMessagePlayerName As XmlAttribute = xmlFile.CreateAttribute("Player")
            Dim xmlBLIgnoredPlayers As XmlElement = xmlFile.CreateElement("IgnoredPlayers")

            xmlBLActivateIfPlayer.InnerText = BattlelistPlayer.Checked
            xmlBLActivateIfMonsterNPC.InnerText = BattlelistMonsterNPC.Checked
            xmlBLActivateIfPlayerKiller.InnerText = BattlelistPlayerKiller.Checked
            xmlBLMultiFloor.InnerText = BattlelistMultiFloor.Checked
            xmlBLMultiFloorBelow.InnerText = BattlelistMultiFloorBelow.Checked
            xmlBLMultiFloorAbove.InnerText = BattlelistMultiFloorAbove.Checked
            xmlBLPlaySound.InnerText = BattlelistPlaySound.Checked
            xmlBLLogOut.InnerText = BattlelistLogout.Checked
            xmlBLMessagePlayerName.InnerText = BattlelistMessagePlayerInput.Text
            xmlBLMessagePlayer.Attributes.Append(xmlBLMessagePlayerName)
            xmlBLMessagePlayer.InnerText = BattlelistMessagePlayer.Checked

            For Each BLPlayer As String In BattlelistIgnoredPlayers.Items
                Dim xmlBLIgnoredPlayersName As XmlElement = xmlFile.CreateElement("Player")
                xmlBLIgnoredPlayersName.InnerText = BLPlayer
                xmlBLIgnoredPlayers.AppendChild(xmlBLIgnoredPlayersName)
            Next

            xmlBattlelist.AppendChild(xmlBLActivateIfPlayer)
            xmlBattlelist.AppendChild(xmlBLActivateIfMonsterNPC)
            xmlBattlelist.AppendChild(xmlBLActivateIfPlayerKiller)
            xmlBLMultiFloor.Attributes.Append(xmlBLMultiFloorBelow)
            xmlBLMultiFloor.Attributes.Append(xmlBLMultiFloorAbove)
            xmlBattlelist.AppendChild(xmlBLMultiFloor)
            xmlBattlelist.AppendChild(xmlBLAlertRemoteAdmins)
            xmlBattlelist.AppendChild(xmlBLPlaySound)
            xmlBattlelist.AppendChild(xmlBLLogOut)
            xmlBattlelist.AppendChild(xmlBLMessagePlayer)
            xmlBattlelist.AppendChild(xmlBLIgnoredPlayers)
            'message
            Dim xmlMessage As XmlElement = xmlFile.CreateElement("Message")
            Dim xmlMSPublic As XmlElement = xmlFile.CreateElement("Public")
            Dim xmlMSPrivate As XmlElement = xmlFile.CreateElement("Private")
            Dim xmlMSPlaySound As XmlElement = xmlFile.CreateElement("PlaySound")
            Dim xmlMSLogOut As XmlElement = xmlFile.CreateElement("LogOut")
            Dim xmlMSForwardMessage As XmlElement = xmlFile.CreateElement("ForwardMessage")
            Dim xmlMSForwardMessagePlayerName As XmlAttribute = xmlFile.CreateAttribute("Player")
            Dim xmlMSIgnoredPlayers As XmlElement = xmlFile.CreateElement("IgnoredPlayers")

            xmlMSPublic.InnerText = MessagePublic.Checked
            xmlMSPrivate.InnerText = MessagePrivate.Checked
            xmlMSPlaySound.InnerText = MessagePlaySound.Checked
            xmlMSLogOut.InnerText = MessageLogOut.Checked

            xmlMSForwardMessage.InnerText = MessageForwardMessage.Checked
            xmlMSForwardMessagePlayerName.InnerText = MessageForwardMessageInput.Text
            xmlMSForwardMessage.Attributes.Append(xmlMSForwardMessagePlayerName)

            For Each MSPlayer As String In MessageIgnoredPlayers.Items
                Dim xmlMSIgnoredPlayersName As XmlElement = xmlFile.CreateElement("Player")
                xmlMSIgnoredPlayersName.InnerText = MSPlayer
                xmlMSIgnoredPlayers.AppendChild(xmlMSIgnoredPlayersName)
            Next

            xmlMessage.AppendChild(xmlMSPublic)
            xmlMessage.AppendChild(xmlMSPrivate)
            xmlMessage.AppendChild(xmlMSPlaySound)
            xmlMessage.AppendChild(xmlMSLogOut)
            xmlMessage.AppendChild(xmlMSForwardMessage)
            xmlMessage.AppendChild(xmlMSIgnoredPlayers)

            'status
            Dim xmlStatus As XmlElement = xmlFile.CreateElement("Status")
            Dim xmlSTHitPoints As XmlElement = xmlFile.CreateElement("HitPoints")
            xmlSTHitPoints.InnerText = StatusHitPoints.Value
            Dim xmlSTManaPoints As XmlElement = xmlFile.CreateElement("ManaPoints")
            xmlSTManaPoints.InnerText = StatusManaPoints.Value
            Dim xmlSTSoulPoints As XmlElement = xmlFile.CreateElement("SoulPoints")
            xmlSTSoulPoints.InnerText = StatusSoulPoints.Value
            Dim xmlSTCapacity As XmlElement = xmlFile.CreateElement("Capacity")
            xmlSTCapacity.InnerText = StatusCapacity.Value
            Dim xmlSTConditions As XmlElement = xmlFile.CreateElement("Conditions")
            Dim xmlSTPlaySound As XmlElement = xmlFile.CreateElement("PlaySound")
            xmlSTPlaySound.InnerText = StatusPlaySound.Checked
            Dim xmlSTLogOut As XmlElement = xmlFile.CreateElement("LogOut")
            xmlSTLogOut.InnerText = StatusLogOut.Checked
            Dim xmlSTMessagePlayer As XmlElement = xmlFile.CreateElement("MessagePlayer")
            xmlSTMessagePlayer.InnerText = StatusMessagePlayer.Checked
            Dim xmlMessagePlayerName As XmlAttribute = xmlFile.CreateAttribute("Player")
            xmlMessagePlayerName.InnerText = StatusMessagePlayerName.Text
            xmlSTMessagePlayer.Attributes.Append(xmlMessagePlayerName)

            Dim xmlSTConditionCombatSign As XmlElement = xmlFile.CreateElement("CombatSign")
            xmlSTConditionCombatSign.InnerText = StatusConditionCombatSign.Checked
            Dim xmlSTConditionBurnt As XmlElement = xmlFile.CreateElement("Burnt")
            xmlSTConditionBurnt.InnerText = StatusConditionBurnt.Checked
            Dim xmlSTConditionPoisoned As XmlElement = xmlFile.CreateElement("Poisoned")
            xmlSTConditionPoisoned.InnerText = StatusConditionPoisoned.Checked
            Dim xmlSTConditionDrowning As XmlElement = xmlFile.CreateElement("Drowning")
            xmlSTConditionDrowning.InnerText = StatusConditionDrowning.Checked
            Dim xmlSTConditionParalized As XmlElement = xmlFile.CreateElement("Paralized")
            xmlSTConditionParalized.InnerText = StatusConditionParalized.Checked
            Dim xmlSTConditionElectrified As XmlElement = xmlFile.CreateElement("Electrified")
            xmlSTConditionElectrified.InnerText = StatusConditionElectrified.Checked
            xmlSTConditions.AppendChild(xmlSTConditionCombatSign)
            xmlSTConditions.AppendChild(xmlSTConditionBurnt)
            xmlSTConditions.AppendChild(xmlSTConditionPoisoned)
            xmlSTConditions.AppendChild(xmlSTConditionDrowning)
            xmlSTConditions.AppendChild(xmlSTConditionParalized)
            xmlSTConditions.AppendChild(xmlSTConditionElectrified)

            xmlStatus.AppendChild(xmlSTHitPoints)
            xmlStatus.AppendChild(xmlSTManaPoints)
            xmlStatus.AppendChild(xmlSTSoulPoints)
            xmlStatus.AppendChild(xmlSTCapacity)
            xmlStatus.AppendChild(xmlSTPlaySound)
            xmlStatus.AppendChild(xmlSTLogOut)
            xmlStatus.AppendChild(xmlSTMessagePlayer)
            xmlStatus.AppendChild(xmlSTConditions)

            'items 
            Dim xmlItems As XmlElement = xmlFile.CreateElement("Items")
            Dim xmlITPlaySound As XmlElement = xmlFile.CreateElement("PlaySound")
            xmlITPlaySound.InnerText = ItemsPlaySound.Checked
            Dim xmlITLogOut As XmlElement = xmlFile.CreateElement("LogOut")
            xmlITLogOut.InnerText = ItemsLogOut.Checked
            Dim xmlITMessagePlayer As XmlElement = xmlFile.CreateElement("MessagePlayer")
            xmlITMessagePlayer.InnerText = ItemsMessagePlayer.Checked
            Dim xmlITMessagePlayerName As XmlAttribute = xmlFile.CreateAttribute("Player")
            xmlITMessagePlayerName.InnerText = ItemsMessagePlayerName.Text
            xmlITMessagePlayer.Attributes.Append(xmlITMessagePlayerName)
            Dim xmlITList As XmlElement = xmlFile.CreateElement("List")
            'food
            Dim xmlITFood As XmlElement = xmlFile.CreateElement("Food")
            xmlITFood.InnerText = FoodCond.Active
            Dim xmlITFoodCondition As XmlAttribute = xmlFile.CreateAttribute("Condition")
            xmlITFoodCondition.InnerText = FoodCond.Condition
            Dim xmlITFoodCount As XmlAttribute = xmlFile.CreateAttribute("Count")
            xmlITFoodCount.InnerText = FoodCond.Count
            Dim xmlITFoodCheckInventory As XmlAttribute = xmlFile.CreateAttribute("CheckInventory")
            xmlITFoodCheckInventory.InnerText = FoodCond.CheckInventory
            Dim xmlITFoodCheckFloor As XmlAttribute = xmlFile.CreateAttribute("CheckFloor")
            xmlITFoodCheckFloor.InnerText = FoodCond.CheckFloor
            xmlITFood.Attributes.Append(xmlITFoodCondition)
            xmlITFood.Attributes.Append(xmlITFoodCount)
            xmlITFood.Attributes.Append(xmlITFoodCheckInventory)
            xmlITFood.Attributes.Append(xmlITFoodCheckFloor)
            'blank runes
            Dim xmlITBlankRunes As XmlElement = xmlFile.CreateElement("BlankRunes")
            xmlITBlankRunes.InnerText = BlankRunesCond.Active
            Dim xmlITBlankRunesCondition As XmlAttribute = xmlFile.CreateAttribute("Condition")
            xmlITBlankRunesCondition.InnerText = BlankRunesCond.Condition
            Dim xmlITBlankRunesCount As XmlAttribute = xmlFile.CreateAttribute("Count")
            xmlITBlankRunesCount.InnerText = BlankRunesCond.Count
            Dim xmlITBlankRunesCheckInventory As XmlAttribute = xmlFile.CreateAttribute("CheckInventory")
            xmlITBlankRunesCheckInventory.InnerText = BlankRunesCond.CheckInventory
            Dim xmlITBlankRunesCheckFloor As XmlAttribute = xmlFile.CreateAttribute("CheckFloor")
            xmlITBlankRunesCheckFloor.InnerText = BlankRunesCond.CheckFloor
            xmlITBlankRunes.Attributes.Append(xmlITBlankRunesCondition)
            xmlITBlankRunes.Attributes.Append(xmlITBlankRunesCount)
            xmlITBlankRunes.Attributes.Append(xmlITBlankRunesCheckInventory)
            xmlITBlankRunes.Attributes.Append(xmlITBlankRunesCheckFloor)
            'worms
            Dim xmlITWorms As XmlElement = xmlFile.CreateElement("Worms")
            xmlITWorms.InnerText = WormsCond.Active
            Dim xmlITWormsCondition As XmlAttribute = xmlFile.CreateAttribute("Condition")
            xmlITWormsCondition.InnerText = WormsCond.Condition
            Dim xmlITWormsCount As XmlAttribute = xmlFile.CreateAttribute("Count")
            xmlITWormsCount.InnerText = WormsCond.Count
            Dim xmlITWormsCheckInventory As XmlAttribute = xmlFile.CreateAttribute("CheckInventory")
            xmlITWormsCheckInventory.InnerText = WormsCond.CheckInventory
            Dim xmlITWormsCheckFloor As XmlAttribute = xmlFile.CreateAttribute("CheckFloor")
            xmlITWormsCheckFloor.InnerText = WormsCond.CheckFloor
            xmlITWorms.Attributes.Append(xmlITWormsCondition)
            xmlITWorms.Attributes.Append(xmlITWormsCount)
            xmlITWorms.Attributes.Append(xmlITWormsCheckInventory)
            xmlITWorms.Attributes.Append(xmlITWormsCheckFloor)
            'throwables
            Dim xmlITThrowables As XmlElement = xmlFile.CreateElement("Throwables")
            xmlITThrowables.InnerText = ThrowablesCond.Active
            Dim xmlITThrowablesCondition As XmlAttribute = xmlFile.CreateAttribute("Condition")
            xmlITThrowablesCondition.InnerText = ThrowablesCond.Condition
            Dim xmlITThrowablesCount As XmlAttribute = xmlFile.CreateAttribute("Count")
            xmlITThrowablesCount.InnerText = ThrowablesCond.Count
            Dim xmlITThrowablesCheckInventory As XmlAttribute = xmlFile.CreateAttribute("CheckInventory")
            xmlITThrowablesCheckInventory.InnerText = ThrowablesCond.CheckInventory
            Dim xmlITThrowablesCheckFloor As XmlAttribute = xmlFile.CreateAttribute("CheckFloor")
            xmlITThrowablesCheckFloor.InnerText = ThrowablesCond.CheckFloor
            xmlITThrowables.Attributes.Append(xmlITThrowablesCondition)
            xmlITThrowables.Attributes.Append(xmlITThrowablesCount)
            xmlITThrowables.Attributes.Append(xmlITThrowablesCheckInventory)
            xmlITThrowables.Attributes.Append(xmlITThrowablesCheckFloor)
            'ammunition
            Dim xmlITAmmunition As XmlElement = xmlFile.CreateElement("Ammunition")
            xmlITAmmunition.InnerText = AmmunitionCond.Active
            Dim xmlITAmmunitionCondition As XmlAttribute = xmlFile.CreateAttribute("Condition")
            xmlITAmmunitionCondition.InnerText = AmmunitionCond.Condition
            Dim xmlITAmmunitionCount As XmlAttribute = xmlFile.CreateAttribute("Count")
            xmlITAmmunitionCount.InnerText = AmmunitionCond.Count
            Dim xmlITAmmunitionCheckInventory As XmlAttribute = xmlFile.CreateAttribute("CheckInventory")
            xmlITAmmunitionCheckInventory.InnerText = AmmunitionCond.CheckInventory
            Dim xmlITAmmunitionCheckFloor As XmlAttribute = xmlFile.CreateAttribute("CheckFloor")
            xmlITAmmunitionCheckFloor.InnerText = AmmunitionCond.CheckFloor
            xmlITAmmunition.Attributes.Append(xmlITAmmunitionCondition)
            xmlITAmmunition.Attributes.Append(xmlITAmmunitionCount)
            xmlITAmmunition.Attributes.Append(xmlITAmmunitionCheckInventory)
            xmlITAmmunition.Attributes.Append(xmlITAmmunitionCheckFloor)

            xmlITList.AppendChild(xmlITFood)
            xmlITList.AppendChild(xmlITBlankRunes)
            xmlITList.AppendChild(xmlITWorms)
            xmlITList.AppendChild(xmlITThrowables)
            xmlITList.AppendChild(xmlITAmmunition)

            xmlItems.AppendChild(xmlITPlaySound)
            xmlItems.AppendChild(xmlITLogOut)
            xmlItems.AppendChild(xmlITMessagePlayer)
            xmlItems.AppendChild(xmlITList)
            'final stuff
            xmlAlarms.AppendChild(xmlBattlelist)
            xmlAlarms.AppendChild(xmlMessage)
            xmlAlarms.AppendChild(xmlStatus)
            xmlAlarms.AppendChild(xmlItems)
            Dim Declaration As XmlDeclaration = xmlFile.CreateXmlDeclaration("1.0", "", "")
            xmlFile.AppendChild(Declaration)
            xmlFile.AppendChild(xmlAlarms)
            If IO.File.Exists(Core.GetProfileDirectory & "\Alarms.xml") Then IO.File.Delete(Core.GetProfileDirectory & "\Alarms.xml")
            xmlFile.Save(Core.GetProfileDirectory & "\Alarms.xml")
        Catch
            MessageBox.Show("Couldn't save the alarm settings.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub AlarmsActivate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AlarmsActivate.Click
        Try
            AlarmsActivate.Enabled = False
            AlarmsSave.Enabled = False
            AlarmsLoad.Enabled = False
            If Core.AlarmsActivated Then
                AlarmsActivate.Text = "Activate"
                Core.AlarmsActivated = False
                BattlelistAlarmTimer.Stop()
                StatusAlarmTimer.Stop()
                ItemsAlarmTimer.Stop()
                Tabs.Enabled = True
                AlarmsLoad.Enabled = True
            Else
                AlarmsActivate.Text = "Deactivate"
                Core.AlarmsActivated = True
                BattlelistAlarmTimer.Start()
                StatusAlarmTimer.Start()
                ItemsAlarmTimer.Start()
                AlarmsLoad.Enabled = False
                Tabs.Enabled = False
            End If
            AlarmsActivate.Enabled = True
            AlarmsSave.Enabled = True
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub BattlelistAlarm_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BattlelistAlarmTimer.Tick
        Try
            Dim BL As New BattleList
            Dim Name As String = ""
            Dim BattleListIgnoredPlayersCount As Integer
            Dim Alert As Boolean = False
            Dim SkullMark As SkullMark
            Dim Output As String = ""
            Dim I As Integer
            BattleListIgnoredPlayersCount = BattlelistIgnoredPlayers.Items.Count
            BL.Reset()
            Do
                If BL.IsOnScreen AndAlso Not BL.IsMyself Then
                    If BattlelistMultiFloor.Checked Then
                        If BL.GetFloor < Core.CharacterLoc.Z AndAlso Not BattlelistMultiFloorAbove.Checked Then Continue Do
                        If BL.GetFloor > Core.CharacterLoc.Z AndAlso Not BattlelistMultiFloorBelow.Checked Then Continue Do
                    Else
                        If BL.GetFloor <> Core.CharacterLoc.Z Then Continue Do
                    End If
                    Name = BL.GetName
                    If Not BL.IsPlayer Then 'Is a monster
                        If BattlelistMonsterNPC.Checked Then
                            Output = "Monster or NPC "
                            Alert = True
                            Exit Do
                        End If
                    Else 'Is a player
                        If BattlelistPlayer.Checked Then Alert = True
                        Output = "Player "
                        For I = 0 To BattleListIgnoredPlayersCount - 1
                            If Regex.IsMatch(Name, "^" & BattlelistIgnoredPlayers.Items(I).ToString & "$", RegexOptions.IgnoreCase) Then
                                If BattlelistPlayer.Checked Then Alert = False
                                Continue Do
                            End If
                        Next
                        If BattlelistPlayerKiller.Checked Then
                            SkullMark = BL.GetSkullMark
                            Select Case SkullMark
                                Case SkullMark.Yellow
                                    Alert = True
                                    Output = "Player Killer "
                                Case SkullMark.White
                                    Alert = True
                                    Output = "Player Killer "
                                Case SkullMark.Red
                                    Alert = True
                                    Output = "Player Killer "
                            End Select
                        End If
                        If Alert Then Exit Do
                    End If
                End If
            Loop While BL.NextEntity(True)
            If Alert Then
                If Core.TibiaWindowState <> ConstantsModule.WindowState.Active AndAlso Consts.FlashTaskbarWhenAlarmFires Then
                    Dim FWI As New Win32API.FlashWInfo(Core.Proxy.Client.MainWindowHandle, Win32API.FlashWFlags.FLASHW_TIMERNOFG Or Win32API.FlashWFlags.FLASHW_TRAY Or Win32API.FlashWFlags.FLASHW_CAPTION, 0, 0)
                    Win32API.FlashWindowEx(FWI)
                End If
                If BattlelistPlaySound.Checked Then
                    Dim Sound As New Audio
                    Try
                        Sound.Play(Core.ExecutablePath & "\Alarms\Battlelist.wav", AudioPlayMode.Background)
                    Catch
                    End Try
                End If
                Dim ChatMessage As New CoreModule.ChatMessageDefinition
                Output &= Name & " has fired my alarm."
                If BattlelistMessagePlayer.Checked Then
                    If BLMessagePlayerInterval = 0 Then
                        ChatMessage.Destinatary = BattlelistMessagePlayerInput.Text
                        ChatMessage.MessageType = MessageType.PM
                        ChatMessage.Message = Output
                        Core.ChatMessageQueueList.Insert(0, ChatMessage)
                        BLMessagePlayerInterval += 1
                    ElseIf BLMessagePlayerInterval <= 15 Then
                        BLMessagePlayerInterval += 1
                    Else
                        BLMessagePlayerInterval = 0
                    End If
                End If
                If Consts.MusicalNotesOnAlarm Then Core.Proxy.SendPacketToClient(MagicEffect(Core.CharacterLoc, MagicEffects.MusicalNotesRed))
                If BattlelistLogout.Checked Then
                    Log("Battlelist Alarm", Output)
                    Log("Battlelist Alarm", "Logging out.")
                    Core.Proxy.SendPacketToServer(PacketUtils.PlayerLogout)
                End If
            End If
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub MessageIgnoredPlayersAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MessageIgnoredPlayersAdd.Click
        If Not MessageIgnoredPlayersInput.Text Is Nothing Then
            Try
                If MessageIgnoredPlayersInput.Text.Length = 0 Then
                    MessageBox.Show("Invalid regular expression: " & MessageIgnoredPlayersInput.Text & ".", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
                    Exit Sub
                End If
                Dim Regexp As New Regex("^" & MessageIgnoredPlayersInput.Text & "$")
                MessageIgnoredPlayers.Items.Add(MessageIgnoredPlayersInput.Text)
                MessageIgnoredPlayersInput.Text = ""
            Catch
                MessageBox.Show("Invalid regular expression: " & MessageIgnoredPlayersInput.Text & ".", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            End Try
        End If
    End Sub

    Private Sub MessageIgnoredPlayersRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MessageIgnoredPlayersRemove.Click
        Try
            If MessageIgnoredPlayers.SelectedIndex > -1 Then
                MessageIgnoredPlayersInput.Text = MessageIgnoredPlayers.Items.Item(MessageIgnoredPlayers.SelectedIndex).ToString
                MessageIgnoredPlayers.Items.RemoveAt(MessageIgnoredPlayers.SelectedIndex)
            End If
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub frmAlarms_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            FoodCond.Active = False
            FoodCond.Condition = LogicConditions.Equal
            FoodCond.Count = 0
            BlankRunesCond.Active = False
            BlankRunesCond.Condition = LogicConditions.Equal
            BlankRunesCond.Count = 0
            WormsCond.Active = False
            WormsCond.Condition = LogicConditions.Equal
            WormsCond.Count = 0
            ThrowablesCond.Active = False
            ThrowablesCond.Condition = LogicConditions.Equal
            ThrowablesCond.Count = 0
            AmmunitionCond.Active = False
            AmmunitionCond.Condition = LogicConditions.Equal
            AmmunitionCond.Count = 0
            Button8_Click(Nothing, Nothing)
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub StatusAlarmTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StatusAlarmTimer.Tick
        Try
            Dim Alert As Boolean = False
            Dim Output As String = ""
            Dim CurrentConditions As Integer = 0

            Core.ReadMemory(Consts.ptrConditions, CurrentConditions, 2)
            If Core.HitPoints < StatusHitPoints.Value Then
                Output &= " My HP is " & Core.HitPoints & ", below " & StatusHitPoints.Value & "."
                Alert = True
            End If
            If Core.ManaPoints < StatusManaPoints.Value Then
                Output &= " My MP is " & Core.ManaPoints & ", below " & StatusManaPoints.Value & "."
                Alert = True
            End If
            If Core.SoulPoints < StatusSoulPoints.Value Then
                Output &= " My SP is " & Core.SoulPoints & ", below " & StatusSoulPoints.Value & "."
                Alert = True
            End If
            Dim Capacity As Integer = 0
            Core.ReadMemory(Consts.ptrCapacity, Capacity, 4)
            If Capacity < StatusCapacity.Value Then
                Output &= " My Capacity is " & Core.SoulPoints & ", below " & StatusSoulPoints.Value & "."
                Alert = True
            End If
            If StatusConditionBurnt.Checked AndAlso (CurrentConditions And Conditions.Burnt) = Conditions.Burnt Then
                Output &= " I'm burnt."
                Alert = True
            End If
            If StatusConditionCombatSign.Checked AndAlso (CurrentConditions And Conditions.CombatSign) = Conditions.CombatSign Then
                Output &= " I'm PZ locked."
                Alert = True
            End If
            If StatusConditionDrowning.Checked AndAlso (CurrentConditions And Conditions.Drowning) = Conditions.Drowning Then
                Output &= " I'm drowning."
                Alert = True
            End If
            If StatusConditionElectrified.Checked AndAlso (CurrentConditions And Conditions.Electrified) = Conditions.Electrified Then
                Output &= " I'm electrified."
                Alert = True
            End If
            If StatusConditionParalized.Checked AndAlso (CurrentConditions And Conditions.Paralized) = Conditions.Paralized Then
                Output &= " I'm paralized."
                Alert = True
            End If
            If Alert Then
                If Core.TibiaWindowState <> ConstantsModule.WindowState.Active AndAlso Consts.FlashTaskbarWhenAlarmFires Then
                    Dim FWI As New Win32API.FlashWInfo(Core.Proxy.Client.MainWindowHandle, Win32API.FlashWFlags.FLASHW_TIMERNOFG Or Win32API.FlashWFlags.FLASHW_TRAY Or Win32API.FlashWFlags.FLASHW_CAPTION, 0, 0)
                    Win32API.FlashWindowEx(FWI)
                End If
                If Consts.MusicalNotesOnAlarm Then Core.Proxy.SendPacketToClient(MagicEffect(Core.CharacterLoc, MagicEffects.MusicalNotesYellow))
                If StatusPlaySound.Checked Then
                    Dim Sound As New Audio
                    Try
                        Sound.Play(Core.ExecutablePath & "\Alarms\Status.wav", AudioPlayMode.Background)
                    Catch
                    End Try
                End If
                Dim ChatMessage As ChatMessageDefinition
                ChatMessage.Message = Output
                ChatMessage.MessageType = MessageType.PM
                If StatusMessagePlayer.Checked Then
                    If STMessagePlayerInterval = 0 Then
                        If Not String.IsNullOrEmpty(StatusMessagePlayerName.Text) Then
                            ChatMessage.Destinatary = StatusMessagePlayerName.Text
                            Core.ChatMessageQueueList.Add(ChatMessage)
                        End If
                        STMessagePlayerInterval += 1
                    ElseIf STMessagePlayerInterval <= 15 Then
                        STMessagePlayerInterval += 1
                    Else
                        STMessagePlayerInterval = 0
                    End If
                End If
                If StatusLogOut.Checked Then
                    Log("Status Alarm", Output)
                    Log("Status Alarm", "Logging out.")
                    Core.Proxy.SendPacketToServer(PlayerLogout())
                End If

            End If
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub ItemsList_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ItemsList.SelectedIndexChanged
        Try
            If ItemsList.SelectedIndex > -1 Then
                ItemsCount.Enabled = True
                ItemsCondition.Enabled = True
                'ItemsCheckFloor.Enabled = True
                'ItemsCheckInventory.Enabled = True
                'ItemsApply.Enabled = True
                CanUpdate = False
                Select Case ItemsList.SelectedIndex
                    Case 0 'food
                        ItemsCondition.SelectedIndex = CInt(FoodCond.Condition)
                        ItemsCount.Value = FoodCond.Count

                        ItemsCheckFloor.Checked = FoodCond.CheckFloor
                        ItemsCheckInventory.Checked = FoodCond.CheckInventory
                    Case 1 'blanks
                        ItemsCondition.SelectedIndex = CInt(BlankRunesCond.Condition)
                        ItemsCount.Value = BlankRunesCond.Count
                        ItemsCheckFloor.Checked = BlankRunesCond.CheckFloor
                        ItemsCheckInventory.Checked = BlankRunesCond.CheckInventory
                    Case 2 'worms
                        ItemsCondition.SelectedIndex = WormsCond.Condition
                        ItemsCount.Value = WormsCond.Count
                        ItemsCheckFloor.Checked = WormsCond.CheckFloor
                        ItemsCheckInventory.Checked = WormsCond.CheckInventory
                    Case 3 'throwables
                        ItemsCondition.SelectedIndex = CInt(ThrowablesCond.Condition)
                        ItemsCount.Value = ThrowablesCond.Count
                        ItemsCheckFloor.Checked = ThrowablesCond.CheckFloor
                        ItemsCheckInventory.Checked = ThrowablesCond.CheckInventory
                    Case 4 'ammunition
                        ItemsCondition.SelectedIndex = CInt(AmmunitionCond.Condition)
                        ItemsCount.Value = AmmunitionCond.Count
                        ItemsCheckFloor.Checked = AmmunitionCond.CheckFloor
                        ItemsCheckInventory.Checked = AmmunitionCond.CheckInventory
                End Select
                CanUpdate = True
            Else
                ItemsCondition.Enabled = False
                ItemsCount.Enabled = False
                'ItemsCheckFloor.Enabled = False
                'ItemsCheckInventory.Enabled = False
                '            ItemsApply.Enabled = False
            End If
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub UpdateItemConditions()
        Try
            If Not CanUpdate Then Exit Sub
            Select Case ItemsList.SelectedIndex
                Case 0 'food
                    FoodCond.Active = ItemsList.GetItemChecked(0)
                    FoodCond.Condition = CType(ItemsCondition.SelectedIndex, LogicConditions)
                    FoodCond.Count = ItemsCount.Value
                    FoodCond.CheckFloor = ItemsCheckFloor.Checked
                    FoodCond.CheckInventory = ItemsCheckInventory.Checked
                Case 1 'blanks
                    BlankRunesCond.Active = ItemsList.GetItemChecked(1)
                    BlankRunesCond.Condition = CType(ItemsCondition.SelectedIndex, LogicConditions)
                    BlankRunesCond.Count = ItemsCount.Value
                    BlankRunesCond.CheckFloor = ItemsCheckFloor.Checked
                    BlankRunesCond.CheckInventory = ItemsCheckInventory.Checked
                Case 2 'worms
                    WormsCond.Active = ItemsList.GetItemChecked(2)
                    WormsCond.Condition = CType(ItemsCondition.SelectedIndex, LogicConditions)
                    WormsCond.Count = ItemsCount.Value
                    WormsCond.CheckFloor = ItemsCheckFloor.Checked
                    WormsCond.CheckInventory = ItemsCheckInventory.Checked
                Case 3 'throwables
                    ThrowablesCond.Active = ItemsList.GetItemChecked(3)
                    ThrowablesCond.Condition = CType(ItemsCondition.SelectedIndex, LogicConditions)
                    ThrowablesCond.Count = ItemsCount.Value
                    ThrowablesCond.CheckFloor = ItemsCheckFloor.Checked
                    ThrowablesCond.CheckInventory = ItemsCheckInventory.Checked
                Case 4 'ammunition
                    AmmunitionCond.Active = ItemsList.GetItemChecked(4)
                    AmmunitionCond.Condition = CType(ItemsCondition.SelectedIndex, LogicConditions)
                    AmmunitionCond.Count = ItemsCount.Value
                    AmmunitionCond.CheckFloor = ItemsCheckFloor.Checked
                    AmmunitionCond.CheckInventory = ItemsCheckInventory.Checked
            End Select
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub ItemsList_ItemCheck(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles ItemsList.ItemCheck
        Try
            Select Case e.Index
                Case 0 'food
                    FoodCond.Active = e.NewValue = CheckState.Checked
                Case 1 'blanks
                    BlankRunesCond.Active = e.NewValue = CheckState.Checked
                Case 2 'worms
                    WormsCond.Active = e.NewValue = CheckState.Checked
                Case 3 'throwables
                    ThrowablesCond.Active = e.NewValue = CheckState.Checked
                Case 4 'ammunition
                    AmmunitionCond.Active = e.NewValue = CheckState.Checked
            End Select
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub ItemsTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ItemsAlarmTimer.Tick
        Try
            Dim MyContainer As New Container
            Dim Item As ContainerItemDefinition
            Dim ContainerItemCount As Integer
            Dim Found As Boolean = False
            Dim FoodCount As Integer = 0
            Dim BlankRunesCount As Integer = 0
            Dim ThrowablesCount As Integer = 0
            Dim AmmunitionCount As Integer = 0
            Dim WormsCount As Integer = 0
            Dim Alert As Boolean = False
            Dim Output As String = ""
            If FoodCond.Active Then
                MyContainer.Reset()
                Do
                    If MyContainer.IsOpened Then
                        ContainerItemCount = MyContainer.GetItemCount
                        For I As Integer = 0 To ContainerItemCount - 1
                            Item = MyContainer.Items(I)
                            If Definitions.IsFood(Item.ID) Then
                                If Item.Count = 0 Then
                                    FoodCount += 1
                                Else
                                    FoodCount += Item.Count
                                End If
                            End If
                        Next
                    End If
                Loop While MyContainer.NextContainer
                Select Case FoodCond.Condition
                    Case LogicConditions.Equal
                        If FoodCount = FoodCond.Count Then Alert = True
                    Case LogicConditions.LessOrEqualThan
                        If FoodCount <= FoodCond.Count Then Alert = True
                    Case LogicConditions.LessThan
                        If FoodCount < FoodCond.Count Then Alert = True
                    Case LogicConditions.MoreOrEqualThan
                        If FoodCount >= FoodCond.Count Then Alert = True
                    Case LogicConditions.MoreThan
                        If FoodCount > FoodCond.Count Then Alert = True
                    Case LogicConditions.NotEqual
                        If FoodCount <> FoodCond.Count Then Alert = True
                End Select
                If Alert Then Output = "Your alarm has fired because you have " & FoodCount & " food items."
            End If
            If Not Alert AndAlso BlankRunesCond.Active Then
                BlankRunesCount = ContainerModule.Container.GetItemCountByItemID(Definitions.GetItemID("Blank"))
                Select Case BlankRunesCond.Condition
                    Case LogicConditions.Equal
                        If BlankRunesCount = BlankRunesCond.Count Then Alert = True
                    Case LogicConditions.LessOrEqualThan
                        If BlankRunesCount <= BlankRunesCond.Count Then Alert = True
                    Case LogicConditions.LessThan
                        If BlankRunesCount < BlankRunesCond.Count Then Alert = True
                    Case LogicConditions.MoreOrEqualThan
                        If BlankRunesCount >= BlankRunesCond.Count Then Alert = True
                    Case LogicConditions.MoreThan
                        If BlankRunesCount > BlankRunesCond.Count Then Alert = True
                    Case LogicConditions.NotEqual
                        If BlankRunesCount <> BlankRunesCond.Count Then Alert = True
                End Select
                If Alert Then Output = "Your alarm has fired because you have " & BlankRunesCount & " blank runes."
            End If
            If Not Alert AndAlso WormsCond.Active Then
                WormsCount = ContainerModule.Container.GetItemCountByItemID(Definitions.GetItemID("Worm"))
                Select Case WormsCond.Condition
                    Case LogicConditions.Equal
                        If WormsCount = WormsCond.Count Then Alert = True
                    Case LogicConditions.LessOrEqualThan
                        If WormsCount <= WormsCond.Count Then Alert = True
                    Case LogicConditions.LessThan
                        If WormsCount < WormsCond.Count Then Alert = True
                    Case LogicConditions.MoreOrEqualThan
                        If WormsCount >= WormsCond.Count Then Alert = True
                    Case LogicConditions.MoreThan
                        If WormsCount > WormsCond.Count Then Alert = True
                    Case LogicConditions.NotEqual
                        If WormsCount <> WormsCond.Count Then Alert = True
                End Select
                If Alert Then Output = "Your alarm has fired because you have " & WormsCount & " worms."
            End If
            If Not Alert AndAlso ThrowablesCond.Active Then
                MyContainer.Reset()
                Do
                    If MyContainer.IsOpened Then
                        ContainerItemCount = MyContainer.GetItemCount
                        For I As Integer = 0 To ContainerItemCount - 1
                            Item = MyContainer.Items(I)
                            If Definitions.IsThrowable(Item.ID) Then
                                If Item.Count = 0 Then
                                    ThrowablesCount += 1
                                Else
                                    ThrowablesCount += Item.Count
                                End If
                            End If
                        Next
                    End If
                Loop While MyContainer.NextContainer
                Select Case ThrowablesCond.Condition
                    Case LogicConditions.Equal
                        If ThrowablesCount = ThrowablesCond.Count Then Alert = True
                    Case LogicConditions.LessOrEqualThan
                        If ThrowablesCount <= ThrowablesCond.Count Then Alert = True
                    Case LogicConditions.LessThan
                        If ThrowablesCount < ThrowablesCond.Count Then Alert = True
                    Case LogicConditions.MoreOrEqualThan
                        If ThrowablesCount >= ThrowablesCond.Count Then Alert = True
                    Case LogicConditions.MoreThan
                        If ThrowablesCount > ThrowablesCond.Count Then Alert = True
                    Case LogicConditions.NotEqual
                        If ThrowablesCount <> ThrowablesCond.Count Then Alert = True
                End Select
                If Alert Then Output = "Your alarm has fired because you have " & ThrowablesCount & " throwables."
            End If
            If Not Alert AndAlso AmmunitionCond.Active Then
                MyContainer.Reset()
                Do
                    If MyContainer.IsOpened Then
                        ContainerItemCount = MyContainer.GetItemCount
                        For I As Integer = 0 To ContainerItemCount - 1
                            Item = MyContainer.Items(I)
                            If Definitions.IsAmmunition(Item.ID) Then
                                If Item.Count = 0 Then
                                    AmmunitionCount += 1
                                Else
                                    AmmunitionCount += Item.Count
                                End If
                            End If
                        Next
                    End If
                Loop While MyContainer.NextContainer
                Select Case AmmunitionCond.Condition
                    Case LogicConditions.Equal
                        If AmmunitionCount = AmmunitionCond.Count Then Alert = True
                    Case LogicConditions.LessOrEqualThan
                        If AmmunitionCount <= AmmunitionCond.Count Then Alert = True
                    Case LogicConditions.LessThan
                        If AmmunitionCount < AmmunitionCond.Count Then Alert = True
                    Case LogicConditions.MoreOrEqualThan
                        If AmmunitionCount >= AmmunitionCond.Count Then Alert = True
                    Case LogicConditions.MoreThan
                        If AmmunitionCount > AmmunitionCond.Count Then Alert = True
                    Case LogicConditions.NotEqual
                        If AmmunitionCount <> AmmunitionCond.Count Then Alert = True
                End Select
                If Alert Then Output = "Your alarm has fired because you have " & AmmunitionCount & " items of ammunition."
            End If
            If Alert Then
                If Core.TibiaWindowState <> ConstantsModule.WindowState.Active AndAlso Consts.FlashTaskbarWhenAlarmFires Then
                    Dim FWI As New Win32API.FlashWInfo(Core.Proxy.Client.MainWindowHandle, Win32API.FlashWFlags.FLASHW_TIMERNOFG Or Win32API.FlashWFlags.FLASHW_TRAY Or Win32API.FlashWFlags.FLASHW_CAPTION, 0, 0)
                    Win32API.FlashWindowEx(FWI)
                End If
                If Consts.MusicalNotesOnAlarm Then Core.Proxy.SendPacketToClient(MagicEffect(Core.CharacterLoc, MagicEffects.MusicalNotesGreen))
                If ItemsPlaySound.Checked Then
                    Dim Sound As New Audio
                    Try
                        Sound.Play(Core.ExecutablePath & "\Alarms\Items.wav", AudioPlayMode.Background)
                    Catch
                    End Try
                End If
                Dim ChatMessage As ChatMessageDefinition
                ChatMessage.Message = Output
                ChatMessage.MessageType = MessageType.PM
                If ItemsMessagePlayer.Checked Then
                    If ITMessagePlayerInterval = 0 Then
                        If Not String.IsNullOrEmpty(ItemsMessagePlayerName.Text) Then
                            ChatMessage.Destinatary = ItemsMessagePlayerName.Text
                            Core.ChatMessageQueueList.Add(ChatMessage)
                        End If
                        ITMessagePlayerInterval += 1
                    ElseIf ITMessagePlayerInterval <= 15 Then
                        ITMessagePlayerInterval += 1
                    Else
                        ITMessagePlayerInterval = 0
                    End If
                End If
                If ItemsLogOut.Checked Then
                    Log("Items Alarm", Output)
                    Log("Items Alarm", "Logging out.")
                    Core.Proxy.SendPacketToServer(PlayerLogout())
                End If
            End If
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub ItemsCheckInventory_CheckStateChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ItemsCheckInventory.CheckedChanged
        UpdateItemConditions()
    End Sub

    Private Sub ItemsCheckFloor_CheckStateChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ItemsCheckFloor.CheckedChanged
        UpdateItemConditions()
    End Sub

    Private Sub ItemsCondition_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ItemsCondition.SelectedIndexChanged
        UpdateItemConditions()
    End Sub

    Private Sub ItemsCount_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ItemsCount.ValueChanged
        UpdateItemConditions()
    End Sub

    Private Sub frmAlarms_Activated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        Try
            Me.Text = "Alarms for " & Core.Proxy.CharacterName
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub BattlelistMultiFloor_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BattlelistMultiFloor.CheckedChanged
        Try
            MultiFloorGroupBox.Enabled = BattlelistMultiFloor.Checked
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

End Class