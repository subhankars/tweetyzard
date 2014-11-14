using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using tweetyzard.utility;

namespace twetyzard.utility
{
    public static class OpenXMLHelper
    {
        /// <summary>
        /// Exports All the tabs sheet by sheet
        /// </summary>
        /// <param name="ds">Data set containing the grid data</param>
        /// <param name="destination">Export destination</param>
        public static void ExportDataSetToExcel(DataSet ds, string destination)
        {
            if (!string.IsNullOrEmpty(destination))
            {
                using (var spreadsheetDocument = SpreadsheetDocument.Create(destination, SpreadsheetDocumentType.Workbook))
                {
                    spreadsheetDocument.AddWorkbookPart();
                    spreadsheetDocument.WorkbookPart.Workbook = new Workbook();
                    spreadsheetDocument.WorkbookPart.Workbook.Sheets = new Sheets();
                    var stylesPart = spreadsheetDocument.WorkbookPart.AddNewPart<WorkbookStylesPart>();
                    stylesPart.Stylesheet = new Stylesheet();

                    // blank font list
                    stylesPart.Stylesheet.Fonts = new Fonts();
                    stylesPart.Stylesheet.Fonts.Count = 1;
                    stylesPart.Stylesheet.Fonts.AppendChild(new Font());

                    // create eserved fills
                    stylesPart.Stylesheet.Fills = new Fills();
                    stylesPart.Stylesheet.Fills.AppendChild(new Fill { PatternFill = new PatternFill { PatternType = PatternValues.None } }); // required, reserved by Excel
                    stylesPart.Stylesheet.Fills.AppendChild(new Fill { PatternFill = new PatternFill { PatternType = PatternValues.Gray125 } }); // required, reserved by Excel
                    stylesPart.Stylesheet.Fills.Count = 2;

                    // blank border list
                    stylesPart.Stylesheet.Borders = new Borders();
                    stylesPart.Stylesheet.Borders.AppendChild(new Border());
                    stylesPart.Stylesheet.Borders.Count = 1;

                    // blank cell format list
                    stylesPart.Stylesheet.CellStyleFormats = new CellStyleFormats();
                    stylesPart.Stylesheet.CellStyleFormats.AppendChild(new CellFormat());
                    stylesPart.Stylesheet.CellStyleFormats.Count = 1;

                    // cell format list
                    stylesPart.Stylesheet.CellFormats = new CellFormats();
                    // empty one for index 0, seems to be required
                    stylesPart.Stylesheet.CellFormats.AppendChild(new CellFormat());
                    stylesPart.Stylesheet.CellFormats.Count = 1;

                    var headerFontIndex = CreateFont(stylesPart.Stylesheet, PrintFormatting.ExcelHeaderFont, Convert.ToInt32(PrintFormatting.ExcelCellFontSize), true, System.Drawing.Color.White);
                    var headerFillIndex = CreateFill(stylesPart.Stylesheet, PrintFormatting.ExcelColumnHeaderFillColor);
                    var cellFontIndex = CreateFont(stylesPart.Stylesheet, PrintFormatting.ExcelCellFont, Convert.ToInt32(PrintFormatting.ExcelCellFontSize), false, System.Drawing.Color.Black);
                    var cellFillIndex = CreateFill(stylesPart.Stylesheet, PrintFormatting.ExcelCellFillColor);
                    var headerBorderIndex = CreateBorder(stylesPart.Stylesheet, BorderStyleValues.Thin);
                    var cellBorderIndex = CreateBorder(stylesPart.Stylesheet, BorderStyleValues.Dotted);
                    var headerCellFormatIndex = CreateCellFormat(stylesPart.Stylesheet, headerFontIndex, headerFillIndex, headerBorderIndex);
                    var cellCellFormatIndex = CreateCellFormat(stylesPart.Stylesheet, cellFontIndex, cellFillIndex, cellBorderIndex);

                    stylesPart.Stylesheet.Save();

                    using (ds)
                    {
                        var tableNames = ds.Tables[ds.Tables.Count - 1];
                        for (var i = 0; i < ds.Tables.Count; i++)
                        {
                            var sheetName = Convert.ToString(tableNames.Rows[i][0]);
                            AddSheet(ds.Tables[i], spreadsheetDocument, sheetName, headerCellFormatIndex, cellCellFormatIndex);
                        }
                    }
                }
            }
        }


