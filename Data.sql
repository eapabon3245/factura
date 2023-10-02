create database FacturaDb
go

use FacturaDb;

create table Facturas
( 
	[IdFactura] [int] IDENTITY(1,1) NOT NULL Primary key,
	NumeroFactura [varchar](800) NOT NULL,
    Fecha datetime NOT NULL,
	TipodePago [varchar](800) NOT NULL,
	Documentocliente  [varchar](800) NOT NULL,
	Nombrecliente  [varchar](800) NOT NULL,
	Subtotal decimal(18,2) NULL,
	Descuento decimal(18,2) NULL,
	IVA decimal(18,2) NULL,
	TotalDescuento decimal(18,2) NULL,
	TotalImpuesto decimal(18,2) NULL,
	Total decimal(18,2) NULL
);


create table Productos
( 
	[IdProducto] [int] IDENTITY(1,1) NOT NULL Primary key,
	Producto [varchar](800) NOT NULL
);

create table Detalles
( 
	[IdDetalle] [int] IDENTITY(1,1) NOT NULL Primary key,
    IdFactura [int] NOT NULL,
	IdProducto [int] NOT NULL,
	Cantidad bigint NOT NULL,
    PrecioUnitario bigint NULL
);

alter table Detalles
   add constraint FK_Producto foreign key (IdProducto)
      references Productos (IdProducto)
go

alter table Detalles
   add constraint FK_Factura foreign key (IdFactura)
      references Facturas (IdFactura)
go

insert into Productos
(Producto)
values
('Camisa'),
('Pantalon'),
('Zapatos'),
('Tenis'),
('Falda'),
('Blusa')

