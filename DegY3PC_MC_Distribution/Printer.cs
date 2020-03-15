using System;
using System.Collections.Generic;
using System.IO;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BBAAbsentList
{
    class Printer
    {
        SqlConnection conn;
        SqlCommand collegeCodeSqlCommand, collegeNameSqlCommand, studentInformationSqlCommand, eSolveSqlCommand;
        StreamWriter sw;
        static readonly int LINE_LIMIT = 50;
        static readonly string COLLEGE_TABLE = "allcol18";
        string EXAM_NAME;
        string FILE_PATH;
        string FF_TABLE;
        string E_ALLDBF_TABLE;
        private static int lineCount, pageCount; //row number indicator
        bool footerPrinted;
        string EXAM_CODE;
        public Printer(String examCode, String examYear)
        {
            switch (examCode)
            {
                case "601":
                    EXAM_NAME = "BBA FIRST YEAR 1ST SEMESTER EXAMINATION";
                    break;
                case "602":
                    EXAM_NAME = "BBA FIRST YEAR 2ND SEMESTER EXAMINATION";
                    break;
                case "603":
                    EXAM_NAME = "BBA SECOND YEAR 3RD SEMESTER EXAMINATION";
                    break;
                case "604":
                    EXAM_NAME = "BBA SECOND YEAR 4TH SEMESTER EXAMINATION";
                    break;
                case "605":
                    EXAM_NAME = "BBA THIRD YEAR 5TH SEMESTER EXAMINATION";
                    break;
                case "606":
                    EXAM_NAME = "BBA THIRD YEAR 6TH SEMESTER EXAMINATION";
                    break;
                case "607":
                    EXAM_NAME = "BBA FOURTH YEAR 7TH SEMESTER EXAMINATION";
                    break;
                case "608":
                    EXAM_NAME = "BBA FOURTH YEAR 8TH SEMESTER EXAMINATION";
                    break;
                default:
                    break;
            }

            EXAM_NAME =  EXAM_NAME + " - " + examYear;
            FILE_PATH = "D:\\bba_absent_list_" + examCode + "_" + examYear + ".txt";
            FF_TABLE = "ff_data_" + examCode + "_" + examYear;
            E_ALLDBF_TABLE = "e_alldbf_" + examCode + "_" + examYear;
            EXAM_CODE = examCode;

            conn = DatabaseHandler.getSqlConnection();
            collegeCodeSqlCommand = new SqlCommand();
            collegeNameSqlCommand = new SqlCommand();
            studentInformationSqlCommand = new SqlCommand();
            eSolveSqlCommand = new SqlCommand();
            collegeCodeSqlCommand.Connection = conn;
            collegeNameSqlCommand.Connection = conn;
            studentInformationSqlCommand.Connection = conn;
            eSolveSqlCommand.Connection = conn;
            sw = new StreamWriter(FILE_PATH);
            lineCount = 0;
            pageCount = 0;
            // MessageBox.Show("Exam Name : " + EXAM_NAME + "\nFile Path : " + FILE_PATH + "\nFF Table : " + FF_TABLE + "\nE_All Table : " + E_ALLDBF_TABLE);
            generateAbsentList(sw);
        }

        public void generateAbsentList(StreamWriter sw)
        {
            collegeCodeSqlCommand.CommandText = String.Format("SELECT DISTINCT col_code FROM " + FF_TABLE + " ORDER BY col_code");
            SqlDataReader collegeCodeReader = collegeCodeSqlCommand.ExecuteReader();
            if (collegeCodeReader.HasRows)
            {
                while (collegeCodeReader.Read())
                {
                    string col_code = collegeCodeReader["col_code"].ToString();
                    string last_col_code = "";

                    collegeNameSqlCommand.CommandText = String.Format("SELECT college FROM " + COLLEGE_TABLE + " WHERE col_code = '" + col_code + "'");
                    SqlDataReader collegeNameReader = collegeNameSqlCommand.ExecuteReader();
                    string college = "";
                    if (collegeNameReader.HasRows)
                    {
                        collegeNameReader.Read();
                        college = collegeNameReader["college"].ToString();
                    }

                    if (lineCount == 0)
                    {
                        pageCount++;
                        printAbsentListHeader(pageCount, sw);
                        //printAbsentListSubHeader(sw, col_code, college);
                    }

                    studentInformationSqlCommand.CommandText = String.Format("SELECT * FROM " + FF_TABLE + " WHERE col_code = '" + col_code + "' ORDER BY reg_no, std_type");
                    SqlDataReader studentInformationReader = studentInformationSqlCommand.ExecuteReader();

                    if (studentInformationReader.HasRows)
                    {
                        while (studentInformationReader.Read())
                        {
                            string regNo = studentInformationReader["reg_no"].ToString();
                            string session = "20" + studentInformationReader["sess1"].ToString() + "-" + studentInformationReader["sess2"].ToString();
                            string std_type = studentInformationReader["std_type"].ToString();
                            string std_name = studentInformationReader["std_name"].ToString();
                            // var regex = new Regex(" +");
                            // string[] paperAll = System.Text.RegularExpressions.Regex.Split(studentInformationReader["sif_sub"].ToString() + " " + studentInformationReader["imp_sub"].ToString(), @"\s+");
                            string[] paperAll = (studentInformationReader["sif_sub"].ToString() + " " + studentInformationReader["imp_sub"].ToString()).Split(' ');
                            List<string> paperAllList = new List<string>();
                            // paperAllList.Sort();

                            // Polpulate list of all subjects found in SIF from array
                            foreach (string paper in paperAll)
                            {
                                // For 8th semester only
                                if(EXAM_CODE == "608")
                                {
                                    if(paper == "004235" || paper == "004245" || paper == "004255" || paper == "004265")
                                    {
                                        continue;
                                    }
                                }

                                if (paper.Trim().Length >= 4)
                                {
                                    paperAllList.Add(paper);
                                }
                            }
                            paperAllList.Sort();

                            List<string> paperAbsentList = new List<string>();
                            List<string> paperEtypeList = new List<string>();

                            eSolveSqlCommand.CommandText = String.Format("SELECT reg_no, pap_code FROM " + E_ALLDBF_TABLE + " WHERE reg_no = '" + regNo + "'");
                            SqlDataReader eSolveReader = eSolveSqlCommand.ExecuteReader();

                            // Polpulate list of all subjects found in E-type data
                            if (eSolveReader.HasRows)
                            {
                                while (eSolveReader.Read())
                                {
                                    string paperPresent = eSolveReader["pap_code"].ToString();
                                    paperEtypeList.Add(paperPresent);
                                }
                            }

                            foreach (string paper in paperAllList)
                            {
                                if (paperEtypeList.Find(x => x.Contains(paper)) == null)
                                {
                                    paperAbsentList.Add(paper);
                                }
                            }

                            // PRINTING FUNCTIONS
                            // lineCount++;
                            if (paperAbsentList.Count > 0)
                            {
                                if (lineCount == 0)
                                {
                                    pageCount++;
                                    printAbsentListHeader(pageCount, sw);
                                    // printAbsentListSubHeader(sw, col_code, college);
                                }

                                if ((last_col_code != col_code) && (lineCount != 0) && ((lineCount + 4) < LINE_LIMIT) && studentInformationReader.HasRows)
                                {
                                    printAbsentListSubHeader(sw, col_code, college);
                                    last_col_code = col_code;
                                    // lineCount += 2;
                                }
                                else if ((last_col_code == col_code) && ((lineCount - 5) == 0))
                                {
                                    printAbsentListSubHeader(sw, col_code, college);
                                    last_col_code = col_code;
                                    // lineCount += 2;
                                }

                                printAbsentListRecord(regNo, session, std_type, std_name, paperAbsentList, sw);

                                if (lineCount >= LINE_LIMIT)
                                {
                                    printAbsentListFooter(sw);
                                }
                            }

                            eSolveReader.Dispose();
                        }
                    }
                    studentInformationReader.Dispose();
                    collegeNameReader.Dispose();

                    if (lineCount >= LINE_LIMIT)
                    {
                        printAbsentListFooter(sw);
                    }
                }
            }

            if (lineCount >= LINE_LIMIT)
            {
                printAbsentListFooter(sw);
            }

            if (!footerPrinted) printAbsentListFooter(sw);

            sw.Dispose();
            collegeCodeReader.Dispose();
        }

        private void printAbsentListHeader(int pageno, StreamWriter sw)
        {
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("DATE : {0}                                                                                                         PAGE NO.: {1}", DateTime.Now.ToString("dd/MM/yyyy"), pageno);
            sw.WriteLine("                                    ABSENT LIST FOR " + EXAM_NAME);
            // sw.WriteLine("Subject : ({0}){1} ", sub_code, sub_name);
            // sw.WriteLine("Subject :  ({0})  - {1}", sub_code, sub_name);
            // sw.WriteLine("Centre  :  ({0}) - {1}", c_code, center);
            // sw.WriteLine();

            lineCount = lineCount + 5;
            footerPrinted = false;
        }

        private void printAbsentListSubHeader(StreamWriter sw, string col_code, string col_name)
        {
            sw.WriteLine();
            sw.WriteLine("COLLEGE : ({0}) --> {1}", col_code, col_name.ToUpper());
            sw.WriteLine("=====================================================================================================================================");
            sw.WriteLine("REGI NO.    | SESSION | TYPE | STUDENT'S NAME                 | SUBJECTS");
            sw.WriteLine("=====================================================================================================================================");

            lineCount = lineCount + 5;
        }

        private void printAbsentListRecord(string regi, string sess, string type, string name, List<string> papers, StreamWriter sw)
        {
            string std_type = "";
            string std_name = "";
            switch (type)
            {
                case "1":
                    std_type = "REG";
                    break;
                case "2":
                    std_type = "IRR";
                    break;
                case "4":
                    std_type = "IMP";
                    break;
                default:
                    std_type = "REG";
                    break;
            }

            if (name.Length > 30) std_name = name.Substring(0, 30).ToUpper();
            else std_name = String.Format("{0,-30}", name).ToUpper();

            sw.Write("{0} | {1} | {2}  | {3} |", regi, sess, std_type, std_name);

            int count = 0;
            foreach (string pap in papers)
            {
                if (pap.Length == 0) continue;
                count++;
                if (count == papers.Count) sw.Write("{0}", pap);
                else sw.Write("{0}          ", pap);
            }

            sw.WriteLine();
            sw.WriteLine("-------------------------------------------------------------------------------------------------------------------------------------");

            lineCount = lineCount + 2;
        }

        private void printAbsentListFooter(StreamWriter sw)
        {
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("CHECKED BY             SECTION OFFICER                            DEPUTY CONTROLLER");
            sw.WriteLine();
            sw.WriteLine("\f");

            lineCount = 0;
            footerPrinted = true;
        }

        private void printSpace(StreamWriter sw)
        {
            sw.WriteLine("                                                                                      ");
            sw.WriteLine("                                                                                      ");
        }
    }
}
