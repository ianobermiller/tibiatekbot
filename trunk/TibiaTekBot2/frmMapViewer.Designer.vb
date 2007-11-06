<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMapViewer
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
        Me.components = New System.ComponentModel.Container
        Dim ListViewItem1 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Myself", 19)
        Dim ListViewItem2 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Walkable", 4)
        Dim ListViewItem3 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Blocked", 17)
        Dim ListViewItem4 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem(New String() {"Void"}, 15, System.Drawing.Color.Empty, System.Drawing.SystemColors.Window, Nothing)
        Dim ListViewItem5 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Water", 6)
        Dim ListViewItem6 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Water W/ Fish", 5)
        Dim ListViewItem7 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Swamp", 2)
        Dim ListViewItem8 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Depot", 8)
        Dim ListViewItem9 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Warp", 3)
        Dim ListViewItem10 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Upstairs", 1)
        Dim ListViewItem11 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Downstairs", 0)
        Dim ListViewItem12 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Ladder", 14)
        Dim ListViewItem13 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Hole", 13)
        Dim ListViewItem14 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Open Door", 11)
        Dim ListViewItem15 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Closed Door", 9)
        Dim ListViewItem16 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Locked Door", 10)
        Dim ListViewItem17 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Player", 18)
        Dim ListViewItem18 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Mailbox", 16)
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMapViewer))
        Me.Timer = New System.Windows.Forms.Timer(Me.components)
        Me.Map = New System.Windows.Forms.GroupBox
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.TileObjectsList = New System.Windows.Forms.ListBox
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.Legend = New System.Windows.Forms.ListView
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Floor = New System.Windows.Forms.NumericUpDown
        Me.Map.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.Floor, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Timer
        '
        Me.Timer.Interval = 1000
        '
        'Map
        '
        Me.Map.Controls.Add(Me.PictureBox1)
        Me.Map.Location = New System.Drawing.Point(12, 12)
        Me.Map.Name = "Map"
        Me.Map.Size = New System.Drawing.Size(469, 393)
        Me.Map.TabIndex = 2
        Me.Map.TabStop = False
        Me.Map.Text = "Map"
        '
        'PictureBox1
        '
        Me.PictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PictureBox1.Location = New System.Drawing.Point(6, 19)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(453, 353)
        Me.PictureBox1.TabIndex = 1
        Me.PictureBox1.TabStop = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.TileObjectsList)
        Me.GroupBox2.Location = New System.Drawing.Point(487, 62)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(182, 218)
        Me.GroupBox2.TabIndex = 3
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Tile Objects"
        '
        'TileObjectsList
        '
        Me.TileObjectsList.FormattingEnabled = True
        Me.TileObjectsList.Location = New System.Drawing.Point(9, 22)
        Me.TileObjectsList.Name = "TileObjectsList"
        Me.TileObjectsList.Size = New System.Drawing.Size(165, 186)
        Me.TileObjectsList.TabIndex = 2
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Legend)
        Me.GroupBox3.Location = New System.Drawing.Point(487, 286)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(182, 118)
        Me.GroupBox3.TabIndex = 4
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Legend"
        '
        'Legend
        '
        Me.Legend.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        ListViewItem1.StateImageIndex = 0
        ListViewItem2.StateImageIndex = 0
        ListViewItem3.StateImageIndex = 0
        ListViewItem4.StateImageIndex = 0
        ListViewItem5.StateImageIndex = 0
        ListViewItem6.StateImageIndex = 0
        ListViewItem7.StateImageIndex = 0
        ListViewItem8.StateImageIndex = 0
        ListViewItem9.StateImageIndex = 0
        ListViewItem10.StateImageIndex = 0
        ListViewItem11.StateImageIndex = 0
        ListViewItem12.StateImageIndex = 0
        ListViewItem13.StateImageIndex = 0
        ListViewItem14.StateImageIndex = 0
        ListViewItem15.StateImageIndex = 0
        ListViewItem16.StateImageIndex = 0
        ListViewItem17.StateImageIndex = 0
        ListViewItem18.StateImageIndex = 0
        Me.Legend.Items.AddRange(New System.Windows.Forms.ListViewItem() {ListViewItem1, ListViewItem2, ListViewItem3, ListViewItem4, ListViewItem5, ListViewItem6, ListViewItem7, ListViewItem8, ListViewItem9, ListViewItem10, ListViewItem11, ListViewItem12, ListViewItem13, ListViewItem14, ListViewItem15, ListViewItem16, ListViewItem17, ListViewItem18})
        Me.Legend.LabelWrap = False
        Me.Legend.Location = New System.Drawing.Point(9, 16)
        Me.Legend.Margin = New System.Windows.Forms.Padding(0)
        Me.Legend.MultiSelect = False
        Me.Legend.Name = "Legend"
        Me.Legend.ShowGroups = False
        Me.Legend.Size = New System.Drawing.Size(165, 91)
        Me.Legend.SmallImageList = Me.ImageList1
        Me.Legend.TabIndex = 0
        Me.Legend.UseCompatibleStateImageBehavior = False
        Me.Legend.View = System.Windows.Forms.View.SmallIcon
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "stairsgodown.gif")
        Me.ImageList1.Images.SetKeyName(1, "stairsgoup.gif")
        Me.ImageList1.Images.SetKeyName(2, "swamp.gif")
        Me.ImageList1.Images.SetKeyName(3, "teleport.gif")
        Me.ImageList1.Images.SetKeyName(4, "walkable.gif")
        Me.ImageList1.Images.SetKeyName(5, "waterfish.gif")
        Me.ImageList1.Images.SetKeyName(6, "waternofish.gif")
        Me.ImageList1.Images.SetKeyName(7, "creature.gif")
        Me.ImageList1.Images.SetKeyName(8, "depot.gif")
        Me.ImageList1.Images.SetKeyName(9, "doorclosed.gif")
        Me.ImageList1.Images.SetKeyName(10, "doorlocked.gif")
        Me.ImageList1.Images.SetKeyName(11, "dooropen.gif")
        Me.ImageList1.Images.SetKeyName(12, "holeclosed.gif")
        Me.ImageList1.Images.SetKeyName(13, "holeopen.gif")
        Me.ImageList1.Images.SetKeyName(14, "ladder.gif")
        Me.ImageList1.Images.SetKeyName(15, "lava.gif")
        Me.ImageList1.Images.SetKeyName(16, "mailbox.gif")
        Me.ImageList1.Images.SetKeyName(17, "notwalkable.gif")
        Me.ImageList1.Images.SetKeyName(18, "otherplayer.gif")
        Me.ImageList1.Images.SetKeyName(19, "player.gif")
        Me.ImageList1.Images.SetKeyName(20, "stairs.gif")
        '
        'ToolTip1
        '
        Me.ToolTip1.AutomaticDelay = 100
        Me.ToolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
        Me.ToolTip1.ToolTipTitle = "Location"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Floor)
        Me.GroupBox1.Location = New System.Drawing.Point(487, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(179, 44)
        Me.GroupBox1.TabIndex = 5
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Z"
        '
        'Floor
        '
        Me.Floor.Location = New System.Drawing.Point(9, 18)
        Me.Floor.Maximum = New Decimal(New Integer() {7, 0, 0, 0})
        Me.Floor.Name = "Floor"
        Me.Floor.Size = New System.Drawing.Size(164, 20)
        Me.Floor.TabIndex = 0
        '
        'frmMapViewer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(678, 416)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.Map)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = Global.TibiaTekBot.My.Resources.Resources.ttb21
        Me.MaximizeBox = False
        Me.Name = "frmMapViewer"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.Text = "Map Viewer"
        Me.Map.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.Floor, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Timer As System.Windows.Forms.Timer
    Friend WithEvents Map As System.Windows.Forms.GroupBox
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents TileObjectsList As System.Windows.Forms.ListBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Legend As System.Windows.Forms.ListView
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Floor As System.Windows.Forms.NumericUpDown
End Class
