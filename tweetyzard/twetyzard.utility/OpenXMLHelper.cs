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
        public static void ExportDataSetToExcel(DataSet ds, string destination, bool isFistColumnNeeded = false)
        {
            if (isFistColumnNeeded)
            {
                ds.Tables[0].Columns.RemoveAt(0);
                ds.AcceptChanges();
            }
            if (!string.IsNullOrEmpty(destination))
            {
                using (var spreadsheetDocument = SpreadsheetDocument.Create(destination, SpreadsheetDocumentType.Workbook))
                {
                    spreadsheetDocument.AddWorkbookPart();
                    spreadsheetDocument.WorkbookPart.Workbook = new Workbook();
                    spreadsheetDocument.WorkbookPart.Workbook.Sheets = new Sheets();
                    var stylesPart = spreadsheetDocument.WorkbookPart.AddNewPart<WorkbookStylesPart>();
                    stylesPart.Stylesheet = new Stylesheet();

                    stylesPart.Stylesheet.Fonts = new Fonts();
                    stylesPart.Stylesheet.Fonts.Count = 1;
                    stylesPart.Stylesheet.Fonts.AppendChild(new Font());

                    stylesPart.Stylesheet.Fills = new Fills();
                    stylesPart.Stylesheet.Fills.AppendChild(new Fill { PatternFill = new PatternFill { PatternType = PatternValues.None } }); 
                    stylesPart.Stylesheet.Fills.AppendChild(new Fill { PatternFill = new PatternFill { PatternType = PatternValues.Gray125 } });
                    stylesPart.Stylesheet.Fills.Count = 2;

                    stylesPart.Stylesheet.Borders = new Borders();
                    stylesPart.Stylesheet.Borders.AppendChild(new Border());
                    stylesPart.Stylesheet.Borders.Count = 1;

                    stylesPart.Stylesheet.CellStyleFormats = new CellStyleFormats();
                    stylesPart.Stylesheet.CellStyleFormats.AppendChild(new CellFormat());
                    stylesPart.Stylesheet.CellStyleFormats.Count = 1;

                    stylesPart.Stylesheet.CellFormats = new CellFormats();
                   
                    stylesPart.Stylesheet.CellFormats.AppendChild(new CellFormat());
                    stylesPart.Stylesheet.CellFormats.Count = 1;

                    var headerFontIndex = CreateFont(stylesPart.Stylesheet, ExcelFormatting.ExcelHeaderFont, Convert.ToInt32(ExcelFormatting.ExcelCellFontSize), true, System.Drawing.Color.White);
                    var headerFillIndex = CreateFill(stylesPart.Stylesheet, ExcelFormatting.ExcelColumnHeaderFillColor);
                    var cellFontIndex = CreateFont(stylesPart.Stylesheet, ExcelFormatting.ExcelCellFont, Convert.ToInt32(ExcelFormatting.ExcelCellFontSize), false, System.Drawing.Color.Black);
                    var cellFillIndex = CreateFill(stylesPart.Stylesheet, ExcelFormatting.ExcelCellFillColor);
                    var headerBorderIndex = CreateBorder(stylesPart.Stylesheet, BorderStyleValues.Thin);
                    var cellBorderIndex = CreateBorder(stylesPart.Stylesheet, BorderStyleValues.Dotted);
                    var headerCellFormatIndex = CreateCellFormat(stylesPart.Stylesheet, headerFontIndex, headerFillIndex, headerBorderIndex);
                    var cellCellFormatIndex = CreateCellFormat(stylesPart.Stylesheet, cellFontIndex, cellFillIndex, cellBorderIndex);

                    stylesPart.Stylesheet.Save();

                    using (ds)
                    {
                        var tableNames = ds.Tables[ds.Tables.Count - 1];
                        string sheetName = Convert.ToString(tableNames.Rows[0]["SearchPhrase"]);

                        if (sheetName.Length > 10)
                        {
                            sheetName = sheetName.Substring(0,10);
                        }
                       
                        for (var i = 0; i < ds.Tables.Count; i++)
                        {
                            AddSheet(ds.Tables[i], spreadsheetDocument, sheetName, headerCellFormatIndex, cellCellFormatIndex);
                        }
                    }
                }
            }
        }

        private static void AddSheet(DataTable dt, SpreadsheetDocument spreadsheetDocument, string sheetName, uint headerCellFormatIndex, uint cellCellFormatIndex)
        {
            var worksheetPart = spreadsheetDocument.WorkbookPart.AddNewPart<WorksheetPart>();
            var worksheet = new Worksheet();
            var sheetData = new SheetData();

            const uint headerRowIndex = 1;
            var headerRow = new Row { RowIndex = headerRowIndex };
            sheetData.AppendChild(headerRow);

            var columns = new List<ExcelColumn>();

            if (dt.Rows.Count < 500)
            {
                for (var intRowPos = dt.Rows.Count; intRowPos < 500; intRowPos++)
                {
                    var blankDataRow = dt.NewRow();
                    dt.Rows.InsertAt(blankDataRow, intRowPos);
                }
            }

            using (var headerFont = new System.Drawing.Font(ExcelFormatting.ExcelHeaderFont, Convert.ToInt32(ExcelFormatting.ExcelCellFontSize), System.Drawing.FontStyle.Bold))
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

            using (var cellFont = new System.Drawing.Font(ExcelFormatting.ExcelCellFont, Convert.ToInt32(ExcelFormatting.ExcelCellFontSize)))
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

            uint sheetId = 1;
            if (sheets.Elements<Sheet>().Any())
            {
                sheetId = sheets.Elements<Sheet>().Select(s => s.SheetId.Value).Max() + 1;
            }

            var sheet = new Sheet
            {
                Id = relationshipId,
                SheetId = sheetId,
                Name = sheetName
            };

            sheets.AppendChild(sheet);
        }

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

        private static void AppendCell(string cellName, string cellValue, Row row, uint rowIndex, uint styleIndex)
        {
            var cellReference = cellName + rowIndex;
            var cell = new Cell { CellReference = cellReference };
            row.AppendChild(cell);
            cell.CellValue = new CellValue(cellValue);
            cell.DataType = new EnumValue<CellValues>(CellValues.String);
            cell.StyleIndex = styleIndex;
        }

        private static double GetWidth(System.Drawing.Font stringFont, string text)
        {
            var textSize = TextRenderer.MeasureText(text, stringFont);
            return (double)decimal.Round(textSize.Width / 7m + 1, 2);
        }

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
            templateSheetobj.Borders.Append(border);
            UInt32Value result = templateSheetobj.Borders.Count;
            templateSheetobj.Borders.Count++;
            return result;

        }

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
