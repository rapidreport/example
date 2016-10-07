using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

using jp.co.systembase.report.data;
using jp.co.systembase.report.component;

namespace example
{
    // CSVデータを帳票に渡すためのクラス
    public class CsvDataSource : IReportDataSource
    {

        private List<String> colNames;
        private List<List<String>> rows = new List<List<String>>();

        private static Regex dateRegex = new Regex("^\\d{4}/\\d{1,2}/\\d{1,2}$");
        private static Regex numRegex = new Regex("^-?\\d+(\\.\\d*)?$");

        public CsvDataSource(StreamReader r)
        {
            // 最初の行から列名を読み込みます
            colNames = readCsv(r);
            // データ本体を読み込みます
            while (true)
            {
                List<String> row = readCsv(r);
                if (row == null)
                {
                    break;
                }
                else
                {
                    rows.Add(row);
                }
            }
        }

        // データの行数を返します
        public int Size()
        {
            return this.rows.Count;
        }

        // i行目のkey列の値を返します
        public object Get(int i, String key)
        {
            int j = colNames.IndexOf(key);
            if (j == -1)
            {
                // 不明な列名が指定されたら例外を発生させます
                throw new UnknownFieldException(this, i, key);
            }
            else
            {
                return this.parseValue(this.rows[i][j]);
            }
        }

        // CSVデータを1行読み込みます
        private List<String> readCsv(StreamReader r)
        {
            int c = r.Read();
            if (c == -1)
            {
                return null;
            }
            List<String> ret = new List<String>();
            StringBuilder sb = new StringBuilder();
            Boolean q = false;
            Boolean qe = false;
            Boolean cr = false;
            while (c != -1)
            {
                if (c == 0x22)
                { // ダブルクオート["]の処理
                    if (!q)
                    {
                        q = true;
                    }
                    else if (!qe)
                    {
                        qe = true;
                    }
                    else
                    {
                        // 値にダブルクオートを追加
                        sb.Append("\"");
                        qe = false;
                    }
                }
                else if (c == 0xd) // CRならばフラグを立てる
                {
                    cr = true;
                }
                else
                {
                    if (qe)
                    {
                        q = false;
                        qe = false;
                    }
                    if (!q && cr && c == 0xA)
                    { // CRLFならば行の区切り
                        break;
                    }
                    cr = false;
                    if (!q && c == 0x2C) // カンマ[,]ならば値の区切り
                    {
                        ret.Add(sb.ToString());
                        sb = new StringBuilder();
                    }
                    else if (c == 0x0A) // LFならば値に改行[CRLF]を追加
                    {
                        if (q)
                        {
                            sb.Append("\r\n");
                        }
                    }
                    else
                    {
                        // それ以外の文字を値に追加
                        sb.Append(Convert.ToChar(c));
                    }
                }
                c = r.Read();
            }
            ret.Add(sb.ToString());
            return ret;
        }

        // 値を適切な型に変換して返します
        private Object parseValue(String v)
        {
            try
            {
                if (v != null)
                {
                    if (dateRegex.IsMatch(v))
                    {
                        return DateTime.ParseExact(v, "yyyy/M/d", null);
                    }
                    else if (numRegex.IsMatch(v))
                    {
                        return Decimal.Parse(v);
                    }
                }
            }
            catch (Exception) { }
            return v;
        }

    }
}
