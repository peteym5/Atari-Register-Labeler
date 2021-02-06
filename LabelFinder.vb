Imports System
Imports System.String
Imports System.IO
Imports System.Text

Public Class LabelFinder
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

        Dim SR As StreamReader = New StreamReader(filenamein)


        DataIn = STRINGIMAGE
        DataOut = ""

        DataIn = Strings.Replace(DataIn, "$D000", "HPOSP0", 1, -1, Constants.vbTextCompare)  'Atari 8-bit GTIA Registers
        DataIn = Strings.Replace(DataIn, "$D001", "HPOSP1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$D002", "HPOSP2", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$D003", "HPOSP3", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$D004", "HPOSM0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$D005", "HPOSM1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$D006", "HPOSM2", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$D007", "HPOSM3", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$D008", "SIZEP0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$D009", "SIZEP1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$D00A", "SIZEP2", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$D00B", "SIZEP3", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$D00C", "SIZEM", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$D00D", "GRAFP0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$D00E", "GRAFP1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$D00F", "GRAFP2", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$D010", "GRAFP3", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$D011", "GRAFM ", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$D012", "COLPM0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$D013", "COLPM1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$D014", "COLPM2", 1, -1, Constants.vbTextCompare)
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
        DataIn = Strings.Replace(DataIn, "53248", "HPOSP0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "53249", "HPOSP1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "53250", "HPOSP2", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "53251", "HPOSP3", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "53252", "HPOSM0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "53253", "HPOSM1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "53254", "HPOSM2", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "53255", "HPOSM3", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "53256", "SIZEP0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "53257", "SIZEP1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "53258", "SIZEP2", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "53259", "SIZEP3", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "53260", "SIZEM", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "53261", "GRAFP0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "53262", "GRAFP1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "53263", "GRAFP2", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "53264", "GRAFP3", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "53265", "GRAFM ", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "53266", "COLPM0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "53267", "COLPM1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "53268", "COLPM2", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "53269", "COLPM3", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "53270", "COLPF0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "53271", "COLPF1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "53272", "COLPF2", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "53273", "COLPF3", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "53274", "COLBAK", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "53274", "COLBK", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "53275", "PRIOR", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "53276", "VDELAY", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "53277", "GRACTL", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "53278", "HITCLR", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "53279", "CONSOL", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$D200", "AUDF1", 1, -1, Constants.vbTextCompare)  'Atari 8-bit Pokey Registers
        DataIn = Strings.Replace(DataIn, "$D202", "AUDF2", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$D204", "AUDF3", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$D206", "AUDF4", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$D201", "AUDC1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$D203", "AUDC2", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$D205", "AUDC3", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$D207", "AUDC4", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$D208", "AUDCTL", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$D209", "STIMER", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$D20A", "SKRES", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$D20B", "POTGO", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$D20D", "SEROUT", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$D20E", "IRQEN", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$D20F", "SKCTL", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "53760", "AUDF1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "53762", "AUDF2", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "53764", "AUDF3", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "53766", "AUDF4", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "53761", "AUDC1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "53763", "AUDC2", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "53765", "AUDC3", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "53767", "AUDC4", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "53768", "AUDCTL", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "53769", "STIMER", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "53770", "SKRES", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "53771", "POTGO", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "53773", "SEROUT", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "53774", "IRQEN", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "53775", "SKCTL", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$D300", "PORTA", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$D301", "PORTB", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$D302", "PACTL", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$D303", "PBCTL", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "54016", "PORTA", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "54017", "PORTB", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "54018", "PACTL", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "54019", "PBCTL", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$D400", "DMACTL", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$D401", "CHACTL", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$D402", "DLISTL", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$D403", "DLISTH", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$D404", "HSCROL", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$D405", "VSCROL", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$D407", "PMBASE", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$D409", "CHBASE", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$D40A", "WSYNC ", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$D40B", "VCOUNT", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$D40C", "PENH", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$D40D", "PENV", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$D40E", "NMIEN", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$D40F", "NMIRES", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$D40F", "NMIST", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "54272", "DMACTL", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "54273", "CHACTL", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "54274", "DLISTL", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "54275", "DLISTH", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "54276", "HSCROL", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "54277", "VSCROL", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "54279", "PMBASE", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "54281", "CHBASE", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "54282", "WSYNC", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "54283", "VCOUNT", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "54284", "PENH ", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "54285", "PENV ", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "54286", "NMIEN", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "54287", "NMIRES", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "54287", "NMIST", 1, -1, Constants.vbTextCompare)

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
        DataIn = Strings.Replace(DataIn, "$0200", "VDSLST + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$0201", "VDSLST + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$0202", "VPRCED + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$0203", "VPRCED + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$0204", "VINTER + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$0205", "VINTER + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$0206", "VBREAK + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$0207", "VBREAK + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$0208", "VKEYBD + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$0209", "VKEYBD + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$020A", "VSERIN + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$020B", "VSERIN + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$020C", "VSEROR + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$020D", "VSEROR + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$020E", "VSEROC + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$020F", "VSEROC + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$0210", "VTIMR1 + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$0211", "VTIMR1 + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$0212", "VTIMR2 + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$0213", "VTIMR2 + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$0214", "VTIMR3 + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$0215", "VTIMR3 + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$0216", "VIMIRQ + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$0217", "VIMIRQ + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$0218", "CDTMV1 + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$0219", "CDTMV1 + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$021A", "CDTMV2 + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$021B", "CDTMV2 + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$021C", "CDTMV3 + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$021D", "CDTMV3 + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$022E", "CDTMV4 + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$022F", "CDTMV4 + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$0220", "CDTMV5 + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$0221", "CDTMV5 + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$0222", "VVBLKI + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$0223", "VVBLKI + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$0224", "VVBLKD + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$0225", "VVBLKD + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$0226", "CDTMA1 + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$0227", "CDTMA1 + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$0228", "CDTMA2 + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$0229", "CDTMA2 + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$022F", "SDMCTL", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$0230", "SDLSTL", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$0231", "SDLSTH", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$026F", "GPRIOR", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "512", "VDSLST + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "513", "VDSLST + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "514", "VPRCED + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "515", "VPRCED + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "516", "VINTER + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "517", "VINTER + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "518", "VBREAK + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "519", "VBREAK + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "520", "VKEYBD + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "521", "VKEYBD + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "522", "VSERIN + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "523", "VSERIN + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "524", "VSEROR + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "525", "VSEROR + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "526", "VSEROC + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "527", "VSEROC + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "528", "VTIMR1 + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "529", "VTIMR1 + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "530", "VTIMR2 + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "531", "VTIMR2 + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "532", "VTIMR3 + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "533", "VTIMR3 + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "534", "VIMIRQ + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "535", "VIMIRQ + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "536", "CDTMV1 + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "537", "CDTMV1 + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "538", "CDTMV2 + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "539", "CDTMV2 + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "540", "CDTMV3 + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "541", "CDTMV3 + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "542", "CDTMV4 + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "543", "CDTMV4 + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "544", "CDTMV5 + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "545", "CDTMV5 + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "546", "VVBLKI + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "547", "VVBLKI + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "548", "VVBLKD + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "549", "VVBLKD + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "550", "CDTMA1 + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "551", "CDTMA1 + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "552", "CDTMA2 + 0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "553", "CDTMA2 + 1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "559", "SDMCTL", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "560", "SDLSTL", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "561", "SDLSTH", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "623", "GPRIOR", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$02C0", "PCOLR0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$02C1", "PCOLR1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$02C2", "PCOLR2", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$02C3", "PCOLR3", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$02C4", "COLOR0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$02C5", "COLOR1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$02C6", "COLOR2", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$02C7", "COLOR3", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$02C8", "COLOR4", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$02F0", "CRSINH", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$02F3", "CHACT", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$02F4", "CHBAS", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "704", "PCOLR0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "705", "PCOLR1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "706", "PCOLR2", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "707", "PCOLR3", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "708", "COLOR0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "709", "COLOR1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "710", "COLOR2", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "711", "COLOR3", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "712", "COLOR4", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "752", "CRSINH", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "755", "CHACT", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "756", "CHBAS", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$278", "STCIK0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$279", "STCIK1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$27A", "STCIK2", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$27B", "STCIK3", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$284", "STRIG0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$285", "STRIG1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$286", "STRIG2", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "$287", "STRIG3", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "632", "STCIK0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "633", "STCIK1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "634", "STCIK2", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "635", "STCIK3", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "644", "STRIG0", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "645", "STRIG1", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "646", "STRIG2", 1, -1, Constants.vbTextCompare)
        DataIn = Strings.Replace(DataIn, "647", "STRIG3", 1, -1, Constants.vbTextCompare)
        'DataIn = Strings.Replace(DataIn, "$10", "IRQENS", 1, -1, Constants.vbTextCompare)
        'DataIn = Strings.Replace(DataIn, "$14", "RTCLOK1", 1, -1, Constants.vbTextCompare)
        'DataIn = Strings.Replace(DataIn, "$13", "RTCLOK2", 1, -1, Constants.vbTextCompare)
        'DataIn = Strings.Replace(DataIn, "$12", "RTCLOK3", 1, -1, Constants.vbTextCompare)
        'DataIn = Strings.Replace(DataIn, "$4D", "ATRACT", 1, -1, Constants.vbTextCompare)
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
