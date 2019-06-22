CREATE TABLE IF NOT EXISTS books (
	Id int(10) AUTO_INCREMENT PRIMARY KEY NOT NULL,
	Author longtext NULL,
	LauchDate datetime(6) NOT NULL,
	Price decimal(65,2) NOT NULL,
	Title longtext NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;