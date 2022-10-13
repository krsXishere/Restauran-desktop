using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Restaurant.Data;

namespace Restaurant.UC
{
    public partial class PemasukanUC : UserControl
    {
        public PemasukanUC()
        {
            InitializeComponent();
        }

        public void PemasukanUC_Load(object sender, EventArgs e)
        {
            LaporanGrafikPenjualanDataContext lgpdc = new LaporanGrafikPenjualanDataContext();
            chart1.DataSource = lgpdc.LaporanGrafikPendapatans.ToList();
            chart1.Series["Pendapatan"].XValueMember = "Tahun";
            chart1.Series["Pendapatan"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
            chart1.Series["Pendapatan"].YValueMembers = "Pendapatan";
            chart1.Series["Pendapatan"].YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
        }
    }
}
