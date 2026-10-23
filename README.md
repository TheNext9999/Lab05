# Lab 05 - Quản lý Sinh Viên (WinForms MDI)

Đây là dự án thực hành Lab 05 thuộc môn học Lập trình C# (Windows Forms). Ứng dụng mô phỏng một hệ thống quản lý sinh viên cơ bản với giao diện đa cửa sổ (MDI).

## 🚀 Các tính năng chính

*   **Giao diện MDI:** Hỗ trợ mở và quản lý nhiều cửa sổ con bên trong một cửa sổ chính.
*   **Xác thực cơ bản:** Form đăng nhập để bảo vệ các chức năng quản lý.
*   **CRUD Sinh viên:** Thêm, Đọc, Cập nhật, và Xóa thông tin sinh viên trực tiếp trên giao diện bằng `DataGridView`.
*   **Kiểm tra hợp lệ (Validation):** 
    *   Bắt lỗi nhập liệu trống hoặc sai định dạng bằng `ErrorProvider`.
    *   Chặn các ký tự không hợp lệ bằng sự kiện `KeyPress`.
*   **Tính toán thời gian thực:** Tự động tính tuổi và hiển thị trạng thái điểm trung bình.

## 🛠️ Công nghệ sử dụng

*   **Ngôn ngữ:** C#
*   **Framework:** .NET Framework (Windows Forms)
*   **Công cụ IDE:** Microsoft Visual Studio

## 📥 Cách chạy dự án

1. Clone kho lưu trữ này về máy hoặc giải nén thư mục mã nguồn.
2. Mở file `.sln` bằng Visual Studio.
3. Nhấn **F5** hoặc **Ctrl + F5** để biên dịch và chạy chương trình.
4. Thông tin đăng nhập mặc định để test:
   * **Tài khoản:** `admin`
   * **Mật khẩu:** `123456`

## 📸 Cấu trúc Form chính
*   `frmMain`: Form MDI Container (Chứa menu và thanh trạng thái).
*   `frmDangNhap`: Xử lý đăng nhập.
*   `Form1`: Giao diện chính thực hiện các thao tác Thêm/Sửa/Xóa sinh viên.
*   `frmChiTietSinhVien`: Giao diện hiển thị chi tiết 1 sinh viên (Truyền dữ liệu giữa 2 Form).