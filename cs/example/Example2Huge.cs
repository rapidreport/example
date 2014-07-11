using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Diagnostics;

using jp.co.systembase.json;
using jp.co.systembase.report;
using jp.co.systembase.report.data;
using jp.co.systembase.report.renderer.pdf;

// 基本サンプル2 売上明細表（PDF1000ページ）
namespace example
{
    class Example2Huge
    {
        public static void Run()
        {
            // 帳票定義ファイルを読み込みます
            Report report = new Report(Json.Read("report\\example2.rrpt"));

            // GlobalScopeに値を登録します
            report.GlobalScope.Add("startDate", DateTime.ParseExact("2013/02/01", "yyyy/MM/dd", null));
            report.GlobalScope.Add("endDate", DateTime.ParseExact("2013/02/28", "yyyy/MM/dd", null));
            report.GlobalScope.Add("printDate", DateTime.Today);
            report.GlobalScope.Add("kaisha", "株式会社　システムベース");

            // 帳票にデータを渡します
            report.Fill(new ReportDataSource(getDataTable()));

            // PDF出力の実行時間を計測します
            Stopwatch sw = new Stopwatch();
            sw.Start();

            // ページ分割を行います
            ReportPages pages = report.GetPages();

            // PDF出力
            using (FileStream fs = new FileStream("output\\example2_huge.pdf", FileMode.Create))
            {
                PdfRenderer renderer = new PdfRenderer(fs);
                // バックスラッシュ文字を円マーク文字に変換します
                renderer.Setting.ReplaceBackslashToYen = true;
                pages.Render(renderer);
            }

            // 計測結果を表示します
            System.Windows.Forms.MessageBox.Show("実行時間は" + sw.ElapsedMilliseconds + "ミリ秒です");
        }

        private static DataTable getDataTable()
        {
            DataTable ret = new DataTable();
            ret.Columns.Add("bumonCd", typeof(Decimal));
            ret.Columns.Add("bumon", typeof(String));
            ret.Columns.Add("uriageDate", typeof(DateTime));
            ret.Columns.Add("denpyoNo", typeof(Decimal));
            ret.Columns.Add("shohinCd", typeof(String));
            ret.Columns.Add("shohin", typeof(String));
            ret.Columns.Add("tanka", typeof(Decimal));
            ret.Columns.Add("suryo", typeof(Decimal));

            // 1000ページ分のデータを作成します
            for (int i = 1; i <= 100; i++)
            {
                for (int j = 1; j <= 50; j++)
                {
                    ret.Rows.Add(i, "部門" + i,
                        DateTime.ParseExact("2013/02/01", "yyyy/MM/dd", null),
                        j, "PC00001", "ノートパソコン", 70000, 10);
                    ret.Rows.Add(i, "部門" + i,
                        DateTime.ParseExact("2013/02/01", "yyyy/MM/dd", null),
                        j, "DP00002", "モニター", 25000, 10);
                    ret.Rows.Add(i, "部門" + i,
                        DateTime.ParseExact("2013/02/01", "yyyy/MM/dd", null),
                        j, "PR00003", "プリンタ", 20000, 2);
                    ret.Rows.Add(i, "部門" + i,
                        DateTime.ParseExact("2013/02/10", "yyyy/MM/dd", null),
                        j, "PR00003", "プリンタ", 20000, 3);
                }
            }
            return ret;
        }

    }
}
