SELECT  veh.idVehicle,
        customer.nameCustomer,
        veh.licensePlate,
        veh.maker,
        veh.model,
        veh.color
FROM vehicle veh
LEFT JOIN customer ON veh.idCustomer = customer.idCustomer;

SELECT *
FROM customer;