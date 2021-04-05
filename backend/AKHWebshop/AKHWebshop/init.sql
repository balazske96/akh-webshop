INSERT INTO `product`
    (id, name, display_name, image_name, status, price)
VALUES ('bf51bc59-f8b6-45ff-e9ad-08d8f8479440', 'akh-classic-polo', 'AKH Classic Póló', '/image/to/polo.jpg', 'Hidden',
        3300);

INSERT INTO `size_record`
    (product_id, size, quantity)
VALUES ('bf51bc59-f8b6-45ff-e9ad-08d8f8479440', 'L', 1),
       ('bf51bc59-f8b6-45ff-e9ad-08d8f8479440', 'M', 2),
       ('bf51bc59-f8b6-45ff-e9ad-08d8f8479440', 'XL', 2);