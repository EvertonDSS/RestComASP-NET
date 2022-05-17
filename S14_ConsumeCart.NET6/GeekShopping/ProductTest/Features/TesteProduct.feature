#language: pt-br

Funcionalidade: Acessar API de produtos e obter o Status Code


@GetAllProducts
Cenario: Acessar todos os produtos sem autenticação
	Dado que não estou autenticado
	Quando executo o metodo Get na URL
	Entao o statusCode deve retornar OK
	
@GetProductId
Cenario: Acessar um produto pelo id estando autenticado
	Dado que estou autenticado no sistema
	E acesso produto com o id '3'
	Quando executo o metodo Get na URL
	E adiciono o id na URL
	Entao o statusCode deve retornar OK

@CreateProduct
Cenario: Adicionar um novo produto no sistema estando autenticado
	Dado que estou autenticado no sistema
	E quero criar um novo product
	Quando executo o metodo Post na URL
	Entao o statusCode deve retornar OK
	
@DeleteProducts
Cenario: Quero apagar um produto do sistema pelo id estando autenticado
	Dado que estou autenticado no sistema
	E acesso produto com o id '25'
	Quando executo o metodo Delete na URL
	Entao o statusCode deve retornar OK

@AtualizarProduct
Cenario: Quero atualizar um produto do sistema pelo id estando autenticado
	Dado que estou autenticado no sistema
	E atualizo um produto existente
	Quando executo o metodo Put na URL
	Entao o statusCode deve retornar OK
