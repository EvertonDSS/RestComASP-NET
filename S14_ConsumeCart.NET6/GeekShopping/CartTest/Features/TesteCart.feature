#language: pt-br
Funcionalidade: Testar CartAPI

@GetCart
Cenario: Obter carrinho do usuario
Dado que acesso a rota '/api/v1/Cart/find-cart' 
E o id de usuario '7fb0fb35-0e49-4c81-b858-fdabd3d5f295'
Quando executar o metodo get
Entao o status code deve ser '200'


@PostCart
Cenario: Adicionar produto ao carrinho
Dado que acesso a rota '/api/v1/Cart/add-cart'
E o id de usuario '7fb0fb35-0e49-4c81-b858-fdabd3d5f295'
E quero criar um novo carrinho
Quando executar o metodo post
Entao o status code deve ser '200'

@DeleteCart
Cenario: Remover meu carrinho de compras
Dado que acesso a rota '/api/v1/Cart/remove-cart'
E o id de usuario '7fb0fb35-0e49-4c81-b858-fdabd3d5f295'
E o id do carrinho é '10'
Quando executar o metodo delete
Entao o status code deve ser '200'

@DeleteCart
Cenario: Adicionar cupom de desconto
Dado que acesso a rota '/api/v1/Cart/apply-cart'
E o id de usuario '7fb0fb35-0e49-4c81-b858-fdabd3d5f295'
E o id do carrinho é '10'
Quando executar o metodo post
Entao o status code deve ser '200'