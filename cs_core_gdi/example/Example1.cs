﻿using System;
using System.Data;

using jp.co.systembase.json;
using jp.co.systembase.report;
using jp.co.systembase.report.data;
using jp.co.systembase.report.renderer.gdi;

// チュートリアル1 見積書
namespace example
{
    class Example1
    {

        public static void Run()
        {
            // 帳票定義ファイルを読み込みます
            Report report = new Report(Json.Read("report/example1.rrpt"));

            // 帳票にデータを渡します
            report.Fill(new ReportDataSource(getDataTable()));

            // ページ分割を行います
            ReportPages pages = report.GetPages();

            // プレビュー
            {
                FmPrintPreview preview = new FmPrintPreview(new Printer(pages));
                preview.StartUpZoomFit = true;
                preview.ShowDialog();
            }
        }

        // DataTableを用いたサンプル
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

        //// DTOを用いたサンプル
        //private static IList getDataTable()
        //{
        //    IList ret = new List<ExampleDto>();
        //    {
        //        ExampleDto r = new ExampleDto();
        //        r.Tokuisaki1 = "株式会社 岩手商事";
        //        r.Tokuisaki2 = "北上支店";
        //        r.MitsumoriNo = 101;
        //        r.MitsumoriDate = DateTime.ParseExact("2013/03/01", "yyyy/MM/dd", null);
        //        r.Hinmei = "プリンタ";
        //        r.Irisu = 1;
        //        r.Hakosu = 2;
        //        r.Tani = "台";
        //        r.Tanka = 25000;
        //        ret.Add(r);
        //    }
        //    {
        //        ExampleDto r = new ExampleDto();
        //        r.Tokuisaki1 = "株式会社 岩手商事";
        //        r.Tokuisaki2 = "北上支店";
        //        r.MitsumoriNo = 101;
        //        r.MitsumoriDate = DateTime.ParseExact("2013/03/01", "yyyy/MM/dd", null);
        //        r.Hinmei = "トナーカートリッジ";
        //        r.Irisu = 2;
        //        r.Hakosu = 2;
        //        r.Tani = "本";
        //        r.Tanka = 5000;
        //        ret.Add(r);
        //    }
        //    {
        //        ExampleDto r = new ExampleDto();
        //        r.Tokuisaki1 = "株式会社 岩手商事";
        //        r.Tokuisaki2 = "北上支店";
        //        r.MitsumoriNo = 101;
        //        r.MitsumoriDate = DateTime.ParseExact("2013/03/01", "yyyy/MM/dd", null);
        //        r.Hinmei = "モニター";
        //        r.Irisu = 1;
        //        r.Hakosu = 2;
        //        r.Tani = "台";
        //        r.Tanka = 20000;
        //        ret.Add(r);
        //    }
        //    return ret;
        //}

    }
}
