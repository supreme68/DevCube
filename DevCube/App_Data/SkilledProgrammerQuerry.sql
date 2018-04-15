CREATE TABLE SkilledProgrammers (
	SkilledPrgrammesID int IDENTITY(1,1) PRIMARY KEY,
	SkillsID int FOREIGN KEY REFERENCES Skills(SkillsID),
	ProgrammersID int FOREIGN KEY REFERENCES Programmers(ProgrammersID),
)