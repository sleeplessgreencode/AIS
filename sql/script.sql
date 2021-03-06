USE [AIS]
GO
/****** Object:  Table [dbo].[Absensi]    Script Date: 5/11/2018 9:18:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Absensi](
	[NIK] [nvarchar](6) NOT NULL,
	[TglMasuk] [date] NOT NULL,
	[JamMasuk] [time](7) NULL,
	[JamKeluar] [time](7) NULL,
	[Status] [nvarchar](15) NULL,
	[Keterangan] [nvarchar](50) NULL,
	[UserEntry] [nvarchar](30) NULL,
	[TimeEntry] [datetime] NULL,
 CONSTRAINT [PK_Absensi] PRIMARY KEY CLUSTERED 
(
	[NIK] ASC,
	[TglMasuk] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Alokasi]    Script Date: 5/11/2018 9:18:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Alokasi](
	[Alokasi] [nvarchar](1) NOT NULL,
	[Keterangan] [nvarchar](200) NOT NULL,
	[UserEntry] [nvarchar](30) NULL,
	[TimeEntry] [datetime] NULL,
 CONSTRAINT [PK_Alokasi] PRIMARY KEY CLUSTERED 
(
	[Alokasi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Bank]    Script Date: 5/11/2018 9:18:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bank](
	[Bank] [nvarchar](20) NOT NULL,
	[UserEntry] [nvarchar](30) NULL,
	[TimeEntry] [datetime] NULL,
 CONSTRAINT [PK_Bank] PRIMARY KEY CLUSTERED 
(
	[Bank] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BLE]    Script Date: 5/11/2018 9:18:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BLE](
	[LedgerNo] [bigint] IDENTITY(1,1) NOT NULL,
	[TglBayar] [date] NULL,
	[NoPD] [nvarchar](50) NULL,
	[JobNo] [nvarchar](10) NULL,
	[Keterangan] [nvarchar](255) NULL,
	[Alokasi] [nvarchar](50) NULL,
	[TipeForm] [nvarchar](50) NULL,
	[NoKO] [nvarchar](15) NULL,
	[RekId] [nvarchar](50) NULL,
	[CaraBayar] [nvarchar](50) NULL,
	[JenisTrf] [nvarchar](50) NULL,
	[NoCG] [nvarchar](50) NULL,
	[NoRek] [nvarchar](30) NULL,
	[AtasNama] [nvarchar](100) NULL,
	[Bank] [nvarchar](20) NULL,
	[Amount] [money] NULL CONSTRAINT [DF_BLE_Amount]  DEFAULT ((0)),
	[NmPenerimaTunai] [nvarchar](50) NULL,
	[SumberKas] [nvarchar](10) NULL,
	[UserEntry] [nvarchar](30) NULL,
	[TimeEntry] [datetime] NULL,
 CONSTRAINT [PK_BLE] PRIMARY KEY CLUSTERED 
(
	[LedgerNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[COA]    Script Date: 5/11/2018 9:18:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[COA](
	[AccNo] [nvarchar](10) NOT NULL,
	[AccName] [nvarchar](100) NULL,
	[UserEntry] [nvarchar](30) NULL,
	[TimeEntry] [datetime] NULL,
 CONSTRAINT [PK_COA_1] PRIMARY KEY CLUSTERED 
(
	[AccNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[COAX]    Script Date: 5/11/2018 9:18:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[COAX](
	[JobNo] [nvarchar](10) NOT NULL,
	[AccNo] [nvarchar](10) NOT NULL,
	[AccName] [nvarchar](100) NULL,
	[UserEntry] [nvarchar](30) NULL,
	[TimeEntry] [datetime] NULL,
 CONSTRAINT [PK_COAX_1] PRIMARY KEY CLUSTERED 
(
	[JobNo] ASC,
	[AccNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Company]    Script Date: 5/11/2018 9:18:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Company](
	[CompanyId] [nvarchar](7) NOT NULL,
	[CompanyNm] [nvarchar](100) NULL,
	[Pic] [nvarchar](50) NULL,
	[Alamat] [nvarchar](255) NULL,
	[Telepon] [nvarchar](20) NULL,
	[Fax] [nvarchar](20) NULL,
	[Email] [nvarchar](100) NULL,
	[NPWP] [nvarchar](30) NULL,
	[Logo] [nvarchar](3000) NULL,
	[UserEntry] [nvarchar](30) NULL,
	[TimeEntry] [datetime] NULL,
 CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED 
(
	[CompanyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Counter]    Script Date: 5/11/2018 9:18:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Counter](
	[JobNo] [nvarchar](10) NOT NULL,
	[Alokasi] [nvarchar](1) NOT NULL,
	[CounterPD] [int] NULL CONSTRAINT [DF_Counter_CounterPD]  DEFAULT ((0)),
	[CounterKO] [int] NULL CONSTRAINT [DF_Counter_CounterKO]  DEFAULT ((0)),
	[CounterKSO] [int] NULL CONSTRAINT [DF_Counter_CounterKSO]  DEFAULT ((0)),
	[CounterKOMIX] [int] NULL CONSTRAINT [DF_Counter_CounterKOMIX]  DEFAULT ((0)),
	[CounterPDMIX] [int] NULL CONSTRAINT [DF_Counter_CounterPDMIX]  DEFAULT ((0)),
	[UserEntry] [nvarchar](30) NULL,
	[TimeEntry] [datetime] NULL,
 CONSTRAINT [PK_Counter] PRIMARY KEY CLUSTERED 
(
	[JobNo] ASC,
	[Alokasi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CounterGl]    Script Date: 5/11/2018 9:18:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CounterGl](
	[JobNo] [nvarchar](10) NOT NULL,
	[Site] [nvarchar](8) NOT NULL,
	[GlYear] [int] NOT NULL,
	[GlMonth] [int] NOT NULL,
	[Counter] [int] NULL,
	[UserEntry] [nvarchar](30) NULL,
	[TimeEntry] [datetime] NULL,
 CONSTRAINT [PK_CounterGL] PRIMARY KEY CLUSTERED 
(
	[JobNo] ASC,
	[Site] ASC,
	[GlYear] ASC,
	[GlMonth] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CPanel]    Script Date: 5/11/2018 9:18:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CPanel](
	[CNotification] [nvarchar](255) NULL,
	[CTimeOut] [datetime] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DIPA]    Script Date: 5/11/2018 9:18:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DIPA](
	[JobNo] [nvarchar](10) NOT NULL,
	[Tahun] [nvarchar](4) NOT NULL,
	[Budget] [money] NULL CONSTRAINT [DF_DIAP_Budget]  DEFAULT ((0)),
	[UserEntry] [nvarchar](30) NULL,
	[TimeEntry] [datetime] NULL,
 CONSTRAINT [PK_DIPA] PRIMARY KEY CLUSTERED 
(
	[JobNo] ASC,
	[Tahun] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[GlDtl]    Script Date: 5/11/2018 9:18:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GlDtl](
	[NoInt] [nvarchar](25) NOT NULL,
	[NoUrut] [int] NOT NULL,
	[AccNo] [nvarchar](10) NOT NULL,
	[Debet] [money] NULL,
	[Kredit] [money] NULL,
	[Identitas] [nvarchar](8) NULL,
	[Uraian] [nvarchar](255) NULL,
	[IdKeu] [nvarchar](10) NULL,
	[UserEntry] [nvarchar](30) NULL,
	[TimeEntry] [datetime] NULL,
 CONSTRAINT [PK_GlDtl] PRIMARY KEY CLUSTERED 
(
	[NoInt] ASC,
	[NoUrut] ASC,
	[AccNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[GlHdr]    Script Date: 5/11/2018 9:18:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GlHdr](
	[JobNo] [nvarchar](10) NOT NULL,
	[Site] [nvarchar](8) NOT NULL,
	[NoNota] [nvarchar](25) NOT NULL,
	[NoInt] [nvarchar](25) NOT NULL,
	[GlPeriode] [char](6) NULL,
	[TglNota] [date] NULL,
	[Jenis] [nvarchar](6) NULL,
	[Debet] [money] NULL CONSTRAINT [DF_GlHdr_Debet]  DEFAULT ((0)),
	[Kredit] [money] NULL CONSTRAINT [DF_GlHdr_Kredit]  DEFAULT ((0)),
	[Alokasi] [nvarchar](1) NULL,
	[UserEntry] [nvarchar](30) NULL,
	[TimeEntry] [datetime] NULL,
	[UserClosed] [nvarchar](30) NULL,
	[TimeClosed] [datetime] NULL,
 CONSTRAINT [PK_GlHdr] PRIMARY KEY CLUSTERED 
(
	[JobNo] ASC,
	[Site] ASC,
	[NoNota] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[GlReff]    Script Date: 5/11/2018 9:18:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GlReff](
	[JobNo] [nvarchar](10) NOT NULL,
	[Site] [nvarchar](10) NOT NULL,
	[Member] [nvarchar](10) NULL,
	[Logo] [nvarchar](3000) NULL,
	[Kasir] [nvarchar](30) NULL,
	[UserEntry] [nvarchar](30) NULL,
	[TimeEntry] [datetime] NULL,
 CONSTRAINT [PK_GlReff] PRIMARY KEY CLUSTERED 
(
	[JobNo] ASC,
	[Site] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[HariLibur]    Script Date: 5/11/2018 9:18:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HariLibur](
	[TglLibur] [date] NOT NULL,
	[Keterangan] [nvarchar](50) NULL,
	[UserEntry] [nvarchar](30) NULL,
	[TimeEntry] [datetime] NULL,
 CONSTRAINT [PK_HariLibur] PRIMARY KEY CLUSTERED 
(
	[TglLibur] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Identitas]    Script Date: 5/11/2018 9:18:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Identitas](
	[JobNo] [nvarchar](10) NOT NULL,
	[Identitas] [nvarchar](4) NOT NULL,
	[Nama] [nvarchar](50) NULL,
	[Keterangan] [nvarchar](255) NULL,
	[UserEntry] [nvarchar](30) NULL,
	[TimeEntry] [datetime] NULL,
 CONSTRAINT [PK_Identitas] PRIMARY KEY CLUSTERED 
(
	[JobNo] ASC,
	[Identitas] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Invoice]    Script Date: 5/11/2018 9:18:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invoice](
	[JobNo] [nvarchar](10) NOT NULL,
	[NoKO] [nvarchar](15) NOT NULL,
	[InvNo] [nvarchar](50) NOT NULL,
	[InvDate] [date] NULL,
	[DueDate] [date] NULL,
	[PPN] [money] NULL CONSTRAINT [DF_Invoice_PPN]  DEFAULT ((0)),
	[FPNo] [nvarchar](50) NULL,
	[FPDate] [date] NULL,
	[Total] [money] NULL CONSTRAINT [DF_Invoice_Total]  DEFAULT ((0)),
	[Keterangan] [nvarchar](255) NULL,
	[UserEntry] [nvarchar](30) NULL,
	[TimeEntry] [datetime] NULL,
 CONSTRAINT [PK_Invoice] PRIMARY KEY CLUSTERED 
(
	[JobNo] ASC,
	[NoKO] ASC,
	[InvNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[InvPD]    Script Date: 5/11/2018 9:18:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InvPD](
	[JobNo] [nvarchar](10) NOT NULL,
	[NoKO] [nvarchar](15) NOT NULL,
	[InvNo] [nvarchar](50) NOT NULL,
	[NoPD] [nvarchar](20) NOT NULL,
	[PaymentAmount] [money] NULL CONSTRAINT [DF_InvPD_PaymentAmount]  DEFAULT ((0)),
	[UserEntry] [nvarchar](30) NULL,
	[TimeEntry] [datetime] NULL,
 CONSTRAINT [PK_InvPD] PRIMARY KEY CLUSTERED 
(
	[JobNo] ASC,
	[NoKO] ASC,
	[InvNo] ASC,
	[NoPD] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Jaminan]    Script Date: 5/11/2018 9:18:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Jaminan](
	[JobNo] [nvarchar](10) NOT NULL,
	[Tipe] [nvarchar](20) NOT NULL,
	[CompanyId] [nvarchar](50) NOT NULL,
	[DrTgl] [date] NULL,
	[SpTgl] [date] NULL,
	[Nominal] [money] NULL CONSTRAINT [DF_Jaminan_Nominal]  DEFAULT ((0)),
	[Penerbit] [nvarchar](20) NULL,
	[Collateral] [char](1) NULL CONSTRAINT [DF_Jaminan_Collateral]  DEFAULT ((0)),
	[NominalCollateral] [money] NULL CONSTRAINT [DF_Jaminan_NominalCollateral]  DEFAULT ((0)),
	[UserEntry] [nvarchar](30) NULL,
	[TimeEntry] [datetime] NULL,
	[UserKembali] [nvarchar](30) NULL,
	[TglKembali] [date] NULL,
 CONSTRAINT [PK_Jaminan] PRIMARY KEY CLUSTERED 
(
	[JobNo] ASC,
	[Tipe] ASC,
	[CompanyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Job]    Script Date: 5/11/2018 9:18:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Job](
	[JobNo] [nvarchar](10) NOT NULL,
	[JobNm] [nvarchar](50) NULL,
	[Deskripsi] [nvarchar](200) NULL,
	[Lokasi] [nvarchar](100) NULL,
	[Instansi] [nvarchar](100) NULL,
	[Kategori] [nvarchar](10) NULL,
	[KSO] [char](1) NULL CONSTRAINT [DF_Job_kso]  DEFAULT ((0)),
	[PersenKSO] [decimal](5, 2) NULL CONSTRAINT [DF_Job_PersenKSO]  DEFAULT ((0)),
	[Member1] [nvarchar](50) NULL,
	[Member2] [nvarchar](50) NULL,
	[Own] [char](1) NULL,
	[PersenShare1] [decimal](5, 2) NULL CONSTRAINT [DF_Job_Share1]  DEFAULT ((0)),
	[PersenShare2] [decimal](5, 2) NULL CONSTRAINT [DF_Job_Share2]  DEFAULT ((0)),
	[BrutoShare1] [money] NULL CONSTRAINT [DF_Job_BrutoShare1]  DEFAULT ((0)),
	[BrutoShare2] [money] NULL CONSTRAINT [DF_Job_BrutoShare2]  DEFAULT ((0)),
	[KetShare1] [nvarchar](255) NULL,
	[KetShare2] [nvarchar](255) NULL,
	[PrdAwal] [date] NULL,
	[PrdAkhir] [date] NULL,
	[Minggu] [int] NULL CONSTRAINT [DF_Job_Minggu]  DEFAULT ((0)),
	[Hari] [int] NULL CONSTRAINT [DF_Job_Hari]  DEFAULT ((0)),
	[Bruto] [money] NULL CONSTRAINT [DF_Job_Bruto]  DEFAULT ((0)),
	[Netto] [money] NULL CONSTRAINT [DF_Job_Netto]  DEFAULT ((0)),
	[CompanyId] [nvarchar](100) NULL,
	[NoKontrak] [nvarchar](100) NULL,
	[TglKontrak] [date] NULL,
	[StatusJob] [nvarchar](20) NULL,
	[AddendumKe] [int] NULL CONSTRAINT [DF_Job_AddendumKe]  DEFAULT ((0)),
	[RemarkAddendum] [nvarchar](255) NULL,
	[NoPHO] [nvarchar](100) NULL,
	[TglPHO] [date] NULL,
	[NoFHO] [nvarchar](100) NULL,
	[TglFHO] [date] NULL,
	[ClosedBy] [nvarchar](30) NULL,
	[TimeClosed] [datetime] NULL,
	[NoRek] [nvarchar](30) NULL,
	[Bank] [nvarchar](20) NULL,
	[AtasNama] [nvarchar](200) NULL,
	[Nama] [nvarchar](100) NULL,
	[Alamat] [nvarchar](255) NULL,
	[Telepon] [nvarchar](20) NULL,
	[NPWP] [nvarchar](30) NULL,
	[NoRekInduk] [nvarchar](30) NULL,
	[BankInduk] [nvarchar](20) NULL,
	[AtasNamaInduk] [nvarchar](200) NULL,
	[NoRekMember] [nvarchar](30) NULL,
	[BankMember] [nvarchar](20) NULL,
	[AtasNamaMember] [nvarchar](200) NULL,
	[NamaPartner] [nvarchar](50) NULL,
	[FHOFile] [nvarchar](1000) NULL,
	[TipeManajerial] [nvarchar](20) NULL,
	[Logo] [nvarchar](3000) NULL,
	[UserEntry] [nvarchar](30) NULL,
	[TimeEntry] [datetime] NULL,
	[NPWPName] [nvarchar](100) NULL,
	[NPWPAddress] [nvarchar](255) NULL,
	[NPWPCompany] [nvarchar](50) NULL,
 CONSTRAINT [PK_Job] PRIMARY KEY CLUSTERED 
(
	[JobNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[JobH]    Script Date: 5/11/2018 9:18:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JobH](
	[JobNo] [nvarchar](10) NOT NULL,
	[Bruto] [money] NULL,
	[Netto] [money] NULL,
	[NoKontrak] [nvarchar](100) NULL,
	[TglKontrak] [date] NOT NULL,
	[AddendumKe] [int] NOT NULL,
	[RemarkAddendum] [nvarchar](255) NULL,
	[PrdAwal] [date] NULL,
	[PrdAkhir] [date] NULL,
	[Hari] [int] NULL,
	[UserEntry] [nvarchar](30) NULL,
	[TimeEntry] [datetime] NULL,
 CONSTRAINT [PK_JobH] PRIMARY KEY CLUSTERED 
(
	[JobNo] ASC,
	[AddendumKe] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[JurnalEntry]    Script Date: 5/11/2018 9:18:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[JurnalEntry](
	[JobNo] [nvarchar](10) NOT NULL,
	[IE] [char](1) NULL CONSTRAINT [DF_JurnalEntry_IE]  DEFAULT ('I'),
	[LedgerNo] [int] NULL,
	[NoJurnal] [nvarchar](25) NOT NULL,
	[TglJurnal] [date] NULL,
	[Bulan] [int] NULL,
	[Tahun] [int] NULL,
	[PC] [char](1) NULL,
	[Site] [nvarchar](10) NULL,
	[Member] [nvarchar](10) NULL,
	[Nota] [nvarchar](5) NULL,
	[Identitas] [nvarchar](4) NULL,
	[NoReg] [nvarchar](20) NULL,
	[AccNo] [nvarchar](10) NULL,
	[Uraian] [nvarchar](255) NULL,
	[DK] [char](1) NULL,
	[Debet] [money] NULL CONSTRAINT [DF_Table_1_Amount]  DEFAULT ((0)),
	[Kredit] [money] NULL CONSTRAINT [DF_JurnalEntry_Kredit]  DEFAULT ((0)),
	[DebetBalance] [money] NULL CONSTRAINT [DF_Table_1_Debet_Balance]  DEFAULT ((0)),
	[KreditBalance] [money] NULL CONSTRAINT [DF_Table_1_Kredit_Balance]  DEFAULT ((0)),
	[ApprovedBy] [nvarchar](30) NULL,
	[TimeApproved] [datetime] NULL,
	[UserEntry] [nvarchar](30) NULL,
	[TimeEntry] [datetime] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Karyawan]    Script Date: 5/11/2018 9:18:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Karyawan](
	[NIK] [nvarchar](6) NOT NULL,
	[Nama] [nvarchar](30) NULL,
	[Divisi] [nvarchar](10) NULL,
	[Dept] [nvarchar](50) NULL,
	[Lokasi] [nvarchar](20) NULL,
	[Jabatan] [nvarchar](50) NULL,
	[Active] [char](1) NULL CONSTRAINT [DF_Karyawan_Active]  DEFAULT ((1)),
	[UserEntry] [nvarchar](30) NULL,
	[TimeEntry] [datetime] NULL,
 CONSTRAINT [PK_Karyawan] PRIMARY KEY CLUSTERED 
(
	[NIK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Kategori]    Script Date: 5/11/2018 9:18:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Kategori](
	[KategoriId] [nvarchar](30) NOT NULL,
	[Keterangan] [nvarchar](255) NULL,
	[UserEntry] [nvarchar](30) NULL,
	[TimeEntry] [datetime] NULL,
 CONSTRAINT [PK_Kategori] PRIMARY KEY CLUSTERED 
(
	[KategoriId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[KoDtl]    Script Date: 5/11/2018 9:18:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KoDtl](
	[NoKO] [nvarchar](15) NOT NULL,
	[NoUrut] [int] NULL,
	[Alokasi] [nvarchar](1) NULL,
	[KdRAP] [nvarchar](10) NULL,
	[Uraian] [nvarchar](255) NULL,
	[Vol] [decimal](10, 3) NULL CONSTRAINT [DF__KoDtl__Vol__308412F8]  DEFAULT ((0)),
	[Uom] [nvarchar](15) NULL,
	[HrgSatuan] [money] NULL CONSTRAINT [DF__KoDtl__HrgSatuan__31783731]  DEFAULT ((0)),
	[UserEntry] [nvarchar](30) NULL,
	[TimeEntry] [datetime] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[KoDtlH]    Script Date: 5/11/2018 9:18:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KoDtlH](
	[NoKO] [nvarchar](15) NOT NULL,
	[TglKO] [date] NOT NULL,
	[NoUrut] [int] NULL,
	[Alokasi] [nvarchar](1) NULL,
	[KdRAP] [nvarchar](10) NULL,
	[Uraian] [nvarchar](255) NULL,
	[Vol] [decimal](10, 3) NULL CONSTRAINT [DF__KoDtlH__Vol__308412F8]  DEFAULT ((0)),
	[Uom] [nvarchar](15) NULL,
	[HrgSatuan] [money] NULL CONSTRAINT [DF__KoDtlH__HrgSatuan__31783731]  DEFAULT ((0)),
	[UserEntry] [nvarchar](30) NULL,
	[TimeEntry] [datetime] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[KoHdr]    Script Date: 5/11/2018 9:18:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[KoHdr](
	[NoKO] [nvarchar](15) NOT NULL,
	[TglKO] [date] NULL,
	[JobNo] [nvarchar](10) NULL,
	[VendorId] [nvarchar](7) NULL,
	[KategoriId] [nvarchar](30) NULL,
	[SubKategoriId] [nvarchar](30) NULL,
	[SubTotal] [money] NULL CONSTRAINT [DF_KoHdr_SubTotal]  DEFAULT ((0)),
	[DiscPercentage] [decimal](6, 2) NULL CONSTRAINT [DF_KoHdr_DiscPercentage]  DEFAULT ((0)),
	[DiscAmount] [money] NULL CONSTRAINT [DF_KoHdr_DiscAmount]  DEFAULT ((0)),
	[PPN] [money] NULL CONSTRAINT [DF_KoHdr_PPN]  DEFAULT ((0)),
	[TotalTerbayar] [money] NULL CONSTRAINT [DF_KoHdr_TotalTerbayar]  DEFAULT ((0)),
	[AddendumKe] [int] NULL CONSTRAINT [DF_KoHdr_AddendumKe]  DEFAULT ((0)),
	[ClosedBy] [nvarchar](30) NULL,
	[TimeClosed] [datetime] NULL,
	[AlamatKirim] [nvarchar](255) NULL,
	[NamaKirim] [nvarchar](100) NULL,
	[TeleponKirim] [nvarchar](20) NULL,
	[MaterialApproval] [char](1) NULL CONSTRAINT [DF_KoHdr_MaterialApproval]  DEFAULT ((0)),
	[RAP] [char](1) NULL CONSTRAINT [DF_KoHdr_RAP]  DEFAULT ((0)),
	[K3] [char](1) NULL CONSTRAINT [DF_KoHdr_K3]  DEFAULT ((0)),
	[SyaratTeknis] [nvarchar](255) NULL,
	[SyaratPembayaran] [nvarchar](255) NULL,
	[JadwalPengiriman] [nvarchar](255) NULL,
	[Sanksi] [nvarchar](255) NULL,
	[JadwalPembayaran] [nvarchar](255) NULL,
	[Keterangan] [nvarchar](255) NULL,
	[QRCode] [nvarchar](6) NULL,
	[ApprovedBy] [nvarchar](30) NULL,
	[TimeApproved] [datetime] NULL,
	[UserEntry] [nvarchar](30) NULL,
	[TimeEntry] [datetime] NULL,
	[OverridePPN] [char](1) NULL CONSTRAINT [DF_KoHdr_OverridePPN]  DEFAULT ((0)),
	[PDFFile] [nvarchar](1000) NULL,
	[CanceledBy] [nvarchar](30) NULL,
	[TimeCancel] [datetime] NULL,
	[CancelReason] [nvarchar](100) NULL,
 CONSTRAINT [PK_KoHdr] PRIMARY KEY CLUSTERED 
(
	[NoKO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[KoHdrH]    Script Date: 5/11/2018 9:18:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[KoHdrH](
	[NoKO] [nvarchar](15) NOT NULL,
	[TglKO] [date] NOT NULL,
	[JobNo] [nvarchar](10) NULL,
	[VendorId] [nvarchar](7) NULL,
	[KategoriId] [nvarchar](30) NULL,
	[SubKategoriId] [nvarchar](30) NULL,
	[SubTotal] [money] NULL,
	[DiscPercentage] [decimal](6, 2) NULL CONSTRAINT [DF_KoHdrH_DiscPercentage]  DEFAULT ((0)),
	[DiscAmount] [money] NULL CONSTRAINT [DF_KoHdrH_DiscAmount]  DEFAULT ((0)),
	[PPN] [money] NULL CONSTRAINT [DF_KoHdrH_PPN]  DEFAULT ((0)),
	[TotalTerbayar] [money] NULL CONSTRAINT [DF_KoHdrH_TotalTerbayar]  DEFAULT ((0)),
	[AddendumKe] [int] NULL CONSTRAINT [DF_KoHdrH_AddendumKe]  DEFAULT ((0)),
	[ClosedBy] [nvarchar](30) NULL,
	[TimeClosed] [datetime] NULL,
	[AlamatKirim] [nvarchar](255) NULL,
	[NamaKirim] [nvarchar](100) NULL,
	[TeleponKirim] [nvarchar](20) NULL,
	[MaterialApproval] [char](1) NULL CONSTRAINT [DF_KoHdrH_MaterialApproval]  DEFAULT ((0)),
	[RAP] [char](1) NULL CONSTRAINT [DF_KoHdrH_RAP]  DEFAULT ((0)),
	[K3] [char](1) NULL CONSTRAINT [DF_KoHdrH_K3]  DEFAULT ((0)),
	[SyaratTeknis] [nvarchar](255) NULL,
	[SyaratPembayaran] [nvarchar](255) NULL,
	[JadwalPengiriman] [nvarchar](255) NULL,
	[Sanksi] [nvarchar](255) NULL,
	[JadwalPembayaran] [nvarchar](255) NULL,
	[Keterangan] [nvarchar](255) NULL,
	[QRCode] [nvarchar](6) NULL,
	[ApprovedBy] [nvarchar](30) NULL,
	[TimeApproved] [datetime] NULL,
	[UserEntry] [nvarchar](30) NULL,
	[TimeEntry] [datetime] NULL,
	[OverridePPN] [char](1) NULL CONSTRAINT [DF_KoHdrH_OverridePPN]  DEFAULT ((0)),
	[PDFFile] [nvarchar](1000) NULL,
	[CanceledBy] [nvarchar](30) NULL,
	[TimeCancel] [datetime] NULL,
	[CancelReason] [nvarchar](100) NULL,
 CONSTRAINT [PK_KoHdrH] PRIMARY KEY CLUSTERED 
(
	[NoKO] ASC,
	[TglKO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[LampiranKO]    Script Date: 5/11/2018 9:18:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LampiranKO](
	[NoPD] [nvarchar](20) NOT NULL,
	[NoUrut] [int] NULL,
	[KdRAP] [nvarchar](10) NULL,
	[Uraian] [nvarchar](255) NULL,
	[Vol] [decimal](10, 2) NULL,
	[Uom] [nvarchar](15) NULL,
	[HrgSatuan] [money] NULL,
	[UserEntry] [nvarchar](30) NULL,
	[TimeEntry] [datetime] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Login]    Script Date: 5/11/2018 9:18:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Login](
	[UserID] [nvarchar](20) NOT NULL,
	[UserName] [nvarchar](30) NOT NULL,
	[Password] [nvarchar](255) NULL,
	[AksesJob] [varchar](3000) NULL,
	[AksesAlokasi] [nvarchar](50) NULL,
	[UserEntry] [nvarchar](30) NULL,
	[TimeEntry] [datetime] NULL,
	[RekPengirim] [char](1) NULL CONSTRAINT [DF_Login_RekPengirim]  DEFAULT ((0)),
	[Vendor] [char](1) NULL CONSTRAINT [DF_Login_Vendor]  DEFAULT ((0)),
	[TipeForm] [char](1) NULL CONSTRAINT [DF_Login_TipeForm]  DEFAULT ((0)),
	[Kategori] [char](1) NULL CONSTRAINT [DF_Login_Kategori]  DEFAULT ((0)),
	[Proposal] [char](1) NULL CONSTRAINT [DF_Login_Prposal]  DEFAULT ((0)),
	[Job] [char](1) NULL CONSTRAINT [DF_Login_Job]  DEFAULT ((0)),
	[RAP] [char](1) NULL CONSTRAINT [DF_Login_RAP]  DEFAULT ((0)),
	[KO] [char](1) NULL CONSTRAINT [DF_Login_KO]  DEFAULT ((0)),
	[ApprovalKO] [char](1) NULL CONSTRAINT [DF_Login_ApprovalKO]  DEFAULT ((0)),
	[ClosingKO] [char](1) NULL CONSTRAINT [DF_Login_ClosingKO]  DEFAULT ((0)),
	[KOAddendum] [char](1) NULL CONSTRAINT [DF_Login_KOAddendum]  DEFAULT ((0)),
	[PD] [char](1) NULL CONSTRAINT [DF_Login_PD]  DEFAULT ((0)),
	[ApprovalPD_KK] [char](1) NULL CONSTRAINT [DF_Login_ApprovalPD_KK]  DEFAULT ((0)),
	[ApprovalPD_KT] [char](1) NULL CONSTRAINT [DF_Login_ApprovalPD_KT]  DEFAULT ((0)),
	[ApprovalPD_DP] [char](1) NULL CONSTRAINT [DF_Login_ApprovalPD_DP]  DEFAULT ((0)),
	[ApprovalPD_TBP] [char](1) NULL CONSTRAINT [DF_Login_ApprovalPD_TBP]  DEFAULT ((0)),
	[ApprovalPD_DK] [char](1) NULL CONSTRAINT [DF_Login_ApprovalPD_DK]  DEFAULT ((0)),
	[RejectPD] [char](1) NULL CONSTRAINT [DF_Login_RejectPD]  DEFAULT ((0)),
	[PJ] [char](1) NULL CONSTRAINT [DF_Login_PJ]  DEFAULT ((0)),
	[ApprovalPJ] [char](1) NULL CONSTRAINT [DF_Login_ApprovalPJ]  DEFAULT ((0)),
	[PayPD] [char](1) NULL CONSTRAINT [DF_Login_PayPD]  DEFAULT ((0)),
	[PayPDRKD] [char](1) NULL CONSTRAINT [DF_Login_PayPDRKD]  DEFAULT ((0)),
	[RPPM] [char](1) NULL CONSTRAINT [DF_Login_RPPM]  DEFAULT ((0)),
	[Termin] [char](1) NULL CONSTRAINT [DF_Login_Termin]  DEFAULT ((0)),
	[Jaminan] [char](1) NULL CONSTRAINT [DF_Login_Jaminan]  DEFAULT ((0)),
	[RekapKO] [char](1) NULL CONSTRAINT [DF_Login_RekapKO]  DEFAULT ((0)),
	[DPPD] [char](1) NULL CONSTRAINT [DF_Login_DPPD]  DEFAULT ((0)),
	[RekapPayment] [char](1) NULL CONSTRAINT [DF_Login_RekapPaymemt]  DEFAULT ((0)),
	[SerapRAP] [char](1) NULL CONSTRAINT [DF_Login_SerapRAP]  DEFAULT ((0)),
	[SRPengeluaran] [char](1) NULL CONSTRAINT [DF_Login_SRPengeluarang]  DEFAULT ((0)),
	[RealisasiTermin] [char](1) NULL CONSTRAINT [DF_Login_RealisasiTermin]  DEFAULT ((0)),
	[TrackPD] [char](1) NULL CONSTRAINT [DF_Login_TrackPD]  DEFAULT ((0)),
	[SetupAkses] [char](1) NULL CONSTRAINT [DF_Login_SetupAkses]  DEFAULT ((0)),
	[ChangePasswd] [char](1) NULL CONSTRAINT [DF_Login_ChangePasswd]  DEFAULT ((0)),
	[DataKaryawan] [char](1) NULL CONSTRAINT [DF_Login_Absensi]  DEFAULT ((0)),
	[HariLibur] [char](1) NULL CONSTRAINT [DF_Login_HariLibur]  DEFAULT ((0)),
	[LoadAbsensi] [char](1) NULL CONSTRAINT [DF_Login_LoadAbsensi]  DEFAULT ((0)),
	[EntryAbsensi] [char](1) NULL CONSTRAINT [DF_Login_EntryAbsensi]  DEFAULT ((0)),
	[RekapAbsensi] [char](1) NULL CONSTRAINT [DF_Login_RekapAbsensi]  DEFAULT ((0)),
	[AbsensiKaryawan] [char](1) NULL CONSTRAINT [DF_Login_AbsensiKaryawan]  DEFAULT ((0)),
	[Bank] [char](1) NULL CONSTRAINT [DF_Login_Bank]  DEFAULT ((0)),
	[Alokasi] [char](1) NULL CONSTRAINT [DF_Login_Alokasi]  DEFAULT ((0)),
	[OverrideSaldo] [char](1) NULL CONSTRAINT [DF_Login_OverrideSaldo]  DEFAULT ((0)),
	[COA] [char](1) NULL CONSTRAINT [DF_Login_COA]  DEFAULT ((0)),
	[GlReff] [char](1) NULL CONSTRAINT [DF_Login_GlReff]  DEFAULT ((0)),
	[Identitas] [char](1) NULL CONSTRAINT [DF_Login_Identitas]  DEFAULT ((0)),
	[JurnalEntry] [char](1) NULL CONSTRAINT [DF_Login_JurnalEntry]  DEFAULT ((0)),
	[JurnalApproval] [char](1) NULL CONSTRAINT [DF_Login_JurnalApproval]  DEFAULT ((0)),
	[ResumePK] [char](1) NULL CONSTRAINT [DF_Login_ResumePK]  DEFAULT ((0)),
	[NeracaMutasi] [char](1) NULL CONSTRAINT [DF__Login__NeracaMut__116138B1]  DEFAULT ((0)),
	[NotaAkuntansi] [char](1) NULL CONSTRAINT [DF__Login__NotaAkunt__12555CEA]  DEFAULT ((0)),
	[BukuTambahan] [char](1) NULL CONSTRAINT [DF__Login__BukuTamba__13498123]  DEFAULT ((0)),
	[BukuBesar] [char](1) NULL CONSTRAINT [DF__Login__BukuBesar__143DA55C]  DEFAULT ((0)),
	[JurnalExport] [char](1) NULL CONSTRAINT [DF__Login__JurnalExp__1531C995]  DEFAULT ((0)),
	[LapKeu] [char](1) NULL CONSTRAINT [DF__Login__LapKeu__1625EDCE]  DEFAULT ((0)),
	[LabaRugi] [char](1) NULL CONSTRAINT [DF__Login__LabaRugi__171A1207]  DEFAULT ((0)),
	[RekapStatusAbsensi] [char](1) NULL CONSTRAINT [DF_Login_RekapStatusAbsensi]  DEFAULT ((0)),
	[TrackingKO] [char](1) NULL CONSTRAINT [DF_Login_TrackingKO]  DEFAULT ((0)),
	[RptTrackingKO] [char](1) NULL CONSTRAINT [DF_Login_RptTrackingKO]  DEFAULT ((0)),
	[CPanel] [char](1) NULL CONSTRAINT [DF_Login_CPanel]  DEFAULT ((0)),
	[Invoice] [char](1) NULL CONSTRAINT [DF_Login_Invoice]  DEFAULT ((0)),
	[InvoiceReceipt] [char](1) NULL CONSTRAINT [DF_Login_InvoiceReceipt]  DEFAULT ((0)),
	[ItemPrice] [char](1) NULL CONSTRAINT [DF_Login_ItemPrice]  DEFAULT ((0)),
	[CancelPO] [char](1) NULL CONSTRAINT [DF_Login_CancelPO]  DEFAULT ((0)),
	[NPWP] [char](1) NULL CONSTRAINT [DF_Login_NPWP]  DEFAULT ((0)),
	[ApprovalTermin] [char](1) NULL CONSTRAINT [DF_Login_ApprovalTermin]  DEFAULT ((0)),
	[CashFlow] [char](1) NULL CONSTRAINT [DF_Login_CashFlow]  DEFAULT ((0)),
	[QueryAP] [char](1) NULL CONSTRAINT [DF_Login_QueryAP]  DEFAULT ((0)),
	[QueryPD] [char](1) NULL CONSTRAINT [DF_Login_QueryPD]  DEFAULT ((0)),
	[QueryJurnal] [char](1) NULL CONSTRAINT [DF_Login_QueryJurnal]  DEFAULT ((0)),
	[SerapRAPKO] [char](1) NULL CONSTRAINT [DF_Login_SerapRAPKO]  DEFAULT ((0)),
	[QueryTermin] [char](1) NULL CONSTRAINT [DF_Login_QueryTermin]  DEFAULT ((0)),
	[RencanaTermin] [char](1) NULL CONSTRAINT [DF_Login_RencanaTermin]  DEFAULT ((0)),
 CONSTRAINT [PK_Login] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[mKaryawan]    Script Date: 5/11/2018 9:18:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[mKaryawan](
	[nip] [nvarchar](6) NOT NULL,
	[nmKaryawan] [nvarchar](100) NOT NULL,
	[kk] [nvarchar](16) NULL,
	[email] [nvarchar](50) NULL,
	[alamat] [nvarchar](200) NULL,
	[kelurahan] [nvarchar](30) NULL,
	[kecamatan] [nvarchar](30) NULL,
	[kota] [nvarchar](30) NULL,
	[kodepos] [nvarchar](10) NULL,
	[telepon] [nvarchar](20) NULL,
	[hp] [nvarchar](20) NULL,
	[kelamin] [char](1) NULL,
	[tmpLahir] [nvarchar](30) NULL,
	[tglLahir] [date] NULL,
	[stsNikah] [nvarchar](3) NULL,
	[agama] [nvarchar](10) NULL,
	[pendidikan] [nvarchar](5) NULL,
	[jurusan] [nvarchar](20) NULL,
	[wn] [nvarchar](15) NULL,
	[suku] [nvarchar](20) NULL,
	[ktp] [nvarchar](16) NULL,
	[tglMasuk] [date] NULL,
	[tglKeluar] [date] NULL,
	[alasanKeluar] [nvarchar](200) NULL,
	[kontrakAwal] [date] NULL,
	[kontrakAkhir] [date] NULL,
	[klasifikasi] [nvarchar](10) NULL,
	[lokasi] [nvarchar](30) NULL,
	[stsKaryawan] [nvarchar](10) NULL,
	[ptkp] [nvarchar](3) NULL,
	[npwp] [nvarchar](20) NULL,
	[bpjs] [nvarchar](20) NULL,
	[noKPA] [nvarchar](20) NULL,
	[tglKPA] [date] NULL,
	[jkk] [nvarchar](20) NULL,
	[jkm] [nvarchar](20) NULL,
	[jht] [nvarchar](20) NULL,
	[jp] [nvarchar](20) NULL,
	[stsPerjanjian] [nvarchar](5) NULL,
	[terdaftar] [nvarchar](10) NULL,
	[bank] [nvarchar](20) NULL,
	[noRek] [nvarchar](30) NULL,
	[cabang] [nvarchar](20) NULL,
	[atasNm] [nvarchar](50) NULL,
	[userEntry] [nvarchar](50) NULL,
	[timeOfEntry] [datetime] NULL,
 CONSTRAINT [PK_mKaryawan] PRIMARY KEY CLUSTERED 
(
	[nip] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MutasiAcc]    Script Date: 5/11/2018 9:18:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MutasiAcc](
	[JobNo] [nvarchar](10) NOT NULL,
	[Site] [nvarchar](8) NOT NULL,
	[AccNo] [nvarchar](10) NOT NULL,
	[Tahun] [int] NOT NULL,
	[Bulan] [int] NOT NULL,
	[Debet] [money] NULL,
	[Kredit] [money] NULL,
	[UserEntry] [nvarchar](30) NULL,
	[TimeEntry] [datetime] NULL,
 CONSTRAINT [PK_Mutasi] PRIMARY KEY CLUSTERED 
(
	[JobNo] ASC,
	[Site] ASC,
	[AccNo] ASC,
	[Tahun] ASC,
	[Bulan] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PdDtl]    Script Date: 5/11/2018 9:18:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PdDtl](
	[NoPD] [nvarchar](20) NOT NULL,
	[NoUrut] [int] NULL,
	[KdRAP] [nvarchar](10) NULL,
	[Uraian] [nvarchar](300) NULL,
	[Vol] [decimal](10, 3) NULL CONSTRAINT [DF__PdDtl__Vol__4B8221F7]  DEFAULT ((0)),
	[Uom] [nvarchar](15) NULL,
	[HrgSatuan] [money] NULL CONSTRAINT [DF__PdDtl__HrgSatuan__4C764630]  DEFAULT ((0)),
	[UserEntry] [nvarchar](30) NULL,
	[TimeEntry] [datetime] NULL,
	[NoPJ] [nvarchar](15) NULL,
	[PjUraian] [nvarchar](2000) NULL,
	[PjVol] [decimal](10, 3) NULL CONSTRAINT [DF__PdDtl__PjVol__4D6A6A69]  DEFAULT ((0)),
	[PjHrgSatuan] [money] NULL CONSTRAINT [DF__PdDtl__PjHrgSatu__4E5E8EA2]  DEFAULT ((0)),
	[PjUserEntry] [nvarchar](30) NULL,
	[PjTimeEntry] [datetime] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PdHdr]    Script Date: 5/11/2018 9:18:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PdHdr](
	[NoPD] [nvarchar](20) NOT NULL,
	[TglPD] [date] NOT NULL,
	[JobNo] [nvarchar](10) NULL,
	[KSO] [int] NULL CONSTRAINT [DF_PdHdr_KSO]  DEFAULT ((0)),
	[Deskripsi] [nvarchar](255) NULL,
	[NoRef] [nvarchar](50) NULL,
	[PrdAwal] [date] NULL,
	[PrdAkhir] [date] NULL,
	[Minggu] [int] NULL CONSTRAINT [DF_PdHdr_Minggu]  DEFAULT ((0)),
	[Alokasi] [nvarchar](1) NULL,
	[TipeForm] [nvarchar](3) NULL,
	[NoKO] [nvarchar](15) NULL,
	[NoTagihan] [nvarchar](100) NULL,
	[Nama] [nvarchar](100) NULL,
	[Alamat] [nvarchar](255) NULL,
	[Telepon] [nvarchar](20) NULL,
	[NPWP] [nvarchar](20) NULL,
	[NoRek] [nvarchar](30) NULL,
	[AtasNama] [nvarchar](100) NULL,
	[Bank] [nvarchar](20) NULL,
	[TotalPD] [money] NULL CONSTRAINT [DF_PdHdr_TotalPD]  DEFAULT ((0)),
	[BuktiPendukung] [nvarchar](255) NULL,
	[ApprovedByKK] [nvarchar](30) NULL,
	[TimeApprovedKK] [datetime] NULL,
	[UserEntry] [nvarchar](30) NULL,
	[TimeEntry] [datetime] NULL,
	[NoPJ] [nvarchar](15) NULL,
	[TglPJ] [date] NULL,
	[TotalPJ] [money] NULL CONSTRAINT [DF_PdHdr_TotalPJ]  DEFAULT ((0)),
	[Saldo] [money] NULL CONSTRAINT [DF_PdHdr_Saldo]  DEFAULT ((0)),
	[OverrideSaldo] [char](1) NULL CONSTRAINT [DF_PdHdr_OverrideSaldo]  DEFAULT ((0)),
	[RemarkOverrideSaldo] [nvarchar](255) NULL,
	[PjApprovedByAK] [nvarchar](30) NULL,
	[PjTimeApprovedAK] [datetime] NULL,
	[PjUserEntry] [nvarchar](30) NULL,
	[PjTimeEntry] [datetime] NULL,
	[ApprovedByKT] [nvarchar](30) NULL,
	[TimeApprovedKT] [datetime] NULL,
	[ApprovedByDP] [nvarchar](30) NULL,
	[TimeApprovedDP] [datetime] NULL,
	[ApprovedByTBP] [nvarchar](30) NULL,
	[TimeApprovedTBP] [datetime] NULL,
	[ApprovedByDK] [nvarchar](30) NULL,
	[TimeApprovedDK] [datetime] NULL,
	[ApprovedByAK] [nvarchar](30) NULL,
	[TimeApprovedAK] [datetime] NULL,
	[RejectBy] [nvarchar](30) NULL,
	[TimeReject] [datetime] NULL,
	[PjApprovedByKK] [nvarchar](30) NULL,
	[PjTimeApprovedKK] [datetime] NULL,
	[AlasanReject] [nvarchar](255) NULL,
 CONSTRAINT [PK_PdHdr] PRIMARY KEY CLUSTERED 
(
	[NoPD] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Progress]    Script Date: 5/11/2018 9:18:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Progress](
	[JobNo] [nvarchar](10) NOT NULL,
	[Tahun] [int] NOT NULL CONSTRAINT [DF_Progress_Tahun]  DEFAULT ((0)),
	[Bulan] [int] NOT NULL CONSTRAINT [DF_Progress_Bulan]  DEFAULT ((0)),
	[RencanaK] [decimal](6, 3) NULL CONSTRAINT [DF_Progress_RencancaK]  DEFAULT ((0)),
	[RealisasiK] [decimal](6, 3) NULL CONSTRAINT [DF_Progress_RealisasiK]  DEFAULT ((0)),
	[RealisasiKeuK] [decimal](6, 3) NULL CONSTRAINT [DF_Progress_RealisasiKeuK]  DEFAULT ((0)),
	[RencanaTB] [decimal](6, 3) NULL CONSTRAINT [DF_Progress_RencancaTB]  DEFAULT ((0)),
	[RealisasiTB] [decimal](6, 3) NULL CONSTRAINT [DF_Progress_RealisasiTB]  DEFAULT ((0)),
	[RealisasiKeuTB] [decimal](6, 3) NULL CONSTRAINT [DF_Progress_RealisasiKeuTB]  DEFAULT ((0)),
	[UserEntry] [nvarchar](30) NULL,
	[TimeEntry] [datetime] NULL,
 CONSTRAINT [PK_Progress] PRIMARY KEY CLUSTERED 
(
	[JobNo] ASC,
	[Tahun] ASC,
	[Bulan] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RAP]    Script Date: 5/11/2018 9:18:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RAP](
	[JobNo] [nvarchar](10) NOT NULL,
	[KdRAP] [nvarchar](15) NOT NULL,
	[Versi] [nvarchar](10) NULL,
	[NoUrut] [int] NULL CONSTRAINT [DF_RAP_NoUrut]  DEFAULT ((0)),
	[Uraian] [nvarchar](200) NULL,
	[Alokasi] [nvarchar](1) NOT NULL,
	[Tipe] [nvarchar](6) NULL,
	[Header] [nvarchar](10) NULL CONSTRAINT [DF_RAP_Header]  DEFAULT ((0)),
	[Uom] [nvarchar](15) NULL,
	[Vol] [decimal](9, 2) NULL CONSTRAINT [DF_RAP_vol]  DEFAULT ((0)),
	[HrgSatuan] [money] NULL CONSTRAINT [DF_RAP_HrgSatuan]  DEFAULT ((0)),
	[HrgRAB] [money] NULL CONSTRAINT [DF_RAP_HrgRAB]  DEFAULT ((0)),
	[TotalTerserap] [money] NULL CONSTRAINT [DF_RAP_TotalTerserap]  DEFAULT ((0)),
	[UserEntry] [nvarchar](30) NULL,
	[TimeEntry] [datetime] NULL,
 CONSTRAINT [PK_RAP_1] PRIMARY KEY CLUSTERED 
(
	[JobNo] ASC,
	[KdRAP] ASC,
	[Alokasi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RAPH]    Script Date: 5/11/2018 9:18:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RAPH](
	[JobNo] [nvarchar](10) NOT NULL,
	[KdRAP] [nvarchar](10) NOT NULL,
	[Versi] [nvarchar](10) NOT NULL,
	[NoUrut] [int] NULL CONSTRAINT [DF_RAPH_NoUrut]  DEFAULT ((0)),
	[Uraian] [nvarchar](200) NULL,
	[Alokasi] [nvarchar](1) NULL,
	[Tipe] [nvarchar](6) NULL,
	[Header] [nvarchar](10) NULL,
	[Uom] [nvarchar](15) NULL,
	[Vol] [decimal](9, 2) NULL CONSTRAINT [DF_RAPH_vol]  DEFAULT ((0)),
	[HrgSatuan] [money] NULL CONSTRAINT [DF_RAPH_HrgSatuan]  DEFAULT ((0)),
	[HrgRAB] [money] NULL CONSTRAINT [DF_RAPH_HrgRAB]  DEFAULT ((0)),
	[TotalTerserap] [money] NULL CONSTRAINT [DF_RAPH_TotalTerserao]  DEFAULT ((0)),
	[UserEntry] [nvarchar](30) NULL,
	[TimeEntry] [datetime] NULL,
 CONSTRAINT [PK_RAPH] PRIMARY KEY CLUSTERED 
(
	[JobNo] ASC,
	[KdRAP] ASC,
	[Versi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Rekening]    Script Date: 5/11/2018 9:18:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rekening](
	[RekId] [nvarchar](50) NOT NULL,
	[Bank] [nvarchar](20) NULL,
	[NoRek] [nvarchar](30) NULL,
	[AtasNama] [nvarchar](200) NULL,
	[UserEntry] [nvarchar](30) NULL,
	[TimeEntry] [datetime] NULL,
 CONSTRAINT [PK_Rekening] PRIMARY KEY CLUSTERED 
(
	[RekId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RencanaTermin]    Script Date: 5/11/2018 9:18:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RencanaTermin](
	[LedgerNo] [bigint] IDENTITY(1,1) NOT NULL,
	[JobNo] [nvarchar](10) NOT NULL,
	[Jenis] [char](10) NULL,
	[TglRencana] [date] NULL,
	[Uraian] [nvarchar](255) NULL,
	[Persentase] [decimal](5, 2) NULL,
	[Bruto] [money] NULL,
	[BrutoRealisasiLalu] [money] NULL,
	[PersentaseUM] [decimal](5, 2) NULL,
	[Netto] [money] NULL,
	[UserEntry] [nvarchar](30) NULL,
	[TimeEntry] [datetime] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RPPM]    Script Date: 5/11/2018 9:18:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RPPM](
	[JobNo] [nvarchar](10) NOT NULL,
	[Minggu] [int] NOT NULL,
	[Bobot] [decimal](5, 2) NULL,
	[UserEntry] [nvarchar](30) NULL,
	[TimeEntry] [datetime] NULL,
 CONSTRAINT [PK_RPPM] PRIMARY KEY CLUSTERED 
(
	[JobNo] ASC,
	[Minggu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Saldo]    Script Date: 5/11/2018 9:18:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Saldo](
	[JobNo] [nvarchar](10) NOT NULL,
	[Tipe] [nvarchar](10) NOT NULL,
	[TglSaldo] [date] NOT NULL,
	[Nominal] [money] NULL,
	[UserEntry] [nvarchar](30) NULL,
	[TimeEntry] [datetime] NULL,
 CONSTRAINT [PK_Saldo] PRIMARY KEY CLUSTERED 
(
	[JobNo] ASC,
	[Tipe] ASC,
	[TglSaldo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TerminInduk]    Script Date: 5/11/2018 9:18:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TerminInduk](
	[LedgerNo] [bigint] IDENTITY(1,1) NOT NULL,
	[JobNo] [nvarchar](10) NOT NULL,
	[Jenis] [char](10) NULL CONSTRAINT [DF_TerminInduk_Jenis]  DEFAULT (N'Termin'),
	[TglCair] [date] NOT NULL,
	[NoBAP] [nvarchar](50) NULL,
	[Uraian] [nvarchar](255) NULL,
	[TerminInduk] [money] NULL CONSTRAINT [DF_TerminInduk_TerminInduk]  DEFAULT ((0)),
	[BrutoBOQ] [money] NULL CONSTRAINT [DF_TerminInduk_BrutoBOQ]  DEFAULT ((0)),
	[UM] [money] NULL CONSTRAINT [DF_TerminInduk_UM]  DEFAULT ((0)),
	[Retensi] [money] NULL CONSTRAINT [DF_TerminInduk_Retensi]  DEFAULT ((0)),
	[UserEntry] [nvarchar](30) NULL,
	[TimeEntry] [datetime] NULL,
	[UserApproval] [nvarchar](30) NULL,
	[TimeApproval] [datetime] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TerminMember]    Script Date: 5/11/2018 9:18:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TerminMember](
	[LedgerNo] [bigint] IDENTITY(1,1) NOT NULL,
	[JobNo] [nvarchar](10) NOT NULL,
	[TglCair] [date] NOT NULL,
	[NoBAP] [nvarchar](50) NULL,
	[Uraian] [nvarchar](255) NULL,
	[TerminMember1] [money] NULL CONSTRAINT [DF_TerminMember_TerminMember1]  DEFAULT ((0)),
	[TerminMember2] [money] NULL CONSTRAINT [DF_TerminMember_TerminMember2]  DEFAULT ((0)),
	[CadanganKSO] [money] NULL CONSTRAINT [DF_TerminMember_CadanganKSO]  DEFAULT ((0)),
	[UserEntry] [nvarchar](30) NULL,
	[TimeEntry] [datetime] NULL,
	[UserApproval] [nvarchar](30) NULL,
	[TimeApproval] [datetime] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TipeForm]    Script Date: 5/11/2018 9:18:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipeForm](
	[TipeForm] [nvarchar](3) NOT NULL,
	[Alokasi] [nvarchar](1) NULL,
	[Keterangan] [nvarchar](100) NULL,
	[UserEntry] [nvarchar](30) NULL,
	[TimeEntry] [datetime] NULL,
 CONSTRAINT [PK_Form] PRIMARY KEY CLUSTERED 
(
	[TipeForm] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TmpBLE]    Script Date: 5/11/2018 9:18:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TmpBLE](
	[LedgerNo] [bigint] IDENTITY(1,1) NOT NULL,
	[TglBayar] [date] NULL,
	[NoPD] [nvarchar](50) NULL,
	[JobNo] [nvarchar](10) NULL,
	[Keterangan] [nvarchar](255) NULL,
	[Alokasi] [nvarchar](50) NULL,
	[TipeForm] [nvarchar](50) NULL,
	[NoKO] [nvarchar](15) NULL,
	[RekId] [nvarchar](50) NULL,
	[CaraBayar] [nvarchar](50) NULL,
	[JenisTrf] [nvarchar](50) NULL,
	[NoCG] [nvarchar](50) NULL,
	[NoRek] [nvarchar](30) NULL,
	[AtasNama] [nvarchar](100) NULL,
	[Bank] [nvarchar](20) NULL,
	[Amount] [money] NULL CONSTRAINT [DF_TmpBLE_Amount]  DEFAULT ((0)),
	[NmPenerimaTunai] [nvarchar](50) NULL,
	[SumberKas] [nvarchar](10) NULL,
	[UserEntry] [nvarchar](30) NULL,
	[TimeEntry] [datetime] NULL,
 CONSTRAINT [PK_TmpBLE] PRIMARY KEY CLUSTERED 
(
	[LedgerNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TraceKO]    Script Date: 5/11/2018 9:18:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TraceKO](
	[NoKO] [nvarchar](15) NOT NULL,
	[Trace#] [bigint] IDENTITY(1,1) NOT NULL,
	[Keterangan] [nvarchar](255) NULL,
	[Tanggal] [date] NULL,
	[Status] [char](1) NULL CONSTRAINT [DF_TraceKO_Status]  DEFAULT ((0)),
	[UserEntry] [nvarchar](30) NULL,
	[TimeEntry] [datetime] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Vendor]    Script Date: 5/11/2018 9:18:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Vendor](
	[VendorId] [nvarchar](7) NOT NULL,
	[VendorNm] [nvarchar](100) NULL,
	[Alamat] [nvarchar](255) NULL,
	[Kota] [nvarchar](50) NULL,
	[Propinsi] [nvarchar](50) NULL,
	[Telepon1] [nvarchar](20) NULL,
	[Telepon2] [nvarchar](20) NULL,
	[Telepon3] [nvarchar](20) NULL,
	[Fax] [nvarchar](20) NULL,
	[Email1] [nvarchar](100) NULL,
	[Email2] [nvarchar](100) NULL,
	[Email3] [nvarchar](100) NULL,
	[ContactPerson] [nvarchar](100) NULL,
	[NPWP] [nvarchar](30) NULL,
	[PKP] [char](1) NULL CONSTRAINT [DF_Vendor_PKP]  DEFAULT ((0)),
	[Kategori] [nvarchar](30) NULL,
	[BidangUsaha] [nvarchar](50) NULL,
	[NoRek] [nvarchar](30) NULL,
	[Bank] [nvarchar](20) NULL,
	[AtasNama] [nvarchar](100) NULL,
	[UserEntry] [nvarchar](30) NULL,
	[TimeEntry] [datetime] NULL,
 CONSTRAINT [PK_Vendor] PRIMARY KEY CLUSTERED 
(
	[VendorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[GlDtl] ADD  CONSTRAINT [DF_GlDtl_Debet]  DEFAULT ((0)) FOR [Debet]
GO
ALTER TABLE [dbo].[GlDtl] ADD  CONSTRAINT [DF_GlDtl_Kredit]  DEFAULT ((0)) FOR [Kredit]
GO
ALTER TABLE [dbo].[JobH] ADD  CONSTRAINT [DF_JobH_AddendumKe]  DEFAULT ((0)) FOR [AddendumKe]
GO
ALTER TABLE [dbo].[JobH] ADD  CONSTRAINT [DF_JobH_Hari]  DEFAULT ((0)) FOR [Hari]
GO
ALTER TABLE [dbo].[LampiranKO] ADD  CONSTRAINT [DF_LampiranKO_Vol]  DEFAULT ((0)) FOR [Vol]
GO
ALTER TABLE [dbo].[LampiranKO] ADD  CONSTRAINT [DF_LampiranKO_HrgSatuan]  DEFAULT ((0)) FOR [HrgSatuan]
GO
ALTER TABLE [dbo].[RencanaTermin] ADD  CONSTRAINT [DF_RencanaTermin_Jenis]  DEFAULT ('Termin') FOR [Jenis]
GO
ALTER TABLE [dbo].[RencanaTermin] ADD  CONSTRAINT [DF_RencanaTermin_Persentase]  DEFAULT ((0)) FOR [Persentase]
GO
ALTER TABLE [dbo].[RencanaTermin] ADD  CONSTRAINT [DF_RencanaTermin_Netto]  DEFAULT ((0)) FOR [Netto]
GO
ALTER TABLE [dbo].[RPPM] ADD  CONSTRAINT [DF_RPPM_Minggu]  DEFAULT ((0)) FOR [Minggu]
GO
ALTER TABLE [dbo].[RPPM] ADD  CONSTRAINT [DF_RPPM_Bobot]  DEFAULT ((0)) FOR [Bobot]
GO
ALTER TABLE [dbo].[Saldo] ADD  CONSTRAINT [DF_Saldo_Nominal]  DEFAULT ((0)) FOR [Nominal]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Persentase KSO' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Job', @level2type=N'COLUMN',@level2name=N'PersenKSO'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description1', @value=N'Persentase KSO' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Job', @level2type=N'COLUMN',@level2name=N'PersenKSO'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'KoHdr', @level2type=N'COLUMN',@level2name=N'NoKO'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Hanya KO Jenis Kontrak.
Jika 0 - Belum Closed
Jika 1 - Sudah Closed' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'KoHdr', @level2type=N'COLUMN',@level2name=N'ClosedBy'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1 = Sudah konfirmasi material approval oleh Direksi Tehnik
0 = Belum konfirmasi
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'KoHdr', @level2type=N'COLUMN',@level2name=N'MaterialApproval'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1 = Sudah ada RAP
0 = Belum ada RAP' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'KoHdr', @level2type=N'COLUMN',@level2name=N'RAP'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Header - Untuk Judul
Detail - Bagian dari Header' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RAP', @level2type=N'COLUMN',@level2name=N'Tipe'
GO
