using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace HuynhThienBao_6551071008_VP107_LTTQ
{
    public partial class Form1 : Form
    {
       
        private string connStr = "Server=BAO\\SQLEXPRESS01;Database=DuLieu;Trusted_Connection=True;TrustServerCertificate=True;";

        public Form1()
        {
            InitializeComponent();

            this.Load += Form1_Load;
            btnThoat.Click += btnThoat_Click;
            txtSoTinChi.KeyPress += txtSoTinChi_KeyPress;
            dgvMonHoc.CellClick += dgvMonHoc_CellClick;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btnThem.Enabled = true;
            btnThoat.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;

            LoadDataMonHoc();
        }

        private void LoadDataMonHoc()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string sql = "SELECT MaMonHoc, TenMonHoc, SoTinChi FROM MonHoc";
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvMonHoc.DataSource = dt;

                    if (dgvMonHoc.Columns["MaMonHoc"] != null)
                        dgvMonHoc.Columns["MaMonHoc"].HeaderText = "Mã môn học";
                    if (dgvMonHoc.Columns["TenMonHoc"] != null)
                        dgvMonHoc.Columns["TenMonHoc"].HeaderText = "Tên môn học";
                    if (dgvMonHoc.Columns["SoTinChi"] != null)
                        dgvMonHoc.Columns["SoTinChi"].HeaderText = "Số tín chỉ";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối dữ liệu: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvMonHoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvMonHoc.Rows.Count)
            {
                DataGridViewRow row = dgvMonHoc.Rows[e.RowIndex];
                if (row.Cells["MaMonHoc"].Value != null)
                {
                    txtMaMonHoc.Text = row.Cells["MaMonHoc"].Value.ToString();
                    txtTenMonHoc.Text = row.Cells["TenMonHoc"].Value?.ToString();
                    txtSoTinChi.Text = row.Cells["SoTinChi"].Value?.ToString();

                    btnSua.Enabled = true;
                    btnXoa.Enabled = true;
                }
            }
        }

        private void txtSoTinChi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
            {
                return;
            }

            if (char.IsDigit(e.KeyChar))
            {
                return;
            }

            if ((e.KeyChar == '.' || e.KeyChar == ',') &&
                !txtSoTinChi.Text.Contains(".") && !txtSoTinChi.Text.Contains(","))
            {
                return;
            }

            e.Handled = true;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có muốn thoát không?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}