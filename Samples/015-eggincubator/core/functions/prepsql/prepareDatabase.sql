IF NOT EXISTS(SELECT name from sys.tables WHERE name = N'SolutionData')
BEGIN
CREATE TABLE [dbo].[SolutionData](
	[InputTimestamp] [datetime] NOT NULL,
	[MetricName] [varchar](800) NOT NULL,
	[MetricValue] [float] NOT NULL,
	CONSTRAINT [PK_SolutionsData_InputTimestamp_MetricName] PRIMARY KEY CLUSTERED 
	(
		[InputTimestamp] ASC,
		[MetricName] ASC
	) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
END
GO
