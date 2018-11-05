# CityGuide- web Api with ASP.netCore

This is a cityGuide Api writen with Asp.net Core.
This application has an  [angular 6](https://github.com/hidayatarg/CityGuide-Angular).front end
#Enjoy Coding

Use Angular 7.0.3 rather than most recent version 7.0.4 at this time.

# SQL Database Design
```sql

Drop table dbo.Users;

Drop table dbo.Photos;

Drop table dbo.Cities;

CREATE TABLE [dbo].[Users] (

		[Id]           INT             IDENTITY (1, 1) NOT NULL,

		[PasswordHash] VARBINARY (MAX) NULL,

		[PasswordSalt] VARBINARY (MAX) NULL,

		[Username]     NVARCHAR (MAX)  NULL

	);



	CREATE TABLE [dbo].[Cities] (

		[Id]          INT            IDENTITY (1, 1) NOT NULL,

		[Description] NVARCHAR (MAX) NULL,

		[Name]        NVARCHAR (MAX) NULL,

		[UserId]      INT            NOT NULL

	);

	
	CREATE TABLE [dbo].[Values] (

		[Id]          INT            IDENTITY (1, 1) NOT NULL,

		[Name] NVARCHAR (50) NULL

	);



	CREATE TABLE [dbo].[Photos] (

		[Id]          INT            IDENTITY (1, 1) NOT NULL,

		[CityId]      INT            NOT NULL,

		[DateAdded]   DATETIME2 (7)  NOT NULL,

		[Description] NVARCHAR (MAX) NULL,

		[IsMain]      BIT            NOT NULL,

		[Url]         NVARCHAR (MAX) NULL,

		[PublicId]    NVARCHAR (250) NULL

	);
  ```
