USE [HotelReview]
GO

/****** Object:  Table [dbo].[Hotels]    Script Date: 01/11/2023 23:17:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Hotels](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[HotelName] [nvarchar](255) NOT NULL,
	[Address] [nvarchar](255) NOT NULL,
	[City] [nvarchar](100) NOT NULL,
	[County] [nvarchar](100) NULL,
	[Country] [nvarchar](100) NOT NULL,
	[RatingAvg] [int] NOT NULL,
	[HotelUrl] [nvarchar](1000) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Hotels] ADD  DEFAULT ((0)) FOR [RatingAvg]
GO

ALTER TABLE [dbo].[Hotels] ADD  CONSTRAINT [DF_Hotels_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO

