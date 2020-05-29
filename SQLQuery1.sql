USE [Fotografa]
GO

/****** Object: Table [dbo].[Foto] Script Date: 24/05/2019 22:18:43 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
drop table Foto
CREATE TABLE [dbo].[Foto] (
    [ID]        UNIQUEIDENTIFIER NOT NULL,
	Album		Uniqueidentifier not null,
    [Nome]      NVARCHAR (MAX)   NULL,
    [Descricao] NVARCHAR (MAX)   NULL,
    [Imagem]    VARBINARY (MAX)  NULL
);


