using System;
using System.Collections.Generic;
using System.IO;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BBAAbsentList
{
    class PrinterDegY3
    {
        SqlConnection conn;
        SqlCommand studentSqlCommand;
        StreamWriter sw;
        private static int lineCount, pageCount; // Row number and page number indicator
        // bool footerPrinted;
        // string EXAM_CODE;
        public PrinterDegY3()
        {
            conn = DatabaseHandler.getSqlConnection();
            studentSqlCommand = new SqlCommand();
            studentSqlCommand.Connection = conn;
            string FILE_PATH = "D:\\DEG_Y3\\pc_mc\\pc_mc_print_list.txt";
            sw = new StreamWriter(FILE_PATH);
            generatePCMCPrintList(sw);
        }

        public void generatePCMCPrintList(StreamWriter sw)
        {
            studentSqlCommand.CommandText = String.Format("SELECT col_code, college, MIN(RIGHT(CRT_SL, 6)) AS min_sl, MAX(RIGHT(CRT_SL, 6)) AS max_sl, COUNT(*) AS total FROM mrkcrt18_copy GROUP BY col_code, college ORDER BY col_code DESC");
            SqlDataReader studentDataReader = studentSqlCommand.ExecuteReader();
            if (studentDataReader.HasRows)
            {
                printAbsentListHeader(sw);
                while (studentDataReader.Read())
                {
                    string college = studentDataReader["college"].ToString();
                    if (college.Length > 42) college = college.Substring(0, 42);
                    else if (college.Length < 42) college = college.PadRight(42);
                    sw.WriteLine("   {0}| {1}| {2} - {3} = {4} |                                |                  |"
                        , studentDataReader["col_code"].ToString().PadRight(7)
                        , college
                        , studentDataReader["min_sl"].ToString().PadRight(6)
                        , studentDataReader["max_sl"].ToString().PadRight(6)
                        , studentDataReader["total"].ToString().PadLeft(6)
                        );
                    sw.WriteLine("--------------------------------------------------------------------------------------------------------------------------------------");
                    lineCount = lineCount + 2;
                    if (lineCount > 58)
                    {
                        printAbsentListHeader(sw);
                    }
                }
            }
            studentDataReader.Close();
            // studentDataReader.Dispose();
            sw.WriteLine("\f");
            sw.Close();
            // sw.Dispose();
            // conn.Close();
            // conn.Dispose();
        }

        private void printAbsentListHeader(StreamWriter sw)
        {
            pageCount++;
            lineCount = 0;
            sw.WriteLine("\f");
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("DATE : {0}                                                                                                         PAGE NO.: {1}", DateTime.Now.ToString("dd/MM/yyyy"), pageCount);
            sw.WriteLine("                             MARKS SHEET & CERTIFICATE PRINTING REGISTER FOR DEGREE PASS EXAMINATION - 2018");
            sw.WriteLine();
            sw.WriteLine("======================================================================================================================================");
            sw.WriteLine(" COL_CODE | COLLEGE NAME                              | SERIAL NO / TOTAL        | FORM SERIAL                    | SIGNATURE        |");
            sw.WriteLine("======================================================================================================================================");
            lineCount = lineCount + 10;
        }
    }
}
