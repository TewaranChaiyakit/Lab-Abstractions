﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library_System_Management
{
    public partial class Form4 : Form
    {
        int selectedRow;
        public Form4()
        {
            InitializeComponent();
        }

        private void btCheakIn_Click(object sender, EventArgs e)
        {
            int n = dataGridViewMember.Rows.Add();
            dataGridViewMember.Rows[n].Cells[0].Value = tbName.Text;
            dataGridViewMember.Rows[n].Cells[1].Value = tbID.Text;
            dataGridViewMember.Rows[n].Cells[2].Value = tbMajor.Text;

            if (rdStudent.Checked)
                dataGridViewMember.Rows[n].Cells[3].Value = rdStudent.Text;
            if (rdTeacher.Checked)
                dataGridViewMember.Rows[n].Cells[3].Value = rdTeacher.Text;
            if (rdStaft.Checked)
                dataGridViewMember.Rows[n].Cells[3].Value = rdStaft.Text;

            dataGridViewMember.Rows[n].Cells[4].Value = label5.Text + " " + label4.Text;
            dataGridViewMember.Rows[n].Cells[5].Value = "";
        }
        private void btCheckOut_Click(object sender, EventArgs e)
        {
            int n = dataGridViewMember.Rows.Add();
            dataGridViewMember.Rows[n].Cells[0].Value = tbName.Text;
            dataGridViewMember.Rows[n].Cells[1].Value = tbID.Text;
            dataGridViewMember.Rows[n].Cells[2].Value = tbMajor.Text;

            if (rdStudent.Checked)
                dataGridViewMember.Rows[n].Cells[3].Value = rdStudent.Text;
            else if (rdTeacher.Checked)
                dataGridViewMember.Rows[n].Cells[3].Value = rdTeacher.Text;

            dataGridViewMember.Rows[n].Cells[4].Value = "";
            dataGridViewMember.Rows[n].Cells[5].Value = label5.Text + " " + label4.Text;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "CSV(.csv)|*.csv";
            openFile.Title = "Please select file";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                dataGridViewMember.DataSource = null;

                DataTable dt = new DataTable();
                string[] colName = { "ชื่อ","รหัสนักศึกษาหรือรหัสประจำตัวประชาชน","สาขา","สถานะ","เวลาเข้า","เวลาออก" };

                foreach (string col in colName)
                {
                    dt.Columns.Add(col);
                }

                foreach (string file in openFile.FileNames)
                {
                    try
                    {
                        if (File.Exists(file) == true)
                        {
                            //import file data
                            StreamReader csv = new StreamReader(file);
                            string textLine; //string line data
                            string[] splitLine; // use array to save split data
                            

                            do
                            {
                                textLine = csv.ReadLine();
                                splitLine = textLine.Split(",");
                                dt.Rows.Add(splitLine);
                            }
                            while (csv.Peek() != -1);
                            csv.Close();
                            csv.Dispose();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                dataGridViewMember.DataSource = dt;
            }
        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(dataGridViewMember.Rows.Count > 0)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "CSV(*.csv)|*.csv";
                bool fileError = false;
                if(saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if (!fileError)
                    {
                        try
                        {
                            int columnCount = dataGridViewMember.Columns.Count;
                            string columnNames = "";
                            string[] output = new string[dataGridViewMember.Rows.Count + 1];
                            for(int i = 0; i < columnCount; i++)
                            {
                                columnNames += dataGridViewMember.Columns[i].HeaderText.ToString() + ",";
                            }
                            output[0] += columnNames;
                            for (int i = 1; (i - 1) < dataGridViewMember.Rows.Count; i++)
                            {
                                for(int j = 0; j < columnCount; j++)
                                {
                                    output[i] += dataGridViewMember.Rows[i - 1].Cells[j].Value.ToString() + ",";
                                }
                            }
                            File.WriteAllLines(saveFileDialog.FileName, output,Encoding.UTF8);
                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
        }
        private void Form4_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label4.Text = DateTime.Now.ToLongTimeString();
            label5.Text = DateTime.Now.ToShortDateString();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }
    }
}
