# Лабораторна 3

Мета роботи: Покращити проект шляхом валідації вхідних даних, обробки помилок та використання ORM з базою даних

Виконала студентка групи ІО-32 Куліченко Софія

Варіант додаткового завдання 32%3 = 2 (Користувацькі категорії витрат)

## Зміст

- [Features](#features)
- [TechStack](#techstack)
- [Локальний запуск](#локальний-запуск)
- [Deploy](#deploy)
- [Usage](#usage)
- [Endpoints API](#endpoints-api)

---

## Features 

API задеплоєно на Render.com. Ви можете переглянути всю документацію та протестувати ендпоінти в реальному часі за посиланням на Swagger UI:

https://be-lab3-sem5.onrender.com/index.html

---

## TechStack

* **.NET 8** 
* **ASP.NET Core**
* **PostgreSQL** 
* **Swashbuckle (Swagger)** 

---

## Локальний запуск

Для того, щоб запустити цей проєкт локально, виконайте наступні кроки.

### **Передумови**

Переконайтесь, що у вас встановлено наступне програмне забезпечення:

1.  **[.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)**
2.  **[Git](https://git-scm.com/)**
3.  **[PostgreSQL Server](https://www.postgresql.org/download/)**
4.  **Будь-який клієнт для керування БД (наприклад, [pgAdmin](https://www.pgadmin.org/) або [DBeaver](https://dbeaver.io/))**
5.  **[.NET user-secrets інструмент](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-9.0&tabs=windows)**

### **Інструкції для запуску**

1.  **Клонуйте репозиторій:**
    Відкрийте термінал або командний рядок і виконайте команду:
    ```bash
    git clone https://github.com/SonikLvl/BE_lab2_sem5.git
    ```

2.  **Налаштування Бази Даних (PostgreSQL)**
    Відкрийте pgAdmin або інший клієнт баз даних.
    Створіть нову, порожню базу даних для проекту.

3.  **Налаштування Конфігурації (User Secrets)**
    Переконайтеся, що ви знаходитесь у директорії проекту (там, де лежить .csproj файл).
    Ініціалізуйте user secrets:
    ```bash
    dotnet user-secrets init
    ```
    Встановіть рядок підключення (Connection String).
    ```bash
    dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Database=finance_db_local;Username=ВАШ_USERNAME;Password=ВАШ_ПАРОЛЬ"
    ```

4.  **Застосування міграцій та запуск**
    Запустіть проект:
    ```bash
    dotnet run
    ```
    
    Проект налаштований на автоматичне створення міграцій але їх можна зробити вручну за допомогою:
    ```bash
    dotnet ef database update
    ```
    
4.  **Перевірка**
    API буде запущено на локальному хості (наприклад, http://localhost:5123 або https://localhost:7123). Точну адресу ви побачите у консолі.
    За цим посиланням буде відкрито Swagger UI.
    
---

## Deploy

Якщо немає бажання підіймати АПІ локально, то проект задеплоєний за посиланням [https://be-lab3-sem5.onrender.com/](https://be-lab3-sem5.onrender.com/index.html).

---

## Usage

Ви можете тестувати API за допомогою будь-якого клієнта, наприклад, **[Postman](https://www.postman.com/)**.

Базова URL-адреса для запитів: `https://localhost:<PORT_NUMBER>`

### **Приклад тестування в Postman**

Для зручного тестування ви можете використати наданий **Postman Flow**.

<img width="1669" height="1121" alt="Screenshot 2025-11-05 180237" src="https://github.com/user-attachments/assets/2f9e8513-4f8c-424b-9cc2-4b20605f0b03" />


---

## Endpoints API

Нижче наведено список доступних ендпоінтів.
Http запит повинен починатись з {{baseURL}}/api/....

| Метод  | Шлях                                            | Опис                                                                                             |
| :----- | :---------------------------------------------- | :-------------------------------------------------------------------------------------------- |
 | User 
| `POST` | `/user`                                        | Створює нового користувача.                                                         |
| `GET`  | `/user/users`                                  | Отримує список усіх користувачів.                                                     |
| `GET`  | `/user/{id}`                                   | Отримує користувача за його `id`.                                                       |
| `DELETE`| `/user/{id}`                                   | Видаляє користувача за його `id`.                                                   |
|  Category 
| `POST`  | `/category?userId={user_id}`                 | Створює нову категорію для юзера (відсутній юзер рівняється публічній категорії).     |
| `GET`  | `/category/all`                 | Отримує список усіх категорій.                |
| `GET`  | `/category/allById?userId=1`                 | Отримує список усіх категорій для юзера (включно з публічними).                |
| `GET`  | `/category/{id}/{user_id}`                 | Отримує категорію за юзером (включно з публічними).             |
| `GET`  | `/category/{user_id}?categoryName={category_name}`                 | Отримує категорію за юзером та ім'ям (включно з публічними).               |
| `DELETE`| `/category/{id}/{user_id}`                    | Видаляє категорію за її `id` (потребує юзера для валідації).                             |
|  Record 
| `POST` | `/record`                                       | Створює новий запис про витрати.                                                         |
| `GET`  | `/record/{id}`                                  | Отримує запис за його `id`.                                                            |
| `DELETE`| `/record/{id}?userId={user_id}`                | Видаляє запис за його `id`.  (потребує юзера для валідації)                     |
| `GET`  | `/record?userId={id}&categoryId={id}`    | Отримує записи, відфільтровані за `userId` та/або `categoryId`. |

---
