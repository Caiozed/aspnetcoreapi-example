/*DROP	TABLE	persons;*/
CREATE TABLE	persons (
	Id int(10) NOT NULL,
	FirstName VARCHAR(500) NULL,
	LastName VARCHAR(500) NULL,
	Address VARCHAR(500) NULL,
	Gender VARCHAR(50) NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;