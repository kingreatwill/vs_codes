CREATE DATABASE Test
GO
USE [Test]
GO
/****** Object:  StoredProcedure [dbo].[Sp_User_Delete]    Script Date: 2017/4/27 15:56:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
------------------------------------
--用途：删除一条记录 
--项目名称：
--说明：
--时间：2017/4/27 14:41:17
------------------------------------
CREATE PROCEDURE [dbo].[Sp_User_Delete]
@ID varchar(36)
 AS 
	DELETE [User]
	 WHERE ID=@ID 


GO
/****** Object:  StoredProcedure [dbo].[Sp_User_Insert]    Script Date: 2017/4/27 15:56:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
------------------------------------
--用途：增加一条记录 
--项目名称：
--说明：
--时间：2017/4/27 14:41:17
------------------------------------
CREATE PROCEDURE [dbo].[Sp_User_Insert]
@ID varchar(36),
@Name nvarchar(20),
@Code varchar(10),
@Sex nvarchar(5),
@Age int,
@IsDelete bit,
@CreateTime datetime

 AS 
	INSERT INTO [User](
	[ID],[Name],[Code],[Sex],[Age],[IsDelete],[CreateTime]
	)VALUES(
	@ID,@Name,@Code,@Sex,@Age,@IsDelete,@CreateTime
	)


GO
/****** Object:  StoredProcedure [dbo].[Sp_User_SelectByWhere]    Script Date: 2017/4/27 15:56:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Sp_User_SelectByWhere]
@SqlWhere NVARCHAR(200)
 AS 
 DECLARE @sql NVARCHAR(1000)
 SET @sql = '
	 SELECT ID,Name,Code,Sex,Age,IsDelete,CreateTime
	 FROM [User] WHERE 1=1 '+ @SqlWhere
 EXEC(@sql)
GO
/****** Object:  StoredProcedure [dbo].[Sp_User_SelectCount]    Script Date: 2017/4/27 15:56:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
------------------------------------
--用途：查询记录信息 
--项目名称：
--说明：
--时间：2017/4/27 14:41:17
------------------------------------
CREATE PROCEDURE [dbo].[Sp_User_SelectCount]
@SqlWhere NVARCHAR(200)
 AS 
 DECLARE @sql NVARCHAR(1000)
 DECLARE @count INT
 SET @sql = '
	 SELECT @Count=count(1)
	 FROM [User] WHERE 1=1'+ @SqlWhere
 EXEC sp_executesql  @sql,N'@Count int output',@Count=@count OUTPUT
 SELECT @count

GO
/****** Object:  StoredProcedure [dbo].[Sp_User_Update]    Script Date: 2017/4/27 15:56:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
------------------------------------
--用途：修改一条记录 
--项目名称：
--说明：
--时间：2017/4/27 14:41:17
------------------------------------
CREATE PROCEDURE [dbo].[Sp_User_Update]
@ID varchar(36),
@Name nvarchar(20),
@Code varchar(10),
@Sex nvarchar(5),
@Age int,
@IsDelete bit,
@CreateTime datetime
 AS 
	UPDATE [User] SET 
	[Name] = @Name,[Code] = @Code,[Sex] = @Sex,[Age] = @Age,[IsDelete] = @IsDelete,[CreateTime] = @CreateTime
	WHERE ID=@ID 


GO
/****** Object:  Table [dbo].[User]    Script Date: 2017/4/27 15:56:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[User](
	[ID] [varchar](36) NOT NULL,
	[Name] [nvarchar](20) NOT NULL,
	[Code] [varchar](10) NULL,
	[Sex] [nvarchar](5) NULL,
	[Age] [int] NULL,
	[IsDelete] [bit] NULL,
	[CreateTime] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
