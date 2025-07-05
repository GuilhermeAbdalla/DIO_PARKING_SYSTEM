CREATE TABLE IF NOT EXISTS customer(
    idCustomer INT NOT NULL AUTO_INCREMENT,
    nameCustomer VARCHAR(150),
    docCustomer VARCHAR(20),
    phoneCustomer VARCHAR(20),
    emailCustomer VARCHAR(150),
    active TINYINT,
    PRIMARY KEY (idCustomer)
);

CREATE TABLE IF NOT EXISTS vehicle(
    idVehicle INT NOT NULL AUTO_INCREMENT,
    idCustomer INT NOT NULL,
    licensePlate VARCHAR(10) NOT NULL,
    maker VARCHAR(255) NOT NULL,
    model VARCHAR(255) NOT NULL,
    color VARCHAR(255) NOT NULL,
    PRIMARY KEY (idVehicle),
    FOREIGN KEY (idCustomer) REFERENCES customer(idCustomer)
);

CREATE TABLE IF NOT EXISTS entryexit(
    parkID int AUTO_INCREMENT NOT NULL,
    idVehicle int NOT NULL,
    entryTime datetime NOT NULL,
    exitTime datetime,
    totalPrice DECIMAL(6, 2),
    PRIMARY KEY (parkID),
    FOREIGN KEY (idVehicle) REFERENCES vehicle(idVehicle)
);

SELECT *
from entryexit