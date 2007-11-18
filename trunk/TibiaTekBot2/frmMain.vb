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
'    Boston, MA 02111-1307, USA.

Imports System.Windows, TibiaTekBot.PProxy2, System.Runtime.InteropServices, _
    System.ComponentModel, System.IO, System.Xml, System.Text.RegularExpressions

Public Class frmMain

    Dim LoginSelectForm As frmLoginSelectDialog
    Dim SC As frmSplashScreen
    Dim IsVisible As Boolean = True
    Public LoginServer As String

    Private Sub frmMain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            NotifyIcon.Visible = False
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            SC = New frmSplashScreen
            SC.ShowDialog()
            LoadTibiaEXE()
            System.GC.Collect()
            InitializeControls()
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub InitializeControls()
        Try
            ' Spell Caster
            For Each Spell As SpellDefinition In CoreModule.Spells.SpellsList
                If Spell.Kind <> SpellKind.Rune Then
                    SpellCasterSpell.Items.Add(Spell.Words)
                End If
            Next
            ' Runemaker
            For Each Spell As SpellDefinition In CoreModule.Spells.SpellsList
                If Spell.Kind = SpellKind.Rune Then
                    RunemakerSpell.Items.Add(Spell.Name)
                End If
            Next
            ' Auto Eater
            AutoEaterInterval.Value = Consts.AutoEaterInterval
            AutoEaterEatFromFloor.Checked = Consts.EatFromFloor
            AutoEaterEatFromFloorFirst.Checked = Consts.EatFromFloorFirst
            ' Auto Looter
            AutoLooterMinCap.Value = Consts.CavebotLootMinCap

        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub frmMain_Closing(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Try
            If MessageBox.Show("Are you sure to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Forms.DialogResult.Yes Then
                Me.NotifyIcon.Visible = False
                e.Cancel = False
            Else
                e.Cancel = True
            End If
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub frmMain_Activated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        Try
            If Core.InGame Then
                Me.Text = "TibiaTek Bot - " & Core.Proxy.CharacterName
                FunctionsToolStripMenuItem.Enabled = True
                AboutToolStripMenuItem.Enabled = True
                RefreshControls()
                MainTabControl.Enabled = True
            Else
                If Not (Core.Proxy Is Nothing OrElse Core.Proxy.Client Is Nothing) Then
                    Me.Text = "TibiaTek Bot - " & Hex(Core.Proxy.Client.Handle.ToString)
                Else
                    Me.Text = "TibiaTek Bot"
                End If
                FunctionsToolStripMenuItem.Enabled = False
                AboutToolStripMenuItem.Enabled = False
                MainTabControl.Enabled = False
            End If
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub RefreshControls()
        Try
            RefreshSpellCasterControls()
            RefreshRunemakerControls()
            RefreshAutoEaterControls()
            RefreshAutoLooterControls()
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub RefreshSpellCasterControls()
        Try
            SpellCasterTrigger.Checked = Core.SpellTimerObj.State = ThreadTimerState.Running
            If SpellCasterTrigger.Checked Then
                SpellCasterSpell.Text = Core.SpellMsg
                SpellCasterMinimumManaPoints.Value = Core.SpellManaRequired
                SpellCasterSpell.Enabled = False
                SpellCasterMinimumManaPoints.Enabled = False
            Else
                SpellCasterSpell.Enabled = True
                SpellCasterMinimumManaPoints.Enabled = True
            End If
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub RefreshAutoLooterControls()
        Try
            AutoLooterTrigger.Checked = Core.LooterTimerObj.State = ThreadTimerState.Running
            If AutoLooterTrigger.Checked Then
                AutoLooterMinCap.Value = Core.LooterMinimumCapacity
                AutoLooterMinCap.Enabled = False
                AutoLooterEdit.Enabled = False
            Else
                AutoLooterMinCap.Enabled = True
                AutoLooterEdit.Enabled = True
            End If
        Catch ex As Exception
            MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub RefreshRunemakerControls()
        Try
            RunemakerTrigger.Checked = Core.RunemakerTimerObj.State = ThreadTimerState.Running
            If RunemakerTrigger.Checked Then
                RunemakerSpell.Text = Core.RunemakerSpell.Name
                RunemakerMinimumManaPoints.Value = Core.RunemakerManaPoints
                RunemakerMinimumSoulPoints.Value = Core.RunemakerSoulPoints
                RunemakerSpell.Enabled = False
                RunemakerMinimumManaPoints.Enabled = False
                RunemakerMinimumSoulPoints.Enabled = False
            Else
                RunemakerSpell.Enabled = True
                RunemakerMinimumManaPoints.Enabled = True
                RunemakerMinimumSoulPoints.Enabled = True
            End If
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub RefreshAutoEaterControls()
        Try
            AutoEaterTrigger.Checked = Core.EaterTimerObj.State = ThreadTimerState.Running
            If AutoEaterTrigger.Checked Then
                AutoEaterSmart.Checked = Core.AutoEaterSmart > 0
                If AutoEaterSmart.Checked Then
                    AutoEaterMinimumHitPoints.Value = Core.AutoEaterSmart
                    'AutoEaterDelay.Value = Consts.AutoEaterSmartInterval
                Else
                    AutoEaterMinimumHitPoints.Value = 0
                    'AutoEaterDelay.Value = Consts.AutoEaterInterval
                End If
                AutoEaterEatFromFloor.Enabled = False
                AutoEaterEatFromFloorFirst.Enabled = False
                AutoEaterSmart.Enabled = False
                AutoEaterMinimumHitPoints.Enabled = False
                AutoEaterInterval.Enabled = False
                AutoEaterInterval.Value = CInt(Core.EaterTimerObj.Interval)
            Else
                AutoEaterEatFromFloor.Enabled = True
                AutoEaterEatFromFloorFirst.Enabled = True
                AutoEaterSmart.Enabled = True
                AutoEaterMinimumHitPoints.Enabled = True
                AutoEaterInterval.Enabled = True
            End If
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

#Region " Load Stuff "

    Public Sub LoadTibiaEXE()
        Dim xmlFile As New System.Xml.XmlDocument()
        Dim strFilename As String = ""
        Dim strDirectory As String = ""
        'Dim strPort As String = ""
        Dim blIsValid As Boolean = False
        Try
            For I As Integer = Application.ExecutablePath.Length - 1 To 0 Step -1
                If Application.ExecutablePath.Chars(I) = "\" Then
                    Core.ExecutablePath = Strings.Left(Application.ExecutablePath, I)
                    Exit For
                End If
            Next
            Do
                If IO.File.Exists(GetConfigurationDirectory() & "\Data.xml") Then
                    xmlFile.Load(GetConfigurationDirectory() & "\Data.xml")
                    strFilename = xmlFile.SelectSingleNode("Client/Filename").InnerText
                    strDirectory = xmlFile.SelectSingleNode("Client/Directory").InnerText
                    If IO.File.Exists(strDirectory & "\" & strFilename) Then
                        blIsValid = True
                    Else
                        CreateLoadXML()
                    End If
                Else
                    CreateLoadXML()
                End If
            Loop Until blIsValid = True
            Me.NotifyIcon.Visible = False
            LoginSelectForm = New frmLoginSelectDialog()
            If LoginSelectForm.ShowDialog() <> Forms.DialogResult.OK Then End
            Core.Proxy = New PProxy2(strFilename, strDirectory)
            Core.TibiaDirectory = strDirectory
            Core.TibiaFilename = strFilename
            DatInfo = New DatReader(strDirectory & "\tibia.dat")
            If Core.Proxy.Exists Then
                Me.NotifyIcon.Visible = True
                System.Threading.Thread.Sleep(1000)
                Core.WindowTimerObj.StartTimer()
                Dim TempInt As Integer = 0
                Do
                    Core.ReadMemory(Consts.ptrServerAddressBegin, TempInt, 1)
                Loop Until TempInt <> 0
                For I As Integer = 0 To Consts.ServerAddressCount - 1
                    Core.WriteMemory(Consts.ptrServerAddressBegin + (Consts.ServerAddressDist * I), "127.0.0.1")
                    Core.WriteMemory(Consts.ptrServerPortBegin + (Consts.ServerAddressDist * I), Core.Proxy.sckLListen.LocalPort, 2)
                Next
                Core.Proxy.LoginPort = Core.LoginPort
                Core.TibiaClientStateTimerObj.StartTimer()
                Core.ReadMemory(Consts.ptrFrameRateBegin, Core.FrameRateBegin, 4)
                InjectLastAttackedId()
                If Core.IsPrivateServer Then
                    Dim Temp As Integer = 0
                    Win32API.VirtualProtectEx(Core.Proxy.Client.Handle, Consts.ptrRSAKey, Consts.RSAKeyOpenTibia.Length, &H40, Temp)
                    Core.WriteMemory(Consts.ptrRSAKey, Consts.RSAKeyOpenTibia)
                End If
            End If
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Public Sub CreateLoadXML()
        Try
            Dim Filename As String = ""
            Dim Directory As String = ""
            Dim dlgOpen As New OpenFileDialog
            Dim Document As New XmlDocument()
            Dim xmlDeclaration As XmlDeclaration = Document.CreateXmlDeclaration("1.0", "us-ascii", "")
            Dim xmlComment As XmlComment = Document.CreateComment(GNUGPLStatement)
            Dim xmlClient As XmlElement
            Dim xmlFilename As XmlElement
            Dim xmlDirectory As XmlElement
            Dim xmlAddresses As XmlElement
            Dim xmlAddress1 As XmlElement
            Dim xmlAddress2 As XmlElement
            Dim xmlAddress3 As XmlElement
            Dim xmlAddress4 As XmlElement
            Dim xmlAddress5 As XmlElement
            Dim xmlAddress6 As XmlElement
            Dim xmlAddress7 As XmlElement
            Dim xmlAddress8 As XmlElement
            Dim xmlAddress9 As XmlElement
            Dim xmlAddress10 As XmlElement
            Dim xmlAddress11 As XmlElement
            With dlgOpen
                .Title = "Tibia's Location"
                .Filter = "Executable|*.exe"
            End With
            If MessageBox.Show("Please find the location of your Tibia Client.", "Notice", MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) = Forms.DialogResult.Cancel Then
                End
            End If
            If dlgOpen.ShowDialog() = Forms.DialogResult.Cancel Then
                End
            End If
            For I As Integer = dlgOpen.FileName.Length - 1 To 0 Step -1
                If dlgOpen.FileName.Chars(I) = "\" Then
                    Directory = Strings.Left(dlgOpen.FileName, I)
                    Filename = Strings.Right(dlgOpen.FileName, dlgOpen.FileName.Length - I - 1)
                    Exit For
                End If
            Next
            If Directory = "" Or Filename = "" Then Exit Sub
            xmlClient = Document.CreateElement("Client")
            xmlFilename = Document.CreateElement("Filename")
            xmlFilename.InnerText = Filename
            xmlDirectory = Document.CreateElement("Directory")
            xmlDirectory.InnerText = Directory
            xmlAddresses = Document.CreateElement("Addresses")
            xmlAddress1 = Document.CreateElement("Address")
            xmlAddress1.InnerText = "login01.tibia.com:7171"
            xmlAddress2 = Document.CreateElement("Address")
            xmlAddress2.InnerText = "login02.tibia.com:7171"
            xmlAddress3 = Document.CreateElement("Address")
            xmlAddress3.InnerText = "login03.tibia.com:7171"
            xmlAddress4 = Document.CreateElement("Address")
            xmlAddress4.InnerText = "login04.tibia.com:7171"
            xmlAddress5 = Document.CreateElement("Address")
            xmlAddress5.InnerText = "login05.tibia.com:7171"
            xmlAddress6 = Document.CreateElement("Address")
            xmlAddress6.InnerText = "tibia1.cipsoft.com:7171"
            xmlAddress7 = Document.CreateElement("Address")
            xmlAddress7.InnerText = "tibia2.cipsoft.com:7171"
            xmlAddress8 = Document.CreateElement("Address")
            xmlAddress8.InnerText = "tibia3.cipsoft.com:7171"
            xmlAddress9 = Document.CreateElement("Address")
            xmlAddress9.InnerText = "tibia4.cipsoft.com:7171"
            xmlAddress10 = Document.CreateElement("Address")
            xmlAddress10.InnerText = "tibia5.cipsoft.com:7171"
            xmlAddress11 = Document.CreateElement("Address")
            xmlAddress11.InnerText = "localhost:7171"
            xmlAddresses.AppendChild(xmlAddress1)
            xmlAddresses.AppendChild(xmlAddress2)
            xmlAddresses.AppendChild(xmlAddress3)
            xmlAddresses.AppendChild(xmlAddress4)
            xmlAddresses.AppendChild(xmlAddress5)
            xmlAddresses.AppendChild(xmlAddress6)
            xmlAddresses.AppendChild(xmlAddress7)
            xmlAddresses.AppendChild(xmlAddress8)
            xmlAddresses.AppendChild(xmlAddress9)
            xmlAddresses.AppendChild(xmlAddress10)
            xmlAddresses.AppendChild(xmlAddress11)
            Document.AppendChild(xmlDeclaration)
            xmlClient.AppendChild(XmlComment)
            xmlClient.AppendChild(xmlFilename)
            xmlClient.AppendChild(xmlDirectory)
            xmlClient.AppendChild(xmlAddresses)
            Document.AppendChild(xmlClient)
            If IO.File.Exists(GetConfigurationDirectory() & "\Data.xml") Then IO.File.Delete(GetConfigurationDirectory() & "\Data.xml")
            Document.Save(GetConfigurationDirectory() & "\Data.xml")
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

#End Region

#Region " Popup Menu "

    Private Sub PopupMenu_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles PopupMenu.Opening
        Try
            If Core.CaveBotTimerObj.State = ThreadTimerState.Running Then
                CavebotMenuItem.Enabled = False
            Else
                CavebotMenuItem.Enabled = True
            End If
            If Core.TibiaClientIsVisible Then
                ShowHideTibiaWindow.Name = "Hide Tibia Window"
            Else
                ShowHideTibiaWindow.Name = "Show Tibia Window"
            End If
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub CloseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClosePopupItem.Click
        Try
            If MessageBox.Show("Are you sure to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Forms.DialogResult.Yes Then
                Try
                    If Not Core.Proxy Is Nothing Then
                        If Not Core.Proxy.Client Is Nothing Then
                            Core.Proxy.Client.CloseMainWindow()
                            Me.NotifyIcon.Visible = False
                        End If
                    End If
                Catch
                Finally
                    End
                End Try
            End If
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub AlarmsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AlarmsToolStripMenuItem.Click
        Try
            If Not Core.InGame Then
                Beep()
                Exit Sub
            End If
            Core.AlarmsForm.Show()
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub ShowHideTibiaWindow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowHideTibiaWindow.Click
        Try
            If Not Core.Proxy Is Nothing Then
                If Not Core.Proxy.Client Is Nothing Then
                    If Core.Proxy.Client.MainWindowHandle = 0 Then Exit Sub
                    If Core.TibiaClientIsVisible Then
                        Win32API.ShowWindow(Core.Proxy.Client.MainWindowHandle, Win32API.ShowState.SW_HIDE)
                    Else
                        Win32API.ShowWindow(Core.Proxy.Client.MainWindowHandle, Win32API.ShowState.SW_SHOW)
                    End If
                    Core.TibiaClientIsVisible = Not Core.TibiaClientIsVisible
                End If
            End If
        Catch
        Finally
        End Try
    End Sub

    Private Sub ConstantsEditorMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConstantsEditorMenuItem.Click
        Try
            Core.ConstantsEditorForm.Show()
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub MCPatchMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MCPatchMenuItem.Click, PatchToolStripMenuItem.Click
        Try
            System.IO.File.Copy(Core.TibiaDirectory & "\" & Core.TibiaFilename, Core.TibiaDirectory & "\_Tibia.exe.tmp")
            Dim FSR As New FileStream(Core.TibiaDirectory & "\_Tibia.exe.tmp", FileMode.Open, FileAccess.Read)
            Dim FSW As New FileStream(Core.TibiaDirectory & "\TibiaMC.exe", FileMode.OpenOrCreate, FileAccess.Write)
            Dim Reader As New BinaryReader(FSR)
            Dim Writer As New BinaryWriter(FSW)
            Dim CurrentByte As Byte = 0
            Try
                Do
                    CurrentByte = Reader.ReadByte()
                    If FSW.Position = Consts.MCPatchOffset Then
                        CurrentByte = CByte(Consts.MCPatchReplacement)
                    End If
                    Writer.Write(CurrentByte)
                Loop While True
            Catch
            End Try
            Writer.Close()
            Reader.Close()
            FSR.Close()
            FSW.Close()
            MessageBox.Show("The new Tibia Client with Multi-Client is now saved at: " & Core.TibiaDirectory & "\" & "TibiaMC.exe")

            Dim Result As DialogResult = MessageBox.Show("Would you like to use the patched Tibia Client the next time you open TibiaTek Bot?", "Information", MessageBoxButtons.YesNo)
            If Result = Forms.DialogResult.Yes Then
                Dim Document As New Xml.XmlDocument()
                Document.Load(GetConfigurationDirectory() & "\Data.xml")
                Document.Item("Client").Item("Filename").InnerText = "TibiaMC.exe"
                Document.Save(GetConfigurationDirectory() & "\Data.xml")
            End If
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        Finally
            If System.IO.File.Exists(Core.TibiaDirectory & "\_Tibia.exe.tmp") Then
                System.IO.File.Delete(Core.TibiaDirectory & "\_Tibia.exe.tmp")
            End If
        End Try
    End Sub

    Private Sub CavebotMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CavebotMenuItem.Click
        Try
            If Not Core.InGame Then
                Beep()
                Exit Sub
            End If
            If Core.CaveBotTimerObj.State = ThreadTimerState.Running Then
                MessageBox.Show("Cavebot is currently running.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If
            Core.CavebotForm.Show()
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub CharacterStatisticsMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CharacterStatisticsMenuItem.Click
        Try
            If Not Core.InGame Then
                Beep()
                Exit Sub
            End If
            Core.CharacterStatisticsForm.Show()
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub ShowHideToolMenuMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowHideToolStripMenuItem.Click
        Try
            If IsVisible Then
                Me.Hide()
                IsVisible = False
            Else
                Me.Show()
                IsVisible = True
            End If
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub changeloginserver_click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChangeLoginServerPopupItem.Click
        Try
            If Not Core.InGame Then
                LoginSelectForm = New frmLoginSelectDialog()
                If LoginSelectForm.ShowDialog() <> Forms.DialogResult.OK Then Exit Sub
                For I As Integer = 0 To 3
                    Core.WriteMemory(Consts.ptrServerAddressBegin + (Consts.ServerAddressDist * I), "localhost")
                    Core.WriteMemory(Consts.ptrServerPortBegin + (Consts.ServerAddressDist * I), Core.Proxy.sckLListen.LocalPort, 2)
                Next
                If Core.IsPrivateServer Then
                    Dim Temp As Integer = 0
                    Win32API.VirtualProtectEx(Core.Proxy.Client.Handle, Consts.ptrRSAKey, Consts.RSAKeyOpenTibia.Length, &H40, Temp)
                    Core.WriteMemory(Consts.ptrRSAKey, Consts.RSAKeyOpenTibia)
                End If
            Else
                MessageBox.Show("You are currently in-game.")
            End If
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

#End Region

#Region " Tool Menu "

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        If MessageBox.Show("Are you sure to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Forms.DialogResult.Yes Then
            Try
                If Not Core.Proxy Is Nothing Then
                    If Not Core.Proxy.Client Is Nothing Then
                        Core.Proxy.Client.CloseMainWindow()
                        Me.NotifyIcon.Visible = False
                    End If
                End If
            Catch
            Finally
                End
            End Try
        End If
    End Sub

    Private Sub WebsiteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WebsiteToolStripMenuItem.Click
        CommandParser("website")
    End Sub

    Private Sub LoadToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        CommandParser("hotkeys load")
    End Sub

    Private Sub SaveToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        CommandParser("hotkeys save")
    End Sub

    Private Sub EditToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditToolStripMenuItem.Click
        CommandParser("config load")
    End Sub

    Private Sub LoadToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoadToolStripMenuItem.Click
        CommandParser("config edit")
    End Sub

    Private Sub ClearToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClearToolStripMenuItem.Click
        CommandParser("config clear")
    End Sub

    Private Sub OnToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OnToolStripMenuItem.Click
        Dim Res As String = InputBox("Enter the minimum capacity. Example: 20.", "Auto Looter Minimum Capacity", "0")
        CommandParser("loot " & Res)
    End Sub

    Private Sub EditToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditToolStripMenuItem1.Click
        CommandParser("loot edit")
    End Sub

    Private Sub OffToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OffToolStripMenuItem.Click
        CommandParser("loot off")
    End Sub

    Private Sub OnToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OnToolStripMenuItem1.Click
        CommandParser("stacker on")
    End Sub

    Private Sub OffToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OffToolStripMenuItem1.Click
        CommandParser("stacker off")
    End Sub

    Private Sub UtToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UtToolStripMenuItem.Click
        CommandParser("light on")
    End Sub

    Private Sub TorchToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TorchToolStripMenuItem.Click
        CommandParser("light torch")
    End Sub

    Private Sub GreatTorchToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GreatTorchToolStripMenuItem.Click
        CommandParser("light great torch")
    End Sub

    Private Sub UltimateTorchToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UltimateTorchToolStripMenuItem.Click
        CommandParser("light ultimate torch")
    End Sub

    Private Sub UtevoLuxToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UtevoLuxToolStripMenuItem.Click
        CommandParser("light utevo lux")
    End Sub

    Private Sub UtevoGranLuxToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UtevoGranLuxToolStripMenuItem.Click
        CommandParser("light utevo gran lux")
    End Sub

    Private Sub UtevoVisLuxToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UtevoVisLuxToolStripMenuItem.Click
        CommandParser("light utevo vis lux")
    End Sub

    Private Sub LightWandToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LightWandToolStripMenuItem.Click
        CommandParser("light light wand")
    End Sub

    Private Sub OffToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OffToolStripMenuItem2.Click
        CommandParser("light off")
    End Sub

    Private Sub OnToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OnToolStripMenuItem2.Click
        Dim Val As String = InputBox("Enter the minimum ammunition (Less than 100). Example: 50.", "Minimum Ammunition", "50")
        CommandParser("ammorestacker " & Val)
    End Sub

    Private Sub OffToolStripMenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OffToolStripMenuItem3.Click
        CommandParser("ammorestacker off")
    End Sub

    Private Sub CommandsListToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CommandsListToolStripMenuItem.Click
        CommandParser("list")
    End Sub

    Private Sub OnToolStripMenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OnToolStripMenuItem3.Click
        Dim number As String = InputBox("Enter the minimum mana points to cast the spell. Example: 100.", "Minimum Mana Points")
        Dim spell As String = InputBox("Enter the spell words. Example: eXuRa """"HeAl pLx.", "Spell Words")
        CommandParser("spell " & number & " """ & spell)
    End Sub

    Private Sub OffToolStripMenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OffToolStripMenuItem4.Click
        CommandParser("spell off")
    End Sub

    Private Sub OnToolStripMenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OnToolStripMenuItem4.Click
        Dim res As String = InputBox("Enter the delay in seconds to eat. Example: 30.", "Auto Eater Delay", "30")
        CommandParser("eat " & res)
    End Sub

    Private Sub OffToolStripMenuItem5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OffToolStripMenuItem5.Click
        CommandParser("eat off")
    End Sub

    Private Sub OnToolStripMenuItem5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OnToolStripMenuItem5.Click
        Dim mp As String = InputBox("Enter the minimum mana points to conjure the spell words. Example: 400.", "Minimum Mana Points")
        Dim sp As String = InputBox("Enter the minimum soul points to conjure the spell words. Example: 3.", "Minimum Soul Points")
        Dim sw As String = InputBox("Enter the spell words or the spell name. Example: great fireball.", "Spell Words/Spell Name")
        CommandParser("runemaker " & mp & " " & sp & " """ & sw & """")
    End Sub

    Private Sub OffToolStripMenuItem6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OffToolStripMenuItem6.Click
        CommandParser("runemaker off")
    End Sub

    Private Sub OnToolStripMenuItem6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OnToolStripMenuItem6.Click
        Dim mincap As String = InputBox("Enter the minimum capacity to fish. Example: 6.", "Minimum Capacity")
        CommandParser("fisher " & mincap)
    End Sub

    Private Sub OffToolStripMenuItem7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OffToolStripMenuItem7.Click
        CommandParser("fisher off")
    End Sub

    Private Sub TurboToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TurboToolStripMenuItem.Click
        Dim mincap As String = InputBox("Enter the minimum capacity to fish. Example: 6.", "Minimum Capacity")
        CommandParser("fisher " & mincap & " turbo")
    End Sub

    Private Sub OnToolStripMenuItem7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OnToolStripMenuItem7.Click
        Dim advertisement As String = InputBox("Enter your advertisement. Example: sell 10 bp of uh ~ thais.", "Advertisement")
        CommandParser("advertise " & advertisement)
    End Sub

    Private Sub OffToolStripMenuItem8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OffToolStripMenuItem8.Click
        CommandParser("advertise off")
    End Sub

    Private Sub OnToolStripMenuItem8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OnToolStripMenuItem8.Click
        Dim reg As String = InputBox("Enter the regular expression pattern to match. Example: bps? of uh.", "Regular Expression Pattern")
        CommandParser("watch " & reg)
    End Sub

    Private Sub OffToolStripMenuItem9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OffToolStripMenuItem9.Click
        CommandParser("watch off")
    End Sub

    Private Sub EventsLoggingToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EventsLoggingToolStripMenuItem.Click
        CommandParser("log on")
    End Sub

    Private Sub OffToolStripMenuItem10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OffToolStripMenuItem10.Click
        CommandParser("log off")
    End Sub

    Private Sub OnToolStripMenuItem10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OnToolStripMenuItem10.Click
        CommandParser("cavebot on")
    End Sub

    Private Sub OffToolStripMenuItem11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OffToolStripMenuItem11.Click
        CommandParser("cavebot off")
    End Sub

    Private Sub OnToolStripMenuItem11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OnToolStripMenuItem11.Click
        CommandParser("statsuploader on")
    End Sub

    Private Sub OffToolStripMenuItem12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OffToolStripMenuItem12.Click
        CommandParser("statsuploader off")
    End Sub

    Private Sub OnToolStripMenuItem12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OnToolStripMenuItem12.Click
        CommandParser("fpschanger on")
    End Sub

    Private Sub OffToolStripMenuItem13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OffToolStripMenuItem13.Click
        CommandParser("fpschanger off")
    End Sub

    Private Sub OnToolStripMenuItem13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OnToolStripMenuItem13.Click
        Dim mhp As String = InputBox("Enter the minimum hit points, or the minimum hit points percent. Example: 50%. Example: 500.", "Minimum Hitpoints")
        Dim sw As String = InputBox("Enter the spell words or spell name.", "Spell Name/Spell Words")
        CommandParser("heal " & mhp & " """ & sw & """")
    End Sub

    Private Sub OffToolStripMenuItem14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OffToolStripMenuItem14.Click
        CommandParser("heal off")
    End Sub

    Private Sub OnToolStripMenuItem14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OnToolStripMenuItem14.Click
        Dim mhp As String = InputBox("Enter the minimum hit points. Example: 500.", "Minimum Hit Points")
        CommandParser("uh " & mhp)
    End Sub

    Private Sub OffToolStripMenuItem15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OffToolStripMenuItem15.Click
        CommandParser("uh off")
    End Sub

    Private Sub OnToolStripMenuItem15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OnToolStripMenuItem15.Click
        Dim mhp As String = InputBox("Enter the minimum hit points percent. Example: 50%.", "Minimum Hit Points Percent")
        Dim sw As String = InputBox("Enter the way of healing: exura sio, adura vita, both. Example: exura sio.", "Spell Name/Spell Words")
        Dim frnd As String = InputBox("Enter your friend's name. Example: Eternal Oblivion.", "Friend's Name")
        CommandParser("healfriend " & mhp & " """ & sw & """ """ & frnd & """")
    End Sub

    Private Sub OffToolStripMenuItem16_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OffToolStripMenuItem16.Click
        CommandParser("healfriend off")
    End Sub

    Private Sub OnToolStripMenuItem16_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OnToolStripMenuItem16.Click
        Dim mhp As String = InputBox("Enter the minimum hit points percent. Example: 50%.", "Minimum Hit Points Percent")
        Dim sw As String = InputBox("Enter the way of healing: exura sio, adura vita, both. Example: exura sio.", "Spell Name/Spell Words")
        CommandParser("healparty " & mhp & " """ & sw & """")
    End Sub

    Private Sub OffToolStripMenuItem17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OffToolStripMenuItem17.Click
        CommandParser("healparty off")
    End Sub

    Private Sub OnToolStripMenuItem17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OnToolStripMenuItem17.Click
        Dim mp As String = InputBox("Enter the minimum mana points. Example: 100.", "Minimum Mana Points")
        CommandParser("drinker " & mp)
    End Sub

    Private Sub OffToolStripMenuItem18_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OffToolStripMenuItem18.Click
        CommandParser("drinker off")
    End Sub

    Private Sub OnToolStripMenuItem22_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OnToolStripMenuItem22.Click
        CommandParser("exp on")
    End Sub

    Private Sub OffToolStripMenuItem23_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OffToolStripMenuItem23.Click
        CommandParser("exp off")
    End Sub

    Private Sub CharacterInformationLookupToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CharacterInformationLookupToolStripMenuItem.Click
        Dim name As String = InputBox("Enter the player's character name. Example: Eternal Oblivion.", "Player's Character Name")
        CommandParser("char """ & name & """")
    End Sub

    Private Sub GuildMembersLookupToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GuildMembersLookupToolStripMenuItem.Click
        Dim gname As String = InputBox("Enter the guild name (case sensitive). Example: Lost Souls.", "Guild Name")
        Dim result As DialogResult = MessageBox.Show("Do you want to show off-line guild members as well?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
        Dim sel As String
        If result = Forms.DialogResult.Yes Then
            sel = "both"
        Else
            sel = "online"
        End If
        CommandParser("guild " & sel & " """ & gname & """")
    End Sub

    Private Sub UpToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UpToolStripMenuItem.Click
        CommandParser("look up")
    End Sub

    Private Sub AroundToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AroundToolStripMenuItem.Click
        CommandParser("look around")
    End Sub

    Private Sub BelowToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BelowToolStripMenuItem.Click
        CommandParser("look down")
    End Sub

    Private Sub OnToolStripMenuItem21_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OnToolStripMenuItem21.Click
        CommandParser("namespy on")
    End Sub

    Private Sub OffToolStripMenuItem22_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OffToolStripMenuItem22.Click
        CommandParser("namespy off")
    End Sub

    Private Sub FileToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FileToolStripMenuItem.Click
        Dim file As String = InputBox("Enter the filepath of the application or filename like in MS-DOS. Example: notepad.", "Filepath")
        CommandParser("open """ & file & """")
    End Sub

    Private Sub TibiawikiToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TibiawikiToolStripMenuItem.Click
        Dim searchterms As String = InputBox("Enter the search terms. Example: apple.", "Seach Terms")
        CommandParser("open wiki """ & searchterms)
    End Sub

    Private Sub CharacterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CharacterToolStripMenuItem.Click
        Dim searchterms As String = InputBox("Enter the search terms. Example: apple.", "Seach Terms")
        CommandParser("open character """ & searchterms)
    End Sub

    Private Sub GuildToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GuildToolStripMenuItem.Click
        Dim searchterms As String = InputBox("Enter the search terms. Example: apple.", "Seach Terms")
        CommandParser("open guild """ & searchterms)
    End Sub

    Private Sub ErignetHighscorePagesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ErignetHighscorePagesToolStripMenuItem.Click
        Dim searchterms As String = InputBox("Enter the search terms. Example: apple.", "Seach Terms")
        CommandParser("open erig """ & searchterms)
    End Sub

    Private Sub GoogleToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GoogleToolStripMenuItem.Click
        Dim searchterms As String = InputBox("Enter the search terms. Example: apple.", "Seach Terms")
        CommandParser("open google """ & searchterms)
    End Sub

    Private Sub MytibiacomToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MytibiacomToolStripMenuItem.Click
        Dim searchterms As String = InputBox("Enter the search terms. Example: apple.", "Seach Terms")
        CommandParser("open mytibia """ & searchterms)
    End Sub


    Private Sub SendLocationToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SendLocationToolStripMenuItem.Click
        Dim pname As String = InputBox("Enter your friend's name.", "Friend's Name")
        CommandParser("sendlocation """ & pname)
    End Sub

    Private Sub GetItemIDsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GetItemIDsToolStripMenuItem.Click
        CommandParser("getitemid")
    End Sub

    Private Sub FeedbackToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FeedbackToolStripMenuItem.Click
        CommandParser("feedback")
    End Sub

    Private Sub OnToolStripMenuItem23_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OnToolStripMenuItem23.Click
        CommandParser("attack on")
    End Sub

    Private Sub AutoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AutoToolStripMenuItem.Click
        CommandParser("attack auto")
    End Sub

    Private Sub StandToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StandToolStripMenuItem.Click
        CommandParser("attack stand")
    End Sub

    Private Sub FollowToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FollowToolStripMenuItem.Click
        CommandParser("attack follow")
    End Sub

    Private Sub OffensiveToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OffensiveToolStripMenuItem.Click
        CommandParser("attack offensive")
    End Sub

    Private Sub BalancedToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BalancedToolStripMenuItem.Click
        CommandParser("attack balanced")
    End Sub

    Private Sub DefensiveToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DefensiveToolStripMenuItem.Click
        CommandParser("attack defensive")
    End Sub

    Private Sub OffToolStripMenuItem26_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OffToolStripMenuItem26.Click
        CommandParser("attack off")
    End Sub

    Private Sub OnToolStripMenuItem24_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OnToolStripMenuItem24.Click
        Dim minhp As String = InputBox("Enter the minimum hit points percent to stop attacking. Example: 50%.", "Minimum Hitpoints Percent")
        Dim maxhp As String = InputBox("Enter the maximum hit points percent to resume attacking. Example: 90%.", "Maximum Hitpoints Percent")
        CommandParser("trainer " & minhp & " " & maxhp)
    End Sub

    Private Sub AddToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddToolStripMenuItem.Click
        CommandParser("trainer add")
    End Sub

    Private Sub RemoveToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemoveToolStripMenuItem.Click
        CommandParser("trainer remove")
    End Sub

    Private Sub ClearToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClearToolStripMenuItem1.Click
        CommandParser("trainer clear")
    End Sub

    Private Sub OffToolStripMenuItem24_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OffToolStripMenuItem24.Click
        CommandParser("trainer off")
    End Sub

    Private Sub OnToolStripMenuItem25_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OnToolStripMenuItem25.Click
        CommandParser("pickup on")
    End Sub

    Private Sub OffToolStripMenuItem25_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OffToolStripMenuItem25.Click
        CommandParser("pickup off")
    End Sub

    Private Sub OnToolStripMenuItem18_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OnToolStripMenuItem18.Click
        Dim newtitle As String = InputBox("Enter the new title for the Tibia Window.", "New Title")
        CommandParser("faketitle """ & newtitle & """")
    End Sub

    Private Sub OffToolStripMenuItem19_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OffToolStripMenuItem19.Click
        CommandParser("faketitle off")
    End Sub

    Private Sub OnToolStripMenuItem20_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OnToolStripMenuItem20.Click
        CommandParser("rainbow on")
    End Sub

    Private Sub FastToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FastToolStripMenuItem.Click
        CommandParser("rainbow fast")
    End Sub

    Private Sub SlowToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SlowToolStripMenuItem.Click
        CommandParser("rainbow slow")
    End Sub

    Private Sub OffToolStripMenuItem21_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OffToolStripMenuItem21.Click
        CommandParser("rainbow off")
    End Sub

    Private Sub OnToolStripMenuItem19_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OnToolStripMenuItem19.Click
        Dim outfit As String = InputBox("Enter the name of the creature or outfit. Example: Male Beggar.", "Outfit Name/Creature Name")
        Dim addons As String = InputBox("Enter the addons. Leave empty if not needed. 0 = None. 1 = First. 2 = Second. 3 = Full. Example: 3.", "Addons")
        CommandParser("chameleon """ & outfit & """ " & addons)
    End Sub

    Private Sub CopyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyToolStripMenuItem.Click
        Dim pname As String = InputBox("Enter the name of the creature in your screen to copy the outfit.", "Creature Name")
        CommandParser("chameleon copy """ & pname)
    End Sub

    Private Sub SpellsxmlToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SpellsxmlToolStripMenuItem.Click
        CommandParser("reload spells")
    End Sub

    Private Sub OutfitsxmlToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OutfitsxmlToolStripMenuItem.Click
        CommandParser("reload outfits")
    End Sub

    Private Sub ItemsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ItemsToolStripMenuItem.Click
        CommandParser("reload items")
    End Sub

    Private Sub ConstantsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConstantsToolStripMenuItem.Click
        CommandParser("reload constants")
    End Sub

    Private Sub TibiadatToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TibiadatToolStripMenuItem.Click
        CommandParser("reload dat")
    End Sub

    Private Sub AboutUsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutUsToolStripMenuItem.Click
        CommandParser("about")
    End Sub

    Private Sub VersionToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VersionToolStripMenuItem.Click
        CommandParser("version")
    End Sub

    Private Sub HideToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HideToolStripMenuItem.Click
        Me.NotifyIcon.Visible = True
        IsVisible = False
        Me.Hide()
    End Sub

    Private Sub OnToolStripMenuItem26_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OnToolStripMenuItem26.Click
        CommandParser("exp creatures on")
    End Sub

    Private Sub OffToolStripMenuItem20_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OffToolStripMenuItem20.Click
        CommandParser("exp creatures off")
    End Sub

    Private Sub OnToolStripMenuItem27_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OnToolStripMenuItem27.Click
        Dim leadername As String = InputBox("Enter the name of the leader. Example: Jokuperkele", "Name of the Leader")
        CommandParser("combobot """ & leadername)
    End Sub

    Private Sub OffToolStripMenuItem27_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OffToolStripMenuItem27.Click
        CommandParser("combobot off")
    End Sub
#End Region

    Private Sub LicenseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LicenseToolStripMenuItem.Click
        Try
            System.Diagnostics.Process.Start("COPYING.txt")
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub SpellCasterTrigger_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SpellCasterTrigger.CheckedChanged
        Static FirstTime As Boolean = True
        Try
            If FirstTime Then
                FirstTime = False
                Exit Sub
            End If
            If SpellCasterTrigger.Checked Then
                If String.IsNullOrEmpty(SpellCasterSpell.Text) Then
                    SpellCasterTrigger.Checked = False
                    MessageBox.Show("The spell must not be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
                If SpellCasterMinimumManaPoints.Value = 0 Then
                    SpellCasterTrigger.Checked = False
                    MessageBox.Show("The spell minimum mana points must not be zero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
                Core.SpellMsg = SpellCasterSpell.Text
                Core.SpellManaRequired = SpellCasterMinimumManaPoints.Value
                Core.SpellTimerObj.StartTimer()
            Else
                Core.SpellTimerObj.StopTimer()
                Core.SpellMsg = String.Empty
                Core.SpellManaRequired = 0
            End If
            RefreshSpellCasterControls()
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub RunemakerTrigger_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RunemakerTrigger.CheckedChanged
        Static FirstTime As Boolean = True
        Try
            If FirstTime Then
                FirstTime = False
                Exit Sub
            End If
            If RunemakerTrigger.Checked Then
                If String.IsNullOrEmpty(RunemakerSpell.Text) Then
                    RunemakerTrigger.Checked = False
                    MessageBox.Show("The spell must not be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
                If RunemakerMinimumManaPoints.Value = 0 Then
                    RunemakerTrigger.Checked = False
                    MessageBox.Show("The runemaker minimum mana points must not be zero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
                If RunemakerMinimumSoulPoints.Value = 0 Then
                    RunemakerTrigger.Checked = False
                    MessageBox.Show("The runemaker minimum soul points must not be zero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
                Dim Found As Boolean = False
                For Each Spell As SpellDefinition In CoreModule.Spells.SpellsList
                    If Spell.Name.Equals(RunemakerSpell.Text) Then
                        Core.RunemakerSpell = Spell
                        Found = True
                        Exit For
                    End If
                Next
                If Not Found Then
                    RunemakerTrigger.Checked = False
                    MessageBox.Show("The runemaker spell was not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
                Core.RunemakerManaPoints = RunemakerMinimumManaPoints.Value
                Core.RunemakerSoulPoints = RunemakerMinimumSoulPoints.Value
                Core.RunemakerTimerObj.StartTimer()
            Else
                Core.RunemakerTimerObj.StopTimer()
                Core.RunemakerManaPoints = 0
                Core.RunemakerSoulPoints = 0
                Core.RunemakerSpell = Nothing
            End If
            RefreshRunemakerControls()
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub AutoEaterSmart_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AutoEaterSmart.CheckedChanged
        Try
            AutoEaterMinimumHitPoints.Enabled = AutoEaterSmart.Checked
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub AutoEaterTrigger_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AutoEaterTrigger.CheckedChanged
        Static FirstTime As Boolean = True
        Try
            If FirstTime Then
                FirstTime = False
                Exit Sub
            End If
            If AutoEaterTrigger.Checked Then
                If AutoEaterSmart.Checked Then
                    If AutoEaterMinimumHitPoints.Value = 0 Then
                        AutoEaterTrigger.Checked = False
                        MessageBox.Show("The minimum hit points when the Auto Smart Eater feature is on must not be zero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End If
                    Core.AutoEaterSmart = AutoEaterMinimumHitPoints.Value
                Else
                    Core.AutoEaterSmart = 0
                End If
                If AutoEaterInterval.Value = 0 Then
                    AutoEaterTrigger.Checked = False
                    MessageBox.Show("The auto eater delay must not be zero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
                Core.EaterTimerObj.Interval = AutoEaterInterval.Value
                Core.EaterTimerObj.StartTimer()
            Else
                Core.AutoEaterSmart = 0
                Core.EaterTimerObj.StopTimer()
            End If
            RefreshAutoEaterControls()
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub AutoEaterEatFromFloor_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AutoEaterEatFromFloor.CheckedChanged
        AutoEaterEatFromFloorFirst.Enabled = AutoEaterEatFromFloor.Checked
        Consts.EatFromFloor = AutoEaterEatFromFloor.Checked
        'If Not AutoEaterEatFromFloorFirst.Enabled Then
        '    AutoEaterEatFromFloorFirst.Checked = False
        'End If
    End Sub

    Private Sub AutoEaterDelay_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AutoEaterInterval.ValueChanged
        If AutoEaterSmart.Checked Then
            Consts.AutoEaterSmartInterval = AutoEaterInterval.Value
        Else
            Consts.AutoEaterInterval = AutoEaterInterval.Value
        End If
    End Sub

    Private Sub AutoEaterEatFromFloorFirst_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AutoEaterEatFromFloorFirst.CheckedChanged
        Consts.EatFromFloorFirst = AutoEaterEatFromFloorFirst.Checked
    End Sub

    Private Sub ConfigLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConfigLoad.Click
        Try
            Core.ConsoleWrite("Please wait...")
            Dim Data As String = ""
            Dim Reader As IO.StreamReader
            Reader = IO.File.OpenText(Core.GetProfileDirectory() & "\config.txt")
            Data = Reader.ReadToEnd
            Dim MCollection As MatchCollection
            Dim GroupMatch As Match
            MCollection = [Regex].Matches(Data, "&([^\n;]+)[;]?")
            For Each GroupMatch In MCollection
                CommandParser(GroupMatch.Groups(1).Value)
            Next
            MsgBox("Done loading your configuration.", MsgBoxStyle.OkOnly, "Done!")
        Catch
            MsgBox("Unable to load your configuration.", MsgBoxStyle.Critical, "Error!")
        End Try
    End Sub

    Private Sub EditConfig_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditConfig.Click
        Try
            Dim ConfigWindow As New frmConfigEdit
            ConfigWindow.Show()
        Catch
        End Try
    End Sub

    Private Sub ClearConfig_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClearConfig.Click
        Try
            IO.File.Delete(Core.GetProfileDirectory() & "\config.txt")
        Catch
            MsgBox("Unable to clear your configuration.", MsgBoxStyle.Critical, "Error!")
        Finally
            MsgBox("Cleared.", MsgBoxStyle.OkOnly, "Done!")
        End Try
    End Sub

    Private Sub AutoLooterTrigger_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AutoLooterTrigger.CheckedChanged
        'Static FirstTime As Boolean = True
        Try
            'If FirstTime Then
            'FirstTime = False
            'Exit Sub
            'End If
            If AutoLooterTrigger.Checked Then
                If AutoLooterMinCap.Value < 0 Then
                    MessageBox.Show("Minimum Capacity must be above zero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
                Core.LooterMinimumCapacity = AutoLooterMinCap.Value
                Core.LooterTimerObj.StartTimer()
            Else
                Core.LooterTimerObj.StopTimer()
                Core.LooterMinimumCapacity = 0
            End If
            RefreshAutoLooterControls()
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub AutoLooterEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AutoLooterEdit.Click
        CoreModule.LootItems.ShowLootCategories()
    End Sub

    Private Sub frmMain_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        If Not Core.IRCClient Is Nothing Then
            If Not Core.IRCClient.DoMainLoopThread Is Nothing Then
                Core.IRCClient.DoMainLoopThread.Abort()
            End If
            If Core.IRCClient.IsConnected Then
                Core.IRCClient.Disconnect()
            End If
        End If
    End Sub
End Class
