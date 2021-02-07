Imports System
Imports System.String
Imports System.IO
Imports System.Text

Public Class RegisterLabeler
    Public STRINGIMAGE As String
    Public STRINGDATASIZE As UInteger
    Public SegSize As UInteger = 65536
    Public bytAr() As Byte
    Public filenamein As String
    Public filenametemp As String
    Public filenameout As String
    Public C As Integer
    Public F0 As Integer
    Public F1 As Integer
    Public F2 As Integer
    Public F3 As Integer
    Public F4 As Integer
    Public F5 As Integer
    Public F6 As Integer
    Public F7 As Integer
    Public F8 As Integer
    Public F9 As Integer
    Public Replacements(512) As UInteger
    Public N0 As Integer
    Public N1 As Integer
    Public N2 As Integer
    Public N3 As Integer
    Public N4 As Integer
    Public N5 As Integer
    Public N6 As Integer
    Public N7 As Integer
    Public N8 As Integer
    Public N9 As Integer
    Public Register_Read() As String = {"LDA ", "LDX ", "LDY ", "CMP ", "CPX ", "CPY ", "BIT "}
    Public Register_Write() As String = {"STA ", "STX ", "STY ", " ("}
    Public foundConv As Integer
    Public DataOut As String

    Private Sub Form1_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown
        OpenFileDialog1.InitialDirectory = My.Application.Info.DirectoryPath
        OpenFileDialog1.Filter = "6502 Assembly Listing Files|*.a65;*.asm;*.lst|Text Label File from MADS -T Switch|*.TXT;*.LAB;*.LBL"
        OpenFileDialog1.FileName = ""
        OpenFileDialog1.ShowDialog()
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click

        Dim DataIn As String


        Dim SetAddrRangeLo As Integer = 0
        Dim SetAddrRangeHi As Integer = 65528
        Dim SetIgnore0RangeLo As Integer = 0
        Dim SetIgnore0RangeHi As Integer = 0
        Dim SetIgnore1RangeLo As Integer = 0
        Dim SetIgnore1RangeHi As Integer = 0
        Dim SetIgnore2RangeLo As Integer = 0
        Dim SetIgnore2RangeHi As Integer = 0
        Dim SetIgnore3RangeLo As Integer = 0
        Dim SetIgnore3RangeHi As Integer = 0


        'Dim DataNumbers() As String
        Dim FirstNumber As Integer = 0
        Dim EndofLine As Integer = 0
        Dim LastEndofLine As Integer = 1
        Dim n As Integer
        Dim r As Integer
        Dim HexOut As String = ""
        Dim Pass As Integer


        Dim SR As StreamReader = New StreamReader(filenamein)


        DataIn = STRINGIMAGE
        DataOut = ""
        For Pass = 0 To UBound(Register_Write)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "$D000", Register_Write(Pass) + "HPOSP0", 1, -1, Constants.vbTextCompare)  'Atari 8-bit GTIA Registers
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "$D001", Register_Write(Pass) + "HPOSP1", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "$D002", Register_Write(Pass) + "HPOSP2", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "$D003", Register_Write(Pass) + "HPOSP3", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "$D004", Register_Write(Pass) + "HPOSM0", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "$D005", Register_Write(Pass) + "HPOSM1", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "$D006", Register_Write(Pass) + "HPOSM2", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "$D007", Register_Write(Pass) + "HPOSM3", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "$D008", Register_Write(Pass) + "SIZEP0", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "$D009", Register_Write(Pass) + "SIZEP1", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "$D00A", Register_Write(Pass) + "SIZEP2", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "$D00B", Register_Write(Pass) + "SIZEP3", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "$D00C", Register_Write(Pass) + "SIZEM", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "$D00D", Register_Write(Pass) + "GRAFP0", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "$D00E", Register_Write(Pass) + "GRAFP1", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "$D00F", Register_Write(Pass) + "GRAFP2", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "$D010", Register_Write(Pass) + "GRAFP3", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "$D011", Register_Write(Pass) + "GRAFM ", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "$D012", Register_Write(Pass) + "COLPM0", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "$D013", Register_Write(Pass) + "COLPM1", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "$D014", Register_Write(Pass) + "COLPM2", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "53248", Register_Write(Pass) + "HPOSP0", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "53249", Register_Write(Pass) + "HPOSP1", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "53250", Register_Write(Pass) + "HPOSP2", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "53251", Register_Write(Pass) + "HPOSP3", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "53252", Register_Write(Pass) + "HPOSM0", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "53253", Register_Write(Pass) + "HPOSM1", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "53254", Register_Write(Pass) + "HPOSM2", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "53255", Register_Write(Pass) + "HPOSM3", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "53256", Register_Write(Pass) + "SIZEP0", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "53257", Register_Write(Pass) + "SIZEP1", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "53258", Register_Write(Pass) + "SIZEP2", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "53259", Register_Write(Pass) + "SIZEP3", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "53260", Register_Write(Pass) + "SIZEM", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "53261", Register_Write(Pass) + "GRAFP0", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "53262", Register_Write(Pass) + "GRAFP1", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "53263", Register_Write(Pass) + "GRAFP2", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "53264", Register_Write(Pass) + "GRAFP3", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "53265", Register_Write(Pass) + "GRAFM ", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "53266", Register_Write(Pass) + "COLPM0", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "53267", Register_Write(Pass) + "COLPM1", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "$D200", Register_Write(Pass) + "AUDF1", 1, -1, Constants.vbTextCompare)  'Atari 8-bit Pokey Registers
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "$D202", Register_Write(Pass) + "AUDF2", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "$D204", Register_Write(Pass) + "AUDF3", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "$D206", Register_Write(Pass) + "AUDF4", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "$D201", Register_Write(Pass) + "AUDC1", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "$D203", Register_Write(Pass) + "AUDC2", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "$D205", Register_Write(Pass) + "AUDC3", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "$D207", Register_Write(Pass) + "AUDC4", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "$D208", Register_Write(Pass) + "AUDCTL", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "$D209", Register_Write(Pass) + "STIMER", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "$D20A", Register_Write(Pass) + "SKRES", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "$D20B", Register_Write(Pass) + "POTGO", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "$D20D", Register_Write(Pass) + "SEROUT", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "$D20E", Register_Write(Pass) + "IRQEN", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "$D20F", Register_Write(Pass) + "SKCTL", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "53760", Register_Write(Pass) + "AUDF1", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "53762", Register_Write(Pass) + "AUDF2", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "53764", Register_Write(Pass) + "AUDF3", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "53766", Register_Write(Pass) + "AUDF4", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "53761", Register_Write(Pass) + "AUDC1", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "53763", Register_Write(Pass) + "AUDC2", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "53765", Register_Write(Pass) + "AUDC3", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "53767", Register_Write(Pass) + "AUDC4", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "53768", Register_Write(Pass) + "AUDCTL", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "53769", Register_Write(Pass) + "STIMER", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "53770", Register_Write(Pass) + "SKRES", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "53771", Register_Write(Pass) + "POTGO", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "53773", Register_Write(Pass) + "SEROUT", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "53774", Register_Write(Pass) + "IRQEN", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "53775", Register_Write(Pass) + "SKCTL", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "$D400", Register_Write(Pass) + "DMACTL", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "$D401", Register_Write(Pass) + "CHACTL", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "$D402", Register_Write(Pass) + "DLISTL", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "$D403", Register_Write(Pass) + "DLISTH", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "$D404", Register_Write(Pass) + "HSCROL", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "$D405", Register_Write(Pass) + "VSCROL", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "$D407", Register_Write(Pass) + "PMBASE", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "$D409", Register_Write(Pass) + "CHBASE", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "$D40A", Register_Write(Pass) + "WSYNC ", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "$D40B", Register_Write(Pass) + "VCOUNT", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "$D40C", Register_Write(Pass) + "PENH", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "$D40D", Register_Write(Pass) + "PENV", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "$D40E", Register_Write(Pass) + "NMIEN", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "$D40F", Register_Write(Pass) + "NMIRES", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "54272", Register_Write(Pass) + "DMACTL", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "54273", Register_Write(Pass) + "CHACTL", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "54274", Register_Write(Pass) + "DLISTL", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "54275", Register_Write(Pass) + "DLISTH", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "54276", Register_Write(Pass) + "HSCROL", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "54277", Register_Write(Pass) + "VSCROL", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "54279", Register_Write(Pass) + "PMBASE", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "54281", Register_Write(Pass) + "CHBASE", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "54282", Register_Write(Pass) + "WSYNC", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "54283", Register_Write(Pass) + "VCOUNT", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "54284", Register_Write(Pass) + "PENH ", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "54285", Register_Write(Pass) + "PENV ", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "54286", Register_Write(Pass) + "NMIEN", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Write(Pass) + "54287", Register_Write(Pass) + "NMIRES", 1, -1, Constants.vbTextCompare)
        Next Pass




        For Pass = 0 To UBound(Register_Read)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "$D000", Register_Read(Pass) + "M0PF", 1, -1, Constants.vbTextCompare)  'Atari 8-bit GTIA Registers
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "$D001", Register_Read(Pass) + "M1PF", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "$D002", Register_Read(Pass) + "M2PF", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "$D003", Register_Read(Pass) + "M3PF", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "$D004", Register_Read(Pass) + "P0PF", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "$D005", Register_Read(Pass) + "P1PF", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "$D006", Register_Read(Pass) + "P2PF", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "$D007", Register_Read(Pass) + "P3PF", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "$D008", Register_Read(Pass) + "M0PL", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "$D009", Register_Read(Pass) + "M1PL", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "$D00A", Register_Read(Pass) + "M2PL", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "$D00B", Register_Read(Pass) + "M3PL", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "$D00C", Register_Read(Pass) + "P0PL", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "$D00D", Register_Read(Pass) + "P1PL", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "$D00E", Register_Read(Pass) + "P2PL", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "$D00F", Register_Read(Pass) + "P3PL", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "$D010", Register_Read(Pass) + "TRIG0", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "$D011", Register_Read(Pass) + "TRIG1", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "$D012", Register_Read(Pass) + "TRIG2", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "$D013", Register_Read(Pass) + "TRIG3", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "$D014", Register_Read(Pass) + "PAL", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "53248", Register_Read(Pass) + "M0PF", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "53249", Register_Read(Pass) + "M1PF", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "53250", Register_Read(Pass) + "M2PF", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "53251", Register_Read(Pass) + "M3PF", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "53252", Register_Read(Pass) + "P0PF", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "53253", Register_Read(Pass) + "P1PF", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "53254", Register_Read(Pass) + "P2PF", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "53255", Register_Read(Pass) + "P3PF", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "53256", Register_Read(Pass) + "M0PL", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "53257", Register_Read(Pass) + "M1PL", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "53258", Register_Read(Pass) + "M2PL", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "53259", Register_Read(Pass) + "M3PL", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "53260", Register_Read(Pass) + "P0PL", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "53261", Register_Read(Pass) + "P1PL", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "53262", Register_Read(Pass) + "P2PL", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "53263", Register_Read(Pass) + "P3PL", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "53264", Register_Read(Pass) + "TRIG0", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "53265", Register_Read(Pass) + "TRIG1", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "53266", Register_Read(Pass) + "TRIG2", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "53267", Register_Read(Pass) + "TRIG3", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "53268", Register_Read(Pass) + "PAL", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "$D200", Register_Read(Pass) + "POT0", 1, -1, Constants.vbTextCompare)  'Atari 8-bit Pokey Registers
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "$D202", Register_Read(Pass) + "POT1", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "$D204", Register_Read(Pass) + "POT2", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "$D206", Register_Read(Pass) + "POT3", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "$D201", Register_Read(Pass) + "POT4", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "$D203", Register_Read(Pass) + "POT5", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "$D205", Register_Read(Pass) + "POT6", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "$D207", Register_Read(Pass) + "POT7", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "$D208", Register_Read(Pass) + "ALLPOT", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "$D209", Register_Read(Pass) + "POTST", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "$D20A", Register_Read(Pass) + "KBCODE", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "$D20B", Register_Read(Pass) + "RANDOM", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "$D20D", Register_Read(Pass) + "SERIN	", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "$D20E", Register_Read(Pass) + "IRQST	", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "$D20F", Register_Read(Pass) + "SKSTAT", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "53760", Register_Read(Pass) + "POT0", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "53762", Register_Read(Pass) + "POT1", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "53764", Register_Read(Pass) + "POT2", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "53766", Register_Read(Pass) + "POT3", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "53761", Register_Read(Pass) + "POT4", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "53763", Register_Read(Pass) + "POT5", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "53765", Register_Read(Pass) + "POT6", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "53767", Register_Read(Pass) + "POT7", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "53768", Register_Read(Pass) + "ALLPOT", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "53769", Register_Read(Pass) + "POTST	", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "53770", Register_Read(Pass) + "KBCODE", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "53771", Register_Read(Pass) + "RANDOM", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "53773", Register_Read(Pass) + "SERIN	", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "53774", Register_Read(Pass) + "IRQST	", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "53775", Register_Read(Pass) + "SKSTAT", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "$D400", Register_Read(Pass) + "DMACTL", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "$D401", Register_Read(Pass) + "CHACTL", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "$D402", Register_Read(Pass) + "DLISTL", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "$D403", Register_Read(Pass) + "DLISTH", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "$D404", Register_Read(Pass) + "HSCROL", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "$D405", Register_Read(Pass) + "VSCROL", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "$D407", Register_Read(Pass) + "PMBASE", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "$D409", Register_Read(Pass) + "CHBASE", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "$D40A", Register_Read(Pass) + "WSYNC ", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "$D40B", Register_Read(Pass) + "VCOUNT", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "$D40C", Register_Read(Pass) + "PENH", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "$D40D", Register_Read(Pass) + "PENV", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "$D40E", Register_Read(Pass) + "NMIEN", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "$D40F", Register_Read(Pass) + "NMIST", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "54272", Register_Read(Pass) + "DMACTL", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "54273", Register_Read(Pass) + "CHACTL", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "54274", Register_Read(Pass) + "DLISTL", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "54275", Register_Read(Pass) + "DLISTH", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "54276", Register_Read(Pass) + "HSCROL", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "54277", Register_Read(Pass) + "VSCROL", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "54279", Register_Read(Pass) + "PMBASE", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "54281", Register_Read(Pass) + "CHBASE", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "54282", Register_Read(Pass) + "WSYNC", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "54283", Register_Read(Pass) + "VCOUNT", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "54284", Register_Read(Pass) + "PENH ", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "54285", Register_Read(Pass) + "PENV ", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "54286", Register_Read(Pass) + "NMIEN", 1, -1, Constants.vbTextCompare)
            DataIn = Strings.Replace(DataIn, Register_Read(Pass) + "54287", Register_Read(Pass) + "NMIST", 1, -1, Constants.vbTextCompare)
        Next Pass

        DataIn = Strings.Replace(DataIn, "$D015", "COLPM3", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$D016", "COLPF0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$D017", "COLPF1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$D018", "COLPF2", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$D019", "COLPF3", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$D01A", "COLBAK", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$D01A", "COLBK ", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$D01B", "PRIOR ", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$D01C", "VDELAY", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$D01D", "GRACTL", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$D01E", "HITCLR", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$D01F", "CONSOL", 1, -1, Constants.vbTextCompare)


        DataIn = Strings.Replace(DataIn, "$D300", "PORTA", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$D301", "PORTB", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$D302", "PACTL", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$D303", "PBCTL", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "54016", "PORTA", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "54017", "PORTB", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "54018", "PACTL", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "54019", "PBCTL", 1, -1, Constants.vbTextCompare)


        DataIn = Strings.Replace(DataIn, "$FFFA", "NMIV", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$FFFC", "RESETV", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$FFFE", "IRQV", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$C642", "PUTLINE", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$E453", "DSKINT", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$E456", "CIOMAIN", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$E4DF", "CIOVXLE", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$E4C4", "CIOV800", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$E456", "CIOVANY", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$E459", "SIOINT", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$E471", "SIOV", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$E477", "SELFTST", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$E477", "COLDV", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$EEBC", "COLD", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$F9C2", "NEWDEV", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$E45C", "SETVBV", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$E45F", "SYSVBV", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$E462", "XITVBV", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $0200", " VDSLST + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $0201", " VDSLST + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $0202", " VPRCED + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $0203", " VPRCED + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $0204", " VINTER + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $0205", " VINTER + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $0206", " VBREAK + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $0207", " VBREAK + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $0208", " VKEYBD + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $0209", " VKEYBD + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $020A", " VSERIN + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $020B", " VSERIN + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $020C", " VSEROR + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $020D", " VSEROR + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $020E", " VSEROC + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $020F", " VSEROC + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $0210", " VTIMR1 + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $0211", " VTIMR1 + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $0212", " VTIMR2 + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $0213", " VTIMR2 + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $0214", " VTIMR3 + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $0215", " VTIMR3 + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $0216", " VIMIRQ + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $0217", " VIMIRQ + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $0218", " CDTMV1 + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $0219", " CDTMV1 + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $021A", " CDTMV2 + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $021B", " CDTMV2 + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $021C", " CDTMV3 + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $021D", " CDTMV3 + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $022E", " CDTMV4 + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $022F", " CDTMV4 + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $0220", " CDTMV5 + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $0221", " CDTMV5 + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $0222", " VVBLKI + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $0223", " VVBLKI + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $0224", " VVBLKD + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $0225", " VVBLKD + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $0226", " CDTMA1 + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $0227", " CDTMA1 + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $0228", " CDTMA2 + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $0229", " CDTMA2 + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $022F", " SDMCTL", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $0230", " SDLSTL", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $0231", " SDLSTH", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $026F", " GPRIOR", 1, -1, Constants.vbTextCompare)


        DataIn = Strings.Replace(DataIn, " $200", " VDSLST + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $201", " VDSLST + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $202", " VPRCED + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $203", " VPRCED + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $204", " VINTER + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $205", " VINTER + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $206", " VBREAK + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $207", " VBREAK + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $208", " VKEYBD + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $209", " VKEYBD + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $20A", " VSERIN + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $20B", " VSERIN + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $20C", " VSEROR + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $20D", " VSEROR + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $20E", " VSEROC + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $20F", " VSEROC + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $210", " VTIMR1 + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $211", " VTIMR1 + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $212", " VTIMR2 + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $213", " VTIMR2 + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $214", " VTIMR3 + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $215", " VTIMR3 + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $216", " VIMIRQ + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $217", " VIMIRQ + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $218", " CDTMV1 + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $219", " CDTMV1 + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $21A", " CDTMV2 + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $21B", " CDTMV2 + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $21C", " CDTMV3 + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $21D", " CDTMV3 + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $22E", " CDTMV4 + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $22F", " CDTMV4 + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $220", " CDTMV5 + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $221", " CDTMV5 + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $222", " VVBLKI + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $223", " VVBLKI + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $224", " VVBLKD + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $225", " VVBLKD + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $226", " CDTMA1 + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $227", " CDTMA1 + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $228", " CDTMA2 + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $229", " CDTMA2 + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $22F", " SDMCTL", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $230", " SDLSTL", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $231", " SDLSTH", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $26F", " GPRIOR", 1, -1, Constants.vbTextCompare)



        DataIn = Strings.Replace(DataIn, " 512", " VDSLST + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " 513", " VDSLST + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " 514", " VPRCED + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " 515", " VPRCED + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " 516", " VINTER + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " 517", " VINTER + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " 518", " VBREAK + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " 519", " VBREAK + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " 520", " VKEYBD + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " 521", " VKEYBD + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " 522", " VSERIN + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " 523", " VSERIN + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " 524", " VSEROR + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " 525", " VSEROR + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " 526", " VSEROC + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " 527", " VSEROC + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " 528", " VTIMR1 + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " 529", " VTIMR1 + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " 530", " VTIMR2 + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " 531", " VTIMR2 + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " 532", " VTIMR3 + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " 533", " VTIMR3 + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " 534", " VIMIRQ + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " 535", " VIMIRQ + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " 536", " CDTMV1 + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " 537", " CDTMV1 + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " 538", " CDTMV2 + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " 539", " CDTMV2 + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " 540", " CDTMV3 + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " 541", " CDTMV3 + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " 542", " CDTMV4 + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " 543", " CDTMV4 + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " 544", " CDTMV5 + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " 545", " CDTMV5 + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " 546", " VVBLKI + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " 547", " VVBLKI + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " 548", " VVBLKD + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " 549", " VVBLKD + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " 550", " CDTMA1 + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " 551", " CDTMA1 + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " 552", " CDTMA2 + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " 553", " CDTMA2 + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " 559", " SDMCTL", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " 560", " SDLSTL", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " 561", " SDLSTH", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " 623", " GPRIOR", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $02C0", "PCOLR0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $02C1", "PCOLR1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $02C2", "PCOLR2", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $02C3", "PCOLR3", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $02C4", "COLOR0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $02C5", "COLOR1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $02C6", "COLOR2", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $02C7", "COLOR3", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $02C8", "COLOR4", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $02F0", "CRSINH", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $02F3", "CHACT", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " $02F4", "CHBAS", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " 704", " PCOLR0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " 705", " PCOLR1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " 706", " PCOLR2", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " 707", " PCOLR3", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " 708", " COLOR0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " 709", " COLOR1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " 710", " COLOR2", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " 711", " COLOR3", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " 712", " COLOR4", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " 752", " CRSINH", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " 755", " CHACT", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " 756", " CHBAS", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$278", "STCIK0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$279", "STCIK1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$27A", "STCIK2", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$27B", "STCIK3", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$284", "STRIG0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$285", "STRIG1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$286", "STRIG2", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$287", "STRIG3", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " 632", " STCIK0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " 633", " STCIK1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " 634", " STCIK2", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " 635", " STCIK3", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " 644", " STRIG0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " 645", " STRIG1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " 646", " STRIG2", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, " 647", " STRIG3", 1, -1, Constants.vbTextCompare)
        'DataIn = Strings.Replace(DataIn, " $10", " IRQENS", 1, -1, Constants.vbTextCompare)
        'DataIn = Strings.Replace(DataIn, " $14", " RTCLOK1", 1, -1, Constants.vbTextCompare)
        'DataIn = Strings.Replace(DataIn, " $13", " RTCLOK2", 1, -1, Constants.vbTextCompare)
        'DataIn = Strings.Replace(DataIn, " $12", " RTCLOK3", 1, -1, Constants.vbTextCompare)
        'DataIn = Strings.Replace(DataIn, " $4D", " ATRACT", 1, -1, Constants.vbTextCompare)
        'DataIn = Strings.Replace(DataIn, " 16", " IRQENS", 1, -1, Constants.vbTextCompare)
        'DataIn = Strings.Replace(DataIn, " 20", " RTCLOK1", 1, -1, Constants.vbTextCompare)
        'DataIn = Strings.Replace(DataIn, " 19", " RTCLOK2", 1, -1, Constants.vbTextCompare)
        'DataIn = Strings.Replace(DataIn, " 18", " RTCLOK3", 1, -1, Constants.vbTextCompare)
        'DataIn = Strings.Replace(DataIn, " 77", " ATRACT", 1, -1, Constants.vbTextCompare)

        DataOut = DataIn
        TextBox1.Text = DataOut
        TextBox1.SelectAll()
        TextBox1.Copy()
        Label1.Text = "Orignal Size:" & Len(STRINGIMAGE) & "  New Size:" & Len(DataOut)
        Label2.Text = n
        Label3.Text = "Copied to Buffer"

    End Sub


    Private Sub OpenFileDialog1_FileOk(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialog1.FileOk
        filenamein = OpenFileDialog1.FileName
        Dim ConvFileNumIn As Integer = FreeFile()
        'Dim fs As New FileStream(filenamein, FileMode.Open)
        Dim ConvFileNumOut As Integer = FreeFile()
        Dim info As New FileInfo(filenamein)
        STRINGDATASIZE = info.Length
        ReDim bytAr(STRINGDATASIZE - 1)
        STRINGIMAGE = New String(Chr(33), STRINGDATASIZE)
        FileOpen(ConvFileNumIn, filenamein, OpenMode.Binary, OpenAccess.Read)
        FileGet(ConvFileNumIn, STRINGIMAGE)
        FileClose(ConvFileNumIn)
        TextBox1.Text = STRINGIMAGE
        Label1.Text = filenamein
        Label2.Text = "0"
        Label3.Text = Len(STRINGIMAGE)
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub
End Class
