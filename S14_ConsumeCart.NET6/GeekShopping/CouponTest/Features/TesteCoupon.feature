#language: pt-br
Funcionalidade: Verificar status de cupom

@GetCupom
Cenario: Verificar status de cupom com sucesso
Dado que eu tenho um cupom com status ativo
E estou aunteticado
Quando eu executar o metodo Get no endpoint do cupom
Entao o status do cupom deve ser OK

@GetCupomConteudo
Cenario: Verificar qual o retorno do cupom
Dado que eu tenho um cupom com status ativo
E estou aunteticado
Quando eu executar o metodo Get
Entao o retorno deve ser o desconto
