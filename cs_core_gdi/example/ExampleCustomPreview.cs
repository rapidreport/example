using System;
using System.Data;

using jp.co.systembase.json;
using jp.co.systembase.report;
using jp.co.systembase.report.data;
using jp.co.systembase.report.renderer.gdi;

// 機能サンプル プレビュー画面のカスタマイズ
namespace example
{
    class ExampleCustomPreview
    {
        public static void Run()
        {
            Report report = new Report(Json.Read("report/example1.rrpt"));
            report.Fill(new ReportDataSource(getDataTable()));
            ReportPages pages = report.GetPages();

            Printer printer = new Printer(pages);
            // カスタマイズされたプレビュー画面を表示します
            MyFmPrintPreview preview = new MyFmPrintPreview(printer);
            preview.ShowDialog();
        }

        private static DataTable getDataTable()
        {
            DataTable ret = new DataTable();
            ret.Columns.Add("mitsumoriNo", typeof(Decimal));
            ret.Columns.Add("mitsumoriDate", typeof(DateTime));
            ret.Columns.Add("tokuisaki1", typeof(String));
            ret.Columns.Add("tokuisaki2", typeof(String));
            ret.Columns.Add("hinmei", typeof(String));
            ret.Columns.Add("irisu", typeof(Decimal));
            ret.Columns.Add("hakosu", typeof(Decimal));
            ret.Columns.Add("tani", typeof(String));
            ret.Columns.Add("tanka", typeof(Decimal));
            ret.Rows.Add(101, DateTime.ParseExact("2013/03/01", "yyyy/MM/dd", null),
                "株式会社 岩手商事", "北上支店",
                "ノートパソコン", 1, 10, "台", 70000);
            ret.Rows.Add(101, DateTime.ParseExact("2013/03/01", "yyyy/MM/dd", null),
                "株式会社 岩手商事", "北上支店",
                "モニター", 1, 10, "台", 20000);
            ret.Rows.Add(101, DateTime.ParseExact("2013/03/01", "yyyy/MM/dd", null),
                "株式会社 岩手商事", "北上支店",
                "プリンタ", 1, 2, "台", 25000);
            ret.Rows.Add(101, DateTime.ParseExact("2013/03/01", "yyyy/MM/dd", null),
                "株式会社 岩手商事", "北上支店",
                "トナーカートリッジ", 2, 2, "本", 5000);
            return ret;
        }
    }
}