        /// <summary>
        /// Adds sheet to workbook
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="spreadsheetDocument"></param>
        /// <param name="sheetName"></param>
        /// <param name="headerCellFormatIndex"></param>
        /// <param name="cellCellFormatIndex"></param>
        private static void AddSheet(DataTable dt, SpreadsheetDocument spreadsheetDocument, string sheetName, uint headerCellFormatIndex, uint cellCellFormatIndex)
        {
            if (dt.Rows.Count < 500)
            {
                for (var intRowPos = dt.Rows.Count; intRowPos < 500; intRowPos++)
                {
                    var blankDataRow = dt.NewRow();
                    dt.Rows.InsertAt(blankDataRow, intRowPos);
                }
            }

            // Add a blank WorksheetPart.
            var worksheetPart = spreadsheetDocument.WorkbookPart.AddNewPart<WorksheetPart>();
            var worksheet = new Worksheet();
            var sheetData = new SheetData();

            const uint headerRowIndex = 1;
            var headerRow = new Row { RowIndex = headerRowIndex };
            sheetData.AppendChild(headerRow);

            var columns = new List<ExcelColumn>();

            using (var headerFont = new System.Drawing.Font(PrintFormatting.ExcelHeaderFont, Convert.ToInt32(PrintFormatting.ExcelCellFontSize), System.Drawing.FontStyle.Bold))
            {
                for (var i = 0; i < dt.Columns.Count; i++)
                {
                    var excelColumnName = GetExcelColumnName(i);
                    var columnName = dt.Columns[i].ColumnName;
                    AppendCell(excelColumnName, columnName, headerRow, headerRowIndex, headerCellFormatIndex);
                    columns.Add(new ExcelColumn
                    {
                        ColumnName = columnName,
                        ExcelColumnName = excelColumnName,
                        Index = (uint)i + 1,
                        Width = GetWidth(headerFont, columnName)
                    });
                }
            }

            using (var cellFont = new System.Drawing.Font(PrintFormatting.ExcelCellFont, Convert.ToInt32(PrintFormatting.ExcelCellFontSize)))
            {
                uint rowIndex = 2;
                foreach (DataRow dr in dt.Rows)
                {
                    var row = new Row { RowIndex = rowIndex };
                    sheetData.AppendChild(row);
                    foreach (var col in columns)
                    {
                        var value = Convert.ToString(dr[col.ColumnName]);
                        AppendCell(col.ExcelColumnName, value, row, rowIndex, cellCellFormatIndex);

                        if (String.IsNullOrWhiteSpace(value)) continue;

                        var width = GetWidth(cellFont, value);
                        if (width > col.Width)
                        {
                            col.Width = width;
                        }
                    }

                    rowIndex += 1;
                }
            }

            var excelColumns = new Columns();
            foreach (var col in columns)
            {
                var c = new Column();
                c.Min = col.Index;
                c.Max = col.Index;
                c.Width = col.Width;
                c.CustomWidth = true;
                c.BestFit = true;
                excelColumns.Append(c);
            }

            worksheet.Append(excelColumns);

            worksheet.Append(sheetData);
            worksheetPart.Worksheet = worksheet;
            worksheetPart.Worksheet.Save();

            var sheets = spreadsheetDocument.WorkbookPart.Workbook.GetFirstChild<Sheets>();
            var relationshipId = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart);

            // Get a unique ID for the new worksheet.
            uint sheetId = 1;
            if (sheets.Elements<Sheet>().Any())
            {
                sheetId = sheets.Elements<Sheet>().Select(s => s.SheetId.Value).Max() + 1;
            }

