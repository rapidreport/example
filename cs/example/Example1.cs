﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;

using NPOI.HSSF.UserModel;

using jp.co.systembase.json;
using jp.co.systembase.report;
using jp.co.systembase.report.data;
using jp.co.systembase.report.renderer.gdi;
using jp.co.systembase.report.renderer.pdf;
using jp.co.systembase.report.renderer.xls;

// 基本サンプル1 見積書
namespace example
{
    class Example1
    {

        public static void Run()
        {
            // 帳票定義ファイルを読み込みます
            Report report = new Report(Json.Read("report\\example1.rrpt"));

            // 帳票にデータを渡します
            report.Fill(new ReportDataSource(getDataTable()));

            // ページ分割を行います
            ReportPages pages = report.GetPages();

            // PDF出力
            using (FileStream fs = new FileStream("output\\example1.pdf", FileMode.Create))
            {
                PdfRenderer renderer = new PdfRenderer(fs);
                // バックスラッシュ文字を円マーク文字に変換します
                renderer.Setting.ReplaceBackslashToYen = true;
                pages.Render(renderer);
            }

            // XLS出力
            using (FileStream fs = new FileStream("output\\example1.xls", FileMode.Create))
            {
                HSSFWorkbook workbook = new HSSFWorkbook();
                XlsRenderer renderer = new XlsRenderer(workbook);
                // Renderメソッドを呼ぶ前に必ずNewSheetメソッドを呼んでワークシートを作成します
                renderer.NewSheet("見積書");
                pages.Render(renderer);
                workbook.Write(fs);
            }

            // 直接印刷、プレビュー画面表示
            {
                Printer printer = new Printer(pages);

                //// 直接印刷
                //// ダイアログを出して印刷します
                //if (printer.PrintDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                //{
                //    printer.PrintDocument.Print();
                //}

                //// ダイアログを出さずに印刷します
                //printer.PrintDocument.Print();

                // プレビュー画面表示
                FmPrintPreview preview = new FmPrintPreview(printer);
                // プレビュー画面が開かれた時点で表示倍率を現在のウィンドウサイズに合わせます
                preview.StartUpZoomFit = true;
                preview.ShowDialog();
            }

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