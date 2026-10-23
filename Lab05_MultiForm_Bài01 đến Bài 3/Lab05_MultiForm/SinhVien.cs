using System;

namespace Lab05_MultiForm
{
    public class SinhVien
    {
        public string MaSV { get; set; }
        public string HoTen { get; set; }
        public DateTime NgaySinh { get; set; }
        public bool GioiTinh { get; set; }
        public string Khoa { get; set; }
        public double DiemTB { get; set; }
        public string DuongDanAnh { get; set; }

        public SinhVien() { }

        public SinhVien(string maSV, string hoTen, DateTime ngaySinh, bool gioiTinh, string khoa, double diemTB, string duongDanAnh = "")
        {
            MaSV = maSV;
            HoTen = hoTen;
            NgaySinh = ngaySinh;
            GioiTinh = gioiTinh;
            Khoa = khoa;
            DiemTB = diemTB;
            DuongDanAnh = duongDanAnh;
        }

        public string XepLoai()
        {
            if (DiemTB >= 8.5) return "Xuất sắc / Giỏi";
            if (DiemTB >= 7.0) return "Khá";
            if (DiemTB >= 5.0) return "Trung bình";
            return "Yếu / Kém";
        }
    }
}