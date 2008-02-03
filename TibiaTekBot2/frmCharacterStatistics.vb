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

Imports System.Math

Public Class frmCharacterStatistics
    Public FirstTime As Boolean = True
    Dim ETA As TimeSpan
    Dim Rate As Double = 0.0
    ' Gold
    Dim InitialGold As Integer = 0
    Dim ActualGold As Integer = 0
    ' Level
    Dim InitialLevelPercent As Integer = 0
    Dim ActualLevelPercent As Integer = 0
    ' Experience
    Dim InitialExperience As Integer = 0
    ' Magic Level
    Dim ActualMagicLevel As Integer = 0
    Dim ActualMagicLevelPercent As Integer = 0
    Dim InitialMagicLevelPercent As Integer = 0
    ' Fist
    Dim ActualFist As Integer = 0
    Dim InitialFistPercent As Integer = 0
    Dim ActualFistPercent As Integer = 0
    ' Club
    Dim ActualClub As Integer = 0
    Dim InitialClubPercent As Integer = 0
    Dim ActualClubPercent As Integer = 0
    ' Axe
    Dim ActualAxe As Integer = 0
    Dim InitialAxePercent As Integer = 0
    Dim ActualAxePercent As Integer = 0
    ' Sword
    Dim ActualSword As Integer = 0
    Dim InitialSwordPercent As Integer = 0
    Dim ActualSwordPercent As Integer = 0
    ' Distance
    Dim ActualDistance As Integer = 0
    Dim InitialDistancePercent As Integer = 0
    Dim ActualDistancePercent As Integer = 0
    ' Shielding
    Dim ActualShielding As Integer = 0
    Dim InitialShieldingPercent As Integer = 0
    Dim ActualShieldingPercent As Integer = 0
    ' Fishing
    Dim ActualFishing As Integer = 0
    Dim InitialFishingPercent As Integer = 0
    Dim ActualFishingPercent As Integer = 0
    ' Stats
    Dim HitPointsMax As Integer = 0
    Dim ManaPointsMax As Integer = 0
    Dim Capacity As Integer = 0

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
        Static TimeDiff As TimeSpan
        Try
            If FirstTime Then Exit Sub

            If Not Kernel.Client.IsConnected Then
                FirstTime = True
                Exit Sub
            End If

            TimeDiff = Now.Subtract(Kernel.CharacterStatisticsTime)
            If TimeDiff.TotalHours = 0 Then Exit Sub
            ElapsedTimeLabel.Text = TimeSpanToString(TimeDiff)

            ' Gold
            ActualGold = (New Container).GetItemCountByItemID(Kernel.Client.Objects.ID("Gold Coin"))
            ActualGoldLabel.Text = ActualGold
            RateGoldLabel.Text = Round((ActualGold - InitialGold) / TimeDiff.TotalHours, 2) & " gp/h"

            ' Level
            ActualLevelLabel.Text = Kernel.Level
            Kernel.Client.ReadMemory(Consts.ptrLevelPercent, ActualLevelPercent, 1)
            RemainingLevelLabel.Text = (100 - ActualLevelPercent) & "%"
            Rate = (ActualLevelPercent - InitialLevelPercent) / TimeDiff.TotalHours
            If Rate > 0 Then
                ETA = TimeSpan.FromHours((100 - ActualLevelPercent) / Rate)
                ETALevelLabel.Text = TimeSpanToString(ETA)
                RateLevelLabel.Text = Round(Rate, 2) & "%/h"
            Else
                If ETALevelLabel.Text <> Double.PositiveInfinity.ToString Then
                    ETALevelLabel.Text = Double.PositiveInfinity.ToString
                End If
                InitialLevelPercent = ActualLevelPercent
            End If

            ' Experience
            ActualExperienceLabel.Text = Kernel.Experience.ToString
            Dim RemainingExperience As Double = (50 / 3) * Pow(Kernel.Level + 1, 3) - (100 * Pow(Kernel.Level + 1, 2)) + (850 / 3) * (Kernel.Level + 1) - 200 - Kernel.Experience
            RemainingExperienceLabel.Text = CInt(RemainingExperience)
            Rate = (Kernel.Experience - InitialExperience) / TimeDiff.TotalHours
            If Rate > 0 Then
                RateExperienceLabel.Text = Round(Rate, 2) & " exp/h"
            Else
                InitialExperience = Kernel.Experience
            End If

            ' Magic Level
            Kernel.Client.ReadMemory(Consts.ptrMagicLevel, ActualMagicLevel, 1)
            ActualMagicLevelLabel.Text = ActualMagicLevel.ToString
            Kernel.Client.ReadMemory(Consts.ptrMagicLevelPercent, ActualMagicLevelPercent, 1)
            RemainingMagicLevelLabel.Text = (100 - ActualMagicLevelPercent) & "%"
            Rate = (ActualMagicLevelPercent - InitialMagicLevelPercent) / TimeDiff.TotalHours
            If Rate > 0 Then
                ETA = TimeSpan.FromHours((100 - ActualMagicLevelPercent) / Rate)
                ETAMagicLevelLabel.Text = TimeSpanToString(ETA)
                RateMagicLevelLabel.Text = Round(Rate, 2) & "%/h"
            Else
                If ETAMagicLevelLabel.Text <> Double.PositiveInfinity.ToString Then
                    ETAMagicLevelLabel.Text = Double.PositiveInfinity.ToString
                End If
                Kernel.Client.ReadMemory(Consts.ptrMagicLevelPercent, InitialMagicLevelPercent, 1)
            End If

            ' Fist
            Kernel.Client.ReadMemory(Consts.ptrSkillsBegin + (Scripting.ITibia.Skills.FistFighting * Consts.SkillsDist), ActualFist, 1)
            ActualFistLabel.Text = ActualFist.ToString
            Kernel.Client.ReadMemory(Consts.ptrSkillsPercentBegin + (Scripting.ITibia.Skills.FistFighting * Consts.SkillsDist), ActualFistPercent, 1)
            RemainingFistLabel.Text = (100 - ActualFistPercent) & "%"
            Rate = (ActualFistPercent - InitialFistPercent) / TimeDiff.TotalHours
            If Rate > 0 Then
                ETA = TimeSpan.FromHours((100 - ActualFistPercent) / Rate)
                ETAFistLabel.Text = TimeSpanToString(ETA)
                RateFistLabel.Text = Round(Rate, 2) & "%/h"
            Else
                If ETAFistLabel.Text <> Double.PositiveInfinity.ToString Then
                    ETAFistLabel.Text = Double.PositiveInfinity.ToString
                End If
                Kernel.Client.ReadMemory(Consts.ptrSkillsPercentBegin + (Scripting.ITibia.Skills.FistFighting * Consts.SkillsDist), InitialFistPercent, 1)
            End If

            ' Club
            Kernel.Client.ReadMemory(Consts.ptrSkillsBegin + (Scripting.ITibia.Skills.ClubFighting * Consts.SkillsDist), ActualClub, 1)
            ActualClubLabel.Text = ActualClub.ToString
            Kernel.Client.ReadMemory(Consts.ptrSkillsPercentBegin + (Scripting.ITibia.Skills.ClubFighting * Consts.SkillsDist), ActualClubPercent, 1)
            RemainingClubLabel.Text = (100 - ActualClubPercent) & "%"
            Rate = (ActualClubPercent - InitialClubPercent) / TimeDiff.TotalHours
            If Rate > 0 Then
                ETA = TimeSpan.FromHours((100 - ActualClubPercent) / Rate)
                ETAClubLabel.Text = TimeSpanToString(ETA)
                RateClubLabel.Text = Round(Rate, 2) & "%/h"
            Else
                If ETAClubLabel.Text <> Double.PositiveInfinity.ToString Then
                    ETAClubLabel.Text = Double.PositiveInfinity.ToString
                End If
                Kernel.Client.ReadMemory(Consts.ptrSkillsPercentBegin + (Scripting.ITibia.Skills.ClubFighting * Consts.SkillsDist), InitialClubPercent, 1)
            End If

            ' Axe
            Kernel.Client.ReadMemory(Consts.ptrSkillsBegin + (Scripting.ITibia.Skills.AxeFighting * Consts.SkillsDist), ActualAxe, 1)
            ActualAxeLabel.Text = ActualAxe.ToString
            Kernel.Client.ReadMemory(Consts.ptrSkillsPercentBegin + (Scripting.ITibia.Skills.AxeFighting * Consts.SkillsDist), ActualAxePercent, 1)
            RemainingAxeLabel.Text = (100 - ActualAxePercent) & "%"
            Rate = (ActualAxePercent - InitialAxePercent) / TimeDiff.TotalHours
            If Rate > 0 Then
                ETA = TimeSpan.FromHours((100 - ActualAxePercent) / Rate)
                ETAAxeLabel.Text = TimeSpanToString(ETA)
                RateAxeLabel.Text = Round(Rate, 2) & "%/h"
            Else
                If ETAAxeLabel.Text <> Double.PositiveInfinity.ToString Then
                    ETAAxeLabel.Text = Double.PositiveInfinity.ToString
                End If
                Kernel.Client.ReadMemory(Consts.ptrSkillsPercentBegin + (Scripting.ITibia.Skills.AxeFighting * Consts.SkillsDist), InitialAxePercent, 1)
            End If

            ' Sword
            Kernel.Client.ReadMemory(Consts.ptrSkillsBegin + (Scripting.ITibia.Skills.SwordFighting * Consts.SkillsDist), ActualSword, 1)
            ActualSwordLabel.Text = ActualSword.ToString
            Kernel.Client.ReadMemory(Consts.ptrSkillsPercentBegin + (Scripting.ITibia.Skills.SwordFighting * Consts.SkillsDist), ActualSwordPercent, 1)
            RemainingSwordLabel.Text = (100 - ActualSwordPercent) & "%"
            Rate = (ActualSwordPercent - InitialSwordPercent) / TimeDiff.TotalHours
            If Rate > 0 Then
                ETA = TimeSpan.FromHours((100 - ActualSwordPercent) / Rate)
                ETASwordLabel.Text = TimeSpanToString(ETA)
                RateSwordLabel.Text = Round(Rate, 2) & "%/h"
            Else
                If ETASwordLabel.Text <> Double.PositiveInfinity.ToString Then
                    ETASwordLabel.Text = Double.PositiveInfinity.ToString
                End If
                Kernel.Client.ReadMemory(Consts.ptrSkillsPercentBegin + (Scripting.ITibia.Skills.SwordFighting * Consts.SkillsDist), InitialSwordPercent, 1)
            End If

            ' Distance
            Kernel.Client.ReadMemory(Consts.ptrSkillsBegin + (Scripting.ITibia.Skills.DistanceFighting * Consts.SkillsDist), ActualDistance, 1)
            ActualDistanceLabel.Text = ActualDistance.ToString
            Kernel.Client.ReadMemory(Consts.ptrSkillsPercentBegin + (Scripting.ITibia.Skills.DistanceFighting * Consts.SkillsDist), ActualDistancePercent, 1)
            RemainingDistanceLabel.Text = (100 - ActualDistancePercent) & "%"
            Rate = (ActualDistancePercent - InitialDistancePercent) / TimeDiff.TotalHours
            If Rate > 0 Then
                ETA = TimeSpan.FromHours((100 - ActualDistancePercent) / Rate)
                ETADistanceLabel.Text = TimeSpanToString(ETA)
                RateDistanceLabel.Text = Round(Rate, 2) & "%/h"
            Else
                If ETADistanceLabel.Text <> Double.PositiveInfinity.ToString Then
                    ETADistanceLabel.Text = Double.PositiveInfinity.ToString
                End If
                Kernel.Client.ReadMemory(Consts.ptrSkillsPercentBegin + (Scripting.ITibia.Skills.DistanceFighting * Consts.SkillsDist), InitialDistancePercent, 1)
            End If

            ' Shielding
            Kernel.Client.ReadMemory(Consts.ptrSkillsBegin + (Scripting.ITibia.Skills.Shielding * Consts.SkillsDist), ActualShielding, 1)
            ActualShieldingLabel.Text = ActualShielding.ToString
            Kernel.Client.ReadMemory(Consts.ptrSkillsPercentBegin + (Scripting.ITibia.Skills.Shielding * Consts.SkillsDist), ActualShieldingPercent, 1)
            RemainingShieldingLabel.Text = (100 - ActualShieldingPercent) & "%"
            Rate = (ActualShieldingPercent - InitialShieldingPercent) / TimeDiff.TotalHours
            If Rate > 0 Then
                ETA = TimeSpan.FromHours((100 - ActualShieldingPercent) / Rate)
                ETAShieldingLabel.Text = TimeSpanToString(ETA)
                RateShieldingLabel.Text = Round(Rate, 2) & "%/h"
            Else
                If ETAShieldingLabel.Text <> Double.PositiveInfinity.ToString Then
                    ETAShieldingLabel.Text = Double.PositiveInfinity.ToString
                End If
                Kernel.Client.ReadMemory(Consts.ptrSkillsPercentBegin + (Scripting.ITibia.Skills.Shielding * Consts.SkillsDist), InitialShieldingPercent, 1)
            End If

            ' Fishing
            Kernel.Client.ReadMemory(Consts.ptrSkillsBegin + (Scripting.ITibia.Skills.Fishing * Consts.SkillsDist), ActualFishing, 1)
            ActualFishingLabel.Text = ActualFishing.ToString
            Kernel.Client.ReadMemory(Consts.ptrSkillsPercentBegin + (Scripting.ITibia.Skills.Fishing * Consts.SkillsDist), ActualFishingPercent, 1)
            RemainingFishingLabel.Text = (100 - ActualFishingPercent) & "%"
            Rate = (ActualFishingPercent - InitialFishingPercent) / TimeDiff.TotalHours
            If Rate > 0 Then
                ETA = TimeSpan.FromHours((100 - ActualFishingPercent) / Rate)
                ETAFishingLabel.Text = TimeSpanToString(ETA)
                RateFishingLabel.Text = Round(Rate, 2) & "%/h"
            Else
                If ETAFishingLabel.Text <> Double.PositiveInfinity.ToString Then
                    ETAFishingLabel.Text = Double.PositiveInfinity.ToString
                End If
                Kernel.Client.ReadMemory(Consts.ptrSkillsPercentBegin + (Scripting.ITibia.Skills.Fishing * Consts.SkillsDist), InitialFishingPercent, 1)
            End If

            ' Stats
            Kernel.Client.ReadMemory(Consts.ptrMaxHitPoints, HitPointsMax, 4)
            Kernel.Client.ReadMemory(Consts.ptrMaxManaPoints, ManaPointsMax, 4)
            Kernel.Client.ReadMemory(Consts.ptrCapacity, Capacity, 4)
            ActualHitPointsLabel.Text = Kernel.HitPoints & "/" & HitPointsMax
            ActualManaPointsLabel.Text = Kernel.ManaPoints & "/" & ManaPointsMax
            ActualSoulPointsLabel.Text = Kernel.SoulPoints
            ActualCapacityLabel.Text = Capacity
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
            Kernel.CharacterStatisticsTime = Now
            ' Gold
            InitialGold = (New Container).GetItemCountByItemID(Kernel.Client.Objects.ID("Gold Coin"))
            ' Level
            Kernel.Client.ReadMemory(Consts.ptrLevelPercent, InitialLevelPercent, 1)
            ' Experience
            InitialExperience = Kernel.Experience
            ' Magic Level
            Kernel.Client.ReadMemory(Consts.ptrMagicLevelPercent, InitialMagicLevelPercent, 1)
            ' Fist
            Kernel.Client.ReadMemory(Consts.ptrSkillsPercentBegin + (Scripting.ITibia.Skills.FistFighting * Consts.SkillsDist), InitialFistPercent, 1)
            ' Club
            Kernel.Client.ReadMemory(Consts.ptrSkillsPercentBegin + (Scripting.ITibia.Skills.ClubFighting * Consts.SkillsDist), InitialClubPercent, 1)
            ' Axe
            Kernel.Client.ReadMemory(Consts.ptrSkillsPercentBegin + (Scripting.ITibia.Skills.AxeFighting * Consts.SkillsDist), InitialAxePercent, 1)
            ' Sword
            Kernel.Client.ReadMemory(Consts.ptrSkillsPercentBegin + (Scripting.ITibia.Skills.SwordFighting * Consts.SkillsDist), InitialSwordPercent, 1)
            ' Distance
            Kernel.Client.ReadMemory(Consts.ptrSkillsPercentBegin + (Scripting.ITibia.Skills.DistanceFighting * Consts.SkillsDist), InitialDistancePercent, 1)
            ' Shielding
            Kernel.Client.ReadMemory(Consts.ptrSkillsPercentBegin + (Scripting.ITibia.Skills.Shielding * Consts.SkillsDist), InitialShieldingPercent, 1)
            ' Fishing
            Kernel.Client.ReadMemory(Consts.ptrSkillsPercentBegin + (Scripting.ITibia.Skills.Fishing * Consts.SkillsDist), InitialFishingPercent, 1)
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub
End Class