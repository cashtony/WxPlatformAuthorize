DROP TABLE IF EXISTS AuthorizeEventRecords;
CREATE TABLE AuthorizeEventRecords(
	Id INTEGER PRIMARY KEY AUTOINCREMENT,
	EventType TEXT,
	EventXml TEXT,
	CreateTime DATETIME
);