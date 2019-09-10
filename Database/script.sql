CREATE TABLE IF NOT EXISTS Computers (
    Name VARCHAR(100) NOT NULL,
	LastUsage DATE NOT NULL,
    SerialNumber VARCHAR(1000) NULL,
	Model VARCHAR(1000) NULL,
	UndecodedWindowsProductKey VARCHAR(1000) NULL,
	DecodedWindowsProductKey VARCHAR(1000) NULL,
	Chassis VARCHAR(1000) NULL,
	PhysicalMemory VARCHAR(1000) NULL,
	Manufacturer VARCHAR(1000) NULL,
	UserName VARCHAR(1000) NULL,
	OS_Build VARCHAR(1000) NULL,
	OS_InstallDate DATE NULL,
	OS_Manufacturer VARCHAR(1000) NULL,
	OS_Architecture VARCHAR(1000) NULL,
	OS_Name VARCHAR(1000) NULL,
	OS_SerialNumber VARCHAR(1000) NULL,
	OS_Version VARCHAR(1000) NULL,
	Bios_Date DATE NULL,
	Bios_Manufacturer VARCHAR(1000) NULL,
	Bios_Version VARCHAR(1000) NULL,
	Motherboard_ProductID VARCHAR(1000) NULL,
	Motherboard_Manufacturer VARCHAR(1000) NULL,
	Motherboard_Version VARCHAR(1000) NULL,
	Motherboard_SerialNumber VARCHAR(1000) NULL,
	Processor_Name VARCHAR(1000) NULL,
	Processor_Manufacturer VARCHAR(1000) NULL,
	Processor_Architecture VARCHAR(1000) NULL,
	Processor_NumberOfCores VARCHAR(1000) NULL,
	Processor_NumberOfLogicalProcessors VARCHAR(1000) NULL,
	PRIMARY KEY (Name)
)  ENGINE=INNODB;


CREATE TABLE IF NOT EXISTS Disks (
    ID CHAR(36) NOT NULL COLLATE ascii_general_ci,
    SerialNumber VARCHAR(1000) NULL,
	Name VARCHAR(1000) NULL,
	Manufacturer VARCHAR(1000) NULL,
	Size VARCHAR(1000) NULL,
	ComputerName VARCHAR(100) NOT NULL,
	RemovedDate DATE NULL,
	PRIMARY KEY (ID),
	FOREIGN KEY (ComputerName) REFERENCES Computers(Name)
)  ENGINE=INNODB;


CREATE TABLE IF NOT EXISTS LogicalDrives (
    ID CHAR(36) NOT NULL COLLATE ascii_general_ci,
    Name VARCHAR(1000) NULL,
	Size VARCHAR(1000) NULL,
	FileSystem VARCHAR(1000) NULL,
	SerialNumber VARCHAR(1000) NULL,
	FreeSpace VARCHAR(1000) NOT NULL,
	ComputerName VARCHAR(100) NOT NULL,
	RemovedDate DATE NULL,
	PRIMARY KEY (ID),
	FOREIGN KEY (ComputerName) REFERENCES Computers(Name)
)  ENGINE=INNODB;


CREATE TABLE IF NOT EXISTS NetworkAdapters (
    ID CHAR(36) NOT NULL COLLATE ascii_general_ci,
    Name VARCHAR(1000) NULL,
	MACAddress VARCHAR(1000) NULL,
	Manufacturer VARCHAR(1000) NULL,
	NetConnectionID VARCHAR(1000) NULL,
	NetEnabled BIT NOT NULL DEFAULT 0,
	ComputerName VARCHAR(100) NOT NULL,
	RemovedDate DATE NULL,
	PRIMARY KEY (ID),
	FOREIGN KEY (ComputerName) REFERENCES Computers(Name)
)  ENGINE=INNODB;


CREATE TABLE IF NOT EXISTS Softwares (
    ID CHAR(36) NOT NULL COLLATE ascii_general_ci,
    Name VARCHAR(1000) NULL,
	Version VARCHAR(1000) NULL,
	Publisher VARCHAR(1000) NULL,
	InstallDate DATE NULL,
	Type VARCHAR(1000) NULL,
	ComputerName VARCHAR(100) NOT NULL,
	RemovedDate DATE NULL,
	PRIMARY KEY (ID),
	FOREIGN KEY (ComputerName) REFERENCES Computers(Name)
)  ENGINE=INNODB;