using System;
using System.IO;
using System.Data;

using jp.co.systembase.NPOI.XSSF.UserModel;

using jp.co.systembase.json;
using jp.co.systembase.report;
using jp.co.systembase.report.data;
using jp.co.systembase.report.renderer.pdf;
using jp.co.systembase.report.renderer.xlsx;

namespace example
{
    class ExampleDataProvider
    {
        public static void Run()
        {
            Report report = new Report(Json.Read("report/example_dataprovider.rrpt"));

            // "group_shohin"という識別子を持ったグループには、
            // getShoninDataTableから得られるデータを割り当てます
            GroupDataProvider dataProvider = new GroupDataProvider();
            dataProvider.GroupDataMap.Add("group_shonin", new ReportDataSource(getShoninDataTable()));

            // 第2引数にdataProviderを渡します
            report.Fill(new ReportDataSource(getDataTable()), dataProvider);

            ReportPages pages = report.GetPages();

            // PDF出力
            using (FileStream fs = new FileStream("output/example_dataprovider.pdf", FileMode.Create))
            {
                PdfRenderer renderer = new PdfRenderer(fs);
                renderer.Setting.ReplaceBackslashToYen = true;
                pages.Render(renderer);
            }

            // Excel(XLSX)出力
            using (FileStream fs = new FileStream("output/example_dataprovider.xlsx", FileMode.Create))
            {
                XSSFWorkbook workbook = new XSSFWorkbook();
                XlsxRenderer renderer = new XlsxRenderer(workbook);
                renderer.NewSheet("example_dataprovider");
                pages.Render(renderer);
                workbook.Write(fs);
            }
        }

        // 商品リスト
        private static DataTable getDataTable(){
            DataTable ret = new DataTable();
            ret.Columns.Add("HAT_ID", typeof(int));
            ret.Columns.Add("TOKUI_NM", typeof(String));
            ret.Columns.Add("TOKUI_TANTO_NM", typeof(String));
            ret.Columns.Add("HIN_NM", typeof(String));
            ret.Columns.Add("HIN_CD", typeof(String));
            ret.Columns.Add("SURYO", typeof(Decimal));
            ret.Columns.Add("TANKA", typeof(Decimal));
            ret.Columns.Add("SHUKKABI", typeof(DateTime));
            ret.Rows.Add(1, "○○精密", "担当太郎", "パイロットパンチ", "AAA-BBB-CCC-DDD-1000",
                1, 600, DateTime.ParseExact("2011/06/07", "yyyy/MM/dd", null));
            ret.Rows.Add(1, "○○精密", "担当太郎", "ガイドプレート", "AIUEO-999.999", 
                5, 1050, DateTime.ParseExact("2011/06/15", "yyyy/MM/dd", null));
            ret.Rows.Add(1, "○○精密", "担当太郎", "イジェクタピン", "1234-5678-9999", 
                1, 7340, DateTime.ParseExact("2011/06/13", "yyyy/MM/dd", null));
            ret.Rows.Add(2, "△△機械", "担当花子", "ブロックダイ", "9999-8888-7777", 
                10, 1600, DateTime.ParseExact("2011/06/10", "yyyy/MM/dd", null));
            ret.Rows.Add(2, "△△機械", "担当花子", "ブランジャ", "ZZZZZ-YYYYY-XXXXX", 
                5, 800, DateTime.ParseExact("2011/06/10", "yyyy/MM/dd", null));
            return ret;
        }
        
        // 承認者リスト
        private static DataTable getShoninDataTable(){
            DataTable ret = new DataTable();
            ret.Columns.Add("HAT_ID", typeof(int));
            ret.Columns.Add("SHONIN_NM", typeof(String));
            ret.Rows.Add(1, "承認一郎");
            ret.Rows.Add(1, "承認二郎");
            ret.Rows.Add(1, "承認三郎");
            ret.Rows.Add(1, "承認四郎");
            ret.Rows.Add(2, "承認花子");
            return ret;
        }

    }
}
