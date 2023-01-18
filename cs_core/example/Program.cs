using System;

namespace example
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("実行中： チュートリアル１");
            Example1.Run();
            Console.WriteLine("実行中： チュートリアル２");
            Example2.Run();
            Console.WriteLine("実行中： 機能サンプル - データの部分割り当て");
            ExampleDataProvider.Run();
            Console.WriteLine("実行中： 機能サンプル - 絶対座標による配置");
            ExampleLocate.Run();
            Console.WriteLine("実行中： 機能サンプル - コンテントのサイズ変更");
            ExampleRegion.Run();
            Console.WriteLine("実行中： 機能サンプル - ページ挿入");
            ExamplePage.Run();
            Console.WriteLine("実行中： 機能サンプル - 動的要素");
            ExampleRender.Run();
            Console.WriteLine("実行中： 機能サンプル - 動的要素（デザイナのみ）");
            ExampleCustomize.Run();
            Console.WriteLine("実行中： 機能サンプル - 動的画像の表示");
            ExampleImage.Run();
            Console.WriteLine("実行中： 機能サンプル - CSVデータソース");
            Example1Csv.Run();
            Console.WriteLine("実行中： 機能サンプル - カスタム書式/要素");
            ExampleExtention.Run();
            Console.WriteLine("実行中： 機能サンプル - コンテントの差し込み");
            ExampleMergeContent.Run();
            Console.WriteLine("実行中： 機能サンプル - 段組み帳票");
            ExampleSubPage.Run();
            Console.WriteLine("実行中： 機能サンプル - クロス集計表");
            ExampleCrosstab.Run();
            Console.WriteLine("実行中： 特徴と機能一覧");
            Feature.Run();
            Console.WriteLine("-----終了-----");
            Console.WriteLine("出力先： " + System.IO.Path.Combine(Environment.CurrentDirectory, "output"));
        }
    }
}
