Feature: Product

In order to have available products for customer to buy in ecommcerce website.
As an admin of the website. I want to manage (get, create, update and delete) products.
And customer can find products that they want to.

@createProduct
Scenario: CreateProduct
	Given The following product dataset
	|	Id	| Name      | Value | Currency | TradeMark | Origin   | Discription         |
	|	1	| Product 1 | 100   | USA      | Viet Nam  | Viet Nam | This is a product 1 |
	When Admin wants to create an product
	Then The product repository should has an product
	|	Id	| Name      | Value | Currency | TradeMark | Origin   | Discription         |
	|	1	| Product 1 | 100   | USA      | Viet Nam  | Viet Nam | This is a product 1 |

@getProducts
Scenario: GetProducts
	Given The product repository already exists the following products
	|	Id	| Name      | Value | Currency | TradeMark | Origin		| Discription         |
	|	1	| Product 1 | 100   | USA      | Viet Nam  | Viet Nam	| This is a product 1 |
	|	2	| Product 2 | 200   | USA      | Viet Nam  | China		| This is a product 2 |
	When User wants to get all products
	Then The product repository should return all products
	|	Id	| Name      | Value | Currency | TradeMark | Origin		| Discription         |
	|	1	| Product 1 | 100   | USA      | Viet Nam  | Viet Nam	| This is a product 1 |
	|	2	| Product 2 | 200   | USA      | Viet Nam  | China		| This is a product 2 |

@getTheDetailsOfAnExistProduct
Scenario: GetTheDetailsOfAnExistingProduct
	Given The product repository already exists the following products
	|	Id	| Name      | Value | Currency | TradeMark | Origin		| Discription         |
	|	1	| Product 1 | 100   | USA      | Viet Nam  | Viet Nam	| This is a product 1 |
	|	2	| Product 2 | 200   | USA      | Viet Nam  | China		| This is a product 2 |
	When User wants to get detais of an product with id 1
	Then The product repository should return required product dataset
	|	Id	| Name      | Value | Currency | TradeMark | Origin		| Discription         |
	|	1	| Product 1 | 100   | USA      | Viet Nam  | Viet Nam	| This is a product 1 |

@getTheDetailsThatDoNotExist
Scenario: GetTheDetailsThatDoNotExist
	Given The product repository already exists the following products
	|	Id	| Name      | Value | Currency | TradeMark | Origin		| Discription         |
	|	1	| Product 1 | 100   | USA      | Viet Nam  | Viet Nam	| This is a product 1 |
	|	2	| Product 2 | 200   | USA      | Viet Nam  | China		| This is a product 2 |
	When User wants to get detais of an product with id 3
	Then There is no product with id 3 and the return status is NotFound

@updateProduct
Scenario: UpdateProduct
	Given The product repository already exists the following products
	|	Id	| Name      | Value | Currency | TradeMark | Origin		| Discription         |
	|	1	| Product 1 | 100   | USA      | Viet Nam  | Viet Nam	| This is a product 1 |
	|	2	| Product 2 | 200   | USA      | Viet Nam  | China		| This is a product 2 |
	When User wants to update product with id 1 according to the following dataset
	|	Id	| Name      | Value | Currency | TradeMark	| Origin	| Discription				|
	|	1	| Product 1 | 200   | USA      | China		| China		| This is a changed product	|
	Then In the product repository the data of the product is 1 is changed


@deleteProduct
Scenario: DeleteProduct
	Given The product repository already exists the following products
	|	Id	| Name      | Value | Currency | TradeMark | Origin		| Discription         |
	|	1	| Product 1 | 100   | USA      | Viet Nam  | Viet Nam	| This is a product 1 |
	|	2	| Product 2 | 200   | USA      | Viet Nam  | China		| This is a product 2 |
	When User wants to delete product with id 2
	Then There is no product with id 2 and the return status is NotFound
