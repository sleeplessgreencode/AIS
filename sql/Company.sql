USE [AIS]
GO
/****** Object:  Table [dbo].[Company]    Script Date: 11/10/2017 9:18:37 AM ******/
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
