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

INSERT INTO [dbo].[SolutionData] VALUES
	(dateadd(hour, -1, getdate()), 'Temperature Reading', 70),
	(dateadd(hour, -1, getdate()), 'Pressure Reading', 1.8),
	(dateadd(hour, -2, getdate()), 'Temperature Reading', 65),
	(dateadd(hour, -2, getdate()), 'Pressure Reading', 1.6),
	(dateadd(hour, -3, getdate()), 'Temperature Reading', 68),
	(dateadd(hour, -3, getdate()), 'Pressure Reading', 1.6),
	(dateadd(hour, -4, getdate()), 'Temperature Reading', 68),
	(dateadd(hour, -4, getdate()), 'Pressure Reading', 1.7),
	(dateadd(hour, -5, getdate()), 'Temperature Reading', 64),
	(dateadd(hour, -5, getdate()), 'Pressure Reading', 1.7),
	(dateadd(hour, -6, getdate()), 'Temperature Reading', 62),
	(dateadd(hour, -6, getdate()), 'Pressure Reading', 1.7),
	(dateadd(hour, -7, getdate()), 'Temperature Reading', 60),
	(dateadd(hour, -7, getdate()), 'Pressure Reading', 1.8),
	(dateadd(hour, -8, getdate()), 'Temperature Reading', 64),
	(dateadd(hour, -8, getdate()), 'Pressure Reading', 1.8),
	(dateadd(hour, -9, getdate()), 'Temperature Reading', 63),
	(dateadd(hour, -9, getdate()), 'Pressure Reading', 1.8),
	(dateadd(hour, -10, getdate()), 'Temperature Reading', 66),
	(dateadd(hour, -10, getdate()), 'Pressure Reading', 1.6),
	(dateadd(hour, -11, getdate()), 'Temperature Reading', 69),
	(dateadd(hour, -11, getdate()), 'Pressure Reading', 1.4),
	(dateadd(hour, -12, getdate()), 'Temperature Reading', 70),
	(dateadd(hour, -12, getdate()), 'Pressure Reading', 1.3)
;
GO