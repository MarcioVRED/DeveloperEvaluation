# Developer Store API
===========================

Este projeto é uma API para gerenciamento de vendas e clientes na Developer Store. A API cria usuários, registra vendas e aplica descontos automaticamente.

🚀 **Como Utilizar a API**

A API segue uma estrutura RESTful e aceita requisições no formato JSON.

🔐 **Autenticação**

A API pode requerer autenticação via token (caso implementado). Para acessar endpoints protegidos, utilize um token JWT no header Authorization:

Authorization: Bearer <seu_token>

🧑 **Criando um Usuário**

POST /api/users

Cria um novo usuário no sistema.

Exemplo de Requisição:

```json
{
  "name": "Marcio Martins",
  "email": "marcio.martins@email.com",
  "password": "senha123"
}
```

Exemplo de Resposta:

```json
{
  "id": "b1f9c89a-8e3f-4b7b-b0e7-c6a1c3b30db7",
  "name": "Marcio Martins",
  "email": "marcio.martins@email.com"
}
```

🛒 **Criando uma Venda**

POST /api/sales

Registra uma nova venda no sistema.

Exemplo de Requisição:

```json
{
  "customerName": "Marcio Martins",
  "branchName": "Filial SP",
  "saleDate": "2024-02-24T10:00:00Z",
  "items": [
    { "productId": "e2b2e9c6-1a7c-4d8e-913f-834c2dfeb0bd", "productName": "Cerveja Pilsen", "quantity": 10, "unitPrice": 5.00 },
    { "productId": "a1f3c77d-5f5d-4c3e-9c88-bd3a81fcb2c0", "productName": "Refrigerante", "quantity": 3, "unitPrice": 7.50 }
  ]
}
```

📌 **Regras de Desconto**

Se a quantidade de um item for maior ou igual a 4, aplica-se um desconto de 20% no valor total desse item.

Caso contrário, o item não recebe desconto.

Cálculo esperado:

Cerveja Pilsen: 10 unidades × R$5,00 = R$50,00 → Desconto 20% (-R$10,00) → R$40,00

Refrigerante Cola: 3 unidades × R$7,50 = R$22,50 (sem desconto)

💰 Total da Venda: R$62,50

Exemplo de Resposta:

```json
{
  "id": "ad3f913d-8c56-4e75-b9a8-7e21c6a24a9d",
  "customerName": "Marcio Martins",
  "branchName": "Filial SP",
  "saleDate": "2024-02-24T10:00:00Z",
  "isCancelled": false,
  "items": [
    { "productName": "Cerveja IPA", "quantity": 10, "unitPrice": 5.00, "discount": 10.00, "totalItemAmount": 40.00 },
    { "productName": "Refrigerante", "quantity": 3, "unitPrice": 7.50, "discount": 0.00, "totalItemAmount": 22.50 }
  ],
  "totalSaleAmount": 62.50
}
```

❌ **Cancelando uma Venda**

PUT /api/sales/{id}/cancel

Cancela uma venda já registrada.

Exemplo de Requisição:

PUT /api/sales/ad3f913d-8c56-4e75-b9a8-7e21c6a24a9d/cancel

Exemplo de Resposta:

```json
{
  "message": "Sale successfully cancelled."
}
```

##📜 **Listando Vendas**

GET /api/sales

Retorna todas as vendas registradas.

Exemplo de Resposta:

```json
[
  {
    "id": "ad3f913d-8c56-4e75-b9a8-7e21c6a24a9d",
    "customerName": "Marcio Martins",
    "branchName": "Filial SP",
    "saleDate": "2024-02-24T10:00:00Z",
    "isCancelled": false,
    "totalSaleAmount": 62.50
  }
]
```

📌 **Considerações**

A API possui validações para garantir que uma venda não seja criada sem itens e que os campos obrigatórios não estejam vazios.

Caso o saleDate não seja enviado na requisição, a API automaticamente assume a data atual.

Vendas canceladas não podem ser revertidas.

▶️ **Como Executar o Projeto**
###Executando Localmente

1. Clone o repositório:
```sh
git clone https://github.com/seu-repositorio/ambev-developer-store-api.git
cd ambev-developer-store-api
```
2. Instale as dependências e execute o projeto:
```sh
dotnet restore
dotnet run
```
3. A API estará disponível em http://localhost:5000.

###Executando com Docker

1. Certifique-se de ter o Docker instalado.
2. No diretório do projeto, execute:
```sh
docker-compose up --build
```
3. A API estará acessível em http://localhost:5000.

Caso precise parar a execução, utilize:
```sh
docker-compose down
```

🛠 **Tecnologias Utilizadas**

.NET 8

Entity Framework Core

XUnit (Testes automatizados)

Docker (opcional para banco de dados PostgreSQL)

