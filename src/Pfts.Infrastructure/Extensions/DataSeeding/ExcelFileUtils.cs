/*namespace Pfts.Infrastructure.Extensions.DataSeeding;

using System.Data;
using ClosedXML.Excel;

public class ExcelFileUtils
{
    public static DataTable GetDataFromExcel(string path, string worksheetName)
    {
        var dt = new DataTable();
        try
        {
            using var workBook = new XLWorkbook(path);
            var workSheet = workBook.Worksheet(worksheetName);

            if (workSheet == null)
            {
                Console.WriteLine("Worksheet not found.");
                return dt;
            }
            dt.TableName = worksheetName;

            var firstRow = true;

            foreach (var row in workSheet.RowsUsed())
            {
                if (firstRow)
                {
                    foreach (var cell in row.Cells())
                    {
                        if (!string.IsNullOrEmpty(cell.Value.ToString()))
                        {
                            dt.Columns.Add(cell.Value.ToString());
                        }
                        else
                        {
                            break;
                        }
                    }

                    firstRow = false;
                }
                else
                {
                    var toInsert = dt.NewRow();

                    for (var i = 0; i < dt.Columns.Count; i++)
                    {
                        var cell = row.Cell(i + 1);
                        toInsert[i] = cell.Value.ToString();
                    }

                    dt.Rows.Add(toInsert);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }

        return dt;
    }
}
*/
