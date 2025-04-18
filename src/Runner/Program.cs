﻿using System.Text;
using TextTabulator;
using TextTabulator.Adapters;
using TextTabulator.Adapters.Json;
using TextTabulator.Adapters.Xml;

internal class Program
{
    private static void Main(string[] args)
    {
        //var headers = new string[]
        //{
        //   "Header1",
        //   "Header2",
        //   "Header3"
        //};

        //var values = new string[][]
        //{
        //   new string[] { "value1A", "value2A", "value3A", },
        //   new string[] { "value1B", "value2B", "value3B", },
        //   new string[] { "value1C", "value2C", "value3C", },
        //};

        //var headers = new string[]
        //{
        //    "Header",
        //    "Header2",
        //    "ZZZHeader3"
        //};

        //var values = new string[][]
        //{
        //    new string[] { "Value1A", "Value2A", "Value3A" },
        //    new string[] { "Value1B", "YYYValue2B", "Value3B" },
        //    new string[] { "XXXValue1C", "Value2C", "Value3C" },
        //};

        //        var csvData =
        //        @"Name,Weight (tons),Diet,Extinction
        //Tyrannosaurus Rex,6.7,Carnivore,66 mya
        //Triceratops,8,Herbivore,66 mya
        //Apatosaurus,33,Herbivore,147 mya
        //Archaeopteryx,0.001,Omnivore,147 mya
        //Anklyosaurus,4.8,Herbivore,66 mya
        //Stegosaurus,3.8,Herbivore,147 mya
        //Hadrosaurus,3,Herbivore,66 mya
        //";

        //        using var textReader = new StringReader(csvData);
        //        using var csvReader = new CsvReader(textReader, CultureInfo.InvariantCulture);
        //        var csvAdapter = new CsvHelperTabulatorAdapter(csvReader, true);

        //var data = new Dinosaur[]
        //{
        //    new Dinosaur("Tyrannosaurus Rex", 6.7, Diet.Carnivore, 66),
        //    new Dinosaur("Triceratops", 8, Diet.Herbivore, 66),
        //    new Dinosaur("Apatosaurus", 33, Diet.Herbivore, 147),
        //    new Dinosaur("Archaeopteryx", 0.001, Diet.Omnivore, 147),
        //    new Dinosaur("Anklyosaurus", 4.8, Diet.Herbivore, 66),
        //    new Dinosaur("Stegosaurus", 3.8, Diet.Herbivore, 147),
        //    new Dinosaur("Hadrosaurus", 3, Diet.Herbivore, 66),
        //};

        using var stream = new FileStream(@"D:\Justin\Projects\Data\books.xml", FileMode.Open, FileAccess.Read);

        var options = new XmlTabulatorAdapterOptions(new SnakeNameTransform(), new System.Xml.XmlReaderSettings
        {
            ConformanceLevel = System.Xml.ConformanceLevel.Fragment,
        });

        var adapter = new XmlTabulatorAdapter(stream, options);
        //var adapter = new JsonTabulatorAdapter(stream);

        //var individualAlignments = new CellAlignment[][]
        //{
        //    new CellAlignment[] { CellAlignment.CenterLeftBias, CellAlignment.CenterRightBias, CellAlignment.Left },
        //    new CellAlignment[] { CellAlignment.CenterRightBias, CellAlignment.CenterLeftBias, CellAlignment.Right },
        //    new CellAlignment[] { CellAlignment.Right, CellAlignment.CenterRightBias, CellAlignment.Left },
        //    new CellAlignment[] { CellAlignment.CenterLeftBias, CellAlignment.Left, CellAlignment.Right },
        //};

        //var valueAlignments = new CellAlignment[][]
        //{
        //    new CellAlignment[] { CellAlignment.CenterLeftBias, CellAlignment.CenterRightBias, CellAlignment.Left },
        //    new CellAlignment[] { CellAlignment.CenterRightBias, CellAlignment.CenterLeftBias, CellAlignment.Right },
        //    new CellAlignment[] { CellAlignment.Right, CellAlignment.CenterRightBias, CellAlignment.Left },
        //};

        //var options = new TabulatorOptions();
        //var options = new TabulatorOptions
        //{
        //    //CellAlignment = new UniformHeaderUniformColumnAlignmentProvider(new CellAlignment[] { CellAlignment.Left, CellAlignment.Right }, CellAlignment.CenterLeftBias),
        //    //CellAlignment = new IndividualCellAlignmentProvider(individualAlignments),
        //    //CellAlignment = new UniformColumnAlignmentProvider(new CellAlignment[] { CellAlignment.Left, CellAlignment.CenterRightBias, CellAlignment.Right }),
        //    //CellAlignment = new UniformValueAlignmentProvider(new CellAlignment[] { CellAlignment.Left, CellAlignment.CenterRightBias, CellAlignment.Right }, CellAlignment.CenterLeftBias),
        //    //CellAlignment = new UniformHeaderUniformValueAlignmentProvider(CellAlignment.Left, CellAlignment.Right),
        //    //CellAlignment = new UniformHeaderAlignmentProvider(valueAlignments, CellAlignment.CenterLeftBias),
        //    CellAlignment = new UniformColumnAlignmentProvider(new CellAlignment[] { CellAlignment.Left, CellAlignment.Right }),
        //    //    Styling = new UnicodeTableStyling(),
        //    //    //ValueRowSeparator = '─',
        //    //    //ValueLeftEdgeJoint = '╟',
        //    //    //ValueMiddleJoint = '┼',
        //    //    //ValueRightEdgeJoint = '╢',
        //    //    //HeaderRowSeparator = '═',
        //    //    //HeaderLeftEdgeJoint = '╠',
        //    //    //HeaderMiddleJoint = '╪',
        //    //    //HeaderRightEdgeJoint = '╣',
        //    //    //LeftEdge = '║',
        //    //    //RightEdge = '║',
        //    //    //TopEdge = '═',
        //    //    //BottomEdge = '═',
        //    //    //ColumnSeparator = '│',
        //    //    //TopLeftCorner = '╔',
        //    //    //TopRightCorner = '╗',
        //    //    //BottomLeftCorner = '╚',
        //    //    //BottomRightCorner = '╝',
        //    //    //TopEdgeJoint = '╤',
        //    //    //BottomEdgeJoint = '╧',
        //};

        var tabulator = new Tabulator();
        var table = tabulator.Tabulate(adapter);

        Console.WriteLine(table);
    }
}