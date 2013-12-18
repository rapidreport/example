using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;

using NPOI.HSSF.UserModel;

using jp.co.systembase.json;
using jp.co.systembase.report;
using jp.co.systembase.report.data;
using jp.co.systembase.report.component;
using jp.co.systembase.report.customizer;
using jp.co.systembase.report.renderer.gdi;
using jp.co.systembase.report.renderer.pdf;
using jp.co.systembase.report.renderer.xls;

// 「特徴と機能一覧」に掲載したサンプル
namespace example
{
    class Feature
    {

        public static void Run()
        {

            Report report = new Report(Json.Read("report\\feature.rrpt"));

            // "feature-4"にgetDataTable1-4をそれぞれ割り当てます
            GroupDataProvider dataProvider = new GroupDataProvider();
            dataProvider.GroupDataMap.Add("feature1", new ReportDataSource(getDataTable1()));
            dataProvider.GroupDataMap.Add("feature2", new ReportDataSource(getDataTable2()));
            dataProvider.GroupDataMap.Add("feature3", new ReportDataSource(getDataTable3()));
            dataProvider.GroupDataMap.Add("feature4", new ReportDataSource(getDataTable4()));
            
            // 第2引数にdataProviderを渡します
            report.Fill(DummyDataSource.GetInstance(), dataProvider);
            ReportPages pages = report.GetPages();

            // PDF出力
            using (FileStream fs = new FileStream("output\\feature.pdf", FileMode.Create))
            {
                PdfRenderer renderer = new PdfRenderer(fs);
                renderer.Setting.ReplaceBackslashToYen = true;
                pages.Render(renderer);
            }

            // XLS出力
            using (FileStream fs = new FileStream("output\\feature.xls", FileMode.Create))
            {
                HSSFWorkbook workbook = new HSSFWorkbook();
                XlsRenderer renderer = new XlsRenderer(workbook);
                renderer.NewSheet("feature");
                pages.Render(renderer);
                workbook.Write(fs);
            }

            // プレビュー
            {
                FmPrintPreview preview = new FmPrintPreview(new Printer(pages));
                preview.StartUpZoomFit = true;
                preview.ShowDialog();
            }

        }

        private static DataTable getDataTable1()
        {
            DataTable ret = new DataTable();
            ret.Columns.Add("KEY1", typeof(String));
            ret.Columns.Add("KEY2", typeof(String));
            ret.Columns.Add("VALUE", typeof(String));
            ret.Rows.Add("大分類Ａ", "小分類１", "データ１");
            ret.Rows.Add("大分類Ａ", "小分類１", "データ２");
            ret.Rows.Add("大分類Ａ", "小分類２", "データ３");
            ret.Rows.Add("大分類Ｂ", "小分類３", "データ４");
            ret.Rows.Add("大分類Ｂ", "小分類３", "データ５");
            ret.Rows.Add("大分類Ｂ", "小分類３", "データ６");
            ret.Rows.Add("大分類Ｃ", "小分類４", "データ７");
            return ret;
        }

        private static DataTable getDataTable2()
        {
            DataTable ret = new DataTable();
            ret.Columns.Add("REGION", typeof(String));
            ret.Columns.Add("PREF", typeof(String));
            ret.Rows.Add("北海道", "北海道");
            ret.Rows.Add("東北", "青森");
            ret.Rows.Add("東北", "岩手");
            ret.Rows.Add("東北", "秋田");
            ret.Rows.Add("東北", "宮城");
            ret.Rows.Add("東北", "山形");
            ret.Rows.Add("東北", "福島");
            ret.Rows.Add("関東", "茨城");
            ret.Rows.Add("関東", "栃木");
            ret.Rows.Add("関東", "群馬");
            ret.Rows.Add("関東", "埼玉");
            ret.Rows.Add("関東", "ちば");
            ret.Rows.Add("関東", "東京");
            ret.Rows.Add("関東", "神奈川");
            return ret;
        }

        private static DataTable getDataTable3()
        {
            DataTable ret = new DataTable();
            ret.Columns.Add("WRAP", typeof(String));
            ret.Columns.Add("SHRINK", typeof(String));
            ret.Columns.Add("FIXDEC", typeof(Decimal));
            ret.Rows.Add("RapidRport", "ラピッドレポート", 12345);
            ret.Rows.Add("RapidReport 帳票ツール", "ラピッドレポート　帳票ツール", 1234.1);
            ret.Rows.Add("開発者のための帳票ツール", "開発者のための帳票ツール", 123.12);
            ret.Rows.Add("(株)システムベース", "(株)システムベース", 12.123);
            return ret;
        }

        private static DataTable getDataTable4()
        {
            DataTable ret = new DataTable();
            ret.Columns.Add("VALUE", typeof(String));
            ret.Rows.Add("AAAA");
            ret.Rows.Add("BBBB");
            ret.Rows.Add("CCCC");
            ret.Rows.Add("DDDD");
            return ret;
        }

    }
}
