CREATE DATABASE [LapTopStore]
GO

USE [LapTopStore]
GO

CREATE TABLE [LoaiSanPham] (
    [ID] int NOT NULL IDENTITY(1,1),
	[Ten] nvarchar(50) NOT NULL,
	[MoTa] text NULL,
	CONSTRAINT [PK_Loai] PRIMARY KEY ([ID])
);
GO

CREATE TABLE [SanPham] (
    [ID] int NOT NULL IDENTITY(1,1),
	[IDLoai] int NOT NULL,
    [Ten] nvarchar(50) NOT NULL,
	[Gia] float NOT NULL,
	[Hinh] nvarchar(max) NOT NULL,
	[MoTa] nvarchar(2000) NULL,
	[SoLuong] int NOT NULL,
	[GiamGia] float NOT NULL,
	[TrangThai] bit NOT NULL,
    CONSTRAINT [PK_SanPham] PRIMARY KEY ([ID]),
	CONSTRAINT [FK_Product_Loai] FOREIGN KEY ([IDLoai]) REFERENCES [LoaiSanPham] ([ID]) ON DELETE CASCADE
);
GO

CREATE TABLE [KhachHang] (
	[ID] nvarchar(100) NOT NULL,
	[HoTen] nvarchar(max) NULL,
	[DiaChi] nvarchar(1000) NULL,
	CONSTRAINT [PK_KhachHang] PRIMARY KEY ([ID])
);
GO

CREATE TABLE [DonHang] (
	[ID] int NOT NULL IDENTITY(1,1),
	[IDKhachHang] nvarchar(100) NOT NULL,
	[DiaChi] nvarchar(1000) NOT NULL,
	[SoLuong] float NOT NULL,
	[NgayGiao] datetime NOT NULL,
	[NgayDat] datetime NOT NULL,
	[MoTa] nvarchar(max) NULL,
	[TrangThai] bit NOT NULL,
	CONSTRAINT [PK_DonHang] PRIMARY KEY ([ID]),
	CONSTRAINT [FK_DonHang_KhachHang_IDKhachHang] FOREIGN KEY ([IDKhachHang]) REFERENCES [KhachHang] ([ID]) ON DELETE CASCADE
);
GO

CREATE TABLE [ChiTietDonHang] (
	[Id] int NOT NULL IDENTITY(1,1),
	[IDDonHang] int NOT NULL,
	[IDSanPham] int NOT NULL,
	[GiamGia] float NOT NULL,
	[SoLuong] int NOT NULL,
	[DonGia] float NOT NULL,
    CONSTRAINT [PK_ChiTietDonHang] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ChiTietDonHang_DonHang_IDDonHang] FOREIGN KEY ([IDDonHang]) REFERENCES [DonHang] ([ID]) ON DELETE CASCADE,
	CONSTRAINT [FK_ChiTietDonHang_Product_ProductID] FOREIGN KEY ([IDSanPham]) REFERENCES [SanPham] ([ID]) ON DELETE CASCADE
);
GO

INSERT INTO [LoaiSanPham] (Ten, MoTa) 
VALUES ('DELL','dell'), ('ASUS','asus'), ('SONY','sony'), ('ACER','acer'), ('HP','hp')
GO

INSERT INTO SanPham(IDLoai, Ten, Gia, Hinh, MoTa, SoLuong, GiamGia, TrangThai)
VALUES ('2', 'Asus Vivobook 14 X405UA-BV327T','15000000', 'asus1.jpg', 'Laptop ASUS-1', '10', '0.1', '1'),
		('2', 'Asus ZenBook UX430UA-GV261T','14000000', 'asus2.jpg', 'Laptop ASUS-2', '15', '0.05', '0'),
		('1', 'Dell Vostro 7570-70138565','13600000', 'dell1.jpg', 'Laptop DELL-1', '10', '0.05', '1'),
		('1', 'Dell Inspiron 3467-M20NR11','12300000', 'dell2.jpg', 'Laptop DELL-2', '21', '0.05', '0'),
		('3', 'Sony VAIO SVF1521BYA','17600000', 'sony1.jpg', 'Laptop SONY-1', '22', '0.02', '1'),
		('3', 'Sony VAIO VPC-W111XX','1930000', 'sony2.jpg', 'Laptop SONY-2', '30', '0.02', '1'),
		('5', 'HP Pavilion 14-ab117TU','9500000', 'hp1.jpg', 'Laptop HP-1', '23', '0.15', '0'),
		('5', 'HP Probook 450 G3-T1A15PA','10500000', 'hp2.jpg', 'Laptop HP-2', '14', '0.15', '1'),
		('4', 'Acer AS E5-575G-73DR','11450000', 'acer1.jpg', 'Laptop ACER-1', '6', '0.08', '1'),
		('4', 'Acer E5-473-58U5','10870000', 'acer2.jpg', 'Laptop ACER-2', '12', '0.08', '0'),
		('1', 'Dell Latitude 5480-70127518','14590000', 'dell3.jpg', 'Laptop DELL-3', '5', '0.05', '0'),
		('1', 'Dell Inspiron 5379-JYN0N1','15670000', 'dell4.jpg', 'Laptop DELL-4', '18', '0.05', '1')
GO

INSERT INTO [KhachHang] (ID, HoTen, DiaChi)
VALUES ('CUS001','Le Thi Xuan', '119/34A Tran Hoan Street District 3 HCMC'),
		('CUS002','Tran Hung Toan', '34Bis Nguyen Van Loi Street District HCMC'),
		('CUS003','Le Huy', '18/8H Tran Ke Xuong Street Phu Nhuan District HCMC'),
		('CUS004', 'Dang Hoang Nhan', '45/9 Le Loi Street District 1 HCMC'),
		('CUS005',  'Le Viet Khanh', '23B Nguyen Van Nghi Street Go Vap District HCMC')
GO

INSERT INTO DonHang(IDKhachHang, DiaChi, SoLuong, NgayGiao, NgayDat, MoTa, TrangThai)
VALUES ('CUS002', '34Bis Nguyen Van Loi Street District 1 HCMC', '2', '03/25/2017', '03/27/2017', '', '1'),
		('CUS001', '119/34A Tran Hoan Street District 3 HCMC', '3', '04/14/2017', '04/16/2017', '', '0'),
		('CUS005', '23B Nguyen Van Nghi Street Go Vap District HCMC', '5', '09/25/2017', '09/30/2017', '', '1'),
		('CUS003', '18/8H Tran Ke Xuong Street Phu Nhuan District HCMC', '10', '10/14/2017', '10/16/2017', '', '1'),
		('CUS004', '45/9 Le Loi Street District 1 HCMC', '8', '04/01/2017', '04/04/2017', '', '0')
GO  

INSERT INTO[ChiTietDonHang] (IDDonHang, IDSanPham, GiamGia, SoLuong, DonGia)
VALUES ('1', '2', '0.1', '10', '14500000'),
		('2', '1', '0.05', '15', '13800000'),
		('5', '3', '0.05', '8', '13200000'),
		('4', '4', '0.05', '10', '12000000'),
		('3', '5', '0.02', '12', '17100000')
GO

