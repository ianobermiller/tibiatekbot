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

Imports System.IO, System.xml

Public Module HotkeySettingsModule

    Public Structure HotkeyDefinition
        Dim ItemID As UShort
        Dim ItemData As Integer
        Dim UseMode As HotkeyUseMode
        Dim Text As String
        Dim AutoSendText As Boolean
        Dim Type As HotkeyType
    End Structure

    Public Class HotkeySettings
        Private Hotkeys(36) As HotkeyDefinition

        Public Function GetHotkey(ByVal Key As HotkeyCombination) As HotkeyDefinition
            Return Hotkeys(Key)
        End Function

        Public Sub New()
        End Sub

        Public Shadows Sub Finalize()
        End Sub

#Region " Saving/Writing "

        Public Function Save(Optional ByVal Filename As String = "") As Boolean
            Try
                If String.IsNullOrEmpty(Filename) Then Filename = Core.GetProfileDirectory & "\" & "HotkeySettings.xml"
                Dim Document As New XmlDocument
                Dim xmlHotkeys As XmlElement = Document.CreateElement("Hotkeys")
                Dim Combinations() As String = System.Enum.GetNames(GetType(HotkeyCombination))
                For I As Integer = 0 To Consts.HotkeyMax - 1
                    Dim xmlHotkey As XmlElement = Document.CreateElement("Hotkey")
                    Dim xmlCombination As XmlAttribute = Document.CreateAttribute("Combination")
                    Dim xmlType As XmlAttribute = Document.CreateAttribute("Type")
                    Dim xmlItemID As XmlAttribute = Document.CreateAttribute("ItemID")
                    Dim xmlItemData As XmlAttribute = Document.CreateAttribute("ItemData")
                    Dim xmlUseMode As XmlAttribute = Document.CreateAttribute("UseMode")
                    Dim xmlAutoSendText As XmlAttribute = Document.CreateAttribute("AutoSendText")
                    xmlCombination.InnerText = Combinations(I)
                    xmlType.InnerText = Hotkeys(I).Type.ToString
                    xmlItemID.InnerText = Hotkeys(I).ItemID
                    xmlItemData.InnerText = Hotkeys(I).ItemData
                    xmlUseMode.InnerText = Hotkeys(I).UseMode.ToString
                    xmlAutoSendText.InnerText = Hotkeys(I).AutoSendText
                    If Not String.IsNullOrEmpty(Hotkeys(I).Text) Then
                        xmlHotkey.InnerText = Hotkeys(I).Text
                    End If
                    xmlHotkey.Attributes.Append(xmlCombination)
                    xmlHotkey.Attributes.Append(xmlType)
                    xmlHotkey.Attributes.Append(xmlItemID)
                    xmlHotkey.Attributes.Append(xmlItemData)
                    xmlHotkey.Attributes.Append(xmlUseMode)
                    xmlHotkey.Attributes.Append(xmlAutoSendText)
                    xmlHotkeys.AppendChild(xmlHotkey)
                Next
                Dim Declaration As XmlDeclaration = Document.CreateXmlDeclaration("1.0", "", "")
                Document.AppendChild(Declaration)
                Document.AppendChild(xmlHotkeys)
                Document.Save(Filename)
                Return True
            Catch
                Return False
            End Try
        End Function

        Private Sub WriteToMemory()
            Try
                For I As Integer = 0 To Consts.HotkeyMax - 1
                    Core.WriteMemory(Consts.ptrHotkeyBegin + (Consts.HotkeyItemDist * I), Hotkeys(I).UseMode, 1)
                    Select Case Hotkeys(I).Type
                        Case HotkeyType.Item
                            Core.WriteMemory(Consts.ptrHotkeyBegin + (Consts.HotkeyItemDist * I) + Consts.HotkeyItemOffset, Hotkeys(I).ItemID, 2)
                            Core.WriteMemory(Consts.ptrHotkeyBegin + (Consts.HotkeyItemDist * I) + Consts.HotkeyItemDataOffset, Hotkeys(I).ItemData, 1)
                        Case HotkeyType.Text
                            Core.WriteMemory(Consts.ptrHotkeyBegin + (Consts.HotkeyTextAutoSendDist * I) + Consts.HotkeyTextAutoSendOffset, Hotkeys(I).AutoSendText, 1)
                            Core.WriteMemory(Consts.ptrHotkeyBegin + (Consts.HotkeyTextDist * I) + Consts.HotkeyTextOffset, Hotkeys(I).Text)
                        Case HotkeyType.None
                            Core.WriteMemory(Consts.ptrHotkeyBegin + (Consts.HotkeyItemDist * I) + Consts.HotkeyItemOffset, 0, 2)
                            Core.WriteMemory(Consts.ptrHotkeyBegin + (Consts.HotkeyItemDist * I) + Consts.HotkeyItemDataOffset, 0, 1)
                            Core.WriteMemory(Consts.ptrHotkeyBegin + (Consts.HotkeyTextAutoSendDist * I) + Consts.HotkeyTextAutoSendOffset, 0, 1)
                            Core.WriteMemory(Consts.ptrHotkeyBegin + (Consts.HotkeyTextDist * I) + Consts.HotkeyTextOffset, "")
                    End Select
                Next
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Sub

