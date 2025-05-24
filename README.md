# Urban-Farm-SQL
Urban Farm é um aplicativo desktop desenvolvido em C# utilizando WPF (.NET), projetado para ajudar no gerenciamento e acompanhamento de operações de agricultura urbana. O aplicativo oferece uma interface intuitiva para monitorar a produção mensal, o crescimento das plantas, o volume colhido e o uso de recursos.

✨ Funcionalidades
📅 Acompanhamento da produção mensal (selecione um mês e insira os dados relevantes)
🌿 Registros de:

Quantidade produzida

Crescimento das plantas

Volume colhido

Uso de recursos

📊 Resumo visual dos dados de produção
💾 Persistência de dados utilizando JSON e SQL Server
🌗 Temas claro e escuro
🎨 Design da interface inspirado em protótipos do Figma
🔒 Sistema de login simples (com opção de login via Google)

🛠️ Tecnologias Utilizadas

C# com WPF (.NET)

XAML para design da interface

Armazenamento local de dados via JSON

Microsoft SQL Server para gerenciamento centralizado e estruturado de dados

📁 Armazenamento de Arquivos e Dados
O Urban Farm adota uma abordagem híbrida de persistência de dados:

Arquivos JSON: Utilizados para acesso rápido às preferências locais do usuário, estado da interface e operações com prioridade offline. Garantem uma persistência leve sem necessidade de conexão com banco de dados.

SQL Server: Utilizado para armazenar dados estruturados como contas de usuários, registros de produção, estatísticas mensais e históricos. Proporciona uma forma mais escalável e segura de gerenciar dados, especialmente em ambientes multiusuário ou em rede.

Os usuários podem continuar utilizando o aplicativo sem acesso à internet, com base nos arquivos JSON locais. Quando conectados ao SQL Server, os dados são sincronizados ou recuperados conforme necessário, permitindo consultas poderosas, geração de relatórios e expansão futura.

🌤️ Este aplicativo utiliza a API Open-Meteo, que é gratuita e não requer chave de autenticação.
