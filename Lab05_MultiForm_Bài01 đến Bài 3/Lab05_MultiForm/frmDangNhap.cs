using System;
using System.Drawing;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Lab05_MultiForm
{
    public partial class frmDangNhap : Form
    {
        private int soLanSai = 0;
        private int thoiGianKhoa = 30;

        public frmDangNhap()
        {
            InitializeComponent();
        }

        private void frmDangNhap_Load(object sender, EventArgs e)
        {
            lblThongBao.Text = "";
            timerLock.Interval = 1000;
        }

        // Sự kiện khi nhấn nút "Đăng nhập"
        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string tk = txtTaiKhoan.Text.Trim();
            string mk = txtMatKhau.Text.Trim(); // Lấy mật khẩu từ ô nhập liệu

            // Kiểm tra tài khoản mẫu (admin / 123456)
            if (tk == "admin" && mk == "123456")
            {
                MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                soLanSai++;
                int soLanConLai = 3 - soLanSai;

                if (soLanSai >= 3)
                {
                    btnDangNhap.Enabled = false;
                    thoiGianKhoa = 30;
                    lblThongBao.Text = $"Bạn đã nhập sai 3 lần! Khóa đăng nhập trong {thoiGianKhoa}s";
                    lblThongBao.ForeColor = Color.Red;

                    // Kích hoạt Timer đếm ngược 30 giây
                    timerLock.Start();
                }
                else
                {
                    lblThongBao.Text = $"Sai tài khoản hoặc mật khẩu! Còn {soLanConLai} lần thử.";
                    lblThongBao.ForeColor = Color.OrangeRed;
                }
            }
        }

        // Xử lý đếm ngược 30 giây
        private void timerLock_Tick(object sender, EventArgs e)
        {
            thoiGianKhoa--;
            if (thoiGianKhoa > 0)
            {
                lblThongBao.Text = $"Bạn đã nhập sai 3 lần! Khóa đăng nhập trong {thoiGianKhoa}s";
            }
            else
            {
                timerLock.Stop();
                soLanSai = 0;
                btnDangNhap.Enabled = true;
                lblThongBao.Text = "Bạn có thể thử đăng nhập lại.";
                lblThongBao.ForeColor = Color.Green;
            }
        }

        // Đồng bộ nếu Visual Studio Designer tự gắn tên sự kiện là timer1_Tick
        private void timer1_Tick(object sender, EventArgs e)
        {
            timerLock_Tick(sender, e);
        }

        // Sự kiện khi nhấn nút "Hủy"
        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        #region SỰ KIỆN TRỐNG GIỮ NGUYÊN ĐỂ TRÁNH LỖI DESIGNER
        private void txtTaiKhoan_TextChanged(object sender, EventArgs e) { }
        private void textBox2_TextChanged(object sender, EventArgs e) { }
        private void lblThongBao_Click(object sender, EventArgs e) { }
        #endregion
    }
}