USE [GiustiChat]
GO
/****** Object:  Table [dbo].[Email]    Script Date: 3/2/2016 12:34:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Email]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Email](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Destinatario] [varchar](100) NOT NULL,
	[Assunto] [varchar](50) NOT NULL,
	[Corpo] [varchar](1000) NOT NULL,
	[FuncionalidadeId] [int] NOT NULL,
	[DataInclusao] [datetime] NOT NULL,
	[DataAlteracao] [datetime] NULL,
	[DataEnvio] [datetime] NULL,
 CONSTRAINT [PK_Email] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Funcionalidade]    Script Date: 3/2/2016 12:34:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Funcionalidade]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Funcionalidade](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](50) NOT NULL,
	[FuncionalidadeIdPai] [int] NULL,
	[UtilizaMenu] [bit] NOT NULL,
	[LinkMenu] [varchar](50) NULL,
 CONSTRAINT [PK_Funcionalidade] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Log]    Script Date: 3/2/2016 12:34:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Log]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Log](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UsuarioId] [int] NOT NULL,
	[RegistroId] [int] NULL,
	[Acao] [varchar](50) NOT NULL,
	[OrigemAcesso] [varchar](250) NOT NULL,
	[IpMaquina] [varchar](50) NOT NULL,
	[DataInclusao] [datetime] NOT NULL,
 CONSTRAINT [PK_Log] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Perfil]    Script Date: 3/2/2016 12:34:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Perfil]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Perfil](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Perfil] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PerfilFuncionalidade]    Script Date: 3/2/2016 12:34:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PerfilFuncionalidade]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PerfilFuncionalidade](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PerfilId] [int] NOT NULL,
	[FuncionalidadeId] [int] NOT NULL,
 CONSTRAINT [PK_Acesso] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[PerfilUsuario]    Script Date: 3/2/2016 12:34:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PerfilUsuario]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PerfilUsuario](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PerfilId] [int] NOT NULL,
	[UsuarioId] [int] NOT NULL,
 CONSTRAINT [PK_PerfilUsuario] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 3/2/2016 12:34:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Usuario]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Usuario](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](100) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[Senha] [varchar](300) NOT NULL,
	[Ativo] [bit] NOT NULL,
	[DataInclusao] [datetime] NOT NULL,
	[DataAlteracao] [datetime] NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Funcionalidade] ON 

GO
INSERT [dbo].[Funcionalidade] ([Id], [Nome], [FuncionalidadeIdPai], [UtilizaMenu], [LinkMenu]) VALUES (1, N'Acesso', NULL, 1, N'acesso')
GO
INSERT [dbo].[Funcionalidade] ([Id], [Nome], [FuncionalidadeIdPai], [UtilizaMenu], [LinkMenu]) VALUES (2, N'Perfil', 1, 1, N'perfil')
GO
INSERT [dbo].[Funcionalidade] ([Id], [Nome], [FuncionalidadeIdPai], [UtilizaMenu], [LinkMenu]) VALUES (3, N'Perfil Consulta', 2, 0, NULL)
GO
INSERT [dbo].[Funcionalidade] ([Id], [Nome], [FuncionalidadeIdPai], [UtilizaMenu], [LinkMenu]) VALUES (4, N'Perfil Edição', 2, 0, NULL)
GO
INSERT [dbo].[Funcionalidade] ([Id], [Nome], [FuncionalidadeIdPai], [UtilizaMenu], [LinkMenu]) VALUES (5, N'Perfil Inclusão', 2, 0, NULL)
GO
INSERT [dbo].[Funcionalidade] ([Id], [Nome], [FuncionalidadeIdPai], [UtilizaMenu], [LinkMenu]) VALUES (6, N'Perfil Exclusão', 2, 0, NULL)
GO
INSERT [dbo].[Funcionalidade] ([Id], [Nome], [FuncionalidadeIdPai], [UtilizaMenu], [LinkMenu]) VALUES (7, N'Funcionalidade', 1, 0, NULL)
GO
INSERT [dbo].[Funcionalidade] ([Id], [Nome], [FuncionalidadeIdPai], [UtilizaMenu], [LinkMenu]) VALUES (8, N'Funcionalidade Consulta', 7, 0, NULL)
GO
INSERT [dbo].[Funcionalidade] ([Id], [Nome], [FuncionalidadeIdPai], [UtilizaMenu], [LinkMenu]) VALUES (9, N'Usuário', 1, 1, N'usuario')
GO
INSERT [dbo].[Funcionalidade] ([Id], [Nome], [FuncionalidadeIdPai], [UtilizaMenu], [LinkMenu]) VALUES (10, N'Usuário Consulta', 9, 0, NULL)
GO
INSERT [dbo].[Funcionalidade] ([Id], [Nome], [FuncionalidadeIdPai], [UtilizaMenu], [LinkMenu]) VALUES (11, N'Usuário Edição', 9, 0, NULL)
GO
INSERT [dbo].[Funcionalidade] ([Id], [Nome], [FuncionalidadeIdPai], [UtilizaMenu], [LinkMenu]) VALUES (12, N'Usuário Inclusão', 9, 0, NULL)
GO
INSERT [dbo].[Funcionalidade] ([Id], [Nome], [FuncionalidadeIdPai], [UtilizaMenu], [LinkMenu]) VALUES (13, N'Usuário Exclusão', 9, 0, NULL)
GO
INSERT [dbo].[Funcionalidade] ([Id], [Nome], [FuncionalidadeIdPai], [UtilizaMenu], [LinkMenu]) VALUES (14, N'Alterar Senha', 1, 1, N'alterarsenha')
GO
INSERT [dbo].[Funcionalidade] ([Id], [Nome], [FuncionalidadeIdPai], [UtilizaMenu], [LinkMenu]) VALUES (16, N'Log', 1, 1, N'log')
GO
INSERT [dbo].[Funcionalidade] ([Id], [Nome], [FuncionalidadeIdPai], [UtilizaMenu], [LinkMenu]) VALUES (17, N'Log Consulta', 16, 0, NULL)
GO
SET IDENTITY_INSERT [dbo].[Funcionalidade] OFF
GO
SET IDENTITY_INSERT [dbo].[Perfil] ON 

GO
INSERT [dbo].[Perfil] ([Id], [Nome]) VALUES (1, N'Master')
GO
SET IDENTITY_INSERT [dbo].[Perfil] OFF
GO
SET IDENTITY_INSERT [dbo].[PerfilFuncionalidade] ON 

GO
INSERT [dbo].[PerfilFuncionalidade] ([Id], [PerfilId], [FuncionalidadeId]) VALUES (1, 1, 1)
GO
INSERT [dbo].[PerfilFuncionalidade] ([Id], [PerfilId], [FuncionalidadeId]) VALUES (2, 1, 2)
GO
INSERT [dbo].[PerfilFuncionalidade] ([Id], [PerfilId], [FuncionalidadeId]) VALUES (3, 1, 3)
GO
INSERT [dbo].[PerfilFuncionalidade] ([Id], [PerfilId], [FuncionalidadeId]) VALUES (4, 1, 4)
GO
INSERT [dbo].[PerfilFuncionalidade] ([Id], [PerfilId], [FuncionalidadeId]) VALUES (5, 1, 5)
GO
INSERT [dbo].[PerfilFuncionalidade] ([Id], [PerfilId], [FuncionalidadeId]) VALUES (6, 1, 6)
GO
INSERT [dbo].[PerfilFuncionalidade] ([Id], [PerfilId], [FuncionalidadeId]) VALUES (7, 1, 7)
GO
INSERT [dbo].[PerfilFuncionalidade] ([Id], [PerfilId], [FuncionalidadeId]) VALUES (8, 1, 8)
GO
INSERT [dbo].[PerfilFuncionalidade] ([Id], [PerfilId], [FuncionalidadeId]) VALUES (9, 1, 9)
GO
INSERT [dbo].[PerfilFuncionalidade] ([Id], [PerfilId], [FuncionalidadeId]) VALUES (10, 1, 10)
GO
INSERT [dbo].[PerfilFuncionalidade] ([Id], [PerfilId], [FuncionalidadeId]) VALUES (11, 1, 11)
GO
INSERT [dbo].[PerfilFuncionalidade] ([Id], [PerfilId], [FuncionalidadeId]) VALUES (12, 1, 12)
GO
INSERT [dbo].[PerfilFuncionalidade] ([Id], [PerfilId], [FuncionalidadeId]) VALUES (13, 1, 13)
GO
INSERT [dbo].[PerfilFuncionalidade] ([Id], [PerfilId], [FuncionalidadeId]) VALUES (63, 1, 14)
GO
INSERT [dbo].[PerfilFuncionalidade] ([Id], [PerfilId], [FuncionalidadeId]) VALUES (64, 1, 16)
GO
INSERT [dbo].[PerfilFuncionalidade] ([Id], [PerfilId], [FuncionalidadeId]) VALUES (65, 1, 17)
GO
SET IDENTITY_INSERT [dbo].[PerfilFuncionalidade] OFF
GO
SET IDENTITY_INSERT [dbo].[PerfilUsuario] ON 

GO
INSERT [dbo].[PerfilUsuario] ([Id], [PerfilId], [UsuarioId]) VALUES (29, 1, 1)
GO
SET IDENTITY_INSERT [dbo].[PerfilUsuario] OFF
GO
SET IDENTITY_INSERT [dbo].[Usuario] ON 

GO
INSERT [dbo].[Usuario] ([Id], [Nome], [Email], [Senha], [Ativo], [DataInclusao], [DataAlteracao]) VALUES (1, N'Lucas Giusti', N'giusti.lucas@gmail.com', N'1000:fXJZy1QqQ13JMTNQFiLN8Jsosc5rWT0P:5CHJXCLjmQRthPvotnQ6g81gaN4=', 1, CAST(N'2016-02-03 03:56:06.583' AS DateTime), NULL)
GO
SET IDENTITY_INSERT [dbo].[Usuario] OFF
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Email_Funcionalidade]') AND parent_object_id = OBJECT_ID(N'[dbo].[Email]'))
ALTER TABLE [dbo].[Email]  WITH CHECK ADD  CONSTRAINT [FK_Email_Funcionalidade] FOREIGN KEY([FuncionalidadeId])
REFERENCES [dbo].[Funcionalidade] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Email_Funcionalidade]') AND parent_object_id = OBJECT_ID(N'[dbo].[Email]'))
ALTER TABLE [dbo].[Email] CHECK CONSTRAINT [FK_Email_Funcionalidade]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Funcionalidade_Funcionalidade]') AND parent_object_id = OBJECT_ID(N'[dbo].[Funcionalidade]'))
ALTER TABLE [dbo].[Funcionalidade]  WITH CHECK ADD  CONSTRAINT [FK_Funcionalidade_Funcionalidade] FOREIGN KEY([FuncionalidadeIdPai])
REFERENCES [dbo].[Funcionalidade] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Funcionalidade_Funcionalidade]') AND parent_object_id = OBJECT_ID(N'[dbo].[Funcionalidade]'))
ALTER TABLE [dbo].[Funcionalidade] CHECK CONSTRAINT [FK_Funcionalidade_Funcionalidade]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Log_Usuario]') AND parent_object_id = OBJECT_ID(N'[dbo].[Log]'))
ALTER TABLE [dbo].[Log]  WITH CHECK ADD  CONSTRAINT [FK_Log_Usuario] FOREIGN KEY([UsuarioId])
REFERENCES [dbo].[Usuario] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Log_Usuario]') AND parent_object_id = OBJECT_ID(N'[dbo].[Log]'))
ALTER TABLE [dbo].[Log] CHECK CONSTRAINT [FK_Log_Usuario]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PerfilFuncionalidade_Funcionalidade]') AND parent_object_id = OBJECT_ID(N'[dbo].[PerfilFuncionalidade]'))
ALTER TABLE [dbo].[PerfilFuncionalidade]  WITH CHECK ADD  CONSTRAINT [FK_PerfilFuncionalidade_Funcionalidade] FOREIGN KEY([FuncionalidadeId])
REFERENCES [dbo].[Funcionalidade] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PerfilFuncionalidade_Funcionalidade]') AND parent_object_id = OBJECT_ID(N'[dbo].[PerfilFuncionalidade]'))
ALTER TABLE [dbo].[PerfilFuncionalidade] CHECK CONSTRAINT [FK_PerfilFuncionalidade_Funcionalidade]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PerfilFuncionalidade_Perfil]') AND parent_object_id = OBJECT_ID(N'[dbo].[PerfilFuncionalidade]'))
ALTER TABLE [dbo].[PerfilFuncionalidade]  WITH CHECK ADD  CONSTRAINT [FK_PerfilFuncionalidade_Perfil] FOREIGN KEY([PerfilId])
REFERENCES [dbo].[Perfil] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PerfilFuncionalidade_Perfil]') AND parent_object_id = OBJECT_ID(N'[dbo].[PerfilFuncionalidade]'))
ALTER TABLE [dbo].[PerfilFuncionalidade] CHECK CONSTRAINT [FK_PerfilFuncionalidade_Perfil]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PerfilUsuario_Perfil]') AND parent_object_id = OBJECT_ID(N'[dbo].[PerfilUsuario]'))
ALTER TABLE [dbo].[PerfilUsuario]  WITH CHECK ADD  CONSTRAINT [FK_PerfilUsuario_Perfil] FOREIGN KEY([PerfilId])
REFERENCES [dbo].[Perfil] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PerfilUsuario_Perfil]') AND parent_object_id = OBJECT_ID(N'[dbo].[PerfilUsuario]'))
ALTER TABLE [dbo].[PerfilUsuario] CHECK CONSTRAINT [FK_PerfilUsuario_Perfil]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PerfilUsuario_Usuario]') AND parent_object_id = OBJECT_ID(N'[dbo].[PerfilUsuario]'))
ALTER TABLE [dbo].[PerfilUsuario]  WITH CHECK ADD  CONSTRAINT [FK_PerfilUsuario_Usuario] FOREIGN KEY([UsuarioId])
REFERENCES [dbo].[Usuario] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PerfilUsuario_Usuario]') AND parent_object_id = OBJECT_ID(N'[dbo].[PerfilUsuario]'))
ALTER TABLE [dbo].[PerfilUsuario] CHECK CONSTRAINT [FK_PerfilUsuario_Usuario]
GO