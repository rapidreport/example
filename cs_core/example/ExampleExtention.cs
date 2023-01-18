using System;
using System.IO;
using System.Data;

using iTextSharp.text.pdf;
using jp.co.systembase.NPOI.XSSF.UserModel;

using jp.co.systembase.json;
using jp.co.systembase.report;
using jp.co.systembase.report.component;
using jp.co.systembase.report.data;
using jp.co.systembase.report.renderer.pdf;
using jp.co.systembase.report.renderer.xlsx;

// System.Drawing.Regionと名前衝突するのでエイリアスを定義しておきます
using Region = jp.co.systembase.report.component.Region;
using SkiaSharp;

// 機能サンプル カスタム書式/要素
namespace example
{
    class ExampleExtention
    {
        public static void Run()
        {
            // 郵便番号フォーマッタが設定されたSettingオブジェクトを用意します
            ReportSetting setting = new ReportSetting();
            setting.TextFormatterMap.Add("yubin", new YubinTextFormatter());

            Report report = new Report(Json.Read("report/example_extention.rrpt"), setting);
            report.Fill(new ReportDataSource(getDataTable()));
            ReportPages pages = report.GetPages();

            // PDF出力
            using (FileStream fs = new FileStream("output/example_extention.pdf", FileMode.Create))
            {
                // チェックボックスレンダラが設定されたSettingオブジェクトを用意します
                PdfRendererSetting pdfSetting = new PdfRendererSetting();
                pdfSetting.ElementRendererMap.Add("checkbox", new PdfCheckBoxRenderer());

                PdfRenderer renderer = new PdfRenderer(fs, pdfSetting);
                renderer.Setting.ReplaceBackslashToYen = true;
                pages.Render(renderer);
            }

            // Excel(XLSX)出力
            using (FileStream fs = new FileStream("output/example_extention.xlsx", FileMode.Create))
            {
                // チェックボックスレンダラが設定されたSettingオブジェクトを用意します
                XlsxRendererSetting xlsxSetting = new XlsxRendererSetting();
                xlsxSetting.ElementRendererMap.Add("checkbox", new XlsxCheckBoxRenderer());

                XSSFWorkbook workbook = new XSSFWorkbook();
                XlsxRenderer renderer = new XlsxRenderer(workbook, xlsxSetting);
                renderer.NewSheet("example_extention");
                pages.Render(renderer);
                workbook.Write(fs);
            }
        }

        private static DataTable getDataTable()
        {
            DataTable ret = new DataTable();
            ret.Columns.Add("check", typeof(bool));
            ret.Rows.Add(true);
            ret.Rows.Add(false);
            ret.Rows.Add(true);
            ret.Rows.Add(false);
            return ret;
        }

        // 郵便番号フォーマッタ
        public class YubinTextFormatter : 
            jp.co.systembase.report.textformatter.ITextFormatter
        {
            public string Format(object v, ElementDesign design)
            {
                if (v == null)
                {
                    return null;
                }
                String _v = v.ToString();
                if (_v.Length > 3)
                {
                    return _v.Substring(0, 3) + "-" + _v.Substring(3);
                }
                else
                {
                    return _v;
                }
            }
        }

        // チェックボックスを描く要素レンダラ(PDF)
        public class PdfCheckBoxRenderer :
            jp.co.systembase.report.renderer.pdf.elementrenderer.IElementRenderer
        {
            public void Render(
                PdfRenderer renderer, 
                ReportDesign reportDesign, 
                Region region, 
                ElementDesign design, 
                object data)
            {
                Region r = region.ToPointScale(reportDesign);
                PdfContentByte cb = renderer.Writer.DirectContent;
                Single x = r.Left + r.GetWidth() / 2;
                Single y = r.Top + r.GetHeight() / 2;
                Single w = 12;
                cb.SaveState();
                cb.Rectangle(renderer.Trans.X(x - w / 2), renderer.Trans.Y(y - w / 2), w, -w);
                cb.Stroke();
                if ((bool)data)
                {
                    cb.SetColorFill(PdfRenderUtil.GetColor("steelblue"));
                    cb.MoveTo(renderer.Trans.X(x - w / 2), renderer.Trans.Y(y - w / 4));
                    cb.LineTo(renderer.Trans.X(x - w / 4), renderer.Trans.Y(y + w / 2));
                    cb.LineTo(renderer.Trans.X(x + w / 2), renderer.Trans.Y(y - w / 2));
                    cb.LineTo(renderer.Trans.X(x - w / 4), renderer.Trans.Y(y));
                    cb.Fill();
                }
                cb.RestoreState();
            }
        }

        // チェックボックスを描く要素レンダラ(XLSX)
        public class XlsxCheckBoxRenderer :
            jp.co.systembase.report.renderer.xlsx.elementrenderer.IElementRenderer
        {
            public void Collect(
                XlsxRenderer renderer,
                ReportDesign reportDesign,
                Region region,
                ElementDesign design,
                object data)
            {
                Region r = region.ToPointScale(reportDesign);
                jp.co.systembase.report.renderer.xlsx.component.Shape shape =
                    new jp.co.systembase.report.renderer.xlsx.component.Shape();
                shape.Region = r;
                shape.Renderer = new CheckBoxShapeRenderer(data);
                renderer.CurrentPage.Shapes.Add(shape);
            }

            private static int checkedImageIndex = 0;
            private static int noCheckedImageIndex = 0;

            private static void createImage(jp.co.systembase.report.renderer.xlsx.component.Page page)
            {
                if (checkedImageIndex == 0)
                {
                    var bmp = new SKBitmap(40, 40);
                    var canvas = new SKCanvas(bmp);
                    canvas.DrawRect(10, 10, 20, 20, new SKPaint() { Style = SKPaintStyle.Stroke, Color = SKColors.Black });
                    var path = new SKPath();
                    path.MoveTo(10, 15);
                    path.LineTo(15, 30);
                    path.LineTo(30, 10);
                    path.LineTo(15, 20);
                    path.Close();
                    canvas.DrawPath(path, new SKPaint() { Style = SKPaintStyle.Fill, Color = SKColors.SteelBlue });
                    checkedImageIndex = page.Renderer.GetImageIndex(SKImage.FromBitmap(bmp).Encode(SKEncodedImageFormat.Png, 100).ToArray());
                }
                if (noCheckedImageIndex == 0)
                {
                    var bmp = new SKBitmap(40, 40);
                    var canvas = new SKCanvas(bmp);
                    var path = new SKPath();
                    canvas.DrawRect(10, 10, 20, 20, new SKPaint() { Style = SKPaintStyle.Stroke, Color = SKColors.Black });
                    SKImage.FromBitmap(bmp).Encode(SKEncodedImageFormat.Png, 100).ToArray();
                    noCheckedImageIndex = page.Renderer.GetImageIndex(SKImage.FromBitmap(bmp).Encode(SKEncodedImageFormat.Png, 100).ToArray());
                }
            }

            public class CheckBoxShapeRenderer :
                jp.co.systembase.report.renderer.xlsx.elementrenderer.IShapeRenderer
            {
                public Object data;
                public CheckBoxShapeRenderer(Object data)
                {
                    this.data = data;
                }
                public void Render(
                    jp.co.systembase.report.renderer.xlsx.component.Page page,
                    jp.co.systembase.report.renderer.xlsx.component.Shape shape)
                {
                    createImage(page);
                    page.Renderer.Sheet.CreateDrawingPatriarch()
                        .CreatePicture(shape.GetXSSFClientAnchor(page.TopRow), 
                        (bool)this.data ? checkedImageIndex : noCheckedImageIndex);
                }
            }
        }

    }
}
