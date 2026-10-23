using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Lab05_MultiForm
{
    public partial class Form1 : Form
    {
        // Danh sách lưu trữ sinh viên dạng BindingList để tự động cập nhật DataGridView
        private BindingList<SinhVien> dsSinhVien = new BindingList<SinhVien>();

        public Form1()
        {
            InitializeComponent();
            KhoiTaoKhoa(); // Khởi tạo danh sách Khoa ngay khi Form khởi chạy

            this.Load += Form1_Load;
            this.btnThem.Click += btnThem_Click;
            this.btnSua.Click += btnSua_Click;
            this.btnXoa.Click += btnXoa_Click;
            this.btnLamMoi.Click += btnLamMoi_Click;
            this.dgvSinhVien.CellClick += dgvSinhVien_CellClick;
        }

        private void KhoiTaoKhoa()
        {
            if (comboBox1.Items.Count == 0)
            {
                comboBox1.Items.Clear();
                comboBox1.Items.Add("Công nghệ thông tin");
                comboBox1.Items.Add("Công nghệ kỹ thuật ô tô");
                comboBox1.Items.Add("Kinh tế");
                comboBox1.Items.Add("Ngoại ngữ");
                comboBox1.SelectedIndex = 0;
                comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            }
        }

        // Sự kiện khi Form vừa được tải lên (Form Load)
        private void Form1_Load(object sender, EventArgs e)
        {
            KhoiTaoKhoa();

            // Lab 05: Cấu hình ErrorProvider
            errorProvider1.BlinkStyle = ErrorBlinkStyle.NeverBlink;

            // Cấu hình hiển thị và gán dữ liệu cho DataGridView
            dgvSinhVien.DataSource = dsSinhVien;
            dgvSinhVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSinhVien.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSinhVien.ReadOnly = true;

            // Khởi tạo hiển thị mặc định
            CapNhatTuoi();
            CapNhatXemTruocVaTrangThai();
        }

        #region HÀM VÀ SỰ KIỆN TỪ LAB 04
        private int TinhTuoi(DateTime ngaySinh)
        {
            int tuoi = DateTime.Today.Year - ngaySinh.Year;
            if (ngaySinh.Date > DateTime.Today.AddYears(-tuoi)) tuoi--;
            return tuoi;
        }

        private void CapNhatTuoi()
        {
            int tuoi = TinhTuoi(dtpNgaySinh.Value);
            textBox1.Text = "Tuổi: " + tuoi;
        }

        private void dtpNgaySinh_ValueChanged(object sender, EventArgs e)
        {
            CapNhatTuoi();
        }

        private void btnSoThich_Click(object sender, EventArgs e)
        {
            List<string> dsSoThich = new List<string>();

            if (chkDocSach.Checked) dsSoThich.Add("Đọc sách");
            if (chkTheThao.Checked) dsSoThich.Add("Thể thao");
            if (chkLapTrinh.Checked) dsSoThich.Add("Lập trình");

            if (dsSoThich.Count == 0)
            {
                lblSoThich.Text = "Chưa chọn sở thích nào.";
            }
            else
            {
                lblSoThich.Text = "Sở thích: " + string.Join(", ", dsSoThich);
            }
        }

        private void btnChonAnh_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "Chọn ảnh đại diện cho sinh viên";
                dlg.Filter = "Tệp hình ảnh (*.jpg; *.jpeg; *.png; *.bmp)|*.jpg;*.jpeg;*.png;*.bmp";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    picAnh.ImageLocation = dlg.FileName;
                    txtDuongDanAnh.Text = dlg.FileName;
                }
            }
        }
        #endregion

        #region BÀI 1 LAB 05: KIỂM TRA BẮT BUỘC BẰNG ERRORPROVIDER
        private bool ValidateInput()
        {
            errorProvider1.Clear();
            bool valid = true;

            if (string.IsNullOrWhiteSpace(txtMaSV.Text))
            {
                errorProvider1.SetError(txtMaSV, "Vui lòng nhập Mã sinh viên!");
                valid = false;
            }

            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                errorProvider1.SetError(txtHoTen, "Vui lòng nhập Họ và tên!");
                valid = false;
            }

            if (string.IsNullOrWhiteSpace(txtDiemTB.Text))
            {
                errorProvider1.SetError(txtDiemTB, "Vui lòng nhập Điểm trung bình!");
                valid = false;
            }
            else if (!double.TryParse(txtDiemTB.Text.Trim(), out double diem) || diem < 0 || diem > 10)
            {
                errorProvider1.SetError(txtDiemTB, "Điểm trung bình phải thuộc đoạn [0.0 - 10.0]!");
                valid = false;
            }

            return valid;
        }
        #endregion

        #region BÀI 2 LAB 05: GIỚI HẠN KÝ TỰ NHẬP ĐIỂM (KEYPRESS)
        private void txtDiemTB_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) return;
            if (char.IsDigit(e.KeyChar)) return;
            if (e.KeyChar == '.' && !txtDiemTB.Text.Contains(".")) return;

            e.Handled = true;
        }
        #endregion

        #region BÀI 3 LAB 05: CẬP NHẬT TRẠNG THÁI THEO THỜI GIAN THỰC (TEXTCHANGED)
        private void txtMaSV_TextChanged(object sender, EventArgs e)
        {
            CapNhatXemTruocVaTrangThai();
        }

        private void txtHoTen_TextChanged(object sender, EventArgs e)
        {
            CapNhatXemTruocVaTrangThai();
        }

        private void txtDiemTB_TextChanged(object sender, EventArgs e)
        {
            CapNhatXemTruocVaTrangThai();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            CapNhatXemTruocVaTrangThai();
        }

        private void CapNhatXemTruocVaTrangThai()
        {
            string ma = string.IsNullOrWhiteSpace(txtMaSV.Text) ? "..." : txtMaSV.Text.Trim();
            string ten = string.IsNullOrWhiteSpace(txtHoTen.Text) ? "..." : txtHoTen.Text.Trim();

            lblXemTruoc.Text = "Xem trước: " + ma + " - " + ten;

            if (double.TryParse(txtDiemTB.Text.Trim(), out double diem) && diem >= 0 && diem <= 10)
            {
                lblTrangThai.Text = "Trạng thái: Dữ liệu điểm hợp lệ (0.0 - 10.0)";
                lblTrangThai.ForeColor = Color.DarkGreen;
            }
            else
            {
                lblTrangThai.Text = "Trạng thái: Điểm phải thuộc đoạn [0.0 - 10.0]";
                lblTrangThai.ForeColor = Color.Red;
            }
        }
        #endregion

        #region BÀI 4 LAB 05: ĐÓNG GÓI DỮ LIỆU & TRUYỀN SANG FORM CHI TIẾT
        private SinhVien TaoDoiTuongSinhVien()
        {
            double.TryParse(txtDiemTB.Text.Trim(), out double diem);
            string khoa = comboBox1.SelectedItem != null ? comboBox1.SelectedItem.ToString() : "Chưa chọn";

            return new SinhVien(
                txtMaSV.Text.Trim(),
                txtHoTen.Text.Trim(),
                dtpNgaySinh.Value,
                radNam.Checked,
                khoa,
                diem,
                txtDuongDanAnh.Text.Trim()
            );
        }

        private void XoaTrangForm()
        {
            txtMaSV.Clear();
            txtHoTen.Clear();
            txtDiemTB.Clear();
            txtDuongDanAnh.Clear();
            picAnh.Image = null;
            radNam.Checked = true;

            // Kiểm tra an toàn trước khi đặt danh sách
            if (comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedIndex = 0;
            }

            chkDocSach.Checked = false;
            chkLapTrinh.Checked = false;
            chkTheThao.Checked = false;
            txtMaSV.Focus();
        }

        private void btnHienThi_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
            {
                MessageBox.Show("Dữ liệu nhập chưa hợp lệ! Vui lòng kiểm tra các vị trí báo lỗi màu đỏ.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SinhVien sv = TaoDoiTuongSinhVien();
            string thongTin = "Lưu thành công thông tin Sinh viên:\n- Mã SV: " + sv.MaSV
                            + "\n- Họ tên: " + sv.HoTen
                            + "\n- Điểm TB: " + sv.DiemTB
                            + "\n- Xếp loại: " + sv.XepLoai();

            MessageBox.Show(thongTin, "XÁC NHẬN THÔNG TIN", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnXemChiTiet_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
            {
                MessageBox.Show("Vui lòng nhập đúng và đầy đủ thông tin sinh viên trước khi xem chi tiết!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SinhVien sv = TaoDoiTuongSinhVien();
            frmChiTietSinhVien fChiTiet = new frmChiTietSinhVien(sv);
            fChiTiet.ShowDialog();
        }
        #endregion

        #region CHỨC NĂNG THÊM - SỬA - XÓA - LÀM MỚI VÀ DATAGRIDVIEW
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            string maSV = txtMaSV.Text.Trim();
            foreach (var item in dsSinhVien)
            {
                if (item.MaSV.Equals(maSV, StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("Mã sinh viên này đã tồn tại trong danh sách!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            SinhVien sv = TaoDoiTuongSinhVien();
            dsSinhVien.Add(sv);
            MessageBox.Show("Thêm sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            XoaTrangForm();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            string maSV = txtMaSV.Text.Trim();
            SinhVien svTim = null;

            foreach (var item in dsSinhVien)
            {
                if (item.MaSV.Equals(maSV, StringComparison.OrdinalIgnoreCase))
                {
                    svTim = item;
                    break;
                }
            }

            if (svTim == null)
            {
                MessageBox.Show("Không tìm thấy Mã sinh viên cần sửa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            svTim.HoTen = txtHoTen.Text.Trim();
            svTim.NgaySinh = dtpNgaySinh.Value;
            svTim.GioiTinh = radNam.Checked;
            svTim.Khoa = comboBox1.SelectedItem != null ? comboBox1.SelectedItem.ToString() : "";
            double.TryParse(txtDiemTB.Text.Trim(), out double diem);
            svTim.DiemTB = diem;
            svTim.DuongDanAnh = txtDuongDanAnh.Text.Trim();

            dgvSinhVien.Refresh();
            MessageBox.Show("Cập nhật thông tin sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvSinhVien.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn dòng sinh viên cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SinhVien svChon = (SinhVien)dgvSinhVien.CurrentRow.DataBoundItem;
            DialogResult dr = MessageBox.Show($"Bạn có chắc muốn xóa sinh viên [{svChon.MaSV} - {svChon.HoTen}] không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                dsSinhVien.Remove(svChon);
                XoaTrangForm();
                MessageBox.Show("Đã xóa sinh viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            XoaTrangForm();
        }

        private void dgvSinhVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            HienThiChiTietDong(e.RowIndex);
        }

        private void dgvSinhVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            HienThiChiTietDong(e.RowIndex);
        }

        private void HienThiChiTietDong(int rowIndex)
        {
            if (rowIndex >= 0 && dgvSinhVien.Rows[rowIndex].DataBoundItem is SinhVien sv)
            {
                txtMaSV.Text = sv.MaSV;
                txtHoTen.Text = sv.HoTen;
                dtpNgaySinh.Value = sv.NgaySinh;
                radNam.Checked = sv.GioiTinh;
                radNu.Checked = !sv.GioiTinh;
                comboBox1.SelectedItem = sv.Khoa;
                txtDiemTB.Text = sv.DiemTB.ToString();
                txtDuongDanAnh.Text = sv.DuongDanAnh;

                if (!string.IsNullOrEmpty(sv.DuongDanAnh) && File.Exists(sv.DuongDanAnh))
                {
                    picAnh.ImageLocation = sv.DuongDanAnh;
                }
                else
                {
                    picAnh.Image = null;
                }
            }
        }
        #endregion

        #region SỰ KIỆN TRỐNG GIỮ NGUYÊN
        private void label2_Click(object sender, EventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void radNam_CheckedChanged(object sender, EventArgs e) { }
        private void radNu_CheckedChanged(object sender, EventArgs e) { }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) { }
        private void chkDocSach_CheckedChanged(object sender, EventArgs e) { }
        private void chkLapTrinh_CheckedChanged(object sender, EventArgs e) { }
        private void chkTheThao_CheckedChanged(object sender, EventArgs e) { }
        private void lblSoThich_Click(object sender, EventArgs e) { }
        private void picAnh_Click(object sender, EventArgs e) { }
        private void txtDuongDanAnh_TextChanged(object sender, EventArgs e) { }
        private void lblXemTruoc_Click(object sender, EventArgs e) { }
        private void lblTrangThai_Click(object sender, EventArgs e) { }
        #endregion
    }
}