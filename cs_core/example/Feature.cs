using System;
using System.IO;
using System.Data;

using jp.co.systembase.NPOI.XSSF.UserModel;

using jp.co.systembase.json;
using jp.co.systembase.report;
using jp.co.systembase.report.data;
using jp.co.systembase.report.renderer.pdf;
using jp.co.systembase.report.renderer.xlsx;

// 「特徴と機能一覧」に掲載したサンプル
namespace example
{
    class Feature
    {

        public static void Run()
        {

            Report report = new Report(Json.Read("report/feature.rrpt"));

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
            using (FileStream fs = new FileStream("output/feature.pdf", FileMode.Create))
            {
                PdfRenderer renderer = new PdfRenderer(fs);
                renderer.Setting.ReplaceBackslashToYen = true;
                pages.Render(renderer);
            }

            // Excel(XLSX)出力
            using (FileStream fs = new FileStream("output/feature.xlsx", FileMode.Create))
            {
                XSSFWorkbook workbook = new XSSFWorkbook();
                XlsxRenderer renderer = new XlsxRenderer(workbook);
                renderer.NewSheet("feature");
                pages.Render(renderer);
                workbook.Write(fs);
            }
        }

        private static DataTable getDataTable1()
        {
            DataTable ret = new DataTable();
            ret.Columns.Add("KUBUN1", typeof(String)); 
            ret.Columns.Add("KUBUN2", typeof(String));
            ret.Columns.Add("ZAISHITSU", typeof(String));
            ret.Columns.Add("TANI", typeof(String));
            ret.Columns.Add("TANKA", typeof(Decimal));
            ret.Columns.Add("SURYO", typeof(Decimal));
            ret.Rows.Add("内装", "床", "フローリング", "㎡", 7250, 20);
            ret.Rows.Add("内装", "床", "畳", "枚", 4500, 12);
            ret.Rows.Add("内装", "壁", "壁紙", "㎡", 1000, 30);
            ret.Rows.Add("外装", "屋根", "瓦", "枚", 13000, 40);
            ret.Rows.Add("外装", "屋根", "トタン", "㎡", 3920, 60);
            ret.Rows.Add("外装", "屋根", "化粧スレート", "㎡", 5000, 45);
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
            ret.Columns.Add("SHOHIN_CD", typeof(String));
            ret.Columns.Add("SHOHIN_NM", typeof(String));
            ret.Columns.Add("SURYO", typeof(Decimal));
            ret.Columns.Add("TANI", typeof(String));
            ret.Columns.Add("TANKA", typeof(Decimal));
            ret.Rows.Add("SH-A0011", "冷凍コロッケ", 25, "袋", 35);
            ret.Rows.Add("SH-A0012", "冷凍ピザ", 8, "袋", 410);
            ret.Rows.Add("SH-B1005", "カップラーメン醤油味", 40, "個", 90);
            return ret;
        }

    }
}
