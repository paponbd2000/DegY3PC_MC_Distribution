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
        SqlCommand centerCodeSqlCommand, regiNoSqlCommand, paperCodeSqlCommand;
        StreamWriter sw;
        private static int lineCount, pageCount; // Row number and page number indicator
        // bool footerPrinted;
        // string EXAM_CODE;
        public PrinterDegY3()
        {
            conn = DatabaseHandler.getSqlConnection();
            centerCodeSqlCommand = new SqlCommand();
            regiNoSqlCommand = new SqlCommand();
            paperCodeSqlCommand = new SqlCommand();
            centerCodeSqlCommand.Connection = conn;
            regiNoSqlCommand.Connection = conn;
            paperCodeSqlCommand.Connection = conn;
            centerCodeSqlCommand.CommandText = String.Format("SELECT DISTINCT c_code, centre FROM absent ORDER BY c_code");

            SqlDataReader centerCodeReader = centerCodeSqlCommand.ExecuteReader();
            if (centerCodeReader.HasRows)
            {
                while (centerCodeReader.Read())
                {
                    string c_code = centerCodeReader["c_code"].ToString();
                    string centre = centerCodeReader["centre"].ToString();
                    string FILE_PATH = "D:\\DEG_Y3\\absent_list\\absent_" + c_code + ".txt";
                    sw = new StreamWriter(FILE_PATH);
                    lineCount = 0;
                    pageCount = 0;
                    generateAbsentList(c_code, centre, sw);
                }
            }
            centerCodeReader.Close();
            // centerCodeReader.Dispose();
        }

        public void generateAbsentList(string c_code, string centre, StreamWriter sw)
        {
            regiNoSqlCommand.CommandText = String.Format("SELECT DISTINCT col_code, reg_no, exm_roll, std_name FROM absent WHERE c_code = '" + c_code + "' ORDER BY col_code, reg_no");
            SqlDataReader regiNoReader = regiNoSqlCommand.ExecuteReader();
            if (regiNoReader.HasRows)
            {
                printAbsentListHeader(c_code, centre, sw);
                while (regiNoReader.Read())
                {
                    string regiNo = regiNoReader["reg_no"].ToString();
                    string exmRoll = regiNoReader["exm_roll"].ToString();
                    string stdName = regiNoReader["std_name"].ToString();
                    if (stdName.Length > 9) stdName = stdName.Substring(0, 9);
                    else if (stdName.Length < 9) stdName = stdName.PadRight(9, ' ');
                    sw.Write("{0}|{1}|{2}", regiNo, exmRoll, stdName);

                    paperCodeSqlCommand.CommandText = String.Format("SELECT pap_code FROM absent WHERE reg_no = '" + regiNo + "' ORDER BY pap_code");
                    SqlDataReader paperCodeReader = paperCodeSqlCommand.ExecuteReader();
                    if (paperCodeReader.HasRows)
                    {
                        int paperCount = 0;
                        while (paperCodeReader.Read())
                        {
                            paperCount++;
                            sw.Write("|{0}|             ", paperCodeReader["pap_code"].ToString());
                        }
                        for (int i = 1; i <= 5 - paperCount; i++)
                        {
                            sw.Write("|      |             ");
                        }
                    }
                    paperCodeReader.Close();
                    // paperCodeReader.Dispose();
                    sw.WriteLine();
                    sw.WriteLine("--------------------------------------------------------------------------------------------------------------------------------------");
                    lineCount = lineCount + 2;
                    if (lineCount > 58)
                    {
                        printAbsentListHeader(c_code, centre, sw);
                    }

                }
            }
            regiNoReader.Close();
            // regiNoReader.Dispose();
            sw.Close();
            // sw.Dispose();
            // conn.Close();
            // conn.Dispose();
        }

        private void printAbsentListHeader(string c_code, string centre, StreamWriter sw)
        {
            pageCount++;
            lineCount = 0;
            sw.WriteLine("\f");
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("DATE : {0}                                                                                                         PAGE NO.: {1}", DateTime.Now.ToString("dd/MM/yyyy"), pageCount);
            sw.WriteLine("                                ABSENT LIST FOR DEGREE PASS AND CERTIFICATE COURSE EXAMINATION (3RD YEAR) - 2018");

            sw.WriteLine("CENTRE CODE & NAME : [{0}] - {1}", c_code, centre.ToUpper());
            sw.WriteLine();
            sw.WriteLine("======================================================================================================================================");
            sw.WriteLine("   REG_NO  |ROLL_NO|   NAME  |SUB_1 |   FORM_SL   |SUB_2 |   FORM_SL   |SUB_3 |  FORM_SL    |SUB_4 | FORM_SL     |SUB_5 |   FORM_SL");
            sw.WriteLine("======================================================================================================================================");

            lineCount = lineCount + 11;
        }

        //private void printAbsentListSubHeader(StreamWriter sw, string col_code, string col_name)
        //{
        //    sw.WriteLine();
        //    sw.WriteLine("COLLEGE : ({0}) --> {1}", col_code, col_name.ToUpper());
        //    sw.WriteLine("=====================================================================================================================================");
        //    sw.WriteLine("REGI NO.    | SESSION | TYPE | STUDENT'S NAME                 | SUBJECTS");
        //    sw.WriteLine("=====================================================================================================================================");

        //    lineCount = lineCount + 5;
        //}

        //private void printAbsentListRecord(string regi, string sess, string type, string name, List<string> papers, StreamWriter sw)
        //{
        //    string std_type = "";
        //    string std_name = "";
        //    switch (type)
        //    {
        //        case "1":
        //            std_type = "REG";
        //            break;
        //        case "2":
        //            std_type = "IRR";
        //            break;
        //        case "4":
        //            std_type = "IMP";
        //            break;
        //        default:
        //            std_type = "REG";
        //            break;
        //    }

        //    if (name.Length > 30) std_name = name.Substring(0, 30).ToUpper();
        //    else std_name = String.Format("{0,-30}", name).ToUpper();

        //    sw.Write("{0} | {1} | {2}  | {3} |", regi, sess, std_type, std_name);

        //    int count = 0;
        //    foreach (string pap in papers)
        //    {
        //        if (pap.Length == 0) continue;
        //        count++;
        //        if (count == papers.Count) sw.Write("{0}", pap);
        //        else sw.Write("{0}          ", pap);
        //    }

        //    sw.WriteLine();
        //    sw.WriteLine("-------------------------------------------------------------------------------------------------------------------------------------");

        //    lineCount = lineCount + 2;
        //}

        //private void printAbsentListFooter(StreamWriter sw)
        //{
        //    sw.WriteLine();
        //    sw.WriteLine();
        //    sw.WriteLine("CHECKED BY             SECTION OFFICER                            DEPUTY CONTROLLER");
        //    sw.WriteLine();
        //    sw.WriteLine("\f");

        //    lineCount = 0;
        //    footerPrinted = true;
        //}

        //private void printSpace(StreamWriter sw)
        //{
        //    sw.WriteLine("                                                                                      ");
        //    sw.WriteLine("                                                                                      ");
        //}
    }
}
