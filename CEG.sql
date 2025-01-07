USE [master]
GO
/****** Object:  Database [ChildrenEnglishGame]    Script Date: 01-Jan-25 11:59:08 ******/
CREATE DATABASE [ChildrenEnglishGame]

GO
ALTER DATABASE [ChildrenEnglishGame] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ChildrenEnglishGame] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ChildrenEnglishGame] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ChildrenEnglishGame] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ChildrenEnglishGame] SET ARITHABORT OFF 
GO
ALTER DATABASE [ChildrenEnglishGame] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ChildrenEnglishGame] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ChildrenEnglishGame] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ChildrenEnglishGame] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ChildrenEnglishGame] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ChildrenEnglishGame] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ChildrenEnglishGame] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ChildrenEnglishGame] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ChildrenEnglishGame] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ChildrenEnglishGame] SET DISABLE_BROKER 
GO
ALTER DATABASE [ChildrenEnglishGame] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ChildrenEnglishGame] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ChildrenEnglishGame] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ChildrenEnglishGame] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ChildrenEnglishGame] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ChildrenEnglishGame] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ChildrenEnglishGame] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ChildrenEnglishGame] SET RECOVERY FULL 
GO
ALTER DATABASE [ChildrenEnglishGame] SET  MULTI_USER 
GO
ALTER DATABASE [ChildrenEnglishGame] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ChildrenEnglishGame] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ChildrenEnglishGame] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ChildrenEnglishGame] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ChildrenEnglishGame] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ChildrenEnglishGame] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [ChildrenEnglishGame] SET QUERY_STORE = ON
GO
ALTER DATABASE [ChildrenEnglishGame] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [ChildrenEnglishGame]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 01-Jan-25 11:59:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[account_id] [int] IDENTITY(1,1) NOT NULL,
	[role_id] [int] NOT NULL,
	[username] [nvarchar](max) NOT NULL,
	[password] [nvarchar](max) NOT NULL,
	[fullname] [nvarchar](max) NOT NULL,
	[created_date] [datetime] NOT NULL,
	[gender] [nvarchar](10) NOT NULL,
	[total_amount] [int] NOT NULL,
	[status] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Accounts] PRIMARY KEY CLUSTERED 
(
	[account_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Attendance]    Script Date: 01-Jan-25 11:59:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Attendance](
	[attendance_id] [int] IDENTITY(1,1) NOT NULL,
	[schedule_id] [int] NOT NULL,
	[student_id] [int] NOT NULL,
	[has_attended] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Attendance] PRIMARY KEY CLUSTERED 
(
	[attendance_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Class]    Script Date: 01-Jan-25 11:59:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Class](
	[class_id] [int] IDENTITY(1,1) NOT NULL,
	[teacher_id] [int] NOT NULL,
	[course_id] [int] NOT NULL,
	[class_name] [nvarchar](max) NOT NULL,
	[minimum_students] [int] NULL,
	[maximum_students] [int] NULL,
	[number_of_students] [int] NULL,
	[start_date] [datetime] NULL,
	[end_date] [datetime] NULL,
	[enrollment_fee] [int] NOT NULL,
	[status] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Class_1] PRIMARY KEY CLUSTERED 
