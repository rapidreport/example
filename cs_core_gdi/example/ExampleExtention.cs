using System;
using System.IO;
using System.Data;
using System.Drawing;

using jp.co.systembase.json;
using jp.co.systembase.report;
using jp.co.systembase.report.component;
using jp.co.systembase.report.data;
using jp.co.systembase.report.renderer.gdi;

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

    }
}
