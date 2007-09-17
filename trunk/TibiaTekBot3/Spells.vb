Imports System.xml

Public Module SpellsModule

    Public Structure SpellDefinition
        Dim Name As String
        Dim Words As String
        Dim ManaPoints As Integer
    End Structure

    Public Structure ConjureDefinition
        Dim Name As String
        Dim Words As String
        Dim ManaPoints As Integer
        Dim SoulPoints As Integer
    End Structure

    Public Class SpellsClass

        Public Conjures As New List(Of ConjureDefinition)
        Public SupportiveSpells As New List(Of SpellDefinition)

        Public Sub New()
            LoadSpells()
        End Sub

        Public Sub LoadSpells()
            Try
                Dim Document As New XmlDocument
                Dim Value As String
                Document.Load(GetConfigurationDirectory() + "\Spells.xml")
                SupportiveSpells.Clear()
                Conjures.Clear()
                Dim XMLConjures As XmlElement = Document.Item("Spells").Item("Conjures")
                For Each XMLConjure As XmlElement In XMLConjures
                    Dim Conjure As ConjureDefinition
                    Conjure.Name = XMLConjure.GetAttribute("Name")
                    Conjure.Words = XMLConjure.GetAttribute("Words")
                    Value = XMLConjure.GetAttribute("Mana")
                    If Value.Length > 0 AndAlso Value.Chars(0) = "H" Then Value = "&" + Value
                    Conjure.ManaPoints = CInt(Value)
                    Value = XMLConjure.GetAttribute("Soul")
                    If Value.Length > 0 AndAlso Value.Chars(0) = "H" Then Value = "&" + Value
                    Conjure.SoulPoints = CInt(Value)
                    Conjures.Add(Conjure)
                Next
                Dim XMLSupportives As XmlElement = Document.Item("Spells").Item("Supportives")
                For Each XMLSupportive As XmlElement In XMLSupportives
                    Dim Spell As SpellDefinition
                    Spell.Name = XMLSupportive.GetAttribute("Name")
                    Spell.Words = XMLSupportive.GetAttribute("Words")
                    Value = XMLSupportive.GetAttribute("Mana")
                    If Value.Length > 0 AndAlso Value.Chars(0) = "H" Then Value = "&" + Value
                    Spell.ManaPoints = CInt(Value)
                    SupportiveSpells.Add(Spell)
                Next
            Catch Ex As Exception
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Sub

        Public Function GetSpellWords(ByVal Name As String) As String
            For Each Spell As SpellDefinition In SupportiveSpells
                If String.Compare(Name, Spell.Name, True) = 0 Then
                    Return Spell.Words
                End If
            Next
            Return ""
        End Function

        Public Function GetSpellMana(ByVal Name As String) As UShort
            For Each Spell As SpellDefinition In SupportiveSpells
                If String.Compare(Name, Spell.Name, True) = 0 Then
                    Return Spell.ManaPoints
                End If
            Next
            Return 0
        End Function

    End Class

End Module
