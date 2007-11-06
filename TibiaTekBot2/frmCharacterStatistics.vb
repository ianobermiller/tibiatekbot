Public Class frmCharacterStatistics
    Public FirstTime As Boolean = True
    Dim InitialGold As Integer = 0
    Dim GoldActualN As Integer = 0
    Dim InitialLevelPercent As Integer = 0
    Dim ActualLevelPercent As Integer = 0

    Private Sub frmCharacterStatistics_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            e.Cancel = True
            Me.Hide()
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub frmCharacterStatistics_Activated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        Try
            If FirstTime Then
                FirstTime = False
                Reset()
            End If
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Try
            If Not Core.InGame Then Exit Sub
            Dim TimeDiff As TimeSpan = Now.Subtract(Core.CharacterStatisticsTime)

            GoldActualN = ContainerModule.Container.GetItemCountByItemID(Definitions.GetItemID("Gold Coin"))
            Core.ReadMemory(Consts.ptrLevelPercent, ActualLevelPercent, 1)

            ActualGoldLabel.Text = GoldActualN
            RateGold.Text = System.Math.Round((GoldActualN - InitialGold) / TimeDiff.TotalMinutes, 2, MidpointRounding.AwayFromZero) & " gp/min"
            ActualLevelLabel.Text = Core.Level
            TotalLevelLabel.Text = ActualLevelPercent & "%"
            RemainingLevelLabel.Text = ((100 - ActualLevelPercent) * TimeDiff.TotalHours / (ActualLevelPercent - InitialLevelPercent)) & "%/h"


            ActualExperienceLabel.Text = Core.Experience.ToString
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Reset()
    End Sub

    Public Sub Reset()
        Try
            Core.CharacterStatisticsTime = Now
            Core.ReadMemory(Consts.ptrLevelPercent, InitialLevelPercent, 1)
            InitialGold = ContainerModule.Container.GetItemCountByItemID(Definitions.GetItemID("Gold Coin"))
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub
End Class