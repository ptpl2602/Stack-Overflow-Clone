CREATE DATABASE StackOverflowClone
GO

USE StackOverflowClone
GO

CREATE TABLE Categories (
	CategoryID	INT IDENTITY(1,1) PRIMARY KEY,
	CategoryName	NVARCHAR(MAX)
)
GO

CREATE TABLE Users(
	UserID	INT IDENTITY(1,1) PRIMARY KEY,
	Email	NVARCHAR(MAX),
	PasswordHash	NVARCHAR(MAX),
	Name	NVARCHAR(MAX),
	PhoneNumber	NVARCHAR(MAX),
	IsAdmin	BIT	DEFAULT(0),
)
GO

CREATE TABLE Questions(
	QuestionID	INT IDENTITY(1,1) PRIMARY KEY,
	QuestionName	NVARCHAR(MAX),
	QuestionDateAndTime	DATETIME,
	VoteCount	INT,
	AnswerCount	INT,
	ViewCount	INT,
	UserID	INT,
	CategoryID	INT,
	FOREIGN KEY (UserID) REFERENCES Users(UserID) ON DELETE CASCADE,		
	FOREIGN KEY (CategoryID) REFERENCES Categories(CategoryID) ON DELETE CASCADE
	/*ON DELETE CASCADE: nếu một khách hàng bị xóa khỏi bảng Khách hàng, tất cả các hàng trong bảng 
	Đơn hàng có chứa cùng một giá trị mã định danh khách hàng cũng sẽ bị xóa. */
)
GO

CREATE TABLE Answers(
	AnswerID	INT IDENTITY(1,1) PRIMARY KEY,
	AnswerText	NVARCHAR(MAX),
	AnswerDataAndTime	DATETIME,
	VoteCount	INT,
	UserID	INT,
	QuestionID	INT,

	FOREIGN KEY (UserID) REFERENCES Users(UserID),
	FOREIGN KEY (QuestionID) REFERENCES Questions(QuestionID) ON DELETE CASCADE
)
GO

CREATE TABLE Votes(
	VoteID	INT	IDENTITY(1,1) PRIMARY KEY,
	UserID	INT,
	AnswerID	INT,
	VoteValue	INT,

	FOREIGN KEY (UserID) REFERENCES Users(UserID),
	FOREIGN KEY (AnswerID) REFERENCES Answers(AnswerID) ON DELETE CASCADE
)
GO

CREATE TABLE VotesQuestions(
	VoteID	INT	IDENTITY(1,1) PRIMARY KEY,
	UserID	INT,
	QuestionID	INT,
	VoteValue	INT,

	FOREIGN KEY (UserID) REFERENCES Users(UserID),
	FOREIGN KEY (QuestionID) REFERENCES Questions(QuestionID) ON DELETE CASCADE
)

ALTER TABLE Answers
ADD IsAccepted BIT DEFAULT(0);

ALTER TABLE Questions
ADD AnswerID INT

ALTER TABLE Questions
ADD FOREIGN KEY (AnswerID) REFERENCES Answers(AnswerID) ON DELETE NO ACTION;
/*-------------------------------------------------------------*/
INSERT INTO Users VALUES ('test@gmail.com', 'ecd71870d1963316a97e3ac3408c9835ad8cf0f3c1bc703527c30265534f75ae', 'Test', '0000000000', 0)

INSERT INTO Categories VALUES ('html')
INSERT INTO Categories VALUES ('css')
INSERT INTO Categories VALUES ('javascript')
INSERT INTO Categories VALUES ('php')
INSERT INTO Categories VALUES ('c#')
INSERT INTO Categories VALUES ('json')
INSERT INTO Categories VALUES ('api')
INSERT INTO Categories VALUES ('python')
INSERT INTO Categories VALUES ('nodejs')
INSERT INTO Categories VALUES ('java')
INSERT INTO Categories VALUES ('c++', 'C++ is a general-purpose programming language. Use this tag for questions about/utilizing C++. Do not also tag questions with [c] unless you have a good reason. C and C++ are different languages. Use a versioned tag such as [c++11], [c++20] etc. for questions specific to a standard revision.')

INSERT INTO Questions VALUES('How to export the Canvas as image using fabric.js and React', '2023-10-10 12:03 am', 0, 0, 0, 1, 9, 'I am using fabric.js in react application . The scenario i was trying is i want to export the Entire canvas as image but below are the issues im gettingCanvas is resetting after pressing export button.  After zoomed or panned the i am unable to export the content which is outside viewport . I am able to export only which is viewable on screen .But i need to export entire object which are placed on the canvas irrespective of zoomed and pan .  below is my code')
INSERT INTO Questions VALUES('Swap a form on error, or redirect on success using HTMX', '2023-10-3 08:26 pm', 0, 0, 0, 1, 1, 'I have a problem when there''s an error, backend returns the form with outlined errors, with 400 status code (hence response-targets extension). On success, backend responds HX-Location header of user''s profile page, where I expect user to be redirected. Put simply:  Form should be re-rendered on error (400 Bad Request)  User should be redirected to their profile page on success (200 OK)  What actually happens is that HTMX renders entire user''s profile page in #login_form instead of redirecting, as if hx-target overrides or takes precedence over HX-Location header.    Tried this without response-target plugin, it works the same. With response-target plugin I can''t omit hx-target because then hx-target-400 is not recognized or handled.    How can I put it together so forms are re-rendered on errors, or redirected to a page on success?')
INSERT INTO Questions VALUES('GCC/Clang function attributes per template instantiation', GETDATE(), 0, 0, 0, 5, 11, 'I have some hand-vectorized C++ code that I''m trying to make a distribute-able binary for via function multiversioning. Since the code uses SIMD intrinsics for different instruction sets (SSE2, AVX2, AVX512), it uses template specializations to decide on which intrinsics to use.')

/*-------------------------------------------------------------*/
SELECT * FROM Categories
SELECT * FROM Users
SELECT * FROM Votes
SELECT * FROM Answers
SELECT * FROM Questions
SELECT * FROM VotesQuestions

ALTER TABLE Categories
ADD CategoryDescription NVARCHAR(MAX)

UPDATE Questions
SET VoteCount = 0
WHERE QuestionID = 5;

UPDATE Answers
SET IsAccepted = 0
WHERE AnswerID = 20

DELETE VotesQuestions WHERE UserID = 7

SELECT A.CategoryName, A.CategoryDescription, COUNT(B.QuestionID) AS QuestionCount
FROM Categories AS A
LEFT JOIN Questions AS B ON A.CategoryID = B.CategoryID
GROUP BY A.CategoryName,  A.CategoryDescription

ALTER TABLE Questions
ADD QuestionContent NVARCHAR(MAX)
WHERE QuestionID = 1;
 
DELETE Answers WHERE AnswerID = 17 OR QuestionID = 13 OR QuestionID = 3

ALTER TABLE Answers
DELETE COLUMN IsAccepted BIT


SELECT COUNT(*) AS TotalQuestions
FROM Questions
WHERE UserID = 2;

SELECT COUNT(*) AS TotalAnswers
FROM Answers
WHERE UserID = 2;

SELECT DISTINCT CategoryID
FROM Questions
WHERE UserID = 2;
