USE Sample2;
GO

-- 1. Remove the existing foreign key if it exists
IF EXISTS (
    SELECT 1
    FROM sys.foreign_keys
    WHERE name = 'tblPerson_genderID_FK'
)
BEGIN
    ALTER TABLE tblPerson
    DROP CONSTRAINT tblPerson_genderID_FK;
END
GO

-- 2. Remove the existing default constraint if it exists
IF EXISTS (
    SELECT 1
    FROM sys.default_constraints
    WHERE name = 'df_tblperson_genderID'
)
BEGIN
    ALTER TABLE tblPerson
    DROP CONSTRAINT df_tblperson_genderID;
END
GO

-- 3. Remove the gender table
IF OBJECT_ID('dbo.tblGender', 'U') IS NOT NULL
BEGIN
    DROP TABLE tblGender;
END
GO

-- 4. Create tblGender again
CREATE TABLE tblGender
(
    ID INT NOT NULL PRIMARY KEY,
    Gender NVARCHAR(50) NOT NULL
);
GO

-- 5. Insert genders FIRST
INSERT INTO tblGender (ID, Gender)
VALUES
(1, 'Female'),
(2, 'Male'),
(3, 'Unknown');
GO

-- 6. Clear existing person data
DELETE FROM tblPerson;
GO

-- 7. Reset the identity counter
DBCC CHECKIDENT ('tblPerson', RESEED, 0);
GO

-- 8. Add default value
ALTER TABLE tblPerson
ADD CONSTRAINT df_tblperson_genderID
DEFAULT 3 FOR GenderID;
GO

-- 9. Add foreign key with CASCADE
ALTER TABLE tblPerson
ADD CONSTRAINT tblPerson_genderID_FK
FOREIGN KEY (GenderID)
REFERENCES tblGender(ID)
ON DELETE CASCADE;
GO

-- 10. Insert people
INSERT INTO tblPerson (Name, GenderID)
VALUES
('John', 2),
('Marry Jane', 1),
('Peter Parker', NULL),
('Fishey', 2);
GO

-- 11. Insert Rita without GenderID
-- Default value 3 will be used
INSERT INTO tblPerson (Name)
VALUES
('Rita');
GO

-- 12. Check both tables
SELECT * FROM tblGender;
SELECT * FROM tblPerson;
GO

-- 13. See the relationship using JOIN
SELECT
    p.ID,
    p.Name,
    p.GenderID,
    g.Gender
FROM tblPerson p
LEFT JOIN tblGender g
    ON p.GenderID = g.ID;
GO

-- 14. adding new cols-- 
alter table tblPerson
ADD 
    Age int,
    Email NVARCHAR(50)
SELECT * from tblPerson

--15. updating rows--
UPDATE tblPerson
SET Age = 25,
    Email = 'john@gmail.com'
WHERE Name = 'John';

UPDATE tblPerson
SET Age = 24,
    Email = 'mary@gmail.com'
WHERE Name = 'Marry Jane';

UPDATE tblPerson
SET Age = 30,
    Email = 'peter@gmail.com'
WHERE Name = 'Peter Parker';

UPDATE tblPerson
SET Age = 28,
    Email = 'fishey@gmail.com'
WHERE Name = 'Fishey';

UPDATE tblPerson
SET Age = 26,
    Email = 'rita@gmail.com'
WHERE Name = 'Rita';

--16. adding check constraint--

ALTER TABLE tblPerson
ADD CONSTRAINT CK_tblPerson_Age
CHECK (Age > 0 AND Age < 100);

UPDATE tblPerson
SET Age=150
WHERE Name='Fishey'; --error

-- 17. adding unique key --

alter table tblPerson
add CONSTRAINT uq_tblPerson_Email
UNIQUE (Email)

INSERT into tblPerson (Name, GenderID,Age, Email) VALUES
(
    'gita',1,34,'gita@mail.com'
),
(
    'gita',1,34,'gita1@mail.com'
)

select * from tblPerson


--- 18. Rseeding--
DBCC CHECKIDENT('tblPerson',RESEED,80) --SQL Server sets the identity's current/reseed value to 80.

INSERT INTO tblPerson (Name, GenderID, Age, Email)
VALUES ('Neha', 1, 25, 'neha@mail.com'); --The new row will normally get: ID = 81


