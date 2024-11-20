# **CertificateManagement**

Um sistema de gerenciamento de certificados que permite gerar certificados automaticamente para eventos ou cursos, enviar por e-mail, e controlar o progresso dos participantes.

---

## **Sumário**

- [Visão Geral] (#visão-geral)
- [Recursos] (#recursos)
- [Instalação] (#instalação)
- [Configuração] (#configuração)
- [Uso] (#uso)
- [Estrutura do Banco de Dados] (#estrutura-do-banco-de-dados)
- [Contribuição] (#contribuição)
- [Licença] (#licença)

---

## **Visão Geral**

O **CertificateManagement** é uma aplicação desenvolvida para facilitar a gestão de certificados de eventos e cursos. O sistema permite:

- Gerar certificados em formato PDF.
- Enviar certificados automaticamente por e-mail.
- Gerenciar usuários, eventos e progressos.

---

## **Recursos**

- Gerenciamento de usuários, eventos e certificados.
- Geração automática de certificados em PDF.
- Envio de certificados por e-mail com anexo.
- Verificação de conclusão de etapas antes de emitir certificados.
- Exclusão lógica de eventos com preservação de histórico.
- Configuração de exclusão em cascata no banco de dados.

---

## **Instalação**

### **Pré-requisitos**

- **.NET 8 SDK**
- **Banco de Dados**:
  - Microsoft SQL Server
- **Ferramentas Opcionais**:
  - Postman (para testar as APIs)
  - Docker (para rodar o banco de dados em container, se preferir)

### **Clonar o Repositório**

```bash
git clone https://github.com/inoccard/certificatemanagement.git
cd certificatemanagement
```

### **Restaurar Dependências**

```bash
dotnet restore
```

### **Aplicar Migrações do Banco de Dados**

- Ao executar a aplicação, as migrations serão aplicadas automaticamente

---

## **Configuração**

### **Configuração do Banco de Dados**

No arquivo `appsettings.json`, configure a string de conexão:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=CertificateManagementDb;Trusted_Connection=True;"
}
```

### **Configuração de Envio de E-mails**

Adicione as configurações de e-mail no arquivo `appsettings.json`:

```json
"EmailSettings": {
  "SmtpServer": "smtp.gmail.com",
  "Port": 587,
  "SenderEmail": "seu-email@gmail.com",
  "SenderPassword": "sua-senha-de-aplicativo"
}
```

---

## **Uso**

### **Iniciar a Aplicação**

```bash
dotnet run --project src/CertificateManagement.Api
```

A aplicação estará disponível em:

`
http://localhost:5000
https://localhost:7143
`

### **Testar as APIs**

Use ferramentas como Postman ou Swagger para testar os endpoints. Acesse a documentação do Swagger:

`
http://localhost:5000/swagger
`

### **Principais Endpoints**

#### **Usuários**

- **GET** `/api/users` - Lista todos os usuários.
- **POST** `/api/users` - Adiciona um novo usuário.

#### **Eventos**

- **GET** `/api/events` - Lista todos os eventos.
- **POST** `/api/events` - Adiciona um novo evento.

#### **Certificados**

- **GET** `/api/certificates/complete/{userId}/{eventId}` - Lista todos os eventos.
- **POST** `/api/certificates/generate/{eventId}` - Adiciona um novo evento.

### Estrutura do Banco de Dados

#### Principais Entidades

- Users: Representa os participantes.
- Events: Representa os eventos ou cursos.
- Certificates: Representa os certificados gerados.

#### Relacionamentos

- User possui muitos Certificate.
- Event possui muitos Certificate.

### Contribuição

Contribuições são bem-vindas! Siga os passos abaixo:

1. Faça um fork do repositório.
2. Crie uma branch para sua funcionalidade:

```bash
git checkout -b minha-nova-funcionalidade
```

3. Faça o commit das alterações:

```bash
git commit -m "Adiciona nova funcionalidade"
```

4. Faça o push para a branch:

```bash
git push origin minha-nova-funcionalidade
```

5. Abra um Pull Request

