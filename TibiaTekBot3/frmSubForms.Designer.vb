<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSubForms
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Spellcaster = New System.Windows.Forms.Panel
        Me.SpellStop = New System.Windows.Forms.Button
        Me.SpellOnOff = New System.Windows.Forms.Button
        Me.SpellManatxtbox = New System.Windows.Forms.TextBox
        Me.SpellMana = New System.Windows.Forms.Label
        Me.SpellNametxtbox = New System.Windows.Forms.TextBox
        Me.Spellnamelba = New System.Windows.Forms.Label
        Me.AutoEater = New System.Windows.Forms.Panel
        Me.EaterSmarttxtbox = New System.Windows.Forms.TextBox
        Me.EaterSmartlbl = New System.Windows.Forms.Label
        Me.EaterSmartchk = New System.Windows.Forms.CheckBox
        Me.EaterStop = New System.Windows.Forms.Button
        Me.EaterOnOff = New System.Windows.Forms.Button
        Me.EaterDelaytxtbox = New System.Windows.Forms.TextBox
        Me.Eaterdelaylbl = New System.Windows.Forms.Label
        Me.Runemaker = New System.Windows.Forms.Panel
        Me.MakerNamelbl = New System.Windows.Forms.Label
        Me.MakerStop = New System.Windows.Forms.Button
        Me.MakerOnOff = New System.Windows.Forms.Button
        Me.MakerSoultxtbox = New System.Windows.Forms.TextBox
        Me.MakerSoullbl = New System.Windows.Forms.Label
        Me.MakerManatxtbox = New System.Windows.Forms.TextBox
        Me.MakerManalbl = New System.Windows.Forms.Label
        Me.MakerNametxtbox = New System.Windows.Forms.TextBox
        Me.AutoFisher = New System.Windows.Forms.Panel
        Me.FisherCaptxtbox = New System.Windows.Forms.TextBox
        Me.FisherCaplbl = New System.Windows.Forms.Label
        Me.FisherStop = New System.Windows.Forms.Button
        Me.FisherOnOff = New System.Windows.Forms.Button
        Me.FisherModecb = New System.Windows.Forms.ComboBox
        Me.FisherModelbl = New System.Windows.Forms.Label
        Me.Namespy = New System.Windows.Forms.Panel
        Me.NamespyOnOffchk = New System.Windows.Forms.CheckBox
        Me.NamespyStop = New System.Windows.Forms.Button
        Me.Autohealer = New System.Windows.Forms.Panel
        Me.HealerStop = New System.Windows.Forms.Button
        Me.HealerHptxtbox = New System.Windows.Forms.TextBox
        Me.HealerOnOff = New System.Windows.Forms.Button
        Me.HealerHplbl = New System.Windows.Forms.Label
        Me.HealerNametxtbox = New System.Windows.Forms.TextBox
        Me.HealerNamelbl = New System.Windows.Forms.Label
        Me.WASD = New System.Windows.Forms.Panel
        Me.WASDStop = New System.Windows.Forms.Button
        Me.WASDOnOffchk = New System.Windows.Forms.CheckBox
        Me.ExpChecker = New System.Windows.Forms.Panel
        Me.ExpStop = New System.Windows.Forms.Button
        Me.ExpCreaturesOnOffchk = New System.Windows.Forms.CheckBox
        Me.ExpOnOffchk = New System.Windows.Forms.CheckBox
        Me.Spellcaster.SuspendLayout()
        Me.AutoEater.SuspendLayout()
        Me.Runemaker.SuspendLayout()
        Me.AutoFisher.SuspendLayout()
        Me.Namespy.SuspendLayout()
        Me.Autohealer.SuspendLayout()
        Me.WASD.SuspendLayout()
        Me.ExpChecker.SuspendLayout()
        Me.SuspendLayout()
        '
        'Spellcaster
        '
        Me.Spellcaster.Controls.Add(Me.SpellStop)
        Me.Spellcaster.Controls.Add(Me.SpellOnOff)
        Me.Spellcaster.Controls.Add(Me.SpellManatxtbox)
        Me.Spellcaster.Controls.Add(Me.SpellMana)
        Me.Spellcaster.Controls.Add(Me.SpellNametxtbox)
        Me.Spellcaster.Controls.Add(Me.Spellnamelba)
        Me.Spellcaster.Location = New System.Drawing.Point(12, 12)
        Me.Spellcaster.Name = "Spellcaster"
        Me.Spellcaster.Size = New System.Drawing.Size(102, 146)
        Me.Spellcaster.TabIndex = 0
        '
        'SpellStop
        '
        Me.SpellStop.Location = New System.Drawing.Point(6, 119)
        Me.SpellStop.Name = "SpellStop"
        Me.SpellStop.Size = New System.Drawing.Size(84, 20)
        Me.SpellStop.TabIndex = 5
        Me.SpellStop.Text = "Stop"
        Me.SpellStop.UseVisualStyleBackColor = True
        '
        'SpellOnOff
        '
        Me.SpellOnOff.Location = New System.Drawing.Point(6, 93)
        Me.SpellOnOff.Name = "SpellOnOff"
        Me.SpellOnOff.Size = New System.Drawing.Size(84, 20)
        Me.SpellOnOff.TabIndex = 4
        Me.SpellOnOff.Text = "Activate"
        Me.SpellOnOff.UseVisualStyleBackColor = True
        '
        'SpellManatxtbox
        '
        Me.SpellManatxtbox.Location = New System.Drawing.Point(6, 67)
        Me.SpellManatxtbox.Name = "SpellManatxtbox"
        Me.SpellManatxtbox.Size = New System.Drawing.Size(84, 20)
        Me.SpellManatxtbox.TabIndex = 3
        '
        'SpellMana
        '
        Me.SpellMana.AutoSize = True
        Me.SpellMana.Location = New System.Drawing.Point(3, 51)
        Me.SpellMana.Name = "SpellMana"
        Me.SpellMana.Size = New System.Drawing.Size(80, 13)
        Me.SpellMana.TabIndex = 2
        Me.SpellMana.Text = "Mana Required"
        '
        'SpellNametxtbox
        '
        Me.SpellNametxtbox.Location = New System.Drawing.Point(6, 16)
        Me.SpellNametxtbox.Name = "SpellNametxtbox"
        Me.SpellNametxtbox.Size = New System.Drawing.Size(84, 20)
        Me.SpellNametxtbox.TabIndex = 1
        '
        'Spellnamelba
        '
        Me.Spellnamelba.AutoSize = True
        Me.Spellnamelba.Location = New System.Drawing.Point(3, 0)
        Me.Spellnamelba.Name = "Spellnamelba"
        Me.Spellnamelba.Size = New System.Drawing.Size(64, 13)
        Me.Spellnamelba.TabIndex = 0
        Me.Spellnamelba.Text = "Spell Words"
        '
        'AutoEater
        '
        Me.AutoEater.Controls.Add(Me.EaterSmarttxtbox)
        Me.AutoEater.Controls.Add(Me.EaterSmartlbl)
        Me.AutoEater.Controls.Add(Me.EaterSmartchk)
        Me.AutoEater.Controls.Add(Me.EaterStop)
        Me.AutoEater.Controls.Add(Me.EaterOnOff)
        Me.AutoEater.Controls.Add(Me.EaterDelaytxtbox)
        Me.AutoEater.Controls.Add(Me.Eaterdelaylbl)
        Me.AutoEater.Location = New System.Drawing.Point(120, 12)
        Me.AutoEater.Name = "AutoEater"
        Me.AutoEater.Size = New System.Drawing.Size(233, 113)
        Me.AutoEater.TabIndex = 1
        '
        'EaterSmarttxtbox
        '
        Me.EaterSmarttxtbox.Enabled = False
        Me.EaterSmarttxtbox.Location = New System.Drawing.Point(112, 67)
        Me.EaterSmarttxtbox.Name = "EaterSmarttxtbox"
        Me.EaterSmarttxtbox.Size = New System.Drawing.Size(108, 20)
        Me.EaterSmarttxtbox.TabIndex = 6
        '
        'EaterSmartlbl
        '
        Me.EaterSmartlbl.AutoSize = True
        Me.EaterSmartlbl.Enabled = False
        Me.EaterSmartlbl.Location = New System.Drawing.Point(109, 51)
        Me.EaterSmartlbl.Name = "EaterSmartlbl"
        Me.EaterSmartlbl.Size = New System.Drawing.Size(115, 13)
        Me.EaterSmartlbl.TabIndex = 5
        Me.EaterSmartlbl.Text = "Eat When Hp Is Below"
        '
        'EaterSmartchk
        '
        Me.EaterSmartchk.AutoSize = True
        Me.EaterSmartchk.Location = New System.Drawing.Point(112, 16)
        Me.EaterSmartchk.Name = "EaterSmartchk"
        Me.EaterSmartchk.Size = New System.Drawing.Size(108, 17)
        Me.EaterSmartchk.TabIndex = 4
        Me.EaterSmartchk.Text = "Use Smart Eating"
        Me.EaterSmartchk.UseVisualStyleBackColor = True
        '
        'EaterStop
        '
        Me.EaterStop.Location = New System.Drawing.Point(6, 77)
        Me.EaterStop.Name = "EaterStop"
        Me.EaterStop.Size = New System.Drawing.Size(84, 20)
        Me.EaterStop.TabIndex = 3
        Me.EaterStop.Text = "Stop"
        Me.EaterStop.UseVisualStyleBackColor = True
        '
        'EaterOnOff
        '
        Me.EaterOnOff.Location = New System.Drawing.Point(6, 51)
        Me.EaterOnOff.Name = "EaterOnOff"
        Me.EaterOnOff.Size = New System.Drawing.Size(84, 20)
        Me.EaterOnOff.TabIndex = 2
        Me.EaterOnOff.Text = "Activate"
        Me.EaterOnOff.UseVisualStyleBackColor = True
        '
        'EaterDelaytxtbox
        '
        Me.EaterDelaytxtbox.Location = New System.Drawing.Point(6, 16)
        Me.EaterDelaytxtbox.Name = "EaterDelaytxtbox"
        Me.EaterDelaytxtbox.Size = New System.Drawing.Size(82, 20)
        Me.EaterDelaytxtbox.TabIndex = 1
        '
        'Eaterdelaylbl
        '
        Me.Eaterdelaylbl.AutoSize = True
        Me.Eaterdelaylbl.Location = New System.Drawing.Point(3, 0)
        Me.Eaterdelaylbl.Name = "Eaterdelaylbl"
        Me.Eaterdelaylbl.Size = New System.Drawing.Size(85, 13)
        Me.Eaterdelaylbl.TabIndex = 0
        Me.Eaterdelaylbl.Text = "Delay For Eating"
        '
        'Runemaker
        '
        Me.Runemaker.Controls.Add(Me.MakerNamelbl)
        Me.Runemaker.Controls.Add(Me.MakerStop)
        Me.Runemaker.Controls.Add(Me.MakerOnOff)
        Me.Runemaker.Controls.Add(Me.MakerSoultxtbox)
        Me.Runemaker.Controls.Add(Me.MakerSoullbl)
        Me.Runemaker.Controls.Add(Me.MakerManatxtbox)
        Me.Runemaker.Controls.Add(Me.MakerManalbl)
        Me.Runemaker.Controls.Add(Me.MakerNametxtbox)
        Me.Runemaker.Location = New System.Drawing.Point(359, 12)
        Me.Runemaker.Name = "Runemaker"
        Me.Runemaker.Size = New System.Drawing.Size(217, 139)
        Me.Runemaker.TabIndex = 2
        '
        'MakerNamelbl
        '
        Me.MakerNamelbl.AutoSize = True
        Me.MakerNamelbl.Location = New System.Drawing.Point(3, 0)
        Me.MakerNamelbl.Name = "MakerNamelbl"
        Me.MakerNamelbl.Size = New System.Drawing.Size(64, 13)
        Me.MakerNamelbl.TabIndex = 0
        Me.MakerNamelbl.Text = "Spell Words"
        '
        'MakerStop
        '
        Me.MakerStop.Location = New System.Drawing.Point(119, 100)
        Me.MakerStop.Name = "MakerStop"
        Me.MakerStop.Size = New System.Drawing.Size(82, 20)
        Me.MakerStop.TabIndex = 7
        Me.MakerStop.Text = "Stop"
        Me.MakerStop.UseVisualStyleBackColor = True
        '
        'MakerOnOff
        '
        Me.MakerOnOff.Location = New System.Drawing.Point(119, 74)
        Me.MakerOnOff.Name = "MakerOnOff"
        Me.MakerOnOff.Size = New System.Drawing.Size(82, 20)
        Me.MakerOnOff.TabIndex = 6
        Me.MakerOnOff.Text = "Activate"
        Me.MakerOnOff.UseVisualStyleBackColor = True
        '
        'MakerSoultxtbox
        '
        Me.MakerSoultxtbox.Location = New System.Drawing.Point(6, 101)
        Me.MakerSoultxtbox.Name = "MakerSoultxtbox"
        Me.MakerSoultxtbox.Size = New System.Drawing.Size(85, 20)
        Me.MakerSoultxtbox.TabIndex = 5
        '
        'MakerSoullbl
        '
        Me.MakerSoullbl.AutoSize = True
        Me.MakerSoullbl.Location = New System.Drawing.Point(3, 84)
        Me.MakerSoullbl.Name = "MakerSoullbl"
        Me.MakerSoullbl.Size = New System.Drawing.Size(102, 13)
        Me.MakerSoullbl.TabIndex = 4
        Me.MakerSoullbl.Text = "Soulpoints Required"
        '
        'MakerManatxtbox
        '
        Me.MakerManatxtbox.Location = New System.Drawing.Point(4, 57)
        Me.MakerManatxtbox.Name = "MakerManatxtbox"
        Me.MakerManatxtbox.Size = New System.Drawing.Size(87, 20)
        Me.MakerManatxtbox.TabIndex = 3
        '
        'MakerManalbl
        '
        Me.MakerManalbl.AutoSize = True
        Me.MakerManalbl.Location = New System.Drawing.Point(3, 41)
        Me.MakerManalbl.Name = "MakerManalbl"
        Me.MakerManalbl.Size = New System.Drawing.Size(80, 13)
        Me.MakerManalbl.TabIndex = 2
        Me.MakerManalbl.Text = "Mana Required"
        '
        'MakerNametxtbox
        '
        Me.MakerNametxtbox.Location = New System.Drawing.Point(6, 16)
        Me.MakerNametxtbox.Name = "MakerNametxtbox"
        Me.MakerNametxtbox.Size = New System.Drawing.Size(85, 20)
        Me.MakerNametxtbox.TabIndex = 1
        '
        'AutoFisher
        '
        Me.AutoFisher.Controls.Add(Me.FisherCaptxtbox)
        Me.AutoFisher.Controls.Add(Me.FisherCaplbl)
        Me.AutoFisher.Controls.Add(Me.FisherStop)
        Me.AutoFisher.Controls.Add(Me.FisherOnOff)
        Me.AutoFisher.Controls.Add(Me.FisherModecb)
        Me.AutoFisher.Controls.Add(Me.FisherModelbl)
        Me.AutoFisher.Location = New System.Drawing.Point(582, 12)
        Me.AutoFisher.Name = "AutoFisher"
        Me.AutoFisher.Size = New System.Drawing.Size(135, 156)
        Me.AutoFisher.TabIndex = 3
        '
        'FisherCaptxtbox
        '
        Me.FisherCaptxtbox.Location = New System.Drawing.Point(0, 65)
        Me.FisherCaptxtbox.Name = "FisherCaptxtbox"
        Me.FisherCaptxtbox.Size = New System.Drawing.Size(93, 20)
        Me.FisherCaptxtbox.TabIndex = 5
        '
        'FisherCaplbl
        '
        Me.FisherCaplbl.AutoSize = True
        Me.FisherCaplbl.Location = New System.Drawing.Point(0, 49)
        Me.FisherCaplbl.Name = "FisherCaplbl"
        Me.FisherCaplbl.Size = New System.Drawing.Size(126, 13)
        Me.FisherCaplbl.TabIndex = 4
        Me.FisherCaplbl.Text = "Minimum Capacity to Fish"
        '
        'FisherStop
        '
        Me.FisherStop.Location = New System.Drawing.Point(0, 126)
        Me.FisherStop.Name = "FisherStop"
        Me.FisherStop.Size = New System.Drawing.Size(84, 20)
        Me.FisherStop.TabIndex = 3
        Me.FisherStop.Text = "Stop"
        Me.FisherStop.UseVisualStyleBackColor = True
        '
        'FisherOnOff
        '
        Me.FisherOnOff.Location = New System.Drawing.Point(-1, 100)
        Me.FisherOnOff.Name = "FisherOnOff"
        Me.FisherOnOff.Size = New System.Drawing.Size(84, 20)
        Me.FisherOnOff.TabIndex = 2
        Me.FisherOnOff.Text = "Activate"
        Me.FisherOnOff.UseVisualStyleBackColor = True
        '
        'FisherModecb
        '
        Me.FisherModecb.FormattingEnabled = True
        Me.FisherModecb.Items.AddRange(New Object() {"Normal", "Turbo"})
        Me.FisherModecb.Location = New System.Drawing.Point(0, 16)
        Me.FisherModecb.Name = "FisherModecb"
        Me.FisherModecb.Size = New System.Drawing.Size(93, 21)
        Me.FisherModecb.TabIndex = 1
        Me.FisherModecb.Text = "Select Mode"
        '
        'FisherModelbl
        '
        Me.FisherModelbl.AutoSize = True
        Me.FisherModelbl.Location = New System.Drawing.Point(3, 0)
        Me.FisherModelbl.Name = "FisherModelbl"
        Me.FisherModelbl.Size = New System.Drawing.Size(70, 13)
        Me.FisherModelbl.TabIndex = 0
        Me.FisherModelbl.Text = "Fishing Mode"
        '
        'Namespy
        '
        Me.Namespy.Controls.Add(Me.NamespyOnOffchk)
        Me.Namespy.Controls.Add(Me.NamespyStop)
        Me.Namespy.Location = New System.Drawing.Point(727, 12)
        Me.Namespy.Name = "Namespy"
        Me.Namespy.Size = New System.Drawing.Size(96, 54)
        Me.Namespy.TabIndex = 4
        '
        'NamespyOnOffchk
        '
        Me.NamespyOnOffchk.AutoSize = True
        Me.NamespyOnOffchk.Location = New System.Drawing.Point(0, 0)
        Me.NamespyOnOffchk.Name = "NamespyOnOffchk"
        Me.NamespyOnOffchk.Size = New System.Drawing.Size(89, 17)
        Me.NamespyOnOffchk.TabIndex = 1
        Me.NamespyOnOffchk.Text = "Show Names"
        Me.NamespyOnOffchk.UseVisualStyleBackColor = True
        '
        'NamespyStop
        '
        Me.NamespyStop.Location = New System.Drawing.Point(0, 23)
        Me.NamespyStop.Name = "NamespyStop"
        Me.NamespyStop.Size = New System.Drawing.Size(82, 20)
        Me.NamespyStop.TabIndex = 0
        Me.NamespyStop.Text = "Stop"
        Me.NamespyStop.UseVisualStyleBackColor = True
        '
        'Autohealer
        '
        Me.Autohealer.Controls.Add(Me.HealerStop)
        Me.Autohealer.Controls.Add(Me.HealerHptxtbox)
        Me.Autohealer.Controls.Add(Me.HealerOnOff)
        Me.Autohealer.Controls.Add(Me.HealerHplbl)
        Me.Autohealer.Controls.Add(Me.HealerNametxtbox)
        Me.Autohealer.Controls.Add(Me.HealerNamelbl)
        Me.Autohealer.Location = New System.Drawing.Point(12, 164)
        Me.Autohealer.Name = "Autohealer"
        Me.Autohealer.Size = New System.Drawing.Size(156, 161)
        Me.Autohealer.TabIndex = 5
        '
        'HealerStop
        '
        Me.HealerStop.Location = New System.Drawing.Point(3, 129)
        Me.HealerStop.Name = "HealerStop"
        Me.HealerStop.Size = New System.Drawing.Size(84, 20)
        Me.HealerStop.TabIndex = 5
        Me.HealerStop.Text = "Stop"
        Me.HealerStop.UseVisualStyleBackColor = True
        '
        'HealerHptxtbox
        '
        Me.HealerHptxtbox.Location = New System.Drawing.Point(3, 66)
        Me.HealerHptxtbox.Name = "HealerHptxtbox"
        Me.HealerHptxtbox.Size = New System.Drawing.Size(84, 20)
        Me.HealerHptxtbox.TabIndex = 4
        '
        'HealerOnOff
        '
        Me.HealerOnOff.Location = New System.Drawing.Point(3, 103)
        Me.HealerOnOff.Name = "HealerOnOff"
        Me.HealerOnOff.Size = New System.Drawing.Size(84, 20)
        Me.HealerOnOff.TabIndex = 3
        Me.HealerOnOff.Text = "Activate"
        Me.HealerOnOff.UseVisualStyleBackColor = True
        '
        'HealerHplbl
        '
        Me.HealerHplbl.AutoSize = True
        Me.HealerHplbl.Location = New System.Drawing.Point(0, 50)
        Me.HealerHplbl.Name = "HealerHplbl"
        Me.HealerHplbl.Size = New System.Drawing.Size(148, 13)
        Me.HealerHplbl.TabIndex = 2
        Me.HealerHplbl.Text = "Minimun Hitpoints (or Percent)"
        '
        'HealerNametxtbox
        '
        Me.HealerNametxtbox.Location = New System.Drawing.Point(3, 16)
        Me.HealerNametxtbox.Name = "HealerNametxtbox"
        Me.HealerNametxtbox.Size = New System.Drawing.Size(84, 20)
        Me.HealerNametxtbox.TabIndex = 1
        '
        'HealerNamelbl
        '
        Me.HealerNamelbl.AutoSize = True
        Me.HealerNamelbl.Location = New System.Drawing.Point(0, 0)
        Me.HealerNamelbl.Name = "HealerNamelbl"
        Me.HealerNamelbl.Size = New System.Drawing.Size(64, 13)
        Me.HealerNamelbl.TabIndex = 0
        Me.HealerNamelbl.Text = "Spell Words"
        '
        'WASD
        '
        Me.WASD.Controls.Add(Me.WASDStop)
        Me.WASD.Controls.Add(Me.WASDOnOffchk)
        Me.WASD.Location = New System.Drawing.Point(726, 78)
        Me.WASD.Name = "WASD"
        Me.WASD.Size = New System.Drawing.Size(107, 46)
        Me.WASD.TabIndex = 6
        '
        'WASDStop
        '
        Me.WASDStop.Location = New System.Drawing.Point(0, 23)
        Me.WASDStop.Name = "WASDStop"
        Me.WASDStop.Size = New System.Drawing.Size(82, 20)
        Me.WASDStop.TabIndex = 1
        Me.WASDStop.Text = "Stop"
        Me.WASDStop.UseVisualStyleBackColor = True
        '
        'WASDOnOffchk
        '
        Me.WASDOnOffchk.AutoSize = True
        Me.WASDOnOffchk.Location = New System.Drawing.Point(0, 0)
        Me.WASDOnOffchk.Name = "WASDOnOffchk"
        Me.WASDOnOffchk.Size = New System.Drawing.Size(81, 17)
        Me.WASDOnOffchk.TabIndex = 0
        Me.WASDOnOffchk.Text = "Use WASD"
        Me.WASDOnOffchk.UseVisualStyleBackColor = True
        '
        'ExpChecker
        '
        Me.ExpChecker.Controls.Add(Me.ExpStop)
        Me.ExpChecker.Controls.Add(Me.ExpCreaturesOnOffchk)
        Me.ExpChecker.Controls.Add(Me.ExpOnOffchk)
        Me.ExpChecker.Location = New System.Drawing.Point(180, 164)
        Me.ExpChecker.Name = "ExpChecker"
        Me.ExpChecker.Size = New System.Drawing.Size(173, 86)
        Me.ExpChecker.TabIndex = 7
        '
        'ExpStop
        '
        Me.ExpStop.Location = New System.Drawing.Point(0, 52)
        Me.ExpStop.Name = "ExpStop"
        Me.ExpStop.Size = New System.Drawing.Size(84, 20)
        Me.ExpStop.TabIndex = 2
        Me.ExpStop.Text = "Stop"
        Me.ExpStop.UseVisualStyleBackColor = True
        '
        'ExpCreaturesOnOffchk
        '
        Me.ExpCreaturesOnOffchk.AutoSize = True
        Me.ExpCreaturesOnOffchk.Location = New System.Drawing.Point(0, 23)
        Me.ExpCreaturesOnOffchk.Name = "ExpCreaturesOnOffchk"
        Me.ExpCreaturesOnOffchk.Size = New System.Drawing.Size(167, 17)
        Me.ExpCreaturesOnOffchk.TabIndex = 1
        Me.ExpCreaturesOnOffchk.Text = "Show Creatures to Next Level"
        Me.ExpCreaturesOnOffchk.UseVisualStyleBackColor = True
        '
        'ExpOnOffchk
        '
        Me.ExpOnOffchk.AutoSize = True
        Me.ExpOnOffchk.Location = New System.Drawing.Point(0, 0)
        Me.ExpOnOffchk.Name = "ExpOnOffchk"
        Me.ExpOnOffchk.Size = New System.Drawing.Size(103, 17)
        Me.ExpOnOffchk.TabIndex = 0
        Me.ExpOnOffchk.Text = "Show Exprience"
        Me.ExpOnOffchk.UseVisualStyleBackColor = True
        '
        'frmSubForms
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(869, 484)
        Me.Controls.Add(Me.ExpChecker)
        Me.Controls.Add(Me.WASD)
        Me.Controls.Add(Me.Autohealer)
        Me.Controls.Add(Me.Namespy)
        Me.Controls.Add(Me.AutoFisher)
        Me.Controls.Add(Me.Runemaker)
        Me.Controls.Add(Me.AutoEater)
        Me.Controls.Add(Me.Spellcaster)
        Me.Name = "frmSubForms"
        Me.Text = "There NEED TO BE better solution than this!"
        Me.Spellcaster.ResumeLayout(False)
        Me.Spellcaster.PerformLayout()
        Me.AutoEater.ResumeLayout(False)
        Me.AutoEater.PerformLayout()
        Me.Runemaker.ResumeLayout(False)
        Me.Runemaker.PerformLayout()
        Me.AutoFisher.ResumeLayout(False)
        Me.AutoFisher.PerformLayout()
        Me.Namespy.ResumeLayout(False)
        Me.Namespy.PerformLayout()
        Me.Autohealer.ResumeLayout(False)
        Me.Autohealer.PerformLayout()
        Me.WASD.ResumeLayout(False)
        Me.WASD.PerformLayout()
        Me.ExpChecker.ResumeLayout(False)
        Me.ExpChecker.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Spellcaster As System.Windows.Forms.Panel
    Friend WithEvents SpellNametxtbox As System.Windows.Forms.TextBox
    Friend WithEvents Spellnamelba As System.Windows.Forms.Label
    Friend WithEvents SpellOnOff As System.Windows.Forms.Button
    Friend WithEvents SpellManatxtbox As System.Windows.Forms.TextBox
    Friend WithEvents SpellMana As System.Windows.Forms.Label
    Friend WithEvents SpellStop As System.Windows.Forms.Button
    Friend WithEvents AutoEater As System.Windows.Forms.Panel
    Friend WithEvents EaterStop As System.Windows.Forms.Button
    Friend WithEvents EaterOnOff As System.Windows.Forms.Button
    Friend WithEvents EaterDelaytxtbox As System.Windows.Forms.TextBox
    Friend WithEvents Eaterdelaylbl As System.Windows.Forms.Label
    Friend WithEvents EaterSmartchk As System.Windows.Forms.CheckBox
    Friend WithEvents EaterSmartlbl As System.Windows.Forms.Label
    Friend WithEvents EaterSmarttxtbox As System.Windows.Forms.TextBox
    Friend WithEvents Runemaker As System.Windows.Forms.Panel
    Friend WithEvents MakerNametxtbox As System.Windows.Forms.TextBox
    Friend WithEvents MakerNamelbl As System.Windows.Forms.Label
    Friend WithEvents MakerManalbl As System.Windows.Forms.Label
    Friend WithEvents MakerOnOff As System.Windows.Forms.Button
    Friend WithEvents MakerSoultxtbox As System.Windows.Forms.TextBox
    Friend WithEvents MakerSoullbl As System.Windows.Forms.Label
    Friend WithEvents MakerManatxtbox As System.Windows.Forms.TextBox
    Friend WithEvents MakerStop As System.Windows.Forms.Button
    Friend WithEvents AutoFisher As System.Windows.Forms.Panel
    Friend WithEvents FisherModecb As System.Windows.Forms.ComboBox
    Friend WithEvents FisherModelbl As System.Windows.Forms.Label
    Friend WithEvents FisherStop As System.Windows.Forms.Button
    Friend WithEvents FisherOnOff As System.Windows.Forms.Button
    Friend WithEvents FisherCaplbl As System.Windows.Forms.Label
    Friend WithEvents FisherCaptxtbox As System.Windows.Forms.TextBox
    Friend WithEvents Namespy As System.Windows.Forms.Panel
    Friend WithEvents NamespyStop As System.Windows.Forms.Button
    Friend WithEvents NamespyOnOffchk As System.Windows.Forms.CheckBox
    Friend WithEvents Autohealer As System.Windows.Forms.Panel
    Friend WithEvents HealerNametxtbox As System.Windows.Forms.TextBox
    Friend WithEvents HealerNamelbl As System.Windows.Forms.Label
    Friend WithEvents HealerHplbl As System.Windows.Forms.Label
    Friend WithEvents HealerHptxtbox As System.Windows.Forms.TextBox
    Friend WithEvents HealerOnOff As System.Windows.Forms.Button
    Friend WithEvents HealerStop As System.Windows.Forms.Button
    Friend WithEvents WASDStop As System.Windows.Forms.Button
    Friend WithEvents WASD As System.Windows.Forms.Panel
    Friend WithEvents WASDOnOffchk As System.Windows.Forms.CheckBox
    Friend WithEvents ExpChecker As System.Windows.Forms.Panel
    Friend WithEvents ExpStop As System.Windows.Forms.Button
    Friend WithEvents ExpCreaturesOnOffchk As System.Windows.Forms.CheckBox
    Friend WithEvents ExpOnOffchk As System.Windows.Forms.CheckBox
End Class
