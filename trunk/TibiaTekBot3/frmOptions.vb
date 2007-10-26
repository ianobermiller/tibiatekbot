Public Class frmOptions

    Private Sub frmOptions_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Load Data to the Controls
        AutoPublishLoccb.Checked = Core.Consts.AutoPublishLocation
        AutoPublisherFreqtxtbox.Text = Core.Consts.AutoPublishLocationInterval
        ExpMultipertxtbox.Text = Core.Consts.CreatureExpMultiplier
        InvisCreaturescb.Checked = Core.Consts.ShowInvisibleCreatures
        LootWithCBcb.Checked = Core.Consts.LootWithCavebot
        CBLootCaptxtbox.Text = Core.Consts.CavebotLootMinCap
        CBAttackingRadiustxtbox.Text = Core.Consts.CavebotAttackerRadius
        LootDelaytxtbox.Text = Core.Consts.LootDelay
        LootInBagcb.Checked = Core.Consts.LootInBag
        LootInBagDelaytxtbox.Text = Core.Consts.LootInBagDelay
        LootEatFromCorpsecb.Checked = Core.Consts.LootEatFromCorpse
        LooterMaxDistancetxtbox.Text = Core.Consts.LootMaxDistance
        EatFromFloorcb.Checked = Core.Consts.EatFromFloor
        EatFirstFromFloorcb.Checked = Core.Consts.EatFromFloorFirst
        EatMaxDistancetxtbox.Text = Core.Consts.EatFromFloorMaxDistance
        AutoPickupDelaytxtbox.Text = Core.Consts.AutoPickUpDelay
        HealersCheckIntervaltextbox.Text = Core.Consts.HealersCheckInterval
        HealersAfterHealDelaytxtbox.Text = Core.Consts.HealersAfterHealDelay
        HotkeysEquipItemscb.Checked = Core.Consts.HotkeysCanEquipItems
        EquipItemOnUsecb.Checked = Core.Consts.EquipItemsOnUse
        StackerDelaytxtbox.Text = Core.Consts.StackerDelay
        FPSActivetxtbox.Text = Core.Consts.FPSWhenActive
        FPSHiddentxtbox.Text = Core.Consts.FPSWhenHidden
        FPSInactivetxtbox.Text = Core.Consts.FPSWhenInactive
        FPSMinmizedtxtbox.Text = Core.Consts.FPSWhenMinimized
        FlashMessagedcb.Checked = Core.Consts.FlashTaskbarWhenMessaged
        FlashAlarmcb.Checked = Core.Consts.FlashTaskbarWhenAlarmFires
        UploaderOnDiskcb.Checked = Core.Consts.StatsUploaderSaveOnDiskOnly
        UploaderUrltxbox.Text = Core.Consts.StatsUploaderUrl
        UploaderPathtxtbox.Text = Core.Consts.StatsUploaderPath
        UploaderFilenametxtbox.Text = Core.Consts.StatsUploaderFilename
        UploaderUserIdtxtbox.Text = Core.Consts.StatsUploaderUserID
        UploaderPasswordtxtbox.Text = Core.Consts.StatsUploaderPassword
        UploaderFreqtxtbox.Text = Core.Consts.StatsUploaderFrequency

    End Sub
End Class