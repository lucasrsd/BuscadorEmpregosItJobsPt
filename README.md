# RPA Buscador vagas ItJobs (Portugal)

Projeto consiste em um crawler que automatiza a busca de emails de recrutadores de vagas recentes para envios de CVs.

### Pontos de atenção:

• Configurar o appSettings com seu email

• Acessar o site ItJobs e buscar a tag de sua preferência

• Alterar no EmailService o destino, deixei padrão para um email fake para evitar spam nos recrutadores

• Acompanhar logs de console / retorno da API

### Payload exemplo: 

POST http://localhost:5000/api/v1/SearchJobs

{
	"Tag":".net",
	"PageStart": 1,
	"PageEnd": 5
}