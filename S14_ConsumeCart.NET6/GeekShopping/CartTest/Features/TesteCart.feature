#language: pt-br
Funcionalidade: Testar CartAPI

@GetCart
Cenario: Obter carrinho do usuario
Dado que acesso a rota '/api/v1/Cart/find-cart' 
E o id de usuario '887101dc-1eb2-4e94-ac26-7915c3f718e8'
Quando executar o metodo get
Entao o status code deve ser '200'


@PostCart
Cenario: Adicionar produto ao carrinho
Dado que acesso a rota '/api/v1/Cart/add-cart'
E o id de usuario '887101dc-1eb2-4e94-ac26-7915c3f718e8'
E quero criar um novo carrinho
Quando executar o metodo post
Entao o status code deve ser '200'

@DeleteCart
Cenario: Remover meu carrinho de compras
Dado que acesso a rota '/api/v1/Cart/remove-cart'
E o id do carrinho é '89'
Quando executar o metodo delete
Entao o status code deve ser '200'