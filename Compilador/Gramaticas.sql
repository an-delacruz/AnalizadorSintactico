USE [AUTOMATAS]
GO
/****** Object:  Table [dbo].[Gramaticas]    Script Date: 28/10/2021 08:11:41 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Gramaticas](
	[Simbolo] [nvarchar](50) NOT NULL,
	[Gramatica] [nvarchar](100) NOT NULL
) ON [PRIMARY]
GO