#End Region

#Region " Loading/Reading "

        Public Function Load(Optional ByVal Filename As String = "") As Boolean
            Try
                If String.IsNullOrEmpty(Filename) Then Filename = Core.GetProfileDirectory & "\" & "HotkeySettings.xml"
                If Not ReadFromFile(Filename) Then Return False
                WriteToMemory()
                Return True
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Function

        Private Function ReadFromFile(ByVal Filename As String) As Boolean
            Try
                Dim Document As New XmlDocument
                If Not IO.File.Exists(Filename) Then Return False
                Document.Load(Filename)
                Dim I As Integer = 0
                Dim xmlHotkeys As XmlElement = Document.Item("Hotkeys")
                For Each xmlHotkey As XmlElement In xmlHotkeys
                    I = CInt(System.Enum.Parse(GetType(HotkeyCombination), xmlHotkey.GetAttribute("Combination")))
                    Hotkeys(I).Type = System.Enum.Parse(GetType(HotkeyType), xmlHotkey.GetAttribute("Type"))
                    Hotkeys(I).ItemID = xmlHotkey.GetAttribute("ItemID")
                    Hotkeys(I).ItemData = xmlHotkey.GetAttribute("ItemData")
                    Hotkeys(I).UseMode = System.Enum.Parse(GetType(HotkeyUseMode), xmlHotkey.GetAttribute("UseMode"))
                    Hotkeys(I).AutoSendText = System.Boolean.Parse(xmlHotkey.GetAttribute("AutoSendText"))
                    Hotkeys(I).Text = xmlHotkey.InnerText
                Next
                Return True
            Catch
                Return False
            End Try
        End Function

        Public Sub LoadFromMemory()
            Try
                Dim UseMode As Integer = 0
                Dim ItemID As Integer = 0
                Dim AutoSend As Integer = 0
                Dim ItemData As Integer = 0
                Dim Text As String = ""
                For I As Integer = 0 To Consts.HotkeyMax - 1
                    Core.ReadMemory(Consts.ptrHotkeyBegin + (Consts.HotkeyItemDist * I), UseMode, 1)
                    Hotkeys(I).UseMode = CType(UseMode, HotkeyUseMode)
                    Core.ReadMemory(Consts.ptrHotkeyBegin + (Consts.HotkeyTextDist * I) + Consts.HotkeyTextOffset, Text)
                    Core.ReadMemory(Consts.ptrHotkeyBegin + (Consts.HotkeyItemDist * I) + Consts.HotkeyItemOffset, ItemID, 2)
                    If ItemID > 0 Then 'item
                        Hotkeys(I).Type = HotkeyType.Item
                        Hotkeys(I).ItemID = CUShort(ItemID)
                        Core.ReadMemory(Consts.ptrHotkeyBegin + (Consts.HotkeyItemDist * I) + Consts.HotkeyItemDataOffset, ItemData, 4)
                        Hotkeys(I).ItemData = ItemData
                    ElseIf Text.Length > 0 Then 'text
                        Hotkeys(I).Type = HotkeyType.Text
                        Hotkeys(I).Text = Text
                        Core.ReadMemory(Consts.ptrHotkeyBegin + (Consts.HotkeyTextAutoSendDist * I) + Consts.HotkeyTextAutoSendOffset, AutoSend, 1)
                        Hotkeys(I).AutoSendText = CBool(AutoSend)
                    Else 'none
                        Hotkeys(I).Type = HotkeyType.None
                    End If
                Next
            Catch Ex As Exception
                MessageBox.Show("Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace, Ex.TargetSite.Name, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Sub

#End Region

    End Class

End Module
