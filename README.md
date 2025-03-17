In this project, we are going to build a inventory management system with .net core (MVC), sql server. If a product's inventory is lower than its safety stock level, the system would send an email to notify the PIC to reorder. By leveraging background jobs (could be performed by Hangfire or Sql server Agent), we could trace our inventory level on a real-time basis. If we want to implement real-time notification, we could leverage signalR. We are going to simulate customer purchase, inventory order & delevery in this case

![image](https://github.com/user-attachments/assets/6406c267-287f-4dd3-b120-1c9232922158)

![image](https://github.com/user-attachments/assets/69448666-7380-4342-beb2-1f9db8c36252)

![image](https://github.com/user-attachments/assets/52034dbc-f5fd-4cc0-a4d2-95c44fff73d6)

![image](https://github.com/user-attachments/assets/b87dad0f-69f9-4c74-920a-621dc35acc17)

