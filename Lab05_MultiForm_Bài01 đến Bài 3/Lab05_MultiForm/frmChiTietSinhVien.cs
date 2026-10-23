using System;
using System.Drawing;
using System.IO;
using System.Reflection.Emit;
using System.Windows.Forms;

namespace Lab05_MultiForm
{
    public partial class frmChiTietSinhVien : Form
    {
        private readonly SinhVien _sv;

        public frmChiTietSinhVien()
        {
            InitializeComponent();
        }

        // Constructor nhận đối tượng SinhVien từ frmSinhVien
        public frmChiTietSinhVien(SinhVien sv) : this()
        {
            _sv = sv;
        }

        private void frmChiTietSinhVien_Load(object sender, EventArgs e)
        {
            if (_sv == null) return;

            this.Text = $"Chi tiết sinh viên: {_sv.MaSV} - {_sv.HoTen}";

            // Kiểm tra tên Control trên Designer và gán tương ứng:
            lblMaSV.Text = "Mã sinh viên: " + _sv.MaSV;

            // Nếu trên Designer đặt tên là lblHoTen, lblNgaySinh... thì dùng tên đó.
            // Nếu trên Designer vẫn để tên mặc định label2, label3... thì giữ nguyên tên trên Designer.
            lblHoTen.Text = "Họ và tên: " + _sv.HoTen;
            lblNgaySinh.Text = $"Ngày sinh: {_sv.NgaySinh:dd/MM/yyyy}";
            lblGioiTinh.Text = "Giới tính: " + (_sv.GioiTinh ? "Nam" : "Nữ");
            lblKhoa.Text = "Khoa: " + _sv.Khoa;

            lblDiemTB.Text = $"Điểm trung bình: {_sv.DiemTB:N2}";
            lblXepLoai.Text = "Xếp loại: " + _sv.XepLoai();

            if (!string.IsNullOrEmpty(_sv.DuongDanAnh) && System.IO.File.Exists(_sv.DuongDanAnh))
            {
                picAnhDetail.ImageLocation = _sv.DuongDanAnh;
            }
        }

        // Sự kiện click nút Đóng
        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region SỰ KIỆN TRỐNG GIỮ NGUYÊN TRÁNH LỖI DESIGNER
        private void lblMaSV_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }
        private void label5_Click(object sender, EventArgs e) { }
        private void lblDiemTB_Click(object sender, EventArgs e) { }
        private void lblXepLoai_Click(object sender, EventArgs e) { }
        private void picAnhDetail_Click(object sender, EventArgs e) { }
        #endregion
    }
}