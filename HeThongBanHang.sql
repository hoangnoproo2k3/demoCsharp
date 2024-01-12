CREATE DATABASE HeThongBanHang
go
USE HeThongBanHang
go
CREATE TABLE tblMatHang (
    iMaH INT PRIMARY KEY,
    sTenhang NVARCHAR(255),
    iSoluong INT CHECK (iSoluong >= 0),
    fDongia DECIMAL(18, 2) CHECK (fDongia >= 0),
    iMaLH INT
);
go
CREATE TABLE tblLoaiHang (
    iMaLH INT PRIMARY KEY,
    sTenLoai NVARCHAR(255)
);
go
ALTER TABLE tblMatHang
ADD CONSTRAINT FK_tblMatHang_tblLoaiHang
FOREIGN KEY (iMaLH) REFERENCES tblLoaiHang(iMaLH);
go
-- Insert or Update (Upsert) Procedure
CREATE PROCEDURE UpsertMatHang
    @iMaH INT,
    @sTenhang NVARCHAR(255),
    @iSoluong INT,
    @fDongia DECIMAL(18, 2),
    @iMaLH INT
AS
BEGIN
    IF EXISTS (SELECT 1 FROM tblMatHang WHERE iMaH = @iMaH)
    BEGIN
        UPDATE tblMatHang
        SET sTenhang = @sTenhang, 
            iSoluong = @iSoluong, 
            fDongia = @fDongia, 
            iMaLH = @iMaLH
        WHERE iMaH = @iMaH;
    END
    ELSE
    BEGIN
        INSERT INTO tblMatHang (iMaH, sTenhang, iSoluong, fDongia, iMaLH)
        VALUES (@iMaH, @sTenhang, @iSoluong, @fDongia, @iMaLH);
    END
END;
go
-- Delete Procedure
CREATE PROCEDURE DeleteMatHang
    @iMaH INT
AS
BEGIN
    DELETE FROM tblMatHang WHERE iMaH = @iMaH;
END;
