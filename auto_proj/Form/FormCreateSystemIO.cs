﻿using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using auto_proj.Popup;
using auto_proj.Classes;
using System.Runtime.InteropServices;
using auto_proj.Enum;
using System.IO;
using System.Data.SqlClient;
using DevExpress.Spreadsheet;

namespace auto_proj.Form
{
    public partial class FormCreateSystemIO : DevExpress.XtraEditors.XtraForm
    {
        Project project = null;
        PopupSelectProj selectProj = null;

        Microsoft.Office.Interop.Excel.Application xlApp = null;
        Microsoft.Office.Interop.Excel.Workbook xlWorkbook = null;

        List<CSheet> sheetList = new List<CSheet>();
        List<PartIoCount> partIoCountList = new List<PartIoCount>();

        string sludgeName = "SLUDGE";
        string[] arrWorkingPart = { "INST", "PKG", "MCC", "공조제어" };
        //string[] arrWorkingPart = { "INST", "PKG" };
        string[] arrIoTypeNames = new string[4];

        DataTable dtInst = new DataTable();
        DataTable dtPkg = new DataTable();
        DataTable dtMcc = new DataTable();
        DataTable dtHvac = new DataTable();
        DataTable dtTempSum = new DataTable();
        DataTable dtSaved = new DataTable();

        HashSet<string> setPid = new HashSet<string>();

        // -- 신규
        string COL_IO_TYPE = "IO_Type";
        string COL_PLC = "PLC";
        string COL_PID = "PID_No";


        public FormCreateSystemIO()
        {
            InitializeComponent();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            selectProj = new PopupSelectProj();
            selectProj.SelectedProj += SelectProj_SelectedProj;
            selectProj.ShowDialog();
            selectProj.SelectedProj -= SelectProj_SelectedProj;

            CreateDataTable();
            SelectDetailIoTitle(project.ProjID);
        }

