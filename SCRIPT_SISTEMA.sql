CREATE DATABASE DBPROYECTO

USE DBPROYECTO

CREATE TABLE CURSO
(
ID_CURSO INT PRIMARY KEY IDENTITY (1,1),
NOMBRE_CURSO VARCHAR (20),
DESCRIPCION_CURSO VARCHAR (50),
)

INSERT INTO CURSO (NOMBRE_CURSO, DESCRIPCION_CURSO) VALUES
('SEXTO', 'MATUTINO'),
('SEPTIMO', 'VESPERTINO'),
('OCTAVO', 'MATUTINO')

SELECT * FROM CURSO


CREATE TABLE INSCRIPCION
(
ID_INSCRIPCION INT PRIMARY KEY IDENTITY (1,1),
FECHA_INSCRIPCION DATE,
ID_ESTUDIANTE VARCHAR (9),
ID_CURSO INT,
ESTADO_INSCRIPCION VARCHAR (50),
CONSTRAINT FK_CURSO FOREIGN KEY (ID_CURSO) REFERENCES CURSO (ID_CURSO)
)

INSERT INTO INSCRIPCION VALUES
('2022-11-19','2022-0344','1','Inscrito')

SELECT * FROM INSCRIPCION

CREATE TABLE SECCIONES
(
ID_SECCIONES INT PRIMARY KEY IDENTITY (1,1),
NOMBRE_SECCION VARCHAR(50),
CUPOS_SECCION INT,
ID_CURSO INT,
CONSTRAINT FK_CURSO_SECCIONES FOREIGN KEY (ID_CURSO) REFERENCES CURSO (ID_CURSO)
)

INSERT INTO SECCIONES VALUES
('2E','25','1')

CREATE TABLE ASIGNATURAS
(
ID_ASIGNATURAS INT PRIMARY KEY IDENTITY (1,1),
NOMBRE_ASIGNATURA VARCHAR(30),
DESCRIPCION VARCHAR(50),
CREDITOS INT,
)

INSERT INTO ASIGNATURAS VALUES
('Analisis y Dise�o', 'Introduccion al Analisis', '4')

CREATE TABLE PROFESORES
(
ID_PROFESOR INT PRIMARY KEY IDENTITY (1,1),
NOMBRE_PROFESOR VARCHAR (20),
CORREO_PROFESOR VARCHAR (50),
ID_SECCIONES INT,
ID_ASIGNATURAS INT,
CONSTRAINT FK_ASIGNATURA FOREIGN KEY (ID_ASIGNATURAS) REFERENCES ASIGNATURAS (ID_ASIGNATURAS),
CONSTRAINT FK_SECCIONES FOREIGN KEY (ID_SECCIONES) REFERENCES SECCIONES (ID_SECCIONES)
)

INSERT INTO PROFESORES VALUES
('Lorenzo','lorenzo@gmail.com','1','1')

CREATE TABLE PAGOS
(
ID_PAGO INT PRIMARY KEY IDENTITY (1,1),
FECHA_PAGO DATE,
ID_INSCRIPCION INT,
MONTO FLOAT,
CONSTRAINT FK_INSCRIPCION FOREIGN KEY (ID_INSCRIPCION) REFERENCES INSCRIPCION (ID_INSCRIPCION)
)

INSERT INTO PAGOS VALUES
('2023-10-20', '2', '2500')