using System;
using System.IO;
using System.Data;

using jp.co.systembase.NPOI.HSSF.UserModel;
using jp.co.systembase.NPOI.XSSF.UserModel;

using jp.co.systembase.json;
using jp.co.systembase.report;
using jp.co.systembase.report.data;
using jp.co.systembase.report.renderer.gdi;
using jp.co.systembase.report.renderer.pdf;
using jp.co.systembase.report.renderer.xls;
using jp.co.systembase.report.renderer.xlsx;

// 機能サンプル クロス集計表
namespace example
{
    class ExampleCrosstab
    {

        public static void Run()
        {
            Report report = new Report(Json.Read("report/example_crosstab.rrpt"));

            // 横方向の列データを設定します。
            report.AddCrosstabCaptionDataSource("crosstab_example", new ReportDataSource(getCaptionDataTable()));

            report.Fill(new ReportDataSource(getDataTable()));
            ReportPages pages = report.GetPages();

            // PDF出力
            using (FileStream fs = new FileStream("output/example_crosstab.pdf", FileMode.Create))
            {
                PdfRenderer renderer = new PdfRenderer(fs);
                renderer.Setting.ReplaceBackslashToYen = true;
                pages.Render(renderer);
            }

            // XLS出力
            using (FileStream fs = new FileStream("output/example_crosstab.xls", FileMode.Create))
            {
                HSSFWorkbook workbook = new HSSFWorkbook();
                XlsRenderer renderer = new XlsRenderer(workbook);
                renderer.NewSheet("example_crosstab");
                pages.Render(renderer);
                workbook.Write(fs);
            }

            // XLSX出力
            using (FileStream fs = new FileStream("output/example_crosstab.xlsx", FileMode.Create))
            {
                XSSFWorkbook workbook = new XSSFWorkbook();
                XlsxRenderer renderer = new XlsxRenderer(workbook);
                renderer.NewSheet("example_crosstab");
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

        private static DataTable getCaptionDataTable()
        {
            DataTable ret = new DataTable();
            ret.Columns.Add("period_cd", typeof(int));
            ret.Columns.Add("period_nm", typeof(String));
            ret.Rows.Add(1, "2010年上期");
            ret.Rows.Add(2, "2010年下期");
            ret.Rows.Add(3, "2011年上期");
            ret.Rows.Add(4, "2011年下期");
            ret.Rows.Add(5, "2012年上期");
            ret.Rows.Add(6, "2012年下期");
            ret.Rows.Add(7, "2013年上期");
            ret.Rows.Add(8, "2013年下期");
            ret.Rows.Add(9, "2014年上期");
            ret.Rows.Add(10, "2014年下期");
            ret.Rows.Add(11, "2015年上期");
            ret.Rows.Add(12, "2015年下期");
            ret.Rows.Add(13, "2016年上期");
            ret.Rows.Add(14, "2016年下期");
            return ret;
        }

        private static DataTable getDataTable()
        {
            DataTable ret = new DataTable();
            String[] branchNms = new string[]
              {"北上本社", "東京支社", "盛岡営業所", "秋田営業所",
               "仙台営業所", "山形営業所", "福島営業所"};
            ret.Columns.Add("branch_cd", typeof(int));
            ret.Columns.Add("branch_nm", typeof(String));
            ret.Columns.Add("period_cd", typeof(int));
            ret.Columns.Add("amount", typeof(decimal));
            for (int i = 0; i < 14; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    ret.Rows.Add(j + 1, branchNms[j], i + 1, 10000 + i * 100 + j * 10);
                }
            }
            return ret;
        }


        //// 横の見出し列値も含めたデータ。
        //// こちらを利用する場合は、CaptionDataを設定する必要はありません。
        //private static DataTable getDataTable(){
        //    DataTable ret = new DataTable();
        //    String[] branchNms = 
        //            {"北上本社", "東京支社", "盛岡営業所", "秋田営業所",
        //             "仙台営業所", "山形営業所", "福島営業所"};
        //    String[] periodNms = 
        //        {"2010年上期", "2010年下期", "2011年上期", "2011年下期",
        //          "2012年上期", "2012年下期", "2013年上期", "2013年下期",
        //          "2014年上期", "2014年下期", "2015年上期", "2015年下期",
        //          "2016年上期", "2016年下期"};
        //    ret.Columns.Add("branch_cd", typeof(int));
        //    ret.Columns.Add("branch_nm", typeof(String));
        //    ret.Columns.Add("period_cd", typeof(int));
        //    ret.Columns.Add("period_nm", typeof(String));
        //    ret.Columns.Add("amount", typeof(decimal));
        //    for (int i = 0;i < 14;i++){
        //        for(int j = 0;j < 7;j++){
        //            ret.Rows.Add(j + 1, branchNms[j], i + 1, periodNms[i], 10000 + i * 100 + j * 10);
        //        }
        //    }
        //    return ret;
        //}
        
    }
}
