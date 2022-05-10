#language: pt-br

Funcionalidade: Acessar API de produtos

@GetAllProducts
Cenario: Acessar todos os produtos sem autenticação
	Dado que eu acesso a rota '/api/v1/products'
	Quando realizo o metodo 'Get'
	Entao o statuscode deve ser 'OK'

@GetProductId
Cenario: Acessar um produto pelo id e com autenticação
	Dado estou autenticado
	E estou acessando rota '/api/v1/products'
	E o produto com o id '2'
	Quando realizo o metodo 'Get' no produto
	Entao o statuscode deve ser 'OK'

@CreateProduct
Cenario: Adicionar um novo produto no sistema
	Dado que estou autenticado
	E quero criar um novo produto
	Quando executar o método 'Post'
	Então o statuscode deve ser 'OK'

@DeleteProducts
Cenario: Estou autenticado e quero apagar um produto do sistema pelo id
	Dado que estou autenticado
	E que eu acesso a rota '/api/v1/products'
	E o produto o id '56'
	Quando executo o metodo 'Delete'
	Entao o statuscode deve ser 'OK'
