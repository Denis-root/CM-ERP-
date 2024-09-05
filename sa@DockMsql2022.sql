CREATE DATABASE TALLER;
GO

CREATE TABLE CATEGORIAEQUIPO (
    idcategoria INT IDENTITY(1,1) PRIMARY KEY,
    codigo CHAR(80),
    categoria TEXT
);

CREATE TABLE CLIENTE (
    idcliente INT IDENTITY(1,1) PRIMARY KEY,
    codigo CHAR(80),
    nombres TEXT,
    apellido TEXT,
    correo TEXT,
    telefono CHAR(16)
);

CREATE TABLE MECANICO (
    idmecanico INT IDENTITY(1,1) PRIMARY KEY,
    codigo CHAR(80),
    nombres TEXT,
    apellido TEXT,
    correo TEXT,
    telefono CHAR(16),
    tarifahora DECIMAL(18,2)
);

CREATE TABLE PRODUCTO (
    idproducto INT IDENTITY(1,1) PRIMARY KEY,
    codigo CHAR(80),
    producto TEXT,
    precio DECIMAL(18,2)
);

CREATE TABLE EQUIPO (
    idequipo INT IDENTITY(1,1) PRIMARY KEY,
    idcategoria INT,
    codigo CHAR(80),
    nombre CHAR(150),
    modelo CHAR(150),
    marca CHAR(150),
    serie CHAR(150),
    estado INT,
    FOREIGN KEY (idcategoria) REFERENCES CATEGORIAEQUIPO(idcategoria)
);

--ODSCAB (Orden de Servicio Cabecera, depende de CLIENTE y MECANICO)
/*
CREATE TABLE ODSCAB (
    idodscab INT IDENTITY(1,1) PRIMARY KEY,
    idcliente INT,
    idmecanico INT,
    fecha DATE,
    FOREIGN KEY (idcliente) REFERENCES CLIENTE(idcliente),
    FOREIGN KEY (idmecanico) REFERENCES MECANICO(idmecanico)
);
*/

CREATE TABLE ODSCAB (
    idodscab INT IDENTITY(1,1) PRIMARY KEY,
    idcliente INT,
    idmecanico INT,
    idequipo INT,  -- Campo nuevo para relacionar con EQUIPO
    fecha DATE,
    FOREIGN KEY (idcliente) REFERENCES CLIENTE(idcliente),
    FOREIGN KEY (idmecanico) REFERENCES MECANICO(idmecanico),
    FOREIGN KEY (idequipo) REFERENCES EQUIPO(idequipo)  -- Nueva clave foránea
);


--ODSET (Orden de Servicio Detalle, depende de ODSCAB y PRODUCTO)
CREATE TABLE ODSET (
    idodsset INT IDENTITY(1,1) PRIMARY KEY,
    idodscab INT,
    idproducto INT,
    codigo CHAR(80),
    cantidad DECIMAL(18,2),
    precio DECIMAL(18,2),
    FOREIGN KEY (idodscab) REFERENCES ODSCAB(idodscab),
    FOREIGN KEY (idproducto) REFERENCES PRODUCTO(idproducto)
);

GO
-- Desactivar la verificación de claves foráneas
EXEC sp_MSforeachtable "ALTER TABLE ? NOCHECK CONSTRAINT all"

-- Eliminar todas las tablas
DROP TABLE IF EXISTS ODSET;
DROP TABLE IF EXISTS ODSCAB;
DROP TABLE IF EXISTS EQUIPO;
DROP TABLE IF EXISTS PRODUCTO;
DROP TABLE IF EXISTS MECANICO;
DROP TABLE IF EXISTS CLIENTE;
DROP TABLE IF EXISTS CATEGORIAEQUIPO;

-- Reactivar la verificación de claves foráneas
EXEC sp_MSforeachtable "ALTER TABLE ? WITH CHECK CHECK CONSTRAINT all"




GO
INSERT INTO CATEGORIAEQUIPO (codigo, categoria) VALUES
('CAT100', 'Herramientas Eléctricas'),
('CAT200', 'Herramientas Manuales'),
('CAT300', 'Equipos de Medición')
('CAT400', 'Equipos de Seguridad'),
('CAT500', 'Accesorios Generales');

GO
INSERT INTO CLIENTE (codigo, nombres, apellido, correo, telefono) VALUES
('CLI100', 'Juan', 'Pérez', 'juan.perez@email.com', '555-1234'),
('CLI200', 'María', 'López', 'maria.lopez@email.com', '555-5678'),
('CLI300', 'Carlos', 'Martínez', 'carlos.martinez@email.com', '555-9101'),
('CLI400', 'Ana', 'Ramírez', 'ana.ramirez@email.com', '555-1213'),
('CLI500', 'Jesus', 'Soriano', 'jesus.sorianoz@email.com', '555-3244');


GO
INSERT INTO EQUIPO (idcategoria, codigo, nombre, modelo, marca, serie, estado) VALUES
(1, 'EQ100', 'Taladro', 'T-800', 'Black&Decker', 'BD12345', 1),
(2, 'EQ200', 'Martillo', 'M-900', 'Stanley', 'ST67890', 1),
(3, 'EQ300', 'Sierra Circular', 'SC-550', 'DeWalt', 'DW2020', 1),
(1, 'EQ400', 'Destornillador Eléctrico', 'DE-400', 'Makita', 'MK4499', 1),
(2, 'EQ500', 'Lijadora', 'L-300', 'Bosch', 'BS7890', 1);



GO
INSERT INTO MECANICO (codigo, nombres, apellido, correo, telefono, tarifahora) VALUES
('MEC001', 'Luis', 'Alvarez', 'luis.alvarez@example.com', '300-400-5000', 75.00),
('MEC002', 'Ana', 'Martinez', 'ana.martinez@example.com', '300-500-6000', 85.00);
GO
INSERT INTO PRODUCTO (codigo, producto, precio) VALUES
('PROD001', 'Aceite de motor 5W-30', 120.00),
('PROD002', 'Filtro de aire', 50.00);

GO
INSERT INTO ODSCAB (idcliente, idmecanico, fecha) VALUES
(1, 1, '2024-01-15'),
(2, 2, '2024-01-16')
(1, 2, '2024-01-19'),
(2, 1, '2024-01-20'),
(1, 1, '2024-01-21');


GO
INSERT INTO ODSET (idodscab, idproducto, codigo, cantidad, precio) VALUES
(1, 1, 'PROD1', 5, 150.00),
(2, 1, 'PROD2', 3, 75.00)
(4, 2, 'PROD2', 2, 50.00),
(5, 1, 'PROD1', 1, 120.00)
(3, 2, 'PROD2', 4, 75.00); 



GO
CREATE PROCEDURE sp_mecanicos
AS
BEGIN
    SELECT 
        codigo, 
        CONVERT(varchar, nombres) + ' ' + CONVERT(varchar, apellido) AS NombreCompleto, 
        telefono, 
        tarifaHora
    FROM 
        MECANICO;
    -- Suma de tarifas por hora
    SELECT 
        SUM(tarifaHora) AS TotalTarifaHora
    FROM 
        MECANICO;
END


GO
EXEC sp_mecanicos


GO
SELECT * FROM CATEGORIAEQUIPO
GO
SELECT * FROM CLIENTE
GO
SELECT * FROM EQUIPO
GO
SELECT * FROM MECANICO
GO
SELECT * FROM PRODUCTO
GO
SELECT * FROM ODSCAB
GO
SELECT * FROM ODSET