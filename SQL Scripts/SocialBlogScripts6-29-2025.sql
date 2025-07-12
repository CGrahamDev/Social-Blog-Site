CREATE DATABASE SocialBlogDB;
GO
USE SocialBlogDB;
GO

CREATE TABLE Users(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Username VARCHAR(32) NOT NULL,
	Password NVARCHAR(128) NOT NULL,
	Description NVARCHAR(500)
);

CREATE TABLE Posts(
	 Id INT IDENTITY(1,1) PRIMARY KEY,
	 Title VARCHAR(128) NOT NULL,
	 Content VARCHAR(1600) NOT NULL,
	 AuthorId INT FOREIGN KEY REFERENCES Users(Id),
);

CREATE TABLE Comments(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Content VARCHAR(256) NOT NULL,
	Likes INT NOT NULL,
	UserId INT FOREIGN KEY REFERENCES Users(Id),
	PostId INT FOREIGN KEY REFERENCES Posts(Id),
);


DBCC CHECKIDENT (Users, RESEED, 0);
DBCC CHECKIDENT (Posts, RESEED, 0);
DBCC CHECKIDENT (Comments, RESEED, 0);

INSERT INTO Users(Username, Password, Description) VALUES ('admin','', null);
INSERT INTO Posts(Title, Content, AuthorId) VALUES ('Sample Post','Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum', 1);
INSERT INTO Comments(Content, Likes, UserId, PostId) VALUES ('I find this post insightful',0,1,1);

SELECT * FROM Users;
SELECT * FROM Posts;
SELECT * FROM Comments;

