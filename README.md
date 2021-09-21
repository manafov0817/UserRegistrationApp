# Technologies Used:
- Asp.Net 5
- Swagger
- Fluent Validation
- Serilog
- Auto Mapper
- RabbitMq
- Postgre Sql
- Entity Framework
- PostegreSql Ado.Net

## You will need docker to containerize rabbitmq. 
## Run this command to download and run rabbitmq image

```bash
docker run -d --hostname my-rabbit --name rabbit -p 15672:15672 -p 5672:5672 rabbitmq:3.9-management
```
I have used PostegreSql in project, so you will need procedures. Good news is you can
```bash
 update-database
```
from Data projects and get what you will need. Do not forget to customize your connection string from ConnectionString class.

I made project to run WebUi(WebApi) and Consumer projects at the same time. If you use VsCode or other Code Editor(or IDE) you should consider this
