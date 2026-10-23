using System;
using System.Windows.Forms;

namespace Lab05_MultiForm
{
    public partial class frmMain : Form
    {
        private string nguoiDungHienTai = "";

        public frmMain()
        {
            InitializeComponent();
            this.IsMdiContainer = true;
        }

        // Tự động bật Form Đăng nhập ngay khi Form Main vừa xuất hiện
        private void frmMain_Load(object sender, EventArgs e)
        {
            CapNhatTrangThaiMenu(false);
            HienThiFormDangNhap();
        }

        #region HÀM HỖ TRỢ XỬ LÝ MDI
        // Kiểm tra tránh mở trùng lặp nhiều cửa sổ con cùng loại
        private bool KiemTraFormDaMo(string formName)
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == formName)
                {
                    f.Activate(); // Nếu đã mở thì kích hoạt đưa lên trên
                    return true;
                }
            }
            return false;
        }

        // Cập nhật ẩn/hiện các menu theo trạng thái đăng nhập
        private void CapNhatTrangThaiMenu(bool daDangNhap)
        {
            if (menuDangNhap != null) menuDangNhap.Enabled = !daDangNhap;
            if (menuDangXuat != null) menuDangXuat.Enabled = daDangNhap;
            if (menuQuanLySV != null) menuQuanLySV.Enabled = daDangNhap;

            if (lblTrangThaiMDI != null)
            {
                if (daDangNhap)
                {
                    lblTrangThaiMDI.Text = $"Trạng thái: Đã đăng nhập | Tài khoản: {nguoiDungHienTai}";
                }
                else
                {
                    lblTrangThaiMDI.Text = "Trạng thái: Chưa đăng nhập";
                }
            }
        }

        private void HienThiFormDangNhap()
        {
            frmDangNhap fLogin = new frmDangNhap();
            if (fLogin.ShowDialog() == DialogResult.OK)
            {
                nguoiDungHienTai = "admin";
                CapNhatTrangThaiMenu(true);
                MoFormQuanLySinhVien(); // Đăng nhập xong tự mở Form1
            }
        }

        private void MoFormQuanLySinhVien()
        {
            if (!KiemTraFormDaMo("Form1"))
            {
                Form1 fSV = new Form1();
                fSV.MdiParent = this; // Đặt frmMain làm cửa sổ mẹ
                fSV.Show();
            }
        }
        #endregion

        #region XỬ LÝ SỰ KIỆN MENU
        private void menuDangNhap_Click(object sender, EventArgs e)
        {
            HienThiFormDangNhap();
        }

        private void menuDangXuat_Click(object sender, EventArgs e)
        {
            // Đóng tất cả Form con đang mở trong MDI
            foreach (Form child in this.MdiChildren)
            {
                child.Close();
            }

            nguoiDungHienTai = "";
            CapNhatTrangThaiMenu(false);
            MessageBox.Show("Đã đăng xuất thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void menuThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void menuQuanLySV_Click(object sender, EventArgs e)
        {
            MoFormQuanLySinhVien();
        }

        // Sắp xếp các cửa sổ con
        private void menuCascade_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.Cascade);
        }

        private void menuHorizontal_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void menuVertical_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileVertical);
        }
        #endregion

        #region SỰ KIỆN TRỐNG GIỮ NGUYÊN ĐỂ TRÁNH LỖI DESIGNER
        private void toolStripStatusLabel1_Click(object sender, EventArgs e) { }
        private void lblTrangThaiMDI_Click(object sender, EventArgs e) { }
        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e) { }
        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e) { }
        #endregion
    }
}