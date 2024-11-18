-- Criação do Banco de Dados
CREATE DATABASE SorteioDB;
GO

USE SorteioDB;

-- Criação da Tabela Participante
CREATE TABLE Participante (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nome VARCHAR(255) NOT NULL,
    StatusPresenca BIT
);

-- Criação da Tabela Sorteio
CREATE TABLE Sorteio (
    Id INT PRIMARY KEY IDENTITY(1,1),
    DataSorteio DATETIME NOT NULL,
    IdParticipante INT,
    FOREIGN KEY (IdParticipante) REFERENCES Participante(Id)
);


SELECT*FROM Participante;

SELECT*FROM Sorteio;