            // Append the new worksheet and associate it with the workbook.
            var sheet = new Sheet
            {
                Id = relationshipId,
                SheetId = sheetId,
                Name = sheetName
            };

            sheets.AppendChild(sheet);
        }

        /// <summary>
        /// Gets excel column name by index
        /// </summary>
        /// <param name="columnNumber"></param>
        /// <returns></returns>
        private static string GetExcelColumnName(int columnNumber)
        {
            var dividend = columnNumber + 1;
            var columnName = String.Empty;

            while (dividend > 0)
            {
                var modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo) + columnName;
                dividend = (int)((dividend - modulo) / 26);
            }

            return columnName;
        }

        /// <summary>
        /// Appends cell to row
        /// </summary>
        /// <param name="cellName"></param>
        /// <param name="cellValue"></param>
        /// <param name="row"></param>
        /// <param name="rowIndex"></param>
        /// <param name="styleIndex"></param>
        private static void AppendCell(string cellName, string cellValue, Row row, uint rowIndex, uint styleIndex)
        {
            var cellReference = cellName + rowIndex;
            var cell = new Cell { CellReference = cellReference };
            row.AppendChild(cell);
            cell.CellValue = new CellValue(cellValue);
            cell.DataType = new EnumValue<CellValues>(CellValues.String);
            cell.StyleIndex = styleIndex;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stringFont"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        private static double GetWidth(System.Drawing.Font stringFont, string text)
        {
            var textSize = TextRenderer.MeasureText(text, stringFont);
            return (double)decimal.Round(textSize.Width / 7m + 1, 2);
        }

        /// <summary>
        /// Creates a font index
        /// </summary>
        /// <param name="templateSheetobj">Style sheet object of the template</param>
        /// <param name="fontName">Name of desired font</param>
        /// <param name="fontSizeToApply">Font size to apply</param>
        /// <param name="isFontBold">Is it a bold font</param>
        /// <param name="FontforeColor">Font color</param>
        /// <returns></returns>
        private static UInt32Value CreateFont(Stylesheet templateSheetobj, string fontName, Nullable<double> fontSizeToApply, bool isFontBold, System.Drawing.Color FontforeColor)
        {
            Font font = new Font();
            if (!string.IsNullOrEmpty(fontName))
            {
                FontName name = new FontName { Val = fontName };
                font.Append(name);
            }
            if (fontSizeToApply.HasValue)
            {
                FontSize size = new FontSize { Val = fontSizeToApply.Value };
                font.Append(size);
            }
            if (isFontBold == true)
            {
                DocumentFormat.OpenXml.Spreadsheet.Bold bold = new DocumentFormat.OpenXml.Spreadsheet.Bold();
                font.Append(bold);
            }
            if (!string.IsNullOrEmpty(FontforeColor.Name))
            {
                Color color = new Color { Rgb = new HexBinaryValue { Value = System.Drawing.ColorTranslator.ToHtml(System.Drawing.Color.FromArgb(FontforeColor.A, FontforeColor.R, FontforeColor.G, FontforeColor.B)).Replace("#", "") } };
                font.Append(color);
            }

            templateSheetobj.Fonts.Append(font);
            UInt32Value result = templateSheetobj.Fonts.Count.Value;
            templateSheetobj.Fonts.Count++;
            return result;
        }

        /// <summary>
        /// Creates Fill index
        /// </summary>
        /// <param name="templateSheetobj">Style sheet object of the template</param>
        /// <param name="colorCode">Hex color code</param>
        /// <returns></returns>
        private static UInt32Value CreateFill(Stylesheet templateSheetobj, string colorCode)
        {
            PatternFill patternFill = new PatternFill() { PatternType = PatternValues.Solid };
            ForegroundColor fgColor = new ForegroundColor() { Rgb = colorCode };
            BackgroundColor bgBolor = new BackgroundColor() { Indexed = (UInt32Value)64U };
            patternFill.Append(fgColor);
            patternFill.Append(bgBolor);
            Fill fill = new Fill(patternFill);
            templateSheetobj.Fills.Append(fill);
            UInt32Value result = templateSheetobj.Fills.Count;
            templateSheetobj.Fills.Count++;
            return result;
        }

        /// <summary>
        /// Creates the border for the cell
        /// </summary>
        /// <param name="templateSheetobj">Style sheet object  of the template</param>
        /// <param name="borderStyle">Desired border style</param>
        /// <returns>Border Index</returns>
        private static UInt32Value CreateBorder(Stylesheet templateSheetobj, BorderStyleValues borderStyle)
        {
            Border border = new Border();
            LeftBorder leftBorder = new LeftBorder();
            RightBorder rightBorder = new RightBorder();
            BottomBorder bottomBorder = new BottomBorder();
            TopBorder topBorder = new TopBorder();
            if (borderStyle == BorderStyleValues.Thin)
            {
                leftBorder.Style = BorderStyleValues.Thin;
                rightBorder.Style = BorderStyleValues.Thin;
                topBorder.Style = BorderStyleValues.Thin;
                bottomBorder.Style = BorderStyleValues.Thin;
                leftBorder.Append(new Color() { Indexed = (UInt32Value)64U });
                rightBorder.Append(new Color() { Indexed = (UInt32Value)64U });
                topBorder.Append(new Color() { Indexed = (UInt32Value)64U });
                bottomBorder.Append(new Color() { Indexed = (UInt32Value)64U });
            }
            else if (borderStyle == BorderStyleValues.Dotted)
            {
                leftBorder.Style = BorderStyleValues.Dotted;
                rightBorder.Style = BorderStyleValues.Dotted;
                topBorder.Style = BorderStyleValues.Dotted;
                bottomBorder.Style = BorderStyleValues.Dotted;
                leftBorder.Append(new Color() { Theme = 0, Tint = -0.499984740745262 });
                rightBorder.Append(new Color() { Theme = 0, Tint = -0.499984740745262 });
                topBorder.Append(new Color() { Theme = 0, Tint = -0.499984740745262 });
                bottomBorder.Append(new Color() { Theme = 0, Tint = -0.499984740745262 });
            }
            border.Append(leftBorder);
            border.Append(rightBorder);
            border.Append(topBorder);
            border.Append(bottomBorder);
            ////borders.Append(border);
            templateSheetobj.Borders.Append(border);
            UInt32Value result = templateSheetobj.Borders.Count;
            templateSheetobj.Borders.Count++;
            return result;

        }

        /// <summary>
        /// Creates the cell format index
        /// </summary>
        /// <param name="templateSheetobj">Style sheet object of the template</param>
        /// <param name="fontIndex">Font Index</param>
        /// <param name="fillIndex">Fill Index</param>
        /// <param name="borderIndex">Border Index</param>
        /// <returns></returns>
        private static UInt32Value CreateCellFormat(Stylesheet templateSheetobj, UInt32Value fontIndex, UInt32Value fillIndex, UInt32Value borderIndex)
        {
            CellFormat cellFormat = new CellFormat(new Alignment { WrapText = false, Vertical = VerticalAlignmentValues.Center, Horizontal = HorizontalAlignmentValues.Left });

            if (fontIndex != null)
            {
                cellFormat.FontId = fontIndex;
                cellFormat.ApplyFont = true;
            }
            if (fillIndex != null)
            {
                cellFormat.FillId = fillIndex;
                cellFormat.ApplyFill = true;
            }
            if (borderIndex != null)
            {
                cellFormat.BorderId = borderIndex;
            }
            templateSheetobj.CellFormats.Append(cellFormat);

            UInt32Value result = templateSheetobj.CellFormats.Count;
            templateSheetobj.CellFormats.Count++;

            return result;
        }
    }
}
