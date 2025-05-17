# Aplikasi Jadwal Dokter

Aplikasi ini dibuat untuk memenuhi tugas teknis Junior .NET Developer, yang menampilkan jadwal dokter dari MS SQL Server dan menghasilkan output Excel dan PDF.

## Arsitektur Repository Pattern

Aplikasi ini mengimplementasikan **Repository Pattern** untuk memisahkan logika akses data dari logika bisnis. Berikut adalah struktur utama:

- **Models**:
  - `DoctorSchedule`: Kelas model yang merepresentasikan entitas jadwal dokter (Id, NamaDokter, Spesialis, Hari, Jam).
- **Repositories**:
  - `IDoctorRepository`: Interface yang mendefinisikan kontrak untuk operasi data (`GetAll()`).
  - `DoctorRepository`: Implementasi interface yang menggunakan ADO.NET untuk menjalankan stored procedure `sp_GetDoctorSchedules` dengan CTE di MS SQL Server.
- **Controllers**:
  - `ExportController`: Menggunakan repository untuk mengambil data dan menghasilkan file Excel (dengan ClosedXML) dan PDF (dengan Rotativa).
- **Data Access**:
  - Menggunakan ADO.NET (`SqlConnection`, `SqlCommand`) untuk mengakses database tanpa ORM.
  - Stored procedure `sp_GetDoctorSchedules` dengan CTE digunakan untuk mengambil data.

**Alur Data**:
1. `ExportController` memanggil `IDoctorRepository.GetAll()`.
2. `DoctorRepository` menjalankan stored procedure `sp_GetDoctorSchedules` menggunakan ADO.NET.
3. Data dari SQL Server dikembalikan sebagai `List<DoctorSchedule>` untuk diolah menjadi Excel atau PDF.

**Diagram Arsitektur**:
[ExportController] --> [IDoctorRepository] --> [DoctorRepository] --> [ADO.NET] --> [SQL Server]


## Cara Menjalankan
1. Pastikan MS SQL Server terinstal dan jalankan script SQL untuk membuat database dan stored procedure (lihat `init_db.sql`).
2. Update connection string di `appsettings.json`.
3. Jalankan `dotnet restore` dan `dotnet run`.
4. Akses aplikasi di browser untuk menguji export Excel dan PDF.

## Dependensi
- ClosedXML: Untuk menghasilkan file Excel.
- Rotativa.AspNetCore: Untuk menghasilkan file PDF.
- System.Data.SqlClient: Untuk akses database dengan ADO.NET.
