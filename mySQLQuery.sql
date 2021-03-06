--ALTER TABLE SubCategories     
--ADD CONSTRAINT FK_SubCategories_QuoteCategoriesID FOREIGN KEY (QuoteCatID)     
--    REFERENCES QuoteCategories (ID)     
--    ON UPDATE CASCADE
	
--ALTER TABLE QuoteCategories
--ADD CONSTRAINT QuoteCategories_PK PRIMARY KEY (ID);   

DBCC CHECKIDENT ('CompanyList', RESEED, 0)


ALTER TABLE Companies
ADD isTopPick bit

ALTER TABLE SubQuoteCategories
ADD CONSTRAINT FK_SubQuoteCategories_QuoteCategories_ID
    FOREIGN KEY (QuoteCatID)
    REFERENCES QuoteCategories (ID);

SELECT a.ID, a.Title, a.Description, a.ImgFilename, b.Name FROM SubQuoteCategories  AS a
INNER JOIN QuoteCategories AS b
ON a.QuoteCatID = b.ID

ALTER TABLE Companies
ALTER COLUMN isTopPick bit not null

ALTER TABLE ReqQuoteForm
ALTER COLUMN ContactNum varchar(30) not null


ALTER TABLE FloorPlanFiles
ADD reqQuoteFormId int
FOREIGN KEY(reqQuoteFormId) REFERENCES ReqQuoteForm (ID)

ALTER TABLE SelectedServices
ADD FOREIGN KEY(ServiceID) REFERENCES [MOR1].[dbo].[Services] (ID)

SELECT ID, CompanyName, CompanyLogoFilename FROM CompanyList
                        WHERE SUBSTRING(CompanyName, 1, 1) = 'B'


SELECT CompanyName, Companies_Services.CompanyID, Companies_Services.ServiceID, 
Services.Title, ServiceCategories.Name 
FROM Companies
INNER JOIN Companies_Services ON Companies.ID = Companies_Services.CompanyID
INNER JOIN Services ON Companies_Services.ServiceID = Services.ID
INNER JOIN ServiceCategories ON Services.CatID = ServiceCategories.ID

SELECT DISTINCT Companies.CompanyName, ServiceCategories.Name 
FROM Companies
INNER JOIN Companies_Services ON Companies.ID = Companies_Services.CompanyID
INNER JOIN Services ON Companies_Services.ServiceID = Services.ID
INNER JOIN ServiceCategories ON Services.CatID = ServiceCategories.ID
WHERE CompanyName LIKE '%B%'

SELECT DISTINCT Comp.CompanyName, ServiceCategories.Name 
FROM (SELECT ID, CompanyName FROM Companies WHERE CompanyName LIKE '%A%') AS Comp
INNER JOIN Companies_Services ON Comp.ID = Companies_Services.CompanyID
INNER JOIN Services ON Companies_Services.ServiceID = Services.ID
INNER JOIN ServiceCategories ON Services.CatID = ServiceCategories.ID
ORDER BY ServiceCategories.Name

WHERE CompanyName LIKE '%B%'

SELECT DISTINCT * FROM Companies WHERE CompanyName LIKE '%A%';

SELECT * FROM Companies_Services
INNER JOIN Services ON Companies_Services.ServiceID = Services.ID

SELECT * FROM Companies WHERE CompanyName LIKE '%A1%';
USE MOR1
SELECT cs.CompanyID, cs.ServiceID, s.CatID, s.Title FROM Companies AS comp  INNER JOIN Companies_Services AS cs ON CompanyName LIKE '%A%' AND comp.ID = cs.CompanyID INNER JOIN Services AS s ON cs.ServiceID = s.ID;


SELECT DISTINCT c.* FROM Companies AS c
INNER JOIN Companies_Services AS cs ON c.CompanyName LIKE '%a%' AND c.ID = cs.CompanyID
INNER JOIN Services AS s ON cs.ServiceID = s.ID
AND s.ID IN (1,2);

SELECT c.* FROM Companies AS c INNER JOIN Companies_Services AS cs ON c.CompanyName LIKE '%b%' AND c.ID = cs.CompanyID INNER JOIN Services AS s ON cs.ServiceID = s.ID;

ALTER TABLE Companies
ADD OverallRating decimal(4,2);

SELECT cm.ID, cm.CommentContent, cm.PersonName, cm.Rating FROM Companies AS comp  INNER JOIN Comments AS cm ON comp.ID = 1 AND comp.ID = cm.CompID;

SELECT ph.ID, ph.FileName, ph.CompID FROM Companies AS comp  INNER JOIN ProjectPhotos AS ph ON comp.ID = 1 AND comp.ID = ph.CompID;


SELECT * FROM ReqQuoteForm

ALTER TABLE ReqQuoteForm
ALTER COLUMN LoanRequired bit not null