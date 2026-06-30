# 1.Sidata.Abstractions
### v.2026.06.1
- awal buat
- BARU: class PersistenObject
- BARU: interface ISoftDelete dan IMasterClass
- BARU: template utk menghasilkan MIT License Header
### v.2026.06.2
- BARU: add AssemblyProperties
 
# 2.Sidata.Abstractions.DataContext
### v.2026.06.1
- buat baru
- BARU: BaseDbContext utk dasar pembuatan DbContext dalam platform Sidata Solusion.
- BARU: enum utk penentu selector
- BARU: interface utk IContextUSer dan IPeriodAware

# 3.Sidata.Abstracions.Queryable
### v.2026.06.1
- awal buat
- BARU : enum FilterOperator, SortDirection dan FilterSortSelector
- BARU : interfaces IPropertyOperator
- BARU : models berisi FilterContent, SortContent dan QueryContent
- BARU : Extension terhadap IQueryable<T> utk Apply QueryContent menjadi Expression
 
# 4.Sidata.Abstractions.Queryable.SqlServer
### v.2026.06.1
- awal buat
- BARU: ekstensi IQueryable utk membuat Expression LIKE di Sql Server

# 5.Sidata.Abstractions.WebApi
### v.2026.06.1
- first build
- BARU: webapi CRUD controller base
- BARU: controllerobjectid
- BARU: cruddefinition, ICrudDefinition
### v.2026.06.2:
- BARU: extension SetupCrudDefinition

# 6.Sidata.Abstractions.WebApi.ResponseRequest
### v.2026.06.1
- build baru
- BARU: definisi request dan response data content
- BARU: ekstensi utk build request dan response content, sehingga mudah dipanggil dari webapi secara generic.

# 7.Sidata.SLIP2.Data
### v.2026.06.1
- buat awal
- BARU : semua class modul utk domain utama Sistem Loyalti Pelanggan 2

# 8.Sidata.SLIP2.Data.Context
### v.2026.06.1
- awal buat
- BARU: LoyaltiDbContext
- BARU: Configure models data utk SLIP2

# 9.Sidata.SLIP2.Data.DTOs
### v.2026.06.1
- awal buat
- BARU: Merchant dan Customer DTO

# 10.Sidata.SLIP2.WebApi
### v.2026.06.1
- first build
- BARU: endpoint utk Merchant dan Customer
### v.2026.06.2
- CHANGE:swagger:using custom tag utk menambahkan sort, dan grouping
- CHANGE:controllers:using custom tag
- NOTE: to build new Crud on xxx model: Add xxxController, Add xxxDto, Add xxxCrudDefinition

# 11.Sidata.Tools.SwaggerJsonToWebApiHttpConverter
### v.2026.06.1
- first build
- build using Claude Code AI

---
Sidata Solution
<br>Copyright (c) 2026 Sidata Solusi Ritel
<br>Licensed under the MIT License.