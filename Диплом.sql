USE [master]
GO
/****** Object:  Database [Пример3]    Script Date: 30.05.2024 16:40:06 ******/
CREATE DATABASE [Пример3]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Пример3', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\Пример3.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Пример3_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\Пример3_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Пример3] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Пример3].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Пример3] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Пример3] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Пример3] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Пример3] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Пример3] SET ARITHABORT OFF 
GO
ALTER DATABASE [Пример3] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [Пример3] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Пример3] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Пример3] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Пример3] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Пример3] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Пример3] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Пример3] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Пример3] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Пример3] SET  ENABLE_BROKER 
GO
ALTER DATABASE [Пример3] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Пример3] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Пример3] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Пример3] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Пример3] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Пример3] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Пример3] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Пример3] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Пример3] SET  MULTI_USER 
GO
ALTER DATABASE [Пример3] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Пример3] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Пример3] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Пример3] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Пример3] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Пример3] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [Пример3] SET QUERY_STORE = OFF
GO
USE [Пример3]
GO
/****** Object:  Table [dbo].[Вид_оборудования]    Script Date: 30.05.2024 16:40:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Вид_оборудования](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Вид] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Заказы]    Script Date: 30.05.2024 16:40:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Заказы](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ID_заявки] [int] NULL,
	[Количество_деталей] [int] NOT NULL,
	[Статус_заказа] [varchar](50) NULL,
	[Дата_заказа] [date] NOT NULL,
 CONSTRAINT [PK__Заказы__3214EC27C17C4F93] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Заявка]    Script Date: 30.05.2024 16:40:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Заявка](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Дата_начала] [date] NOT NULL,
	[Дата_окончания] [date] NULL,
	[ID_оборудования] [int] NOT NULL,
	[Описание_проблемы] [varchar](60) NOT NULL,
	[ID_клиента] [int] NOT NULL,
	[ID_исполнителя] [int] NULL,
	[ID_статус_заявки] [int] NULL,
	[ID_приоритет] [int] NULL,
 CONSTRAINT [PK__Заявка__3214EC276D3F6404] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Исполнители]    Script Date: 30.05.2024 16:40:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Исполнители](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Фамилия] [varchar](60) NOT NULL,
	[Имя] [varchar](60) NOT NULL,
	[Отчество] [varchar](60) NOT NULL,
	[Телефон] [varchar](11) NOT NULL,
	[Почта] [varchar](50) NOT NULL,
	[ID_пользователя] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Материалы]    Script Date: 30.05.2024 16:40:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Материалы](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Деталь] [varchar](50) NOT NULL,
	[Описание] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Оборудование]    Script Date: 30.05.2024 16:40:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Оборудование](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Индивидуальный_номер] [varchar](20) NOT NULL,
	[ID_вид_оборудования] [int] NOT NULL,
	[Описание] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Отчет]    Script Date: 30.05.2024 16:40:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Отчет](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ID_заявки] [int] NOT NULL,
	[ID_клиента] [int] NOT NULL,
	[ID_исполнителя] [int] NULL,
	[Дата_начала] [date] NOT NULL,
	[Дата_окончания] [date] NULL,
	[Результат_работы] [varchar](max) NULL,
 CONSTRAINT [PK__Отчет__3214EC2756E4DF1B] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Позиции_заказа]    Script Date: 30.05.2024 16:40:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Позиции_заказа](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ID_заказа] [int] NULL,
	[Количество] [int] NULL,
	[ID_материала] [int] NULL,
 CONSTRAINT [PK__Позиции___3214EC274B781E95] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Пользователи]    Script Date: 30.05.2024 16:40:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Пользователи](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Логин] [varchar](50) NOT NULL,
	[Пароль] [varchar](50) NOT NULL,
	[ID_роли] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Приоритет_заявки]    Script Date: 30.05.2024 16:40:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Приоритет_заявки](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Приоритет] [varchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Работники]    Script Date: 30.05.2024 16:40:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Работники](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Фамилия] [varchar](60) NOT NULL,
	[Имя] [varchar](60) NOT NULL,
	[Отчество] [varchar](60) NOT NULL,
	[Телефон] [varchar](11) NOT NULL,
	[Почта] [varchar](50) NOT NULL,
	[ID_пользователя] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Роли]    Script Date: 30.05.2024 16:40:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Роли](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Роль] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Статус_заявки]    Script Date: 30.05.2024 16:40:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Статус_заявки](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Статус] [varchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Вид_оборудования] ON 

INSERT [dbo].[Вид_оборудования] ([ID], [Вид]) VALUES (1, N'Компьютер')
INSERT [dbo].[Вид_оборудования] ([ID], [Вид]) VALUES (2, N'Ноутбук')
INSERT [dbo].[Вид_оборудования] ([ID], [Вид]) VALUES (3, N'Принтер')
INSERT [dbo].[Вид_оборудования] ([ID], [Вид]) VALUES (4, N'Сканер')
INSERT [dbo].[Вид_оборудования] ([ID], [Вид]) VALUES (5, N'Монитор')
SET IDENTITY_INSERT [dbo].[Вид_оборудования] OFF
GO
SET IDENTITY_INSERT [dbo].[Заказы] ON 

INSERT [dbo].[Заказы] ([ID], [ID_заявки], [Количество_деталей], [Статус_заказа], [Дата_заказа]) VALUES (1, 1, 2, N'Отклонен', CAST(N'2024-04-24' AS Date))
INSERT [dbo].[Заказы] ([ID], [ID_заявки], [Количество_деталей], [Статус_заказа], [Дата_заказа]) VALUES (13, 1, 1, N'Одобрен', CAST(N'2024-05-02' AS Date))
INSERT [dbo].[Заказы] ([ID], [ID_заявки], [Количество_деталей], [Статус_заказа], [Дата_заказа]) VALUES (14, 1, 1, N'Отклонен', CAST(N'2024-05-02' AS Date))
INSERT [dbo].[Заказы] ([ID], [ID_заявки], [Количество_деталей], [Статус_заказа], [Дата_заказа]) VALUES (15, 1, 1, N'Отклонен', CAST(N'2024-05-02' AS Date))
INSERT [dbo].[Заказы] ([ID], [ID_заявки], [Количество_деталей], [Статус_заказа], [Дата_заказа]) VALUES (23, 2, 3, N'На рассмотрении', CAST(N'2024-05-04' AS Date))
INSERT [dbo].[Заказы] ([ID], [ID_заявки], [Количество_деталей], [Статус_заказа], [Дата_заказа]) VALUES (33, 1, 1, N'На рассмотрении', CAST(N'2024-05-08' AS Date))
INSERT [dbo].[Заказы] ([ID], [ID_заявки], [Количество_деталей], [Статус_заказа], [Дата_заказа]) VALUES (36, 2, 2, N'На рассмотрении', CAST(N'2024-05-16' AS Date))
INSERT [dbo].[Заказы] ([ID], [ID_заявки], [Количество_деталей], [Статус_заказа], [Дата_заказа]) VALUES (54, 1, 0, NULL, CAST(N'2024-05-23' AS Date))
SET IDENTITY_INSERT [dbo].[Заказы] OFF
GO
SET IDENTITY_INSERT [dbo].[Заявка] ON 

INSERT [dbo].[Заявка] ([ID], [Дата_начала], [Дата_окончания], [ID_оборудования], [Описание_проблемы], [ID_клиента], [ID_исполнителя], [ID_статус_заявки], [ID_приоритет]) VALUES (1, CAST(N'2024-04-24' AS Date), CAST(N'2024-04-24' AS Date), 1, N'Греется', 2, 1, 1, 1)
INSERT [dbo].[Заявка] ([ID], [Дата_начала], [Дата_окончания], [ID_оборудования], [Описание_проблемы], [ID_клиента], [ID_исполнителя], [ID_статус_заявки], [ID_приоритет]) VALUES (2, CAST(N'2024-04-24' AS Date), CAST(N'2024-04-25' AS Date), 2, N'Тормозит', 1, 2, 1, 1)
SET IDENTITY_INSERT [dbo].[Заявка] OFF
GO
SET IDENTITY_INSERT [dbo].[Исполнители] ON 

INSERT [dbo].[Исполнители] ([ID], [Фамилия], [Имя], [Отчество], [Телефон], [Почта], [ID_пользователя]) VALUES (1, N'Жуков', N'Илья', N'Юрьевич', N'3453', N'dfgdfg', 3)
INSERT [dbo].[Исполнители] ([ID], [Фамилия], [Имя], [Отчество], [Телефон], [Почта], [ID_пользователя]) VALUES (2, N'Цыганков', N'Алексей', N'Александрович', N'5645', N'dffgdfg', 4)
INSERT [dbo].[Исполнители] ([ID], [Фамилия], [Имя], [Отчество], [Телефон], [Почта], [ID_пользователя]) VALUES (3, N'Голданова', N'Любовь', N'Владимировна', N'5756', N'ghyiuo', 5)
INSERT [dbo].[Исполнители] ([ID], [Фамилия], [Имя], [Отчество], [Телефон], [Почта], [ID_пользователя]) VALUES (4, N'Кондауров', N'Павел', N'Олегович', N'7784', N'klhjghj', 6)
SET IDENTITY_INSERT [dbo].[Исполнители] OFF
GO
SET IDENTITY_INSERT [dbo].[Материалы] ON 

INSERT [dbo].[Материалы] ([ID], [Деталь], [Описание]) VALUES (1, N'Куллер', N'Тест')
INSERT [dbo].[Материалы] ([ID], [Деталь], [Описание]) VALUES (2, N'Материнская плата', N'Тест')
INSERT [dbo].[Материалы] ([ID], [Деталь], [Описание]) VALUES (3, N'Процессор', N'Тест')
INSERT [dbo].[Материалы] ([ID], [Деталь], [Описание]) VALUES (4, N'Жесткий диск', N'Тест')
INSERT [dbo].[Материалы] ([ID], [Деталь], [Описание]) VALUES (5, N'ОЗУ', N'Тест')
SET IDENTITY_INSERT [dbo].[Материалы] OFF
GO
SET IDENTITY_INSERT [dbo].[Оборудование] ON 

INSERT [dbo].[Оборудование] ([ID], [Индивидуальный_номер], [ID_вид_оборудования], [Описание]) VALUES (1, N'123456', 1, N'Тест')
INSERT [dbo].[Оборудование] ([ID], [Индивидуальный_номер], [ID_вид_оборудования], [Описание]) VALUES (2, N'234567', 2, NULL)
INSERT [dbo].[Оборудование] ([ID], [Индивидуальный_номер], [ID_вид_оборудования], [Описание]) VALUES (3, N'456789', 3, NULL)
INSERT [dbo].[Оборудование] ([ID], [Индивидуальный_номер], [ID_вид_оборудования], [Описание]) VALUES (4, N'567890', 4, NULL)
SET IDENTITY_INSERT [dbo].[Оборудование] OFF
GO
SET IDENTITY_INSERT [dbo].[Отчет] ON 

INSERT [dbo].[Отчет] ([ID], [ID_заявки], [ID_клиента], [ID_исполнителя], [Дата_начала], [Дата_окончания], [Результат_работы]) VALUES (1, 1, 2, 1, CAST(N'2024-04-24' AS Date), CAST(N'2024-04-24' AS Date), N'Замена куллера')
INSERT [dbo].[Отчет] ([ID], [ID_заявки], [ID_клиента], [ID_исполнителя], [Дата_начала], [Дата_окончания], [Результат_работы]) VALUES (2, 2, 1, 1, CAST(N'2024-04-29' AS Date), CAST(N'2024-04-30' AS Date), N'Замена процессора')
SET IDENTITY_INSERT [dbo].[Отчет] OFF
GO
SET IDENTITY_INSERT [dbo].[Позиции_заказа] ON 

INSERT [dbo].[Позиции_заказа] ([ID], [ID_заказа], [Количество], [ID_материала]) VALUES (2, 1, 2, 1)
INSERT [dbo].[Позиции_заказа] ([ID], [ID_заказа], [Количество], [ID_материала]) VALUES (19, 13, 1, 4)
INSERT [dbo].[Позиции_заказа] ([ID], [ID_заказа], [Количество], [ID_материала]) VALUES (20, 14, 2, 3)
INSERT [dbo].[Позиции_заказа] ([ID], [ID_заказа], [Количество], [ID_материала]) VALUES (21, 15, 1, 2)
INSERT [dbo].[Позиции_заказа] ([ID], [ID_заказа], [Количество], [ID_материала]) VALUES (22, 23, 1, 4)
INSERT [dbo].[Позиции_заказа] ([ID], [ID_заказа], [Количество], [ID_материала]) VALUES (23, 23, 1, 3)
INSERT [dbo].[Позиции_заказа] ([ID], [ID_заказа], [Количество], [ID_материала]) VALUES (24, 23, 1, 2)
INSERT [dbo].[Позиции_заказа] ([ID], [ID_заказа], [Количество], [ID_материала]) VALUES (27, 33, 1, 2)
INSERT [dbo].[Позиции_заказа] ([ID], [ID_заказа], [Количество], [ID_материала]) VALUES (28, 36, 7, 4)
INSERT [dbo].[Позиции_заказа] ([ID], [ID_заказа], [Количество], [ID_материала]) VALUES (29, 36, 1, 5)
SET IDENTITY_INSERT [dbo].[Позиции_заказа] OFF
GO
SET IDENTITY_INSERT [dbo].[Пользователи] ON 

INSERT [dbo].[Пользователи] ([ID], [Логин], [Пароль], [ID_роли]) VALUES (1, N'Klim', N'123', 1)
INSERT [dbo].[Пользователи] ([ID], [Логин], [Пароль], [ID_роли]) VALUES (2, N'Klim2', N'123', 1)
INSERT [dbo].[Пользователи] ([ID], [Логин], [Пароль], [ID_роли]) VALUES (3, N'Tex', N'234', 2)
INSERT [dbo].[Пользователи] ([ID], [Логин], [Пароль], [ID_роли]) VALUES (4, N'Tex2', N'234', 2)
INSERT [dbo].[Пользователи] ([ID], [Логин], [Пароль], [ID_роли]) VALUES (5, N'Men', N'345', 3)
INSERT [dbo].[Пользователи] ([ID], [Логин], [Пароль], [ID_роли]) VALUES (6, N'Adi', N'456', 4)
INSERT [dbo].[Пользователи] ([ID], [Логин], [Пароль], [ID_роли]) VALUES (7, N'апа', N'ап', 2)
SET IDENTITY_INSERT [dbo].[Пользователи] OFF
GO
SET IDENTITY_INSERT [dbo].[Приоритет_заявки] ON 

INSERT [dbo].[Приоритет_заявки] ([ID], [Приоритет]) VALUES (1, N'Важно')
INSERT [dbo].[Приоритет_заявки] ([ID], [Приоритет]) VALUES (2, N'Не важно')
INSERT [dbo].[Приоритет_заявки] ([ID], [Приоритет]) VALUES (3, N'Не очень важно')
SET IDENTITY_INSERT [dbo].[Приоритет_заявки] OFF
GO
SET IDENTITY_INSERT [dbo].[Работники] ON 

INSERT [dbo].[Работники] ([ID], [Фамилия], [Имя], [Отчество], [Телефон], [Почта], [ID_пользователя]) VALUES (1, N'Иванова', N'Татьяна', N'Юрьевна', N'34534', N'dffgdg', 1)
INSERT [dbo].[Работники] ([ID], [Фамилия], [Имя], [Отчество], [Телефон], [Почта], [ID_пользователя]) VALUES (2, N'Ростенко', N'Елизавета', N'Павловна', N'63534', N'dfgdg', 2)
INSERT [dbo].[Работники] ([ID], [Фамилия], [Имя], [Отчество], [Телефон], [Почта], [ID_пользователя]) VALUES (3, N'паа', N'апап', N'ап', N'45', N'ап', 7)
SET IDENTITY_INSERT [dbo].[Работники] OFF
GO
SET IDENTITY_INSERT [dbo].[Роли] ON 

INSERT [dbo].[Роли] ([ID], [Роль]) VALUES (1, N'Клиент')
INSERT [dbo].[Роли] ([ID], [Роль]) VALUES (2, N'Техник')
INSERT [dbo].[Роли] ([ID], [Роль]) VALUES (3, N'Менеджер')
INSERT [dbo].[Роли] ([ID], [Роль]) VALUES (4, N'Администратор')
SET IDENTITY_INSERT [dbo].[Роли] OFF
GO
SET IDENTITY_INSERT [dbo].[Статус_заявки] ON 

INSERT [dbo].[Статус_заявки] ([ID], [Статус]) VALUES (1, N'Готово')
INSERT [dbo].[Статус_заявки] ([ID], [Статус]) VALUES (2, N'В работе')
INSERT [dbo].[Статус_заявки] ([ID], [Статус]) VALUES (3, N'На расмотрении')
INSERT [dbo].[Статус_заявки] ([ID], [Статус]) VALUES (4, N'Отклонено')
SET IDENTITY_INSERT [dbo].[Статус_заявки] OFF
GO
ALTER TABLE [dbo].[Заказы]  WITH CHECK ADD  CONSTRAINT [FK_Заказы_Заявка] FOREIGN KEY([ID_заявки])
REFERENCES [dbo].[Заявка] ([ID])
GO
ALTER TABLE [dbo].[Заказы] CHECK CONSTRAINT [FK_Заказы_Заявка]
GO
ALTER TABLE [dbo].[Заявка]  WITH CHECK ADD  CONSTRAINT [FK_Заявка_Исполнители] FOREIGN KEY([ID_исполнителя])
REFERENCES [dbo].[Исполнители] ([ID])
GO
ALTER TABLE [dbo].[Заявка] CHECK CONSTRAINT [FK_Заявка_Исполнители]
GO
ALTER TABLE [dbo].[Заявка]  WITH CHECK ADD  CONSTRAINT [FK_Заявка_Оборудование] FOREIGN KEY([ID_оборудования])
REFERENCES [dbo].[Оборудование] ([ID])
GO
ALTER TABLE [dbo].[Заявка] CHECK CONSTRAINT [FK_Заявка_Оборудование]
GO
ALTER TABLE [dbo].[Заявка]  WITH CHECK ADD  CONSTRAINT [FK_Заявка_Приоритет_заявки] FOREIGN KEY([ID_приоритет])
REFERENCES [dbo].[Приоритет_заявки] ([ID])
GO
ALTER TABLE [dbo].[Заявка] CHECK CONSTRAINT [FK_Заявка_Приоритет_заявки]
GO
ALTER TABLE [dbo].[Заявка]  WITH CHECK ADD  CONSTRAINT [FK_Заявка_Работники] FOREIGN KEY([ID_клиента])
REFERENCES [dbo].[Работники] ([ID])
GO
ALTER TABLE [dbo].[Заявка] CHECK CONSTRAINT [FK_Заявка_Работники]
GO
ALTER TABLE [dbo].[Заявка]  WITH CHECK ADD  CONSTRAINT [FK_Заявка_Статус_заявки] FOREIGN KEY([ID_статус_заявки])
REFERENCES [dbo].[Статус_заявки] ([ID])
GO
ALTER TABLE [dbo].[Заявка] CHECK CONSTRAINT [FK_Заявка_Статус_заявки]
GO
ALTER TABLE [dbo].[Исполнители]  WITH CHECK ADD  CONSTRAINT [FK_Исполнители_Пользователи] FOREIGN KEY([ID_пользователя])
REFERENCES [dbo].[Пользователи] ([ID])
GO
ALTER TABLE [dbo].[Исполнители] CHECK CONSTRAINT [FK_Исполнители_Пользователи]
GO
ALTER TABLE [dbo].[Оборудование]  WITH CHECK ADD  CONSTRAINT [FK_Оборудование_Вид_оборудования] FOREIGN KEY([ID_вид_оборудования])
REFERENCES [dbo].[Вид_оборудования] ([ID])
GO
ALTER TABLE [dbo].[Оборудование] CHECK CONSTRAINT [FK_Оборудование_Вид_оборудования]
GO
ALTER TABLE [dbo].[Отчет]  WITH CHECK ADD  CONSTRAINT [FK_Отчет_Заявка] FOREIGN KEY([ID_заявки])
REFERENCES [dbo].[Заявка] ([ID])
GO
ALTER TABLE [dbo].[Отчет] CHECK CONSTRAINT [FK_Отчет_Заявка]
GO
ALTER TABLE [dbo].[Позиции_заказа]  WITH CHECK ADD  CONSTRAINT [FK_Позиции_заказа_Заказы] FOREIGN KEY([ID_заказа])
REFERENCES [dbo].[Заказы] ([ID])
GO
ALTER TABLE [dbo].[Позиции_заказа] CHECK CONSTRAINT [FK_Позиции_заказа_Заказы]
GO
ALTER TABLE [dbo].[Позиции_заказа]  WITH CHECK ADD  CONSTRAINT [FK_Позиции_заказа_Материалы] FOREIGN KEY([ID_материала])
REFERENCES [dbo].[Материалы] ([ID])
GO
ALTER TABLE [dbo].[Позиции_заказа] CHECK CONSTRAINT [FK_Позиции_заказа_Материалы]
GO
ALTER TABLE [dbo].[Пользователи]  WITH CHECK ADD  CONSTRAINT [FK_Пользователи_Роли] FOREIGN KEY([ID_роли])
REFERENCES [dbo].[Роли] ([ID])
GO
ALTER TABLE [dbo].[Пользователи] CHECK CONSTRAINT [FK_Пользователи_Роли]
GO
ALTER TABLE [dbo].[Работники]  WITH CHECK ADD  CONSTRAINT [FK_Работники_Пользователи] FOREIGN KEY([ID_пользователя])
REFERENCES [dbo].[Пользователи] ([ID])
GO
ALTER TABLE [dbo].[Работники] CHECK CONSTRAINT [FK_Работники_Пользователи]
GO
USE [master]
GO
ALTER DATABASE [Пример3] SET  READ_WRITE 
GO
