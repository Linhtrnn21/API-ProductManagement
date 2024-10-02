# API - Quản Lý Sản Phẩm

## Mô Tả Dự Án
Dự án này là một ứng dụng API quản lý sản phẩm, cho phép người dùng thực hiện các thao tác CRUD (Tạo, Đọc, Cập nhật, Xóa) cho sản phẩm. Ứng dụng sử dụng .NET 8 với Entity Framework Core, Identity cho xác thực người dùng và sử dụng In-Memory Caching để tăng tốc độ truy xuất dữ liệu.

### Tính Năng
- **Quản lý sản phẩm:** Người dùng có thể thêm, sửa, xóa và xem danh sách sản phẩm.
- **Xác thực và phân quyền:** Sử dụng JWT cho xác thực người dùng và phân quyền cho các vai trò (Admin, User).
- **Có thể xóa, update role cho các user khác khi là Admin
- **Caching:** Sử dụng In-Memory Caching để tăng hiệu suất truy xuất dữ liệu.
- **Logging:** Ghi log hoạt động của người dùng khi truy cập vào API.

## Công Nghệ Sử Dụng
- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- JWT Authentication
- In-Memory Caching
- Serilog cho Logging
- xUnit cho Unit Testing

## Cài Đặt
### Yêu Cầu
- .NET 8 SDK
- Visual Studio hoặc bất kỳ IDE nào hỗ trợ .NET


**##Cách Sử Dụng**
	1. Xác thực người dùng:
		Tài khoản admin: admin@admin.com
		Mật khẩu       : Admin123@
	2. Đăng ký người dùng mới sẽ nhận role là "User"
	3. Đăng nhập để lấy token JWT.
	4. Các API có sẵn:

		GET /api/products: Lấy danh sách sản phẩm.
		GET /api/products/{id}: Lấy thông tin sản phẩm theo ID.
		POST /api/products: Thêm sản phẩm mới (Chỉ Admin).
		PUT /api/products/{id}: Cập nhật thông tin sản phẩm (Chỉ Admin).
		DELETE /api/products/{id}: Xóa sản phẩm (Chỉ Admin).
		
		PUT /api/users/update-role: Cập nhật role cho tài khoản khác (Chỉ Admin).
		DELETE /api/users/{email}: Xóa tài khoản (Chỉ Admin).

**##Unit Tests**
Dự án có các bài kiểm tra đơn vị cho các controller bằng xUnit. Để chạy các bài kiểm tra:
	Mở dự án test trong Visual Studio.
	Chạy các bài kiểm tra thông qua Test Explorer.

**##Liên Hệ**
Nếu bạn có bất kỳ câu hỏi hoặc góp ý nào, hãy liên hệ với tôi qua:
Email: linhmta2111@gmail.com
Sdt: 0328398289
