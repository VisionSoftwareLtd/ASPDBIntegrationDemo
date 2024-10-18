CREATE TABLE Item (ItemName VARCHAR(255) NOT NULL);
-- @block
SELECT *
FROM Item;
-- @block
INSERT INTO Item (ItemName)
VALUES ('Apple'),
  ('Banana'),
  ('Cherry');
-- @block
DELETE FROM Item
WHERE ItemName LIKE '@%';