        private void SelectProj_SelectedProj(object sender, EventArgs e)
        {
            project = ((PopupSelectProj.SelectedProjArgs)e).project;
            txtCode.Text = project.ProjCode;
            txtName.Text = project.ProjName;
            txtPlc.Text = project.PlcBrand;
            txtCount.Text = project.PlcCount.ToString();
            txtAi.Text = project.AiDefine;
            txtAo.Text = project.AoDefine;
            txtDi.Text = project.DiDefine;
            txtDo.Text = project.DoDefine;
            txtInst.Text = project.InstFileName;
            txtCreated.Text = project.Created.ToString("yyyy-MM-dd HH:mm:ss");

            txtAiCh.Text = project.AiChannel.ToString();
            txtAoCh.Text = project.AoChannel.ToString();
            txtDiCh.Text = project.DiChannel.ToString();
            txtDoCh.Text = project.DoChannel.ToString();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (project == null) return;

            arrIoTypeNames[0] = project.AiDefine;
            arrIoTypeNames[1] = project.AoDefine;
            arrIoTypeNames[2] = project.DiDefine;
            arrIoTypeNames[3] = project.DoDefine;

            try
            {
                bool isExists = BinaryFile.MakeFileFromBinary(project.InstExcel, project.InstFileName);
                string path = Environment.CurrentDirectory + "\\" + project.InstFileName;

                if (!isExists)
                {
                    MessageBox.Show("Excel 파일을 불러오는데 실패하였습니다.");
                    return;
                }

                // 초기화
                partIoCountList.Clear();

                using (Workbook wb = new Workbook())
                {
                    wb.LoadDocument(path);
                    wb.History.IsEnabled = false;

                    for (int i = 0; i < wb.Worksheets.Count; i++)
                    {
                        string shtName = wb.Worksheets[i].Name;

                        for (int j = 0; j < arrWorkingPart.Length; j++)
                        {
                            string part = arrWorkingPart[j];

                            if (shtName.Contains(part) && !shtName.Contains(sludgeName))
                            {
                                // IO Counting
                                CheckIOCountToExcelSheet(wb.Worksheets[i], part);
                                break;
                            }
                        }
                    }

                    wb.Dispose();
                }

                GatheringIoCount();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {

            }


        }

        // ========== Excel
        private void CheckIOCountToExcelSheet(Worksheet _ws, string _part)
        {
            try
            {
                int stRowIdx = 0;
                int idxIOType = 0;
                int idxPLC = 0;
                int idxPID = 0;

                // IO Type Column 찾기
                stRowIdx = SearchColumnIndex(_ws, COL_IO_TYPE, out idxIOType);

                // PLC Column 찾기
                SearchColumnIndex(_ws, COL_PLC, out idxPLC);

                // PID Column 찾기
                SearchColumnIndex(_ws, COL_PID, out idxPID);

                

                PartIoCount partIoCount = new PartIoCount(_part);
                int returnCount = 0;

                // Sheet 검색
                for (int i = stRowIdx + 1; i < _ws.Rows.LastUsedIndex + 1; i++)
                {
                    string strIO = _ws.Cells[i, idxIOType].DisplayText;
                    string strPLC = _ws.Cells[i, idxPLC].DisplayText;
                    string strPID = _ws.Cells[i, idxPID].DisplayText.Trim();

                    //PID 저장
                    if(strPID != "") setPid.Add(strPID);

                    if (strIO == string.Empty || strPLC == string.Empty)
                    {
                        returnCount++;

                        if (returnCount == 50)
                            break;

                        continue;
                    }

                    string strNum = strPLC.Replace("PLC", "");
                    int plcNum = -1;

                    if(int.TryParse(strNum, out plcNum))
                    {
                        if (strIO == project.AiDefine)
                            partIoCount[plcNum - 1].AI_COUNT++;
                        else if (strIO == project.AoDefine)
                            partIoCount[plcNum - 1].AO_COUNT++;
                        else if (strIO == project.DiDefine)
                            partIoCount[plcNum - 1].DI_COUNT++;
                        else if (strIO == project.DoDefine)
                            partIoCount[plcNum - 1].DO_COUNT++;
                    }

                    returnCount = 0;
                }                
                partIoCountList.Add(partIoCount);

                SavePid(_ws, setPid.GetEnumerator());
            }
            catch (Exception ex)
            {
                SIDS.Instance.WriteErrorLog(ex.ToString());
            }
        }

        private void SavePid(Worksheet ws, HashSet<string>.Enumerator enumerator)
        {
            string connectString = SIDS.Instance.MakeConnectionString("DB");
            try
            {
                using (SqlConnection conn = new SqlConnection(connectString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("dbo.proc_insert_pid_page", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        while (enumerator.MoveNext())
                        {
                            string pageName = enumerator.Current;
                            cmd.Parameters.Add("@projId", SqlDbType.Int).Value = project.ProjID;
                            cmd.Parameters.Add("@partName", SqlDbType.NVarChar).Value = ws.Name;
                            cmd.Parameters.Add("@pageName", SqlDbType.NVarChar).Value = pageName;

                            cmd.ExecuteNonQuery();

                            cmd.Parameters.Clear();
                        }
                    }
                }
            }catch(Exception ex)
            {
                throw ex;
            }

        }

        private int SearchColumnIndex(Worksheet _ws, string _findStr, out int _result)
        {
            int rowResult = 0;

            _result = -1;

            try
            {
                SearchOptions opt = new SearchOptions();
                opt.SearchBy = SearchBy.Rows;
                opt.SearchIn = SearchIn.Values;
                opt.MatchEntireCellContents = true;

                IEnumerable<Cell> result = _ws.Search(_findStr, opt);

                foreach (Cell c in result)
                {
                    rowResult = c.RowIndex;
                    _result = c.ColumnIndex;
                    break;
                }
            }
            catch (Exception ex)
            {
                SIDS.Instance.WriteErrorLog(ex.ToString());
            }

            return rowResult;
        }
        // ========== Excel

        private void GatheringIoCount()
        {
            foreach (var v in partIoCountList)
            {
                if (v.PART_NAME == "INST")
                {
                    DoGathering("INST", v, dtInst, gridInst);
                }

                if (v.PART_NAME == "PKG")
                {
                    DoGathering("PKG", v, dtPkg, gridPkg);
                }

                if (v.PART_NAME == "MCC")
                {
                    DoGathering("MCC", v, dtMcc, gridMcc);
                }

                if (v.PART_NAME == "공조제어")
                {
                    DoGathering("HVAC", v, dtHvac, gridHvac);
                }
            }

        }

        private void DoGathering(string part, PartIoCount v, DataTable dt, DevExpress.XtraGrid.GridControl grid)
        {
            int aiSum = 0, aoSum = 0, diSum = 0, doSum = 0;

            for (int i = 0; i < project.PlcCount; i++)
            {
                // MessageBox.Show(v.ToString());
                DataRow row = dt.NewRow();
                row["PART"] = part;
                row["PLC"] = "PLC" + (i + 1).ToString();
                row["AI"] = v[i].AI_COUNT;
                row["AO"] = v[i].AO_COUNT;
                row["DI"] = v[i].DI_COUNT;
                row["DO"] = v[i].DO_COUNT;

                aiSum += v[i].AI_COUNT;
                aoSum += v[i].AO_COUNT;
                diSum += v[i].DI_COUNT;
                doSum += v[i].DO_COUNT;

                dt.Rows.Add(row);
            }

            DataRow instRow = dt.NewRow();
            instRow["PART"] = part;
            instRow["PLC"] = "합계 :";
            instRow["AI"] = aiSum;
            instRow["AO"] = aoSum;
            instRow["DI"] = diSum;
            instRow["DO"] = doSum;
            dt.Rows.Add(instRow);
            grid.DataSource = dt;
        }

        private void FormCreateSystemIO_Load(object sender, EventArgs e)
        {
            chkInst.CheckedChanged += ChkInst_CheckedChanged;
            chkPkg.CheckedChanged += ChkInst_CheckedChanged;
            chkMcc.CheckedChanged += ChkInst_CheckedChanged;
            chkHvac.CheckedChanged += ChkInst_CheckedChanged;

            cmbDetailIo.SelectedValueChanged += CmbDetailIo_SelectedValueChanged;
        }

        private void CmbDetailIo_SelectedValueChanged(object sender, EventArgs e)
        {
            string selected = cmbDetailIo.Text;
            SelectDetailIoList(project.ProjID, selected);
        }

        private void ChkInst_CheckedChanged(object sender, EventArgs e)
        {
            lblTitle.Text = "";

            List<string> lists = new List<string>();

            if (chkInst.Checked) lists.Add("INST");
            if (chkPkg.Checked) lists.Add("PKG");
            if (chkMcc.Checked) lists.Add("MCC");
            if (chkHvac.Checked) lists.Add("HVAC");

            foreach (var title in lists)
            {
                lblTitle.Text += title + " ";
            }

            GetSumIoCount();
        }

        /* private void GetSumIoCount()
         {
             if (project == null) return;

             //int[,] aiSum = new int[project.PlcCount, 1];
             //int[,] aoSum = new int[project.PlcCount, 1];
             //int[,] diSum = new int[project.PlcCount, 1];
             //int[,] doSum = new int[project.PlcCount, 1];
             int aiTotal = 0, aoTotal = 0, diTotal = 0, doTotal = 0;

             DataRow[] instArr = dtInst.Select();
             DataRow[] pkgArr = dtPkg.Select();
             DataRow[] mccArr = dtMcc.Select();
             DataRow[] hvacArr = dtHvac.Select();
             DataRow[] tempArr = dtTempSum.Select();


             for (int i = 0; i < project.PlcCount; i++)  //cpu  수량만큼  
             {
                 tempArr[i]["AI"] = 0;
                 tempArr[i]["AO"] = 0;
                 tempArr[i]["DI"] = 0;
                 tempArr[i]["DO"] = 0;
             }


             if (chkInst.Checked)
             {
                 if (instArr.Length < 1) return;

                 for (int i = 0; i < project.PlcCount; i++)  //cpu  수량 만큼  
                 {
                     tempArr[i]["AI"] = int.Parse(tempArr[i]["AI"].ToString()) + int.Parse(instArr[i]["AI"].ToString()); aiTotal += int.Parse(instArr[i]["AI"].ToString());
                     tempArr[i]["AO"] = int.Parse(tempArr[i]["AO"].ToString()) + int.Parse(instArr[i]["AO"].ToString()); aoTotal += int.Parse(instArr[i]["AO"].ToString());
                     tempArr[i]["DI"] = int.Parse(tempArr[i]["DI"].ToString()) + int.Parse(instArr[i]["DI"].ToString()); diTotal += int.Parse(instArr[i]["DI"].ToString());
                     tempArr[i]["DO"] = int.Parse(tempArr[i]["DO"].ToString()) + int.Parse(instArr[i]["DO"].ToString()); doTotal += int.Parse(instArr[i]["DO"].ToString());

                 }
             }

             if (chkPkg.Checked)
             {
                 if (pkgArr.Length < 1) return;
                 for (int i = 0; i < project.PlcCount; i++)  //cpu  수량 만큼  
                 {
                     tempArr[i]["AI"] = int.Parse(tempArr[i]["AI"].ToString()) + int.Parse(pkgArr[i]["AI"].ToString()); aiTotal += int.Parse(pkgArr[i]["AI"].ToString());
                     tempArr[i]["AO"] = int.Parse(tempArr[i]["AO"].ToString()) + int.Parse(pkgArr[i]["AO"].ToString()); aoTotal += int.Parse(pkgArr[i]["AO"].ToString());
                     tempArr[i]["DI"] = int.Parse(tempArr[i]["DI"].ToString()) + int.Parse(pkgArr[i]["DI"].ToString()); diTotal += int.Parse(pkgArr[i]["DI"].ToString());
                     tempArr[i]["DO"] = int.Parse(tempArr[i]["DO"].ToString()) + int.Parse(pkgArr[i]["DO"].ToString()); doTotal += int.Parse(pkgArr[i]["DO"].ToString());

                 }
             }

             if (chkMcc.Checked)
             {
                 if (mccArr.Length < 1) return;
                 for (int i = 0; i < project.PlcCount; i++)  //cpu  수량 만큼  
                 {
                     tempArr[i]["AI"] = int.Parse(tempArr[i]["AI"].ToString()) + int.Parse(mccArr[i]["AI"].ToString()); aiTotal += int.Parse(mccArr[i]["AI"].ToString());
                     tempArr[i]["AO"] = int.Parse(tempArr[i]["AO"].ToString()) + int.Parse(mccArr[i]["AO"].ToString()); aoTotal += int.Parse(mccArr[i]["AO"].ToString());
                     tempArr[i]["DI"] = int.Parse(tempArr[i]["DI"].ToString()) + int.Parse(mccArr[i]["DI"].ToString()); diTotal += int.Parse(mccArr[i]["DI"].ToString());
                     tempArr[i]["DO"] = int.Parse(tempArr[i]["DO"].ToString()) + int.Parse(mccArr[i]["DO"].ToString()); doTotal += int.Parse(mccArr[i]["DO"].ToString());
                 }
             }

             if (chkHvac.Checked)
             {
                 if (hvacArr.Length < 1) return;
                 for (int i = 0; i < project.PlcCount; i++)  //cpu  수량 만큼  
                 {
                     tempArr[i]["AI"] = int.Parse(tempArr[i]["AI"].ToString()) + int.Parse(hvacArr[i]["AI"].ToString()); aiTotal += int.Parse(hvacArr[i]["AI"].ToString());
                     tempArr[i]["AO"] = int.Parse(tempArr[i]["AO"].ToString()) + int.Parse(hvacArr[i]["AO"].ToString()); aoTotal += int.Parse(hvacArr[i]["AO"].ToString());
                     tempArr[i]["DI"] = int.Parse(tempArr[i]["DI"].ToString()) + int.Parse(hvacArr[i]["DI"].ToString()); diTotal += int.Parse(hvacArr[i]["DI"].ToString());
                     tempArr[i]["DO"] = int.Parse(tempArr[i]["DO"].ToString()) + int.Parse(hvacArr[i]["DO"].ToString()); doTotal += int.Parse(hvacArr[i]["DO"].ToString());
                 }
             }

             //합계
             tempArr[project.PlcCount]["AI"] = aiTotal;
             tempArr[project.PlcCount]["AO"] = aoTotal;
             tempArr[project.PlcCount]["DI"] = diTotal;
             tempArr[project.PlcCount]["DO"] = doTotal;


             //스페어 
             tempArr[project.PlcCount + 1]["AI"] = (int)Math.Ceiling(aiTotal * 0.3);
             tempArr[project.PlcCount + 1]["AO"] = (int)Math.Ceiling(aoTotal * 0.3);
             tempArr[project.PlcCount + 1]["DI"] = (int)Math.Ceiling(diTotal * 0.3);
             tempArr[project.PlcCount + 1]["DO"] = (int)Math.Ceiling(doTotal * 0.3);

             //Total
             tempArr[project.PlcCount + 2]["AI"] = (int)Math.Ceiling(aiTotal + (aiTotal * 0.3));
             tempArr[project.PlcCount + 2]["AO"] = (int)Math.Ceiling(aoTotal + (aoTotal * 0.3));
             tempArr[project.PlcCount + 2]["DI"] = (int)Math.Ceiling(diTotal + (diTotal * 0.3));
             tempArr[project.PlcCount + 2]["DO"] = (int)Math.Ceiling(doTotal + (doTotal * 0.3));

             //module
             tempArr[project.PlcCount + 3]["AI"] = (int)Math.Ceiling((aiTotal + (aiTotal * 0.3)) / project.AiChannel);
             tempArr[project.PlcCount + 3]["AO"] = (int)Math.Ceiling((aoTotal + (aoTotal * 0.3)) / project.AoChannel);
             tempArr[project.PlcCount + 3]["DI"] = (int)Math.Ceiling((diTotal + (diTotal * 0.3)) / project.DiChannel);
             tempArr[project.PlcCount + 3]["DO"] = (int)Math.Ceiling((doTotal + (doTotal * 0.3)) / project.DoChannel);

             gridTemp.DataSource = dtTempSum;
         }*/

        private void GetSumIoCount()
        {
            if (project == null) return;
           
            DataRow[] instArr = dtInst.Select();
            DataRow[] pkgArr = dtPkg.Select();
            DataRow[] mccArr = dtMcc.Select();
            DataRow[] hvacArr = dtHvac.Select();
            DataRow[] tempArr = dtTempSum.Select();

            int j = 0;
            int aiTotalModule = 0, aoTotalModule = 0, diTotalModule = 0, doTotalModule = 0;
            

            for (int i = 0; i < project.PlcCount; i++)  //cpu  수량만큼  
            {
                tempArr[i]["AI"] = 0;
                tempArr[i]["AO"] = 0;
                tempArr[i]["DI"] = 0;
                tempArr[i]["DO"] = 0;
            }           

            for (int i = 0; i < project.PlcCount; i++)
            {
                int aiTotal = 0, aoTotal = 0, diTotal = 0, doTotal = 0;
                tempArr[j]["AI"] = 0;
                tempArr[j]["AO"] = 0;
                tempArr[j]["DI"] = 0;
                tempArr[j]["DO"] = 0;

                if (chkInst.Checked)
                {
                    if (instArr.Length < 1) return;
                    tempArr[j]["AI"] = int.Parse(tempArr[j]["AI"].ToString()) + int.Parse(instArr[i]["AI"].ToString()); aiTotal += int.Parse(instArr[i]["AI"].ToString());
                    tempArr[j]["AO"] = int.Parse(tempArr[j]["AO"].ToString()) + int.Parse(instArr[i]["AO"].ToString()); aoTotal += int.Parse(instArr[i]["AO"].ToString());
                    tempArr[j]["DI"] = int.Parse(tempArr[j]["DI"].ToString()) + int.Parse(instArr[i]["DI"].ToString()); diTotal += int.Parse(instArr[i]["DI"].ToString());
                    tempArr[j]["DO"] = int.Parse(tempArr[j]["DO"].ToString()) + int.Parse(instArr[i]["DO"].ToString()); doTotal += int.Parse(instArr[i]["DO"].ToString());
                }

                if (chkPkg.Checked)
                {
                    if (pkgArr.Length < 1) return;
                    tempArr[j]["AI"] = int.Parse(tempArr[j]["AI"].ToString()) + int.Parse(pkgArr[i]["AI"].ToString()); aiTotal += int.Parse(pkgArr[i]["AI"].ToString());
                    tempArr[j]["AO"] = int.Parse(tempArr[j]["AO"].ToString()) + int.Parse(pkgArr[i]["AO"].ToString()); aoTotal += int.Parse(pkgArr[i]["AO"].ToString());
                    tempArr[j]["DI"] = int.Parse(tempArr[j]["DI"].ToString()) + int.Parse(pkgArr[i]["DI"].ToString()); diTotal += int.Parse(pkgArr[i]["DI"].ToString());
                    tempArr[j]["DO"] = int.Parse(tempArr[j]["DO"].ToString()) + int.Parse(pkgArr[i]["DO"].ToString()); doTotal += int.Parse(pkgArr[i]["DO"].ToString());
                }

                if (chkMcc.Checked)
                {
                    if (mccArr.Length < 1) return;
                    tempArr[j]["AI"] = int.Parse(tempArr[j]["AI"].ToString()) + int.Parse(mccArr[i]["AI"].ToString()); aiTotal += int.Parse(mccArr[i]["AI"].ToString());
                    tempArr[j]["AO"] = int.Parse(tempArr[j]["AO"].ToString()) + int.Parse(mccArr[i]["AO"].ToString()); aoTotal += int.Parse(mccArr[i]["AO"].ToString());
                    tempArr[j]["DI"] = int.Parse(tempArr[j]["DI"].ToString()) + int.Parse(mccArr[i]["DI"].ToString()); diTotal += int.Parse(mccArr[i]["DI"].ToString());
                    tempArr[j]["DO"] = int.Parse(tempArr[j]["DO"].ToString()) + int.Parse(mccArr[i]["DO"].ToString()); doTotal += int.Parse(mccArr[i]["DO"].ToString());
                }

                if (chkHvac.Checked)
                {
                    if (hvacArr.Length < 1) return;
                    tempArr[j]["AI"] = int.Parse(tempArr[j]["AI"].ToString()) + int.Parse(hvacArr[i]["AI"].ToString()); aiTotal += int.Parse(hvacArr[i]["AI"].ToString());
                    tempArr[j]["AO"] = int.Parse(tempArr[j]["AO"].ToString()) + int.Parse(hvacArr[i]["AO"].ToString()); aoTotal += int.Parse(hvacArr[i]["AO"].ToString());
                    tempArr[j]["DI"] = int.Parse(tempArr[j]["DI"].ToString()) + int.Parse(hvacArr[i]["DI"].ToString()); diTotal += int.Parse(hvacArr[i]["DI"].ToString());
                    tempArr[j]["DO"] = int.Parse(tempArr[j]["DO"].ToString()) + int.Parse(hvacArr[i]["DO"].ToString()); doTotal += int.Parse(hvacArr[i]["DO"].ToString());
                }


                //합계
                //tempArr[++j]["AI"] = aiTotal;
                //tempArr[j]["AO"] = aoTotal;
                //tempArr[j++]["DI"] = diTotal;
                //tempArr[j++]["DO"] = doTotal;

                //스페어 
                tempArr[++j]["AI"] = (int)Math.Ceiling(aiTotal * 0.3);
                tempArr[j]["AO"] = (int)Math.Ceiling(aoTotal * 0.3);
                tempArr[j]["DI"] = (int)Math.Ceiling(diTotal * 0.3);
                tempArr[j]["DO"] = (int)Math.Ceiling(doTotal * 0.3);

                //Total
                tempArr[++j]["AI"] = (int)Math.Ceiling(aiTotal + (aiTotal * 0.3)); 
                tempArr[j]["AO"] = (int)Math.Ceiling(aoTotal + (aoTotal * 0.3)); 
                tempArr[j]["DI"] = (int)Math.Ceiling(diTotal + (diTotal * 0.3)); 
                tempArr[j]["DO"] = (int)Math.Ceiling(doTotal + (doTotal * 0.3)); 

                //module
                tempArr[++j]["AI"] = (int)Math.Ceiling((aiTotal + (aiTotal * 0.3)) / project.AiChannel); aiTotalModule += (int)Math.Ceiling((aiTotal + (aiTotal * 0.3)) / project.AiChannel);
                tempArr[j]["AO"] = (int)Math.Ceiling((aoTotal + (aoTotal * 0.3)) / project.AoChannel); aoTotalModule += (int)Math.Ceiling((aoTotal + (aoTotal * 0.3)) / project.AoChannel);
                tempArr[j]["DI"] = (int)Math.Ceiling((diTotal + (diTotal * 0.3)) / project.DiChannel); diTotalModule += (int)Math.Ceiling((diTotal + (diTotal * 0.3)) / project.DiChannel);
                tempArr[j]["DO"] = (int)Math.Ceiling((doTotal + (doTotal * 0.3)) / project.DoChannel); doTotalModule += (int)Math.Ceiling((doTotal + (doTotal * 0.3)) / project.DoChannel);

                j++;
            }

            tempArr[j]["AI"] = aiTotalModule;
            tempArr[j]["AO"] = aoTotalModule;
            tempArr[j]["DI"] = diTotalModule;
            tempArr[j]["DO"] = doTotalModule;

            gridTemp.DataSource = dtTempSum;
        }
    

        private void CreateDataTable()
        {        
            DataTable[] dts = { dtInst, dtPkg, dtMcc, dtHvac };

            foreach(var dt in dts)
            {
                DataColumn column1 = new DataColumn("PART", typeof(string));
                DataColumn column2 = new DataColumn("PLC", typeof(string));
                DataColumn column3 = new DataColumn("AI", typeof(int));
                DataColumn column4 = new DataColumn("AO", typeof(int));
                DataColumn column5 = new DataColumn("DI", typeof(int));
                DataColumn column6 = new DataColumn("DO", typeof(int));

                dt.Columns.Add(column1);
                dt.Columns.Add(column2);
                dt.Columns.Add(column3);
                dt.Columns.Add(column4);
                dt.Columns.Add(column5);
                dt.Columns.Add(column6);
            }

            {// 합계를 위한 임시 DataTable 
                DataColumn column1 = new DataColumn("TITLE", typeof(string));
                DataColumn column2 = new DataColumn("AI", typeof(int));
                DataColumn column3 = new DataColumn("AO", typeof(int));
                DataColumn column4 = new DataColumn("DI", typeof(int));
                DataColumn column5 = new DataColumn("DO", typeof(int));

                dtTempSum.Columns.Add(column1);
                dtTempSum.Columns.Add(column2);
                dtTempSum.Columns.Add(column3);
                dtTempSum.Columns.Add(column4);
                dtTempSum.Columns.Add(column5);

                for (int i = 0; i < project.PlcCount; i++)
                {
                    {
                        DataRow row = dtTempSum.NewRow();
                        row["TITLE"] = "PLC" + (i + 1).ToString();
                        row["AI"] = 0;
                        row["AO"] = 0;
                        row["DI"] = 0;
                        row["DO"] = 0;
                        dtTempSum.Rows.Add(row);
                    }
                   
                    {
                        DataRow row = dtTempSum.NewRow();
                        row["TITLE"] = "SPARE";
                        row["AI"] = 0;
                        row["AO"] = 0;
                        row["DI"] = 0;
                        row["DO"] = 0;
                        dtTempSum.Rows.Add(row);
                    }
                    {
                        DataRow row = dtTempSum.NewRow();
                        row["TITLE"] = "TOTAL";
                        row["AI"] = 0;
                        row["AO"] = 0;
                        row["DI"] = 0;
                        row["DO"] = 0;
                        dtTempSum.Rows.Add(row);
                    }

                    {
                        DataRow row = dtTempSum.NewRow();
                        row["TITLE"] = "MODULE";
                        row["AI"] = 0;
                        row["AO"] = 0;
                        row["DI"] = 0;
                        row["DO"] = 0;
                        dtTempSum.Rows.Add(row);
                    }                    
                }

                {
                    DataRow row = dtTempSum.NewRow();
                    row["TITLE"] = "TOTAL MODULE";
                    row["AI"] = 0;
                    row["AO"] = 0;
                    row["DI"] = 0;
                    row["DO"] = 0;
                    dtTempSum.Rows.Add(row);
                }
            }

            {// 저장된 IO 불러오기 위한 
                DataColumn column1 = new DataColumn("TITLE", typeof(string));
                DataColumn column2 = new DataColumn("AI", typeof(int));
                DataColumn column3 = new DataColumn("AO", typeof(int));
                DataColumn column4 = new DataColumn("DI", typeof(int));
                DataColumn column5 = new DataColumn("DO", typeof(int));

                dtSaved.Columns.Add(column1);
                dtSaved.Columns.Add(column2);
                dtSaved.Columns.Add(column3);
                dtSaved.Columns.Add(column4);
                dtSaved.Columns.Add(column5);

                for (int i = 0; i < project.PlcCount; i++)
                {
                    {
                        DataRow row = dtSaved.NewRow();
                        row["TITLE"] = "PLC" + (i + 1).ToString();
                        row["AI"] = 0;
                        row["AO"] = 0;
                        row["DI"] = 0;
                        row["DO"] = 0;
                        dtSaved.Rows.Add(row);
                    }                   
                    {
                        DataRow row = dtSaved.NewRow();
                        row["TITLE"] = "SPARE";
                        row["AI"] = 0;
                        row["AO"] = 0;
                        row["DI"] = 0;
                        row["DO"] = 0;
                        dtSaved.Rows.Add(row);
                    }
                    {
                        DataRow row = dtSaved.NewRow();
                        row["TITLE"] = "TOTAL";
                        row["AI"] = 0;
                        row["AO"] = 0;
                        row["DI"] = 0;
                        row["DO"] = 0;
                        dtSaved.Rows.Add(row);
                    }
                    {
                        DataRow row = dtSaved.NewRow();
                        row["TITLE"] = "MODULE";
                        row["AI"] = 0;
                        row["AO"] = 0;
                        row["DI"] = 0;
                        row["DO"] = 0;
                        dtSaved.Rows.Add(row);
                    }                    
                }

                {
                    DataRow row = dtSaved.NewRow();
                    row["TITLE"] = "TOTAL MODULE";
                    row["AI"] = 0;
                    row["AO"] = 0;
                    row["DI"] = 0;
                    row["DO"] = 0;
                    dtSaved.Rows.Add(row);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (gridTemp.DataSource == null) return;
            string mainTitle = lblTitle.Text.Trim();
            if (mainTitle == "") return;

            bool isExistDetailIo = checkExistDetailIo(project.ProjID, mainTitle);

            if (isExistDetailIo)
            {
                MessageBox.Show($"{mainTitle}이 이미 존재합니다? 삭제 후 진행 바랍니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show($"{mainTitle}를 저장 하시겠습니까?", "Save", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
            if (result == DialogResult.Cancel) return;

            string connectString = SIDS.Instance.MakeConnectionString("DB");
            using(SqlConnection DBConn = new SqlConnection(connectString))
            {
                string query = @"INSERT INTO project_detail_io(project_id, main_title, sub_title, ai_count, ao_count, di_count, do_count)
                                VALUES(@proj_id, @main_title, @sub_title, @ai_count, @ao_count, @di_count, @do_count)";

                DataRow[] rows = dtTempSum.Select();

                foreach(var row in rows)
                {
                    using (SqlCommand cmd = new SqlCommand(query, DBConn))
                    {
                        cmd.Parameters.Add("@proj_id", SqlDbType.Int).Value = project.ProjID;
                        cmd.Parameters.Add("@main_title", SqlDbType.NVarChar).Value = lblTitle.Text.Trim();
                        cmd.Parameters.Add("@sub_title", SqlDbType.NVarChar).Value = row["TITLE"].ToString();
                        cmd.Parameters.Add("@ai_count", SqlDbType.Int).Value = int.Parse(row["AI"].ToString());
                        cmd.Parameters.Add("@ao_count", SqlDbType.Int).Value = int.Parse(row["AO"].ToString());
                        cmd.Parameters.Add("@di_count", SqlDbType.Int).Value = int.Parse(row["DI"].ToString());
                        cmd.Parameters.Add("@do_count", SqlDbType.Int).Value = int.Parse(row["DO"].ToString());

                        try
                        {
                            DBConn.Open();
                            cmd.ExecuteNonQuery();
                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                        finally
                        {
                            DBConn.Close();
                        }
                    }
                }                
            }

            gridTemp.DataSource = null;

            dtSaved = dtTempSum.Copy();
            gridSaved.DataSource = dtSaved;
            SelectDetailIoTitle(project.ProjID);
            cmbDetailIo.Text = mainTitle;
            lblTitle.Text = "";
            chkInst.Checked = false;
            chkPkg.Checked = false;
            chkMcc.Checked = false;
            chkHvac.Checked = false;
            MessageBox.Show($"{mainTitle} 저장이 완료 되었습니다.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private bool checkExistDetailIo(int projId, string mainTitle)
        {
            int count = 0;
            string connectString = SIDS.Instance.MakeConnectionString("DB");
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                string query = "SELECT COUNT(project_id) FROM project_detail_io WHERE project_id = @proj_id and main_title = @main_title";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add("@proj_id", SqlDbType.Int).Value = projId;
                    cmd.Parameters.Add("@main_title", SqlDbType.NVarChar).Value = mainTitle;

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        int.TryParse(reader[0].ToString(), out count);
                    }
                }
            }

            return count > 0;
        }

        private void DeleteDetailIoList(int projId, string mainTitle)
        {
            DialogResult result = MessageBox.Show($"{mainTitle}를 삭제 하시겠습니까?", "Remove", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Cancel) return;

            string connectString = SIDS.Instance.MakeConnectionString("DB");
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                string query = "DELETE project_detail_io WHERE project_id = @proj_id and main_title = @main_title";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add("@proj_id", SqlDbType.Int).Value = projId;
                    cmd.Parameters.Add("@main_title", SqlDbType.NVarChar).Value = mainTitle;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }            
        }

        private void SelectDetailIoTitle(int projId)
        {

            cmbDetailIo.Properties.Items.Clear();
            string connectString = SIDS.Instance.MakeConnectionString("DB");
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                string query = "SELECT distinct main_title FROM project_detail_io WHERE project_id = @proj_id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add("@proj_id", SqlDbType.Int).Value = projId;

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        cmbDetailIo.Properties.Items.Add(reader[0].ToString());
                    }
                }
            }
            if(cmbDetailIo.Properties.Items.Count > 0)
            {
                cmbDetailIo.SelectedIndex = 0;
            }
        }

        private void SelectDetailIoTitle(int projId, string mainTitle)
        {
            cmbDetailIo.Properties.Items.Clear();
            string connectString = SIDS.Instance.MakeConnectionString("DB");
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                string query = "SELECT distinct main_title FROM project_detail_io WHERE project_id = @proj_id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add("@proj_id", SqlDbType.Int).Value = projId;

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        cmbDetailIo.Properties.Items.Add(reader[0].ToString());
                    }
                }
            }
            cmbDetailIo.Text = mainTitle;
        }

        private void SelectDetailIoList(int projId, string mainTitle)
        {
            dtSaved.Rows.Clear();

            string connectString = SIDS.Instance.MakeConnectionString("DB");
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                string query = "SELECT sub_title, ai_count, ao_count, di_count, do_count FROM project_detail_io WHERE project_id = @proj_id and main_title = @main_title" ;
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add("@proj_id", SqlDbType.Int).Value = projId;
                    cmd.Parameters.Add("@main_title", SqlDbType.NVarChar).Value = mainTitle;

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        DataRow row = dtSaved.NewRow();
                        row["TITLE"] = reader["sub_title"].ToString();
                        row["AI"] = int.Parse(reader["ai_count"].ToString());
                        row["AO"] = int.Parse(reader["ao_count"].ToString());
                        row["DI"] = int.Parse(reader["di_count"].ToString());
                        row["DO"] = int.Parse(reader["do_count"].ToString());
                        dtSaved.Rows.Add(row);
                    }
                }
            }
            gridSaved.DataSource = dtSaved;

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string mainTitle = cmbDetailIo.Text.Trim();
            DeleteDetailIoList(project.ProjID, mainTitle);
            SelectDetailIoTitle(project.ProjID);
        }
    }
}