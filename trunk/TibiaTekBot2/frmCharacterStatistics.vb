Public Class frmCharacterStatistics
    Public FirstTime As Boolean = True
    Dim GoldInitial As Integer = 0
    Dim GoldActualN As Integer = 0
    Dim LevelPercentInitial As Integer = 0
    Dim LevelPercentActualN As Integer = 0

    Private Sub frmCharacterStatistics_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            e.Cancel = True
            Me.Hide()
        Catch Ex As Exception
            MessageBox.Show("Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source, Ex.TargetSite.Name, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub frmCharacterStatistics_Activated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        Try
            If FirstTime Then
                Reset()
                FirstTime = False
            End If
        Catch Ex As Exception
            MessageBox.Show("Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source, Ex.TargetSite.Name, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Try
            If Not Core.InGame Then
                'set everything to zero
                Exit Sub
            End If
            Dim TimeDiff As TimeSpan = Now.Subtract(Core.CharacterStatisticsTime)

            GoldActualN = ContainerModule.Container.GetItemCountByItemID(Definitions.GetItemID("Gold Coin"))
            Core.ReadMemory(Consts.ptrLevelPercent, LevelPercentActualN, 1)

            GoldActual.Text = GoldActualN
            GoldRate.Text = System.Math.Round((GoldActualN - GoldInitial) / TimeDiff.TotalMinutes, 2, MidpointRounding.AwayFromZero) & " gp/min"
            LevelActual.Text = Core.Level
            LevelCompleted.Text = LevelPercentActualN & "%"
            LevelRemaining.Text = ((100 - LevelPercentActualN) * TimeDiff.TotalHours / (LevelPercentActualN - LevelPercentInitial)) & "%/h"

            ExperienceActual.Text = Core.Experience
        Catch Ex As Exception
            MessageBox.Show("Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source, Ex.TargetSite.Name, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Reset()
    End Sub

    Public Sub Reset()
        Try
            Core.CharacterStatisticsTime = Now
            Core.ReadMemory(Consts.ptrLevelPercent, LevelPercentInitial, 1)
            GoldInitial = ContainerModule.Container.GetItemCountByItemID(Definitions.GetItemID("Gold Coin"))
        Catch Ex As Exception
            MessageBox.Show("Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source, Ex.TargetSite.Name, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub
End Class