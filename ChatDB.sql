CREATE DATABASE Chat
GO

USE Chat
GO

CREATE TABLE Users(
	Id int NOT NULL IDENTITY PRIMARY KEY,
	Username nvarchar(100) NOT NULL,
	Pass varchar(40) NOT NULL,
	PictureUrl varchar(256) NOT NULL,
	LastActivity date,
	UserDetails text,
	SessionKey nvarchar(40)
);
GO

CREATE TABLE SendFiles(
	Id int NOT NULL IDENTITY PRIMARY KEY,
	SenderId int NOT NULL FOREIGN KEY REFERENCES Users(Id),
	ReceiverId int NOT NULL FOREIGN KEY REFERENCES Users(Id),
	Content text NOT NULL,
	MessageStatus int NOT NULL,
);
GO

CREATE TABLE Chats(
	Id int NOT NULL IDENTITY PRIMARY KEY,
	FisrtUserId int NOT NULL FOREIGN KEY REFERENCES Users(Id),
	SecondUserId int NOT NULL FOREIGN KEY REFERENCES Users(Id),
	ConnectionString nvarchar(500) NOT NULL,
	Seen int NOT NULL,
);