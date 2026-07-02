# SIDATA SOLUTIONS

### PROYEK YANG ADA
terdiri atas kelompok proyek sbb:
1. Abstractions
2. Sistem Loyalti Pelanggan 2
3. (WIP) Authentications
4. (Planned) SIMARI
5. (Planned) SIPOS
6. (Planned) SISDM

### MIGRASI DATABASE
1. Sistem Loyalti Pelanggan 2
Step utk Migrasi database SLIP2
a. Pastikan Sidata.Slip2.WebApi menjadi startup project
b. buka menu Tools > NuGet Package Manager > Package Manager Console
c. pilih Default Project ke Sidata.SLIP2.Data.Context

Jika hanya membuat Database saja lakukan ini
d. kemudian di prompt ketikkan "Update-Database"

Jika melakukan migrasi lakukan ini 
e. ketikkan perintah ini "Add-Migration [nama unik] -Project [namaDbContext] -StartupProject [namaStartupProject]"
f. lalu setelah review hasilnya di folder \Migrations, ketikkan perintah "Update-Database"