(
	[class_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Course]    Script Date: 01-Jan-25 11:59:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Course](
	[course_id] [int] IDENTITY(1,1) NOT NULL,
	[course_name] [nvarchar](50) NOT NULL,
	[course_type] [nvarchar](50) NOT NULL,
	[description] [nvarchar](max) NOT NULL,
	[status] [nvarchar](50) NULL,
	[total_hours] [int] NULL,
	[image] [nvarchar](max) NULL,
	[required_age] [int] NULL,
	[difficulty] [nvarchar](20) NULL,
	[category] [nvarchar](20) NULL,
 CONSTRAINT [PK_Course_1] PRIMARY KEY CLUSTERED 
(
	[course_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Enroll]    Script Date: 01-Jan-25 11:59:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Enroll](
	[enroll_id] [int] IDENTITY(1,1) NOT NULL,
	[student_id] [int] NOT NULL,
	[class_id] [int] NOT NULL,
	[transaction_id] [int] NOT NULL,
	[registration_date] [datetime] NOT NULL,
	[enrolled_date] [datetime] NOT NULL,
	[status] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Enroll] PRIMARY KEY CLUSTERED 
(
	[enroll_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Game]    Script Date: 01-Jan-25 11:59:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Game](
	[game_id] [int] IDENTITY(1,1) NOT NULL,
	[game_config_id] [int] NOT NULL,
	[download_link] [nvarchar](max) NULL,
	[title] [nvarchar](50) NOT NULL,
	[point] [int] NULL,
	[status] [nvarchar](50) NULL,
	[type] [nvarchar](50) NULL,
 CONSTRAINT [PK_Game] PRIMARY KEY CLUSTERED 
(
	[game_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GameConfig]    Script Date: 01-Jan-25 11:59:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GameConfig](
	[game_config_id] [int] IDENTITY(1,1) NOT NULL,
	[title] [nvarchar](50) NOT NULL,
	[point] [int] NULL,
	[correct_answer] [nvarchar](max) NULL,
	[status] [nvarchar](50) NULL,
 CONSTRAINT [PK_GameConfig] PRIMARY KEY CLUSTERED 
(
	[game_config_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GameLevel]    Script Date: 01-Jan-25 11:59:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GameLevel](
	[game_level_id] [int] IDENTITY(1,1) NOT NULL,
	[game_id] [int] NOT NULL,
	[title] [nvarchar](50) NOT NULL,
	[status] [nvarchar](50) NULL,
 CONSTRAINT [PK_GameLevel] PRIMARY KEY CLUSTERED 
(
	[game_level_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Homework]    Script Date: 01-Jan-25 11:59:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Homework](
	[homework_id] [int] IDENTITY(1,1) NOT NULL,
	[session_id] [int] NOT NULL,
	[title] [nvarchar](50) NULL,
	[description] [nvarchar](max) NULL,
	[game_config_id] [int] NULL,
	[start_date] [datetime] NULL,
	[end_date] [datetime] NULL,
	[hours] [int] NULL,
	[type] [nvarchar](50) NULL,
 CONSTRAINT [PK_Homework] PRIMARY KEY CLUSTERED 
(
	[homework_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HomeworkAnswer]    Script Date: 01-Jan-25 11:59:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HomeworkAnswer](
	[homework_answer_id] [int] IDENTITY(1,1) NOT NULL,
	[homework_question_id] [int] NOT NULL,
	[answer] [nvarchar](max) NULL,
	[type] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_HomeworkAnswer] PRIMARY KEY CLUSTERED 
(
	[homework_answer_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HomeworkQuestion]    Script Date: 01-Jan-25 11:59:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HomeworkQuestion](
	[homework_question_id] [int] IDENTITY(1,1) NOT NULL,
	[homework_id] [int] NULL,
	[question] [nvarchar](max) NULL,
 CONSTRAINT [PK_HomeworkData] PRIMARY KEY CLUSTERED 
(
	[homework_question_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HomeworkResult]    Script Date: 01-Jan-25 11:59:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HomeworkResult](
	[homework_result_id] [int] IDENTITY(1,1) NOT NULL,
	[total_point] [int] NULL,
	[total_correct_answers] [int] NULL,
	[playtime] [time](7) NULL,
 CONSTRAINT [PK_HomeworkResult] PRIMARY KEY CLUSTERED 
(
	[homework_result_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Parent]    Script Date: 01-Jan-25 11:59:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Parent](
	[parent_id] [int] IDENTITY(1,1) NOT NULL,
	[account_id] [int] NOT NULL,
	[email] [nvarchar](max) NOT NULL,
	[phone] [nvarchar](10) NULL,
	[address] [nvarchar](max) NULL,
 CONSTRAINT [PK_Parent] PRIMARY KEY CLUSTERED 
(
	[parent_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 01-Jan-25 11:59:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[role_id] [int] IDENTITY(1,1) NOT NULL,
	[role_name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[role_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Schedule]    Script Date: 01-Jan-25 11:59:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Schedule](
	[schedule_id] [int] IDENTITY(1,1) NOT NULL,
	[session_id] [int] NOT NULL,
	[class_id] [int] NOT NULL,
	[schedule_date] [datetime] NULL,
	[start_time] [time](7) NULL,
	[end_time] [time](7) NULL,
	[status] [nvarchar](50) NULL,
 CONSTRAINT [PK_Schedule] PRIMARY KEY CLUSTERED 
(
	[schedule_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Session]    Script Date: 01-Jan-25 11:59:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Session](
	[session_id] [int] IDENTITY(1,1) NOT NULL,
	[course_id] [int] NOT NULL,
	[title] [nvarchar](50) NOT NULL,
	[description] [nvarchar](max) NULL,
	[hours] [int] NULL,
	[session_number] [int] NULL,
 CONSTRAINT [PK_Session] PRIMARY KEY CLUSTERED 
(
	[session_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Student]    Script Date: 01-Jan-25 11:59:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Student](
	[student_id] [int] IDENTITY(1,1) NOT NULL,
	[parent_id] [int] NOT NULL,
	[account_id] [int] NOT NULL,
	[description] [nvarchar](max) NOT NULL,
	[cur_level] [int] NULL,
	[age] [int] NULL,
	[birthdate] [datetime] NULL,
	[image] [nvarchar](max) NULL,
 CONSTRAINT [PK_Student_1] PRIMARY KEY CLUSTERED 
(
	[student_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudentAnswer]    Script Date: 01-Jan-25 11:59:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentAnswer](
	[student_answer_id] [int] IDENTITY(1,1) NOT NULL,
	[game_id] [int] NOT NULL,
	[student_homework_id] [int] NOT NULL,
	[answer] [nvarchar](max) NULL,
	[type] [nvarchar](max) NULL,
 CONSTRAINT [PK_StudentAnswer] PRIMARY KEY CLUSTERED 
(
	[student_answer_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudentHomework]    Script Date: 01-Jan-25 11:59:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentHomework](
	[student_homework_id] [int] IDENTITY(1,1) NOT NULL,
	[homework_id] [int] NOT NULL,
	[student_progress_id] [int] NOT NULL,
	[homework_result_id] [int] NOT NULL,
	[point] [int] NULL,
	[playtime] [time](7) NULL,
	[status] [nvarchar](50) NULL,
	[correct_answers] [int] NULL,
 CONSTRAINT [PK_StudentHomework] PRIMARY KEY CLUSTERED 
(
	[student_homework_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudentProgress]    Script Date: 01-Jan-25 11:59:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentProgress](
	[student_progress_id] [int] IDENTITY(1,1) NOT NULL,
	[student_id] [int] NOT NULL,
	[class_id] [int] NOT NULL,
	[total_point] [int] NULL,
	[playtime] [time](7) NULL,
 CONSTRAINT [PK_StudentProgress] PRIMARY KEY CLUSTERED 
(
	[student_progress_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Teacher]    Script Date: 01-Jan-25 11:59:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Teacher](
	[teacher_id] [int] IDENTITY(1,1) NOT NULL,
	[account_id] [int] NOT NULL,
	[email] [nvarchar](max) NOT NULL,
	[phone] [nvarchar](10) NOT NULL,
	[certificate] [nvarchar](max) NOT NULL,
	[address] [nvarchar](max) NOT NULL,
	[image] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Teacher] PRIMARY KEY CLUSTERED 
(
	[teacher_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transaction]    Script Date: 01-Jan-25 11:59:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transaction](
	[transaction_id] [int] IDENTITY(1,1) NOT NULL,
	[account_id] [int] NOT NULL,
	[vnpay_id] [nvarchar](max) NULL,
	[transaction_amount] [int] NOT NULL,
	[transaction_date] [datetime] NOT NULL,
	[transaction_status] [nvarchar](max) NOT NULL,
	[transaction_type] [nvarchar](50) NOT NULL,
	[confirm_date] [datetime] NOT NULL,
	[description] [nvarchar](MAX) NULL,
	 CONSTRAINT [PK_Transaction_1] PRIMARY KEY CLUSTERED 
(
	[transaction_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Account] ON 
GO
INSERT [dbo].[Account] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [total_amount], [status]) VALUES (1, 1, N'HoaDM1', N'pass123', N'Dao Minh Hoa', CAST(N'2024-09-10T00:00:00.000' AS DateTime), N'Male', 50000000, N'Active')
GO
INSERT [dbo].[Account] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [total_amount], [status]) VALUES (2, 3, N'MinhDH2', N'pass123', N'Dang Hoang Minh', CAST(N'2024-09-12T00:00:00.000' AS DateTime), N'Male', 50000000, N'Active')
GO
INSERT [dbo].[Account] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [total_amount], [status]) VALUES (3, 4, N'VinhBH3', N'pass123', N'Bui Huu Vinh', CAST(N'2024-09-12T00:00:00.000' AS DateTime), N'Female', 50000000, N'Active')
GO
INSERT [dbo].[Account] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [total_amount], [status]) VALUES (4, 2, N'LinhNK4', N'pass123', N'Nguyen Khanh Linh', CAST(N'2024-09-13T00:00:00.000' AS DateTime), N'Male', 50000000, N'Active')
GO
INSERT [dbo].[Account] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [total_amount], [status]) VALUES (5, 4, N'KhoaPA5', N'pass123', N'Pham Anh Khoa', CAST(N'2024-09-13T00:00:00.000' AS DateTime), N'Male', 50000000, N'Active')
GO
INSERT [dbo].[Account] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [total_amount], [status]) VALUES (6, 3, N'MinhLT6', N'pass123', N'Lam The Minh', CAST(N'2024-09-13T00:00:00.000' AS DateTime), N'Male', 50000000, N'Active')
GO
INSERT [dbo].[Account] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [total_amount], [status]) VALUES (7, 4, N'DuyDNM7', N'pass123', N'Dang Nguyen Minh Duy', CAST(N'2024-09-13T00:00:00.000' AS DateTime), N'Male', 50000000, N'Active')
GO
INSERT [dbo].[Account] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [total_amount], [status]) VALUES (8, 2, N'NganVT8', N'pass123', N'Vo Thuy Ngan', CAST(N'2024-09-14T00:00:00.000' AS DateTime), N'Female', 50000000, N'Active')
GO
INSERT [dbo].[Account] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [total_amount], [status]) VALUES (9, 2, N'UyenDNM9', N'pass123', N'Do Ngoc My Uyen', CAST(N'2024-09-16T00:00:00.000' AS DateTime), N'Female', 50000000, N'Active')
GO
INSERT [dbo].[Account] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [total_amount], [status]) VALUES (10, 4, N'NganLT10', N'pass123', N'Le Thu Ngan', CAST(N'2024-09-16T00:00:00.000' AS DateTime), N'Female', 50000000, N'Active')
GO
INSERT [dbo].[Account] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [total_amount], [status]) VALUES (11, 2, N'DucPM11', N'pass123', N'Pham Minh Duc', CAST(N'2024-09-17T00:00:00.000' AS DateTime), N'Female', 50000000, N'Active')
GO
INSERT [dbo].[Account] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [total_amount], [status]) VALUES (12, 3, N'ThongHH12', N'pass123', N'Ho Hieu Thong', CAST(N'2024-09-17T00:00:00.000' AS DateTime), N'Male', 50000000, N'Active')
GO
INSERT [dbo].[Account] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [total_amount], [status]) VALUES (13, 4, N'NamMN13', N'pass123', N'Mai Nhat Nam', CAST(N'2024-09-17T00:00:00.000' AS DateTime), N'Male', 50000000, N'Active')
GO
INSERT [dbo].[Account] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [total_amount], [status]) VALUES (14, 2, N'TuMNT14', N'pass123', N'Mai Nguyen Tuan Tu', CAST(N'2024-09-19T00:00:00.000' AS DateTime), N'Male', 50000000, N'Active')
GO
INSERT [dbo].[Account] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [total_amount], [status]) VALUES (15, 4, N'DuyBD15', N'pass123', N'Bui Duc Duy', CAST(N'2024-09-19T00:00:00.000' AS DateTime), N'Male', 50000000, N'Active')
GO
INSERT [dbo].[Account] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [total_amount], [status]) VALUES (16, 3, N'MinhNN16', N'pass123', N'Nguyen Ngoc Minh', CAST(N'2024-09-19T00:00:00.000' AS DateTime), N'Female', 50000000, N'Active')
GO
INSERT [dbo].[Account] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [total_amount], [status]) VALUES (17, 4, N'KhanhHN17', N'pass123', N'Ho Ngoc Khanh', CAST(N'2024-09-20T00:00:00.000' AS DateTime), N'Female', 50000000, N'Active')
GO
INSERT [dbo].[Account] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [total_amount], [status]) VALUES (18, 2, N'MaiPTN18', N'pass123', N'Pham Thi Ngoc Mai', CAST(N'2024-09-21T00:00:00.000' AS DateTime), N'Female', 50000000, N'Active')
GO
INSERT [dbo].[Account] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [total_amount], [status]) VALUES (19, 2, N'KimHN19', N'pass123', N'Ho Ngoc Kim', CAST(N'2024-09-23T00:00:00.000' AS DateTime), N'Female', 50000000, N'Active')
GO
INSERT [dbo].[Account] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [total_amount], [status]) VALUES (20, 4, N'KhoaBA20', N'pass123', N'Bui Anh Khoa', CAST(N'2024-09-24T00:00:00.000' AS DateTime), N'Male', 50000000, N'Active')
GO
INSERT [dbo].[Account] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [total_amount], [status]) VALUES (21, 2, N'SonNT21', N'pass123', N'Nguyen Truc Son', CAST(N'2024-09-26T00:00:00.000' AS DateTime), N'Female', 50000000, N'Active')
GO
INSERT [dbo].[Account] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [total_amount], [status]) VALUES (22, 1, N'DuyHTK22', N'pass123', N'Ho Tran Khanh Duy', CAST(N'2024-09-27T00:00:00.000' AS DateTime), N'Male', 50000000, N'Active')
GO
INSERT [dbo].[Account] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [total_amount], [status]) VALUES (23, 2, N'TuanHA23', N'pass123', N'Huynh Anh Tuan', CAST(N'2024-09-27T00:00:00.000' AS DateTime), N'Male', 50000000, N'Active')
GO
INSERT [dbo].[Account] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [total_amount], [status]) VALUES (24, 2, N'TrinhLT24', N'pass123', N'Le Thi Trinh', CAST(N'2024-09-28T00:00:00.000' AS DateTime), N'Female', 50000000, N'Active')
GO
INSERT [dbo].[Account] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [total_amount], [status]) VALUES (25, 2, N'BaoPN25', N'pass123', N'Pham Nguyen Bao', CAST(N'2024-09-28T00:00:00.000' AS DateTime), N'Male', 50000000, N'Active')
GO
INSERT [dbo].[Account] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [total_amount], [status]) VALUES (26, 2, N'MinhDL26', N'pass123', N'Do Le Minh', CAST(N'2024-09-29T00:00:00.000' AS DateTime), N'Female', 50000000, N'Active')
GO
INSERT [dbo].[Account] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [total_amount], [status]) VALUES (27, 2, N'LanTT27', N'pass123', N'Tran Thi Lan', CAST(N'2024-09-29T00:00:00.000' AS DateTime), N'Female', 50000000, N'Active')
GO
INSERT [dbo].[Account] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [total_amount], [status]) VALUES (28, 2, N'CuongNV28', N'pass123', N'Nguyen Van Cuong', CAST(N'2024-09-30T00:00:00.000' AS DateTime), N'Male', 50000000, N'Active')
GO
INSERT [dbo].[Account] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [total_amount], [status]) VALUES (29, 2, N'TrangTT29', N'pass123', N'Tran Thi Trang', CAST(N'2024-09-30T00:00:00.000' AS DateTime), N'Female', 50000000, N'Active')
GO
INSERT [dbo].[Account] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [total_amount], [status]) VALUES (30, 2, N'KhanhND30', N'pass123', N'Nguyen Duc Khanh', CAST(N'2024-10-01T00:00:00.000' AS DateTime), N'Male', 50000000, N'Active')
GO
INSERT [dbo].[Account] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [total_amount], [status]) VALUES (31, 2, N'ThuVN31', N'pass123', N'Vu Ngoc Thu', CAST(N'2024-10-01T00:00:00.000' AS DateTime), N'Female', 50000000, N'Active')
GO
INSERT [dbo].[Account] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [total_amount], [status]) VALUES (32, 2, N'PhongTT32', N'pass123', N'Tran Tuan Phong', CAST(N'2024-10-02T00:00:00.000' AS DateTime), N'Male', 50000000, N'Active')
GO
INSERT [dbo].[Account] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [total_amount], [status]) VALUES (33, 2, N'LinhNT33', N'pass123', N'Nguyen Thi Linh', CAST(N'2024-10-02T00:00:00.000' AS DateTime), N'Female', 50000000, N'Active')
GO
INSERT [dbo].[Account] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [total_amount], [status]) VALUES (34, 2, N'TuanBT34', N'pass123', N'Bui Tuan Tu', CAST(N'2024-10-03T00:00:00.000' AS DateTime), N'Male', 50000000, N'Active')
GO
INSERT [dbo].[Account] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [total_amount], [status]) VALUES (35, 2, N'ThuyLT35', N'pass123', N'Le Thi Thuy', CAST(N'2024-10-03T00:00:00.000' AS DateTime), N'Female', 50000000, N'Active')
GO
INSERT [dbo].[Account] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [total_amount], [status]) VALUES (36, 2, N'KienPV36', N'pass123', N'Pham Van Kien', CAST(N'2024-10-04T00:00:00.000' AS DateTime), N'Male', 50000000, N'Active')
GO
INSERT [dbo].[Account] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [total_amount], [status]) VALUES (37, 2, N'DuongTV37', N'pass123', N'Tran Van Duong', CAST(N'2024-10-04T00:00:00.000' AS DateTime), N'Male', 50000000, N'Active')
GO
INSERT [dbo].[Account] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [total_amount], [status]) VALUES (38, 2, N'HieuNL38', N'pass123', N'Nguyen Le Hieu', CAST(N'2024-10-05T00:00:00.000' AS DateTime), N'Male', 50000000, N'Active')
GO
INSERT [dbo].[Account] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [total_amount], [status]) VALUES (39, 2, N'HoaTL39', N'pass123', N'Tran Lan Hoa', CAST(N'2024-10-05T00:00:00.000' AS DateTime), N'Female', 50000000, N'Active')
GO
INSERT [dbo].[Account] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [total_amount], [status]) VALUES (40, 2, N'ThanhPH40', N'pass123', N'Phan Huu Thanh', CAST(N'2024-10-06T00:00:00.000' AS DateTime), N'Male', 50000000, N'Active')
GO
INSERT [dbo].[Account] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [total_amount], [status]) VALUES (41, 2, N'DiepNT41', N'pass123', N'Nguyen Thi Diep', CAST(N'2024-10-06T00:00:00.000' AS DateTime), N'Female', 50000000, N'Active')
GO
INSERT [dbo].[Account] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [total_amount], [status]) VALUES (42, 2, N'TrungDN42', N'pass123', N'Dang Nguyen Trung', CAST(N'2024-10-07T00:00:00.000' AS DateTime), N'Male', 50000000, N'Active')
GO
INSERT [dbo].[Account] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [total_amount], [status]) VALUES (43, 2, N'HangLH43', N'pass123', N'Le Hong Hang', CAST(N'2024-10-07T00:00:00.000' AS DateTime), N'Female', 50000000, N'Active')
GO
INSERT [dbo].[Account] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [total_amount], [status]) VALUES (44, 2, N'ToanTB44', N'pass123', N'Tran Bao Toan', CAST(N'2024-10-08T00:00:00.000' AS DateTime), N'Male', 50000000, N'Active')
GO
INSERT [dbo].[Account] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [total_amount], [status]) VALUES (45, 2, N'TrangLN45', N'pass123', N'Le Ngoc Trang', CAST(N'2024-10-08T00:00:00.000' AS DateTime), N'Female', 50000000, N'Active')
GO
INSERT [dbo].[Account] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [total_amount], [status]) VALUES (46, 2, N'KhanhBT46', N'pass123', N'Bui Thi Khanh', CAST(N'2024-10-09T00:00:00.000' AS DateTime), N'Female', 50000000, N'Active')
GO
INSERT [dbo].[Account] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [total_amount], [status]) VALUES (47, 2, N'ThuyNT47', N'pass123', N'Nguyen Thi Thuy', CAST(N'2024-10-09T00:00:00.000' AS DateTime), N'Female', 50000000, N'Active')
GO
INSERT [dbo].[Account] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [total_amount], [status]) VALUES (48, 2, N'TuanHV48', N'pass123', N'Hoang Van Tuan', CAST(N'2024-10-10T00:00:00.000' AS DateTime), N'Male', 50000000, N'Active')
GO
INSERT [dbo].[Account] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [total_amount], [status]) VALUES (49, 2, N'DuyenTD49', N'pass123', N'Tran Thi Duyen', CAST(N'2024-10-10T00:00:00.000' AS DateTime), N'Female', 50000000, N'Active')
GO
INSERT [dbo].[Account] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [total_amount], [status]) VALUES (50, 2, N'HieuNV50', N'pass123', N'Nguyen Van Hieu', CAST(N'2024-10-11T00:00:00.000' AS DateTime), N'Male', 50000000, N'Active')
GO
INSERT [dbo].[Account] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [total_amount], [status]) VALUES (51, 2, N'HoaLT51', N'pass123', N'Le Thi Hoa', CAST(N'2024-10-11T00:00:00.000' AS DateTime), N'Female', 50000000, N'Active')
GO
INSERT [dbo].[Account] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [total_amount], [status]) VALUES (52, 2, N'ThanhTH52', N'pass123', N'Tran Huu Thanh', CAST(N'2024-10-12T00:00:00.000' AS DateTime), N'Male', 50000000, N'Active')
GO
INSERT [dbo].[Account] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [total_amount], [status]) VALUES (53, 2, N'PhuongNN53', N'pass123', N'Nguyen Ngoc Phuong', CAST(N'2024-10-12T00:00:00.000' AS DateTime), N'Female', 50000000, N'Active')
GO
INSERT [dbo].[Account] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [total_amount], [status]) VALUES (54, 2, N'CuongLB54', N'pass123', N'Le Bao Cuong', CAST(N'2024-10-13T00:00:00.000' AS DateTime), N'Male', 50000000, N'Active')
GO
INSERT [dbo].[Account] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [total_amount], [status]) VALUES (55, 2, N'LinhNT55', N'pass123', N'Nguyen Thi Linh', CAST(N'2024-10-13T00:00:00.000' AS DateTime), N'Female', 50000000, N'Active')
GO
INSERT [dbo].[Account] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [total_amount], [status]) VALUES (56, 2, N'HieuHN56', N'pass123', N'Hoang Ngoc Hieu', CAST(N'2024-10-14T00:00:00.000' AS DateTime), N'Male', 50000000, N'Active')
GO
INSERT [dbo].[Account] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [total_amount], [status]) VALUES (57, 2, N'NgocLT57', N'pass123', N'Le Thanh Ngoc', CAST(N'2024-10-14T00:00:00.000' AS DateTime), N'Female', 50000000, N'Active')
GO
INSERT [dbo].[Account] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [total_amount], [status]) VALUES (58, 2, N'HungVT58', N'pass123', N'Vo Thanh Hung', CAST(N'2024-10-15T00:00:00.000' AS DateTime), N'Male', 50000000, N'Active')
GO
INSERT [dbo].[Account] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [total_amount], [status]) VALUES (59, 2, N'DucPV59', N'pass123', N'Pham Van Duc', CAST(N'2024-10-15T00:00:00.000' AS DateTime), N'Male', 50000000, N'Active')
GO
INSERT [dbo].[Account] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [total_amount], [status]) VALUES (60, 2, N'KhoaLN60', N'pass123', N'Le Ngoc Khoa', CAST(N'2024-10-16T00:00:00.000' AS DateTime), N'Male', 50000000, N'Active')
GO
INSERT [dbo].[Account] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [total_amount], [status]) VALUES (61, 2, N'ThuyPT61', N'pass123', N'Pham Thanh Thuy', CAST(N'2024-10-17T00:00:00.000' AS DateTime), N'Female', 50000000, N'Active')
GO
INSERT [dbo].[Account] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [total_amount], [status]) VALUES (62, 4, N'HaiTA62', N'pass123', N'Tran Anh Hai', CAST(N'2024-10-17T00:00:00.000' AS DateTime), N'Male', 50000000, N'Active')
GO
INSERT [dbo].[Account] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [total_amount], [status]) VALUES (63, 4, N'AnhNL63', N'pass123', N'Nguyen Lan Anh', CAST(N'2024-10-17T00:00:00.000' AS DateTime), N'Female', 50000000, N'Active')
GO
INSERT [dbo].[Account] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [total_amount], [status]) VALUES (64, 4, N'ThienLH64', N'pass123', N'Le Ha Thien', CAST(N'2024-10-17T00:00:00.000' AS DateTime), N'Male', 50000000, N'Active')
GO
INSERT [dbo].[Account] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [total_amount], [status]) VALUES (65, 4, N'CuongVM65', N'pass123', N'Vu Manh Cuong', CAST(N'2024-10-17T00:00:00.000' AS DateTime), N'Male', 50000000, N'Active')
GO
INSERT [dbo].[Account] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [total_amount], [status]) VALUES (66, 4, N'KhaiTV66', N'pass123', N'Tran Van Khai', CAST(N'2024-10-18T00:00:00.000' AS DateTime), N'Male', 50000000, N'Active')
GO
INSERT [dbo].[Account] ([account_id], [role_id], [username], [password], [fullname], [created_date], [gender], [total_amount], [status]) VALUES (67, 4, N'ManhVV67', N'pass123', N'Vu Van Manh', CAST(N'2024-10-18T00:00:00.000' AS DateTime), N'Male', 50000000, N'Active')
GO
SET IDENTITY_INSERT [dbo].[Account] OFF
GO
SET IDENTITY_INSERT [dbo].[Attendance] ON 
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (1, 1, 1, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (2, 1, 2, N'Absent')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (3, 1, 3, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (4, 1, 4, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (5, 1, 5, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (6, 1, 6, N'Absent')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (7, 1, 7, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (8, 1, 8, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (9, 1, 9, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (10, 1, 10, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (11, 1, 11, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (12, 1, 12, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (13, 1, 13, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (14, 1, 14, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (15, 1, 15, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (16, 1, 16, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (17, 2, 1, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (18, 2, 2, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (19, 2, 3, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (20, 2, 4, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (21, 2, 5, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (22, 2, 6, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (23, 2, 7, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (24, 2, 8, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (25, 2, 9, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (26, 2, 10, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (27, 2, 11, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (28, 2, 12, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (29, 2, 13, N'Absent')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (30, 2, 14, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (31, 2, 15, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (32, 2, 16, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (33, 3, 1, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (34, 2, 2, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (35, 2, 3, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (36, 2, 4, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (37, 2, 5, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (38, 2, 6, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (39, 2, 7, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (40, 2, 8, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (41, 2, 9, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (42, 2, 10, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (43, 2, 11, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (44, 2, 12, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (45, 2, 13, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (46, 2, 14, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (47, 2, 15, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (48, 2, 16, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (49, 3, 1, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (50, 3, 2, N'Absent')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (51, 3, 3, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (52, 3, 4, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (53, 3, 5, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (54, 3, 6, N'Absent')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (55, 3, 7, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (56, 3, 8, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (57, 3, 9, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (58, 3, 10, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (59, 3, 11, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (60, 3, 12, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (61, 3, 13, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (62, 3, 14, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (63, 3, 15, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (64, 3, 16, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (65, 4, 1, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (66, 4, 2, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (67, 4, 3, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (68, 4, 4, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (69, 4, 5, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (70, 4, 6, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (71, 4, 7, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (72, 4, 8, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (73, 4, 9, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (74, 4, 10, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (75, 4, 11, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (76, 4, 12, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (77, 4, 13, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (78, 4, 14, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (79, 4, 15, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (80, 4, 16, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (81, 5, 1, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (82, 5, 2, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (83, 5, 3, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (84, 5, 4, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (85, 5, 5, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (86, 5, 6, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (87, 5, 7, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (88, 5, 8, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (89, 5, 9, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (90, 5, 10, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (91, 5, 11, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (92, 5, 12, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (93, 5, 13, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (94, 5, 14, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (95, 5, 15, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (96, 5, 16, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (97, 6, 1, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (98, 6, 2, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (99, 6, 3, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (100, 6, 4, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (101, 6, 5, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (102, 6, 6, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (103, 6, 7, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (104, 6, 8, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (105, 6, 9, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (106, 6, 10, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (107, 6, 11, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (108, 6, 12, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (109, 6, 13, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (110, 6, 14, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (111, 6, 15, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (112, 6, 16, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (113, 7, 1, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (114, 7, 2, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (115, 7, 3, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (116, 7, 4, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (117, 7, 5, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (118, 7, 6, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (119, 7, 7, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (120, 7, 8, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (121, 7, 9, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (122, 7, 10, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (123, 7, 11, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (124, 7, 12, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (125, 7, 13, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (126, 7, 14, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (127, 7, 15, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (128, 7, 16, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (129, 8, 1, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (130, 8, 2, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (131, 8, 3, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (132, 8, 4, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (133, 8, 5, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (134, 8, 6, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (135, 8, 7, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (136, 8, 8, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (137, 8, 9, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (138, 8, 10, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (139, 8, 11, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (140, 8, 12, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (141, 8, 13, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (142, 8, 14, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (143, 8, 15, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (144, 8, 16, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (145, 9, 1, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (146, 9, 2, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (147, 9, 3, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (148, 9, 4, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (149, 9, 5, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (150, 9, 6, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (151, 9, 7, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (152, 9, 8, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (153, 9, 9, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (154, 9, 10, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (155, 9, 11, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (156, 9, 12, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (157, 9, 13, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (158, 9, 14, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (159, 9, 15, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (160, 9, 16, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (161, 10, 1, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (162, 10, 2, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (163, 10, 3, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (164, 10, 4, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (165, 10, 5, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (166, 10, 6, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (167, 10, 7, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (168, 10, 8, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (169, 10, 9, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (170, 10, 10, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (171, 10, 11, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (172, 10, 12, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (173, 10, 13, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (174, 10, 14, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (175, 10, 15, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (176, 10, 16, N'Attended')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (177, 13, 17, N'Absent')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (178, 13, 18, N'Absent')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (179, 13, 19, N'Absent')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (180, 13, 20, N'Absent')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (181, 13, 21, N'Absent')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (182, 13, 22, N'Absent')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (183, 13, 23, N'Absent')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (184, 13, 24, N'Absent')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (185, 13, 25, N'Absent')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (186, 13, 26, N'Absent')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (187, 13, 27, N'Absent')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (188, 13, 28, N'Absent')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (189, 13, 29, N'Absent')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (190, 13, 30, N'Absent')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (191, 13, 31, N'Absent')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (192, 13, 32, N'Absent')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (193, 14, 17, N'Absent')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (194, 14, 18, N'Absent')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (195, 14, 19, N'Absent')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (196, 14, 20, N'Absent')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (197, 14, 21, N'Absent')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (198, 14, 22, N'Absent')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (199, 14, 23, N'Absent')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (200, 14, 24, N'Absent')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (201, 14, 25, N'Absent')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (202, 14, 26, N'Absent')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (203, 14, 27, N'Absent')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (204, 14, 28, N'Absent')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (205, 14, 29, N'Absent')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (206, 14, 30, N'Absent')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (207, 14, 31, N'Absent')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (208, 14, 32, N'Absent')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (209, 15, 17, N'Absent')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (210, 15, 18, N'Absent')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (211, 15, 19, N'Absent')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (212, 15, 20, N'Absent')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (213, 15, 21, N'Absent')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (214, 15, 22, N'Absent')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (215, 15, 23, N'Absent')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (216, 15, 24, N'Absent')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (217, 15, 25, N'Absent')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (218, 15, 26, N'Absent')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (219, 15, 27, N'Absent')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (220, 15, 28, N'Absent')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (221, 15, 29, N'Absent')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (222, 15, 30, N'Absent')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (223, 15, 31, N'Absent')
GO
INSERT [dbo].[Attendance] ([attendance_id], [schedule_id], [student_id], [has_attended]) VALUES (224, 15, 32, N'Absent')
GO
SET IDENTITY_INSERT [dbo].[Attendance] OFF
GO
SET IDENTITY_INSERT [dbo].[Class] ON 
GO
INSERT [dbo].[Class] ([class_id], [teacher_id], [course_id], [class_name], [minimum_students], [maximum_students], [number_of_students], [start_date], [end_date], [enrollment_fee], [status]) VALUES (1, 1, 1, N'GRAM101', 5, 20, 16, CAST(N'2024-09-30T00:00:00.000' AS DateTime), CAST(N'2025-01-30T00:00:00.000' AS DateTime), 1000000, N'Ongoing')
GO
INSERT [dbo].[Class] ([class_id], [teacher_id], [course_id], [class_name], [minimum_students], [maximum_students], [number_of_students], [start_date], [end_date], [enrollment_fee], [status]) VALUES (2, 1, 2, N'GRAM102', 5, 20, 16, CAST(N'2024-12-15T00:00:00.000' AS DateTime), CAST(N'2025-02-28T00:00:00.000' AS DateTime), 1000000, N'Ongoing')
GO
INSERT [dbo].[Class] ([class_id], [teacher_id], [course_id], [class_name], [minimum_students], [maximum_students], [number_of_students], [start_date], [end_date], [enrollment_fee], [status]) VALUES (3, 1, 3, N'VOC101', 5, 20, 16, CAST(N'2024-10-15T00:00:00.000' AS DateTime), CAST(N'2024-12-30T00:00:00.000' AS DateTime), 1000000, N'Ended')
GO
INSERT [dbo].[Class] ([class_id], [teacher_id], [course_id], [class_name], [minimum_students], [maximum_students], [number_of_students], [start_date], [end_date], [enrollment_fee], [status]) VALUES (4, 1, 4, N'VOC102', 5, 20, 0, CAST(N'2025-01-15T00:00:00.000' AS DateTime), CAST(N'2025-03-25T00:00:00.000' AS DateTime), 1000000, N'Open')
GO
INSERT [dbo].[Class] ([class_id], [teacher_id], [course_id], [class_name], [minimum_students], [maximum_students], [number_of_students], [start_date], [end_date], [enrollment_fee], [status]) VALUES (5, 1, 5, N'PRO101', 5, 20, 16, CAST(N'2024-09-30T00:00:00.000' AS DateTime), CAST(N'2024-12-28T00:00:00.000' AS DateTime), 1000000, N'Ended')
GO
INSERT [dbo].[Class] ([class_id], [teacher_id], [course_id], [class_name], [minimum_students], [maximum_students], [number_of_students], [start_date], [end_date], [enrollment_fee], [status]) VALUES (6, 1, 6, N'PRO102', 5, 20, 16, CAST(N'2024-09-30T00:00:00.000' AS DateTime), CAST(N'2024-12-30T00:00:00.000' AS DateTime), 1000000, N'Ended')
GO
INSERT [dbo].[Class] ([class_id], [teacher_id], [course_id], [class_name], [minimum_students], [maximum_students], [number_of_students], [start_date], [end_date], [enrollment_fee], [status]) VALUES (7, 1, 7, N'GRAM103', 5, 20, 0, CAST(N'2024-10-01T00:00:00.000' AS DateTime), CAST(N'2024-10-28T00:00:00.000' AS DateTime), 1000000, N'Draft')
GO
INSERT [dbo].[Class] ([class_id], [teacher_id], [course_id], [class_name], [minimum_students], [maximum_students], [number_of_students], [start_date], [end_date], [enrollment_fee], [status]) VALUES (8, 1, 8, N'PRO103', 5, 20, 0, CAST(N'2024-09-30T00:00:00.000' AS DateTime), CAST(N'2024-12-30T00:00:00.000' AS DateTime), 1000000, N'Draft')
GO
INSERT [dbo].[Class] ([class_id], [teacher_id], [course_id], [class_name], [minimum_students], [maximum_students], [number_of_students], [start_date], [end_date], [enrollment_fee], [status]) VALUES (9, 1, 9, N'VOC103', 5, 20, 0, CAST(N'2024-09-30T00:00:00.000' AS DateTime), CAST(N'2024-12-30T00:00:00.000' AS DateTime), 1000000, N'Draft')
GO
INSERT [dbo].[Class] ([class_id], [teacher_id], [course_id], [class_name], [minimum_students], [maximum_students], [number_of_students], [start_date], [end_date], [enrollment_fee], [status]) VALUES (10, 1, 1, N'GRAM201', 5, 20, 0, CAST(N'2024-09-30T00:00:00.000' AS DateTime), CAST(N'2024-12-30T00:00:00.000' AS DateTime), 1000000, N'Draft')
GO
SET IDENTITY_INSERT [dbo].[Class] OFF
GO
SET IDENTITY_INSERT [dbo].[Course] ON 
GO
INSERT [dbo].[Course] ([course_id], [course_name], [course_type], [description], [status], [total_hours], [image], [required_age], [difficulty], [category]) VALUES (1, N'Grammar Foundations: Building Strong Sentences', N'A1', N'Middle School Course', N'Available', 56, NULL, 11, N'Beginner', N'Grammar')
GO
INSERT [dbo].[Course] ([course_id], [course_name], [course_type], [description], [status], [total_hours], [image], [required_age], [difficulty], [category]) VALUES (2, N'Mastering Tenses: A Grammar Guide', N'B1', N'Middle School Course', N'Draft', 56, NULL, 12, N'Intermediate', N'Grammar')
GO
INSERT [dbo].[Course] ([course_id], [course_name], [course_type], [description], [status], [total_hours], [image], [required_age], [difficulty], [category]) VALUES (3, N'Word Wizardry: Advanced Vocabulary Skills', N'B1', N'Middle School Course', N'Available', 56, NULL, 12, N'Intermediate', N'Vocabulary')
GO
INSERT [dbo].[Course] ([course_id], [course_name], [course_type], [description], [status], [total_hours], [image], [required_age], [difficulty], [category]) VALUES (4, N'Vocabulary Boosters: Everyday Words', N'A2', N'Middle School Course', N'Available', 56, NULL, 11, N'Beginner', N'Vocabulary')
GO
INSERT [dbo].[Course] ([course_id], [course_name], [course_type], [description], [status], [total_hours], [image], [required_age], [difficulty], [category]) VALUES (5, N'Perfect Pronunciation Basics', N'A1', N'Middle School Course', N'Draft', 56, NULL, 12, N'Intermediate', N'Pronunciation')
GO
INSERT [dbo].[Course] ([course_id], [course_name], [course_type], [description], [status], [total_hours], [image], [required_age], [difficulty], [category]) VALUES (6, N'Speak Clearly: English Sounds Made Easy', N'A2', N'Middle School Course', N'Available', 56, NULL, 11, N'Beginner', N'Pronunciation')
GO
INSERT [dbo].[Course] ([course_id], [course_name], [course_type], [description], [status], [total_hours], [image], [required_age], [difficulty], [category]) VALUES (7, N'Grammar in Action: Fixing Common Mistakes', N'B2', N'Middle School Course', N'Draft', 56, NULL, 12, N'Intermediate', N'Grammar')
GO
INSERT [dbo].[Course] ([course_id], [course_name], [course_type], [description], [status], [total_hours], [image], [required_age], [difficulty], [category]) VALUES (8, N'Pronounce It Right: English Challenges', N'A2', N'Middle School Course', N'Draft', 56, NULL, 11, N'Beginner', N'Pronunciation')
GO
INSERT [dbo].[Course] ([course_id], [course_name], [course_type], [description], [status], [total_hours], [image], [required_age], [difficulty], [category]) VALUES (9, N'Speak and Spell: Fun Vocabulary Practice', N'B2', N'Middle School Course', N'Draft', 56, NULL, 12, N'Intermediate', N'Vocabulary')
GO
SET IDENTITY_INSERT [dbo].[Course] OFF
GO
SET IDENTITY_INSERT [dbo].[Enroll] ON 
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (1, 1, 1, 1, CAST(N'2024-09-20T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (2, 2, 1, 2, CAST(N'2024-09-21T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (3, 3, 1, 3, CAST(N'2024-09-22T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (4, 4, 1, 4, CAST(N'2024-09-23T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (5, 5, 1, 5, CAST(N'2024-09-24T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (6, 6, 1, 6, CAST(N'2024-09-24T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (7, 7, 1, 7, CAST(N'2024-09-24T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (8, 8, 1, 8, CAST(N'2024-09-24T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (9, 9, 1, 9, CAST(N'2024-09-24T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (10, 10, 1, 10, CAST(N'2024-09-24T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (11, 11, 1, 11, CAST(N'2024-09-24T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (12, 12, 1, 12, CAST(N'2024-09-24T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (13, 13, 1, 13, CAST(N'2024-09-24T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (14, 14, 1, 14, CAST(N'2024-09-24T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (15, 15, 1, 15, CAST(N'2024-09-24T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (16, 16, 1, 16, CAST(N'2024-09-24T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (17, 17, 2, 17, CAST(N'2024-12-10T00:00:00.000' AS DateTime), CAST(N'2024-12-15T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (18, 18, 2, 18, CAST(N'2024-12-11T00:00:00.000' AS DateTime), CAST(N'2024-12-15T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (19, 19, 2, 19, CAST(N'2024-12-12T00:00:00.000' AS DateTime), CAST(N'2024-12-15T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (20, 20, 2, 20, CAST(N'2024-12-13T00:00:00.000' AS DateTime), CAST(N'2024-12-15T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (21, 21, 2, 21, CAST(N'2024-12-14T00:00:00.000' AS DateTime), CAST(N'2024-12-15T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (22, 22, 2, 22, CAST(N'2024-12-14T00:00:00.000' AS DateTime), CAST(N'2024-12-15T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (23, 23, 2, 23, CAST(N'2024-12-14T00:00:00.000' AS DateTime), CAST(N'2024-12-15T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (24, 24, 2, 24, CAST(N'2024-12-14T00:00:00.000' AS DateTime), CAST(N'2024-12-15T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (25, 25, 2, 25, CAST(N'2024-12-14T00:00:00.000' AS DateTime), CAST(N'2024-12-15T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (26, 26, 2, 26, CAST(N'2024-12-14T00:00:00.000' AS DateTime), CAST(N'2024-12-15T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (27, 27, 2, 27, CAST(N'2024-12-14T00:00:00.000' AS DateTime), CAST(N'2024-12-15T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (28, 28, 2, 28, CAST(N'2024-12-14T00:00:00.000' AS DateTime), CAST(N'2024-12-15T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (29, 29, 2, 29, CAST(N'2024-12-14T00:00:00.000' AS DateTime), CAST(N'2024-12-15T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (30, 30, 2, 30, CAST(N'2024-12-14T00:00:00.000' AS DateTime), CAST(N'2024-12-15T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (31, 31, 2, 31, CAST(N'2024-12-14T00:00:00.000' AS DateTime), CAST(N'2024-12-15T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (32, 32, 2, 32, CAST(N'2024-12-14T00:00:00.000' AS DateTime), CAST(N'2024-12-15T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (33, 1, 3, 33, CAST(N'2024-09-20T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (34, 2, 3, 34, CAST(N'2024-09-21T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (35, 3, 3, 35, CAST(N'2024-09-22T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (36, 4, 3, 36, CAST(N'2024-09-23T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (37, 5, 3, 37, CAST(N'2024-09-24T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (38, 6, 3, 38, CAST(N'2024-09-24T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (39, 7, 3, 39, CAST(N'2024-09-24T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (40, 8, 3, 40, CAST(N'2024-09-24T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (41, 9, 3, 41, CAST(N'2024-09-24T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (42, 10, 3, 42, CAST(N'2024-09-24T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (43, 11, 3, 43, CAST(N'2024-09-24T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (44, 12, 3, 44, CAST(N'2024-09-24T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (45, 13, 3, 45, CAST(N'2024-09-24T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (46, 14, 3, 46, CAST(N'2024-09-24T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (47, 15, 3, 47, CAST(N'2024-09-24T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (48, 16, 3, 48, CAST(N'2024-09-24T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (49, 1, 5, 49, CAST(N'2024-09-20T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (50, 2, 5, 50, CAST(N'2024-09-21T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (51, 3, 5, 51, CAST(N'2024-09-22T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (52, 4, 5, 52, CAST(N'2024-09-23T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (53, 5, 5, 53, CAST(N'2024-09-24T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (54, 6, 5, 54, CAST(N'2024-09-24T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (55, 7, 5, 55, CAST(N'2024-09-24T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (56, 8, 5, 56, CAST(N'2024-09-24T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (57, 9, 5, 57, CAST(N'2024-09-24T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (58, 10, 5, 58, CAST(N'2024-09-24T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (59, 11, 5, 59, CAST(N'2024-09-24T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (60, 12, 5, 60, CAST(N'2024-09-24T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (61, 13, 5, 61, CAST(N'2024-09-24T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (62, 14, 5, 62, CAST(N'2024-09-24T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (63, 15, 5, 63, CAST(N'2024-09-24T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (64, 16, 5, 64, CAST(N'2024-09-24T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (65, 32, 6, 65, CAST(N'2024-09-20T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (66, 33, 6, 66, CAST(N'2024-09-21T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (67, 34, 6, 67, CAST(N'2024-09-22T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (68, 35, 6, 68, CAST(N'2024-09-23T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (69, 36, 6, 69, CAST(N'2024-09-24T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (70, 37, 6, 70, CAST(N'2024-09-24T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (71, 38, 6, 71, CAST(N'2024-09-24T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (72, 39, 6, 72, CAST(N'2024-09-24T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (73, 40, 6, 73, CAST(N'2024-09-24T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (74, 41, 6, 74, CAST(N'2024-09-24T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (75, 42, 6, 75, CAST(N'2024-09-24T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (76, 43, 6, 76, CAST(N'2024-09-24T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (77, 44, 6, 77, CAST(N'2024-09-24T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (78, 45, 6, 78, CAST(N'2024-09-24T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (79, 46, 6, 79, CAST(N'2024-09-24T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (80, 47, 6, 80, CAST(N'2024-09-24T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (81, 36, 4, 81, CAST(N'2025-01-03T00:00:00.000' AS DateTime), CAST(N'2025-01-07T01:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (82, 37, 4, 82, CAST(N'2025-01-04T00:00:00.000' AS DateTime), CAST(N'2025-01-07T01:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (83, 38, 4, 83, CAST(N'2025-01-04T00:00:00.000' AS DateTime), CAST(N'2025-01-07T01:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (84, 39, 4, 84, CAST(N'2025-01-05T00:00:00.000' AS DateTime), CAST(N'2025-01-07T01:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (85, 40, 4, 85, CAST(N'2025-01-06T00:00:00.000' AS DateTime), CAST(N'2025-01-07T01:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (86, 41, 4, 86, CAST(N'2025-01-06T00:00:00.000' AS DateTime), CAST(N'2025-01-07T01:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (87, 42, 4, 87, CAST(N'2025-01-06T00:00:00.000' AS DateTime), CAST(N'2025-01-07T01:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (88, 43, 4, 88, CAST(N'2025-01-06T00:00:00.000' AS DateTime), CAST(N'2025-01-07T01:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (89, 44, 4, 89, CAST(N'2025-01-06T00:00:00.000' AS DateTime), CAST(N'2025-01-07T01:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (90, 45, 4, 90, CAST(N'2025-01-06T00:00:00.000' AS DateTime), CAST(N'2025-01-07T01:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (91, 46, 4, 91, CAST(N'2025-01-06T00:00:00.000' AS DateTime), CAST(N'2025-01-07T01:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (92, 47, 4, 92, CAST(N'2025-01-07T00:00:00.000' AS DateTime), CAST(N'2025-01-07T01:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (93, 20, 4, 93, CAST(N'2025-01-07T00:00:00.000' AS DateTime), CAST(N'2025-01-07T01:00:00.000' AS DateTime), N'Enrolled')
GO
INSERT [dbo].[Enroll] ([enroll_id], [student_id], [class_id], [transaction_id], [registration_date], [enrolled_date], [status]) VALUES (94, 21, 4, 94, CAST(N'2025-01-07T00:00:00.000' AS DateTime), CAST(N'2025-01-07T01:00:00.000' AS DateTime), N'Enrolled')
GO

SET IDENTITY_INSERT [dbo].[Enroll] OFF
GO
SET IDENTITY_INSERT [dbo].[Homework] ON 
GO
INSERT [dbo].[Homework] ([homework_id], [session_id], [title], [description], [game_config_id], [start_date], [end_date], [hours], [type]) VALUES (1, 1, N'Basic Homework', N'Learn the Basic', NULL, CAST(N'2024-09-30T00:00:00.000' AS DateTime), CAST(N'2024-12-15T00:00:00.000' AS DateTime), 1, N'Vocabulary')
GO
INSERT [dbo].[Homework] ([homework_id], [session_id], [title], [description], [game_config_id], [start_date], [end_date], [hours], [type]) VALUES (2, 13, N'Standard Homework', N'Get used to English', NULL, CAST(N'2024-12-15T00:00:00.000' AS DateTime), CAST(N'2025-02-15T00:00:00.000' AS DateTime), 1, N'Vocabulary')
GO
SET IDENTITY_INSERT [dbo].[Homework] OFF
GO
SET IDENTITY_INSERT [dbo].[HomeworkAnswer] ON 
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (1, 1, N'to', N'Correct')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (2, 1, N'top', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (3, 1, N'off', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (4, 1, N'with', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (5, 2, N'of', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (6, 2, N'with', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (7, 2, N'at', N'Correct')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (8, 2, N'to', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (9, 3, N'to', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (10, 3, N'of', N'Correct')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (11, 3, N'to', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (12, 3, N'to', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (13, 4, N'to', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (14, 4, N'in', N'Correct')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (15, 4, N'with', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (16, 4, N'to', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (17, 5, N'of', N'Correct')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (18, 5, N'off', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (19, 5, N'to', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (20, 5, N'the', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (21, 6, N'to', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (22, 6, N'to', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (23, 6, N'to', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (24, 6, N'about', N'Correct')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (25, 7, N'to', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (26, 7, N'to', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (27, 7, N'for', N'Correct')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (28, 7, N'to', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (29, 8, N'to', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (30, 8, N'to', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (31, 8, N'for', N'Correct')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (32, 8, N'to', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (33, 9, N'to', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (34, 9, N'for', N'Correct')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (35, 9, N'not', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (36, 9, N'the', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (37, 10, N'with', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (38, 10, N'off', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (39, 10, N'them', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (40, 10, N'of', N'Correct')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (41, 11, N'to', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (42, 11, N'to', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (43, 11, N'about', N'Correct')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (44, 11, N'to', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (45, 12, N'to', N'Correct')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (46, 12, N'to', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (47, 12, N'to', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (48, 12, N'to', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (49, 13, N'to', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (50, 13, N'with', N'Correct')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (51, 13, N'to', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (52, 13, N'to', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (53, 14, N'to', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (54, 14, N'to', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (55, 14, N'of', N'Correct')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (56, 14, N'to', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (57, 15, N'to', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (58, 15, N'to', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (59, 15, N'to', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (60, 15, N'about', N'Correct')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (61, 16, N'in', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (62, 16, N'for', N'Correct')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (63, 16, N'with', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (64, 16, N'of', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (65, 17, N'about', N'Correct')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (66, 17, N'on', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (67, 17, N'in', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (68, 17, N'to', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (69, 18, N'for', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (70, 18, N'about', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (71, 18, N'in', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (72, 18, N'at', N'Correct')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (73, 19, N'to', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (74, 19, N'with', N'Correct')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (75, 19, N'for', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (76, 19, N'in', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (77, 20, N'on', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (78, 20, N'to', N'Incorrect')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (79, 20, N'for', N'Correct')
GO
INSERT [dbo].[HomeworkAnswer] ([homework_answer_id], [homework_question_id], [answer], [type]) VALUES (80, 20, N'with', N'Incorrect')
GO
SET IDENTITY_INSERT [dbo].[HomeworkAnswer] OFF
GO
SET IDENTITY_INSERT [dbo].[HomeworkQuestion] ON 
GO
INSERT [dbo].[HomeworkQuestion] ([homework_question_id], [homework_id], [question]) VALUES (1, 1, N'I want _ go home.')
GO
INSERT [dbo].[HomeworkQuestion] ([homework_question_id], [homework_id], [question]) VALUES (2, 1, N'She is good _ dancing.')
GO
INSERT [dbo].[HomeworkQuestion] ([homework_question_id], [homework_id], [question]) VALUES (3, 1, N'He is afraid _ the dark.')
GO
INSERT [dbo].[HomeworkQuestion] ([homework_question_id], [homework_id], [question]) VALUES (4, 1, N'They are interested _ learning new things.')
GO
INSERT [dbo].[HomeworkQuestion] ([homework_question_id], [homework_id], [question]) VALUES (5, 1, N'She is tired _ studying.')
GO
INSERT [dbo].[HomeworkQuestion] ([homework_question_id], [homework_id], [question]) VALUES (6, 1, N'I am excited _ the trip.')
GO
INSERT [dbo].[HomeworkQuestion] ([homework_question_id], [homework_id], [question]) VALUES (7, 1, N'He is famous _ his paintings.')
GO
INSERT [dbo].[HomeworkQuestion] ([homework_question_id], [homework_id], [question]) VALUES (8, 1, N'We are late _ the meeting.')
GO
INSERT [dbo].[HomeworkQuestion] ([homework_question_id], [homework_id], [question]) VALUES (9, 1, N'I am responsible _ organizing the event.')
GO
INSERT [dbo].[HomeworkQuestion] ([homework_question_id], [homework_id], [question]) VALUES (10, 1, N'They are proud _ their accomplishments.')
GO
INSERT [dbo].[HomeworkQuestion] ([homework_question_id], [homework_id], [question]) VALUES (11, 2, N'She is worried _ the exam.')
GO
INSERT [dbo].[HomeworkQuestion] ([homework_question_id], [homework_id], [question]) VALUES (12, 2, N'He is married _ a doctor.')
GO
INSERT [dbo].[HomeworkQuestion] ([homework_question_id], [homework_id], [question]) VALUES (13, 2, N'I am bored _ this movie.')
GO
INSERT [dbo].[HomeworkQuestion] ([homework_question_id], [homework_id], [question]) VALUES (14, 2, N'She is jealous _ her friend’s success.')
GO
INSERT [dbo].[HomeworkQuestion] ([homework_question_id], [homework_id], [question]) VALUES (15, 2, N'He is angry _ the delay.')
GO
INSERT [dbo].[HomeworkQuestion] ([homework_question_id], [homework_id], [question]) VALUES (16, 2, N'They are ready _ the party.')
GO
INSERT [dbo].[HomeworkQuestion] ([homework_question_id], [homework_id], [question]) VALUES (17, 2, N'I am addicted _ video games.')
GO
INSERT [dbo].[HomeworkQuestion] ([homework_question_id], [homework_id], [question]) VALUES (18, 2, N'She is good _ solving problems.')
GO
INSERT [dbo].[HomeworkQuestion] ([homework_question_id], [homework_id], [question]) VALUES (19, 2, N'We are familiar _ this software.')
GO
INSERT [dbo].[HomeworkQuestion] ([homework_question_id], [homework_id], [question]) VALUES (20, 2, N'He is famous _ being an actor.')
GO
SET IDENTITY_INSERT [dbo].[HomeworkQuestion] OFF
GO
SET IDENTITY_INSERT [dbo].[HomeworkResult] ON 
GO
INSERT [dbo].[HomeworkResult] ([homework_result_id], [total_point], [total_correct_answers], [playtime]) VALUES (1, 80, 8, CAST(N'00:27:21' AS Time))
GO
INSERT [dbo].[HomeworkResult] ([homework_result_id], [total_point], [total_correct_answers], [playtime]) VALUES (2, 50, 5, CAST(N'00:28:55' AS Time))
GO
INSERT [dbo].[HomeworkResult] ([homework_result_id], [total_point], [total_correct_answers], [playtime]) VALUES (3, 70, 7, CAST(N'00:25:21' AS Time))
GO
INSERT [dbo].[HomeworkResult] ([homework_result_id], [total_point], [total_correct_answers], [playtime]) VALUES (4, 70, 7, CAST(N'00:24:21' AS Time))
GO
INSERT [dbo].[HomeworkResult] ([homework_result_id], [total_point], [total_correct_answers], [playtime]) VALUES (5, 60, 6, CAST(N'00:28:57' AS Time))
GO
INSERT [dbo].[HomeworkResult] ([homework_result_id], [total_point], [total_correct_answers], [playtime]) VALUES (6, 50, 5, CAST(N'00:28:41' AS Time))
GO
INSERT [dbo].[HomeworkResult] ([homework_result_id], [total_point], [total_correct_answers], [playtime]) VALUES (7, 90, 9, CAST(N'00:10:23' AS Time))
GO
INSERT [dbo].[HomeworkResult] ([homework_result_id], [total_point], [total_correct_answers], [playtime]) VALUES (8, 80, 8, CAST(N'00:15:55' AS Time))
GO
INSERT [dbo].[HomeworkResult] ([homework_result_id], [total_point], [total_correct_answers], [playtime]) VALUES (9, 70, 7, CAST(N'00:20:03' AS Time))
GO
INSERT [dbo].[HomeworkResult] ([homework_result_id], [total_point], [total_correct_answers], [playtime]) VALUES (10, 60, 6, CAST(N'00:23:05' AS Time))
GO
INSERT [dbo].[HomeworkResult] ([homework_result_id], [total_point], [total_correct_answers], [playtime]) VALUES (11, 80, 8, CAST(N'00:20:10' AS Time))
GO
INSERT [dbo].[HomeworkResult] ([homework_result_id], [total_point], [total_correct_answers], [playtime]) VALUES (12, 90, 9, CAST(N'00:13:29' AS Time))
GO
INSERT [dbo].[HomeworkResult] ([homework_result_id], [total_point], [total_correct_answers], [playtime]) VALUES (13, 80, 8, CAST(N'00:25:31' AS Time))
GO
INSERT [dbo].[HomeworkResult] ([homework_result_id], [total_point], [total_correct_answers], [playtime]) VALUES (14, 80, 8, CAST(N'00:27:01' AS Time))
GO
INSERT [dbo].[HomeworkResult] ([homework_result_id], [total_point], [total_correct_answers], [playtime]) VALUES (15, 60, 6, CAST(N'00:16:18' AS Time))
GO
INSERT [dbo].[HomeworkResult] ([homework_result_id], [total_point], [total_correct_answers], [playtime]) VALUES (16, 60, 6, CAST(N'00:05:37' AS Time))
GO
INSERT [dbo].[HomeworkResult] ([homework_result_id], [total_point], [total_correct_answers], [playtime]) VALUES (17, 70, 7, CAST(N'00:27:21' AS Time))
GO
INSERT [dbo].[HomeworkResult] ([homework_result_id], [total_point], [total_correct_answers], [playtime]) VALUES (18, 80, 8, CAST(N'00:28:55' AS Time))
GO
INSERT [dbo].[HomeworkResult] ([homework_result_id], [total_point], [total_correct_answers], [playtime]) VALUES (19, 80, 8, CAST(N'00:25:21' AS Time))
GO
INSERT [dbo].[HomeworkResult] ([homework_result_id], [total_point], [total_correct_answers], [playtime]) VALUES (20, 90, 9, CAST(N'00:24:21' AS Time))
GO
INSERT [dbo].[HomeworkResult] ([homework_result_id], [total_point], [total_correct_answers], [playtime]) VALUES (21, 70, 7, CAST(N'00:28:57' AS Time))
GO
INSERT [dbo].[HomeworkResult] ([homework_result_id], [total_point], [total_correct_answers], [playtime]) VALUES (22, 60, 6, CAST(N'00:28:41' AS Time))
GO
INSERT [dbo].[HomeworkResult] ([homework_result_id], [total_point], [total_correct_answers], [playtime]) VALUES (23, 80, 8, CAST(N'00:10:23' AS Time))
GO
INSERT [dbo].[HomeworkResult] ([homework_result_id], [total_point], [total_correct_answers], [playtime]) VALUES (24, 70, 7, CAST(N'00:15:55' AS Time))
GO
INSERT [dbo].[HomeworkResult] ([homework_result_id], [total_point], [total_correct_answers], [playtime]) VALUES (25, 80, 8, CAST(N'00:20:03' AS Time))
GO
INSERT [dbo].[HomeworkResult] ([homework_result_id], [total_point], [total_correct_answers], [playtime]) VALUES (26, 70, 7, CAST(N'00:23:05' AS Time))
GO
INSERT [dbo].[HomeworkResult] ([homework_result_id], [total_point], [total_correct_answers], [playtime]) VALUES (27, 80, 8, CAST(N'00:20:10' AS Time))
GO
INSERT [dbo].[HomeworkResult] ([homework_result_id], [total_point], [total_correct_answers], [playtime]) VALUES (28, 90, 9, CAST(N'00:13:29' AS Time))
GO
INSERT [dbo].[HomeworkResult] ([homework_result_id], [total_point], [total_correct_answers], [playtime]) VALUES (29, 60, 6, CAST(N'00:25:31' AS Time))
GO
INSERT [dbo].[HomeworkResult] ([homework_result_id], [total_point], [total_correct_answers], [playtime]) VALUES (30, 70, 7, CAST(N'00:27:01' AS Time))
GO
INSERT [dbo].[HomeworkResult] ([homework_result_id], [total_point], [total_correct_answers], [playtime]) VALUES (31, 80, 8, CAST(N'00:16:18' AS Time))
GO
INSERT [dbo].[HomeworkResult] ([homework_result_id], [total_point], [total_correct_answers], [playtime]) VALUES (32, 70, 7, CAST(N'00:05:37' AS Time))
GO
SET IDENTITY_INSERT [dbo].[HomeworkResult] OFF
GO
SET IDENTITY_INSERT [dbo].[Parent] ON 
GO
INSERT [dbo].[Parent] ([parent_id], [account_id], [email], [phone], [address]) VALUES (1, 3, N'vinhbh@gmail.com', N'0938743302', N'413/21 Le Van Sy Street, Ward 12, District 3')
GO
INSERT [dbo].[Parent] ([parent_id], [account_id], [email], [phone], [address]) VALUES (2, 5, N'khoapa@gmail.com', N'0939956001', N'60, Ho Han Thuong Street')
GO
INSERT [dbo].[Parent] ([parent_id], [account_id], [email], [phone], [address]) VALUES (3, 7, N'duydnm@gmail.com', N'0999558026', N'271 Nguyen Trai Street')
GO
INSERT [dbo].[Parent] ([parent_id], [account_id], [email], [phone], [address]) VALUES (4, 10, N'nganlt@gmail.com', N'0939438834', N'426 Tran Khat Chan Street')
GO
INSERT [dbo].[Parent] ([parent_id], [account_id], [email], [phone], [address]) VALUES (5, 13, N'nammn@gmail.com', N'0935420752', N'54 Phung Van Cung')
GO
INSERT [dbo].[Parent] ([parent_id], [account_id], [email], [phone], [address]) VALUES (6, 15, N'duybd@gmail.com', N'0983822065', N'108 Nguyen Van Cu Street')
GO
INSERT [dbo].[Parent] ([parent_id], [account_id], [email], [phone], [address]) VALUES (7, 17, N'khanhhn@gmail.com', N'0938220659', N'12 Tran Quang Khai Street, Hong Bang District')
GO
INSERT [dbo].[Parent] ([parent_id], [account_id], [email], [phone], [address]) VALUES (8, 20, N'khoaba@gmail.com', N'0973344678', N' 136 Hang Trong, Hang Trong Ward')
GO
INSERT [dbo].[Parent] ([parent_id], [account_id], [email], [phone], [address]) VALUES (9, 62, N'haita@gmail.com', N'0912838984', N'61 Tay Son, Quang Trung, Dong Da')
GO
INSERT [dbo].[Parent] ([parent_id], [account_id], [email], [phone], [address]) VALUES (10, 63, N'anhnl@gmail.com', N'0987213462', N'15 Hoang Dieu, Ward 10')
GO
INSERT [dbo].[Parent] ([parent_id], [account_id], [email], [phone], [address]) VALUES (11, 64, N'thienlh@gmail.com', N'0918829734', N'207 Hai Thuong Lan Ong Street, Ward 13, District 5')
GO
INSERT [dbo].[Parent] ([parent_id], [account_id], [email], [phone], [address]) VALUES (12, 65, N'cuongvm@gmail.com', N'0923028345', N'36/7 Trung Hamlet')
GO
INSERT [dbo].[Parent] ([parent_id], [account_id], [email], [phone], [address]) VALUES (13, 66, N'khaitv@gmail.com', N'0923847435', N'12 Pham Huy Thong Street')
GO
INSERT [dbo].[Parent] ([parent_id], [account_id], [email], [phone], [address]) VALUES (14, 67, N'manhvv@gmail.com', N'0912049208', N'52 Kham Thien Street')
GO
SET IDENTITY_INSERT [dbo].[Parent] OFF
GO
SET IDENTITY_INSERT [dbo].[Role] ON 
GO
INSERT [dbo].[Role] ([role_id], [role_name]) VALUES (1, N'Admin')
GO
INSERT [dbo].[Role] ([role_id], [role_name]) VALUES (2, N'Student')
GO
INSERT [dbo].[Role] ([role_id], [role_name]) VALUES (3, N'Teacher')
GO
INSERT [dbo].[Role] ([role_id], [role_name]) VALUES (4, N'Parent')
GO
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
SET IDENTITY_INSERT [dbo].[Schedule] ON 
GO
INSERT [dbo].[Schedule] ([schedule_id], [session_id], [class_id], [schedule_date], [start_time], [end_time], [status]) VALUES (1, 1, 1, CAST(N'2024-11-10T00:00:00.000' AS DateTime), CAST(N'12:00:00' AS Time), CAST(N'14:00:00' AS Time), N'Ended')
GO
INSERT [dbo].[Schedule] ([schedule_id], [session_id], [class_id], [schedule_date], [start_time], [end_time], [status]) VALUES (2, 2, 1, CAST(N'2024-12-16T00:00:00.000' AS DateTime), CAST(N'12:00:00' AS Time), CAST(N'14:00:00' AS Time), N'Ended')
GO
INSERT [dbo].[Schedule] ([schedule_id], [session_id], [class_id], [schedule_date], [start_time], [end_time], [status]) VALUES (3, 3, 1, CAST(N'2024-12-20T00:00:00.000' AS DateTime), CAST(N'12:00:00' AS Time), CAST(N'14:00:00' AS Time), N'Ended')
GO
INSERT [dbo].[Schedule] ([schedule_id], [session_id], [class_id], [schedule_date], [start_time], [end_time], [status]) VALUES (4, 4, 1, CAST(N'2025-01-15T00:00:00.000' AS DateTime), CAST(N'12:00:00' AS Time), CAST(N'14:00:00' AS Time), N'Upcoming')
GO
INSERT [dbo].[Schedule] ([schedule_id], [session_id], [class_id], [schedule_date], [start_time], [end_time], [status]) VALUES (5, 5, 1, CAST(N'2025-01-20T00:00:00.000' AS DateTime), CAST(N'12:00:00' AS Time), CAST(N'14:00:00' AS Time), N'Upcoming')
GO
INSERT [dbo].[Schedule] ([schedule_id], [session_id], [class_id], [schedule_date], [start_time], [end_time], [status]) VALUES (6, 6, 1, CAST(N'2025-01-24T00:00:00.000' AS DateTime), CAST(N'12:00:00' AS Time), CAST(N'14:00:00' AS Time), N'Upcoming')
GO
INSERT [dbo].[Schedule] ([schedule_id], [session_id], [class_id], [schedule_date], [start_time], [end_time], [status]) VALUES (7, 7, 1, CAST(N'2025-02-01T00:00:00.000' AS DateTime), CAST(N'12:00:00' AS Time), CAST(N'14:00:00' AS Time), N'Upcoming')
GO
INSERT [dbo].[Schedule] ([schedule_id], [session_id], [class_id], [schedule_date], [start_time], [end_time], [status]) VALUES (8, 8, 1, CAST(N'2025-02-05T00:00:00.000' AS DateTime), CAST(N'12:00:00' AS Time), CAST(N'14:00:00' AS Time), N'Upcoming')
GO
INSERT [dbo].[Schedule] ([schedule_id], [session_id], [class_id], [schedule_date], [start_time], [end_time], [status]) VALUES (9, 9, 1, CAST(N'2025-02-09T00:00:00.000' AS DateTime), CAST(N'12:00:00' AS Time), CAST(N'14:00:00' AS Time), N'Upcoming')
GO
INSERT [dbo].[Schedule] ([schedule_id], [session_id], [class_id], [schedule_date], [start_time], [end_time], [status]) VALUES (10, 10, 1, CAST(N'2025-02-15T00:00:00.000' AS DateTime), CAST(N'12:00:00' AS Time), CAST(N'14:00:00' AS Time), N'Upcoming')
GO
INSERT [dbo].[Schedule] ([schedule_id], [session_id], [class_id], [schedule_date], [start_time], [end_time], [status]) VALUES (11, 11, 2, CAST(N'2024-12-16T00:00:00.000' AS DateTime), CAST(N'12:00:00' AS Time), CAST(N'14:00:00' AS Time), N'Ended')
GO
INSERT [dbo].[Schedule] ([schedule_id], [session_id], [class_id], [schedule_date], [start_time], [end_time], [status]) VALUES (12, 12, 2, CAST(N'2024-12-20T00:00:00.000' AS DateTime), CAST(N'12:00:00' AS Time), CAST(N'14:00:00' AS Time), N'Ended')
GO
INSERT [dbo].[Schedule] ([schedule_id], [session_id], [class_id], [schedule_date], [start_time], [end_time], [status]) VALUES (13, 13, 2, CAST(N'2025-01-15T00:00:00.000' AS DateTime), CAST(N'12:00:00' AS Time), CAST(N'14:00:00' AS Time), N'Upcoming')
GO
INSERT [dbo].[Schedule] ([schedule_id], [session_id], [class_id], [schedule_date], [start_time], [end_time], [status]) VALUES (14, 14, 2, CAST(N'2025-01-20T00:00:00.000' AS DateTime), CAST(N'12:00:00' AS Time), CAST(N'14:00:00' AS Time), N'Upcoming')
GO
INSERT [dbo].[Schedule] ([schedule_id], [session_id], [class_id], [schedule_date], [start_time], [end_time], [status]) VALUES (15, 15, 2, CAST(N'2025-01-24T00:00:00.000' AS DateTime), CAST(N'12:00:00' AS Time), CAST(N'14:00:00' AS Time), N'Upcoming')
GO
INSERT [dbo].[Schedule] ([schedule_id], [session_id], [class_id], [schedule_date], [start_time], [end_time], [status]) VALUES (16, 16, 2, CAST(N'2025-02-01T00:00:00.000' AS DateTime), CAST(N'12:00:00' AS Time), CAST(N'14:00:00' AS Time), N'Upcoming')
GO
INSERT [dbo].[Schedule] ([schedule_id], [session_id], [class_id], [schedule_date], [start_time], [end_time], [status]) VALUES (17, 17, 2, CAST(N'2025-02-05T00:00:00.000' AS DateTime), CAST(N'12:00:00' AS Time), CAST(N'14:00:00' AS Time), N'Upcoming')
GO
INSERT [dbo].[Schedule] ([schedule_id], [session_id], [class_id], [schedule_date], [start_time], [end_time], [status]) VALUES (18, 18, 2, CAST(N'2025-02-09T00:00:00.000' AS DateTime), CAST(N'12:00:00' AS Time), CAST(N'14:00:00' AS Time), N'Upcoming')
GO
INSERT [dbo].[Schedule] ([schedule_id], [session_id], [class_id], [schedule_date], [start_time], [end_time], [status]) VALUES (19, 19, 2, CAST(N'2025-02-15T00:00:00.000' AS DateTime), CAST(N'12:00:00' AS Time), CAST(N'14:00:00' AS Time), N'Upcoming')
GO
INSERT [dbo].[Schedule] ([schedule_id], [session_id], [class_id], [schedule_date], [start_time], [end_time], [status]) VALUES (20, 20, 2, CAST(N'2025-02-18T00:00:00.000' AS DateTime), CAST(N'12:00:00' AS Time), CAST(N'14:00:00' AS Time), N'Upcoming')
GO
INSERT [dbo].[Schedule] ([schedule_id], [session_id], [class_id], [schedule_date], [start_time], [end_time], [status]) VALUES (21, 21, 3, CAST(N'2024-10-15T00:00:00.000' AS DateTime), CAST(N'12:00:00' AS Time), CAST(N'14:00:00' AS Time), N'Ended')
GO
INSERT [dbo].[Schedule] ([schedule_id], [session_id], [class_id], [schedule_date], [start_time], [end_time], [status]) VALUES (22, 22, 3, CAST(N'2024-10-22T00:00:00.000' AS DateTime), CAST(N'12:00:00' AS Time), CAST(N'14:00:00' AS Time), N'Ended')
GO
INSERT [dbo].[Schedule] ([schedule_id], [session_id], [class_id], [schedule_date], [start_time], [end_time], [status]) VALUES (23, 23, 3, CAST(N'2024-10-29T00:00:00.000' AS DateTime), CAST(N'12:00:00' AS Time), CAST(N'14:00:00' AS Time), N'Ended')
GO
INSERT [dbo].[Schedule] ([schedule_id], [session_id], [class_id], [schedule_date], [start_time], [end_time], [status]) VALUES (24, 24, 3, CAST(N'2024-11-07T00:00:00.000' AS DateTime), CAST(N'12:00:00' AS Time), CAST(N'14:00:00' AS Time), N'Ended')
GO
INSERT [dbo].[Schedule] ([schedule_id], [session_id], [class_id], [schedule_date], [start_time], [end_time], [status]) VALUES (25, 25, 3, CAST(N'2024-11-15T00:00:00.000' AS DateTime), CAST(N'12:00:00' AS Time), CAST(N'14:00:00' AS Time), N'Ended')
GO
INSERT [dbo].[Schedule] ([schedule_id], [session_id], [class_id], [schedule_date], [start_time], [end_time], [status]) VALUES (26, 26, 3, CAST(N'2024-11-22T00:00:00.000' AS DateTime), CAST(N'12:00:00' AS Time), CAST(N'14:00:00' AS Time), N'Ended')
GO
INSERT [dbo].[Schedule] ([schedule_id], [session_id], [class_id], [schedule_date], [start_time], [end_time], [status]) VALUES (27, 27, 3, CAST(N'2024-11-29T00:00:00.000' AS DateTime), CAST(N'12:00:00' AS Time), CAST(N'14:00:00' AS Time), N'Ended')
GO
INSERT [dbo].[Schedule] ([schedule_id], [session_id], [class_id], [schedule_date], [start_time], [end_time], [status]) VALUES (28, 28, 3, CAST(N'2024-12-06T00:00:00.000' AS DateTime), CAST(N'12:00:00' AS Time), CAST(N'14:00:00' AS Time), N'Ended')
GO
INSERT [dbo].[Schedule] ([schedule_id], [session_id], [class_id], [schedule_date], [start_time], [end_time], [status]) VALUES (29, 29, 3, CAST(N'2024-12-13T00:00:00.000' AS DateTime), CAST(N'12:00:00' AS Time), CAST(N'14:00:00' AS Time), N'Ended')
GO
INSERT [dbo].[Schedule] ([schedule_id], [session_id], [class_id], [schedule_date], [start_time], [end_time], [status]) VALUES (30, 30, 3, CAST(N'2024-12-20T00:00:00.000' AS DateTime), CAST(N'12:00:00' AS Time), CAST(N'14:00:00' AS Time), N'Ended')
GO
INSERT [dbo].[Schedule] ([schedule_id], [session_id], [class_id], [schedule_date], [start_time], [end_time], [status]) VALUES (31, 31, 4, CAST(N'2024-09-10T00:00:00.000' AS DateTime), CAST(N'12:00:00' AS Time), CAST(N'14:00:00' AS Time), N'Ended')
GO
INSERT [dbo].[Schedule] ([schedule_id], [session_id], [class_id], [schedule_date], [start_time], [end_time], [status]) VALUES (32, 32, 4, CAST(N'2024-09-15T00:00:00.000' AS DateTime), CAST(N'12:00:00' AS Time), CAST(N'14:00:00' AS Time), N'Ended')
GO
INSERT [dbo].[Schedule] ([schedule_id], [session_id], [class_id], [schedule_date], [start_time], [end_time], [status]) VALUES (33, 33, 4, CAST(N'2024-09-19T00:00:00.000' AS DateTime), CAST(N'12:00:00' AS Time), CAST(N'14:00:00' AS Time), N'Ended')
GO
INSERT [dbo].[Schedule] ([schedule_id], [session_id], [class_id], [schedule_date], [start_time], [end_time], [status]) VALUES (34, 34, 4, CAST(N'2024-10-01T00:00:00.000' AS DateTime), CAST(N'12:00:00' AS Time), CAST(N'14:00:00' AS Time), N'Ended')
GO
INSERT [dbo].[Schedule] ([schedule_id], [session_id], [class_id], [schedule_date], [start_time], [end_time], [status]) VALUES (35, 35, 4, CAST(N'2024-10-06T00:00:00.000' AS DateTime), CAST(N'12:00:00' AS Time), CAST(N'14:00:00' AS Time), N'Ended')
GO
INSERT [dbo].[Schedule] ([schedule_id], [session_id], [class_id], [schedule_date], [start_time], [end_time], [status]) VALUES (36, 36, 4, CAST(N'2024-10-13T00:00:00.000' AS DateTime), CAST(N'12:00:00' AS Time), CAST(N'14:00:00' AS Time), N'Ended')
GO
INSERT [dbo].[Schedule] ([schedule_id], [session_id], [class_id], [schedule_date], [start_time], [end_time], [status]) VALUES (37, 37, 4, CAST(N'2024-10-21T00:00:00.000' AS DateTime), CAST(N'12:00:00' AS Time), CAST(N'14:00:00' AS Time), N'Ended')
GO
INSERT [dbo].[Schedule] ([schedule_id], [session_id], [class_id], [schedule_date], [start_time], [end_time], [status]) VALUES (38, 38, 4, CAST(N'2024-10-26T00:00:00.000' AS DateTime), CAST(N'12:00:00' AS Time), CAST(N'14:00:00' AS Time), N'Ended')
GO
INSERT [dbo].[Schedule] ([schedule_id], [session_id], [class_id], [schedule_date], [start_time], [end_time], [status]) VALUES (39, 39, 4, CAST(N'2024-11-01T00:00:00.000' AS DateTime), CAST(N'12:00:00' AS Time), CAST(N'14:00:00' AS Time), N'Ended')
GO
INSERT [dbo].[Schedule] ([schedule_id], [session_id], [class_id], [schedule_date], [start_time], [end_time], [status]) VALUES (40, 40, 4, CAST(N'2024-11-10T00:00:00.000' AS DateTime), CAST(N'12:00:00' AS Time), CAST(N'14:00:00' AS Time), N'Ended')
GO
SET IDENTITY_INSERT [dbo].[Schedule] OFF
GO
SET IDENTITY_INSERT [dbo].[Session] ON 
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (1, 1, N'The Basics of Sentence Structure', N'Explore the building blocks of sentences, including subjects, verbs, and objects.', 2, 1)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (2, 1, N'Understanding Parts of Speech', N'Learn about nouns, verbs, adjectives, adverbs, and how they work together in sentences.', 2, 2)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (3, 1, N'Subject-Verb Agreement', N'Master the rules of agreement to ensure grammatical accuracy in your writing.', 2, 3)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (4, 1, N'Creating Compound Sentences', N'Discover how to connect ideas effectively using coordinating conjunctions.', 2, 4)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (5, 1, N'Using Modifiers Effectively', N'Learn to use adjectives and adverbs to add depth and clarity to your sentences.', 2, 5)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (6, 1, N'Punctuating Sentences Correctly', N'Understand the rules for using commas, periods, and other punctuation marks.', 2, 6)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (7, 1, N'Introducing Clauses and Phrases', N'Explore how to use dependent and independent clauses to create complex sentences.', 2, 7)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (8, 1, N'Writing Clear and Concise Sentences', N'Practice techniques for eliminating redundancy and improving clarity.', 2, 8)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (9, 1, N'Avoiding Common Grammar Errors', N'Identify and correct frequent grammar mistakes to enhance your writing.', 2, 9)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (10, 1, N'Grammar Wrap-up: Practice Quiz', N'Test your understanding of sentence structure and grammar rules with a comprehensive quiz.', 2, 10)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (11, 2, N'Present Tenses in Everyday Use', N'Learn to use present simple and continuous tenses for daily conversations.', 2, 1)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (12, 2, N'Exploring Past Simple and Continuous', N'Understand how to talk about completed actions and ongoing events in the past.', 2, 2)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (13, 2, N'Future Tenses: Planning Ahead', N'Master different ways to express future intentions, plans, and predictions.', 2, 3)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (14, 2, N'Perfect Tenses Simplified', N'Dive into the usage of perfect tenses to connect actions across different timeframes.', 2, 4)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (15, 2, N'When to Use Continuous Forms', N'Discover when and how to use continuous tenses for actions in progress.', 2, 5)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (16, 2, N'Common Tense Mistakes', N'Identify and avoid frequent tense errors to improve accuracy.', 2, 6)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (17, 2, N'Conditional Sentences Explained', N'Learn how to use different conditionals to discuss possibilities and hypothetical situations.', 2, 7)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (18, 2, N'Tenses in Storytelling', N'Practice combining tenses to narrate stories effectively.', 2, 8)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (19, 2, N'Practicing with Real-life Scenarios', N'Apply your knowledge of tenses to practical, everyday situations.', 2, 9)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (20, 2, N'Tense Review and Final Practice', N'Consolidate your understanding of tenses through review and hands-on exercises.', 2, 10)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (21, 3, N'Expanding Descriptive Vocabulary', N'Discover advanced descriptive words to make your communication more vivid.', 2, 1)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (22, 3, N'The Power of Adjectives and Adverbs', N'Learn how to effectively use adjectives and adverbs to enhance sentences.', 2, 2)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (23, 3, N'Talking About Emotions', N'Master vocabulary to express and describe emotions accurately.', 2, 3)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (24, 3, N'Formal vs. Informal Words', N'Understand the difference between formal and informal vocabulary for various contexts.', 2, 4)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (25, 3, N'Learning Prefixes and Suffixes', N'Expand your vocabulary by understanding how prefixes and suffixes modify meanings.', 2, 5)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (26, 3, N'Advanced Synonyms and Antonyms', N'Deepen your knowledge with high-level synonyms and antonyms for precision.', 2, 6)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (27, 3, N'Vocabulary for Work and Study', N'Acquire essential words for professional and academic settings.', 2, 7)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (28, 3, N'Building Better Contextual Understanding', N'Learn to derive word meanings from context in different scenarios.', 2, 8)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (29, 3, N'Idiomatic Expressions for Fluency', N'Explore idiomatic phrases to make your speech more fluent and natural.', 2, 9)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (30, 3, N'Final Vocabulary Test', N'Test your mastery of advanced vocabulary through a comprehensive challenge.', 2, 10)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (31, 4, N'Essential Words for Daily Life', N'Learn the foundational vocabulary needed for everyday communication.', 2, 1)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (32, 4, N'Common Expressions and Idioms', N'Explore frequently used idioms and expressions to sound more natural.', 2, 2)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (33, 4, N'Building Words from Root Forms', N'Understand word roots, prefixes, and suffixes to expand your vocabulary.', 2, 3)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (34, 4, N'Words for Talking About Family', N'Master words and phrases for discussing family relationships and dynamics.', 2, 4)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (35, 4, N'Describing Places and Activities', N'Learn descriptive vocabulary for locations, events, and activities.', 2, 5)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (36, 4, N'Vocabulary for Shopping and Food', N'Gain essential words and phrases for shopping, dining, and cooking.', 2, 6)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (37, 4, N'Talking About Weather and Seasons', N'Explore terms related to weather, climate, and seasonal changes.', 2, 7)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (38, 4, N'Words for Hobbies and Sports', N'Build your vocabulary for discussing hobbies, games, and sports.', 2, 8)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (39, 4, N'Expanding Your Vocabulary with Synonyms', N'Discover synonyms to avoid repetition and enrich your expression.', 2, 9)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (40, 4, N'Vocabulary Game Challenge', N'Reinforce your learning with interactive vocabulary games and challenges.', 2, 10)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (41, 5, N'Introduction to English Sounds', N'Learn the foundational vowel and consonant sounds in English.', 2, 1)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (42, 5, N'Mastering Vowels', N'Practice distinguishing and pronouncing long and short vowel sounds accurately.', 2, 2)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (43, 5, N'Understanding Consonant Clusters', N'Focus on combining consonants smoothly in words like "strength" and "plants."', 2, 3)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (44, 5, N'The Art of Syllable Stress', N'Master syllable emphasis to improve clarity in speech.', 2, 4)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (45, 5, N'Common Mispronunciations to Avoid', N'Identify and correct frequent pronunciation errors for better communication.', 2, 5)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (46, 5, N'The Magic of Intonation', N'Explore the musical quality of English by practicing rising and falling tones.', 2, 6)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (47, 5, N'Linking Words for Fluency', N'Learn to connect words naturally for a smoother speaking flow.', 2, 7)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (48, 5, N'Practice: Everyday Phrases', N'Apply pronunciation skills to common, real-life phrases and expressions.', 2, 8)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (49, 5, N'Real-life Conversations', N'Simulate everyday conversations to practice pronunciation in context.', 2, 9)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (50, 5, N'Final Pronunciation Challenge', N'Test your pronunciation mastery through a fun, practical speaking challenge.', 2, 10)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (51, 6, N'Breaking Down the IPA Chart', N'Decode the International Phonetic Alphabet to understand English sounds better.', 2, 1)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (52, 6, N'Voiced vs. Voiceless Sounds', N'Learn the difference between sounds like "b" and "p" or "z" and "s."', 2, 2)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (53, 6, N'Perfecting Diphthongs', N'Practice gliding vowel sounds like "oi" in "coin" or "ou" in "cloud."', 2, 3)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (54, 6, N'Silent Letters and Their Tricks', N'Discover and overcome the challenges of silent letters in words like "knight" or "hour."', 2, 4)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (55, 6, N'Regional Accents vs. Standard English', N'Understand variations in pronunciation between regions and standard English.', 2, 5)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (56, 6, N'Sounding Natural with Connected Speech', N'Master the art of linking and reducing sounds for natural fluency.', 2, 6)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (57, 6, N'Practicing Minimal Pairs', N'Differentiate between similar-sounding words like "ship" and "sheep."', 2, 7)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (58, 6, N'Word Stress: Changing Meanings', N'Learn how stressing different syllables can change word meanings (e.g., record vs. record).', 2, 8)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (59, 6, N'Listening and Imitation Exercises', N'Sharpen listening skills and mimic native speakers for accurate pronunciation.', 2, 9)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (60, 6, N'Pronunciation Mastery Test', N'Showcase your progress with a final test covering all aspects of pronunciation.', 2, 10)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (61, 7, N'Overview of Grammar Pitfalls', N'Identify and understand the most common grammar issues faced by English learners.', 2, 1)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (62, 7, N'Avoiding Run-on Sentences', N'Learn to structure sentences properly to avoid long, unmanageable sentences.', 2, 2)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (63, 7, N'Tackling Comma Splices', N'Discover how to fix comma-related errors and create clear, concise sentences.', 2, 3)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (64, 7, N'Using Articles Correctly', N'Master the use of "a," "an," and "the" in different contexts.', 2, 4)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (65, 7, N'Pronoun Agreement and Reference', N'Ensure that pronouns agree with their antecedents and are clear in meaning.', 2, 5)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (66, 7, N'Active vs. Passive Voice', N'Understand when to use active voice for clarity and passive voice for emphasis.', 2, 6)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (67, 7, N'Prepositions Made Easy', N'Simplify the correct use of prepositions in phrases and sentences.', 2, 7)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (68, 7, N'Fixing Dangling Modifiers', N'Avoid confusing or misplaced modifiers to improve sentence clarity.', 2, 8)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (69, 7, N'Grammar Error Correction Practice', N'Engage in exercises to identify and correct various grammar errors.', 2, 9)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (70, 8, N'Real-world Grammar Application', N'Apply your grammar knowledge in practical writing and speaking tasks.', 2, 10)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (71, 8, N'Hard-to-Pronounce Words', N'Tackle a list of commonly mispronounced words and learn to say them with confidence.', 2, 1)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (73, 8, N'Pronouncing Long Words with Ease', N'Break down multi-syllable words into manageable parts for accurate pronunciation.', 2, 3)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (74, 8, N'Common Homophones Explained', N'Learn to distinguish and pronounce words that sound similar but have different meanings, such as "there" and "their."', 2, 4)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (75, 8, N'Practicing Stress in Phrases', N'Discover how stress patterns in phrases can change meaning and improve fluency.', 2, 5)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (76, 8, N'Tongue Twisters for Fun Learning', N'Use engaging tongue twisters to sharpen articulation and improve pronunciation speed.', 2, 6)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (77, 8, N'Tricky Consonant Combinations', N'Focus on challenging blends like "str," "bl," and "chr" to enhance clarity.', 2, 7)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (78, 8, N'Accent Reduction Tips', N'Learn techniques to neutralize strong accents and achieve a more standard English pronunciation.', 2, 8)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (79, 8, N'Listening Practice with Native Speakers', N'Practice identifying and mimicking pronunciation from authentic audio clips.', 2, 9)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (80, 9, N'Confidence in Pronunciation', N'Apply all skills in real-life scenarios to speak clearly and confidently.', 2, 10)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (81, 9, N'Learning Words with Games', N'Enhance vocabulary with engaging word games like hangman and word bingo.', 2, 1)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (82, 9, N'Vocabulary for Everyday Actions', N'Learn practical words and phrases for daily tasks and routines.', 2, 2)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (83, 9, N'Spelling Tips and Tricks', N'Master common spelling rules and strategies to avoid errors.', 2, 3)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (84, 9, N'Words That Sound Alike', N'Explore homophones and learn to differentiate them in spelling and meaning.', 2, 4)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (85, 9, N'Building Vocabulary with Crossword Puzzles', N'Solve crossword puzzles to discover new words and reinforce spelling.', 2, 5)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (86, 9, N'Fun with Word Associations', N'Use creative associations to remember word meanings and expand vocabulary.', 2, 6)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (87, 9, N'Practicing Vocabulary in Sentences', N'Create and practice sentences to reinforce new words in context.	', 2, 7)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (88, 9, N'Group Activities: Word Charades', N'Play charades to guess words and strengthen vocabulary recall.', 2, 8)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (89, 9, N'Listening and Spelling Exercises', N'Improve spelling through listening activities and dictation practice.', 2, 9)
GO
INSERT [dbo].[Session] ([session_id], [course_id], [title], [description], [hours], [session_number]) VALUES (90, 9, N'Final Vocabulary Bee', N'Test your vocabulary and spelling skills in a fun, competitive spelling bee challenge.', 2, 10)
GO
SET IDENTITY_INSERT [dbo].[Session] OFF
GO
SET IDENTITY_INSERT [dbo].[Student] ON 
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (1, 1, 4, N'Dang Thuy Vi', 0, 13, CAST(N'2011-03-22T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (2, 2, 8, N'Pham Anh Khoa', 0, 14, CAST(N'2010-05-03T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (3, 3, 9, N'Dang Nguyen Minh Duy', 0, 13, CAST(N'2011-01-14T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (4, 4, 11, N'Le Thu Ngan', 0, 15, CAST(N'2009-02-14T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (5, 5, 14, N'Mai Nhat Nam', 0, 13, CAST(N'2011-04-17T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (6, 6, 18, N'Bui Duc Duy', 0, 12, CAST(N'2011-11-19T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (7, 7, 19, N'Ho Ngoc Khanh', 0, 13, CAST(N'2011-06-24T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (8, 8, 21, N'Bui Anh Khoa', 0, 13, CAST(N'2011-01-30T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (9, 9, 23, N'Huynh Anh Tuan', 1, 14, CAST(N'2010-07-22T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (10, 10, 24, N'Le Thi Trinh', 0, 12, CAST(N'2012-10-11T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (11, 11, 25, N'Pham Nguyen Bao', 1, 11, CAST(N'2013-05-20T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (12, 12, 26, N'Do Le Minh', 0, 14, CAST(N'2010-04-08T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (13, 13, 27, N'Tran Thi Lan', 1, 13, CAST(N'2011-08-17T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (14, 14, 28, N'Nguyen Van Cuong', 2, 15, CAST(N'2009-09-23T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (15, 14, 29, N'Tran Thi Trang', 0, 12, CAST(N'2012-06-14T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (16, 9, 30, N'Nguyen Duc Khanh', 1, 14, CAST(N'2010-02-25T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (17, 12, 31, N'Vu Ngoc Thu', 1, 11, CAST(N'2013-12-06T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (18, 2, 32, N'Tran Tuan Phong', 0, 15, CAST(N'2009-01-30T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (19, 7, 33, N'Nguyen Thi Linh', 2, 13, CAST(N'2011-10-19T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (20, 8, 34, N'Bui Tuan Tu', 1, 12, CAST(N'2012-05-25T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (21, 9, 35, N'Le Thi Thuy', 0, 11, CAST(N'2013-03-08T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (22, 2, 36, N'Pham Van Kien', 1, 14, CAST(N'2010-11-30T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (23, 3, 37, N'Tran Van Duong', 2, 15, CAST(N'2009-08-22T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (24, 4, 38, N'Nguyen Le Hieu', 0, 13, CAST(N'2011-06-12T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (25, 3, 39, N'Tran Lan Hoa', 1, 12, CAST(N'2012-02-18T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (26, 11, 40, N'Phan Huu Thanh', 0, 15, CAST(N'2009-10-05T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (27, 10, 41, N'Nguyen Thi Diep', 1, 11, CAST(N'2013-01-16T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (28, 8, 42, N'Dang Nguyen Trung', 2, 14, CAST(N'2010-04-30T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (29, 5, 43, N'Le Hong Hang', 1, 12, CAST(N'2012-07-19T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (30, 4, 44, N'Tran Bao Toan', 0, 15, CAST(N'2009-12-25T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (31, 7, 45, N'Le Ngoc Trang', 1, 11, CAST(N'2013-11-11T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (32, 8, 46, N'Bui Thi Khanh', 1, 13, CAST(N'2011-04-03T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (33, 6, 47, N'Nguyen Thi Thuy', 2, 12, CAST(N'2012-09-29T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (34, 8, 48, N'Hoang Van Tuan', 1, 14, CAST(N'2010-05-15T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (35, 9, 49, N'Tran Thi Duyen', 0, 11, CAST(N'2013-03-28T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (36, 11, 50, N'Nguyen Van Hieu', 1, 15, CAST(N'2009-08-13T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (37, 6, 51, N'Le Thi Hoa', 0, 13, CAST(N'2011-01-04T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (38, 12, 52, N'Tran Huu Thanh', 1, 12, CAST(N'2012-06-09T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (39, 14, 53, N'Nguyen Ngoc Phuong', 2, 14, CAST(N'2010-10-21T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (40, 9, 54, N'Le Bao Cuong', 0, 11, CAST(N'2013-12-03T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (41, 13, 55, N'Nguyen Thi Linh', 1, 13, CAST(N'2011-11-18T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (42, 7, 56, N'Hoang Ngoc Hieu', 0, 12, CAST(N'2012-02-09T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (43, 5, 57, N'Le Thanh Ngoc', 1, 15, CAST(N'2009-07-20T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (44, 14, 58, N'Vo Thanh Hung', 2, 14, CAST(N'2010-09-05T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (45, 9, 59, N'Pham Van Duc', 1, 11, CAST(N'2013-05-27T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (46, 14, 60, N'Le Ngoc Khoa', 0, 12, CAST(N'2012-10-01T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Student] ([student_id], [parent_id], [account_id], [description], [cur_level], [age], [birthdate], [image]) VALUES (47, 13, 61, N'Pham Thanh Thuy', 0, 13, CAST(N'2011-03-15T00:00:00.000' AS DateTime), NULL)
GO
SET IDENTITY_INSERT [dbo].[Student] OFF
GO
SET IDENTITY_INSERT [dbo].[StudentHomework] ON 
GO
INSERT [dbo].[StudentHomework] ([student_homework_id], [homework_id], [student_progress_id], [homework_result_id], [point], [playtime], [status], [correct_answers]) VALUES (1, 1, 1, 1, 80, CAST(N'00:27:21' AS Time), N'Submitted', 8)
GO
INSERT [dbo].[StudentHomework] ([student_homework_id], [homework_id], [student_progress_id], [homework_result_id], [point], [playtime], [status], [correct_answers]) VALUES (2, 1, 2, 2, 50, CAST(N'00:28:55' AS Time), N'Submitted', 5)
GO
INSERT [dbo].[StudentHomework] ([student_homework_id], [homework_id], [student_progress_id], [homework_result_id], [point], [playtime], [status], [correct_answers]) VALUES (3, 1, 3, 3, 70, CAST(N'00:25:21' AS Time), N'Submitted', 7)
GO
INSERT [dbo].[StudentHomework] ([student_homework_id], [homework_id], [student_progress_id], [homework_result_id], [point], [playtime], [status], [correct_answers]) VALUES (4, 1, 4, 4, 70, CAST(N'00:24:21' AS Time), N'Submitted', 7)
GO
INSERT [dbo].[StudentHomework] ([student_homework_id], [homework_id], [student_progress_id], [homework_result_id], [point], [playtime], [status], [correct_answers]) VALUES (5, 1, 5, 5, 60, CAST(N'00:28:57' AS Time), N'Submitted', 6)
GO
INSERT [dbo].[StudentHomework] ([student_homework_id], [homework_id], [student_progress_id], [homework_result_id], [point], [playtime], [status], [correct_answers]) VALUES (6, 1, 6, 6, 50, CAST(N'00:28:41' AS Time), N'Submitted', 5)
GO
INSERT [dbo].[StudentHomework] ([student_homework_id], [homework_id], [student_progress_id], [homework_result_id], [point], [playtime], [status], [correct_answers]) VALUES (7, 1, 7, 7, 90, CAST(N'00:10:23' AS Time), N'Submitted', 9)
GO
INSERT [dbo].[StudentHomework] ([student_homework_id], [homework_id], [student_progress_id], [homework_result_id], [point], [playtime], [status], [correct_answers]) VALUES (8, 1, 8, 8, 80, CAST(N'00:15:55' AS Time), N'Submitted', 8)
GO
INSERT [dbo].[StudentHomework] ([student_homework_id], [homework_id], [student_progress_id], [homework_result_id], [point], [playtime], [status], [correct_answers]) VALUES (9, 1, 9, 9, 70, CAST(N'00:20:03' AS Time), N'Submitted', 7)
GO
INSERT [dbo].[StudentHomework] ([student_homework_id], [homework_id], [student_progress_id], [homework_result_id], [point], [playtime], [status], [correct_answers]) VALUES (10, 1, 10, 10, 60, CAST(N'00:23:05' AS Time), N'Submitted', 6)
GO
INSERT [dbo].[StudentHomework] ([student_homework_id], [homework_id], [student_progress_id], [homework_result_id], [point], [playtime], [status], [correct_answers]) VALUES (11, 1, 11, 11, 80, CAST(N'00:20:10' AS Time), N'Submitted', 8)
GO
INSERT [dbo].[StudentHomework] ([student_homework_id], [homework_id], [student_progress_id], [homework_result_id], [point], [playtime], [status], [correct_answers]) VALUES (12, 1, 12, 12, 90, CAST(N'00:13:29' AS Time), N'Submitted', 9)
GO
INSERT [dbo].[StudentHomework] ([student_homework_id], [homework_id], [student_progress_id], [homework_result_id], [point], [playtime], [status], [correct_answers]) VALUES (13, 1, 13, 13, 80, CAST(N'00:25:31' AS Time), N'Submitted', 8)
GO
INSERT [dbo].[StudentHomework] ([student_homework_id], [homework_id], [student_progress_id], [homework_result_id], [point], [playtime], [status], [correct_answers]) VALUES (14, 1, 14, 14, 80, CAST(N'00:27:01' AS Time), N'Submitted', 8)
GO
INSERT [dbo].[StudentHomework] ([student_homework_id], [homework_id], [student_progress_id], [homework_result_id], [point], [playtime], [status], [correct_answers]) VALUES (15, 1, 15, 15, 60, CAST(N'00:16:18' AS Time), N'Submitted', 6)
GO
INSERT [dbo].[StudentHomework] ([student_homework_id], [homework_id], [student_progress_id], [homework_result_id], [point], [playtime], [status], [correct_answers]) VALUES (16, 1, 16, 16, 60, CAST(N'00:05:37' AS Time), N'Submitted', 6)
GO
INSERT [dbo].[StudentHomework] ([student_homework_id], [homework_id], [student_progress_id], [homework_result_id], [point], [playtime], [status], [correct_answers]) VALUES (17, 2, 17, 17, 70, CAST(N'00:27:21' AS Time), N'Submitted', 7)
GO
INSERT [dbo].[StudentHomework] ([student_homework_id], [homework_id], [student_progress_id], [homework_result_id], [point], [playtime], [status], [correct_answers]) VALUES (18, 2, 18, 18, 80, CAST(N'00:28:55' AS Time), N'Not Submitted', 8)
GO
INSERT [dbo].[StudentHomework] ([student_homework_id], [homework_id], [student_progress_id], [homework_result_id], [point], [playtime], [status], [correct_answers]) VALUES (19, 2, 19, 19, 80, CAST(N'00:25:21' AS Time), N'Submitted', 8)
GO
INSERT [dbo].[StudentHomework] ([student_homework_id], [homework_id], [student_progress_id], [homework_result_id], [point], [playtime], [status], [correct_answers]) VALUES (20, 2, 20, 20, 90, CAST(N'00:24:21' AS Time), N'Submitted', 9)
GO
INSERT [dbo].[StudentHomework] ([student_homework_id], [homework_id], [student_progress_id], [homework_result_id], [point], [playtime], [status], [correct_answers]) VALUES (21, 2, 21, 21, 70, CAST(N'00:28:57' AS Time), N'Submitted', 7)
GO
INSERT [dbo].[StudentHomework] ([student_homework_id], [homework_id], [student_progress_id], [homework_result_id], [point], [playtime], [status], [correct_answers]) VALUES (22, 2, 22, 22, 60, CAST(N'00:28:41' AS Time), N'Not Submitted', 6)
GO
INSERT [dbo].[StudentHomework] ([student_homework_id], [homework_id], [student_progress_id], [homework_result_id], [point], [playtime], [status], [correct_answers]) VALUES (23, 2, 23, 23, 80, CAST(N'00:10:23' AS Time), N'Submitted', 8)
GO
INSERT [dbo].[StudentHomework] ([student_homework_id], [homework_id], [student_progress_id], [homework_result_id], [point], [playtime], [status], [correct_answers]) VALUES (24, 2, 24, 24, 70, CAST(N'00:15:55' AS Time), N'Not Submitted', 7)
GO
INSERT [dbo].[StudentHomework] ([student_homework_id], [homework_id], [student_progress_id], [homework_result_id], [point], [playtime], [status], [correct_answers]) VALUES (25, 2, 25, 25, 80, CAST(N'00:20:03' AS Time), N'Not Submitted', 8)
GO
INSERT [dbo].[StudentHomework] ([student_homework_id], [homework_id], [student_progress_id], [homework_result_id], [point], [playtime], [status], [correct_answers]) VALUES (26, 2, 26, 26, 70, CAST(N'00:23:05' AS Time), N'Submitted', 7)
GO
INSERT [dbo].[StudentHomework] ([student_homework_id], [homework_id], [student_progress_id], [homework_result_id], [point], [playtime], [status], [correct_answers]) VALUES (27, 2, 27, 27, 80, CAST(N'00:20:10' AS Time), N'Submitted', 8)
GO
INSERT [dbo].[StudentHomework] ([student_homework_id], [homework_id], [student_progress_id], [homework_result_id], [point], [playtime], [status], [correct_answers]) VALUES (28, 2, 28, 28, 90, CAST(N'00:13:29' AS Time), N'Submitted', 9)
GO
INSERT [dbo].[StudentHomework] ([student_homework_id], [homework_id], [student_progress_id], [homework_result_id], [point], [playtime], [status], [correct_answers]) VALUES (29, 2, 29, 29, 60, CAST(N'00:25:31' AS Time), N'Not Submitted', 6)
GO
INSERT [dbo].[StudentHomework] ([student_homework_id], [homework_id], [student_progress_id], [homework_result_id], [point], [playtime], [status], [correct_answers]) VALUES (30, 2, 30, 30, 70, CAST(N'00:27:01' AS Time), N'Not Submitted', 7)
GO
INSERT [dbo].[StudentHomework] ([student_homework_id], [homework_id], [student_progress_id], [homework_result_id], [point], [playtime], [status], [correct_answers]) VALUES (31, 2, 31, 31, 80, CAST(N'00:16:18' AS Time), N'Submitted', 8)
GO
INSERT [dbo].[StudentHomework] ([student_homework_id], [homework_id], [student_progress_id], [homework_result_id], [point], [playtime], [status], [correct_answers]) VALUES (32, 2, 32, 32, 70, CAST(N'00:05:37' AS Time), N'Submitted', 7)
GO
SET IDENTITY_INSERT [dbo].[StudentHomework] OFF
GO
SET IDENTITY_INSERT [dbo].[StudentProgress] ON 
GO
INSERT [dbo].[StudentProgress] ([student_progress_id], [student_id], [class_id], [total_point], [playtime]) VALUES (1, 1, 1, 80, CAST(N'00:27:21' AS Time))
GO
INSERT [dbo].[StudentProgress] ([student_progress_id], [student_id], [class_id], [total_point], [playtime]) VALUES (2, 2, 1, 50, CAST(N'00:28:55' AS Time))
GO
INSERT [dbo].[StudentProgress] ([student_progress_id], [student_id], [class_id], [total_point], [playtime]) VALUES (3, 3, 1, 70, CAST(N'00:25:21' AS Time))
GO
INSERT [dbo].[StudentProgress] ([student_progress_id], [student_id], [class_id], [total_point], [playtime]) VALUES (4, 4, 1, 70, CAST(N'00:24:21' AS Time))
GO
INSERT [dbo].[StudentProgress] ([student_progress_id], [student_id], [class_id], [total_point], [playtime]) VALUES (5, 5, 1, 60, CAST(N'00:28:57' AS Time))
GO
INSERT [dbo].[StudentProgress] ([student_progress_id], [student_id], [class_id], [total_point], [playtime]) VALUES (6, 6, 1, 50, CAST(N'00:28:41' AS Time))
GO
INSERT [dbo].[StudentProgress] ([student_progress_id], [student_id], [class_id], [total_point], [playtime]) VALUES (7, 7, 1, 90, CAST(N'00:10:23' AS Time))
GO
INSERT [dbo].[StudentProgress] ([student_progress_id], [student_id], [class_id], [total_point], [playtime]) VALUES (8, 8, 1, 80, CAST(N'00:15:55' AS Time))
GO
INSERT [dbo].[StudentProgress] ([student_progress_id], [student_id], [class_id], [total_point], [playtime]) VALUES (9, 9, 1, 70, CAST(N'00:20:03' AS Time))
GO
INSERT [dbo].[StudentProgress] ([student_progress_id], [student_id], [class_id], [total_point], [playtime]) VALUES (10, 10, 1, 60, CAST(N'00:23:05' AS Time))
GO
INSERT [dbo].[StudentProgress] ([student_progress_id], [student_id], [class_id], [total_point], [playtime]) VALUES (11, 11, 1, 80, CAST(N'00:20:10' AS Time))
GO
INSERT [dbo].[StudentProgress] ([student_progress_id], [student_id], [class_id], [total_point], [playtime]) VALUES (12, 12, 1, 90, CAST(N'00:13:29' AS Time))
GO
INSERT [dbo].[StudentProgress] ([student_progress_id], [student_id], [class_id], [total_point], [playtime]) VALUES (13, 13, 1, 80, CAST(N'00:25:31' AS Time))
GO
INSERT [dbo].[StudentProgress] ([student_progress_id], [student_id], [class_id], [total_point], [playtime]) VALUES (14, 14, 1, 80, CAST(N'00:27:01' AS Time))
GO
INSERT [dbo].[StudentProgress] ([student_progress_id], [student_id], [class_id], [total_point], [playtime]) VALUES (15, 15, 1, 60, CAST(N'00:16:18' AS Time))
GO
INSERT [dbo].[StudentProgress] ([student_progress_id], [student_id], [class_id], [total_point], [playtime]) VALUES (16, 16, 1, 60, CAST(N'00:05:37' AS Time))
GO
INSERT [dbo].[StudentProgress] ([student_progress_id], [student_id], [class_id], [total_point], [playtime]) VALUES (17, 17, 2, 70, CAST(N'00:27:21' AS Time))
GO
INSERT [dbo].[StudentProgress] ([student_progress_id], [student_id], [class_id], [total_point], [playtime]) VALUES (18, 18, 2, 80, CAST(N'00:28:55' AS Time))
GO
INSERT [dbo].[StudentProgress] ([student_progress_id], [student_id], [class_id], [total_point], [playtime]) VALUES (19, 19, 2, 80, CAST(N'00:25:21' AS Time))
GO
INSERT [dbo].[StudentProgress] ([student_progress_id], [student_id], [class_id], [total_point], [playtime]) VALUES (20, 20, 2, 90, CAST(N'00:24:21' AS Time))
GO
INSERT [dbo].[StudentProgress] ([student_progress_id], [student_id], [class_id], [total_point], [playtime]) VALUES (21, 21, 2, 70, CAST(N'00:28:57' AS Time))
GO
INSERT [dbo].[StudentProgress] ([student_progress_id], [student_id], [class_id], [total_point], [playtime]) VALUES (22, 22, 2, 60, CAST(N'00:28:41' AS Time))
GO
INSERT [dbo].[StudentProgress] ([student_progress_id], [student_id], [class_id], [total_point], [playtime]) VALUES (23, 23, 2, 80, CAST(N'00:10:23' AS Time))
GO
INSERT [dbo].[StudentProgress] ([student_progress_id], [student_id], [class_id], [total_point], [playtime]) VALUES (24, 24, 2, 70, CAST(N'00:15:55' AS Time))
GO
INSERT [dbo].[StudentProgress] ([student_progress_id], [student_id], [class_id], [total_point], [playtime]) VALUES (25, 25, 2, 80, CAST(N'00:20:03' AS Time))
GO
INSERT [dbo].[StudentProgress] ([student_progress_id], [student_id], [class_id], [total_point], [playtime]) VALUES (26, 26, 2, 70, CAST(N'00:23:05' AS Time))
GO
INSERT [dbo].[StudentProgress] ([student_progress_id], [student_id], [class_id], [total_point], [playtime]) VALUES (27, 27, 2, 80, CAST(N'00:20:10' AS Time))
GO
INSERT [dbo].[StudentProgress] ([student_progress_id], [student_id], [class_id], [total_point], [playtime]) VALUES (28, 28, 2, 90, CAST(N'00:13:29' AS Time))
GO
INSERT [dbo].[StudentProgress] ([student_progress_id], [student_id], [class_id], [total_point], [playtime]) VALUES (29, 29, 2, 60, CAST(N'00:25:31' AS Time))
GO
INSERT [dbo].[StudentProgress] ([student_progress_id], [student_id], [class_id], [total_point], [playtime]) VALUES (30, 30, 2, 70, CAST(N'00:27:01' AS Time))
GO
INSERT [dbo].[StudentProgress] ([student_progress_id], [student_id], [class_id], [total_point], [playtime]) VALUES (31, 31, 2, 80, CAST(N'00:16:18' AS Time))
GO
INSERT [dbo].[StudentProgress] ([student_progress_id], [student_id], [class_id], [total_point], [playtime]) VALUES (32, 32, 2, 70, CAST(N'00:05:37' AS Time))
GO
SET IDENTITY_INSERT [dbo].[StudentProgress] OFF
GO
SET IDENTITY_INSERT [dbo].[Teacher] ON 
GO
INSERT [dbo].[Teacher] ([teacher_id], [account_id], [email], [phone], [certificate], [address], [image]) VALUES (1, 2, N'haint@gmail.com', N'0912868225', N'CertificatePlaceholder', N'District', N'ImagePlaceholder')
GO
INSERT [dbo].[Teacher] ([teacher_id], [account_id], [email], [phone], [certificate], [address], [image]) VALUES (2, 6, N'minhlt@gmail.com', N'0938833549', N'CertificatePlaceholder', N'District', N'ImagePlaceholder')
GO
INSERT [dbo].[Teacher] ([teacher_id], [account_id], [email], [phone], [certificate], [address], [image]) VALUES (3, 12, N'thonghh@gmail.com', N'0912868225', N'CertificatePlaceholder', N'District', N'ImagePlaceholder')
GO
INSERT [dbo].[Teacher] ([teacher_id], [account_id], [email], [phone], [certificate], [address], [image]) VALUES (4, 16, N'minhnn@gmail.com', N'0984949957', N'CertificatePlaceholder', N'District', N'ImagePlaceholder')
GO
SET IDENTITY_INSERT [dbo].[Teacher] OFF
GO
SET IDENTITY_INSERT [dbo].[Transaction] ON 
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (1, 3, N'VNPAY1', 11200000, CAST(N'2024-09-20T00:00:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2024-09-20T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (2, 5, N'VNPAY2', 11200000, CAST(N'2024-09-21T00:00:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2024-09-21T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (3, 7, N'VNPAY3', 11200000, CAST(N'2024-09-22T00:00:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2024-09-22T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (4, 10, N'VNPAY4', 11200000, CAST(N'2024-09-23T00:00:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2024-09-23T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (5, 13, N'VNPAY5', 11200000, CAST(N'2024-09-24T00:00:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2024-09-24T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (6, 3, N'VNPAY6', 11200000, CAST(N'2024-09-24T00:00:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2024-09-24T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (7, 3, N'VNPAY7', 11200000, CAST(N'2024-09-24T00:00:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2024-09-24T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (8, 5, N'VNPAY8', 11200000, CAST(N'2024-09-24T00:00:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2024-09-24T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (9, 7, N'VNPAY9', 11200000, CAST(N'2024-09-24T00:00:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2024-09-24T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (10, 7, N'VNPAY10', 11200000, CAST(N'2024-09-24T00:00:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2024-09-24T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (11, 7, N'VNPAY11', 11200000, CAST(N'2024-09-24T00:00:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2024-09-24T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (12, 10, N'VNPAY12', 11200000, CAST(N'2024-09-24T00:00:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2024-09-24T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (13, 13, N'VNPAY13', 11200000, CAST(N'2024-09-24T00:00:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2024-09-24T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (14, 13, N'VNPAY14', 11200000, CAST(N'2024-09-24T00:00:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2024-09-24T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (15, 15, N'VNPAY15', 11200000, CAST(N'2024-09-24T00:00:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2024-09-24T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (16, 15, N'VNPAY16', 11200000, CAST(N'2024-09-24T00:00:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2024-09-24T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (17, 5, N'VNPAY17', 11200000, CAST(N'2024-12-15T00:00:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2024-12-15T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (18, 17, N'VNPAY18', 11200000, CAST(N'2024-12-15T00:00:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2024-12-15T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (19, 20, N'VNPAY19', 11200000, CAST(N'2024-12-15T00:00:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2024-12-15T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (20, 62, N'VNPAY20', 11200000, CAST(N'2024-12-15T00:00:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2024-12-15T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (21, 5, N'VNPAY21', 11200000, CAST(N'2024-12-15T00:00:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2024-12-15T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (22, 7, N'VNPAY22', 11200000, CAST(N'2024-12-15T00:00:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2024-12-15T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (23, 10, N'VNPAY23', 11200000, CAST(N'2024-12-15T00:00:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2024-12-15T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (24, 7, N'VNPAY24', 11200000, CAST(N'2024-12-15T00:00:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2024-12-15T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (25, 64, N'VNPAY25', 11200000, CAST(N'2024-12-15T00:00:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2024-12-15T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (26, 63, N'VNPAY26', 11200000, CAST(N'2024-12-15T00:00:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2024-12-15T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (27, 20, N'VNPAY27', 11200000, CAST(N'2024-12-15T00:00:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2024-12-15T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (28, 13, N'VNPAY28', 11200000, CAST(N'2024-12-15T00:00:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2024-12-15T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (29, 10, N'VNPAY29', 11200000, CAST(N'2024-12-15T00:00:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2024-12-15T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (30, 17, N'VNPAY30', 11200000, CAST(N'2024-12-15T00:00:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2024-12-15T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (31, 20, N'VNPAY31', 11200000, CAST(N'2024-12-15T00:00:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2024-12-15T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (32, 15, N'VNPAY32', 11200000, CAST(N'2024-12-15T00:00:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2024-12-15T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (33, 3, N'VNPAY33', 11200000, CAST(N'2024-10-14T00:00:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2024-10-14T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (34, 5, N'VNPAY34', 11200000, CAST(N'2024-10-14T00:00:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2024-10-14T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (35, 7, N'VNPAY35', 11200000, CAST(N'2024-10-14T00:00:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2024-10-14T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (36, 10, N'VNPAY36', 11200000, CAST(N'2024-10-14T00:00:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2024-10-14T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (37, 13, N'VNPAY37', 11200000, CAST(N'2024-10-14T00:00:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2024-10-14T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (38, 3, N'VNPAY38', 11200000, CAST(N'2024-10-14T00:00:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2024-10-14T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (39, 3, N'VNPAY39', 11200000, CAST(N'2024-10-14T00:00:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2024-10-14T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (40, 5, N'VNPAY40', 11200000, CAST(N'2024-10-14T00:00:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2024-10-14T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (41, 7, N'VNPAY41', 11200000, CAST(N'2024-10-14T00:00:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2024-10-14T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (42, 7, N'VNPAY42', 11200000, CAST(N'2024-10-14T00:00:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2024-10-14T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (43, 7, N'VNPAY43', 11200000, CAST(N'2024-10-14T00:00:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2024-10-14T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (44, 10, N'VNPAY44', 11200000, CAST(N'2024-10-14T00:00:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2024-10-14T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (45, 13, N'VNPAY45', 11200000, CAST(N'2024-10-14T00:00:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2024-10-14T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (46, 13, N'VNPAY46', 11200000, CAST(N'2024-10-14T00:00:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2024-10-14T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (47, 15, N'VNPAY47', 11200000, CAST(N'2024-10-14T00:00:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2024-10-14T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (48, 15, N'VNPAY48', 11200000, CAST(N'2024-10-14T00:00:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2024-10-14T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (49, 3, N'VNPAY49', 11200000, CAST(N'2024-09-20T00:00:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2024-09-20T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (50, 5, N'VNPAY50', 11200000, CAST(N'2024-09-21T00:00:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2024-09-21T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (51, 7, N'VNPAY51', 11200000, CAST(N'2024-09-22T00:00:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2024-09-22T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (52, 10, N'VNPAY52', 11200000, CAST(N'2024-09-23T00:00:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2024-09-23T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (53, 13, N'VNPAY53', 11200000, CAST(N'2024-09-24T00:00:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2024-09-24T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (54, 3, N'VNPAY54', 11200000, CAST(N'2024-09-24T00:00:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2024-09-24T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (55, 3, N'VNPAY55', 11200000, CAST(N'2024-09-24T00:00:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2024-09-24T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (56, 5, N'VNPAY56', 11200000, CAST(N'2024-09-24T00:00:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2024-09-24T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (57, 7, N'VNPAY57', 11200000, CAST(N'2024-09-24T00:00:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2024-09-24T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (58, 7, N'VNPAY58', 11200000, CAST(N'2024-09-24T00:00:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2024-09-24T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (59, 7, N'VNPAY59', 11200000, CAST(N'2024-09-24T00:00:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2024-09-24T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (60, 10, N'VNPAY60', 11200000, CAST(N'2024-09-24T00:00:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2024-09-24T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (61, 13, N'VNPAY61', 11200000, CAST(N'2024-09-24T00:00:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2024-09-24T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (62, 13, N'VNPAY62', 11200000, CAST(N'2024-09-24T00:00:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2024-09-24T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (63, 15, N'VNPAY63', 11200000, CAST(N'2024-09-24T00:00:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2024-09-24T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (64, 15, N'VNPAY64', 11200000, CAST(N'2024-09-24T00:00:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2024-09-24T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (65, 20, N'VNPAY65', 11200000, CAST(N'2024-10-14T00:00:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2024-10-14T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (66, 15, N'VNPAY66', 11200000, CAST(N'2024-09-20T00:00:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2024-09-20T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (67, 20, N'VNPAY67', 11200000, CAST(N'2024-09-21T00:00:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2024-09-21T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (68, 62, N'VNPAY68', 11200000, CAST(N'2024-09-22T00:00:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2024-09-22T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (69, 64, N'VNPAY69', 11200000, CAST(N'2024-09-23T00:00:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2024-09-23T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (70, 15, N'VNPAY70', 11200000, CAST(N'2024-09-24T00:00:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2024-09-24T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (71, 65, N'VNPAY71', 11200000, CAST(N'2024-09-24T00:00:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2024-09-24T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (72, 67, N'VNPAY72', 11200000, CAST(N'2024-09-24T00:00:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2024-09-24T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (73, 62, N'VNPAY73', 11200000, CAST(N'2024-09-24T00:00:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2024-09-24T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (74, 66, N'VNPAY74', 11200000, CAST(N'2024-09-24T00:00:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2024-09-24T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (75, 17, N'VNPAY75', 11200000, CAST(N'2024-09-24T00:00:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2024-09-24T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (76, 13, N'VNPAY76', 11200000, CAST(N'2024-09-24T00:00:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2024-09-24T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (77, 67, N'VNPAY77', 11200000, CAST(N'2024-09-24T00:00:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2024-09-24T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (78, 62, N'VNPAY78', 11200000, CAST(N'2024-09-24T00:00:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2024-09-24T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (79, 67, N'VNPAY79', 11200000, CAST(N'2024-09-24T00:00:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2024-09-24T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (80, 66, N'VNPAY80', 11200000, CAST(N'2024-09-24T00:00:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2024-09-24T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (81, 13, N'VNPAY81', 11200000, CAST(N'2025-01-07T00:30:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2025-01-07T00:30:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (82, 3, N'VNPAY82', 11200000, CAST(N'2025-01-07T00:30:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2025-01-07T00:30:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (83, 3, N'VNPAY83', 11200000, CAST(N'2025-01-07T00:30:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2025-01-07T00:30:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (84, 5, N'VNPAY84', 11200000, CAST(N'2025-01-07T00:30:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2025-01-07T00:30:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (85, 7, N'VNPAY85', 11200000, CAST(N'2025-01-07T00:30:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2025-01-07T00:30:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (86, 7, N'VNPAY86', 11200000, CAST(N'2025-01-07T00:30:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2025-01-07T00:30:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (87, 7, N'VNPAY87', 11200000, CAST(N'2025-01-07T00:30:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2025-01-07T00:30:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (88, 10, N'VNPAY88', 11200000, CAST(N'2025-01-07T00:30:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2025-01-07T00:30:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (89, 13, N'VNPAY89', 11200000, CAST(N'2025-01-07T00:30:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2025-01-07T00:30:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (90, 13, N'VNPAY90', 11200000, CAST(N'2025-01-07T00:30:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2025-01-07T00:30:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (91, 15, N'VNPAY91', 11200000, CAST(N'2025-01-07T00:30:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2025-01-07T00:30:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (92, 15, N'VNPAY92', 11200000, CAST(N'2025-01-07T00:30:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2025-01-07T00:30:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (93, 15, N'VNPAY93', 11200000, CAST(N'2025-01-07T00:30:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2025-01-07T00:30:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Transaction] ([transaction_id], [account_id], [vnpay_id], [transaction_amount], [transaction_date], [transaction_status], [transaction_type], [confirm_date], [description]) VALUES (94, 15, N'VNPAY94', 11200000, CAST(N'2025-01-07T00:30:00.000' AS DateTime), N'Completed', N'Enrollment Payment', CAST(N'2025-01-07T00:30:00.000' AS DateTime), NULL)
GO

SET IDENTITY_INSERT [dbo].[Transaction] OFF
GO
ALTER TABLE [dbo].[Account]  WITH CHECK ADD  CONSTRAINT [FK_Account_Role] FOREIGN KEY([role_id])
REFERENCES [dbo].[Role] ([role_id])
GO
ALTER TABLE [dbo].[Account] CHECK CONSTRAINT [FK_Account_Role]
GO
ALTER TABLE [dbo].[Attendance]  WITH CHECK ADD  CONSTRAINT [FK_Attendance_Schedule] FOREIGN KEY([schedule_id])
REFERENCES [dbo].[Schedule] ([schedule_id])
GO
ALTER TABLE [dbo].[Attendance] CHECK CONSTRAINT [FK_Attendance_Schedule]
GO
ALTER TABLE [dbo].[Attendance]  WITH CHECK ADD  CONSTRAINT [FK_Attendance_Student] FOREIGN KEY([student_id])
REFERENCES [dbo].[Student] ([student_id])
GO
ALTER TABLE [dbo].[Attendance] CHECK CONSTRAINT [FK_Attendance_Student]
GO
ALTER TABLE [dbo].[Class]  WITH CHECK ADD  CONSTRAINT [FK_Class_Course1] FOREIGN KEY([course_id])
REFERENCES [dbo].[Course] ([course_id])
GO
ALTER TABLE [dbo].[Class] CHECK CONSTRAINT [FK_Class_Course1]
GO
ALTER TABLE [dbo].[Class]  WITH CHECK ADD  CONSTRAINT [FK_Class_Teacher] FOREIGN KEY([teacher_id])
REFERENCES [dbo].[Teacher] ([teacher_id])
GO
ALTER TABLE [dbo].[Class] CHECK CONSTRAINT [FK_Class_Teacher]
GO
ALTER TABLE [dbo].[Enroll]  WITH CHECK ADD  CONSTRAINT [FK_Enroll_Class] FOREIGN KEY([class_id])
REFERENCES [dbo].[Class] ([class_id])
GO
ALTER TABLE [dbo].[Enroll] CHECK CONSTRAINT [FK_Enroll_Class]
GO
ALTER TABLE [dbo].[Enroll]  WITH CHECK ADD  CONSTRAINT [FK_Enroll_Student] FOREIGN KEY([student_id])
REFERENCES [dbo].[Student] ([student_id])
GO
ALTER TABLE [dbo].[Enroll] CHECK CONSTRAINT [FK_Enroll_Student]
GO
ALTER TABLE [dbo].[Enroll]  WITH CHECK ADD  CONSTRAINT [FK_Enroll_Transaction] FOREIGN KEY([transaction_id])
REFERENCES [dbo].[Transaction] ([transaction_id])
GO
ALTER TABLE [dbo].[Enroll] CHECK CONSTRAINT [FK_Enroll_Transaction]
GO
ALTER TABLE [dbo].[Game]  WITH CHECK ADD  CONSTRAINT [FK_Game_GameConfig] FOREIGN KEY([game_config_id])
REFERENCES [dbo].[GameConfig] ([game_config_id])
GO
ALTER TABLE [dbo].[Game] CHECK CONSTRAINT [FK_Game_GameConfig]
GO
ALTER TABLE [dbo].[GameLevel]  WITH CHECK ADD  CONSTRAINT [FK_GameLevel_Game] FOREIGN KEY([game_id])
REFERENCES [dbo].[Game] ([game_id])
GO
ALTER TABLE [dbo].[GameLevel] CHECK CONSTRAINT [FK_GameLevel_Game]
GO
ALTER TABLE [dbo].[Homework]  WITH CHECK ADD  CONSTRAINT [FK_Homework_GameConfig] FOREIGN KEY([game_config_id])
REFERENCES [dbo].[GameConfig] ([game_config_id])
GO
ALTER TABLE [dbo].[Homework] CHECK CONSTRAINT [FK_Homework_GameConfig]
GO
ALTER TABLE [dbo].[Homework]  WITH CHECK ADD  CONSTRAINT [FK_Homework_Session] FOREIGN KEY([session_id])
REFERENCES [dbo].[Session] ([session_id])
GO
ALTER TABLE [dbo].[Homework] CHECK CONSTRAINT [FK_Homework_Session]
GO
ALTER TABLE [dbo].[HomeworkAnswer]  WITH CHECK ADD  CONSTRAINT [FK_HomeworkAnswer_HomeworkQuestion] FOREIGN KEY([homework_question_id])
REFERENCES [dbo].[HomeworkQuestion] ([homework_question_id])
GO
ALTER TABLE [dbo].[HomeworkAnswer] CHECK CONSTRAINT [FK_HomeworkAnswer_HomeworkQuestion]
GO
ALTER TABLE [dbo].[HomeworkQuestion]  WITH CHECK ADD  CONSTRAINT [FK_HomeworkQuestion_Homework] FOREIGN KEY([homework_id])
REFERENCES [dbo].[Homework] ([homework_id])
GO
ALTER TABLE [dbo].[HomeworkQuestion] CHECK CONSTRAINT [FK_HomeworkQuestion_Homework]
GO
ALTER TABLE [dbo].[Parent]  WITH CHECK ADD  CONSTRAINT [FK_Parents_Accounts] FOREIGN KEY([account_id])
REFERENCES [dbo].[Account] ([account_id])
GO
ALTER TABLE [dbo].[Parent] CHECK CONSTRAINT [FK_Parents_Accounts]
GO
ALTER TABLE [dbo].[Schedule]  WITH CHECK ADD  CONSTRAINT [FK_Schedule_Class] FOREIGN KEY([class_id])
REFERENCES [dbo].[Class] ([class_id])
GO
ALTER TABLE [dbo].[Schedule] CHECK CONSTRAINT [FK_Schedule_Class]
GO
ALTER TABLE [dbo].[Schedule]  WITH CHECK ADD  CONSTRAINT [FK_Schedule_Session] FOREIGN KEY([session_id])
REFERENCES [dbo].[Session] ([session_id])
GO
ALTER TABLE [dbo].[Schedule] CHECK CONSTRAINT [FK_Schedule_Session]
GO
ALTER TABLE [dbo].[Session]  WITH CHECK ADD  CONSTRAINT [FK_Session_Course] FOREIGN KEY([course_id])
REFERENCES [dbo].[Course] ([course_id])
GO
ALTER TABLE [dbo].[Session] CHECK CONSTRAINT [FK_Session_Course]
GO
ALTER TABLE [dbo].[Student]  WITH CHECK ADD  CONSTRAINT [FK_Student_Account] FOREIGN KEY([account_id])
REFERENCES [dbo].[Account] ([account_id])
GO
ALTER TABLE [dbo].[Student] CHECK CONSTRAINT [FK_Student_Account]
GO
ALTER TABLE [dbo].[Student]  WITH CHECK ADD  CONSTRAINT [FK_Student_Parent] FOREIGN KEY([parent_id])
REFERENCES [dbo].[Parent] ([parent_id])
GO
ALTER TABLE [dbo].[Student] CHECK CONSTRAINT [FK_Student_Parent]
GO
ALTER TABLE [dbo].[StudentAnswer]  WITH CHECK ADD  CONSTRAINT [FK_StudentAnswer_Game] FOREIGN KEY([game_id])
REFERENCES [dbo].[Game] ([game_id])
GO
ALTER TABLE [dbo].[StudentAnswer] CHECK CONSTRAINT [FK_StudentAnswer_Game]
GO
ALTER TABLE [dbo].[StudentAnswer]  WITH CHECK ADD  CONSTRAINT [FK_StudentAnswer_StudentHomework] FOREIGN KEY([student_homework_id])
REFERENCES [dbo].[StudentHomework] ([student_homework_id])
GO
ALTER TABLE [dbo].[StudentAnswer] CHECK CONSTRAINT [FK_StudentAnswer_StudentHomework]
GO
ALTER TABLE [dbo].[StudentHomework]  WITH CHECK ADD  CONSTRAINT [FK_StudentHomework_Homework] FOREIGN KEY([homework_id])
REFERENCES [dbo].[Homework] ([homework_id])
GO
ALTER TABLE [dbo].[StudentHomework] CHECK CONSTRAINT [FK_StudentHomework_Homework]
GO
ALTER TABLE [dbo].[StudentHomework]  WITH CHECK ADD  CONSTRAINT [FK_StudentHomework_HomeworkResult] FOREIGN KEY([homework_result_id])
REFERENCES [dbo].[HomeworkResult] ([homework_result_id])
GO
ALTER TABLE [dbo].[StudentHomework] CHECK CONSTRAINT [FK_StudentHomework_HomeworkResult]
GO
ALTER TABLE [dbo].[StudentHomework]  WITH CHECK ADD  CONSTRAINT [FK_StudentHomework_StudentProgress] FOREIGN KEY([student_progress_id])
REFERENCES [dbo].[StudentProgress] ([student_progress_id])
GO
ALTER TABLE [dbo].[StudentHomework] CHECK CONSTRAINT [FK_StudentHomework_StudentProgress]
GO
ALTER TABLE [dbo].[StudentProgress]  WITH CHECK ADD  CONSTRAINT [FK_StudentProgress_Class] FOREIGN KEY([class_id])
REFERENCES [dbo].[Class] ([class_id])
GO
ALTER TABLE [dbo].[StudentProgress] CHECK CONSTRAINT [FK_StudentProgress_Class]
GO
ALTER TABLE [dbo].[StudentProgress]  WITH CHECK ADD  CONSTRAINT [FK_StudentProgress_Student] FOREIGN KEY([student_id])
REFERENCES [dbo].[Student] ([student_id])
GO
ALTER TABLE [dbo].[StudentProgress] CHECK CONSTRAINT [FK_StudentProgress_Student]
GO
ALTER TABLE [dbo].[Teacher]  WITH CHECK ADD  CONSTRAINT [FK_Teacher_Account] FOREIGN KEY([account_id])
REFERENCES [dbo].[Account] ([account_id])
GO
ALTER TABLE [dbo].[Teacher] CHECK CONSTRAINT [FK_Teacher_Account]
GO
ALTER TABLE [dbo].[Transaction]  WITH CHECK ADD  CONSTRAINT [FK_Transaction_Account] FOREIGN KEY([account_id])
REFERENCES [dbo].[Account] ([account_id])
GO
ALTER TABLE [dbo].[Transaction] CHECK CONSTRAINT [FK_Transaction_Account]
GO
USE [master]
GO
ALTER DATABASE [ChildrenEnglishGame] SET  READ_WRITE 
GO
