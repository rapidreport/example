Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions

Imports jp.co.systembase.report.data

' CSVデータを帳票に渡すためのクラス
Public Class CsvDataSource
    Implements IReportDataSource

    Private colNames As List(Of String)
    Private rows As New List(Of List(Of String))

    Private Shared dateRegex As New Regex("^\d{4}/\d{1,2}/\d{1,2}$")
    Private Shared numRegex As New Regex("^-?\d+(\.\d*)?$")

    Public Sub New(r As StreamReader)
        ' 最初の行から列名を読み込みます
        colNames = readCsv(r)
        ' データ本体を読み込みます
        Do
            Dim row As List(Of String) = readCsv(r)
            If row Is Nothing Then
                Exit Do
            Else
                rows.Add(row)
            End If
        Loop
    End Sub

    ' データの行数を返します
    Public Function Size() As Integer Implements IReportDataSource.Size
        Return rows.Count
    End Function

    ' i行目のkey列の値を返します
    Public Function [Get](i As Integer, key As String) As Object Implements IReportDataSource.Get
        Dim j As Integer = colNames.IndexOf(key)
        If j = -1 Then
            Return Nothing
        Else
            Return parseValue(rows(i)(j))
        End If
    End Function

    ' CSVデータを1行読み込みます
    Private Function readCsv(r As StreamReader) As List(Of String)
        Dim c As Integer = r.Read()
        If c = -1 Then
            Return Nothing
        End If
        Dim ret As New List(Of String)
        Dim sb As New StringBuilder
        Dim q As Boolean = False
        Dim qe As Boolean = False
        Dim cr As Boolean = False
        Do While c <> -1
            If c = &H22 Then ' ダブルクオート["]の処理
                If Not q Then
                    q = True
                ElseIf Not qe Then
                    qe = True
                Else
                    ' 値にダブルクオートを追加
                    sb.Append("""")
                    qe = False
                End If
            ElseIf c = &HD Then ' CRならばフラグを立てる
                cr = True
            Else
                If qe Then
                    q = False
                    qe = False
                End If
                If Not q And cr And c = &HA Then ' CRLFならば行の区切り
                    Exit Do
                End If
                cr = False
                If Not q And c = &H2C Then ' カンマ[,]ならば値の区切り
                    ret.Add(sb.ToString)
                    sb = New StringBuilder
                ElseIf c = &HA Then ' LFならば値に改行[CRLF]を追加
                    If q Then
                        sb.Append(vbCrLf)
                    End If
                Else                    ' それ以外の文字を値に追加
                    sb.Append(Convert.ToChar(c))
                End If
            End If
            c = r.Read()
        Loop
        ret.Add(sb.ToString)
        Return ret
    End Function

    ' 値を適切な型に変換して返します
    Private Function parseValue(v As String) As Object
        Try
            If v IsNot Nothing Then
                If dateRegex.IsMatch(v) Then
                    Return DateTime.ParseExact(v, "yyyy/M/d", Nothing)
                ElseIf numRegex.IsMatch(v) Then
                    Return Decimal.Parse(v)
                End If
            End If
        Catch ex As Exception
        End Try
        Return v
    End Function

End Class
