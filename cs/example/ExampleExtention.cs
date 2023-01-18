using System;
using System.IO;
using System.Data;
using System.Drawing;

using iTextSharp.text.pdf;
using jp.co.systembase.NPOI.SS.UserModel;
using jp.co.systembase.NPOI.HSSF.UserModel;
using jp.co.systembase.NPOI.XSSF.UserModel;

using jp.co.systembase.json;
using jp.co.systembase.report;
using jp.co.systembase.report.component;
using jp.co.systembase.report.data;
using jp.co.systembase.report.renderer.gdi;
using jp.co.systembase.report.renderer.pdf;
using jp.co.systembase.report.renderer.xls;
using jp.co.systembase.report.renderer.xlsx;

// System.Drawing.Regionと名前衝突するのでエイリアスを定義しておきます
using Region = jp.co.systembase.report.component.Region;

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

            // XLS出力
            using (FileStream fs = new FileStream("output/example_extention.xls", FileMode.Create))
            {
                // チェックボックスレンダラが設定されたSettingオブジェクトを用意します
                XlsRendererSetting xlsSetting = new XlsRendererSetting();
                xlsSetting.ElementRendererMap.Add("checkbox", new XlsCheckBoxRenderer());

                HSSFWorkbook workbook = new HSSFWorkbook();
                XlsRenderer renderer = new XlsRenderer(workbook, xlsSetting);
                renderer.NewSheet("example_extention");
                pages.Render(renderer);
                workbook.Write(fs);
            }

            // XLSX出力
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

            // プレビュー
            {
                // チェックボックスレンダラが設定されたSettingオブジェクトを用意します
                GdiRendererSetting gdiSetting = new GdiRendererSetting();
                gdiSetting.ElementRendererMap.Add("checkbox", new GdiCheckBoxRenderer());

                FmPrintPreview preview = new FmPrintPreview(new Printer(pages, gdiSetting));
                preview.StartUpZoomFit = true;
                preview.ShowDialog();
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

        // チェックボックスを描く要素レンダラ(プレビュー・直接印刷)
        public class GdiCheckBoxRenderer :
            jp.co.systembase.report.renderer.gdi.elementrenderer.IElementRenderer
        {
            public void Render(
                RenderingEnv env,
                ReportDesign reportDesign,
                Region region,
                ElementDesign design,
                object data)
            {
                Region r = region.ToPointScale(reportDesign);
                Single x = r.Left + r.GetWidth() / 2;
                Single y = r.Top + r.GetHeight() / 2;
                Single w = 12;
                env.Graphics.DrawRectangle(Pens.Black, x - w / 2, y - w / 2, w, w);
                if ((bool)data)
                {
                    Point[] p = {
                      new Point((int)(x - w / 2), (int)(y - w / 4)),
                      new Point((int)(x - w / 4), (int)(y + w / 2)),
                      new Point((int)(x + w / 2), (int)(y - w / 2)),
                      new Point((int)(x - w / 4), (int)(y))};
                    env.Graphics.FillPolygon(Brushes.SteelBlue, p);
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

        // チェックボックスを描く要素レンダラ(XLS)
        public class XlsCheckBoxRenderer :
            jp.co.systembase.report.renderer.xls.elementrenderer.IElementRenderer
        {
            public void Collect(
                XlsRenderer renderer,
                ReportDesign reportDesign,
                Region region,
                ElementDesign design,
                object data)
            {
                Region r = region.ToPointScale(reportDesign);
                jp.co.systembase.report.renderer.xls.component.Shape shape =
                    new jp.co.systembase.report.renderer.xls.component.Shape();
                shape.Region = r;
                shape.Renderer = new CheckBoxShapeRenderer(data);
                renderer.CurrentPage.Shapes.Add(shape);
            }

            private static Image checkedImage = null;
            private static Image noCheckedImage = null;

            private static void createImage()
            {
                if (checkedImage == null)
                {
                    checkedImage = new Bitmap(40, 40);
                    Graphics g = Graphics.FromImage(checkedImage);
                    g.DrawRectangle(Pens.Black, 10, 10, 20, 20);
                    Point[] p = {
                      new Point(10, 15),
                      new Point(15, 30),
                      new Point(30, 10),
                      new Point(15, 20)};
                    g.FillPolygon(Brushes.SteelBlue, p);
                }
                if (noCheckedImage == null)
                {
                    noCheckedImage = new Bitmap(40, 40);
                    Graphics g = Graphics.FromImage(noCheckedImage);
                    g.DrawRectangle(Pens.Black, 10, 10, 20, 20);
                }
            }

            public class CheckBoxShapeRenderer :
                jp.co.systembase.report.renderer.xls.elementrenderer.IShapeRenderer
            {
                public Object data;
                public CheckBoxShapeRenderer(Object data)
                {
                    this.data = data;
                }
                public void Render(
                    jp.co.systembase.report.renderer.xls.component.Page page,
                    jp.co.systembase.report.renderer.xls.component.Shape shape)
                {
                    createImage();
                    int index;
                    if ((bool)this.data)
                    {
                        index = page.Renderer.GetImageIndex(checkedImage);
                    }
                    else
                    {
                        index = page.Renderer.GetImageIndex(noCheckedImage);
                    }
                    if (index > 0)
                    {
                        HSSFPatriarch p = (HSSFPatriarch)page.Renderer.Sheet.DrawingPatriarch;
                        p.CreatePicture(shape.GetHSSFClientAnchor(page.TopRow), index);
                    }
                }
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

            private static Image checkedImage = null;
            private static Image noCheckedImage = null;

            private static void createImage()
            {
                if (checkedImage == null)
                {
                    checkedImage = new Bitmap(40, 40);
                    Graphics g = Graphics.FromImage(checkedImage);
                    g.DrawRectangle(Pens.Black, 10, 10, 20, 20);
                    Point[] p = {
                      new Point(10, 15),
                      new Point(15, 30),
                      new Point(30, 10),
                      new Point(15, 20)};
                    g.FillPolygon(Brushes.SteelBlue, p);
                }
                if (noCheckedImage == null)
                {
                    noCheckedImage = new Bitmap(40, 40);
                    Graphics g = Graphics.FromImage(noCheckedImage);
                    g.DrawRectangle(Pens.Black, 10, 10, 20, 20);
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
                    createImage();
                    int index;
                    if ((bool)this.data)
                    {
                        index = page.Renderer.GetImageIndex(checkedImage);
                    }
                    else
                    {
                        index = page.Renderer.GetImageIndex(noCheckedImage);
                    }
                    if (index >= 0)
                    {
                        IDrawing p = page.Renderer.Sheet.CreateDrawingPatriarch();
                        p.CreatePicture(shape.GetXSSFClientAnchor(page.TopRow), index);
                    }
                }
            }
        }

    }
}
