using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using GrowthTracker.BackEnd.Dto;
using System.Linq;

namespace GrowthTracker.BackEnd.Integration.Excel
{
    public class ExcelReader
    {
        public List<AddCompetencyDto> Read()
        {
            List<AddCompetencyDto> list = new List<AddCompetencyDto>();
            using (SpreadsheetDocument? spreadSheetDocument = SpreadsheetDocument.Open(@"C:\Users\andys\OneDrive\Documents\Normalized Developer Matrix.xlsx", false))
            {

                WorkbookPart workbookPart = spreadSheetDocument.WorkbookPart;
                IEnumerable<Sheet> sheets = spreadSheetDocument.WorkbookPart.Workbook.GetFirstChild<Sheets>().Elements<Sheet>();
                string relationshipId = sheets.First().Id.Value;
                WorksheetPart worksheetPart = (WorksheetPart)spreadSheetDocument.WorkbookPart.GetPartById(relationshipId);
                Worksheet workSheet = worksheetPart.Worksheet;
                SheetData sheetData = workSheet.GetFirstChild<SheetData>();
                IEnumerable<Row> rows = sheetData.Descendants<Row>();


                foreach (Row row in rows.Skip(1))
                {
                    var key = GetCellValue(spreadSheetDocument, row.Descendants<Cell>().ElementAt(0));
                    var attr = GetCellValue(spreadSheetDocument, row.Descendants<Cell>().ElementAt(1));
                    var comp = GetCellValue(spreadSheetDocument, row.Descendants<Cell>().ElementAt(2));
                    var d1 = GetCellValue(spreadSheetDocument, row.Descendants<Cell>().ElementAt(3));
                    var d2 = GetCellValue(spreadSheetDocument, row.Descendants<Cell>().ElementAt(4));
                    var d3 = GetCellValue(spreadSheetDocument, row.Descendants<Cell>().ElementAt(5));
                    var d4 = GetCellValue(spreadSheetDocument, row.Descendants<Cell>().ElementAt(6));
                    var d5 = GetCellValue(spreadSheetDocument, row.Descendants<Cell>().ElementAt(7));
                    var d6 = GetCellValue(spreadSheetDocument, row.Descendants<Cell>().ElementAt(8));
                    AddCompetencyDto dto = new AddCompetencyDto(comp,key,attr);
                    dto.Level1Description = d1;
                    dto.Level2Description = d2;
                    dto.Level3Description = d3;
                    dto.Level4Description = d4;
                    dto.Level5Description = d5;
                    dto.Level6Description = d6;

                    list.Add(dto);
                }
             return list;
            }
        }

        public static string GetCellValue(SpreadsheetDocument document, Cell cell)
        {
            SharedStringTablePart stringTablePart = document.WorkbookPart.SharedStringTablePart;
            if (cell.CellValue is null)
                return String.Empty;

            string value = cell.CellValue.InnerXml;

            if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
            {
                return stringTablePart.SharedStringTable.ChildElements[Int32.Parse(value)].InnerText;
            }
            else
            {
                return value;
            }
        }

    }
}
